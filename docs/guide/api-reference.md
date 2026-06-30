# API Reference

This page contains links to the detailed API references for each module, alongside core wrapper structs.

## 📦 API Reference by Module
Select a module below to view all its classes, enums, and methods:

> [!NOTE]
> **OpenCV 5 Module Reorganization:** If you're coming from OpenCV 4.x, note that the `calib3d` module has been split into three separate modules in OpenCV 5: **Calib** (camera calibration), **Stereo** (stereo correspondence), and **Geometry** (geometric algorithms). The `features2d` module has similarly been renamed to **Features**.

* [**Core Module**](/guide/reference/core) — Matrix class (`Mat`), CUDA execution contexts, arrays, and basic mathematical operations.
* [**Imgproc (Image Processing)**](/guide/reference/imgproc) — Image filters (blur, dilate, erode), color conversions, resize, histograms, and contour extraction.
* [**Dnn (Deep Learning)**](/guide/reference/dnn) — Deep Neural Network loading, layers execution, forward passes, classification, text detection, and tokenizers.
* [**Objdetect (Object Detection)**](/guide/reference/objdetect) — Face detectors (YuNet), barcode decoders, QR detectors, and cascades.
* [**Imgcodecs (Image Codecs)**](/guide/reference/imgcodecs) — File reading/writing (`Imread`/`Imwrite`), memory encoding/decoding, and animations.
* [**Features (Feature Detection)**](/guide/reference/features) — 2D feature detectors/descriptors (SIFT, ORB, FAST, ALIKED) and matchers.
* [**Video (Motion & Tracking)**](/guide/reference/video) — Optical flow, Kalman filters, and motion tracking.
* [**VideoIO (Video Input/Output)**](/guide/reference/videoio) — Video file reading (`VideoCapture`) and writing (`VideoWriter`).
* [**Ptcloud (Point Cloud)**](/guide/reference/ptcloud) — 3D point cloud structures and geometry.
* [**Stereo (Stereo Correspondence)**](/guide/reference/stereo) — Stereo matching, disparity algorithms, and calibration.
* [**Stitching (Image Stitching)**](/guide/reference/stitching) — Panoramas, image matching, and warpers.
* [**Calib (Camera Calibration)**](/guide/reference/calib) — Camera calibration matrix computations, distortions, and calibration grids.
* [**Photo (Computational Photography)**](/guide/reference/photo) — Inpainting, denoising, HDR, and high-level photography processing.
* [**Geometry (Geometric Algorithms)**](/guide/reference/geometry) — Bounding boxes, convex hulls, and geometric calculations.
* [**Flann (Fast Nearest Neighbors)**](/guide/reference/flann) — Fast approximate nearest neighbors index matching.
* [**Highgui (GUI / Display)**](/guide/reference/highgui) — Window creation, image showing (`Cv2.Imshow`), trackbars, and simple UI interaction.

---

## 📦 Core Classes & Structs

### 1. Mat
The primary CPU matrix class, matching C++ `cv::Mat`. Holds an unmanaged pointer to image data on the CPU heap.

| Member | Type | Description |
| :--- | :--- | :--- |
| **`Rows`** | `int` | The number of rows (height) of the matrix. |
| **`Cols`** | `int` | The number of columns (width) of the matrix. |
| **`Type()`** | `int` | Returns the data type and channel layout as an integer constant (e.g. 0 for CV_8UC1, 16 for CV_8UC3). |
| **`Empty()`** | `bool` | Returns `true` if the matrix holds no data (0 rows or 0 columns). |
| **`Dispose()`** | `void` | Releases the underlying C++ memory heap immediately. |

---

### 2. CudaGpuMat
The GPU matrix class, matching C++ `cv::cuda::GpuMat`. Holds an unmanaged pointer to device memory in GPU VRAM.

| Member | Type | Description |
| :--- | :--- | :--- |
| **`Rows`** | `int` | Height of the matrix in GPU VRAM. |
| **`Cols`** | `int` | Width of the matrix in GPU VRAM. |
| **`Upload(Mat cpuMat)`** | `void` | **Blocking Call**: Copies data from CPU memory (`Mat`) to GPU device memory. |
| **`Download(Mat cpuMat)`** | `void` | **Blocking Call**: Copies data from GPU device memory back to CPU memory. |
| **`Dispose()`** | `void` | Deallocates the device pointer in GPU VRAM immediately. |
| **`DefaultAllocator()`** | `IntPtr` | **Static Method**: Returns the pointer to OpenCV's global default GPU memory allocator (required when instantiating `CudaGpuMat` to prevent Access Violations). |

---

## 🎨 Enumerations & Structs

### 1. Mat Type Constants
OpenCV uses integer constants for matrix type identifiers. Common values:

| Constant | Value | Description |
| :--- | :---: | :--- |
| `CV_8UC1` | `0` | 8-bit unsigned integer, single channel (Grayscale) |
| `CV_8UC3` | `16` | 8-bit unsigned integer, three channels (BGR) |
| `CV_32FC1` | `5` | 32-bit float, single channel (Depth maps) |
| `CV_32FC3` | `21` | 32-bit float, three channels |

Pass these as raw `int` values to methods that accept a type parameter.

---

### 2. Size
A lightweight C# struct mapping dimensions:
```csharp
public struct Size
{
    public int Width;
    public int Height;
}
```

---

### 3. Scalar
A 4-element double vector, mapping color thresholds or background values:
```csharp
public struct Scalar
{
    public double V0;
    public double V1;
    public double V2;
    public double V3;
}
```

---

## 🛡️ Error Handling & Interop

### 1. OpenCVException
When a native OpenCV C++ call fails (e.g., passing invalid dimensions to a filter), the wrapper detects the failure code and calls `ErrorHelper.CheckError()`.
* The wrapper catches C++ exceptions at the interop boundary, parses them, and throws a managed **`OpenCVException`** in C#.
* This prevents memory leaks and ensures C++ crashes do not kill the C# process.

### 2. P/Invoke Memory Boundary
```mermaid
sequenceDiagram
    participant C# Application
    participant C# Wrapper (OpenCV5Sharp)
    participant C++ Interop (opencv5sharp_native)
    participant OpenCV Engine (opencv_world)

    C# Application->>C# Wrapper: new CudaGpuMat(h, w, type, allocator)
    C# Wrapper->>C++ Interop: cuda_GpuMat_New_0(h, w, type, allocator)
    Note over C++ Interop: Resolves allocator pointer
    C++ Interop->>OpenCV Engine: new cv::cuda::GpuMat(...)
    OpenCV Engine-->>C++ Interop: Returns instance pointer
    C++ Interop-->>C# Wrapper: Returns IntPtr (handle)
    C# Wrapper-->>C# Application: Returns CudaGpuMat instance
```
