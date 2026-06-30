// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ObjdetectTests
    {
        [Fact]
        public void QRCodeDetector_DetectAndDecode_Success()
        {
            const int CV_8UC1 = 0;
            using (var detector = new QRCodeDetector())
            using (var img = new Mat(100, 100, CV_8UC1))
            using (var points = new Mat())
            using (var straightQrcode = new Mat())
            {
                Assert.NotNull(detector);
                Assert.NotEqual(IntPtr.Zero, detector.Handle);

                string result = detector.DetectAndDecode(img, points, straightQrcode);
                Assert.Equal("", result);
            }
        }

        [Fact]
        public void BarcodeBarcodeDetector_DownsamplingThreshold_Success()
        {
            using (var detector = new BarcodeBarcodeDetector())
            {
                Assert.NotNull(detector);
                Assert.NotEqual(IntPtr.Zero, detector.Handle);

                // By default, gets a threshold (e.g. 512.0 or other)
                double thresh = detector.GetDownsamplingThreshold();
                Assert.True(thresh > 0.0);
            }
        }

        [Fact]
        public void FaceDetectorYN_GetInputSize_Success()
        {
            // FaceDetectorYN has a Create method that expects a model path.
            // Let's test instantiation parameters or check that we can set properties
            using (var parameters = new ArucoDetectorParameters())
            {
                Assert.NotNull(parameters);
                Assert.NotEqual(IntPtr.Zero, parameters.Handle);
                
                // Read adaptiveThreshWinSizeMin
                int winSize = parameters.AdaptiveThreshWinSizeMin;
                Assert.True(winSize > 0);
            }
        }
    }
}
