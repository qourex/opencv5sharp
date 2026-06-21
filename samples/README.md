# OpenCV5Sharp C# Examples Application Suite

Welcome to the **OpenCV5Sharp** Examples Application Suite! This project contains runnable C# samples showcasing core computer vision operations in OpenCV 5, designed to compile and run on Windows using .NET 8.0.

---

## 1. Prerequisites & Compilation

Before running the examples, ensure you have the following installed:
1. **.NET 8.0 SDK** (via [dotnet.microsoft.com](https://dotnet.microsoft.com/download))
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

When run, you will see an interactive CLI menu allowing you to select and execute any of the four detailed sample modules:
```text
==================================================
       OpenCV5Sharp Example Applications Suite    
==================================================
1. Mat Basics & Math Operations
2. Image Processing Pipeline & Drawing
3. VideoIO & Camera Stream
4. Deep Neural Network (DNN) Inference
5. Exit
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
