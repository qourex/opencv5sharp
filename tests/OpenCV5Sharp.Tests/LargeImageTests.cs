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
        public void Mat_InvalidLargeDimensions_ThrowsExceptions()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Mat(-1, 100, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Mat(100, -1, 0));
            Assert.Throws<ArgumentException>(() => new Mat(40000, 40000, 0)); // exceed max size
        }

        [Fact]
        public void Mat_ValidLargeDimensions_Succeeds()
        {
            const int CV_8UC1 = 0;
            // Create a 10000x10000 image (100 million pixels, 100MB of unmanaged memory)
            using (var mat = new Mat(10000, 10000, CV_8UC1))
            {
                Assert.NotNull(mat);
                Assert.NotEqual(IntPtr.Zero, mat.Handle);
                Assert.Equal(10000, mat.Rows);
                Assert.Equal(10000, mat.Cols);
                Assert.False(mat.Empty());
            }
        }
    }
}
