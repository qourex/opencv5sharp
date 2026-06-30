// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void Imencode_Imdecode_BgrImage_Success()
        {
            const int CV_8UC3 = 64;
            // Create a BGR image with a solid color
            using (var img = new Mat(50, 50, CV_8UC3))
            {
                // Fill with BGR (0, 0, 255) - red
                int size = img.Rows * img.Cols * img.Channels();
                byte[] fillData = new byte[size];
                for (int i = 0; i < size; i += 3)
                {
                    fillData[i] = 0;     // Blue
                    fillData[i + 1] = 0; // Green
                    fillData[i + 2] = 255; // Red
                }
                Marshal.Copy(fillData, 0, img.Data, size);

                // Encode to PNG byte array
                byte[] encodedBytes = Cv2.Imencode(".png", img);
                Assert.NotNull(encodedBytes);
                Assert.True(encodedBytes.Length > 0);

                // Decode back to Mat
                using (var decoded = Cv2.Imdecode(encodedBytes, 1)) // 1 = IMREAD_COLOR
                {
                    Assert.NotNull(decoded);
                    Assert.NotEqual(IntPtr.Zero, decoded.Handle);
                    Assert.Equal(50, decoded.Rows);
                    Assert.Equal(50, decoded.Cols);
                    Assert.Equal(3, decoded.Channels());

                    // Verify pixel color in the center
                    byte[] readData = new byte[size];
                    Marshal.Copy(decoded.Data, readData, 0, size);
                    int midIdx = (25 * 50 + 25) * 3;
                    Assert.Equal(0, readData[midIdx]);       // Blue
                    Assert.Equal(0, readData[midIdx + 1]);   // Green
                    Assert.Equal(255, readData[midIdx + 2]); // Red
                }
            }
        }
    }
}
