![OpenCV5Sharp Banner](https://raw.githubusercontent.com/qourex/opencv5sharp/main/social_card.png)

# OpenCV5Sharp

**by [Qourex](https://qourex.com)** — High-Performance, Cross-Platform Computer Vision for .NET 8.0, 9.0, & 10.0.

[![Build & Test](https://github.com/qourex/opencv5sharp/actions/workflows/build.yml/badge.svg)](https://github.com/qourex/opencv5sharp/actions/workflows/build.yml)
[![NuGet](https://img.shields.io/nuget/v/OpenCV5Sharp.svg?style=flat-square&logo=nuget&label=NuGet)](https://www.nuget.org/packages/OpenCV5Sharp)
[![Downloads](https://img.shields.io/nuget/dt/OpenCV5Sharp.svg?style=flat-square&logo=nuget&label=Downloads)](https://www.nuget.org/packages/OpenCV5Sharp)
[![Documentation](https://img.shields.io/badge/docs-VitePress-brightgreen.svg?style=flat-square)](https://qourex.github.io/opencv5sharp/)
[![License: Apache-2.0](https://img.shields.io/badge/License-Apache--2.0-blue.svg?style=flat-square)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-8.0%20%7C%209.0%20%7C%2010.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com)

📖 **[Read the Documentation](https://qourex.github.io/opencv5sharp/)** | 🚀 **[GPU Acceleration Guide](README_GPU.md)** | 🏃 **[C# Runnable Samples](https://github.com/qourex/opencv5sharp/tree/main/samples)**

---

**OpenCV5Sharp** is a production-ready C# wrapper for **OpenCV 5.x**. It provides a clean, automatic .NET API mapping of OpenCV's core computer vision algorithms, enabling high-performance image processing, feature detection, object tracking, and deep learning pipelines in modern C# without unmanaged memory leaks.

## 🚀 Key Features

* **🔌 Native .NET API Surface** — Elegant, idiomatic C# wrappers covering 2,600+ OpenCV methods.
* **⚡ OpenCV 5 Backend** — Powered by precompiled OpenCV 5 native libraries utilizing modern CPU vector instructions (AVX/NEON).
* **🎮 GPU & CUDA Acceleration** — Direct cuDNN and CUDA support for high-speed pixel manipulation and DNN inference.
* **📱 First-Class Cross-Platform** — Built-in runtime identifiers (RIDs) supporting Windows, Linux, macOS, Android, and iOS.
* **🔒 Deterministic Memory Management** — Type-safe `SafeHandle` implementations and `IDisposable` wrappers that clean up native pointers.
* **🤖 Deep Learning (DNN)** — Direct ONNX model support for face detection (YuNet) and image classification.
* **📦 Small Mobile Footprint** — Automatic workload isolation strips unused platform binaries to reduce package sizes.

---

## 📦 NuGet Package Matrix

To comply with the NuGet.org 250 MB package size limit, OpenCV5Sharp is distributed via modular packages:

| Package | Platform | Focus |
| :--- | :--- | :--- |
| **`OpenCV5Sharp`** | Desktop (Windows, Linux, macOS) | CPU-only image processing |
| **`OpenCV5Sharp.Mobile`** | Mobile (Android, iOS) | CPU processing optimized for ARM64 |
| **`OpenCV5Sharp.Gpu.Windows`** | Windows x64 | GPU / CUDA 12.8 & cuDNN 8.9.7 acceleration |
| **`OpenCV5Sharp.Gpu.Linux`** | Linux x64 | GPU / CUDA 12.8 & cuDNN 8.9.7 acceleration |

---

## 💻 Quick Start: Canny Edge Detection

Here is a copy-pasteable example of loading an image, converting it to grayscale, running a Canny filter, and saving the output using C#-idiomatic patterns.

```csharp
using System;
using OpenCV5Sharp; // Provides classes and ToInt() enum extensions

class Program
{
    static void Main()
    {
        // 1. Load an image from disk
        using var src = Cv2.Imread("lena.jpg", ImreadModes.Color.ToInt());
        if (src == null || src.Empty())
        {
            Console.WriteLine("Could not load image.");
            return;
        }

        // 2. Prepare workspace matrices
        using var gray = new Mat();
        using var edges = new Mat();

        // 3. Convert to grayscale and run Canny Filter
        Cv2.CvtColor(src, gray, ColorConversionCodes.Bgr2gray.ToInt(), 0, AlgorithmHint.Default);
        Cv2.Canny(gray, edges, 50, 150, 3, false);

        // 4. Save the output
        Cv2.Imwrite("edges.png", edges, IntPtr.Zero);
        Console.WriteLine("Edge detection complete! Output saved to edges.png.");
    }
}
```

---

## 🔒 Memory Management Guidelines

Because OpenCV5Sharp wraps raw C++ pointers, you must follow the `.NET IDisposable` pattern to avoid native heap memory leaks:
* **Always wrap in `using` blocks**: Ensure `Mat`, `CudaGpuMat`, `VideoCapture`, and other classes holding native handles are disposed immediately.
* **Do not rely on GC**: The .NET Garbage Collector is unaware of native VRAM allocations or large CPU heaps. Dispose of resources manually or via scope-bound `using var` variables.

---

## 🎨 UI Integration: Displaying Mats in C# UI Frameworks

Displaying a raw unmanaged matrix pixel buffer inside .NET GUI frameworks is simple. Copy row-by-row using strided memory writes:

### WPF (Windows Presentation Foundation)
```csharp
public void UpdateWpfImage(WriteableBitmap wpfBitmap, Mat frame)
{
    if (frame == null || frame.IsDisposed || frame.Data == IntPtr.Zero)
        return;

    wpfBitmap.Lock();
    try
    {
        int srcStride = (int)frame.Step; 
        int dstStride = wpfBitmap.BackBufferStride;
        int bytesToCopyPerRow = frame.Cols * frame.Channels(); // Assuming 8-bit channels

        unsafe
        {
            byte* srcPtr = (byte*)frame.Data;
            byte* dstPtr = (byte*)wpfBitmap.BackBuffer;

            int bytesToCopy = Math.Min(bytesToCopyPerRow, dstStride);
            for (int y = 0; y < frame.Rows; y++)
            {
                Buffer.MemoryCopy(srcPtr + (y * srcStride), dstPtr + (y * dstStride), dstStride, bytesToCopy);
            }
        }
        wpfBitmap.AddDirtyRect(new Int32Rect(0, 0, frame.Cols, frame.Rows));
    }
    finally
    {
        wpfBitmap.Unlock();
    }
}
```

---

## 🛠️ Troubleshooting `DllNotFoundException`

If you receive a `DllNotFoundException` when invoking `Cv2` methods, check the following checklist:

1. **Missing Visual C++ Redistributable (Windows)**:
   * Native wrappers require the latest [Visual C++ Redistributable x64](https://aka.ms/vs/17/release/vc_redist.x64.exe) installed.
2. **CUDA / cuDNN DLL Paths (GPU Packages)**:
   * Ensure NVIDIA CUDA Toolkit 12.8 and cuDNN 8.9.7 are installed and their binary directories are in your system `PATH` (Windows) or `LD_LIBRARY_PATH` (Linux).
   * Ensure libraries like `cudart64_12.dll` and `cudnn64_8.dll` are loadable from command prompt/shell.
3. **Architecture Mismatch (RID)**:
   * Verify that your project architecture target matches the runtime identifier. OpenCV5Sharp supports only 64-bit platforms (`win-x64`, `linux-x64`, `osx-x64`, `osx-arm64`, `android-arm64`, `ios-arm64`). Check that your project does not build as `x86` or `Any CPU` with "Prefer 32-bit" enabled.

---

## 📄 License & Trademarks

* The managed wrapper code and build scripts are licensed under the **Apache License, Version 2.0**.
* Bundled native FFmpeg binaries are licensed under the **GNU LGPL v2.1 or later**.
* "OpenCV" is a registered trademark of the OpenCV Foundation. This project is independent and not affiliated with OpenCV.org.
