# OpenCV5Sharp Samples

The repository includes a comprehensive samples suite showing CPU and GPU features.

---

## 🏃 How to Run the Samples

To execute the samples, navigate to the root directory and run:

```bash
dotnet run --project samples/OpenCV5Sharp.Samples --configuration Release
```

A console menu will prompt you to select an option from the list below.

---

## 📂 Available Examples

### 1. Basic Operations
* **`MatBasics` (Option 1)**: Basic matrix creation, pixel manipulation (get/set), sub-matrix slicing (ROI), and element multiplication.
* **`ImageProcessing` (Option 2)**: Core image transformations (bilateral filtering, gaussian blurring, thresholding).
* **`VideoCapture` (Option 3)**: Reading frames from a video file or live web camera feed.
* **`DnnInference` (Option 4)**: Basic deep learning network layout verification.

### 2. Detection & Recognition
* **`QrCode` (Option 5)**: Detecting and decoding QR Codes using the built-in QR Code detector.
* **`BackgroundSegmentation` (Option 6)**: Background subtraction and segmentation using Gaussian Mixture Models (MOG2).
* **`DnnClassification` (Option 7)**: Image classification using the SqueezeNet ONNX model.
* **`FaceDetection` (Option 8)**: Real-time face detection using the YuNet ONNX model.
* **`HandTracker` (Option 9)**: Hand skeleton tracking and hand gesture detection.
* **`CornerDetection` (Option 10)**: Harris corner detection and Shi-Tomasi feature tracking.
* **`Aruco` (Option 11)**: Generating, detecting, and drawing ArUco markers for robotics and positioning.

### 3. Tracking & Reconstruction
* **`Stitching` (Option 12)**: Stitching multiple overlapping images together into a wide panorama.
* **`Inpaint` (Option 13)**: Restoring scratches or text overlays on images using image inpainting.
* **`OpticalFlow` (Option 14)**: Lucas-Kanade and Farneback dense optical flow tracking for motion analysis.
* **`StereoDepth` (Option 15)**: Disparity map generation from stereo camera image pairs (StereoBM/StereoSGBM).
* **`KalmanFilter` (Option 16)**: Multi-dimensional trajectory prediction (e.g. tracking a bouncing ball) using Kalman Filters.
* **`WarpPerspective` (Option 17)**: Calculating perspective warp transforms and restoring skewed documents.
* **`CamShift` (Option 18)**: Object tracking based on color histograms and CamShift tracking window.
* **`HoughTransform` (Option 19)**: Standard and probabilistic Hough Transform algorithms for detecting lines and circles.

### 4. Deep Learning & GPU (CUDA)
* **`GpuSample` (Option 20)**: CUDA-accelerated image denoising (Non-Local Means Denoising) comparing CPU vs GPU execution times.

---

## Framework-Specific Samples

Beyond the interactive CLI suite, OpenCV5Sharp provides sample projects for Console, WinForms, Blazor, ASP.NET Core, and MAUI — each in CPU and GPU variants. See the [samples/README.md](https://github.com/qourex/opencv5sharp/tree/main/samples) for full details.
