// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class NullArgumentTests
    {
        [Fact]
        public void CvtColor_NullArguments_ThrowsArgumentNullException()
        {
            // Verify that passing null for either source or destination throws ArgumentNullException and doesn't leak memory
            using (var validMat = new Mat())
            {
                Assert.Throws<ArgumentNullException>(() => Cv2.CvtColor(null!, validMat, 6, 0, AlgorithmHint.Default));
                Assert.Throws<ArgumentNullException>(() => Cv2.CvtColor(validMat, null!, 6, 0, AlgorithmHint.Default));
            }
        }
    }
}
