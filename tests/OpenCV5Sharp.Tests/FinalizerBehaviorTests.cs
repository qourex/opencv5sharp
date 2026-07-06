// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class FinalizerBehaviorTests
    {
        [Fact]
        public void Mat_FinalizerPath_DoesNotCrash()
        {
            // Create Mat objects WITHOUT using or Dispose — rely on the SafeHandle finalizer.
            // The test passing without an AccessViolationException IS the verification.
            const int CV_8UC1 = 0;

            for (int i = 0; i < 10; i++)
            {
                CreateOrphanedMat(CV_8UC1);
            }

            // Force the GC to collect and finalize the orphaned objects
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // If we reached here, the SafeHandle finalizer path works correctly
            Assert.True(true, "SafeHandle finalizer path completed without crash.");
        }

        /// <summary>
        /// Creates a Mat and intentionally does NOT dispose it, returning without any reference.
        /// This forces the finalizer to run on GC.
        /// </summary>
        private static void CreateOrphanedMat(int type)
        {
            // Allocate a non-trivial Mat so the native memory is meaningful
            var m = new Mat(50, 50, type);
            // Do something with it to prevent the JIT from optimizing it away
            _ = m.Rows;
            // Intentionally let it fall out of scope without Dispose
            m = null;
        }
    }
}
