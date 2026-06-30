// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Windows.Forms;

namespace OpenCV5Sharp.Samples.WinForms.Cpu
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if NET10_0_OR_GREATER
            // Use native .NET 10.0 Dark Mode setting if available
            Application.SetColorMode(SystemColorMode.Dark);
#endif

            Application.Run(new MainForm());
        }
    }
}
