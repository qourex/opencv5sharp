// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class FaceDetectionSample
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ModelUrl = "https://github.com/opencv/opencv_zoo/raw/main/models/face_detection_yunet/face_detection_yunet_2023mar.onnx";
        private const string ModelPath = "face_detection_yunet.onnx";

        public static void Run()
        {
            Console.WriteLine("--- [8] DNN Face & Landmark Detection (YuNet ONNX) ---");

            // Ensure model is downloaded
            if (!File.Exists(ModelPath))
            {
                Console.WriteLine($"\nYuNet face detector model not found locally.");
                DownloadModel(ModelUrl, ModelPath);
            }
            else
            {
                Console.WriteLine($"\nFound YuNet model at: {ModelPath}");
            }

            // Attempt webcam or fallback to synthetic image
            using (var capture = new VideoCapture(0, 0))
            {
                if (capture.IsOpened())
                {
                    Console.WriteLine("\nWebcam detected. Running live face detection for 30 frames...");

                    double width = capture.Get((int)VideoCaptureProperties.FrameWidth);
                    double height = capture.Get((int)VideoCaptureProperties.FrameHeight);
                    if (width <= 0) width = 640;
                    if (height <= 0) height = 480;

                    int backend = 0;
                    int target = 0;
                    try
                    {
                        if (Cv2.CudaGetCudaEnabledDeviceCount() > 0)
                        {
                            backend = (int)DnnBackend.Cuda;
                            target = (int)DnnTarget.Cuda;
                            Console.WriteLine("   CUDA detected! Enabling GPU acceleration for FaceDetectorYN...");
                        }
                    }
                    catch { }

                    using (var detector = FaceDetectorYN.Create(ModelPath, "", new Size((int)width, (int)height), 0.6f, 0.3f, 5000, backend, target))
                    {
                        if (detector == null)
                        {
                            Console.WriteLine("   [ERROR] Failed to create FaceDetectorYN.");
                            return;
                        }

                        using (var frame = new Mat())
                        using (var faces = new Mat())
                        {
                            for (int i = 1; i <= 30; i++)
                            {
                                if (capture.Read(frame) && frame.Handle != IntPtr.Zero)
                                {
                                    detector.Detect(frame, faces);
                                    PrintFaceDetails($"Webcam Frame {i}/30", faces);
                                }
                                System.Threading.Thread.Sleep(50);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n[INFO] Webcam not detected. Skipping live feed.");
                }
            }

            Console.WriteLine("\nRunning verification on synthetic face pattern...");
            RunSyntheticFallback();

            Console.WriteLine("\nFace detection sample completed.");
        }

        private static void RunSyntheticFallback()
        {
            string localLena = "lena.jpg";
            string repoLena = @"opencv_prebuilt/opencv/sources/doc/js_tutorials/js_assets/lena.jpg";

            string selectedPath = "";
            if (File.Exists(localLena))
            {
                selectedPath = localLena;
            }
            else if (File.Exists(repoLena))
            {
                selectedPath = repoLena;
            }
            else
            {
                string url = "https://raw.githubusercontent.com/opencv/opencv/master/samples/data/lena.jpg";
                Console.WriteLine($"   Lena image not found. Downloading from: {url}");
                DownloadModel(url, localLena);
                selectedPath = localLena;
            }

            using (var img = Cv2.Imread(selectedPath, (int)ImreadModes.Color))
            {
                if (img.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to load Lena image for face detection verification.");
                    return;
                }

                int backend = 0;
                int target = 0;
                try
                {
                    if (Cv2.CudaGetCudaEnabledDeviceCount() > 0)
                    {
                        backend = (int)DnnBackend.Cuda;
                        target = (int)DnnTarget.Cuda;
                        Console.WriteLine("   CUDA detected! Enabling GPU acceleration for FaceDetectorYN...");
                    }
                }
                catch { }

                using (var detector = FaceDetectorYN.Create(ModelPath, "", new Size(img.Cols, img.Rows), 0.5f, 0.3f, 5000, backend, target))
                {
                    if (detector == null)
                    {
                        Console.WriteLine("   [ERROR] Failed to create FaceDetectorYN.");
                        return;
                    }

                    using (var faces = new Mat())
                    {
                        detector.Detect(img, faces);
                        PrintFaceDetails("Lena Verification Image", faces);
                    }
                }
            }
        }

        private static void PrintFaceDetails(string sourceName, Mat faces)
        {
            int numFaces = faces.Rows;
            if (numFaces <= 0)
            {
                Console.WriteLine($"   [{sourceName}] No faces detected.");
                return;
            }

            int cols = faces.Cols; // Output is 15 columns
            int totalFloats = numFaces * cols;
            float[] data = new float[totalFloats];
            Marshal.Copy(faces.Data, data, 0, totalFloats);

            Console.WriteLine($"   [{sourceName}] Detected {numFaces} face(s):");
            for (int i = 0; i < numFaces; i++)
            {
                int offset = i * cols;
                float x = data[offset + 0];
                float y = data[offset + 1];
                float w = data[offset + 2];
                float h = data[offset + 3];
                float score = data[offset + 14];

                // Landmarks
                float reX = data[offset + 4]; float reY = data[offset + 5]; // Right eye
                float leX = data[offset + 6]; float leY = data[offset + 7]; // Left eye
                float noseX = data[offset + 8]; float noseY = data[offset + 9]; // Nose tip

                Console.WriteLine($"      Face [{i}]: Rect=[x:{x:F1}, y:{y:F1}, w:{w:F1}, h:{h:F1}], Confidence={score:P1}");
                Console.WriteLine($"         Landmarks: RightEye=[{reX:F1}, {reY:F1}], LeftEye=[{leX:F1}, {leY:F1}], Nose=[{noseX:F1}, {noseY:F1}]");
            }
        }

        private static void DownloadModel(string url, string path)
        {
            Console.WriteLine($"   Downloading YuNet (337 KB) from: {url}");
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
                throw new Exception($"Failed to download YuNet model: {ex.Message}", ex);
            }
        }

        private static void DrawCircle(byte[] pixels, int width, int height, int cx, int cy, int radius, byte b, byte g, byte r)
        {
            for (int y = cy - radius; y <= cy + radius; y++)
            {
                for (int x = cx - radius; x <= cx + radius; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        int dx = x - cx;
                        int dy = y - cy;
                        if (dx * dx + dy * dy <= radius * radius)
                        {
                            int idx = (y * width + x) * 3;
                            pixels[idx] = b;
                            pixels[idx + 1] = g;
                            pixels[idx + 2] = r;
                        }
                    }
                }
            }
        }

        private static void DrawRect(byte[] pixels, int width, int height, int rx, int ry, int rw, int rh, byte b, byte g, byte r)
        {
            for (int y = ry; y <= ry + rh; y++)
            {
                for (int x = rx; x <= rx + rw; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        int idx = (y * width + x) * 3;
                        pixels[idx] = b;
                        pixels[idx + 1] = g;
                        pixels[idx + 2] = r;
                    }
                }
            }
        }
    }
}
