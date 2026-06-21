// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class MatBasicsTests
    {
        [Fact]
        public void TestMatBasics()
        {
            const int CV_8UC3 = 64; // 8-bit unsigned 3-channel (BGR)

            using (Mat mat = new Mat(200, 300, CV_8UC3))
            {
                Assert.NotEqual(IntPtr.Zero, mat.Handle);
                Assert.Equal(200, mat.Rows);
                Assert.Equal(300, mat.Cols);
                Assert.Equal(CV_8UC3, mat.Type());
                Assert.Equal(0, mat.Depth());
                Assert.Equal(3, mat.Channels());
                Assert.Equal(60000, (int)mat.Total());
                Assert.True(mat.IsContinuous());
                Assert.False(mat.IsSubmatrix());
            }
        }
    }
}
