// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class NullArgumentTests
    {
        [Fact]
        public void TestNullArgumentThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Cv2.CvtColor(null!, new Mat(), 6, 0, AlgorithmHint.Default));
            Assert.Throws<ArgumentNullException>(() => Cv2.CvtColor(new Mat(), null!, 6, 0, AlgorithmHint.Default));
        }
    }
}
