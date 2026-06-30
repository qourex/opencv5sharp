// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DataDrivenTests
    {
        // Helper to construct CV matrix types in OpenCV 5.0
        private static int MakeType(int depth, int channels)
        {
            return depth + ((channels - 1) << 5);
        }

        #region 1. Threshold Combinations (100 tests)
        public static IEnumerable<object[]> GetThresholdTestData()
        {
            int[] depths = { 0, 5 }; // CV_8U, CV_32F (Officially supported threshold depths)
            int[] types = { 0, 1, 2, 3, 4 }; // Binary, BinaryInv, Trunc, Tozero, TozeroInv
            double[] thresholds = { 10.0, 30.0, 50.0, 70.0, 90.0, 110.0, 130.0, 150.0, 170.0, 190.0 };

            foreach (var d in depths)
            {
                foreach (var t in types)
                {
                    foreach (var v in thresholds)
                    {
                        yield return new object[] { d, t, v };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetThresholdTestData))]
        public void TestThresholdCombinations(int depth, int thresholdType, double thresholdVal)
        {
            int type = MakeType(depth, 1);
            using (var src = new Mat(10, 10, type))
            using (var dst = new Mat())
            {
                // Fill with dummy value 128
                int bytes = (int)(src.Rows * src.Step);
                if (depth == 0) // CV_8U
                {
                    byte[] fillData = new byte[bytes];
                    for (int i = 0; i < fillData.Length; i++) fillData[i] = 128;
                    Marshal.Copy(fillData, 0, src.Data, fillData.Length);
                }
                else // CV_32F
                {
                    float[] fillData = new float[10 * 10];
                    for (int i = 0; i < fillData.Length; i++) fillData[i] = 128.0f;
                    Marshal.Copy(fillData, 0, src.Data, fillData.Length);
                }

                double threshRet = Cv2.Threshold(src, dst, thresholdVal, 255.0, thresholdType);
                Assert.Equal(thresholdVal, threshRet);
                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(10, dst.Rows);
                Assert.Equal(10, dst.Cols);

                // Verify output correctness (H6)
                if (depth == 0) // CV_8U
                {
                    byte[] readData = new byte[bytes];
                    Marshal.Copy(dst.Data, readData, 0, readData.Length);
                    byte expected = 0;
                    if (thresholdType == 0) expected = (byte)(128.0 > thresholdVal ? 255 : 0);
                    else if (thresholdType == 1) expected = (byte)(128.0 > thresholdVal ? 0 : 255);
                    else if (thresholdType == 2) expected = (byte)(128.0 > thresholdVal ? thresholdVal : 128);
                    else if (thresholdType == 3) expected = (byte)(128.0 > thresholdVal ? 128 : 0);
                    else if (thresholdType == 4) expected = (byte)(128.0 > thresholdVal ? 0 : 128);

                    Assert.Equal(expected, readData[0]);
                }
                else // CV_32F
                {
                    float[] readData = new float[10 * 10];
                    Marshal.Copy(dst.Data, readData, 0, readData.Length);
                    float expected = 0.0f;
                    if (thresholdType == 0) expected = (float)(128.0 > thresholdVal ? 255.0 : 0.0);
                    else if (thresholdType == 1) expected = (float)(128.0 > thresholdVal ? 0.0 : 255.0);
                    else if (thresholdType == 2) expected = (float)(128.0 > thresholdVal ? thresholdVal : 128.0);
                    else if (thresholdType == 3) expected = (float)(128.0 > thresholdVal ? 128.0 : 0.0);
                    else if (thresholdType == 4) expected = (float)(128.0 > thresholdVal ? 0.0 : 128.0);

                    Assert.Equal(expected, readData[0], 2);
                }
            }
        }
        #endregion

        #region 2. Color Conversions (30 tests)
        public static IEnumerable<object[]> GetColorConversionData()
        {
            // List of (code, inputChannels)
            var conversions = new List<(int, int)>
            {
                (0, 3), (1, 4), (2, 3), (3, 4), (4, 3), (5, 4), // Bgr2bgra, Bgra2bgr, Bgr2rgba, Rgba2bgr, Bgr2rgb, Bgra2rgba
                (6, 3), (7, 3), (8, 1), (9, 1), (10, 4), (11, 4), // Bgr2gray, Rgb2gray, Gray2bgr, Gray2bgra, Bgra2gray, Rgba2gray
                (40, 3), (41, 3), (54, 3), (55, 3), // Bgr2hsv, Rgb2hsv, Hsv2bgr, Hsv2rgb
                (32, 3), (33, 3), (34, 3), (35, 3), // Bgr2xyz, Rgb2xyz, Xyz2bgr, Xyz2rgb
                (82, 3), (83, 3) // Bgr2yuv, Rgb2yuv
            };

            foreach (var conv in conversions)
            {
                yield return new object[] { conv.Item1, conv.Item2 };
            }
        }

        [Theory]
        [MemberData(nameof(GetColorConversionData))]
        public void TestColorConversionCombinations(int code, int inputChannels)
        {
            int inputType = MakeType(0, inputChannels); // CV_8U
            using (var src = new Mat(10, 10, inputType))
            using (var dst = new Mat())
            {
                Cv2.CvtColor(src, dst, code, 0, AlgorithmHint.Default);
                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(10, dst.Rows);
                Assert.Equal(10, dst.Cols);
                Assert.True(dst.Channels() > 0);
            }
        }
        #endregion

        #region 3. Matrix Math (108 tests)
        public static IEnumerable<object[]> GetMatrixMathData()
        {
            int[] ops = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] depths = { 0, 5, 6 }; // CV_8U, CV_32F, CV_64F
            int[] channels = { 1, 2, 3, 4 };

            foreach (var op in ops)
            {
                foreach (var d in depths)
                {
                    foreach (var ch in channels)
                    {
                        // Bitwise operations are only supported on integer types (depth 0)
                        if (op >= 4 && op <= 6 && d != 0) continue;
                        yield return new object[] { op, d, ch };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetMatrixMathData))]
        public void TestMatrixMathCombinations(int op, int depth, int channels)
        {
            int type = MakeType(depth, channels);
            using (var src1 = new Mat(5, 5, type))
            using (var src2 = new Mat(5, 5, type))
            using (var dst = new Mat())
            {
                switch (op)
                {
                    case 0: Cv2.Add(src1, src2, dst, null, -1); break;
                    case 1: Cv2.Subtract(src1, src2, dst, null, -1); break;
                    case 2: Cv2.Multiply(src1, src2, dst, 1.0, -1); break;
                    case 3: Cv2.Divide(src1, src2, dst, 1.0, -1); break;
                    case 4: Cv2.BitwiseAnd(src1, src2, dst, null); break;
                    case 5: Cv2.BitwiseOr(src1, src2, dst, null); break;
                    case 6: Cv2.BitwiseXor(src1, src2, dst, null); break;
                    case 7: Cv2.Min(src1, src2, dst); break;
                    case 8: Cv2.Max(src1, src2, dst); break;
                    case 9: Cv2.Absdiff(src1, src2, dst); break;
                }

                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(5, dst.Rows);
                Assert.Equal(5, dst.Cols);
                Assert.Equal(channels, dst.Channels());
            }
        }
        #endregion

        #region 4. Image Filters (34 tests)
        public static IEnumerable<object[]> GetFilterTestData()
        {
            int[] types = { 0, 1, 2 }; // Blur, GaussianBlur, MedianBlur
            int[] sizes = { 3, 5 };
            int[] borders = { 0, 1, 2, 4 }; // Constant, Replicate, Reflect, Reflect101
            int[] depths = { 0, 5 }; // CV_8U, CV_32F

            foreach (var t in types)
            {
                foreach (var s in sizes)
                {
                    foreach (var b in borders)
                    {
                        foreach (var d in depths)
                        {
                            // MedianBlur only supports CV_8U and doesn't take border parameter (defaulting to 4)
                            if (t == 2)
                            {
                                if (d != 0 || b != 4) continue;
                            }
                            yield return new object[] { t, s, b, d };
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetFilterTestData))]
        public void TestFilterCombinations(int filterType, int kernelSize, int borderType, int depth)
        {
            int type = MakeType(depth, 1);
            using (var src = new Mat(15, 15, type))
            using (var dst = new Mat())
            {
                if (filterType == 0) // Blur
                {
                    Cv2.Blur(src, dst, new Size(kernelSize, kernelSize), new Point(-1, -1), borderType);
                }
                else if (filterType == 1) // GaussianBlur
                {
                    Cv2.GaussianBlur(src, dst, new Size(kernelSize, kernelSize), 1.0, 1.0, borderType, AlgorithmHint.Default);
                }
                else if (filterType == 2) // MedianBlur
                {
                    Cv2.MedianBlur(src, dst, kernelSize);
                }

                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(15, dst.Rows);
                Assert.Equal(15, dst.Cols);
            }
        }
        #endregion

        #region 5. Morphology (105 tests)
        public static IEnumerable<object[]> GetMorphTestData()
        {
            int[] ops = { 0, 1, 2, 3, 4, 5, 6 }; // Erode, Dilate, Open, Close, Gradient, Tophat, Blackhat
            int[] shapes = { 0, 1, 2 }; // Rect, Cross, Ellipse
            int[] sizes = { 3, 5, 7, 9, 11 };

            foreach (var op in ops)
            {
                foreach (var sh in shapes)
                {
                    foreach (var sz in sizes)
                    {
                        yield return new object[] { op, sh, sz };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetMorphTestData))]
        public void TestMorphologyCombinations(int op, int shape, int size)
        {
            int type = MakeType(0, 1); // CV_8UC1
            using (var src = new Mat(20, 20, type))
            using (var dst = new Mat())
            using (var kernel = Cv2.GetStructuringElement(shape, new Size(size, size), new Point(-1, -1)))
            {
                Assert.NotNull(kernel);
                Cv2.MorphologyEx(src, dst, op, kernel!, new Point(-1, -1), 1, 4, new Scalar(0, 0, 0, 0));

                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(20, dst.Rows);
                Assert.Equal(20, dst.Cols);
            }
        }
        #endregion

        #region 6. Image Resize (120 tests)
        public static IEnumerable<object[]> GetResizeTestData()
        {
            int[] interpolations = { 0, 1, 2, 3, 4 }; // InterNearest, InterLinear, InterCubic, InterArea, InterLanczos4
            double[] scales = { 0.25, 0.5, 0.75, 1.5, 2.0, 3.0 };
            int[] formats = { 0, 1, 2, 3 }; // CV_8UC1, CV_8UC3, CV_32FC1, CV_32FC3

            foreach (var interp in interpolations)
            {
                foreach (var sc in scales)
                {
                    foreach (var f in formats)
                    {
                        int depth = f < 2 ? 0 : 5; // CV_8U = 0, CV_32F = 5
                        int channels = (f == 0 || f == 2) ? 1 : 3;
                        yield return new object[] { interp, sc, depth, channels };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetResizeTestData))]
        public void TestResizeCombinations(int interpolation, double scale, int depth, int channels)
        {
            int type = MakeType(depth, channels);
            using (var src = new Mat(20, 20, type))
            using (var dst = new Mat())
            {
                int newWidth = (int)(20 * scale);
                int newHeight = (int)(20 * scale);

                Cv2.Resize(src, dst, new Size(newWidth, newHeight), 0.0, 0.0, interpolation);

                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(newHeight, dst.Rows);
                Assert.Equal(newWidth, dst.Cols);
                Assert.Equal(channels, dst.Channels());
            }
        }
        #endregion

        #region 7. Pixel Access and Marshalling (80 tests)
        public static IEnumerable<object[]> GetPixelAccessTestData()
        {
            int[] depths = { 0, 2, 4, 5, 6 }; // CV_8U, CV_16U, CV_32S, CV_32F, CV_64F
            int[] channels = { 1, 2, 3, 4 };
            int[] sizes = { 2, 3, 5, 8 };

            foreach (var d in depths)
            {
                foreach (var ch in channels)
                {
                    foreach (var sz in sizes)
                    {
                        yield return new object[] { d, ch, sz };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetPixelAccessTestData))]
        public void TestPixelAccessAndMarshalling(int depth, int channels, int size)
        {
            int type = MakeType(depth, channels);
            using (var mat = new Mat(size, size, type))
            {
                Assert.NotEqual(IntPtr.Zero, mat.Handle);
                Assert.Equal(size, mat.Rows);
                Assert.Equal(size, mat.Cols);
                Assert.Equal(channels, mat.Channels());

                int elemSize = 1;
                if (depth == 2) elemSize = 2; // CV_16U
                else if (depth == 4 || depth == 5) elemSize = 4; // CV_32S, CV_32F
                else if (depth == 6) elemSize = 8; // CV_64F

                int totalBytes = (int)(mat.Rows * mat.Step);
                byte[] fillBuffer = new byte[totalBytes];
                for (int i = 0; i < fillBuffer.Length; i++)
                {
                    fillBuffer[i] = (byte)(i % 256);
                }
                if (mat.Data != IntPtr.Zero)
                {
                    Marshal.Copy(fillBuffer, 0, mat.Data, fillBuffer.Length);
                }

                byte[] readBuffer = new byte[totalBytes];
                if (mat.Data != IntPtr.Zero)
                {
                    Marshal.Copy(mat.Data, readBuffer, 0, readBuffer.Length);
                }

                for (int i = 0; i < totalBytes; i++)
                {
                    Assert.Equal(fillBuffer[i], readBuffer[i]);
                }
            }
        }
        #endregion
    }
}
