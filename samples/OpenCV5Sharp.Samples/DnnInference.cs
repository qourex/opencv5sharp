// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class DnnInference
    {
        public static void Run()
        {
            Console.WriteLine("--- [4] Deep Neural Network (DNN) Inference ---");

            // Detailed explanation of how ONNX classification works in C#
            Console.WriteLine("\n[Instruction] To run DNN inference in a real application, you would load an ONNX model file.");
            Console.WriteLine($"   Example code pattern:\n");
            Console.WriteLine($"   // 1. Load ONNX model");
            Console.WriteLine($"   using (DnnNet net = Cv2.DnnReadNetFromONNX(\"resnet50.onnx\", 0))");
            Console.WriteLine($"   // 2. Load input image");
            Console.WriteLine($"   using (Mat img = Cv2.Imread(\"cat.jpg\", 1))");
            Console.WriteLine($"   // 3. Preprocess image into a 4D tensor blob (1/255 scale, 224x224 input size, Swap Red/Blue)");
            Console.WriteLine($"   using (Mat blob = Cv2.DnnBlobFromImage(img, 1.0 / 255.0, new Size(224, 224), new Scalar(123.675, 116.28, 103.53, 0), true, false, -1))");
            Console.WriteLine($"   {{");
            Console.WriteLine($"       // 4. Set network input");
            Console.WriteLine($"       net.SetInput(blob, \"\", 1.0, new Scalar(0, 0, 0, 0));");
            Console.WriteLine($"       // 5. Run forward inference pass");
            Console.WriteLine($"       using (Mat prob = net.Forward(\"\"))");
            Console.WriteLine($"       {{");
            Console.WriteLine($"           // 6. Decode output scores...");
            Console.WriteLine($"       }}");
            Console.WriteLine($"   }}");

            // Verify DNN API linkage inside the compiled opencv5sharp_native.dll
            try
            {
                Console.WriteLine("\nVerifying DNN APIs link successfully in native DLL...");
                using (DnnNet dummyNet = new DnnNet())
                {
                    Console.WriteLine($"   DnnNet initialized successfully. Handle: 0x{dummyNet.Handle.ToString("X")}");
                }
                Console.WriteLine("   DNN API linkage verification passed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   Linkage verification failed: {ex.Message}");
            }

            Console.WriteLine("\nDNN Inference sample completed.");
        }
    }
}
