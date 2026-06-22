// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class WarpPerspectiveSample
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string LenaUrl = "https://raw.githubusercontent.com/opencv/opencv/master/samples/data/lena.jpg";
        private const string LenaPath = "lena.jpg";

        public static void Run()
        {
            Console.WriteLine("--- [17] Perspective Warp & Homography Correction ---");

            // Ensure Lena image is available
            EnsureLenaImage();

            using (var original = Cv2.Imread(LenaPath, (int)ImreadModes.Color))
            {
                if (original.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to load source image for warping.");
                    return;
                }

                // Resize original to a standard 400x400 for clean mapping
                using (var srcImg = new Mat())
                {
                    Cv2.Resize(original, srcImg, new Size(400, 400), 0, 0, (int)InterpolationFlags.InterLinear);

                    int width = srcImg.Cols;
                    int height = srcImg.Rows;

                    Console.WriteLine($"\n1. Defining perspective warp target coordinate transformation...");
                    const int CV_32FC2 = 37;

                    // Source corners: (0,0), (width-1, 0), (width-1, height-1), (0, height-1)
                    // Target corners: skewed to simulate an inclined card/document plane
                    using (var srcPts = new Mat(4, 1, CV_32FC2))
                    using (var dstPts = new Mat(4, 1, CV_32FC2))
                    {
                        float[] srcCoords = new float[] {
                            0f, 0f,
                            width - 1f, 0f,
                            width - 1f, height - 1f,
                            0f, height - 1f
                        };

                        float[] dstCoords = new float[] {
                            60f, 40f,             // Top-Left skewed in
                            width - 80f, 70f,     // Top-Right skewed down
                            width - 30f, height - 50f, // Bottom-Right skewed out
                            40f, height - 90f      // Bottom-Left skewed up
                        };

                        Marshal.Copy(srcCoords, 0, srcPts.Data, srcCoords.Length);
                        Marshal.Copy(dstCoords, 0, dstPts.Data, dstCoords.Length);

                        // 2. Compute perspective transform matrix (M)
                        Console.WriteLine("\n2. Computing perspective warp matrix (M)...");
                        using (var M = Cv2.GetPerspectiveTransform(srcPts, dstPts, 0))
                        {
                            if (M == null || M.Handle == IntPtr.Zero)
                            {
                                Console.WriteLine("   [ERROR] Failed to calculate perspective matrix.");
                                return;
                            }

                            // Output matrix elements for logging
                            float[] matrixVals = new float[9];
                            // Wait, M is CV_64F (double). Let's copy as doubles!
                            double[] matrixDoubles = new double[9];
                            Marshal.Copy(M.Data, matrixDoubles, 0, 9);
                            Console.WriteLine($"   M = [{matrixDoubles[0]:F3}, {matrixDoubles[1]:F3}, {matrixDoubles[2]:F3}]");
                            Console.WriteLine($"       [{matrixDoubles[3]:F3}, {matrixDoubles[4]:F3}, {matrixDoubles[5]:F3}]");
                            Console.WriteLine($"       [{matrixDoubles[6]:F3}, {matrixDoubles[7]:F3}, {matrixDoubles[8]:F3}]");

                            // Apply forward warp
                            using (var warped = new Mat())
                            {
                                Cv2.WarpPerspective(
                                    srcImg, warped, M, new Size(width, height),
                                    (int)InterpolationFlags.InterLinear, (int)BorderTypes.Constant,
                                    new Scalar(0, 0, 0), AlgorithmHint.Default
                                );

                                Cv2.Imwrite("perspective_warped.png", warped, IntPtr.Zero);
                                Console.WriteLine("   Saved skewed/warped image to: perspective_warped.png");

                                // 3. Compute inverse perspective transform to restore/flatten
                                Console.WriteLine("\n3. Computing inverse perspective matrix and restoring image...");
                                using (var invM = Cv2.GetPerspectiveTransform(dstPts, srcPts, 0))
                                using (var restored = new Mat())
                                {
                                    if (invM != null)
                                    {
                                        Cv2.WarpPerspective(
                                            warped, restored, invM, new Size(width, height),
                                            (int)InterpolationFlags.InterLinear, (int)BorderTypes.Constant,
                                            new Scalar(0, 0, 0), AlgorithmHint.Default
                                        );

                                        Cv2.Imwrite("perspective_restored.png", restored, IntPtr.Zero);
                                        Console.WriteLine("   Saved de-warped/restored flat image to: perspective_restored.png");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nPerspective Warp sample completed.");
        }

        private static void EnsureLenaImage()
        {
            if (File.Exists(LenaPath))
            {
                return;
            }

            // Check if available in relative paths
            string repoLena = @"opencv_prebuilt/opencv/sources/doc/js_tutorials/js_assets/lena.jpg";
            if (File.Exists(repoLena))
            {
                File.Copy(repoLena, LenaPath, true);
                Console.WriteLine($"Found local Lena image at {repoLena}. Copied to output.");
                return;
            }

            Console.WriteLine("Lena image not found locally. Downloading from GitHub...");
            try
            {
                using (var response = _httpClient.GetAsync(LenaUrl).GetAwaiter().GetResult())
                {
                    response.EnsureSuccessStatusCode();
                    using (var fs = new FileStream(LenaPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        response.Content.CopyToAsync(fs).GetAwaiter().GetResult();
                    }
                }
                Console.WriteLine("Download complete!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARNING] Failed to download Lena image: {ex.Message}");
            }
        }
    }
}
