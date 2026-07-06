// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

namespace OpenCV5Sharp.Extensions
{
    /// <summary>
    /// Extension methods for converting wrapper enums to integers to ease P/Invoke calls.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Converts <see cref="ImreadModes"/> to its integer value.
        /// </summary>
        public static int ToInt(this ImreadModes mode) => (int)mode;

        /// <summary>
        /// Converts <see cref="ColorConversionCodes"/> to its integer value.
        /// </summary>
        public static int ToInt(this ColorConversionCodes code) => (int)code;

        /// <summary>
        /// Converts <see cref="ThresholdTypes"/> to its integer value.
        /// </summary>
        public static int ToInt(this ThresholdTypes type) => (int)type;

        /// <summary>
        /// Converts <see cref="BorderTypes"/> to its integer value.
        /// </summary>
        public static int ToInt(this BorderTypes border) => (int)border;
    }
}
