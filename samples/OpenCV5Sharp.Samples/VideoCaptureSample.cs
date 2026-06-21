// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class VideoCaptureSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [3] VideoIO & Camera Stream ---");

            Console.WriteLine("\nOpening VideoCapture on device 0 (Webcam)...");
            using (VideoCapture capture = new VideoCapture(0, 0))
            {
                if (!capture.IsOpened())
                {
                    Console.WriteLine("  [INFO] Webcam device 0 is not available on this machine.");
                    Console.WriteLine("  In a real application with a camera attached, this opens the webcam stream.");
                    return;
                }

                // Retrieve video properties
                double width = capture.Get((int)VideoCaptureProperties.FrameWidth);
                double height = capture.Get((int)VideoCaptureProperties.FrameHeight);
                double fps = capture.Get((int)VideoCaptureProperties.Fps);

                Console.WriteLine($"   Camera Stream Info: {width}x{height} @ {fps} FPS");

                // Capture a few frames as a demo
                Console.WriteLine("\nReading 10 frames from camera feed...");
                using (Mat frame = new Mat())
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        bool success = capture.Read(frame);
                        if (success && frame.Handle != IntPtr.Zero)
                        {
                            Console.WriteLine($"   Frame [{i}]: Size={frame.Cols}x{frame.Rows}, Channels={frame.Channels()}");
                        }
                        else
                        {
                            Console.WriteLine($"   Frame [{i}]: Failed to read frame.");
                        }
                        System.Threading.Thread.Sleep(100); // Simulate processing time
                    }
                }

                capture.Release();
                Console.WriteLine("\nCamera stream released successfully.");
            }

            Console.WriteLine("VideoCapture sample completed.");
        }
    }
}
