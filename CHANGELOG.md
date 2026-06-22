# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Added 15 advanced computer vision example modules to the suite, including: QR code, background subtraction (MOG2), DNN SqueezeNet, face/landmark detection (YuNet), hand tracking, corner detection, ArUco markers, stitching, inpainting, Lucas-Kanade optical flow, StereoBM depth, Kalman filter tracking, perspective warping, CamShift, and Hough transform.

### Changed
- Formatted the codebase to CRLF line endings to comply with project `.editorconfig` rules.

### Fixed
- Resolved a critical `StereoBM` access violation crash on x64 by replacing struct return-by-value for `getROI1` and `getROI2` with out pointer parameters.
- Fixed the CycloneDX SBOM generation command in `release.yml` by removing the deprecated `-j` flag.
- Improved XML documentation for generated APIs, structs, and enums.
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
- Multi-target framework support: `net8.0` (LTS) and `net9.0`.
- Complete NuGet package with bundled native binaries:
  `opencv5sharp_native.dll`, `opencv_world500.dll`, `opencv_videoio_ffmpeg500_64.dll`.
- 8-stage test suite including memory leak detection, exception boundary validation,
  concurrency tests, and byte-for-byte Python cv2 parity checks.
- Interactive samples application with Mat basics, image processing, video capture,
  and DNN inference examples.
- Apache 2.0 license with full THIRD-PARTY-NOTICES for all bundled dependencies.
- NOTICE file with trademark disclaimer per Apache 2.0 Section 4(d).

### Known Limitations
- Windows x64 only (Linux and macOS support planned for v1.1).
- Vector parameters use raw `IntPtr` (managed vector wrappers planned for v1.1).

[Unreleased]: https://github.com/qourex/opencv5sharp/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/qourex/opencv5sharp/releases/tag/v1.0.0
