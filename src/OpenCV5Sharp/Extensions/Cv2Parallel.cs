// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenCV5Sharp
{
    public static partial class Cv2
    {
        /// <summary>
        /// Executes a parallel loop to process individual rows of a matrix.
        /// Each row is processed in parallel using the Task Parallel Library (TPL).
        /// </summary>
        /// <param name="src">The source matrix to read from. Must not be null or disposed.</param>
        /// <param name="dst">The destination matrix to write to. Must not be null or disposed. Must match <paramref name="src"/> in size.</param>
        /// <param name="processRow">
        /// The delegate invoked for each row index. 
        /// Arguments are: (source row mat, destination row mat, row index).
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="src"/>, <paramref name="dst"/>, or <paramref name="processRow"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="src"/> or <paramref name="dst"/> has been disposed.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="dst"/> size does not match <paramref name="src"/>.</exception>
        /// <example>
        /// <code>
        /// Cv2.ParallelProcessRows(src, dst, (srcRow, dstRow, rowIndex) => {
        ///     Cv2.BitwiseNot(srcRow, dstRow);
        /// });
        /// </code>
        /// </example>
        public static void ParallelProcessRows(Mat src, Mat dst, Action<Mat, Mat, int> processRow)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));
            if (processRow == null) throw new ArgumentNullException(nameof(processRow));

            src.ThrowIfDisposed();
            dst.ThrowIfDisposed();

            if (src.Rows != dst.Rows || src.Cols != dst.Cols)
                throw new ArgumentException("Destination matrix size must match the source matrix size.");

            int totalRows = src.Rows;
            int totalCols = src.Cols;

            Parallel.For(0, totalRows, r =>
            {
                using var srcRow = new Mat(src, new Rect(0, r, totalCols, 1));
                using var dstRow = new Mat(dst, new Rect(0, r, totalCols, 1));
                processRow(srcRow, dstRow, r);
            });
        }

        /// <summary>
        /// Executes a parallel loop to process a matrix divided into non-overlapping grid tiles.
        /// Each tile is processed in parallel using the Task Parallel Library (TPL).
        /// </summary>
        /// <param name="src">The source matrix to read from. Must not be null or disposed.</param>
        /// <param name="dst">The destination matrix to write to. Must not be null or disposed. Must match <paramref name="src"/> in size.</param>
        /// <param name="tileSize">The size of each tile (width and height). Must be greater than zero.</param>
        /// <param name="processTile">
        /// The delegate invoked for each tile. 
        /// Arguments are: (source tile mat, destination tile mat, tile bounding rectangle).
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="src"/>, <paramref name="dst"/>, or <paramref name="processTile"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="src"/> or <paramref name="dst"/> has been disposed.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="dst"/> size does not match <paramref name="src"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="tileSize"/> width or height is less than or equal to zero.</exception>
        /// <example>
        /// <code>
        /// Cv2.ParallelProcessTiles(src, dst, new Size(64, 64), (srcTile, dstTile, roi) => {
        ///     Cv2.GaussianBlur(srcTile, dstTile, new Size(5, 5), 0);
        /// });
        /// </code>
        /// </example>
        public static void ParallelProcessTiles(Mat src, Mat dst, Size tileSize, Action<Mat, Mat, Rect> processTile)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (dst == null) throw new ArgumentNullException(nameof(dst));
            if (processTile == null) throw new ArgumentNullException(nameof(processTile));

            src.ThrowIfDisposed();
            dst.ThrowIfDisposed();

            if (src.Rows != dst.Rows || src.Cols != dst.Cols)
                throw new ArgumentException("Destination matrix size must match the source matrix size.");

            if (tileSize.Width <= 0 || tileSize.Height <= 0)
                throw new ArgumentOutOfRangeException(nameof(tileSize), "Tile width and height must be greater than zero.");

            int cols = src.Cols;
            int rows = src.Rows;

            var tiles = new List<Rect>();
            for (int y = 0; y < rows; y += tileSize.Height)
            {
                int h = Math.Min(tileSize.Height, rows - y);
                for (int x = 0; x < cols; x += tileSize.Width)
                {
                    int w = Math.Min(tileSize.Width, cols - x);
                    tiles.Add(new Rect(x, y, w, h));
                }
            }

            Parallel.ForEach(tiles, tile =>
            {
                using var srcTile = new Mat(src, tile);
                using var dstTile = new Mat(dst, tile);
                processTile(srcTile, dstTile, tile);
            });
        }

        /// <summary>
        /// Processes a batch of input matrices concurrently in parallel using the Task Parallel Library (TPL).
        /// </summary>
        /// <param name="inputs">The collection of source matrices to process. Must not contain null or disposed matrices.</param>
        /// <param name="processor">
        /// The function invoked for each matrix in the batch.
        /// Takes a source matrix and returns a new processed matrix.
        /// </param>
        /// <returns>A thread-safe array containing the processed matrices in the original order.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="inputs"/> or <paramref name="processor"/> is null, or when an individual matrix inside <paramref name="inputs"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when an input matrix inside <paramref name="inputs"/> has been disposed.</exception>
        /// <example>
        /// <code>
        /// var results = Cv2.ParallelBatchProcess(images, img => {
        ///     var gray = new Mat();
        ///     Cv2.CvtColor(img, gray, ColorConversionCodes.BGR2GRAY);
        ///     return gray;
        /// });
        /// </code>
        /// </example>
        public static Mat[] ParallelBatchProcess(IList<Mat> inputs, Func<Mat, Mat> processor)
        {
            if (inputs == null) throw new ArgumentNullException(nameof(inputs));
            if (processor == null) throw new ArgumentNullException(nameof(processor));

            int count = inputs.Count;
            var results = new Mat[count];

            Parallel.For(0, count, i =>
            {
                var input = inputs[i];
                if (input == null)
                    throw new ArgumentNullException($"Matrix at index {i} in the inputs list is null.");

                input.ThrowIfDisposed();

                results[i] = processor(input);
            });

            return results;
        }
    }
}
