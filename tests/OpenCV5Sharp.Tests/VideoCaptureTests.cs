// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class VideoCaptureTests
    {
        [Fact]
        public void TestVideoCaptureInitialization()
        {
            using (VideoCapture cap = new VideoCapture())
            {
                Assert.NotNull(cap);
                Assert.False(cap.IsOpened());
            }
        }

        [Fact]
        public void TestVideoCaptureFromFile()
        {
            // Opening a non-existent file should not crash, it should just return false for IsOpened()
            using (VideoCapture cap = new VideoCapture("non_existent_video.mp4", 0))
            {
                Assert.NotNull(cap);
                Assert.False(cap.IsOpened());
            }
        }
    }
}
