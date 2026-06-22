// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class DnnClassificationSample
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ModelUrl = "https://github.com/onnx/models/raw/main/validated/vision/classification/squeezenet/model/squeezenet1.1-7.onnx";
        private const string ModelPath = "squeezenet.onnx";

        public static void Run()
        {
            Console.WriteLine("--- [7] DNN Image Classification (SqueezeNet ONNX) ---");

            // Ensure model is downloaded
            if (!File.Exists(ModelPath))
            {
                Console.WriteLine($"\nSqueezeNet model not found locally.");
                DownloadModel(ModelUrl, ModelPath);
            }
            else
            {
                Console.WriteLine($"\nFound SqueezeNet model at: {ModelPath}");
            }

            // Create a synthetic test image (drawing a circle)
            string imagePath = "classification_test_input.png";
            CreateSyntheticInputImage(imagePath);

            try
            {
                Console.WriteLine("\n1. Loading ONNX network...");
                using (DnnNet? net = Cv2.DnnReadNetFromONNX(ModelPath, 1))
                {
                    if (net == null)
                    {
                        Console.WriteLine("   [ERROR] Failed to load ONNX model.");
                        return;
                    }

                    Console.WriteLine("   Network loaded successfully.");

                    Console.WriteLine("\n2. Preprocessing input image...");
                    using (Mat img = Cv2.Imread(imagePath, (int)ImreadModes.Color))
                    {
                        if (img.Handle == IntPtr.Zero)
                        {
                            Console.WriteLine("   [ERROR] Failed to load test image.");
                            return;
                        }

                        // SqueezeNet expects 224x224 input size, SwapRedBlue=true
                        using (Mat? blob = Cv2.DnnBlobFromImage(
                            img,
                            1.0 / 255.0,
                            new Size(224, 224),
                            new Scalar(123.675, 116.28, 103.53, 0),
                            true,
                            false,
                            5))
                        {
                            if (blob == null || blob.Handle == IntPtr.Zero)
                            {
                                Console.WriteLine("   [ERROR] Failed to create blob from image.");
                                return;
                            }

                            Console.WriteLine($"   Blob shape size: {blob.Cols}x{blob.Rows}, Channels: {blob.Channels()}");

                            Console.WriteLine("\n3. Running network forward inference pass...");
                            net.SetInput(blob, "", 1.0, new Scalar(0, 0, 0, 0));
                            using (Mat? prob = net.Forward(""))
                            {
                                if (prob == null || prob.Handle == IntPtr.Zero)
                                {
                                    Console.WriteLine("   [ERROR] Forward pass returned null.");
                                    return;
                                }

                                Console.WriteLine($"   Output probability matrix size: {prob.Cols}x{prob.Rows}, Channels: {prob.Channels()}");

                                // Parse outputs using MinMaxLoc
                                IntPtr minValPtr = Marshal.AllocHGlobal(sizeof(double));
                                IntPtr maxValPtr = Marshal.AllocHGlobal(sizeof(double));
                                IntPtr minLocPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Point>());
                                IntPtr maxLocPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Point>());
                                try
                                {
                                    Cv2.MinMaxLoc(prob, minValPtr, maxValPtr, minLocPtr, maxLocPtr, null);
                                    double confidence = Marshal.PtrToStructure<double>(maxValPtr);
                                    Point maxLoc = Marshal.PtrToStructure<Point>(maxLocPtr);

                                    // For a 1x1000 classification probability matrix, the X coordinate of the max location point is the class ID.
                                    int classId = maxLoc.X;

                                    Console.WriteLine($"\n[RESULT] Top Class ID: {classId}");
                                    Console.WriteLine($"[RESULT] Raw Score: {confidence:F4}");
                                }
                                finally
                                {
                                    Marshal.FreeHGlobal(minValPtr);
                                    Marshal.FreeHGlobal(maxValPtr);
                                    Marshal.FreeHGlobal(minLocPtr);
                                    Marshal.FreeHGlobal(maxLocPtr);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] An exception occurred during DNN execution: {ex.Message}");
            }
            finally
            {
                // Cleanup test image
                try
                {
                    if (File.Exists(imagePath)) File.Delete(imagePath);
                }
                catch { }
            }

            Console.WriteLine("\nDNN classification sample completed.");
        }

        private static void DownloadModel(string url, string path)
        {
            Console.WriteLine($"   Downloading SqueezeNet (5 MB) from: {url}");
            Console.WriteLine("   Please wait, downloading...");

            try
            {
                using (var response = _httpClient.GetAsync(url).GetAwaiter().GetResult())
                {
                    response.EnsureSuccessStatusCode();
                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        response.Content.CopyToAsync(fs).GetAwaiter().GetResult();
                    }
                }
                Console.WriteLine("   Model download complete!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to download model file: {ex.Message}", ex);
            }
        }

        private static void CreateSyntheticInputImage(string path)
        {
            const int size = 300;
            const int CV_8UC3 = 64;
            using (Mat src = new Mat(size, size, CV_8UC3))
            {
                int bufferSize = src.Rows * src.Cols * src.Channels();
                byte[] bg = new byte[bufferSize];

                // Light grey background
                for (int i = 0; i < bufferSize; i++) bg[i] = 200;
                Marshal.Copy(bg, 0, src.Data, bufferSize);

                // Draw a simple dark blue block in the center (simulating an object)
                byte[] pixels = new byte[bufferSize];
                Marshal.Copy(src.Data, pixels, 0, bufferSize);
                for (int r = 80; r < 220; r++)
                {
                    for (int c = 80; c < 220; c++)
                    {
                        int idx = (r * size + c) * 3;
                        pixels[idx] = 120;     // Blue
                        pixels[idx + 1] = 50;  // Green
                        pixels[idx + 2] = 50;  // Red
                    }
                }
                Marshal.Copy(pixels, 0, src.Data, bufferSize);

                Cv2.Imwrite(path, src, IntPtr.Zero);
            }
        }
    }
}
