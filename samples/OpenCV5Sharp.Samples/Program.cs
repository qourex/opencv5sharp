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
                Console.WriteLine("5. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Select an option (1-5): ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "5")
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
