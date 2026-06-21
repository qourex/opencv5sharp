// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Threading.Tasks;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ConcurrencyTests
    {
        [Fact]
        public async Task TestConcurrency()
        {
            const int CV_8UC3 = 64;
            const int NumThreads = 4;
            const int IterationsPerThread = 200;

            Task[] tasks = new Task[NumThreads];
            for (int t = 0; t < NumThreads; t++)
            {
                int threadId = t;
                tasks[t] = Task.Run(() =>
                {
                    for (int i = 0; i < IterationsPerThread; i++)
                    {
                        using (Mat m1 = new Mat(100, 100, CV_8UC3))
                        {
                            using (Mat m2 = m1.Clone())
                            {
                                Assert.Equal(100, m2.Rows);
                                Assert.Equal(100, m2.Cols);
                            }
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
        }
    }
}
