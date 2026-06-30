// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class Calib3dTests
    {
        [Fact]
        public void TestStereoBMMatching()
        {
            const int CV_8UC1 = 0;
            // Create left and right dummy stereo images
            using (var left = new Mat(100, 100, CV_8UC1))
            using (var right = new Mat(100, 100, CV_8UC1))
            using (var disparity = new Mat())
            {
                // Draw matching structures (with shift for disparity)
                Cv2.Circle(left, new Point(50, 50), 15, new Scalar(255, 0, 0, 0), -1, 8, 0);
                Cv2.Circle(right, new Point(46, 50), 15, new Scalar(255, 0, 0, 0), -1, 8, 0);

                // Create StereoBM matcher (numDisparities = 16, blockSize = 9)
                using (var stereo = StereoBM.Create(16, 9))
                {
                    Assert.NotNull(stereo);
                    Assert.NotEqual(IntPtr.Zero, stereo!.Handle);

                    // Test properties
                    Assert.Equal(9, stereo.GetBlockSize());
                    Assert.Equal(16, stereo.GetMinDisparity() + stereo.GetNumDisparities());

                    stereo.SetBlockSize(11);
                    Assert.Equal(11, stereo.GetBlockSize());

                    // Compute disparity
                    stereo.Compute(left, right, disparity);

                    Assert.NotEqual(IntPtr.Zero, disparity.Handle);
                    Assert.Equal(100, disparity.Rows);
                    Assert.Equal(100, disparity.Cols);
                }
            }
        }

        [Fact]
        public void TestStereoSGBMMatching()
        {
            const int CV_8UC1 = 0;
            using (var left = new Mat(100, 100, CV_8UC1))
            using (var right = new Mat(100, 100, CV_8UC1))
            using (var disparity = new Mat())
            {
                // Create StereoSGBM matcher
                using (var sgbm = StereoSGBM.Create(0, 16, 3, 0, 0, 0, 0, 0, 0, 0, 0))
                {
                    Assert.NotNull(sgbm);
                    Assert.NotEqual(IntPtr.Zero, sgbm!.Handle);

                    // Compute
                    sgbm.Compute(left, right, disparity);
                    Assert.NotEqual(IntPtr.Zero, disparity.Handle);
                    Assert.Equal(100, disparity.Rows);
                }
            }
        }

        [Fact]
        public void TestDrawChessboardCorners()
        {
            const int CV_8UC3 = 64;
            using (var image = new Mat(100, 100, CV_8UC3))
            using (var corners = new Mat(4, 1, MakeType(5, 2))) // 4 points of float2 (CV_32FC2)
            {
                // Just verify that DrawChessboardCorners executes cleanly
                Cv2.DrawChessboardCorners(image, new Size(2, 2), corners, true);
                Assert.Equal(100, image.Rows);
            }
        }

        private static int MakeType(int depth, int channels)
        {
            return depth + ((channels - 1) << 5);
        }
    }
}
