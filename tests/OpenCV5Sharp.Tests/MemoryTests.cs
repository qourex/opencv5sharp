// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class MemoryTests
    {
        [Fact]
        public void TestMemoryManagementAndLeaks()
        {
            const int CV_8UC3 = 64;

            // Warm up and run GC before capturing start memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long startMemory = GC.GetTotalMemory(true);

            for (int i = 0; i < 2000; i++)
            {
                using (Mat m1 = new Mat(100, 100, CV_8UC3))
                {
                    using (Mat m2 = m1.Clone())
                    {
                        Assert.False(m2.Empty());
                    }
                }
            }

            // Run GC after loops to clean up any transient objects
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            long endMemory = GC.GetTotalMemory(true);
            
            long diff = endMemory - startMemory;
            // Strict threshold of 500 KB (accounting for transient heap / JIT metadata variations)
            Assert.True(diff / 1024 < 500, $"Memory difference too high: {diff / 1024} KB");
        }
    }
}
