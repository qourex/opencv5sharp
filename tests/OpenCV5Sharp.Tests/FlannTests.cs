// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class FlannTests
    {
        [Fact]
        public void FlannIndex_InstantiationAndDispose_Success()
        {
            // Verify that we can instantiate FlannIndex and its params classes cleanly
            var index = new FlannIndex();
            var indexParams = new FlannIndexParams();
            var searchParams = new FlannSearchParams();

            Assert.NotNull(index);
            Assert.NotEqual(IntPtr.Zero, index.Handle);

            Assert.NotNull(indexParams);
            Assert.NotEqual(IntPtr.Zero, indexParams.Handle);

            Assert.NotNull(searchParams);
            Assert.NotEqual(IntPtr.Zero, searchParams.Handle);

            // Dispose them
            index.Dispose();
            indexParams.Dispose();
            searchParams.Dispose();

            // Verify they are marked as disposed
            Assert.True(index.IsDisposed);
            Assert.True(indexParams.IsDisposed);
            Assert.True(searchParams.IsDisposed);

            // Calling operations on a disposed index must throw ObjectDisposedException safely in C#
            const int CV_32FC1 = 5;
            using (var query = new Mat(1, 2, CV_32FC1))
            using (var indices = new Mat())
            using (var dists = new Mat())
            {
                Assert.Throws<ObjectDisposedException>(() => index.KnnSearch(query, indices, dists, 2, searchParams));
            }
        }
    }
}
