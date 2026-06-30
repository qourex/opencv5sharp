// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Guards against usage on unsupported platforms.
    /// OpenCV5Sharp ships native binaries for Windows (x64), Linux (x64),
    /// macOS (x64, ARM64), Android (ARM64), and iOS (ARM64).
    /// </summary>
    internal static class PlatformGuard
    {
        static PlatformGuard()
        {
            bool supported = false;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && RuntimeInformation.ProcessArchitecture == Architecture.X64)
                supported = true;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && RuntimeInformation.ProcessArchitecture == Architecture.X64)
                supported = true;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) && (RuntimeInformation.ProcessArchitecture == Architecture.X64 || RuntimeInformation.ProcessArchitecture == Architecture.Arm64))
                supported = true;
#if NET8_0_OR_GREATER
            else if (OperatingSystem.IsAndroid() && RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
                supported = true;
            else if (OperatingSystem.IsIOS() && RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
                supported = true;
#endif

            if (!supported)
            {
                throw new PlatformNotSupportedException(
                    $"OpenCV5Sharp does not support {RuntimeInformation.RuntimeIdentifier}. " +
                    $"Supported platforms: win-x64, linux-x64, osx-x64, osx-arm64, android-arm64, ios-arm64.");
            }
        }

        /// <summary>
        /// Forces the static constructor to run, validating platform compatibility.
        /// Call this early in the library initialization path.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">Thrown when executing on an unsupported OS or process architecture.</exception>
        internal static void EnsureSupported() { }
    }
}
