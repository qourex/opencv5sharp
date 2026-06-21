// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DllSearchPathTests
    {
        [Fact]
        public void TestDefaultDllImportSearchPathsAttributeIsPresent()
        {
            Assembly assembly = typeof(Cv2).Assembly;
            DefaultDllImportSearchPathsAttribute? attr = assembly.GetCustomAttribute<DefaultDllImportSearchPathsAttribute>();

            Assert.NotNull(attr);
            Assert.Equal(
                DllImportSearchPath.AssemblyDirectory | DllImportSearchPath.UseDllDirectoryForDependencies,
                attr.Paths
            );
        }
    }
}
