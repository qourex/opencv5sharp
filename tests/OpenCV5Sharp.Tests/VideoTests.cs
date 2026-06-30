// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class VideoTests
    {
        [Fact]
        public void TestKalmanFilter()
        {
            // type float = 5 (CV_32F)
            const int CV_32F = 5;
            using (var kf = new KalmanFilter(4, 2, 0, CV_32F))
            {
                Assert.NotNull(kf);
                Assert.NotEqual(IntPtr.Zero, kf!.Handle);

                // Initialize matrices
                using (var state = new Mat(4, 1, CV_32F))
                using (var transitionMat = new Mat(4, 4, CV_32F))
                {
                    // Set transition matrix diagonal to 1.0 (identity)
                    float[] transData = new float[16];
                    for (int i = 0; i < 4; i++) transData[i * 4 + i] = 1.0f;
                    Marshal.Copy(transData, 0, transitionMat.Data, transData.Length);
                    kf.TransitionMatrix = transitionMat;

                    // Predict
                    using (Mat? prediction = kf.Predict(null))
                    {
                        Assert.NotNull(prediction);
                        Assert.NotEqual(IntPtr.Zero, prediction!.Handle);
                        Assert.Equal(4, prediction.Rows);
                        Assert.Equal(1, prediction.Cols);
                    }

                    // Correct
                    using (var measurement = new Mat(2, 1, CV_32F))
                    {
                        float[] measData = new float[] { 10.0f, 20.0f };
                        Marshal.Copy(measData, 0, measurement.Data, measData.Length);

                        using (Mat? correction = kf.Correct(measurement))
                        {
                            Assert.NotNull(correction);
                            Assert.NotEqual(IntPtr.Zero, correction!.Handle);
                            Assert.Equal(4, correction.Rows);
                        }
                    }
                }
            }
        }

        [Fact]
        public void TestBackgroundSubtractorMOG2()
        {
            const int CV_8UC3 = 64;
            using (var sub = Cv2.CreateBackgroundSubtractorMOG2(500, 16.0, true))
            {
                Assert.NotNull(sub);
                Assert.NotEqual(IntPtr.Zero, sub!.Handle);

                Assert.Equal(500, sub.GetHistory());
                sub.SetHistory(300);
                Assert.Equal(300, sub.GetHistory());

                // Feed 2 dummy frames to it
                using (var frame = new Mat(50, 50, CV_8UC3))
                using (var mask = new Mat())
                {
                    sub.Apply(frame, mask, -1.0);
                    Assert.NotEqual(IntPtr.Zero, mask.Handle);
                    Assert.Equal(50, mask.Rows);
                }
            }
        }
    }
}
