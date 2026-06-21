// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class LargeImageTests
    {
        [Fact]
        public void TestLargeDimensionsThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Mat(-1, 100, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Mat(100, -1, 0));
            Assert.Throws<ArgumentException>(() => new Mat(40000, 40000, 0)); // exceed max size
        }
    }
}
