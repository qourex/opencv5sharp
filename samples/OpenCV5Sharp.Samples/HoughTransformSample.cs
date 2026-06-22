// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class HoughTransformSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [15] Hough Line & Circle Detection ---");

            const int CV_8UC1 = 0;
            const int size = 300;

            Console.WriteLine("\n1. Generating synthetic image with a circle and a line...");
            using (var img = new Mat(size, size, CV_8UC1))
            {
                // Clear to black (0)
                byte[] bg = new byte[size * size];
                Marshal.Copy(bg, 0, img.Data, bg.Length);

                // Draw a white circle at (150, 150) with radius 45, thickness 2
                Cv2.Circle(img, new Point(150, 150), 45, new Scalar(255), 2, 8, 0);

                // Draw a white diagonal line from (30, 30) to (270, 270), thickness 2
                Cv2.Line(img, new Point(30, 30), new Point(270, 270), new Scalar(255), 2, 8, 0);

                Cv2.Imwrite("hough_input.png", img, IntPtr.Zero);
                Console.WriteLine("   Saved synthetic input image to: hough_input.png");

                // 2. Hough Circle Detection
                Console.WriteLine("\n2. Running Cv2.HoughCircles...");
                using (var circles = new Mat())
                {
                    // Parameters: image, circles, method=Gradient, dp=1, minDist=100, param1=100, param2=10, minRadius=10, maxRadius=100
                    Cv2.HoughCircles(img, circles, (int)HoughModes.Gradient, 1.0, 100.0, 100.0, 10.0, 10, 100);

                    int circleCount = circles.Cols > circles.Rows ? circles.Cols : circles.Rows;
                    Console.WriteLine($"   Detected circles count: {circleCount}");

                    if (circleCount > 0)
                    {
                        float[] circleData = new float[circleCount * 3];
                        Marshal.Copy(circles.Data, circleData, 0, circleData.Length);

                        for (int i = 0; i < circleCount; i++)
                        {
                            float cx = circleData[i * 3];
                            float cy = circleData[i * 3 + 1];
                            float r = circleData[i * 3 + 2];
                            Console.WriteLine($"   - Circle {i}: Center = ({cx:F1}, {cy:F1}), Radius = {r:F1} (Expected: Center=(150.0, 150.0), Radius=45.0)");
                        }
                    }
                }

                // 3. Hough Line Detection
                Console.WriteLine("\n3. Running Cv2.HoughLines...");
                using (var lines = new Mat())
                {
                    // Parameters: image, lines, rho=1, theta=1 degree, threshold=50
                    Cv2.HoughLines(img, lines, 1.0, Math.PI / 180.0, 50, 0, 0, 0, Math.PI, false);

                    int lineCount = lines.Cols > lines.Rows ? lines.Cols : lines.Rows;
                    Console.WriteLine($"   Detected lines count: {lineCount}");

                    if (lineCount > 0)
                    {
                        float[] lineData = new float[lineCount * 2];
                        Marshal.Copy(lines.Data, lineData, 0, lineData.Length);

                        // Print the top 3 detected lines
                        int printCount = Math.Min(lineCount, 3);
                        for (int i = 0; i < printCount; i++)
                        {
                            float rho = lineData[i * 2];
                            float theta = lineData[i * 2 + 1];
                            double angleDeg = theta * (180.0 / Math.PI);
                            Console.WriteLine($"   - Line {i}: rho = {rho:F1}, theta = {theta:F3} ({angleDeg:F1}°)");
                        }
                    }
                }
            }

            Console.WriteLine("\nHough Transform sample completed.");
        }
    }
}
