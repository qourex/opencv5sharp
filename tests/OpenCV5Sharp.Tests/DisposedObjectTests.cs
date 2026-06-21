// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DisposedObjectTests
    {
        [Fact]
        public void TestDisposedObjectThrowsException()
        {
            Mat m = new Mat();
            m.Dispose();
            Assert.Throws<ObjectDisposedException>(() => m.Clone());
            Assert.Throws<ObjectDisposedException>(() => Cv2.CvtColor(m, new Mat(), 6, 0, AlgorithmHint.Default));
        }
    }
}
