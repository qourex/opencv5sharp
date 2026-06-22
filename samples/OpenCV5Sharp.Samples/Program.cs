// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace OpenCV5Sharp.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                RunOption(args[0]);
                return;
            }

            if (Console.IsInputRedirected)
            {
                Console.WriteLine("Non-interactive mode detected. Running all samples sequentially...");
                RunOption("1");
                RunOption("2");
                RunOption("3");
                RunOption("4");
                RunOption("5");
                RunOption("6");
                RunOption("7");
                RunOption("8");
                RunOption("9");
                RunOption("10");
                RunOption("11");
                RunOption("12");
                RunOption("13");
                RunOption("14");
                RunOption("15");
                RunOption("16");
                RunOption("17");
                RunOption("18");
                RunOption("19");
                return;
            }

            while (true)
            {
                try { Console.Clear(); } catch (IOException) { }
                Console.WriteLine("==================================================");
                Console.WriteLine("       OpenCV5Sharp Example Applications Suite    ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Mat Basics & Math Operations");
                Console.WriteLine("2. Image Processing Pipeline & Drawing");
                Console.WriteLine("3. VideoIO & Camera Stream");
                Console.WriteLine("4. Deep Neural Network (DNN) Inference");
                Console.WriteLine("5. QR Code Encoding & Decoding");
                Console.WriteLine("6. Live Video Background Segmentation (MOG2)");
                Console.WriteLine("7. DNN Image Classification (SqueezeNet ONNX)");
                Console.WriteLine("8. DNN Face & Landmark Detection (YuNet ONNX)");
                Console.WriteLine("9. Classic Hand & Finger Tracker");
                Console.WriteLine("10. Corner & Feature Detection");
                Console.WriteLine("11. ArUco Marker Detection & Generation");
                Console.WriteLine("12. Panoramic Image Stitching");
                Console.WriteLine("13. Image Inpainting & Restoration");
                Console.WriteLine("14. Sparse Optical Flow (Lucas-Kanade)");
                Console.WriteLine("15. Stereo Depth Estimation (StereoBM)");
                Console.WriteLine("16. Trajectory Prediction & Tracking (Kalman Filter)");
                Console.WriteLine("17. Perspective Warp & Homography Correction");
                Console.WriteLine("18. Object Tracking via CamShift");
                Console.WriteLine("19. Hough Line & Circle Detection");
                Console.WriteLine("20. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Select an option (1-20): ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "20")
                {
                    Console.WriteLine("Exiting example suite...");
                    return;
                }

                if (choice != null)
                {
                    RunOption(choice);
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                try { Console.ReadKey(); } catch (InvalidOperationException) { }
            }
        }

        static void RunOption(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "1":
                        MatBasics.Run();
                        break;
                    case "2":
                        ImageProcessing.Run();
                        break;
                    case "3":
                        VideoCaptureSample.Run();
                        break;
                    case "4":
                        DnnInference.Run();
                        break;
                    case "5":
                        QrCodeSample.Run();
                        break;
                    case "6":
                        BackgroundSegmentationSample.Run();
                        break;
                    case "7":
                        DnnClassificationSample.Run();
                        break;
                    case "8":
                        FaceDetectionSample.Run();
                        break;
                    case "9":
                        HandTrackerSample.Run();
                        break;
                    case "10":
                        CornerDetectionSample.Run();
                        break;
                    case "11":
                        ArucoSample.Run();
                        break;
                    case "12":
                        StitchingSample.Run();
                        break;
                    case "13":
                        InpaintSample.Run();
                        break;
                    case "14":
                        OpticalFlowSample.Run();
                        break;
                    case "15":
                        StereoDepthSample.Run();
                        break;
                    case "16":
                        KalmanFilterSample.Run();
                        break;
                    case "17":
                        WarpPerspectiveSample.Run();
                        break;
                    case "18":
                        CamShiftSample.Run();
                        break;
                    case "19":
                        HoughTransformSample.Run();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] An exception occurred during execution of option {choice}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
