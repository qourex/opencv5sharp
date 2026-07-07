// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Represents OpenCV 5.0 matrix type constants.
    /// In OpenCV 5.0, type encoding uses a 5-bit shift for channels: depth + ((channels - 1) &lt;&lt; 5).
    /// </summary>
    public static class MatType
    {
        // Depth Constants
        public const int CV_8U = 0;
        public const int CV_8S = 1;
        public const int CV_16U = 2;
        public const int CV_16S = 3;
        public const int CV_32S = 4;
        public const int CV_32F = 5;
        public const int CV_64F = 6;
        public const int CV_16F = 7;

        // Common Matrix Types (OpenCV 5.0 Compliant)
        public const int CV_8UC1 = CV_8U + ((1 - 1) << 5); // 0
        public const int CV_8UC2 = CV_8U + ((2 - 1) << 5); // 32
        public const int CV_8UC3 = CV_8U + ((3 - 1) << 5); // 64
        public const int CV_8UC4 = CV_8U + ((4 - 1) << 5); // 96

        public const int CV_8SC1 = CV_8S + ((1 - 1) << 5); // 1
        public const int CV_8SC2 = CV_8S + ((2 - 1) << 5); // 33
        public const int CV_8SC3 = CV_8S + ((3 - 1) << 5); // 65
        public const int CV_8SC4 = CV_8S + ((4 - 1) << 5); // 97

        public const int CV_16UC1 = CV_16U + ((1 - 1) << 5); // 2
        public const int CV_16UC2 = CV_16U + ((2 - 1) << 5); // 34
        public const int CV_16UC3 = CV_16U + ((3 - 1) << 5); // 66
        public const int CV_16UC4 = CV_16U + ((4 - 1) << 5); // 98

        public const int CV_16SC1 = CV_16S + ((1 - 1) << 5); // 3
        public const int CV_16SC2 = CV_16S + ((2 - 1) << 5); // 35
        public const int CV_16SC3 = CV_16S + ((3 - 1) << 5); // 67
        public const int CV_16SC4 = CV_16S + ((4 - 1) << 5); // 99

        public const int CV_32SC1 = CV_32S + ((1 - 1) << 5); // 4
        public const int CV_32SC2 = CV_32S + ((2 - 1) << 5); // 36
        public const int CV_32SC3 = CV_32S + ((3 - 1) << 5); // 68
        public const int CV_32SC4 = CV_32S + ((4 - 1) << 5); // 100

        public const int CV_32FC1 = CV_32F + ((1 - 1) << 5); // 5
        public const int CV_32FC2 = CV_32F + ((2 - 1) << 5); // 37
        public const int CV_32FC3 = CV_32F + ((3 - 1) << 5); // 69
        public const int CV_32FC4 = CV_32F + ((4 - 1) << 5); // 101

        public const int CV_64FC1 = CV_64F + ((1 - 1) << 5); // 6
        public const int CV_64FC2 = CV_64F + ((2 - 1) << 5); // 38
        public const int CV_64FC3 = CV_64F + ((3 - 1) << 5); // 70
        public const int CV_64FC4 = CV_64F + ((4 - 1) << 5); // 102

        /// <summary>
        /// Computes the type identifier dynamically based on depth and channel count.
        /// </summary>
        public static int MakeType(int depth, int channels)
        {
            if (depth < 0 || depth > 7)
                throw new ArgumentOutOfRangeException(nameof(depth), "Depth must be between 0 (CV_8U) and 7 (CV_16F).");
            if (channels < 1 || channels > 512)
                throw new ArgumentOutOfRangeException(nameof(channels), "Channels must be positive (1-512).");
            return depth + ((channels - 1) << 5);
        }
    }
}
