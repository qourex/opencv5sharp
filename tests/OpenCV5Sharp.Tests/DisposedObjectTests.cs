// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DisposedObjectTests
    {
        [Fact]
        public void VideoCapture_AfterDispose_ThrowsObjectDisposedException()
        {
            var cap = new VideoCapture();
            cap.Dispose();
            Assert.Throws<ObjectDisposedException>(() => cap.IsOpened());
            Assert.Throws<ObjectDisposedException>(() => cap.Release());
        }

        [Fact]
        public void StereoBM_AfterDispose_ThrowsObjectDisposedException()
        {
            var stereo = StereoBM.Create(16, 9);
            Assert.NotNull(stereo);
            stereo!.Dispose();
            Assert.Throws<ObjectDisposedException>(() => stereo.GetBlockSize());
        }
    }
}
