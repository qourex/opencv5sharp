// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Diagnostics;
using System.IO;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples.Console.Gpu
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("=== OpenCV5Sharp GPU Console Sample ===");

            // 1. Check for CUDA support
            int deviceCount = Cv2.CudaGetCudaEnabledDeviceCount();
            System.Console.WriteLine($"CUDA Devices Found: {deviceCount}");

            if (deviceCount == 0)
            {
                System.Console.WriteLine("No CUDA-enabled GPU devices found. This GPU sample requires a compatible NVIDIA GPU.");
                return;
            }

            Cv2.CudaSetDevice(0);
            System.Console.WriteLine("Active CUDA Device set to 0.");

            // 2. Generate noisy image on CPU
            int width = 1920;
            int height = 1080;
            System.Console.WriteLine($"Generating noisy synthetic image ({width}x{height})...");
            using var noisyImg = new Mat(height, width, 0); // CV_8UC1 = 0
            var rand = new Random(42);
            byte[] noiseBuf = new byte[width * height];
            rand.NextBytes(noiseBuf);
            for (int i = 0; i < noiseBuf.Length; i++) noiseBuf[i] = (byte)(noiseBuf[i] % 50);
            System.Runtime.InteropServices.Marshal.Copy(noiseBuf, 0, noisyImg.Data, noiseBuf.Length);

            // 3. CPU Denoising Benchmark
            System.Console.WriteLine("\n[1] Running CPU Non-Local Means Denoising...");
            using var cpuDst = new Mat();
            var swCpu = Stopwatch.StartNew();
            Cv2.FastNlMeansDenoising(noisyImg, cpuDst, 15.0f, 7, 21);
            swCpu.Stop();
            long cpuTime = swCpu.ElapsedMilliseconds;
            System.Console.WriteLine($"CPU Denoising completed in: {cpuTime} ms");

            // Fetch default allocator
            IntPtr allocator = CudaGpuMat.DefaultAllocator();

            // 4. GPU Synchronous Denoising Benchmark
            System.Console.WriteLine("\n[2] Running GPU Synchronous Denoising...");
            using var gpuSrc = new CudaGpuMat(height, width, 0, allocator);
            using var gpuDst = new CudaGpuMat(height, width, 0, allocator);
            using var resultSync = new Mat();

            // Warmup
            gpuSrc.Upload(noisyImg);
            Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
            gpuDst.Download(resultSync);

            // Real run
            var swGpuSync = Stopwatch.StartNew();
            gpuSrc.Upload(noisyImg);
            Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
            gpuDst.Download(resultSync);
            swGpuSync.Stop();
            long gpuSyncTime = swGpuSync.ElapsedMilliseconds;
            System.Console.WriteLine($"GPU Synchronous Denoising completed in: {gpuSyncTime} ms");

            // 5. GPU Asynchronous Denoising Benchmark (using CudaStream)
            System.Console.WriteLine("\n[3] Running GPU Asynchronous Denoising (with CudaStream)...");
            using var stream = new CudaStream();
            using var resultAsync = new Mat();

            var swGpuAsync = Stopwatch.StartNew();
            // Async upload
            gpuSrc.Upload(noisyImg, stream);
            // Async denoise
            Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, stream);
            // Async download
            gpuDst.Download(resultAsync, stream);
            // Synchronize host CPU thread with GPU stream
            stream.WaitForCompletion();
            swGpuAsync.Stop();
            long gpuAsyncTime = swGpuAsync.ElapsedMilliseconds;
            System.Console.WriteLine($"GPU Asynchronous Denoising completed in: {gpuAsyncTime} ms");

            // 6. Print Comparison Table
            System.Console.WriteLine("\n=== BENCHMARK COMPARISON ===");
            System.Console.WriteLine($"{"Execution Mode",-30} | {"Time (ms)",-10} | {"Speedup vs CPU",-15}");
            System.Console.WriteLine(new string('-', 62));
            System.Console.WriteLine($"{"CPU (FastNlMeans)",-30} | {cpuTime,-10} | {"1.00x",-15}");
            System.Console.WriteLine($"{"GPU Synchronous",-30} | {gpuSyncTime,-10} | {((double)cpuTime / gpuSyncTime):F2}x");
            System.Console.WriteLine($"{"GPU Asynchronous (Stream)",-30} | {gpuAsyncTime,-10} | {((double)cpuTime / gpuAsyncTime):F2}x");
            System.Console.WriteLine(new string('-', 62));

            System.Console.WriteLine("\nFinished GPU Console Sample successfully.");
        }
    }
}
