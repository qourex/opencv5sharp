// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class GpuSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [20] CUDA GPU Denoising & Benchmark ---");

            // 1. Check if CUDA is available
            Console.WriteLine("\nChecking for CUDA support...");
            int deviceCount = Cv2.CudaGetCudaEnabledDeviceCount();
            Console.WriteLine($"CUDA-enabled devices found: {deviceCount}");

            if (deviceCount == 0)
            {
                Console.WriteLine("[WARNING] OpenCV5Sharp was compiled without CUDA or no CUDA devices were found.");
                Console.WriteLine("Please ensure that you compiled with -DWITH_CUDA=ON and your NVIDIA drivers are installed.");
                return;
            }

            // 2. Set active device and print device details
            Cv2.CudaSetDevice(0);
            int currentDevice = Cv2.CudaGetDevice();
            Console.WriteLine($"Active CUDA Device: {currentDevice}");
            Console.WriteLine("\nDevice Capabilities:");
            Cv2.CudaPrintShortCudaDeviceInfo(currentDevice);

            // 3. Generate a synthetic high-resolution image with random Gaussian noise
            const int CV_8UC1 = 0;
            const int width = 1920;
            const int height = 1080;
            Console.WriteLine($"\nGenerating noisy {width}x{height} synthetic image...");

            using (var cleanImg = new Mat(height, width, CV_8UC1))
            using (var noisyImg = new Mat(height, width, CV_8UC1))
            {
                // Fill clean image with a pattern
                byte[] pattern = new byte[width * height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Generate a grid pattern
                        pattern[y * width + x] = (byte)(((x / 80) % 2 == 0 ^ (y / 80) % 2 == 0) ? 200 : 50);
                    }
                }
                Marshal.Copy(pattern, 0, cleanImg.Data, pattern.Length);

                // Add Gaussian noise (add random values)
                Random rand = new Random(42);
                byte[] noisyData = new byte[width * height];
                for (int i = 0; i < noisyData.Length; i++)
                {
                    int noise = (int)(rand.NextDouble() * 80 - 40); // Noise range [-40, 40]
                    int val = pattern[i] + noise;
                    noisyData[i] = (byte)(val < 0 ? 0 : (val > 255 ? 255 : val));
                }
                Marshal.Copy(noisyData, 0, noisyImg.Data, noisyData.Length);
                Cv2.Imwrite("gpu_input_noisy.png", noisyImg, IntPtr.Zero);
                Console.WriteLine("   Noisy input image saved to: gpu_input_noisy.png");

                // 4. CPU Benchmark
                Console.WriteLine("\nRunning CPU Non-Local Means Denoising benchmark...");
                var swCpu = Stopwatch.StartNew();
                using (var cpuResult = new Mat())
                {
                    // FastNlMeansDenoising(src, dst, h, templateWindowSize, searchWindowSize)
                    Cv2.FastNlMeansDenoising(noisyImg, cpuResult, 15.0f, 7, 21);
                    swCpu.Stop();
                    Console.WriteLine($"   CPU Denoising completed in: {swCpu.ElapsedMilliseconds} ms");
                    Cv2.Imwrite("gpu_output_cpu.png", cpuResult, IntPtr.Zero);
                }

                // 5. GPU Benchmark (Warm-up first to compile/initialize pipelines)
                Console.WriteLine("\nInitializing GPU pipelines (Warm-up run)...");
                IntPtr defaultAllocator = CudaGpuMat.DefaultAllocator();
                using (var gpuSrc = new CudaGpuMat(height, width, CV_8UC1, defaultAllocator))
                using (var gpuDst = new CudaGpuMat(height, width, CV_8UC1, defaultAllocator))
                {
                    gpuSrc.Upload(noisyImg);
                    Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
                    using (var warmMat = new Mat())
                    {
                        gpuDst.Download(warmMat);
                    }
                }

                // Run actual GPU Benchmark
                Console.WriteLine("Running GPU CUDA Non-Local Means Denoising benchmark...");
                var swGpuTotal = new Stopwatch();
                var swGpuUpload = new Stopwatch();
                var swGpuKernel = new Stopwatch();
                var swGpuDownload = new Stopwatch();

                swGpuTotal.Start();
                swGpuUpload.Start();
                using (var gpuSrc = new CudaGpuMat(height, width, CV_8UC1, defaultAllocator))
                using (var gpuDst = new CudaGpuMat(height, width, CV_8UC1, defaultAllocator))
                {
                    gpuSrc.Upload(noisyImg);
                    swGpuUpload.Stop();

                    swGpuKernel.Start();
                    Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
                    swGpuKernel.Stop();

                    swGpuDownload.Start();
                    using (var gpuResult = new Mat())
                    {
                        gpuDst.Download(gpuResult);
                        swGpuDownload.Stop();
                        swGpuTotal.Stop();

                        Console.WriteLine($"   GPU Upload Time: {swGpuUpload.ElapsedMilliseconds} ms");
                        Console.WriteLine($"   GPU Kernel Execution Time: {swGpuKernel.ElapsedMilliseconds} ms");
                        Console.WriteLine($"   GPU Download Time: {swGpuDownload.ElapsedMilliseconds} ms");
                        Console.WriteLine($"   GPU Total Time (including I/O): {swGpuTotal.ElapsedMilliseconds} ms");

                        Cv2.Imwrite("gpu_output_cuda.png", gpuResult, IntPtr.Zero);
                        Console.WriteLine("   CUDA denoised output saved to: gpu_output_cuda.png");
                    }
                }

                // 6. Print Comparison
                double speedupTotal = (double)swCpu.ElapsedMilliseconds / swGpuTotal.ElapsedMilliseconds;
                double speedupKernel = (double)swCpu.ElapsedMilliseconds / swGpuKernel.ElapsedMilliseconds;

                Console.WriteLine("\n================ BENCHMARK RESULTS ================");
                Console.WriteLine($"CPU Execution:  {swCpu.ElapsedMilliseconds} ms");
                Console.WriteLine($"GPU Execution:  {swGpuTotal.ElapsedMilliseconds} ms (Total including Upload/Download)");
                Console.WriteLine($"                {swGpuKernel.ElapsedMilliseconds} ms (Pure GPU Computation)");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"GPU Speedup (Total):  {speedupTotal:F2}x faster");
                Console.WriteLine($"GPU Speedup (Kernel): {speedupKernel:F2}x faster");
                Console.WriteLine("===================================================");
            }
        }
    }
}
