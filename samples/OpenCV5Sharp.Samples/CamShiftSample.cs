// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class CamShiftSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [18] Object Tracking via CamShift ---");

            const int CV_8UC1 = 0;
            const int size = 250;

            Console.WriteLine("\n1. Running CamShift tracking simulation (10 frames)...");

            // Initial tracking window
            Rect trackWindow = new Rect(20, 20, 45, 45);
            Console.WriteLine($"   Initial Search Window: [x: {trackWindow.X}, y: {trackWindow.Y}, w: {trackWindow.Width}, h: {trackWindow.Height}]");

            // Define motion path: starting at (20,20) and moving diagonally
            int curX = 20;
            int curY = 20;
            int boxW = 40;
            int boxH = 40;

            using (var probImage = new Mat(size, size, CV_8UC1))
            {
                byte[] zeros = new byte[size * size];

                for (int frame = 1; frame <= 10; frame++)
                {
                    // Move the simulated target object
                    curX += 2;
                    curY += 1;

                    // Re-generate binary probability map for simulated object (white box on black background)
                    Marshal.Copy(zeros, 0, probImage.Data, zeros.Length);
                    Cv2.Rectangle(probImage, new Point(curX, curY), new Point(curX + boxW, curY + boxH), new Scalar(255), -1, 8, 0);

                    double pixelSum = Cv2.Sum(probImage).V0;
                    Console.WriteLine($"   [Frame {frame}] Pixel Sum: {pixelSum}, Target Rect: ({curX}, {curY}, {boxW}, {boxH}), Window: ({trackWindow.X}, {trackWindow.Y}, {trackWindow.Width}, {trackWindow.Height})");

                    // Stop criteria: 10 iterations or epsilon = 1.0
                    var criteria = new TermCriteria(3, 10, 1.0);

                    using (var trackBox = Cv2.CamShift(probImage, trackWindow, criteria))
                    {
                        if (trackBox != null)
                        {
                            Point2F center = trackBox.Center;
                            Size2F boxSize = trackBox.Size;
                            float angle = trackBox.Angle;

                            // Update trackWindow using the RotatedRect bounding box for the next frame
                            trackWindow = trackBox.BoundingRect();

                            // Constrain window to stay within image bounds
                            int newX = Math.Max(0, Math.Min(trackWindow.X, size - 1));
                            int newY = Math.Max(0, Math.Min(trackWindow.Y, size - 1));
                            int newW = Math.Max(1, Math.Min(trackWindow.Width, size - newX));
                            int newH = Math.Max(1, Math.Min(trackWindow.Height, size - newY));
                            trackWindow = new Rect(newX, newY, newW, newH);

                            Console.WriteLine($"   [Frame {frame}/10] Object Center: ({center.X:F1}, {center.Y:F1}), Size: {boxSize.Width:F1}x{boxSize.Height:F1}, Angle: {angle:F1}°");
                        }
                        else
                        {
                            Console.WriteLine($"   [Frame {frame}/10] [WARNING] Object lost.");
                        }
                    }
                }
            }

            Console.WriteLine("\nCamShift sample completed.");
        }
    }
}
