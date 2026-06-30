// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

// Restrict DLL search to the assembly directory to prevent DLL hijacking (CWE-426).
// This ensures opencv5sharp_native.dll and opencv_world500.dll are loaded only from
// the application's own directory, not from untrusted locations in the system PATH.
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.AssemblyDirectory | DllImportSearchPath.UseDllDirectoryForDependencies)]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("OpenCV5Sharp.Samples")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("OpenCV5Sharp.Tests")]
