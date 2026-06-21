// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PixelAccessTests
    {
        [Fact]
        public void TestPixelAccess()
        {
            const int CV_8UC3 = 64;

            using (Mat mat = new Mat(100, 100, CV_8UC3))
            {
                IntPtr dataPtr = mat.Data;
                Assert.NotEqual(IntPtr.Zero, dataPtr);

                // Fill image with blue color (BGR: 255, 0, 0)
                int size = mat.Rows * mat.Cols * mat.Channels();
                byte[] blueBuffer = new byte[size];
                for (int i = 0; i < size; i += 3)
                {
                    blueBuffer[i] = 255;   // Blue
                    blueBuffer[i + 1] = 0; // Green
                    blueBuffer[i + 2] = 0; // Red
                }

                // Copy to unmanaged memory
                Marshal.Copy(blueBuffer, 0, dataPtr, size);

                // Read back and verify a pixel in the middle (50, 50)
                byte[] readBuffer = new byte[size];
                Marshal.Copy(dataPtr, readBuffer, 0, size);

                int midIdx = (50 * 100 + 50) * 3;
                Assert.Equal(255, readBuffer[midIdx]);
                Assert.Equal(0, readBuffer[midIdx + 1]);
                Assert.Equal(0, readBuffer[midIdx + 2]);
            }
        }
    }
}
