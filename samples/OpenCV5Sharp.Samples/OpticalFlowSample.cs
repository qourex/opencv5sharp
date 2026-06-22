// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class OpticalFlowSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [14] Sparse Optical Flow (Lucas-Kanade) ---");

            // Live Webcam flow tracking attempt
            using (var capture = new VideoCapture(0, 0))
            {
                if (capture.IsOpened())
                {
                    Console.WriteLine("\nWebcam detected. Running live Lucas-Kanade optical flow for 30 frames...");
                    RunLiveOpticalFlow(capture);
                }
                else
                {
                    Console.WriteLine("\n[INFO] Webcam not detected. Skipping live feed.");
                }
            }

            Console.WriteLine("\nRunning verification on synthetic moving dot frames...");
            RunSyntheticVerification();

            Console.WriteLine("\nOptical Flow sample completed.");
        }

        private static void RunLiveOpticalFlow(VideoCapture capture)
        {
            using (var prevFrame = new Mat())
            using (var prevGray = new Mat())
            {
                if (!capture.Read(prevFrame) || prevFrame.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to read first frame from camera.");
                    return;
                }

                Cv2.CvtColor(prevFrame, prevGray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);

                // Detect initial features to track
                using (var prevPts = new Mat())
                {
                    Cv2.GoodFeaturesToTrack(prevGray, prevPts, 50, 0.01, 10, null, 3, false, 0.04);
                    int ptsCount = prevPts.Rows > prevPts.Cols ? prevPts.Rows : prevPts.Cols;
                    if (ptsCount == 0)
                    {
                        Console.WriteLine("   [INFO] No prominent features found on camera feed to track.");
                        return;
                    }

                    Console.WriteLine($"   Detected {ptsCount} initial features to track.");

                    using (var nextFrame = new Mat())
                    using (var nextGray = new Mat())
                    using (var nextPts = new Mat())
                    using (var status = new Mat())
                    using (var err = new Mat())
                    {
                        for (int f = 1; f <= 30; f++)
                        {
                            if (!capture.Read(nextFrame) || nextFrame.Handle == IntPtr.Zero)
                                break;

                            Cv2.CvtColor(nextFrame, nextGray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);

                            // Run Lucas-Kanade Sparse Optical Flow
                            Cv2.CalcOpticalFlowPyrLK(
                                prevGray, nextGray, prevPts, nextPts, status, err,
                                new Size(15, 15), 3, new TermCriteria(3, 30, 0.01), 0, 1e-4
                            );

                            int trackedCount = 0;
                            int totalPoints = nextPts.Rows > nextPts.Cols ? nextPts.Rows : nextPts.Cols;
                            if (totalPoints > 0)
                            {
                                byte[] statusBytes = new byte[totalPoints];
                                Marshal.Copy(status.Data, statusBytes, 0, totalPoints);
                                for (int i = 0; i < totalPoints; i++)
                                {
                                    if (statusBytes[i] == 1) trackedCount++;
                                }
                            }

                            Console.WriteLine($"   [Frame {f}/30] Successfully tracked {trackedCount}/{ptsCount} points.");

                            // Prepare for next iteration
                            nextGray.CopyTo(prevGray);
                            nextPts.CopyTo(prevPts);
                            ptsCount = trackedCount;

                            if (ptsCount < 5)
                            {
                                Console.WriteLine("   [INFO] Tracked points dropped below threshold. Re-detecting features...");
                                Cv2.GoodFeaturesToTrack(prevGray, prevPts, 50, 0.01, 10, null, 3, false, 0.04);
                                ptsCount = prevPts.Rows > prevPts.Cols ? prevPts.Rows : prevPts.Cols;
                            }

                            System.Threading.Thread.Sleep(50);
                        }
                    }
                }
            }
        }

        private static void RunSyntheticVerification()
        {
            const int CV_8UC1 = 0;
            const int CV_32FC2 = 37;
            const int size = 200;

            // Generate Frame 1: Black background with a white circle at (80, 80)
            using (var frame1 = new Mat(size, size, CV_8UC1))
            using (var frame2 = new Mat(size, size, CV_8UC1))
            {
                // Clear images to black
                byte[] zeros = new byte[size * size];
                Marshal.Copy(zeros, 0, frame1.Data, zeros.Length);
                Marshal.Copy(zeros, 0, frame2.Data, zeros.Length);

                // Draw circles (displaced by 5 pixels horizontally and 3 pixels vertically)
                Cv2.Circle(frame1, new Point(80, 80), 8, new Scalar(255), -1, 8, 0);
                Cv2.Circle(frame2, new Point(85, 83), 8, new Scalar(255), -1, 8, 0);

                Cv2.Imwrite("optical_flow_frame1.png", frame1, IntPtr.Zero);
                Cv2.Imwrite("optical_flow_frame2.png", frame2, IntPtr.Zero);

                // Define initial point to track
                using (var prevPts = new Mat(1, 1, CV_32FC2))
                using (var nextPts = new Mat(1, 1, CV_32FC2))
                using (var status = new Mat())
                using (var err = new Mat())
                {
                    float[] ptsData = new float[] { 80.0f, 80.0f };
                    Marshal.Copy(ptsData, 0, prevPts.Data, 2);

                    // Run Optical Flow tracking from Frame 1 to Frame 2
                    Cv2.CalcOpticalFlowPyrLK(
                        frame1, frame2, prevPts, nextPts, status, err,
                        new Size(15, 15), 3, new TermCriteria(3, 30, 0.01), 0, 1e-4
                    );

                    float[] resultData = new float[2];
                    Marshal.Copy(nextPts.Data, resultData, 0, 2);

                    byte[] statusBytes = new byte[1];
                    Marshal.Copy(status.Data, statusBytes, 0, 1);

                    Console.WriteLine($"   Tracking Status: {statusBytes[0]} (1 = Success)");
                    Console.WriteLine($"   Start Position:  (80.0, 80.0)");
                    Console.WriteLine($"   End Position:    ({resultData[0]:F1}, {resultData[1]:F1})");

                    double dx = resultData[0] - 80.0;
                    double dy = resultData[1] - 80.0;
                    Console.WriteLine($"   Measured Offset: dx={dx:F1}, dy={dy:F1} (Expected: dx=5.0, dy=3.0)");
                }
            }
        }
    }
}
