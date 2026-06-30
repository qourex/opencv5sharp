// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class StitchingTests
    {
        [Fact]
        public void TestStitcherWorkflow()
        {
            const int CV_8UC3 = 64;
            // Create two small dummy images
            using (var left = new Mat(100, 100, CV_8UC3))
            using (var right = new Mat(100, 100, CV_8UC3))
            using (var pano = new Mat())
            {
                // Draw some shapes to ensure mats are not empty/zero-filled
                Cv2.Circle(left, new Point(50, 50), 20, new Scalar(255, 0, 0), -1, 8, 0);
                Cv2.Circle(right, new Point(50, 50), 20, new Scalar(255, 0, 0), -1, 8, 0);
                // Create a standard VectorMat using handles
                IntPtr[] handles = new IntPtr[] { left.Handle, right.Handle };
                IntPtr vecPtr = NativeMethods.cv_VectorMat_New(handles, handles.Length);

                try
                {
                    // Create Stitcher (cast 0 to FileStorageMode which maps to StitcherMode.Panorama = 0)
                    using (var stitcher = Stitcher.Create((FileStorageMode)0))
                    {
                        Assert.NotNull(stitcher);
                        Assert.NotEqual(IntPtr.Zero, stitcher!.Handle);

                        // Running stitching on dummy black images will return a failure status (e.g. ErrorNeedMoreImgs or similar)
                        // but it should run successfully and return a valid StitcherStatus enum rather than crashing.
                        StitcherStatus status = stitcher.Stitch(vecPtr, pano);
                        
                        // Assert that the enum returned is a valid defined StitcherStatus value
                        Assert.True(Enum.IsDefined(typeof(StitcherStatus), status));
                    }
                }
                finally
                {
                    NativeMethods.cv_VectorMat_Delete(vecPtr);
                }
            }
        }
    }
}
