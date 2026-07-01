# Getting Started with OpenCV5Sharp

**OpenCV5Sharp** is a clean, automatic C# wrapper for **OpenCV 5.x**, providing modern .NET 8+ applications with access to 2,600+ computer vision APIs. With built-in `IDisposable` native memory management, it delivers C++ speed with C# safety.

---

## 🛠️ System Prerequisites

OpenCV5Sharp wraps compiled C++ binaries. Depending on your target operating system, ensure the following requirements are met:

### Windows
- **CPU**: Requires [Visual C++ Redistributable](https://aka.ms/vs/17/release/vc_redist.x64.exe) to be installed.
- **GPU (CUDA)**: Requires the CUDA Toolkit 12.8 and cuDNN 8.9.7 libraries to be installed and available in the system environment `PATH`.

### Linux
- **CPU**: Standard Linux x64 runs with no additional dependencies.
- **GPU (CUDA)**: Requires NVIDIA CUDA 12.8 drivers and cuDNN 8.9.7 packages on the host.

### macOS
- Compiles natively for Apple Silicon (Arm64) and Intel (x64) architectures. No external runtimes are needed.

### Android / iOS
- Fully supports `arm64-v8a` (Android) and `arm64` (iOS). 32-bit hardware targets and desktop iOS/Android simulators are not supported.

---

## 📦 Installation

OpenCV5Sharp is distributed as CPU and GPU NuGet packages. Choose the package matching your performance target and target operating systems:

### 1. CPU Packages
To comply with the 250 MB NuGet.org limit, CPU native packages are separated by workload:

#### Desktop (Windows, Linux, macOS)
```bash
dotnet add package OpenCV5Sharp
```

#### Mobile (Android, iOS)
```bash
dotnet add package OpenCV5Sharp.Mobile
```

### 2. GPU-Accelerated Packages
To comply with the 250 MB NuGet.org limit, GPU packages are separated by platform:

#### Windows x64 (CUDA)
```bash
dotnet add package OpenCV5Sharp.Gpu.Windows
```

#### Linux x64 (CUDA)
```bash
dotnet add package OpenCV5Sharp.Gpu.Linux
```

---

## 🚀 Quick Start (Canny Edge Detection)

Here is a complete, copy-pasteable console application that loads an image, converts it to grayscale, runs Canny edge detection, and saves the output.

```csharp
using System;
using OpenCV5Sharp;

class Program
{
    static void Main()
    {
        string inputPath = "lena.jpg";
        string outputPath = "edges.png";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.WriteLine($"Please provide a valid image file at: {inputPath}");
            return;
        }

        // 1. Load the input image
        Console.WriteLine("Loading image...");
        using var src = Cv2.Imread(inputPath, (int)ImreadModes.Color);
        if (src == null || src.Handle == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load image.");
            return;
        }

        // 2. Create destination mats
        using var gray = new Mat();
        using var edges = new Mat();

        // 3. Convert to grayscale
        Console.WriteLine("Converting to grayscale...");
        Cv2.CvtColor(src, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);

        // 4. Run Canny Edge Detection
        Console.WriteLine("Running Canny Edge Detection...");
        Cv2.Canny(gray, edges, 50, 150, 3, false);

        // 5. Save the result
        Cv2.Imwrite(outputPath, edges, IntPtr.Zero);
        Console.WriteLine($"Saved output to: {outputPath}");
    }
}
```

---

## 🔒 Memory Management Guidelines

Because OpenCV5Sharp wraps native C++ pointers, you must follow the `.NET IDisposable` pattern to avoid unmanaged memory leaks:

* **Always use `using` statements** or call `.Dispose()` on `Mat`, `CudaGpuMat`, `VideoCapture`, and other classes holding native resources.
* Do not rely on the Garbage Collector to free GPU VRAM or large CPU heaps immediately. Manual resource disposal guarantees deterministic native memory cleanup.

---

## 🧪 Running the Test Suite

OpenCV5Sharp includes an extensive unit test project located in the `tests/OpenCV5Sharp.Tests/` directory. It target both `.NET 8.0` and `.NET 9.0` frameworks, running **602 unique test cases** (totaling **1,204 execution runs**).

The test suite systematically verifies:
* **Memory and Alignment**: Blittable structures (`Point`, `Size`, `Rect`, `Scalar`), marshaling layouts, and row-stride padding safety.
* **Core & Imgproc**: Image filtering, resizing, morphology, arithmetic matrix math, and color space transformations.
* **Advanced Modules**: ONNX deep learning (DNN models, YuNet face detector), ORB/SIFT descriptors, camera calibration drawing (`Cv2.DrawChessboardCorners`), and block matching (`StereoBM`).
* **GPU Computing**: CUDA device count verification, capability querying (`CudaDeviceInfo`), GPU memory copying (`CudaGpuMat`), and async operations (`CudaStream`).
* **Negative & Exception Boundaries**: Ensuring that null or disposed inputs raise C# exceptions (`ArgumentNullException`, `ObjectDisposedException`) and native errors propagate cleanly (`OpenCVException`).

To run the full test suite locally:
```bash
dotnet test
```

> [!NOTE]
> CUDA GPU tests are designed with dynamic skip conditions and will automatically skip on CPU-only machines or where CUDA runtimes are unconfigured. This keeps the test pipeline green on all development machines.
