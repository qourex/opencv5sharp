# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Split the CUDA/cuDNN GPU-accelerated C# wrapper into platform-specific packages: `OpenCV5Sharp.Gpu.Windows` and `OpenCV5Sharp.Gpu.Linux` to comply with NuGet.org's 250 MB package size limits.
- Added `-Precompiled` switch to `build.ps1` to automatically fetch, stage, and package precompiled native libraries directly from GitHub Release tags.
- Expanded the unit test suite to **602 unique test cases** (running **1,204 test runs** in total across .NET 8.0 and .NET 9.0) covering:
  - Core row-stride matrix alignment, continuous layout, and memory copy limits.
  - Image processing data-driven grids (577 parameterized combinations of thresholding, color conversion space paths, matrix math operators, morphology operations, resizing interpolations, and pixel access).
  - Advanced modules: DNN ONNX inferences, SIFT/ORB features, Stitcher workflows, inpainting restoration, and motion prediction (Kalman filters).
  - GPU-accelerated computing (`CudaGpuMat` allocations, device info querying, and `CudaStream` sync).
  - Stereo block matching (`StereoBM`) and calibration rig chessboards (`Cv2.DrawChessboardCorners`).
  - Native calling conventions and custom exception boundaries (`OpenCVException`, `ArgumentNullException`, `ObjectDisposedException`).
- Added 15 advanced computer vision example modules to the suite, including: QR code, background subtraction (MOG2), DNN SqueezeNet, face/landmark detection (YuNet), hand tracking, corner detection, ArUco markers, stitching, inpainting, Lucas-Kanade optical flow, StereoBM depth, Kalman filter tracking, perspective warping, CamShift, and Hough transform.

### Changed
- Consolidated all branding, copyrights, assembly attributes, licenses, notice attributions, and documentation under the **Qourex** organization.
- Redesigned the repository social preview card (`social_card.png`) to match the clean, glowing visual theme of Qourex `FasterWhisper.NET`.
- Formatted the codebase to CRLF line endings to comply with project `.editorconfig` rules.

### Fixed
- Fixed `release.yml` workflow to apply TFM version suffix cleanup to both `.nupkg` and `.snupkg` packages, preventing NuGet.org symbol validation mismatches.
- Resolved a critical `StereoBM` access violation crash on x64 by replacing struct return-by-value for `getROI1` and `getROI2` with out pointer parameters.
- Fixed the CycloneDX SBOM generation command in `release.yml` by removing the deprecated `-j` flag.
- Improved XML documentation for generated APIs, structs, and enums.
- Added .NET 10.0 target framework support.
- Expanded cross-platform support: Linux (x64), macOS (x64, ARM64), Android (ARM64), iOS (ARM64).
- Added 10 new framework-specific sample projects: Console, WinForms, Blazor, ASP.NET Core, and MAUI (both CPU and GPU variants).
- Fixed `MatValidation.CheckDimensions` return type (void instead of int).
- Added `OpenCVException.ToString()` override for better diagnostics.

## [1.0.0] - 2026-06-19

### Added
- Initial release targeting OpenCV 5.0.0 on Windows x64.
- Auto-generated C# wrappers for 2,600+ OpenCV API methods across all major modules:
  core, imgproc, imgcodecs, highgui, video, videoio, calib, calib3d, features,
  features2d, dnn, flann, objdetect, photo, stereo, stitching, ptcloud, geometry.
- Thread-safe `DisposableOpenCVObject` base class with `Interlocked.Exchange` disposal.
- `OpenCVException` custom exception type for native error propagation.
- Platform guard for Windows x64 validation at runtime.
- DLL import search path restriction to prevent DLL hijacking (CWE-426).
- Multi-target framework support: `net8.0` (LTS), `net9.0`, and `net10.0`.
- Complete NuGet package with bundled native binaries:
  `opencv5sharp_native.dll`, `opencv_world500.dll`, `opencv_videoio_ffmpeg500_64.dll`.
- 8-stage test suite including memory leak detection, exception boundary validation,
  concurrency tests, and byte-for-byte Python cv2 parity checks.
- Interactive samples application with Mat basics, image processing, video capture,
  and DNN inference examples.
- Apache 2.0 license with full THIRD-PARTY-NOTICES for all bundled dependencies.
- NOTICE file with trademark disclaimer per Apache 2.0 Section 4(d).

### Known Limitations
- Vector parameters use raw `IntPtr` (managed vector wrappers planned for v1.1).

[Unreleased]: https://github.com/qourex/opencv5sharp/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/qourex/opencv5sharp/releases/tag/v1.0.0
