// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class ImageProcessing
    {
        public static void Run()
        {
            Console.WriteLine("--- [2] Image Processing Pipeline & Drawing ---");

            string inputPath = "sample_input.png";
            string outputPath = "sample_output.png";

            // 1. Generate a synthetic image to draw on and process
            Console.WriteLine("\n1. Creating a synthetic input image with shapes and text...");
            const int CV_8UC3 = 64;
            using (Mat src = new Mat(300, 300, CV_8UC3))
            {
                // Clear image to a custom color (BGR: dark grey 50, 50, 50)
                int bufferSize = src.Rows * src.Cols * src.Channels();
                byte[] bg = new byte[bufferSize];
                for (int i = 0; i < bufferSize; i += 3)
                {
                    bg[i] = 50;     // Blue
                    bg[i + 1] = 50; // Green
                    bg[i + 2] = 50; // Red
                }
                Marshal.Copy(bg, 0, src.Data, bufferSize);

                // Draw a line and a circle
                Console.WriteLine("   Drawing line onto Mat...");
                byte[] pixels = new byte[bufferSize];
                Marshal.Copy(src.Data, pixels, 0, bufferSize);
                for (int i = 0; i < 300; i++)
                {
                    // Draw a white diagonal line (BGR: 255, 255, 255)
                    int idx = (i * 300 + i) * 3;
                    pixels[idx] = 255;
                    pixels[idx + 1] = 255;
                    pixels[idx + 2] = 255;

                    // Draw a red horizontal line in the middle (BGR: 0, 0, 255)
                    int midIdx = (150 * 300 + i) * 3;
                    pixels[midIdx] = 0;
                    pixels[midIdx + 1] = 0;
                    pixels[midIdx + 2] = 255;
                }
                Marshal.Copy(pixels, 0, src.Data, bufferSize);

                // Save this as the input image
                Cv2.Imwrite(inputPath, src, IntPtr.Zero);
                Console.WriteLine($"   Saved synthetic image to: {inputPath}");
            }

            // 2. Read image back
            Console.WriteLine($"\n2. Loading image from disk using Imread...");
            using (Mat img = Cv2.Imread(inputPath, (int)ImreadModes.Color))
            {
                if (img.Handle == IntPtr.Zero)
                {
                    throw new Exception("Failed to load image.");
                }
                Console.WriteLine($"   Loaded image size: {img.Cols}x{img.Rows}, Channels: {img.Channels()}");

                // 3. Convert to Grayscale
                Console.WriteLine("\n3. Converting to Grayscale using CvtColor...");
                using (Mat gray = new Mat())
                {
                    Cv2.CvtColor(img, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                    Console.WriteLine($"   Grayscale Channels: {gray.Channels()}");

                    // 4. Gaussian Blur
                    Console.WriteLine("\n4. Applying Gaussian Blur filter...");
                    using (Mat blurred = new Mat())
                    {
                        Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, 4, AlgorithmHint.Default);

                        // 5. Canny Edge Detection
                        Console.WriteLine("\n5. Running Canny Edge Detection...");
                        using (Mat edges = new Mat())
                        {
                            Cv2.Canny(blurred, edges, 50, 150, 3, false);

                            // Save output
                            Cv2.Imwrite(outputPath, edges, IntPtr.Zero);
                            Console.WriteLine($"   Saved processed edge output to: {outputPath}");
                        }
                    }
                }
            }

            // Clean up files
            try
            {
                if (File.Exists(inputPath)) File.Delete(inputPath);
                if (File.Exists(outputPath)) File.Delete(outputPath);
                Console.WriteLine("\nTemporary files cleaned up.");
            }
            catch { }

            Console.WriteLine("Image Processing sample completed successfully.");
        }
    }
}
