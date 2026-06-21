// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class InvalidFormatTests
    {
        [Fact]
        public void TestInvalidColorConversionCodeThrowsException()
        {
            const int CV_8UC3 = 64;
            using (Mat src = new Mat(100, 100, CV_8UC3))
            using (Mat dst = new Mat())
            {
                // Passing an invalid color conversion code (e.g., -1 or 9999) should throw OpenCVException
                Assert.Throws<OpenCVException>(() => Cv2.CvtColor(src, dst, -1, 0, AlgorithmHint.Default));
                Assert.Throws<OpenCVException>(() => Cv2.CvtColor(src, dst, 9999, 0, AlgorithmHint.Default));
            }
        }
    }
}
