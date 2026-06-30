// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ParallelMethodsTests
    {
        [Fact]
        public void TestParallelProcessRows()
        {
            const int CV_8UC1 = 0;
            using var src = new Mat(100, 100, CV_8UC1);
            using var dst = new Mat(100, 100, CV_8UC1);

            // Fill src with 5 using Marshal.Copy
            byte[] fillData = new byte[100 * 100];
            for (int i = 0; i < fillData.Length; i++) fillData[i] = 5;
            System.Runtime.InteropServices.Marshal.Copy(fillData, 0, src.Data, fillData.Length);

            // Run parallel row-by-row inversion
            Cv2.ParallelProcessRows(src, dst, (srcRow, dstRow, r) =>
            {
                Cv2.BitwiseNot(srcRow, dstRow, null);
            });

            // Inversion of 5 is 250 (255 - 5 = 250)
            byte[] pixelData = new byte[100 * 100];
            System.Runtime.InteropServices.Marshal.Copy(dst.Data, pixelData, 0, pixelData.Length);

            Assert.Equal(250, pixelData[0]);
            Assert.Equal(250, pixelData[500]);
        }

        [Fact]
        public void TestParallelProcessTiles()
        {
            const int CV_8UC1 = 0;
            using var src = new Mat(64, 64, CV_8UC1);
            using var dst = new Mat(64, 64, CV_8UC1);

            byte[] fillData = new byte[64 * 64];
            for (int i = 0; i < fillData.Length; i++) fillData[i] = 10;
            System.Runtime.InteropServices.Marshal.Copy(fillData, 0, src.Data, fillData.Length);

            // Process 16x16 tiles in parallel
            Cv2.ParallelProcessTiles(src, dst, new Size(16, 16), (srcTile, dstTile, rect) =>
            {
                // In C#, we construct a 1x1 matrix with value 2 to multiply
                using var factor = new Mat(srcTile.Rows, srcTile.Cols, CV_8UC1);
                byte[] factData = new byte[srcTile.Rows * srcTile.Cols];
                for (int j = 0; j < factData.Length; j++) factData[j] = 2;
                System.Runtime.InteropServices.Marshal.Copy(factData, 0, factor.Data, factData.Length);

                Cv2.Multiply(srcTile, factor, dstTile, 1.0, -1);
            });

            byte[] pixelData = new byte[64 * 64];
            System.Runtime.InteropServices.Marshal.Copy(dst.Data, pixelData, 0, pixelData.Length);

            Assert.Equal(20, pixelData[0]);
            Assert.Equal(20, pixelData[1024]);
        }

        [Fact]
        public void TestParallelBatchProcess()
        {
            const int CV_8UC1 = 0;
            var list = new List<Mat>();
            for (int i = 0; i < 5; i++)
            {
                var mat = new Mat(10, 10, CV_8UC1);
                byte[] fillData = new byte[100];
                for (int j = 0; j < fillData.Length; j++) fillData[j] = (byte)i;
                System.Runtime.InteropServices.Marshal.Copy(fillData, 0, mat.Data, fillData.Length);
                list.Add(mat);
            }

            var results = Cv2.ParallelBatchProcess(list, mat =>
            {
                var result = new Mat(10, 10, CV_8UC1);
                using var factor = new Mat(10, 10, CV_8UC1);
                byte[] factData = new byte[100];
                for (int j = 0; j < factData.Length; j++) factData[j] = 3;
                System.Runtime.InteropServices.Marshal.Copy(factData, 0, factor.Data, factData.Length);

                Cv2.Multiply(mat, factor, result, 1.0, -1);
                return result;
            });

            Assert.Equal(5, results.Length);
            for (int i = 0; i < 5; i++)
            {
                byte[] data = new byte[100];
                System.Runtime.InteropServices.Marshal.Copy(results[i].Data, data, 0, data.Length);
                Assert.Equal(i * 3, data[0]);

                results[i].Dispose();
                list[i].Dispose();
            }
        }

        [Fact]
        public void TestParallelValidation()
        {
            Assert.Throws<ArgumentNullException>(() => Cv2.ParallelProcessRows(null!, new Mat(), (s, d, r) => { }));
            Assert.Throws<ArgumentNullException>(() => Cv2.ParallelProcessTiles(null!, new Mat(), new Size(10, 10), (s, d, r) => { }));
            Assert.Throws<ArgumentNullException>(() => Cv2.ParallelBatchProcess(null!, m => m));
        }
    }
}
