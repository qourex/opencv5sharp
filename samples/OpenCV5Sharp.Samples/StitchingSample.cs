// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class StitchingSample
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string LenaUrl = "https://raw.githubusercontent.com/opencv/opencv/master/samples/data/lena.jpg";
        private const string LenaPath = "lena.jpg";

        public static void Run()
        {
            Console.WriteLine("--- [12] Panoramic Image Stitching ---");

            // Ensure Lena image is available
            EnsureLenaImage();

            Console.WriteLine("\n1. Slicing test image into overlapping left and right halves...");
            using (var img = Cv2.Imread(LenaPath, (int)ImreadModes.Color))
            {
                if (img.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to load source image for stitching.");
                    return;
                }

                int width = img.Cols;
                int height = img.Rows;

                // Slice into left 65% and right 65% (providing 30% overlap)
                int leftWidth = (int)(width * 0.65);
                int rightStart = (int)(width * 0.35);
                int rightWidth = width - rightStart;

                using (var left = new Mat(img, new Range(0, height), new Range(0, leftWidth)))
                using (var right = new Mat(img, new Range(0, height), new Range(rightStart, width)))
                {
                    Console.WriteLine($"   Source Image: {width}x{height}");
                    Console.WriteLine($"   Left Sub-image ROI: {left.Cols}x{left.Rows}");
                    Console.WriteLine($"   Right Sub-image ROI: {right.Cols}x{right.Rows}");

                    Cv2.Imwrite("stitch_left.png", left, IntPtr.Zero);
                    Cv2.Imwrite("stitch_right.png", right, IntPtr.Zero);

                    // 2. Stitch the images together using Stitcher
                    Console.WriteLine("\n2. Stitching sub-images using Stitcher...");
                    IntPtr[] handles = new IntPtr[] { left.Handle, right.Handle };
                    IntPtr vecPtr = NativeMethods.cv_VectorMat_New(handles, handles.Length);

                    try
                    {
                        // FileStorageMode.Read = 0. StitcherMode.Panorama is 0.
                        // Since Stitcher.Create takes FileStorageMode due to a binding generator quirk, we cast 0.
                        using (var stitcher = Stitcher.Create((FileStorageMode)0))
                        {
                            if (stitcher == null)
                            {
                                Console.WriteLine("   [ERROR] Failed to create Stitcher.");
                                return;
                            }

                            using (var pano = new Mat())
                            {
                                Console.WriteLine("   Running Stitcher.Stitch (this may take a few seconds)...");
                                StitcherStatus status = stitcher.Stitch(vecPtr, pano);

                                Console.WriteLine($"   Stitcher completed with Status: {status}");
                                if (status == StitcherStatus.Ok)
                                {
                                    Console.WriteLine($"   Success! Panoramic image generated. Size: {pano.Cols}x{pano.Rows}");
                                    Cv2.Imwrite("stitched_panorama.png", pano, IntPtr.Zero);
                                    Console.WriteLine("   Saved stitched panorama to: stitched_panorama.png");
                                }
                                else
                                {
                                    Console.WriteLine("   [WARNING] Stitching failed. Note that stitching requires distinct overlapping features.");
                                }
                            }
                        }
                    }
                    finally
                    {
                        NativeMethods.cv_VectorMat_Delete(vecPtr);
                    }
                }
            }

            Console.WriteLine("\nStitching sample completed.");
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
