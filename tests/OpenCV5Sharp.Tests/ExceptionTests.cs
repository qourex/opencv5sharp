// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class ExceptionTests
    {
        [Fact]
        public void TestExceptionBoundary()
        {
            // Trigger a real C++ exception by passing an empty Mat to CvtColor
            using (Mat empty = new Mat())
            using (Mat dst = new Mat())
            {
                Assert.Throws<OpenCVException>(() => Cv2.CvtColor(empty, dst, 6, 0, AlgorithmHint.Default));
            }
        }

        [Fact]
        public void TestNullArgumentValidation()
        {
            using (Mat dst = new Mat())
            {
                // Passing null to a non-nullable Mat parameter should throw ArgumentNullException in C# wrapper
                Assert.Throws<ArgumentNullException>(() => Cv2.CvtColor(null!, dst, 6, 0, AlgorithmHint.Default));
            }
        }

        [Fact]
        public void TestDisposedObjectException()
        {
            Mat mat = new Mat(10, 10, 0); // CV_8UC1
            mat.Dispose();

            // Calling operations on a disposed Mat should throw ObjectDisposedException
            Assert.Throws<ObjectDisposedException>(() => mat.Rows);
            Assert.Throws<ObjectDisposedException>(() => mat.Cols);
            Assert.Throws<ObjectDisposedException>(() => mat.Data);
            Assert.Throws<ObjectDisposedException>(() => mat.IsContinuous());

            using (Mat dst = new Mat())
            {
                Assert.Throws<ObjectDisposedException>(() => Cv2.CvtColor(mat, dst, 6, 0, AlgorithmHint.Default));
            }
        }

        [Fact]
        public void TestSizeMismatchException()
        {
            using (Mat src1 = new Mat(10, 10, 0)) // CV_8UC1
            using (Mat src2 = new Mat(5, 5, 0))   // Size mismatch
            using (Mat dst = new Mat())
            {
                // Adding matrices of different sizes should throw OpenCVException (native assertion fails)
                Assert.Throws<OpenCVException>(() => Cv2.Add(src1, src2, dst, null, -1));
            }
        }
    }
}
