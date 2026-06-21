// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PythonParityTests
    {
        [SkippableFact]
        public void TestPythonParity()
        {
            string pythonCode = @"
import cv2
import numpy as np

img = np.zeros((100, 100, 3), dtype=np.uint8)
cv2.line(img, (0, 0), (100, 100), (255, 255, 255), 2)
cv2.imwrite('parity_python_input.png', img)

gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
blur = cv2.GaussianBlur(gray, (5, 5), 1.5, 1.5)
canny = cv2.Canny(blur, 50, 150)
cv2.imwrite('parity_python_output.png', canny)
";

            string pyFile = "verify_parity.py";
            try
            {
                File.WriteAllText(pyFile, pythonCode);
            }
            catch
            {
                Skip.If(true, "Cannot write temporary Python script file");
                return;
            }

            string[] pythonCommands = { "py", "python", "python3" };
            bool pythonRan = false;

            foreach (string cmd in pythonCommands)
            {
                try
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = cmd,
                        Arguments = "verify_parity.py",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (var process = System.Diagnostics.Process.Start(startInfo))
                    {
                        process?.WaitForExit();
                        if (process?.ExitCode == 0)
                        {
                            pythonRan = true;
                            break;
                        }
                    }
                }
                catch
                {
                    // This command not available, try next
                }
            }

            if (!pythonRan)
            {
                CleanupParityFiles();
                Skip.If(true, "Python is not available in PATH — skipping parity test");
                return;
            }

            if (!File.Exists("parity_python_input.png") || !File.Exists("parity_python_output.png"))
            {
                CleanupParityFiles();
                Skip.If(true, "Python cv2 did not produce expected output files — skipping parity test");
                return;
            }

            try
            {
                using (Mat loaded = Cv2.Imread("parity_python_input.png", 1))
                using (Mat gray = new Mat())
                using (Mat blurred = new Mat())
                using (Mat edges = new Mat())
                {
                    Cv2.CvtColor(loaded, gray, 6, 0, AlgorithmHint.Default);
                    Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, 4, AlgorithmHint.Default);
                    Cv2.Canny(blurred, edges, 50, 150, 3, false);
                    Cv2.Imwrite("parity_csharp_output.png", edges, IntPtr.Zero);
                }

                byte[] pyBytes = File.ReadAllBytes("parity_python_output.png");
                byte[] csBytes = File.ReadAllBytes("parity_csharp_output.png");

                Assert.Equal(pyBytes.Length, csBytes.Length);

                bool bytesMatch = true;
                for (int i = 0; i < pyBytes.Length; i++)
                {
                    if (pyBytes[i] != csBytes[i])
                    {
                        bytesMatch = false;
                        break;
                    }
                }
                Assert.True(bytesMatch);
            }
            finally
            {
                CleanupParityFiles();
            }
        }

        private void CleanupParityFiles()
        {
            string[] files = { "verify_parity.py", "parity_python_input.png", "parity_python_output.png", "parity_csharp_output.png" };
            foreach (var file in files)
            {
                try
                {
                    if (File.Exists(file)) File.Delete(file);
                }
                catch { }
            }
        }
    }
}

