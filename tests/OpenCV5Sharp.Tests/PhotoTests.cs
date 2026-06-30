// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PhotoTests
    {
        [Fact]
        public void TestInpaint()
        {
            const int CV_8UC3 = 64;
            const int CV_8UC1 = 0;

            // Create a 50x50 BGR image filled with white (255, 255, 255)
            using (var src = new Mat(50, 50, CV_8UC3))
            using (var mask = new Mat(50, 50, CV_8UC1))
            using (var dst = new Mat())
            {
                byte[] srcData = new byte[50 * 50 * 3];
                for (int i = 0; i < srcData.Length; i++) srcData[i] = 255;
                Marshal.Copy(srcData, 0, src.Data, srcData.Length);

                // Corrupt a 5x5 area in the middle (set to 0, 0, 0)
                byte[] maskData = new byte[50 * 50];
                for (int r = 22; r < 27; r++)
                {
                    for (int c = 22; c < 27; c++)
                    {
                        int srcIdx = (r * 50 + c) * 3;
                        srcData[srcIdx] = 0;
                        srcData[srcIdx + 1] = 0;
                        srcData[srcIdx + 2] = 0;

                        maskData[r * 50 + c] = 255;
                    }
                }
                Marshal.Copy(srcData, 0, src.Data, srcData.Length);
                Marshal.Copy(maskData, 0, mask.Data, maskData.Length);

                // Inpaint using Telea algorithm = 1
                Cv2.Inpaint(src, mask, dst, 3.0, 1);

                Assert.NotEqual(IntPtr.Zero, dst.Handle);
                Assert.Equal(50, dst.Rows);
                Assert.Equal(50, dst.Cols);

                // Verify that the corrupted pixels are no longer black
                byte[] dstData = new byte[50 * 50 * 3];
                Marshal.Copy(dst.Data, dstData, 0, dstData.Length);

                int middlePixelIdx = (24 * 50 + 24) * 3;
                Assert.True(dstData[middlePixelIdx] > 100); // Should be restored to near-white
            }
        }
    }
}
