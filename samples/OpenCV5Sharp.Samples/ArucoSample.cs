// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class ArucoSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [11] ArUco Marker Detection & Generation ---");

            using (var dict = Cv2.ArucoGetPredefinedDictionary((int)ArucoPredefinedDictionaryType._6x650))
            {
                if (dict == null)
                {
                    Console.WriteLine("   [ERROR] Failed to load ArUco dictionary.");
                    return;
                }

                // 1. Generate an ArUco marker
                Console.WriteLine("\n1. Generating ArUco marker (ID: 24, Size: 200x200 pixels)...");
                const int CV_8UC1 = 0;
                using (var markerMat = new Mat(200, 200, CV_8UC1))
                {
                    // Generate marker with ID 24, 200x200 size, border bits 1
                    dict.GenerateImageMarker(24, 200, markerMat, 1);
                    Cv2.Imwrite("aruco_marker_24.png", markerMat, IntPtr.Zero);
                    Console.WriteLine("   Saved generated marker to: aruco_marker_24.png");

                    // 2. Simulate a scene containing the marker
                    Console.WriteLine("\n2. Simulating a test scene with the generated marker...");
                    const int CV_8UC3 = 64;
                    const int sceneSize = 400;
                    using (var scene = new Mat(sceneSize, sceneSize, CV_8UC3))
                    {
                        // Fill scene with light gray color
                        byte[] bg = new byte[sceneSize * sceneSize * 3];
                        for (int k = 0; k < bg.Length; k++) bg[k] = 220;
                        Marshal.Copy(bg, 0, scene.Data, bg.Length);

                        // Copy markerMat (which is grayscale CV_8UC1) into the center of scene (which is CV_8UC3)
                        byte[] markerData = new byte[200 * 200];
                        Marshal.Copy(markerMat.Data, markerData, 0, markerData.Length);

                        byte[] sceneData = new byte[sceneSize * sceneSize * 3];
                        Marshal.Copy(scene.Data, sceneData, 0, sceneData.Length);

                        int offsetRow = 100;
                        int offsetCol = 100;
                        for (int r = 0; r < 200; r++)
                        {
                            for (int c = 0; c < 200; c++)
                            {
                                byte grayVal = markerData[r * 200 + c];
                                int sceneIndex = ((r + offsetRow) * sceneSize + (c + offsetCol)) * 3;
                                sceneData[sceneIndex] = grayVal;     // B
                                sceneData[sceneIndex + 1] = grayVal; // G
                                sceneData[sceneIndex + 2] = grayVal; // R
                            }
                        }
                        Marshal.Copy(sceneData, 0, scene.Data, sceneData.Length);
                        Cv2.Imwrite("aruco_test_scene.png", scene, IntPtr.Zero);
                        Console.WriteLine("   Saved simulated scene to: aruco_test_scene.png");

                        // 3. Detect the ArUco marker in the simulated scene
                        Console.WriteLine("\n3. Detecting markers in the simulated scene...");
                        using (var detector = new ArucoArucoDetector(dict, null, null))
                        {
                            IntPtr cornersVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);
                            IntPtr rejectedVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);
                            using (var ids = new Mat())
                            {
                                detector.DetectMarkers(scene, cornersVec, ids, rejectedVec);

                                int detectedCount = NativeMethods.cv_VectorMat_Size(cornersVec);
                                Console.WriteLine($"   Markers detected: {detectedCount}");

                                if (detectedCount > 0)
                                {
                                    int[] markerIds = new int[detectedCount];
                                    Marshal.Copy(ids.Data, markerIds, 0, detectedCount);

                                    for (int i = 0; i < detectedCount; i++)
                                    {
                                        Console.WriteLine($"   - Marker Index {i}: ID = {markerIds[i]}");

                                        IntPtr matPtr = NativeMethods.cv_VectorMat_GetElement(cornersVec, i);
                                        using (var cornersMat = new Mat(matPtr))
                                        {
                                            float[] points = new float[8];
                                            Marshal.Copy(cornersMat.Data, points, 0, 8);
                                            Console.WriteLine($"     Corner 0 (Top-Left):  ({points[0]:F1}, {points[1]:F1})");
                                            Console.WriteLine($"     Corner 1 (Top-Right): ({points[2]:F1}, {points[3]:F1})");
                                            Console.WriteLine($"     Corner 2 (Bottom-Right): ({points[4]:F1}, {points[5]:F1})");
                                            Console.WriteLine($"     Corner 3 (Bottom-Left):  ({points[6]:F1}, {points[7]:F1})");
                                        }
                                    }

                                    // Draw detected marker visual indicators on the scene
                                    using (var markedScene = scene.Clone())
                                    {
                                        if (markedScene != null)
                                        {
                                            Cv2.ArucoDrawDetectedMarkers(markedScene, cornersVec, ids, new Scalar(0, 255, 0));
                                            Cv2.Imwrite("aruco_detected_output.png", markedScene, IntPtr.Zero);
                                            Console.WriteLine("   Saved visual detection results to: aruco_detected_output.png");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("   [WARNING] No markers were detected in the scene.");
                                }
                            }
                            NativeMethods.cv_VectorMat_Delete(cornersVec);
                            NativeMethods.cv_VectorMat_Delete(rejectedVec);
                        }
                    }
                }
            }

            // Live Webcam Detection attempt
            using (var capture = new VideoCapture(0, 0))
            {
                if (capture.IsOpened())
                {
                    Console.WriteLine("\nWebcam detected. Running live ArUco detection for 30 frames...");
                    using (var dict = Cv2.ArucoGetPredefinedDictionary((int)ArucoPredefinedDictionaryType._6x650))
                    using (var detector = new ArucoArucoDetector(dict, null, null))
                    using (var frame = new Mat())
                    using (var ids = new Mat())
                    {
                        for (int f = 1; f <= 30; f++)
                        {
                            if (capture.Read(frame) && frame.Handle != IntPtr.Zero)
                            {
                                IntPtr cornersVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);
                                IntPtr rejectedVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);

                                detector.DetectMarkers(frame, cornersVec, ids, rejectedVec);
                                int detected = NativeMethods.cv_VectorMat_Size(cornersVec);
                                if (detected > 0)
                                {
                                    int[] markerIds = new int[detected];
                                    Marshal.Copy(ids.Data, markerIds, 0, detected);
                                    Console.WriteLine($"   [Frame {f}/30] Detected {detected} marker(s). IDs: {string.Join(", ", markerIds)}");
                                }

                                NativeMethods.cv_VectorMat_Delete(cornersVec);
                                NativeMethods.cv_VectorMat_Delete(rejectedVec);
                            }
                            System.Threading.Thread.Sleep(80);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n[INFO] Webcam not detected. Skipping live ArUco detection.");
                }
            }

            Console.WriteLine("\nArUco sample completed.");
        }
    }
}
