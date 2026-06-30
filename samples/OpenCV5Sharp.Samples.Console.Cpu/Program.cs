// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples.Console.Cpu
{
    class Program
    {
        private const string FsrcnnUrl = "https://raw.githubusercontent.com/opencv/opencv_extra/4.x/testdata/dnn/fsrcnn_x2.onnx";
        private const string FsrcnnFile = "fsrcnn_x2.onnx";

        static async Task Main(string[] args)
        {
            System.Console.WriteLine("=== OpenCV5Sharp CPU Console Sample ===");

            // Create a dummy image for processing if none exists
            string inputImage = "input.png";
            CreateDummyImage(inputImage);

            // 1. Run CPU Canny Edge Detection
            RunCannyDemo(inputImage);

            // 2. Run Zero-Copy Span<byte> Benchmark
            RunZeroCopyBenchmark();

            // 3. Run FSRCNN Super Resolution
            await RunSuperResolutionAsync(inputImage);

            System.Console.WriteLine("Finished CPU Console Sample successfully.");
        }

        private static void CreateDummyImage(string path)
        {
            if (File.Exists(path)) return;
            using var mat = new Mat(480, 640, 16);
            Cv2.Rectangle(mat, new Rect(0, 0, mat.Cols, mat.Rows), new Scalar(100, 150, 200), -1, 8, 0);
            // Draw some shapes
            Cv2.Circle(mat, new Point(320, 240), 100, new Scalar(255, 255, 255), 3, 8, 0);
            Cv2.Rectangle(mat, new Rect(100, 100, 200, 150), new Scalar(0, 255, 0), 2, 8, 0);
            Cv2.Imwrite(path, mat, IntPtr.Zero);
        }

        private static void RunCannyDemo(string imagePath)
        {
            System.Console.WriteLine("\n[1] Running Canny Edge Detection...");
            using var src = Cv2.Imread(imagePath, (int)ImreadModes.Grayscale);
            if (src == null || src.Empty()) return;
            using var dst = new Mat();

            var sw = Stopwatch.StartNew();
            Cv2.Canny(src, dst, 50, 150, 3, false);
            sw.Stop();

            string outputPath = "canny_output.png";
            Cv2.Imwrite(outputPath, dst, IntPtr.Zero);
            System.Console.WriteLine($"Canny Edge detection completed in {sw.ElapsedMilliseconds} ms. Saved to: {outputPath}");
        }

        private static void RunZeroCopyBenchmark()
        {
            System.Console.WriteLine("\n[2] Running Zero-Copy Span<byte> Benchmark...");
            int size = 2000 * 2000 * 3; // 12 MB buffer
            byte[] managedArray = new byte[size];

            // Initialize buffer
            for (int i = 0; i < size; i++) managedArray[i] = 100;

            // Pin buffer and wrap inside Mat without copying
            var sw = Stopwatch.StartNew();
            GCHandle handle = GCHandle.Alloc(managedArray, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                using (var mat = new Mat(2000, 2000, 16, pointer, 2000 * 3))
                {
                    // Run filter that runs in-place
                    Cv2.BitwiseNot(mat, mat, null);
                }
            }
            finally
            {
                handle.Free();
            }
            sw.Stop();

            // Verify managed array was modified
            bool verified = managedArray[0] == unchecked((byte)~100);
            System.Console.WriteLine($"Zero-Copy in-place inversion completed in {sw.Elapsed.TotalMilliseconds:F2} ms. Verified: {verified}");
        }

        private static async Task RunSuperResolutionAsync(string imagePath)
        {
            System.Console.WriteLine("\n[3] Running FSRCNN Super Resolution (Deep Learning Upscaling)...");

            try
            {
                await DownloadModelIfMissingAsync(FsrcnnFile, FsrcnnUrl);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Could not download FSRCNN model: {ex.Message}. Skipping Super Resolution demo.");
                return;
            }

            using var src = Cv2.Imread(imagePath, 1);
            if (src == null || src.Empty()) return;

            System.Console.WriteLine("Loading FSRCNN ONNX model into OpenCV DNN...");
            using var net = Cv2.DnnReadNetFromONNX(FsrcnnFile, (int)DnnEngineType.Classic)!;

            var sw = Stopwatch.StartNew();
            // Pre-process frame for FSRCNN x2
            using var blob = Cv2.DnnBlobFromImage(src, 1.0 / 255.0, new OpenCV5Sharp.Size(src.Cols, src.Rows), new Scalar(0, 0, 0), false, false, 5)!;
            net.SetInput(blob, null, 1.0, new Scalar(0, 0, 0));

            using var prob = net.Forward(null);
            sw.Stop();

            System.Console.WriteLine($"Super Resolution x2 inference completed in {sw.ElapsedMilliseconds} ms.");
            System.Console.WriteLine($"Original size: {src.Cols}x{src.Rows} -> Upscaled size: {src.Cols * 2}x{src.Rows * 2}");
        }

        private static async Task DownloadModelIfMissingAsync(string localPath, string url)
        {
            if (File.Exists(localPath)) return;
            System.Console.WriteLine($"Model '{localPath}' not found. Downloading from {url}...");
            using var client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(localPath, bytes);
            System.Console.WriteLine("Download complete.");
        }
    }
}
