# OpenCV5Sharp Library

**Published by [Qourex](https://qourex.com)**

Clean, automatic C# wrapper for **OpenCV 5.0.0** with complete API coverage and robust unmanaged memory management.

[![Nuget](https://img.shields.io/nuget/v/OpenCV5Sharp.svg)](https://www.nuget.org/packages/OpenCV5Sharp/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/OpenCV5Sharp.svg)](https://www.nuget.org/packages/OpenCV5Sharp/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0)
[![Platform](https://img.shields.io/badge/Platform-Cross--Platform-lightgrey.svg)]()

---

## Installation

```bash
dotnet add package OpenCV5Sharp
```

Or via the NuGet Package Manager Console:

```powershell
Install-Package OpenCV5Sharp
```

---

## Key Features

- **OpenCV 5.0.0 Alignment**: Matches the restructured OpenCV 5 API layout (Features module, new Stereo/Calib split, and DNN).
- **Auto-Generated Complete API**: Over 2,600 methods from `core`, `imgproc`, `videoio`, `highgui`, `features2d`, `calib3d`, `objdetect`, `photo`, and `dnn` modules.
- **Thread-Safe Unmanaged Memory**: Uses a specialized, atomic `Interlocked.Exchange`-based `DisposableOpenCVObject` base class to prevent double-free conditions and memory leaks.
- **Exceptions Boundary Protection**: Zero raw native crashes. Every single C++ entry point is wrapped in standard `try/catch` handlers that map exceptions back to managed `OpenCVException` objects.
- **Zero-Configuration Bundling**: Prebuilt `opencv_world500.dll`, `opencv_videoio_ffmpeg500_64.dll`, and `opencv5sharp_native.dll` are bundled and copied automatically to the build output.

---

## Supported Platforms

- **Operating System**: Windows (x64), Linux (x64), macOS (x64, ARM64), Android (ARM64), iOS (ARM64)
- **Frameworks**: .NET 8.0, .NET 9.0, .NET 10.0

---

## Quick Start Example

```csharp
using System;
using OpenCV5Sharp;

class Program
{
    static void Main()
    {
        // 1. Read input image (BGR color)
        using (Mat src = Cv2.Imread("input.jpg", (int)ImreadModes.Color))
        using (Mat gray = new Mat())
        using (Mat blurred = new Mat())
        using (Mat edges = new Mat())
        {
            if (src == null || src.Handle == IntPtr.Zero)
            {
                Console.WriteLine("Error: Could not load image.");
                return;
            }

            // 2. Convert to Grayscale
            Cv2.CvtColor(src, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);

            // 3. Apply a 5x5 Gaussian Blur
            Cv2.GaussianBlur(gray, blurred, new Size(5, 5), 1.5, 1.5, (int)BorderTypes.Default, AlgorithmHint.Default);

            // 4. Perform Canny Edge Detection
            Cv2.Canny(blurred, edges, 50, 150, 3, false);

            // 5. Write the processed image back to disk
            Cv2.Imwrite("output_edges.jpg", edges, IntPtr.Zero);
        } // All unmanaged memory is automatically freed here
    }
}
```

---

## Type Conversion Guide

| OpenCV C++ Type | OpenCV5Sharp Managed Type | Marshaling Details |
| :--- | :--- | :--- |
| `cv::Mat` | `OpenCV5Sharp.Mat` | Wrapped `IntPtr` to unmanaged C++ instance |
| `cv::Size_<int>` | `OpenCV5Sharp.Size` | Sequential layout struct |
| `cv::Point_<int>` | `OpenCV5Sharp.Point` | Sequential layout struct |
| `cv::Rect_<int>` | `OpenCV5Sharp.Rect` | Sequential layout struct |
| `cv::Scalar_<double>`| `OpenCV5Sharp.Scalar` | Sequential layout struct |
| `cv::Range` | `OpenCV5Sharp.Range` | Sequential layout struct |
| `cv::TermCriteria` | `OpenCV5Sharp.TermCriteria` | Sequential layout struct |
| `cv::String` / `std::string` | `System.String` | UTF-8 marshaled string |

---

## Error Handling

All C++ exceptions (including OpenCV's native `cv::Exception`) are caught on the native side and converted into thread-local error states. When a wrapper call fails:
1. The native function returns a failure status.
2. The managed side detects the failure and fetches the error details.
3. An `OpenCVException` is thrown containing the native stack trace and error code.

### Example:
```csharp
try
{
    using (Mat src = new Mat()) // Empty image
    using (Mat dst = new Mat())
        {
            Cv2.CvtColor(src, dst, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
        }
}
catch (OpenCVException ex)
{
    Console.WriteLine($"OpenCV Error: {ex.Message} (Code: {ex.ErrorCode})");
}
```

---

## Memory Ownership and Thread Safety

1. **Explicit Disposal**: Any object inheriting from `DisposableOpenCVObject` (e.g. `Mat`, `VideoCapture`, `CLAHE`) allocates unmanaged resources. You **must** dispose them when done, preferably using C# `using` blocks.
2. **Double-Free Prevention**: Calling `Dispose()` multiple times, or having `Dispose()` race with the Garbage Collector Finalizer, is thread-safe and handled atomically.
3. **Thread Safety**: P/Invoke calls are thread-safe and re-entrant. However, mutating a single `Mat` instance from multiple threads simultaneously is not synchronized by the library; you must manage instance-level synchronization in user code.

---

## Support & Contact

- **GitHub**: [github.com/qourex/opencv5sharp](https://github.com/qourex/opencv5sharp)
- **Website**: [qourex.com](https://qourex.com)
- **Email**: [info@qourex.com](mailto:info@qourex.com)

---

## License

- **Library Code**: Licensed under the [Apache License, Version 2.0](https://github.com/qourex/opencv5sharp/blob/main/LICENSE).
- **FFmpeg Integration**: Dynamically links to FFmpeg binaries which are licensed under the [GNU LGPL v2.1 or later](https://github.com/qourex/opencv5sharp/blob/main/LICENSE_FFMPEG.txt).
- **Trademark Notice**: "OpenCV" is a registered trademark of the OpenCV Foundation. This project is a separate community wrapper and is not endorsed by or affiliated with the OpenCV Foundation.

---

<p align="center">Built with ❤️ by <a href="https://qourex.com">Qourex</a></p>
