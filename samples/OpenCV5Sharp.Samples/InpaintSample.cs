// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class InpaintSample
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string LenaUrl = "https://raw.githubusercontent.com/opencv/opencv/master/samples/data/lena.jpg";
        private const string LenaPath = "lena.jpg";

        public static void Run()
        {
            Console.WriteLine("--- [13] Image Inpainting & Restoration ---");

            // Ensure Lena image is available
            EnsureLenaImage();

            using (var original = Cv2.Imread(LenaPath, (int)ImreadModes.Color))
            {
                if (original.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to load source image for inpainting.");
                    return;
                }

                Console.WriteLine("\n1. Simulating image corruption (adding artificial scratches and text)...");
                int width = original.Cols;
                int height = original.Rows;

                const int CV_8UC1 = 0;
                using (var corrupted = original.Clone())
                using (var mask = new Mat(height, width, CV_8UC1))
                {
                    if (corrupted == null)
                    {
                        Console.WriteLine("   [ERROR] Failed to clone original image.");
                        return;
                    }

                    // Clear mask to 0 (black)
                    byte[] zeros = new byte[width * height];
                    Marshal.Copy(zeros, 0, mask.Data, zeros.Length);

                    // Draw random scratches on both the image (as bright green) and the mask (as white)
                    // Scratch 1
                    Cv2.Line(corrupted, new Point(50, 50), new Point(200, 180), new Scalar(0, 255, 0), 4, 8, 0);
                    Cv2.Line(mask, new Point(50, 50), new Point(200, 180), new Scalar(255), 4, 8, 0);

                    // Scratch 2
                    Cv2.Line(corrupted, new Point(300, 400), new Point(450, 420), new Scalar(0, 255, 0), 6, 8, 0);
                    Cv2.Line(mask, new Point(300, 400), new Point(450, 420), new Scalar(255), 6, 8, 0);

                    // Scratch 3 (Text overlay)
                    // FontFace.HersheySimplex = 0.
                    Cv2.PutText(corrupted, "SCARRED", new Point(120, 300), 0, 1.5, new Scalar(0, 255, 0), 4, 8, false);
                    Cv2.PutText(mask, "SCARRED", new Point(120, 300), 0, 1.5, new Scalar(255), 4, 8, false);

                    Cv2.Imwrite("inpaint_corrupted.png", corrupted, IntPtr.Zero);
                    Cv2.Imwrite("inpaint_mask.png", mask, IntPtr.Zero);
                    Console.WriteLine("   Saved corrupted image to: inpaint_corrupted.png");
                    Console.WriteLine("   Saved corruption mask to: inpaint_mask.png");

                    // 2. Perform Image Inpainting
                    Console.WriteLine("\n2. Restoring image using Cv2.Inpaint...");
                    using (var restoredNS = new Mat())
                    using (var restoredTelea = new Mat())
                    {
                        // Navier-Stokes method (flags = 0)
                        Cv2.Inpaint(corrupted, mask, restoredNS, 3.0, 0);
                        Cv2.Imwrite("inpaint_restored_ns.png", restoredNS, IntPtr.Zero);
                        Console.WriteLine("   Saved Navier-Stokes restored image to: inpaint_restored_ns.png");

                        // Telea method (flags = 1)
                        Cv2.Inpaint(corrupted, mask, restoredTelea, 3.0, 1);
                        Cv2.Imwrite("inpaint_restored_telea.png", restoredTelea, IntPtr.Zero);
                        Console.WriteLine("   Saved Telea restored image to: inpaint_restored_telea.png");
                    }
                }
            }

            Console.WriteLine("\nInpainting sample completed.");
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
