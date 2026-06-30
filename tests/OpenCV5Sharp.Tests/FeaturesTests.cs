// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class FeaturesTests
    {
        [Fact]
        public void TestOrbCreationAndConfig()
        {
            // Allocate scoreType enum on heap: HARRIS_SCORE = 0
            IntPtr scoreTypePtr = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(scoreTypePtr, 0);

            try
            {
                using (var orb = Orb.Create(500, 1.2f, 8, 31, 0, 2, scoreTypePtr, 31, 20))
                {
                    Assert.NotNull(orb);
                    Assert.NotEqual(IntPtr.Zero, orb!.Handle);

                    Assert.Equal(500, orb.GetMaxFeatures());
                    orb.SetMaxFeatures(1000);
                    Assert.Equal(1000, orb.GetMaxFeatures());
                }
            }
            finally
            {
                Marshal.FreeHGlobal(scoreTypePtr);
            }
        }

        [Fact]
        public void TestSiftCreation()
        {
            using (var sift = Sift.Create(0, 3, 0.04, 10.0, 1.6, false))
            {
                Assert.NotNull(sift);
                Assert.NotEqual(IntPtr.Zero, sift!.Handle);
            }
        }

        [Fact]
        public void TestDescriptorMatchers()
        {
            using (var matcher1 = DescriptorMatcher.Create("BruteForce"))
            {
                Assert.NotNull(matcher1);
                Assert.NotEqual(IntPtr.Zero, matcher1!.Handle);
                Assert.True(matcher1.Empty());
            }

            using (var matcher2 = DescriptorMatcher.Create("FlannBased"))
            {
                Assert.NotNull(matcher2);
                Assert.NotEqual(IntPtr.Zero, matcher2!.Handle);
            }

            // NormTypes.Hamming is 4 in OpenCV
            using (var bfMatcher = BFMatcher.Create(4, true))
            {
                Assert.NotNull(bfMatcher);
                Assert.NotEqual(IntPtr.Zero, bfMatcher!.Handle);
            }
        }
    }
}
