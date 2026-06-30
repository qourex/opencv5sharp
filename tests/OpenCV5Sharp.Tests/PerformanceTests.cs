// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PerformanceTests
    {
        [Fact]
        public void Threshold_PerformanceBudget_Success()
        {
            const int CV_8UC1 = 0;
            const int iterations = 1000;
            
            using (var src = new Mat(500, 500, CV_8UC1))
            using (var dst = new Mat())
            {
                // Fill image directly via Marshal.Copy
                byte[] data = new byte[500 * 500];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)(i % 256);
                }
                Marshal.Copy(data, 0, src.Data, data.Length);

                // Run once for JIT warm-up
                Cv2.Threshold(src, dst, 128, 255, 0);

                var sw = Stopwatch.StartNew();
                for (int i = 0; i < iterations; i++)
                {
                    Cv2.Threshold(src, dst, 128, 255, 0);
                }
                sw.Stop();

                double avgMs = sw.Elapsed.TotalMilliseconds / iterations;
                
                // Assert reasonable time budget (typically takes < 1ms per run on modern hardware)
                Assert.True(avgMs < 10.0, $"Average execution time {avgMs:F3} ms exceeded performance budget of 10.0 ms.");
            }
        }
    }
}
