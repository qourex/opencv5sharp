// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class StereoDepthSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [15] Stereo Depth Estimation (StereoBM) ---");

            const int CV_8UC1 = 0;
            const int width = 320;
            const int height = 240;

            Console.WriteLine("\n1. Generating synthetic stereo left and right image pair...");
            using (var left = new Mat(height, width, CV_8UC1))
            using (var right = new Mat(height, width, CV_8UC1))
            {
                // Clear both to black background (intensity 20)
                byte[] bg = new byte[width * height];
                for (int i = 0; i < bg.Length; i++) bg[i] = 20;
                Marshal.Copy(bg, 0, left.Data, bg.Length);
                Marshal.Copy(bg, 0, right.Data, bg.Length);

                // Draw a patterned square in the left image at (100, 60) to (220, 180)
                // In the right image, we shift it to (90, 60) to (210, 180)
                // This simulates a horizontal disparity of exactly 10 pixels!
                DrawPatternedSquare(left, 100, 60, 120, 120);
                DrawPatternedSquare(right, 90, 60, 120, 120);

                Cv2.Imwrite("stereo_left.png", left, IntPtr.Zero);
                Cv2.Imwrite("stereo_right.png", right, IntPtr.Zero);
                Console.WriteLine("   Saved stereo left frame to: stereo_left.png");
                Console.WriteLine("   Saved stereo right frame to: stereo_right.png");

                // 2. Compute Disparity using StereoBM
                Console.WriteLine("\n2. Computing disparity map via StereoBM...");
                using (var stereo = StereoBM.Create(16, 15)) // numDisparities=16, blockSize=15
                {
                    if (stereo == null)
                    {
                        Console.WriteLine("   [ERROR] Failed to create StereoBM matcher.");
                        return;
                    }

                    // Test ROI getters/setters to ensure no Access Violation
                    try
                    {
                        Console.WriteLine("   Testing StereoBM ROI methods...");
                        Rect roi1 = stereo.GetROI1();
                        Rect roi2 = stereo.GetROI2();
                        Console.WriteLine($"   Initial ROI1: ({roi1.X}, {roi1.Y}, {roi1.Width}, {roi1.Height})");
                        Console.WriteLine($"   Initial ROI2: ({roi2.X}, {roi2.Y}, {roi2.Width}, {roi2.Height})");

                        Rect testRoi = new Rect(10, 20, 100, 200);
                        stereo.SetROI1(testRoi);
                        Rect setRoi1 = stereo.GetROI1();
                        Console.WriteLine($"   Updated ROI1: ({setRoi1.X}, {setRoi1.Y}, {setRoi1.Width}, {setRoi1.Height})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"   [WARNING] ROI methods failed: {ex.Message}");
                    }

                    using (var disparity = new Mat())
                    {
                        stereo.Compute(left, right, disparity);
                        Console.WriteLine($"   Disparity map computed. Size: {disparity.Cols}x{disparity.Rows}, Type: {disparity.Type()}");

                        // Retrieve disparity values from the center of the square (approx row 120, col 160)
                        // Note: StereoBM disparity matrix has type CV_16SC1 (short).
                        // Disparity values are scaled by 16 (4 fractional bits).
                        short[] dispData = new short[width * height];
                        Marshal.Copy(disparity.Data, dispData, 0, dispData.Length);

                        int nonZeroCount = 0;
                        short minDisp = short.MaxValue;
                        short maxDisp = short.MinValue;
                        for (int i = 0; i < dispData.Length; i++)
                        {
                            short val = dispData[i];
                            if (val != 0)
                            {
                                nonZeroCount++;
                                if (val < minDisp) minDisp = val;
                                if (val > maxDisp) maxDisp = val;
                            }
                        }

                        Console.WriteLine($"   Disparity non-zero count: {nonZeroCount}/{dispData.Length}");
                        if (nonZeroCount > 0)
                        {
                            Console.WriteLine($"   Non-zero disparity range: [{minDisp / 16.0:F2}, {maxDisp / 16.0:F2}] px");
                        }

                        int centerIdx = 115 * width + 155;
                        short rawDisp = dispData[centerIdx];
                        double realDisp = rawDisp / 16.0;

                        Console.WriteLine($"   Raw disparity value at (155, 115): {rawDisp}");
                        Console.WriteLine($"   Decoded disparity in pixels: {realDisp:F2} px (Expected close to 10.0 px)");

                        // 3. Normalize and save the disparity map for visualization
                        using (var disp8 = new Mat())
                        {
                            Cv2.Normalize(disparity, disp8, 0, 255, (int)NormTypes.Minmax, CV_8UC1, null);
                            Cv2.Imwrite("stereo_disparity.png", disp8, IntPtr.Zero);
                            Console.WriteLine("   Saved colorized disparity map to: stereo_disparity.png");
                        }
                    }
                }
            }

            Console.WriteLine("\nStereo Depth sample completed.");
        }

        private static void DrawPatternedSquare(Mat img, int startX, int startY, int w, int h)
        {
            int cols = img.Cols;
            int rows = img.Rows;
            byte[] data = new byte[cols * rows];
            Marshal.Copy(img.Data, data, 0, data.Length);

            for (int r = startY; r < startY + h; r++)
            {
                if (r < 0 || r >= rows) continue;
                for (int c = startX; c < startX + w; c++)
                {
                    if (c < 0 || c >= cols) continue;
                    // Draw a checkerboard pattern inside the square for high texture details
                    bool isEven = ((r / 10) + (c / 10)) % 2 == 0;
                    data[r * cols + c] = (byte)(isEven ? 240 : 100);
                }
            }
            Marshal.Copy(data, 0, img.Data, data.Length);
        }
    }
}
