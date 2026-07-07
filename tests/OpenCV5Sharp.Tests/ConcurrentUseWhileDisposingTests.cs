// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ConcurrentUseWhileDisposingTests
    {
        [Fact]
        public async Task Mat_ConcurrentReadAndDispose_NeverThrowsAccessViolation()
        {
            const int CV_8UC1 = 0;
            const int Iterations = 20;

            var exceptions = new List<Exception>();

            for (int iteration = 0; iteration < Iterations; iteration++)
            {
                var mat = new Mat(100, 100, CV_8UC1);

                var tasks = new Task[5];

                // 4 tasks trying to read properties
                for (int t = 0; t < 4; t++)
                {
                    tasks[t] = Task.Run(() =>
                    {
                        try
                        {
                            // Attempt to read properties; may succeed or throw ObjectDisposedException
                            for (int r = 0; r < 50; r++)
                            {
                                _ = mat.Rows;
                                _ = mat.Cols;
                                _ = mat.Empty();
                            }
                        }
                        catch (AccessViolationException ex)
                        {
                            // This must NEVER happen — if it does, our SafeHandle is broken
                            lock (exceptions)
                            {
                                exceptions.Add(ex);
                            }
                        }
                        catch (Exception)
                        {
                            // Expected — any other exception is acceptable when concurrently reading and disposing
                        }
                    });
                }

                // 1 task trying to dispose
                tasks[4] = Task.Run(() =>
                {
                    try
                    {
                        mat.Dispose();
                    }
                    catch (Exception ex)
                    {
                        lock (exceptions)
                        {
                            exceptions.Add(ex);
                        }
                    }
                });

                await Task.WhenAll(tasks);

                // Ensure dispose happened
                Assert.True(mat.IsDisposed, $"Iteration {iteration}: Mat should be disposed after Task.WaitAll");
            }

            // The critical assertion: no AccessViolationExceptions were caught
            Assert.Empty(exceptions);
        }
    }
}
