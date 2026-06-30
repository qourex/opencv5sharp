// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class GeometryTests
    {
        [Fact]
        public void TestContourAreaAndBoundingRect()
        {
            // CV_32SC2 = 36
            const int CV_32SC2 = 36;
            using (Mat contour = new Mat(3, 1, CV_32SC2))
            {
                int[] points = new int[]
                {
                    10, 10,
                    50, 10,
                    10, 40
                };
                Marshal.Copy(points, 0, contour.Data, points.Length);

                double area = Cv2.ContourArea(contour, false);
                Assert.Equal(600.0, area);

                Rect rect = Cv2.BoundingRect(contour);
                Assert.Equal(10, rect.X);
                Assert.Equal(10, rect.Y);
                Assert.Equal(41, rect.Width);
                Assert.Equal(31, rect.Height);
            }
        }

        [Fact]
        public void TestConvexHull()
        {
            // CV_32SC2 = 36
            const int CV_32SC2 = 36;
            using (Mat points = new Mat(4, 1, CV_32SC2))
            using (Mat hull = new Mat())
            {
                int[] data = new int[]
                {
                    10, 10,
                    20, 20, // inside point
                    10, 40,
                    50, 10
                };
                Marshal.Copy(data, 0, points.Data, data.Length);

                Cv2.ConvexHull(points, hull, false, true);

                int rows = hull.Rows > hull.Cols ? hull.Rows : hull.Cols;
                Assert.Equal(3, rows);
            }
        }

        [Fact]
        public void TestSubdiv2D()
        {
            using (var subdiv = new Subdiv2D(new Rect(0, 0, 100, 100)))
            {
                int id1 = subdiv.Insert(new Point2F(20, 20));
                int id2 = subdiv.Insert(new Point2F(80, 20));
                int id3 = subdiv.Insert(new Point2F(50, 80));

                Assert.True(id1 > 0);
                Assert.True(id2 > 0);
                Assert.True(id3 > 0);

                IntPtr nearestPtPtr = Marshal.AllocHGlobal(8); // sizeof(Point2F) = 8 bytes
                try
                {
                    int vertexId = subdiv.FindNearest(new Point2F(21, 21), nearestPtPtr);
                    Assert.True(vertexId > 0);

                    Point2F nearest = Marshal.PtrToStructure<Point2F>(nearestPtPtr);
                    Assert.Equal(20f, nearest.X);
                    Assert.Equal(20f, nearest.Y);
                }
                finally
                {
                    Marshal.FreeHGlobal(nearestPtPtr);
                }
            }
        }

        [Fact]
        public void TestMinAreaRectAndFitEllipse()
        {
            // CV_32FC2 = 5 + (1 << 5) = 37
            const int CV_32FC2 = 37;
            using (var points = new Mat(5, 1, CV_32FC2))
            {
                float[] data = new float[]
                {
                    10f, 10f,
                    10f, 20f,
                    20f, 20f,
                    20f, 10f,
                    15f, 15f
                };
                Marshal.Copy(data, 0, points.Data, data.Length);

                RotatedRect? rect = Cv2.MinAreaRect(points);
                Assert.NotNull(rect);
                Assert.True(rect.Size.Width > 0);
                Assert.True(rect.Size.Height > 0);

                RotatedRect? ellipse = Cv2.FitEllipse(points);
                Assert.NotNull(ellipse);
                Assert.True(ellipse.Size.Width > 0);
            }
        }

        [Fact]
        public void TestPointPolygonTestAndMoments()
        {
            // CV_32SC2 = 36
            const int CV_32SC2 = 36;
            using (var contour = new Mat(4, 1, CV_32SC2))
            {
                int[] points = new int[]
                {
                    0, 0,
                    100, 0,
                    100, 100,
                    0, 100
                };
                Marshal.Copy(points, 0, contour.Data, points.Length);

                // Center is (50, 50), which is inside (returns > 0)
                double distInside = Cv2.PointPolygonTest(contour, new Point2F(50, 50), false);
                Assert.True(distInside > 0);

                // (150, 150) is outside (returns < 0)
                double distOutside = Cv2.PointPolygonTest(contour, new Point2F(150, 150), false);
                Assert.True(distOutside < 0);

                // Moments
                Moments? m = Cv2.Moments(contour, false);
                Assert.NotNull(m);
                Assert.True(m.M00 > 0); // Area
            }
        }
    }
}
