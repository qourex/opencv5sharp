![OpenCV5Sharp Banner](https://raw.githubusercontent.com/qourex/opencv5sharp/main/social_card.png)

# OpenCV5Sharp.Gpu

**by [Qourex](https://qourex.com)** — Bringing high-performance GPU-accelerated computer vision to .NET

[![Build & Test](https://github.com/qourex/opencv5sharp/actions/workflows/build.yml/badge.svg)](https://github.com/qourex/opencv5sharp/actions/workflows/build.yml)
[![Windows NuGet](https://img.shields.io/nuget/v/OpenCV5Sharp.Gpu.Windows.svg?style=flat-square&logo=nuget&label=NuGet%20Windows)](https://www.nuget.org/packages/OpenCV5Sharp.Gpu.Windows)
[![Linux NuGet](https://img.shields.io/nuget/v/OpenCV5Sharp.Gpu.Linux.svg?style=flat-square&logo=nuget&label=NuGet%20Linux)](https://www.nuget.org/packages/OpenCV5Sharp.Gpu.Linux)
[![License: Apache-2.0](https://img.shields.io/badge/License-Apache--2.0-blue.svg?style=flat-square)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com)

---

**OpenCV5Sharp.Gpu** is the GPU-accelerated suite of the C# port of OpenCV 5. To comply with the 250 MB NuGet.org package size limit, the GPU version is separated into platform-specific packages bundling precompiled native binaries built with **CUDA** and **cuDNN** enabled:

- **`OpenCV5Sharp.Gpu.Windows`**: Bundles `opencv_world500.dll` and `opencv5sharp_native.dll` compiled for Windows x64.
- **`OpenCV5Sharp.Gpu.Linux`**: Bundles `libopencv_world.so` and `libopencv5sharp_native.so` compiled for Linux x64.

For CPU-only execution without CUDA prerequisites, please use the cross-platform CPU packages: [OpenCV5Sharp](https://www.nuget.org/packages/OpenCV5Sharp) for desktop (Windows, Linux, macOS) or [OpenCV5Sharp.Mobile](https://www.nuget.org/packages/OpenCV5Sharp.Mobile) for mobile devices (Android, iOS).

---

## ⚡ Key GPU Advantages

- **🎮 CUDA & cuDNN Acceleration** — Native GPU-bound inference and image processing for OpenCV pipelines.
- **📈 Accelerated DNN Modules** — Up to 4x speedup for ONNX neural networks via CUDA execution backends.
- **📉 Optimized GPU Memory Allocator** — Direct access to OpenCV's global GPU memory allocators.
- **🔄 Parallel Batch Execution** — Direct pipeline data uploads to GPU VRAM for multi-threaded processes.

---

## 📦 Installation

To install the GPU-enabled package matching your host operating system:

### Windows x64
```bash
dotnet add package OpenCV5Sharp.Gpu.Windows
```

### Linux x64
```bash
dotnet add package OpenCV5Sharp.Gpu.Linux
```

---

## 🚀 CUDA Prerequisites

To run these packages with GPU acceleration, you must have the following NVIDIA runtimes installed and configured on your host system:

### Windows
1. **NVIDIA CUDA Toolkit 12.8** — [CUDA Downloads](https://developer.nvidia.com/cuda-downloads)
2. **NVIDIA cuDNN 8.9.7** — [cuDNN Downloads Archive](https://developer.nvidia.com/cudnn-downloads-archive)

Ensure that the following DLLs from these installations are available in your system `PATH`:
- `cudart64_12.dll` (specifically CUDA 12.x runtime)
- `cublas64_12.dll`
- `cublasLt64_12.dll`
- `cudnn64_8.dll` (specifically cuDNN v8.9.x)

### Linux / WSL2
1. **NVIDIA CUDA Toolkit 12.8** — [WSL/Linux CUDA Downloads](https://developer.nvidia.com/cuda-downloads)
2. **NVIDIA cuDNN 8.9.7** — [cuDNN Downloads Archive](https://developer.nvidia.com/cudnn-downloads-archive)

Ensure that the following shared libraries from these installations are available in your `LD_LIBRARY_PATH` or system library paths (e.g. `/usr/local/cuda/lib64`):
- `libcudart.so.12`
- `libcublas.so.12`
- `libcublasLt.so.12`
- `libcudnn.so.8` (specifically cuDNN v8.9.x)

---

## 🐳 Docker Compilation & CI Hosting

For Linux and WSL2 environments, you can compile the CUDA native libraries natively without installing compilers on your host system by using a Docker container.

### Local Packaging via Precompiled Assets
To package the library directly from precompiled binary assets (downloaded automatically from GitHub Releases) without needing to configure a CUDA toolchain or compiler on the build host:
```bash
# Packs both Windows and Linux GPU packages using precompiled binaries
.\build.ps1 -GpuOnly -Precompiled
```

### From-Source Local Compilation
Run the following command from the root of the repository:
```bash
docker run --rm --gpus all -v "$(pwd)":/workspace -w /workspace nvcr.io/nvidia/cuda:12.8.0-cudnn-devel-ubuntu22.04 bash -c "
  apt-get update && \
  apt-get install -y ca-certificates gpg wget && \
  wget -O - https://apt.kitware.com/keys/kitware-archive-latest.asc 2>/dev/null | gpg --dearmor - | tee /usr/share/keyrings/kitware-archive-keyring.gpg >/dev/null && \
  echo 'deb [signed-by=/usr/share/keyrings/kitware-archive-keyring.gpg] https://apt.kitware.com/ubuntu/ jammy main' | tee /etc/apt/sources.list.d/kitware.list >/dev/null && \
  apt-get update && \
  apt-get install -y cmake build-essential ninja-build git && \
  ./build.sh --gpu-only
"
```
*Note: If your local Docker setup does not have the NVIDIA Container Toolkit configured, you can omit the `--gpus all` flag, as a physical GPU is not required during compilation.*

---

## 💻 Quick Start

Here is a simple benchmark using CUDA Fast Non-Local Means Denoising:

```csharp
using System;
using System.Diagnostics;
using OpenCV5Sharp;

class Program
{
    static void Main()
    {
        const int CV_8UC1 = 0; // 8-bit single channel
        int width = 1920;
        int height = 1080;

        // Initialize 1080p noisy image
        using (var src = new Mat(height, width, CV_8UC1))
        using (var dst = new Mat())
        {
            // Set random noise
            var rand = new Random(42);
            byte[] buffer = new byte[width * height];
            rand.NextBytes(buffer);
            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, src.Data, buffer.Length);

            // Upload Mat data to GPU Memory (VRAM)
            using (var gpuSrc = new CudaGpuMat(src))
            using (var gpuDst = new CudaGpuMat())
            {
                var sw = Stopwatch.StartNew();

                // Run CUDA-accelerated Non-Local Means Denoising
                Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 3.0f, 7, 21, null);

                sw.Stop();
                Console.WriteLine($"GPU Denoising Time: {sw.ElapsedMilliseconds} ms");

                // Download result back to CPU RAM
                gpuDst.Download(dst);
            }
        }
    }
}
```
