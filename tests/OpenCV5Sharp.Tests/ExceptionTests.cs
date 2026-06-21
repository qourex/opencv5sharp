// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void TestExceptionBoundary()
        {
            // Trigger a real C++ exception by passing an empty Mat to CvtColor
            using (Mat empty = new Mat())
            using (Mat dst = new Mat())
            {
                Assert.Throws<OpenCVException>(() => Cv2.CvtColor(empty, dst, 6, 0, AlgorithmHint.Default));
            }
        }
    }
}
