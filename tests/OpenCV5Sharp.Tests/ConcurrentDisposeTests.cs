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
        public void TestConcurrentDisposeDoesNotCrash()
        {
            for (int i = 0; i < 50; i++)
            {
                Mat m = new Mat(100, 100, 0);
                Parallel.Invoke(
                    () => m.Dispose(),
                    () => m.Dispose(),
                    () => m.Dispose()
                );
            }
        }
    }
}
