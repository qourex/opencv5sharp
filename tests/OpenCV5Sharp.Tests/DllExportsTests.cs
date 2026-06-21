// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DllExportsTests
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeLibrary(IntPtr hModule);

        [Fact]
        public void TestDllExportsCoverage()
        {
            Type nativeMethodsType = typeof(Cv2).Assembly.GetType("OpenCV5Sharp.NativeMethods")
                ?? throw new Exception("Could not find OpenCV5Sharp.NativeMethods type.");

            var methods = nativeMethodsType.GetMethods(BindingFlags.Public |
                                                       BindingFlags.NonPublic |
                                                       BindingFlags.Static);

            string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            string dllPath = Path.Combine(assemblyDir, "runtimes", "win-x64", "native", "opencv5sharp_native.dll");
            IntPtr hModule = LoadLibrary(dllPath);
            Assert.NotEqual(IntPtr.Zero, hModule);

            try
            {
                int missingCount = 0;
                var missingMethods = new List<string>();

                foreach (var method in methods)
                {
                    if ((method.Attributes & MethodAttributes.PinvokeImpl) == 0)
                    {
                        continue;
                    }

                    string entryPoint = method.Name;
                    var attrs = method.GetCustomAttributes(typeof(DllImportAttribute), false);
                    if (attrs.Length > 0)
                    {
                        var dllImportAttr = (DllImportAttribute)attrs[0];
                        if (!string.IsNullOrEmpty(dllImportAttr.EntryPoint))
                        {
                            entryPoint = dllImportAttr.EntryPoint;
                        }
                    }

                    IntPtr addr = GetProcAddress(hModule, entryPoint);
                    if (addr == IntPtr.Zero)
                    {
                        missingCount++;
                        missingMethods.Add(entryPoint);
                    }
                }

                if (missingCount > 0)
                {
                    string details = string.Join(", ", missingMethods.GetRange(0, Math.Min(missingCount, 10)));
                    Assert.Fail($"{missingCount} DLL exports were missing in opencv5sharp_native.dll! Missing: {details}");
                }
            }
            finally
            {
                FreeLibrary(hModule);
            }
        }
    }
}
