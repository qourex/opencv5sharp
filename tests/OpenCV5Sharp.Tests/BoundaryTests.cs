// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class BoundaryTests
    {
        [Fact]
        public void TestEmptyMat0x0()
        {
            using (Mat mat = new Mat(0, 0, 0))
            {
                Assert.Equal(0, mat.Rows);
                Assert.Equal(0, mat.Cols);
                Assert.Equal(0, (int)mat.Total());
            }
        }

        [Fact]
        public void TestMat1x1()
        {
            using (Mat mat = new Mat(1, 1, 0))
            {
                Assert.Equal(1, mat.Rows);
                Assert.Equal(1, mat.Cols);
                Assert.Equal(1, (int)mat.Total());
            }
        }
    }
}
