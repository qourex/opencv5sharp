// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ErrorMessageTests
    {
        [Fact]
        public void TestErrorMessageContainsDetails()
        {
            var ex = Assert.Throws<OpenCVException>(() =>
            {
                using (Mat empty = new Mat())
                using (Mat dst = new Mat())
                {
                    Cv2.CvtColor(empty, dst, 6, 0, AlgorithmHint.Default);
                }
            });
            Assert.Contains("empty", ex.Message.ToLower());
        }
    }
}
