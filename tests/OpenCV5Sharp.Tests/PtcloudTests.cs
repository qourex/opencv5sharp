// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PtcloudTests
    {
        [Fact]
        public void Octree_CreateWithDepth_Success()
        {
            // Allocate origin: (0f, 0f, 0f)
            IntPtr originPtr = Marshal.AllocHGlobal(12); // 3 * sizeof(float)
            float[] origin = { 0.0f, 0.0f, 0.0f };
            Marshal.Copy(origin, 0, originPtr, 3);
            try
            {
                using (var octree = Octree.CreateWithDepth(4, 20.0, originPtr, false))
                {
                    Assert.NotNull(octree);
                    Assert.NotEqual(IntPtr.Zero, octree!.Handle);

                    // Insert point: (1.0f, 2.0f, 3.0f)
                    IntPtr pointPtr = Marshal.AllocHGlobal(12);
                    float[] point = { 1.0f, 2.0f, 3.0f };
                    Marshal.Copy(point, 0, pointPtr, 3);
                    try
                    {
                        bool success = octree.InsertPoint(pointPtr, IntPtr.Zero);
                        Assert.True(success);

                        bool inBound = octree.IsPointInBound(pointPtr);
                        Assert.True(inBound);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(pointPtr);
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(originPtr);
            }
        }
    }
}
