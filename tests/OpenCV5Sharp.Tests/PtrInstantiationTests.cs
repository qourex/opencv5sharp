// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class PtrInstantiationTests
    {
        [Fact]
        public void TestPtrInstantiation()
        {
            using (Clahe clahe = Cv2.CreateCLAHE(40.0, new Size(8, 8)))
            {
                Assert.NotNull(clahe);
                Assert.Equal(40.0, clahe.GetClipLimit());

                clahe.SetClipLimit(2.0);
                Assert.Equal(2.0, clahe.GetClipLimit());
            }

            using (Orb orb = Orb.Create(500, 1.2f, 8, 31, 0, 2, IntPtr.Zero, 31, 20))
            {
                Assert.NotNull(orb);
            }
        }
    }
}
