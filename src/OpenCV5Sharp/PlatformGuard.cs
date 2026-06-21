// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Guards against usage on unsupported platforms.
    /// OpenCV5Sharp currently ships native binaries for Windows x64 only.
    /// </summary>
    internal static class PlatformGuard
    {
        static PlatformGuard()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException(
                    "OpenCV5Sharp currently supports Windows x64 only. " +
                    "Linux and macOS support is planned for a future release.");
            }

            if (RuntimeInformation.ProcessArchitecture != Architecture.X64)
            {
                throw new PlatformNotSupportedException(
                    $"OpenCV5Sharp requires x64 architecture. " +
                    $"Current architecture: {RuntimeInformation.ProcessArchitecture}.");
            }
        }

        /// <summary>
        /// Forces the static constructor to run, validating platform compatibility.
        /// Call this early in the library initialization path.
        /// </summary>
        internal static void EnsureSupported() { }
    }
}
