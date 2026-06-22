// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class CornerDetectionSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [10] Corner & Feature Detection ---");

            using (var capture = new VideoCapture(0, 0))
            {
                if (capture.IsOpened())
                {
                    Console.WriteLine("\nWebcam detected. Running Corner Detection for 30 frames...");
                    using (var frame = new Mat())
                    {
                        for (int i = 1; i <= 30; i++)
                        {
                            if (capture.Read(frame) && frame.Handle != IntPtr.Zero)
                            {
                                DetectAndDrawCorners($"Webcam Frame {i}/30", frame);
                            }
                            System.Threading.Thread.Sleep(80);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n[INFO] Webcam not detected. Skipping live feed.");
                }
            }

            Console.WriteLine("\nRunning corner detection on synthetic geometric image...");
            RunSyntheticVerification();

            Console.WriteLine("\nCorner detection sample completed.");
        }

        private static void DetectAndDrawCorners(string name, Mat frame)
        {
            const int CV_8UC1 = 0;

            // Convert to grayscale
            using (var gray = new Mat())
            {
                if (frame.Channels() == 3)
                {
                    Cv2.CvtColor(frame, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                }
                else
                {
                    frame.CopyTo(gray);
                }

                // 1. Shi-Tomasi Corner Detection (Good Features to Track)
                using (var corners = new Mat())
                using (var shiTomasiOutput = frame.Clone())
                {
                    if (shiTomasiOutput != null)
                    {
                        // Parameters: maxCorners=50, qualityLevel=0.01, minDistance=10
                        Cv2.GoodFeaturesToTrack(gray, corners, 50, 0.01, 10, null, 3, false, 0.04);

                        int count = corners.Rows > corners.Cols ? corners.Rows : corners.Cols;
                        if (count > 0)
                        {
                            float[] cornerData = new float[count * 2];
                            Marshal.Copy(corners.Data, cornerData, 0, cornerData.Length);

                            for (int i = 0; i < count; i++)
                            {
                                float x = cornerData[i * 2];
                                float y = cornerData[i * 2 + 1];
                                Cv2.Circle(shiTomasiOutput, new Point((int)x, (int)y), 5, new Scalar(0, 255, 0), -1, 8, 0); // Green circle
                            }
                        }

                        Console.WriteLine($"   [{name}] Shi-Tomasi: Found {count} corners.");
                        Cv2.Imwrite("corner_shitomasi_output.png", shiTomasiOutput, IntPtr.Zero);
                    }
                }

                // 2. Harris Corner Detection
                using (var harrisResponse = new Mat())
                using (var harrisOutput = frame.Clone())
                {
                    if (harrisOutput != null)
                    {
                        // Parameters: blockSize=2, ksize=3, k=0.04
                        Cv2.CornerHarris(gray, harrisResponse, 2, 3, 0.04, (int)BorderTypes.Default);

                        // Normalize harrisResponse to 0-255
                        using (var norm = new Mat())
                        using (var normScaled = new Mat())
                        {
                            Cv2.Normalize(harrisResponse, norm, 0, 255, (int)NormTypes.Minmax, -1, null);
                            norm.ConvertTo(normScaled, CV_8UC1, 1.0, 0.0);

                            int rows = normScaled.Rows;
                            int cols = normScaled.Cols;
                            byte[] data = new byte[rows * cols];
                            Marshal.Copy(normScaled.Data, data, 0, data.Length);

                            int harrisCount = 0;
                            for (int r = 0; r < rows; r++)
                            {
                                for (int c = 0; c < cols; c++)
                                {
                                    if (data[r * cols + c] > 180) // Corner threshold
                                    {
                                        Cv2.Circle(harrisOutput, new Point(c, r), 5, new Scalar(0, 0, 255), 1, 8, 0); // Red circle contour
                                        harrisCount++;
                                    }
                                }
                            }
                            Console.WriteLine($"   [{name}] Harris: Visualized {harrisCount} potential corner pixels.");
                            Cv2.Imwrite("corner_harris_output.png", harrisOutput, IntPtr.Zero);
                        }
                    }
                }
            }
        }

        private static void RunSyntheticVerification()
        {
            const int CV_8UC3 = 64;
            const int size = 300;

            // Generate synthetic chessboard-like image with shapes
            using (var img = new Mat(size, size, CV_8UC3))
            {
                // Clear to white
                byte[] bg = new byte[size * size * 3];
                for (int i = 0; i < bg.Length; i++) bg[i] = 255;
                Marshal.Copy(bg, 0, img.Data, bg.Length);

                // Draw black rectangles
                Cv2.Rectangle(img, new Point(50, 50), new Point(150, 150), new Scalar(0, 0, 0), -1, 8, 0);
                Cv2.Rectangle(img, new Point(180, 100), new Point(250, 250), new Scalar(100, 100, 100), -1, 8, 0);

                // Draw a cross
                Cv2.Line(img, new Point(30, 200), new Point(130, 200), new Scalar(50, 50, 50), 10, 8, 0);
                Cv2.Line(img, new Point(80, 150), new Point(80, 250), new Scalar(50, 50, 50), 10, 8, 0);

                DetectAndDrawCorners("Synthetic", img);
            }
        }
    }
}
