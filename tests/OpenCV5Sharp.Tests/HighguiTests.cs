// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class HighguiTests
    {
        [Fact]
        public void Highgui_WindowWorkflow_Success()
        {
            // NamedWindow and DestroyWindow should run without crashing in headless mode
            string winName = "Test Window";
            Cv2.NamedWindow(winName, 0); // 0 = WINDOW_NORMAL
            
            // Poll for key without waiting
            int key = Cv2.PollKey();
            Assert.Equal(-1, key);

            // Destroy the created window
            Cv2.DestroyWindow(winName);

            // Destroy all windows
            Cv2.DestroyAllWindows();
        }
    }
}
