// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ImageProcessingTests
    {
        [Fact]
        public void TestImageIOAndProcessing()
        {
            const int CV_8UC3 = 64;

            string testFile = "test_pattern.png";
            string grayFile = "test_grayscale.png";
            string blurFile = "test_blur.png";
            string cannyFile = "test_canny.png";

            // Clean old files
            if (File.Exists(testFile)) File.Delete(testFile);
            if (File.Exists(grayFile)) File.Delete(grayFile);
            if (File.Exists(blurFile)) File.Delete(blurFile);
            if (File.Exists(cannyFile)) File.Delete(cannyFile);

            try
            {
                // 1. Create a BGR image with a white diagonal line
                using (Mat mat = new Mat(200, 200, CV_8UC3))
                {
                    int size = mat.Rows * mat.Cols * mat.Channels();
                    byte[] black = new byte[size];
                    Marshal.Copy(black, 0, mat.Data, size);

                    // Draw diagonal line (white: 255, 255, 255)
                    byte[] lineData = new byte[size];
                    Marshal.Copy(mat.Data, lineData, 0, size);
                    for (int i = 0; i < 200; i++)
                    {
                        int idx = (i * 200 + i) * 3;
                        lineData[idx] = 255;
                        lineData[idx + 1] = 255;
                        lineData[idx + 2] = 255;
                    }
                    Marshal.Copy(lineData, 0, mat.Data, size);

                    // Save to disk
                    bool writeSuccess = Cv2.Imwrite(testFile, mat, IntPtr.Zero);
                    Assert.True(writeSuccess);
                }

                // 2. Read image back
                using (Mat loaded = Cv2.Imread(testFile, (int)ImreadModes.Color))
                {
                    Assert.NotEqual(IntPtr.Zero, loaded.Handle);
                    Assert.Equal(200, loaded.Rows);
                    Assert.Equal(200, loaded.Cols);
                    Assert.Equal(3, loaded.Channels());

                    // 3. Convert to Grayscale
                    using (Mat gray = new Mat())
                    {
                        Cv2.CvtColor(loaded, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                        Assert.Equal(200, gray.Rows);
                        Assert.Equal(1, gray.Channels());
                        Cv2.Imwrite(grayFile, gray, IntPtr.Zero);

                        // 4. Apply Blur
                        using (Mat blurred = new Mat())
                        {
                            Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, 4, AlgorithmHint.Default);
                            Assert.Equal(200, blurred.Rows);
                            Cv2.Imwrite(blurFile, blurred, IntPtr.Zero);

                            // 5. Apply Canny Edge Detection
                            using (Mat edges = new Mat())
                            {
                                Cv2.Canny(blurred, edges, 50, 150, 3, false);
                                Assert.Equal(200, edges.Rows);
                                Cv2.Imwrite(cannyFile, edges, IntPtr.Zero);
                            }
                        }
                    }
                }

                // Verify output files exist
                Assert.True(File.Exists(testFile));
                Assert.True(File.Exists(grayFile));
                Assert.True(File.Exists(blurFile));
                Assert.True(File.Exists(cannyFile));
            }
            finally
            {
                // Clean up generated files
                try { if (File.Exists(testFile)) File.Delete(testFile); } catch { }
                try { if (File.Exists(grayFile)) File.Delete(grayFile); } catch { }
                try { if (File.Exists(blurFile)) File.Delete(blurFile); } catch { }
                try { if (File.Exists(cannyFile)) File.Delete(cannyFile); } catch { }
            }
        }
    }
}
