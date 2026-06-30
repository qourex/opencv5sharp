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
        private static Mat CreatePatternImage()
        {
            const int CV_8UC3 = 64;
            var mat = new Mat(200, 200, CV_8UC3);
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
            return mat;
        }

        [Fact]
        public void Imwrite_Imread_BgrImage_Success()
        {
            string testFile = "test_pattern_io.png";
            if (File.Exists(testFile)) File.Delete(testFile);

            try
            {
                using (var mat = CreatePatternImage())
                {
                    bool writeSuccess = Cv2.Imwrite(testFile, mat, IntPtr.Zero);
                    Assert.True(writeSuccess);
                }

                using (var loaded = Cv2.Imread(testFile, (int)ImreadModes.Color))
                {
                    Assert.NotNull(loaded);
                    Assert.NotEqual(IntPtr.Zero, loaded.Handle);
                    Assert.Equal(200, loaded.Rows);
                    Assert.Equal(200, loaded.Cols);
                    Assert.Equal(3, loaded.Channels());
                }
            }
            finally
            {
                if (File.Exists(testFile)) File.Delete(testFile);
            }
        }

        [Fact]
        public void CvtColor_BgrToGray_Success()
        {
            using (var src = CreatePatternImage())
            using (var dst = new Mat())
            {
                Cv2.CvtColor(src, dst, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(200, dst.Rows);
                Assert.Equal(1, dst.Channels());
            }
        }

        [Fact]
        public void GaussianBlur_GrayImage_Success()
        {
            using (var src = CreatePatternImage())
            using (var gray = new Mat())
            using (var blurred = new Mat())
            {
                Cv2.CvtColor(src, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, 4, AlgorithmHint.Default);
                Assert.NotEqual(IntPtr.Zero, blurred.Handle);
                Assert.Equal(200, blurred.Rows);
            }
        }

        [Fact]
        public void Canny_BlurredImage_Success()
        {
            using (var src = CreatePatternImage())
            using (var gray = new Mat())
            using (var blurred = new Mat())
            using (var edges = new Mat())
            {
                Cv2.CvtColor(src, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, 4, AlgorithmHint.Default);
                Cv2.Canny(blurred, edges, 50, 150, 3, false);
                Assert.NotEqual(IntPtr.Zero, edges.Handle);
                Assert.Equal(200, edges.Rows);
            }
        }
    }
}
