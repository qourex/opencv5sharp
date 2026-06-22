// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class HandTrackerSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [9] Classic Hand & Finger Tracker ---");

            using (var capture = new VideoCapture(0, 0))
            {
                if (capture.IsOpened())
                {
                    Console.WriteLine("\nWebcam detected. Running hand tracker for 30 frames...");
                    using (var frame = new Mat())
                    {
                        for (int i = 1; i <= 30; i++)
                        {
                            if (capture.Read(frame) && frame.Handle != IntPtr.Zero)
                            {
                                TrackHandAndFingers($"Webcam Frame {i}/30", frame);
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

            Console.WriteLine("\nRunning verification on synthetic hand drawings...");
            RunSyntheticFallback();

            Console.WriteLine("\nHand tracker sample completed.");
        }

        private static void TrackHandAndFingers(string sourceName, Mat frame)
        {
            const int CV_8UC3 = 64;

            // 1. Convert BGR to YCrCb
            using (var ycrcb = new Mat())
            {
                Cv2.CvtColor(frame, ycrcb, (int)ColorConversionCodes.BGR2YCrCb, 0, AlgorithmHint.Default);

                // 2. Threshold for skin color in YCrCb
                // Typical skin range in YCrCb: Y: [0, 255], Cr: [133, 173], Cb: [77, 127]
                int rows = ycrcb.Rows;
                int cols = ycrcb.Cols;
                using (var lowerb = new Mat(rows, cols, CV_8UC3))
                using (var upperb = new Mat(rows, cols, CV_8UC3))
                using (var skinMask = new Mat())
                {
                    int pixelCount = rows * cols;
                    int size = pixelCount * 3;
                    byte[] lowData = new byte[size];
                    byte[] highData = new byte[size];
                    for (int j = 0; j < size; j += 3)
                    {
                        lowData[j] = 0;       // Y min
                        lowData[j + 1] = 133; // Cr min
                        lowData[j + 2] = 77;  // Cb min

                        highData[j] = 255;    // Y max
                        highData[j + 1] = 173; // Cr max
                        highData[j + 2] = 127; // Cb max
                    }
                    Marshal.Copy(lowData, 0, lowerb.Data, size);
                    Marshal.Copy(highData, 0, upperb.Data, size);

                    Cv2.InRange(ycrcb, lowerb, upperb, skinMask);

                    // 3. Clean up mask using morphological operations
                    using (var element = Cv2.GetStructuringElement((int)MorphShapes.Ellipse, new Size(3, 3), new Point(-1, -1)))
                    using (var cleanMask = new Mat())
                    {
                        if (element != null)
                        {
                            Cv2.Erode(skinMask, cleanMask, element, new Point(-1, -1), 1, (int)BorderTypes.Constant, new Scalar(0));
                            Cv2.Dilate(cleanMask, skinMask, element, new Point(-1, -1), 2, (int)BorderTypes.Constant, new Scalar(0));
                        }

                        // 4. Calculate Moments and Centroid
                        using (var m = Cv2.Moments(skinMask, true))
                        {
                            if (m == null || m.M00 < 800)
                            {
                                Console.WriteLine($"   [{sourceName}] No hand detected (area too small: {(m?.M00 ?? 0):F1}).");
                                return;
                            }

                            double cx = m.M10 / m.M00;
                            double cy = m.M01 / m.M00;

                            // 5. Analyze bounding box and count fingers
                            int width = skinMask.Cols;
                            int height = skinMask.Rows;
                            byte[] maskPixels = new byte[width * height];
                            Marshal.Copy(skinMask.Data, maskPixels, 0, maskPixels.Length);

                            int minX = width, maxX = 0, minY = height, maxY = 0;
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    if (maskPixels[y * width + x] == 255)
                                    {
                                        if (x < minX) minX = x;
                                        if (x > maxX) maxX = x;
                                        if (y < minY) minY = y;
                                        if (y > maxY) maxY = y;
                                    }
                                }
                            }

                            // Sample a slice above the centroid to count finger peaks
                            // Slice height is midway between minY and the centroid Y
                            int sliceY = (int)(cy - (cy - minY) * 0.55);
                            int fingerCount = 0;
                            bool inFinger = false;
                            int fingerWidth = 0;

                            if (sliceY >= 0 && sliceY < height)
                            {
                                for (int x = minX; x <= maxX; x++)
                                {
                                    if (maskPixels[sliceY * width + x] == 255)
                                    {
                                        if (!inFinger)
                                        {
                                            inFinger = true;
                                            fingerWidth = 1;
                                        }
                                        else
                                        {
                                            fingerWidth++;
                                        }
                                    }
                                    else
                                    {
                                        if (inFinger)
                                        {
                                            inFinger = false;
                                            // Ensure the peak is wide enough to not be noise, but not too wide (like the palm itself)
                                            if (fingerWidth > 2 && fingerWidth < (maxX - minX) * 0.3)
                                            {
                                                fingerCount++;
                                            }
                                        }
                                    }
                                }
                                // Handle case where finger reaches the end of the bounding box
                                if (inFinger && fingerWidth > 2 && fingerWidth < (maxX - minX) * 0.3)
                                {
                                    fingerCount++;
                                }
                            }

                            Console.WriteLine($"   [{sourceName}] Hand Centroid: ({cx:F1}, {cy:F1}), Bounding Box: [{minX}, {minY}, {maxX - minX}x{maxY - minY}], Fingers Counted: {fingerCount}");
                        }
                    }
                }
            }
        }

        private static void RunSyntheticFallback()
        {
            const int width = 320;
            const int height = 240;
            const int CV_8UC3 = 64;

            // Generate 3 different frames with 1, 3, and 5 fingers
            int[] fingersToDraw = { 1, 3, 5 };

            foreach (int count in fingersToDraw)
            {
                using (var img = new Mat(height, width, CV_8UC3))
                {
                    byte[] pixels = new byte[width * height * 3];

                    // Fill background with black
                    Array.Clear(pixels, 0, pixels.Length);

                    // 1. Draw a skin-colored palm (BGR: 110, 150, 200)
                    // Centered at (160, 150), radius 35
                    DrawCircle(pixels, width, height, 160, 150, 35, 110, 150, 200);

                    // 2. Draw fingers (vertical lines extending upwards)
                    // Index of fingers coordinates
                    int[] fingerXCoords = { 130, 145, 160, 175, 190 };
                    for (int f = 0; f < count; f++)
                    {
                        int fx = fingerXCoords[f];
                        // Draw finger: 10 pixels wide, extending up to y=70
                        DrawRect(pixels, width, height, fx - 5, 70, 10, 50, 110, 150, 200);
                    }

                    Marshal.Copy(pixels, 0, img.Data, pixels.Length);

                    // Track on this synthetic image
                    TrackHandAndFingers($"Synthetic drawing: {count} fingers", img);
                }
            }
        }

        private static void DrawCircle(byte[] pixels, int width, int height, int cx, int cy, int radius, byte b, byte g, byte r)
        {
            for (int y = cy - radius; y <= cy + radius; y++)
            {
                for (int x = cx - radius; x <= cx + radius; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        int dx = x - cx;
                        int dy = y - cy;
                        if (dx * dx + dy * dy <= radius * radius)
                        {
                            int idx = (y * width + x) * 3;
                            pixels[idx] = b;
                            pixels[idx + 1] = g;
                            pixels[idx + 2] = r;
                        }
                    }
                }
            }
        }

        private static void DrawRect(byte[] pixels, int width, int height, int rx, int ry, int rw, int rh, byte b, byte g, byte r)
        {
            for (int y = ry; y <= ry + rh; y++)
            {
                for (int x = rx; x <= rx + rw; x++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        int idx = (y * width + x) * 3;
                        pixels[idx] = b;
                        pixels[idx + 1] = g;
                        pixels[idx + 2] = r;
                    }
                }
            }
        }
    }
}
