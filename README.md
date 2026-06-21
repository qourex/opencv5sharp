# OpenCV5Sharp

<p align="center">
  <img src="logo.png" alt="Qourex Logo" width="120" />
</p>

<p align="center">
  <strong>by <a href="https://qourex.com">Qourex</a></strong> — Bringing computer vision to .NET
</p>

<p align="center">
  <a href="https://github.com/qourex/opencv5sharp/actions/workflows/build.yml"><img src="https://github.com/qourex/opencv5sharp/actions/workflows/build.yml/badge.svg" alt="Build &amp; Test" /></a>
  <a href="https://www.nuget.org/packages/OpenCV5Sharp"><img src="https://img.shields.io/nuget/v/OpenCV5Sharp.svg" alt="NuGet" /></a>
  <a href="https://www.nuget.org/packages/OpenCV5Sharp"><img src="https://img.shields.io/nuget/dt/OpenCV5Sharp.svg" alt="NuGet Downloads" /></a>
  <a href="LICENSE"><img src="https://img.shields.io/badge/license-Apache%202.0-blue.svg" alt="License" /></a>
  <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-8.0%20%7C%209.0-512BD4" alt=".NET" /></a>
</p>

---

**OpenCV5Sharp** is an automatic, clean C# wrapper for **OpenCV 5.0.0** on Windows x64.
It parses OpenCV headers using the official OpenCV parser and generates flat C++ exports
along with managed C# P/Invoke wrappers covering **2,600+ API methods**.

## ✨ Features

- **Complete OpenCV 5 Coverage** — 2,600+ methods across core, imgproc, videoio, highgui, features2d, calib3d, objdetect, photo, dnn, and more.
- **Thread-Safe Memory Management** — Atomic `Interlocked.Exchange`-based disposal prevents double-free conditions in multi-threaded scenarios.
- **Exception Boundary Protection** — Every native C++ entry point is wrapped in `try/catch` handlers. Zero raw crashes; all errors surface as managed `OpenCVException`.
- **Zero-Configuration Bundling** — Prebuilt `opencv_world500.dll`, `opencv_videoio_ffmpeg500_64.dll`, and `opencv5sharp_native.dll` are bundled and auto-copied at build time.
- **Multi-Target Framework Support** — Targets both .NET 8.0 (LTS) and .NET 9.0.
- **Auto-Generated Bindings** — A Python generator ensures consistent, up-to-date wrappers directly from OpenCV headers.
- **DLL Hijack Prevention** — `DllImportSearchPath` restriction (CWE-426 mitigation).

## 📦 Installation

```bash
dotnet add package OpenCV5Sharp
```

Or via the NuGet Package Manager Console:

```powershell
Install-Package OpenCV5Sharp
```

## 🚀 Quick Start

```csharp
using OpenCV5Sharp;

// Create a Mat, process it, and save
using var src = Cv2.Imread("input.jpg", (int)ImreadModes.Color);
using var dst = new Mat();
Cv2.GaussianBlur(src, dst, new Size(5, 5), 1.5, 1.5, (int)BorderTypes.Default, AlgorithmHint.Default);
Cv2.Imwrite("output.jpg", dst, IntPtr.Zero);
```

## 🔑 Primary API Entry Points

| Class | Description |
|:---|:---|
| `Mat` | Core matrix type — image data container with automatic memory management |
| `Cv2` | Static entry point for 2,600+ OpenCV functions (imgproc, core, calib3d, etc.) |
| `VideoCapture` | Video and camera input capture |
| `CLAHE` | Contrast Limited Adaptive Histogram Equalization |
| `Net` | DNN module — load and run deep learning models (ONNX, TensorFlow, Caffe) |
| `QRCodeDetector` | QR code detection and decoding |
| `CascadeClassifier` | Haar/LBP cascade-based object detection |

## 💻 Supported Platforms

| OS | Architecture | Status |
|:---|:---|:---|
| Windows 10/11 | x64 | ✅ Supported |
| Linux | x64 | 🔜 Planned (v1.1) |
| macOS | ARM64 / x64 | 🔜 Planned (v1.1) |

