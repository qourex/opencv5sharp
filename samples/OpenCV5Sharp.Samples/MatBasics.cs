// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class MatBasics
    {
        public static void Run()
        {
            Console.WriteLine("--- [1] Mat Basics & Math Operations ---");

            // 1. Creation and Properties
            Console.WriteLine("\n1. Creating a 10x10 matrix initialized to 0...");
            using (Mat m = new Mat(10, 10, 0)) // CV_8UC1 = 0
            {
                Console.WriteLine($"Mat Dimension: {m.Cols}x{m.Rows}, Channels: {m.Channels()}");
                Console.WriteLine($"Is Continuous: {m.IsContinuous()}");
            }

            // 2. Initializing with Scalar
            Console.WriteLine("\n2. Initializing a 3x3 RGB matrix with a blue color scalar...");
            const int CV_8UC3 = 64; // 8-bit, 3 channels
            using (Mat rgb = new Mat(3, 3, CV_8UC3))
            {
                Console.WriteLine($"RGB Channels: {rgb.Channels()}, Type: {rgb.Type()}");
            }

            // 3. Matrix Math
            Console.WriteLine("\n3. Performing matrix addition...");
            using (Mat m1 = new Mat(2, 2, CV_8UC3))
            using (Mat m2 = new Mat(2, 2, CV_8UC3))
            using (Mat dst = new Mat())
            {
                Cv2.Add(m1, m2, dst, null, -1);
                Console.WriteLine($"Result Matrix Size: {dst.Cols}x{dst.Rows}");
            }

            // 4. Submatrix (Region of Interest - ROI)
            Console.WriteLine("\n4. Creating a submatrix (Region of Interest)...");
            using (Mat parent = new Mat(100, 100, CV_8UC3))
            {
                // Extract a 50x50 ROI from the top-left corner
                using (Mat roi = new Mat(parent, new Range(0, 50), new Range(0, 50)))
                {
                    Console.WriteLine($"Parent Size: {parent.Cols}x{parent.Rows}");
                    Console.WriteLine($"ROI Size: {roi.Cols}x{roi.Rows}");
                    Console.WriteLine($"Is ROI a submatrix? {roi.IsSubmatrix()}");
                }
            }

            Console.WriteLine("\nMat Basics sample completed successfully.");
        }
    }
}
