// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class BackgroundSegmentationSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [6] Live Video Background Segmentation (MOG2) ---");

            using (var bgSubtractor = Cv2.CreateBackgroundSubtractorMOG2(500, 16.0, true))
            {
                if (bgSubtractor == null)
                {
                    Console.WriteLine("   [ERROR] Failed to create BackgroundSubtractorMOG2.");
                    return;
                }

                Console.WriteLine("   MOG2 Background Subtractor created successfully.");

                // Attempt to open the camera stream
                using (var capture = new VideoCapture(0, 0))
                {
                    if (capture.IsOpened())
                    {
                        Console.WriteLine("\nWebcam detected. Running live background segmentation for 30 frames...");
                        using (var frame = new Mat())
                        using (var fgMask = new Mat())
                        {
                            for (int i = 1; i <= 30; i++)
                            {
                                if (capture.Read(frame) && frame.Handle != IntPtr.Zero)
                                {
                                    bgSubtractor.Apply(frame, fgMask, -1.0);

                                    // Count non-zero pixels in foreground mask (motion level)
                                    int motionPixels = Cv2.CountNonZero(fgMask);
                                    Console.WriteLine($"   Frame [{i}/30]: Size={frame.Cols}x{frame.Rows}, Foreground Pixels={motionPixels}");
                                }
                                System.Threading.Thread.Sleep(50);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[INFO] Webcam not detected. Running fallback segmentation on synthetic moving frames...");
                        RunSyntheticFallback(bgSubtractor);
                    }
                }
            }

            Console.WriteLine("\nBackground segmentation sample completed.");
        }

        private static void RunSyntheticFallback(BackgroundSubtractorMOG2 bgSubtractor)
        {
            const int width = 320;
            const int height = 240;
            const int CV_8UC3 = 64;

            using (var frame = new Mat(height, width, CV_8UC3))
            using (var fgMask = new Mat())
            {
                // Clear frame to black initially
                byte[] pixels = new byte[width * height * 3];
                Marshal.Copy(pixels, 0, frame.Data, pixels.Length);

                for (int step = 1; step <= 10; step++)
                {
                    // Generate motion: draw a white moving square (BGR: 255, 255, 255)
                    // The square moves across the frame
                    int boxX = step * 20;
                    int boxY = 80;
                    int boxSize = 50;

                    // Clear pixels
                    Array.Clear(pixels, 0, pixels.Length);

                    // Draw moving box
                    for (int y = boxY; y < boxY + boxSize; y++)
                    {
                        for (int x = boxX; x < boxX + boxSize; x++)
                        {
                            if (x >= 0 && x < width && y >= 0 && y < height)
                            {
                                int idx = (y * width + x) * 3;
                                pixels[idx] = 255;     // Blue
                                pixels[idx + 1] = 255; // Green
                                pixels[idx + 2] = 255; // Red
                            }
                        }
                    }
                    Marshal.Copy(pixels, 0, frame.Data, pixels.Length);

                    // Apply background subtraction
                    bgSubtractor.Apply(frame, fgMask, -1.0);

                    int motionPixels = Cv2.CountNonZero(fgMask);
                    Console.WriteLine($"   Synthetic Frame [{step}/10]: Box X={boxX}, Foreground Pixels={motionPixels}");
                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}
