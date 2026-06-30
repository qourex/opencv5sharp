// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ConcurrentDisposeTests
    {
        [Fact]
        public void Mat_ConcurrentDispose_SucceedsAndSetsDisposedState()
        {
            for (int i = 0; i < 50; i++)
            {
                Mat m = new Mat(100, 100, 0);
                Assert.False(m.IsDisposed);
                Assert.NotEqual(IntPtr.Zero, m.Handle);

                Parallel.Invoke(
                    () => m.Dispose(),
                    () => m.Dispose(),
                    () => m.Dispose()
                );

                // Assert that concurrent dispose correctly sets Disposed state (L11)
                Assert.True(m.IsDisposed);
                Assert.Equal(IntPtr.Zero, m.Handle);
            }
        }
    }
}
