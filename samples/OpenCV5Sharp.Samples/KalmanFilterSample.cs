// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class KalmanFilterSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [16] Trajectory Prediction & Tracking (Kalman Filter) ---");

            const int CV_32F = 5;

            // 1. Initialize Kalman Filter
            // Dimensionality: State=2 (Position, Velocity), Measurement=1 (Position), Control=0
            Console.WriteLine("\n1. Initializing Kalman Filter (State dimension: 2, Measurement dimension: 1)...");
            using (var kf = new KalmanFilter(2, 1, 0, CV_32F))
            {
                if (kf.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to instantiate KalmanFilter.");
                    return;
                }

                // 2. Set State Transition Matrix (A)
                // State equation: pos_new = pos + vel, vel_new = vel
                // Matrix:
                // [ 1.0  1.0 ]
                // [ 0.0  1.0 ]
                using (var transition = kf.TransitionMatrix)
                {
                    if (transition == null) return;
                    float[] data = new float[] { 1.0f, 1.0f, 0.0f, 1.0f };
                    Marshal.Copy(data, 0, transition.Data, data.Length);
                }

                // 3. Set Measurement Matrix (H)
                // We only measure the position directly: measurement = 1.0 * pos + 0.0 * vel
                // Matrix:
                // [ 1.0  0.0 ]
                using (var measurementMatrix = kf.MeasurementMatrix)
                {
                    if (measurementMatrix == null) return;
                    float[] data = new float[] { 1.0f, 0.0f };
                    Marshal.Copy(data, 0, measurementMatrix.Data, data.Length);
                }

                // 4. Set Covariances
                // Process Noise Covariance (Q): Small uncertainties in motion
                using (var processNoise = kf.ProcessNoiseCov)
                {
                    if (processNoise == null) return;
                    float[] data = new float[] { 1e-5f, 0.0f, 0.0f, 1e-5f };
                    Marshal.Copy(data, 0, processNoise.Data, data.Length);
                }

                // Measurement Noise Covariance (R): Accuracy of the sensor
                using (var measurementNoise = kf.MeasurementNoiseCov)
                {
                    if (measurementNoise == null) return;
                    float[] data = new float[] { 1e-1f }; // High noise standard deviation
                    Marshal.Copy(data, 0, measurementNoise.Data, data.Length);
                }

                // Post error covariance (P): initial state uncertainty
                using (var errorCovPost = kf.ErrorCovPost)
                {
                    if (errorCovPost == null) return;
                    float[] data = new float[] { 1.0f, 0.0f, 0.0f, 1.0f };
                    Marshal.Copy(data, 0, errorCovPost.Data, data.Length);
                }

                // 5. Set Initial State
                // Starting position = 0, initial velocity = 2.5
                using (var statePost = kf.StatePost)
                {
                    if (statePost == null) return;
                    float[] data = new float[] { 0.0f, 2.5f };
                    Marshal.Copy(data, 0, statePost.Data, data.Length);
                }

                // 6. Run simulation loop
                Console.WriteLine("\n2. Simulating 20 steps of tracking (True Velocity = 2.5)...");
                Console.WriteLine("   {0,-6} | {1,-10} | {2,-15} | {3,-15} | {4,-15}", "Step", "True Pos", "Measured Pos", "Filtered Pos", "Est Velocity");
                Console.WriteLine("   " + new string('-', 70));

                var rand = new Random(42);
                float truePosition = 0.0f;
                const float trueVelocity = 2.5f;

                using (var measurement = new Mat(1, 1, CV_32F))
                {
                    for (int step = 1; step <= 20; step++)
                    {
                        // Update true position
                        truePosition += trueVelocity;

                        // Generate noisy measurement (add Gaussian-like noise)
                        float noise = (float)(rand.NextDouble() * 2.0 - 1.0) * 0.8f; // random offset
                        float measuredPosition = truePosition + noise;

                        float[] measVal = new float[] { measuredPosition };
                        Marshal.Copy(measVal, 0, measurement.Data, 1);

                        // A. Predict step
                        using (var prediction = kf.Predict(null))
                        {
                            // B. Correct step using the new measurement
                            using (var correction = kf.Correct(measurement))
                            {
                                if (correction == null) continue;

                                float[] correctedState = new float[2];
                                Marshal.Copy(correction.Data, correctedState, 0, 2);

                                float filteredPos = correctedState[0];
                                float filteredVel = correctedState[1];

                                Console.WriteLine("   {0,-6} | {1,-10:F1} | {2,-15:F2} | {3,-15:F2} | {4,-15:F2}",
                                    step, truePosition, measuredPosition, filteredPos, filteredVel);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nKalman Filter sample completed.");
        }
    }
}