## Prerequisites

- **.NET 8.0** or **.NET 9.0** SDK
- **Windows x64** (Linux/macOS support planned for v1.1)

For building from source:
- **Python 3.8+** (for the binding generator)
- **Visual Studio 2022+** with "Desktop development with C++" workload
- **CMake 3.12+** (optional, for CMake-based native builds)

## Project Structure

| Directory | Description |
|:---|:---|
| `src/OpenCV5Sharp/` | Managed C# library (NuGet package) with generated wrappers and bundled native DLLs |
| `src/OpenCV5Sharp.Generator/` | Python binding generator (`generator.py`) and shadow templates |
| `src/OpenCV5Sharp.Native/` | C-linkage C++ wrapper code and build scripts |
| `tests/OpenCV5Sharp.Tests/` | Test suite: core properties, pixel manipulation, exceptions, threading, Python parity |
| `samples/OpenCV5Sharp.Samples/` | Interactive examples: Mat basics, image processing, video capture, DNN inference |

## Building from Source

### 1. Regenerating Bindings (Optional)

If you modify the OpenCV headers or the generator script:

```bash
py src/OpenCV5Sharp.Generator/generator.py --opencv-dir ./opencv --workspace-dir .
```

### 2. Compiling the Native DLL

```powershell
# PowerShell (auto-detects Visual Studio)
powershell -ExecutionPolicy Bypass -File src/OpenCV5Sharp.Native/compile.ps1

# Or use CMake
cd src/OpenCV5Sharp.Native && mkdir build && cd build
cmake -G "NMake Makefiles" -DCMAKE_BUILD_TYPE=Release ..
nmake
```

### 3. Building the Solution

```bash
dotnet build OpenCV5Sharp.slnx -c Release
dotnet pack src/OpenCV5Sharp/OpenCV5Sharp.csproj -c Release
```

## Running Tests

```bash
dotnet test OpenCV5Sharp.slnx -c Release
```

The test suite includes 8 validation stages: memory leak detection, exception boundary
validation, concurrency tests, and byte-for-byte Python cv2 parity checks.

## Running Samples

```bash
dotnet run --project samples/OpenCV5Sharp.Samples/OpenCV5Sharp.Samples.csproj
```

For detailed coding usage and guides, see the [samples/README.md](samples/README.md).

## Known Limitations

- **Windows x64 only** — Linux and macOS support is planned for v1.1.
- **Vector parameters** use raw `IntPtr` — managed vector wrappers planned for v1.1.
- **Package size** is ~106 MB due to bundled native OpenCV binaries.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md) for development setup and guidelines.

## Documentation

- [ARCHITECTURE.md](ARCHITECTURE.md) — Internal architecture and design
- [CHANGELOG.md](CHANGELOG.md) — Version history
- [SECURITY.md](SECURITY.md) — Security policy and vulnerability reporting
- [CODE_OF_CONDUCT.md](CODE_OF_CONDUCT.md) — Community standards
- [THIRD-PARTY-NOTICES.txt](THIRD-PARTY-NOTICES.txt) — Third-party component licenses

## 📬 Support & Contact

- **Issues & Bugs**: [GitHub Issues](https://github.com/qourex/opencv5sharp/issues)
- **Website**: [qourex.com](https://qourex.com)
- **Email**: [info@qourex.com](mailto:info@qourex.com)

## License

Licensed under the [Apache License 2.0](LICENSE). FFmpeg components are licensed
under [LGPL 2.1](LICENSE_FFMPEG.txt).

## Trademarks

"OpenCV" is a registered trademark of the OpenCV Foundation. This project is not
affiliated with, endorsed by, or sponsored by the OpenCV Foundation or OpenCV.org.
The use of the "OpenCV" name is purely for descriptive purposes to indicate
compatibility with the OpenCV library.

---

<p align="center">
  Built with ❤️ by <a href="https://qourex.com">Qourex</a>
</p>
