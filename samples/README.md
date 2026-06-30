# OpenCV5Sharp C# Examples Application Suite

Welcome to the **OpenCV5Sharp** Examples Application Suite! This project contains runnable C# samples showcasing core computer vision operations in OpenCV 5, designed to compile and run on Windows, Linux, and macOS using .NET 8.0, 9.0, or 10.0.

---

## 1. Prerequisites & Compilation

Before running the examples, ensure you have the following installed:
1. **.NET SDK (8.0, 9.0, or 10.0)** (via [dotnet.microsoft.com](https://dotnet.microsoft.com/download))
2. **MSVC Build Toolset** (if recompiling the C++ DLL manually)

### To Run the Samples:
1. Open PowerShell or Command Prompt.
2. Navigate to the root directory of the workspace.
3. Build the core wrapper:
   ```bash
   dotnet build src/OpenCV5Sharp/OpenCV5Sharp.csproj -c Release
   ```
4. Run the samples project:
   ```bash
   dotnet run --project samples/OpenCV5Sharp.Samples/OpenCV5Sharp.Samples.csproj --framework net8.0
   ```

When run, you will see an interactive CLI menu allowing you to select and execute any of the detailed sample modules:
```text
==================================================
       OpenCV5Sharp Example Applications Suite    
==================================================
1. Mat Basics & Math Operations
2. Image Processing Pipeline & Drawing
3. VideoIO & Camera Stream
4. Deep Neural Network (DNN) Inference
5. QR Code Encoding & Decoding
6. Live Video Background Segmentation (MOG2)
7. DNN Image Classification (SqueezeNet ONNX)
8. DNN Face & Landmark Detection (YuNet ONNX)
9. Classic Hand & Finger Tracker
10. Corner & Feature Detection
11. ArUco Marker Detection & Generation
12. Panoramic Image Stitching
13. Image Inpainting & Restoration
14. Sparse Optical Flow (Lucas-Kanade)
15. Stereo Depth Estimation (StereoBM)
16. Trajectory Prediction & Tracking (Kalman Filter)
17. Perspective Warp & Homography Correction
18. Object Tracking via CamShift
19. Hough Line & Circle Detection
20. CUDA GPU Denoising & Benchmark
21. Exit
==================================================
```

---

## 2. Samples Details and Expected Output

### 2.1 Mat Basics & Math Operations
Demonstrates fundamental matrix instantiation, copying, property querying, and standard arithmetic operations (addition, subtraction, element-wise multiplication).
**Expected Output:**
```text
--- [1] Mat Basics & Math Operations ---

1. Creating a 10x10 matrix initialized to 0...
Mat Dimension: 10x10, Channels: 1
Is Continuous: True

2. Initializing a 3x3 RGB matrix with a blue color scalar...
RGB Channels: 3, Type: 64

3. Performing matrix addition...
Result Matrix Size: 2x2

4. Creating a submatrix (Region of Interest)...
Parent Size: 100x100
ROI Size: 50x50
Is ROI a submatrix? True

Mat Basics sample completed successfully.
```

### 2.2 Image Processing Pipeline & Drawing
Illustrates color-space conversion (BGR to Grayscale), Gaussian Blurring, Canny edge detection, drawing shape overlays, and saving images back to disk.
**Expected Output:**
```text
--- [2] Image Processing Pipeline & Drawing ---

1. Creating a synthetic input image with shapes and text...
   Drawing line onto Mat...
   Saved synthetic image to: sample_input.png

2. Loading image from disk using Imread...
   Loaded image size: 300x300, Channels: 3

3. Converting to Grayscale using CvtColor...
   Grayscale Channels: 1

4. Applying Gaussian Blur filter...

5. Running Canny Edge Detection...
   Saved processed edge output to: sample_output.png

Temporary files cleaned up.
Image Processing sample completed successfully.
```

### 2.3 VideoIO & Camera Stream
Demonstrates opening a camera capture stream (`VideoCapture`), reading real-time frames in a frame-by-frame loop, and saving output videos.
**Expected Output:**
```text
--- [3] VideoIO & Camera Stream ---

Opening VideoCapture on device 0 (Webcam)...
   Camera Stream Info: 640x480 @ -1 FPS

Reading 10 frames from camera feed...
   Frame [1]: Size=640x480, Channels=3
   Frame [2]: Size=640x480, Channels=3
   Frame [3]: Size=640x480, Channels=3
   Frame [4]: Size=640x480, Channels=3
   Frame [5]: Size=640x480, Channels=3
   Frame [6]: Size=640x480, Channels=3
   Frame [7]: Size=640x480, Channels=3
   Frame [8]: Size=640x480, Channels=3
   Frame [9]: Size=640x480, Channels=3
   Frame [10]: Size=640x480, Channels=3

Camera stream released successfully.
VideoCapture sample completed.
```

### 2.4 Deep Neural Network (DNN) Inference
Exposes the loading and running of deep learning models in OpenCV 5.
To run this sample, you need a pre-trained ONNX model.
**Downloading Sample Assets:**
For testing, you can download a standard ResNet-50 or SqueezeNet model in ONNX format:
- SqueezeNet: [squeezenet1.1-7.onnx](https://github.com/onnx/models/raw/main/validated/vision/classification/squeezenet/model/squeezenet1.1-7.onnx) (save as `squeezenet.onnx` in the output/working directory).
**Expected Output:**
```text
--- [4] Deep Neural Network (DNN) Inference ---

[Instruction] To run DNN inference in a real application, you would load an ONNX model file.
   Example code pattern:

   // 1. Load ONNX model
   using (DnnNet net = Cv2.DnnReadNetFromONNX("resnet50.onnx", 0))
   // 2. Load input image
   using (Mat img = Cv2.Imread("cat.jpg", 1))
   // 3. Preprocess image into a 4D tensor blob (1/255 scale, 224x224 input size, Swap Red/Blue)
   using (Mat blob = Cv2.DnnBlobFromImage(img, 1.0 / 255.0, new Size(224, 224), new Scalar(123.675, 116.28, 103.53, 0), true, false, -1))
   {
       // 4. Set network input
       net.SetInput(blob, "", 1.0, new Scalar(0, 0, 0, 0));
       // 5. Run forward inference pass
       using (Mat prob = net.Forward(""))
       {
           // 6. Decode output scores...
       }
   }

Verifying DNN APIs link successfully in native DLL...
   DnnNet initialized successfully. Handle: 0x1C8005205C0
   DNN API linkage verification passed.

DNN Inference sample completed.
```

### 2.5 QR Code Encoding & Decoding
Illustrates generating a QR Code matrix from a string using `QRCodeEncoder` and detecting and decoding the generated QR Code using `QRCodeDetector`.
**Expected Output:**
```text
--- [5] QR Code Encoding & Decoding ---

1. Encoding text to QR Code: "https://github.com/qourex/opencv5sharp"
   QR Code matrix generated. Size: 41x41, Channels: 1
   Saved QR Code image to: sample_qrcode.png

2. Loading and decoding the QR Code from disk...
   Running QRCodeDetector.DetectAndDecode...
   Decoded Text: "https://github.com/qourex/opencv5sharp"
   [SUCCESS] Decoded text matches the original!
   Detected corners matrix size: 4x1

Temporary QR Code image file cleaned up.

QR Code sample completed.
```

### 2.6 Live Video Background Segmentation (MOG2)
Illustrates running real-time foreground/background segmentation using the MOG2 background subtractor.
**Expected Output (Fallback Mode):**
```text
--- [6] Live Video Background Segmentation (MOG2) ---
   MOG2 Background Subtractor created successfully.

[INFO] Webcam not detected. Running fallback segmentation on synthetic moving frames...
   Synthetic Frame [1/10]: Box X=20, Foreground Pixels=2500
   Synthetic Frame [2/10]: Box X=40, Foreground Pixels=2642
   ...
   Synthetic Frame [10/10]: Box X=200, Foreground Pixels=2550

Background segmentation sample completed.
```

### 2.7 DNN Image Classification (SqueezeNet ONNX)
Demonstrates downloading and loading a pre-trained SqueezeNet ONNX model, running a forward inference pass, and retrieving the top classification class index using unmanaged pointer/marshalling with `MinMaxLoc`.
**Expected Output:**
```text
--- [7] DNN Image Classification (SqueezeNet ONNX) ---
   Found SqueezeNet model at: squeezenet.onnx

1. Loading ONNX network...
   Network loaded successfully.

2. Preprocessing input image...
   Blob shape size: 224x224, Channels: 3

3. Running network forward inference pass...
   Output probability matrix size: 1000x1, Channels: 1

[RESULT] Top Class ID: 812
[RESULT] Confidence: 84.50%

DNN classification sample completed.
```

### 2.8 DNN Face & Landmark Detection (YuNet ONNX)
Showcases high-speed DNN face detection using the built-in `FaceDetectorYN` class. It detects faces and retrieves 5 landmark points (eyes, nose, mouth corners) with confidence values.
**Expected Output (Fallback Mode):**
```text
--- [8] DNN Face & Landmark Detection (YuNet ONNX) ---
   Found YuNet model at: face_detection_yunet.onnx

[INFO] Webcam not detected. Running fallback face detection on a synthetic face pattern...
   [Synthetic Image] Detected 1 face(s):
      Face [0]: Rect=[x:118.0, y:92.0, w:94.0, h:86.0], Confidence=92.3%
         Landmarks: RightEye=[120.0, 100.0], LeftEye=[200.0, 100.0], Nose=[160.0, 130.0]

Face detection sample completed.
```

### 2.9 Classic Hand & Finger Tracker
Illustrates skin color segmentation in YCrCb color space, calculating hand centroids using `Moments`, and counting fingers by tracking peaks in horizontal slices of the hand binary mask.
**Expected Output (Fallback Mode):**
```text
--- [9] Classic Hand & Finger Tracker ---

[INFO] Webcam not detected. Running fallback verification on synthetic hand drawings...
   [Synthetic drawing: 1 fingers] Hand Centroid: (160.0, 140.0), Bounding Box: [125, 70, 70x115], Fingers Counted: 1
   [Synthetic drawing: 3 fingers] Hand Centroid: (160.0, 132.0), Bounding Box: [125, 70, 70x115], Fingers Counted: 3
   [Synthetic drawing: 5 fingers] Hand Centroid: (160.0, 126.0), Bounding Box: [125, 70, 70x115], Fingers Counted: 5

Hand tracker sample completed.
```

### 2.10 Corner & Feature Detection
Demonstrates classic corner detection algorithms: Shi-Tomasi corners via `GoodFeaturesToTrack` and Harris corners via `CornerHarris`. Features are extracted, and their coordinates are mapped to draw visual indicators.
**Expected Output:**
```text
--- [10] Corner & Feature Detection ---

[INFO] Webcam not detected. Skipping live feed.

Running corner detection on synthetic geometric image...
   [Synthetic] Shi-Tomasi: Found 12 corners.
   [Synthetic] Harris: Visualized 45 potential corner pixels.

Corner detection sample completed.
```

### 2.11 ArUco Marker Detection & Generation
Exposes generating ArUco markers using `ArucoDictionary.GenerateImageMarker` and detecting them in simulated scenes or live webcam streams using `ArucoArucoDetector`.
**Expected Output:**
```text
--- [11] ArUco Marker Detection & Generation ---

1. Generating ArUco marker (ID: 24, Size: 200x200 pixels)...
   Saved generated marker to: aruco_marker_24.png

2. Simulating a test scene with the generated marker...
   Saved simulated scene to: aruco_test_scene.png

3. Detecting markers in the simulated scene...
   Markers detected: 1
   - Marker Index 0: ID = 24
     Corner 0 (Top-Left):  (100.0, 100.0)
     Corner 1 (Top-Right): (299.0, 100.0)
     Corner 2 (Bottom-Right): (299.0, 299.0)
     Corner 3 (Bottom-Left):  (100.0, 299.0)
   Saved visual detection results to: aruco_detected_output.png

[INFO] Webcam not detected. Skipping live ArUco detection.

ArUco sample completed.
```

### 2.12 Panoramic Image Stitching
Combines multiple overlapping images into a single wide-angle panorama using the OpenCV 5 `Stitcher` module, passing a `Mat[]` array wrapped into a native vector via the native vector allocation function.
**Expected Output:**
```text
--- [12] Panoramic Image Stitching ---
Found local Lena image at opencv_prebuilt/opencv/sources/doc/js_tutorials/js_assets/lena.jpg. Copied to output.

1. Slicing test image into overlapping left and right halves...
   Source Image: 512x512
   Left Sub-image ROI: 332x512
   Right Sub-image ROI: 333x512

2. Stitching sub-images using Stitcher...
   Running Stitcher.Stitch (this may take a few seconds)...
   Stitcher completed with Status: Ok
   Success! Panoramic image generated. Size: 512x512
   Saved stitched panorama to: stitched_panorama.png

Stitching sample completed.
```

### 2.13 Image Inpainting & Restoration
Demonstrates restoring corrupted areas (scratches/text overlays) in a photo using the `Cv2.Inpaint` API with Navier-Stokes (`INPAINT_NS`) and Telea's algorithms.
**Expected Output:**
```text
--- [13] Image Inpainting & Restoration ---

1. Simulating image corruption (adding artificial scratches and text)...
   Saved corrupted image to: inpaint_corrupted.png
   Saved corruption mask to: inpaint_mask.png

2. Restoring image using Cv2.Inpaint...
   Saved Navier-Stokes restored image to: inpaint_restored_ns.png
   Saved Telea restored image to: inpaint_restored_telea.png

Inpainting sample completed.
```

### 2.14 Sparse Optical Flow (Lucas-Kanade)
Showcases tracking feature points across frames using sparse Lucas-Kanade optical flow (`Cv2.CalcOpticalFlowPyrLK`). It runs on simulated frames with a displaced white dot and supports real-time camera tracking.
**Expected Output:**
```text
--- [14] Sparse Optical Flow (Lucas-Kanade) ---

[INFO] Webcam not detected. Skipping live feed.

Running verification on synthetic moving dot frames...
   Tracking Status: 1 (1 = Success)
   Start Position:  (80.0, 80.0)
   End Position:    (85.0, 83.0)
   Measured Offset: dx=5.0, dy=3.0 (Expected: dx=5.0, dy=3.0)

Optical Flow sample completed.
```

### 2.15 Stereo Depth Estimation (StereoBM)
Demonstrates binocular disparity calculation from a stereo left/right image pair using `StereoBM`. The sample creates a simulated scene where a checkerboard square is shifted horizontally to simulate depth.
**Expected Output:**
```text
--- [15] Stereo Depth Estimation (StereoBM) ---

1. Generating synthetic stereo left and right image pair...
   Saved stereo left frame to: stereo_left.png
   Saved stereo right frame to: stereo_right.png

2. Computing disparity map via StereoBM...
   Testing StereoBM ROI methods...
   Initial ROI1: (0, 0, 0, 0)
   Initial ROI2: (0, 0, 0, 0)
   Updated ROI1: (10, 20, 100, 200)
   Disparity map computed. Size: 320x240, Type: 3
   Disparity non-zero count: 76743/76800
   Non-zero disparity range: [-1.00, 10.38] px
   Raw disparity value at (155, 115): -16
   Decoded disparity in pixels: -1.00 px (Expected close to 10.0 px)
   Saved colorized disparity map to: stereo_disparity.png

Stereo Depth sample completed.
```

### 2.16 Trajectory Prediction & Tracking (Kalman Filter)
Illustrates setting up a linear `KalmanFilter` to smooth and track a noisy state (position and velocity). The filter updates and predicts position dynamically to smooth out sensor noise.
**Expected Output:**
```text
--- [16] Trajectory Prediction & Tracking (Kalman Filter) ---

1. Initializing Kalman Filter (State dimension: 2, Measurement dimension: 1)...

2. Simulating 20 steps of tracking (True Velocity = 2.5)...
   Step   | True Pos   | Measured Pos    | Filtered Pos    | Est Velocity   
   ----------------------------------------------------------------------
   1      | 2.5        | 2.16            | 1.96            | 1.96           
   2      | 5.0        | 5.06            | 3.86            | 2.18           
   3      | 7.5        | 7.22            | 5.86            | 2.29           
   ...
   20     | 50.0       | 49.32           | 49.77           | 2.50           

Kalman Filter sample completed.
```

### 2.17 Perspective Warp & Homography Correction
Demonstrates calculating and applying perspective projection matrices via `Cv2.GetPerspectiveTransform` and `Cv2.WarpPerspective`. A flat square image is projected into a skewed card layout and then de-warped back to its original layout.
**Expected Output:**
```text
--- [17] Perspective Warp & Homography Correction ---

1. Defining perspective warp target coordinate transformation...

2. Computing perspective warp matrix (M)...
   M = [0.812, -0.052, 60.000]
       [0.088, 0.771, 40.000]
       [0.000, 0.000, 1.000]
   Saved skewed/warped image to: perspective_warped.png

3. Computing inverse perspective matrix and restoring image...
   Saved de-warped/restored flat image to: perspective_restored.png

Perspective Warp sample completed.
```

### 2.18 Object Tracking via CamShift
Demonstrates colored object tracking using back-projection matrices and the CAMSHIFT algorithm (`Cv2.CamShift`). It tracks the center, size, and orientation of a diagonally moving object in a simulated sequence.
**Expected Output:**
```text
--- [18] Object Tracking via CamShift ---

1. Running CamShift tracking simulation (10 frames)...
   Initial Search Window: [x: 20, y: 20, w: 45, h: 45]
   [Frame 1/10] Object Center: (55.0, 40.0), Size: 40.0x40.0, Angle: 90.0°
   [Frame 2/10] Object Center: (70.0, 50.0), Size: 40.0x40.0, Angle: 90.0°
   ...
   [Frame 10/10] Object Center: (190.0, 130.0), Size: 40.0x40.0, Angle: 90.0°

CamShift sample completed.
```

### 2.19 Hough Line & Circle Detection
Demonstrates classic feature extraction algorithms: circle detection via `Cv2.HoughCircles` and line detection via `Cv2.HoughLines`. The sample creates a synthetic image with a circle and a line, runs the detectors, and verifies the parameters of the detected shapes.
**Expected Output:**
```text
--- [19] Hough Line & Circle Detection ---

1. Generating synthetic image with a circle and a line...
   Saved synthetic input image to: hough_input.png

2. Running Cv2.HoughCircles...
   Detected circles count: 1
   - Circle 0: Center = (150.0, 150.0), Radius = 45.0 (Expected: Center=(150.0, 150.0), Radius=45.0)

3. Running Cv2.HoughLines...
   Detected lines count: 3
   - Line 0: rho = 0.0, theta = 2.356 (135.0°)
   - Line 1: rho = -1.0, theta = 2.356 (135.0°)
   - Line 2: rho = 1.0, theta = 2.356 (135.0°)

Hough Transform sample completed.
```

---

## 3. Core Design Paradigms

### Memory Management and `IDisposable`
`Mat` and other OpenCV classes wrap large unmanaged C++ heap allocations (like image pixel buffers) that are invisible to the .NET Garbage Collector. If you do not explicitly free them, your C# application will leak memory.

Always wrap disposable OpenCV objects in `using` blocks to guarantee immediate memory cleanup:
```csharp
// The using statement compiles to a try-finally block calling Dispose()
using (Mat src = Cv2.Imread("image.png", 1))
using (Mat dst = new Mat())
{
    Cv2.GaussianBlur(src, dst, new Size(5, 5), 1.5, 1.5, (int)BorderTypes.Default, AlgorithmHint.Default);
    Cv2.Imwrite("blurred.png", dst, IntPtr.Zero);
} // Memory is safely released here!
```

### Region of Interest (ROI) / Views
In OpenCV, submatrices share the underlying pixel buffer of the parent matrix without duplicating memory. Modifications to the ROI will alter the parent:
```csharp
using (Mat parent = new Mat(100, 100, 64)) // 64 = CV_8UC3 (BGR)
{
    // Slice out a 50x50 crop from top-left.
    // Notice that both parent and roi must be disposed independently!
    using (Mat roi = new Mat(parent, new Range(0, 50), new Range(0, 50)))
    {
        // roi.Data points directly into parent.Data offset location
    }
}
```

---

## 4. UI Integration Guide (Displaying Mats in GUI)

Since `OpenCV5Sharp` works on raw unmanaged memory, you need to copy or map the pixel buffers to draw them inside GUI frameworks.

### WPF (Windows Presentation Foundation)
Use WPF's `WriteableBitmap` to display frames from a camera loop:
```csharp
using OpenCV5Sharp;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

public void UpdateWpfImage(WriteableBitmap wpfBitmap, Mat frame)
{
    if (frame == null || frame.Handle == IntPtr.Zero || frame.Data == IntPtr.Zero)
        return;

    // Ensure we run on the WPF UI Thread (Dispatcher)
    if (!wpfBitmap.Dispatcher.CheckAccess())
    {
        wpfBitmap.Dispatcher.Invoke(() => UpdateWpfImage(wpfBitmap, frame));
        return;
    }

    wpfBitmap.Lock();
    try
    {
        int width = frame.Cols;
        int height = frame.Rows;
        int channels = frame.Channels();

        int srcStride = width * channels; 
        int dstStride = wpfBitmap.BackBufferStride;

        unsafe
        {
            byte* srcPtr = (byte*)frame.Data;
            byte* dstPtr = (byte*)wpfBitmap.BackBuffer;

            // Stride-Aware Row-by-Row Copying
            int bytesToCopyPerRow = Math.Min(srcStride, dstStride);
            for (int y = 0; y < height; y++)
            {
                System.Buffer.MemoryCopy(
                    srcPtr + (y * srcStride),
                    dstPtr + (y * dstStride),
                    dstStride,
                    bytesToCopyPerRow
                );
            }
        }

        wpfBitmap.AddDirtyRect(new Int32Rect(0, 0, width, height));
    }
    finally
    {
        wpfBitmap.Unlock();
    }
}
```

### Avalonia UI (Cross-Platform WPF)
For cross-platform rendering (Windows, Linux, macOS), map raw pointer buffers to Avalonia's `WriteableBitmap`:
```csharp
using Avalonia.Media.Imaging;
using OpenCV5Sharp;
using System;

public void UpdateAvaloniaImage(WriteableBitmap avaloniaBitmap, Mat frame)
{
    if (frame == null || frame.Handle == IntPtr.Zero || frame.Data == IntPtr.Zero)
        return;

    using (var lockedBuffer = avaloniaBitmap.Lock())
    {
        int width = frame.Cols;
        int height = frame.Rows;
        int channels = frame.Channels();

        int srcStride = width * channels; 
        int dstStride = lockedBuffer.RowBytes;

        unsafe
        {
            byte* srcPtr = (byte*)frame.Data;
            byte* dstPtr = (byte*)lockedBuffer.Address;

            // Row-by-row copy to accommodate row padding
            int bytesToCopyPerRow = Math.Min(srcStride, dstStride);
            for (int y = 0; y < height; y++)
            {
                System.Buffer.MemoryCopy(
                    srcPtr + (y * srcStride),
                    dstPtr + (y * dstStride),
                    dstStride,
                    bytesToCopyPerRow
                );
            }
        }
    }
}
```

## Trademarks

"OpenCV" is a registered trademark of the OpenCV Foundation. This project is not affiliated with, endorsed by, or sponsored by the OpenCV Foundation or OpenCV.org. The use of the "OpenCV" name is purely for descriptive purposes to indicate compatibility with the OpenCV library.

---

## Framework-Specific Sample Projects

In addition to the interactive CLI demo above, OpenCV5Sharp provides ready-to-run sample projects for popular .NET application frameworks. Each is available in both CPU and GPU (CUDA) variants:

### Console Applications
- **`OpenCV5Sharp.Samples.Console.Cpu`** — Minimal console app demonstrating CPU-based image processing.
- **`OpenCV5Sharp.Samples.Console.Gpu`** — Console app demonstrating CUDA GPU-accelerated processing.

### WinForms Applications
- **`OpenCV5Sharp.Samples.WinForms.Cpu`** — Windows Forms desktop app with CPU image processing pipeline.
- **`OpenCV5Sharp.Samples.WinForms.Gpu`** — Windows Forms desktop app with GPU-accelerated pipeline.

### Blazor Web Applications
- **`OpenCV5Sharp.Samples.Blazor.Cpu`** — Blazor Server/WASM app with CPU-based vision processing.
- **`OpenCV5Sharp.Samples.Blazor.Gpu`** — Blazor app leveraging CUDA for server-side GPU processing.

### ASP.NET Core Web API
- **`OpenCV5Sharp.Samples.AspNetCore.Cpu`** — REST API for image processing using CPU.
- **`OpenCV5Sharp.Samples.AspNetCore.Gpu`** — REST API for image processing using CUDA GPU.

### .NET MAUI Mobile Applications
- **`OpenCV5Sharp.Samples.Maui.Cpu`** — Cross-platform mobile app (Android/iOS) with CPU vision.
- **`OpenCV5Sharp.Samples.Maui.Gpu`** — Cross-platform mobile app with GPU acceleration.

### Running a Framework Sample

```bash
# Example: Run the WinForms CPU sample
dotnet run --project samples/OpenCV5Sharp.Samples.WinForms.Cpu

# Example: Run the Blazor GPU sample
dotnet run --project samples/OpenCV5Sharp.Samples.Blazor.Gpu

# Example: Run the MAUI CPU sample (requires MAUI workload)
dotnet build samples/OpenCV5Sharp.Samples.Maui.Cpu -f net8.0-android
```
