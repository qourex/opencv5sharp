# Advanced Features & Demos

This guide explores advanced configurations in OpenCV5Sharp, including CUDA-accelerated denoising, ONNX deep learning integration, image inpainting, and ArUco markers.

---

## ⚡ CUDA GPU Benchmarks

Using the `OpenCV5Sharp.Gpu.Windows` or `OpenCV5Sharp.Gpu.Linux` package, you can offload heavy pixel computation to the GPU. Below is an example running Non-Local Means Denoising on a high-resolution synthetic image:

```csharp
using System;
using System.Diagnostics;
using OpenCV5Sharp;

public static void RunCudaBenchmark()
{
    int width = 1920;
    int height = 1080;

    // Generate random noisy source image on CPU
    using var noisyImg = new Mat(height, width, 0); // CV_8UC1 = 0
    using var randMat = new Mat(height, width, 0);
    using var lowMat = new Mat(height, width, 0);
    using var highMat = new Mat(height, width, 0);

    // Populate boundary matrices for Randu interop
    byte[] lowData = new byte[width * height];
    byte[] highData = new byte[width * height];
    Array.Fill(lowData, (byte)0);
    Array.Fill(highData, (byte)50);
    System.Runtime.InteropServices.Marshal.Copy(lowData, 0, lowMat.Data, lowData.Length);
    System.Runtime.InteropServices.Marshal.Copy(highData, 0, highMat.Data, highData.Length);

    Cv2.Randu(randMat, lowMat, highMat);
    Cv2.Add(noisyImg, randMat, noisyImg, null, -1); // 5 arguments required

    // Get the default CUDA allocator to prevent access violations
    IntPtr defaultAllocator = CudaGpuMat.DefaultAllocator();

    // 1. Warm-up GPU pipelines
    Console.WriteLine("Initializing GPU...");
    using (var gpuSrc = new CudaGpuMat(height, width, 0, defaultAllocator))
    using (var gpuDst = new CudaGpuMat(height, width, 0, defaultAllocator))
    {
        gpuSrc.Upload(noisyImg);
        Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
    }

    // 2. Run actual benchmark
    var swGpu = Stopwatch.StartNew();
    using (var gpuSrc = new CudaGpuMat(height, width, 0, defaultAllocator))
    using (var gpuDst = new CudaGpuMat(height, width, 0, defaultAllocator))
    {
        gpuSrc.Upload(noisyImg);
        Cv2.CudaFastNlMeansDenoising(gpuSrc, gpuDst, 15.0f, 21, 7, null);
        
        using (var result = new Mat())
        {
            gpuDst.Download(result);
            swGpu.Stop();
            Console.WriteLine($"GPU Execution completed in: {swGpu.ElapsedMilliseconds} ms");
        }
    }
}
```

---

## 🤖 Deep Learning (DNN) with ONNX

OpenCV 5 offers robust DNN features. Here is how to load a model like SqueezeNet from ONNX, set the input blob, and run classification:

```csharp
using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;

class DnnClassifier
{
    public static void RunClassification(string onnxModelPath, string imagePath)
    {
        // 1. Read network from ONNX (engine: 0 = default)
        using var net = Cv2.DnnReadNetFromONNX(onnxModelPath, 0);
        
        // 2. Prepare input image
        using var img = Cv2.Imread(imagePath, (int)ImreadModes.Color);
        if (img == null || img.Handle == IntPtr.Zero)
        {
            Console.WriteLine("Failed to load image.");
            return;
        }

        using var blob = Cv2.DnnBlobFromImage(img, 1.0 / 255.0, new Size(224, 224),
            new Scalar(0, 0, 0), true, false, 5); // ddepth: CV_32F = 5
        net.SetInput(blob, "", 1.0, new Scalar(0, 0, 0, 0));
        
        // 3. Forward pass
        using var prob = net.Forward(null);
        
        // 4. Find best class using raw pointer access
        IntPtr minValPtr = Marshal.AllocHGlobal(sizeof(double));
        IntPtr maxValPtr = Marshal.AllocHGlobal(sizeof(double));
        IntPtr minLocPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Point>());
        IntPtr maxLocPtr = Marshal.AllocHGlobal(Marshal.SizeOf<Point>());
        try
        {
            Cv2.MinMaxLoc(prob, minValPtr, maxValPtr, minLocPtr, maxLocPtr, null);
            double maxVal = Marshal.PtrToStructure<double>(maxValPtr);
            Point maxLoc = Marshal.PtrToStructure<Point>(maxLocPtr);
            Console.WriteLine($"Best Class ID: {maxLoc.X} with Confidence: {maxVal:P2}");
        }
        finally
        {
            Marshal.FreeHGlobal(minValPtr);
            Marshal.FreeHGlobal(maxValPtr);
            Marshal.FreeHGlobal(minLocPtr);
            Marshal.FreeHGlobal(maxLocPtr);
        }
    }
}
```

---

## 🎨 Image Inpainting

Inpainting recovers corrupted regions of an image using surrounding pixels. We support both Navier-Stokes and Telea's algorithms:

```csharp
using System;
using OpenCV5Sharp;

void RestoreImage(string corruptedPath, string maskPath)
{
    using var src = Cv2.Imread(corruptedPath, (int)ImreadModes.Color);
    using var mask = Cv2.Imread(maskPath, (int)ImreadModes.Grayscale);
    using var restored = new Mat();

    // Inpaint using Telea's method (flags: 1 = TELEA, 0 = NS)
    Cv2.Inpaint(src, mask, restored, 3.0, 1);
    Cv2.Imwrite("restored.png", restored, IntPtr.Zero);
}
```

---

## 🏁 ArUco Marker Detection

OpenCV 5 includes built-in marker detection for robotics and augmented reality applications:

```csharp
using System;
using OpenCV5Sharp;

void DetectMarkers(string imagePath)
{
    using var src = Cv2.Imread(imagePath, (int)ImreadModes.Color);
    if (src == null || src.Handle == IntPtr.Zero)
    {
        Console.WriteLine("Failed to load image.");
        return;
    }

    // Get a predefined ArUco dictionary (DICT_6X6_250 = 10)
    using var dictionary = Cv2.ArucoGetPredefinedDictionary(10);
    using var detectorParams = new ArucoDetectorParameters();
    
    // Instantiate detector
    using var detector = new ArucoArucoDetector(dictionary, detectorParams, null);

    // Set up vector pointers for interop (managed by native vector wrapper)
    IntPtr cornersVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);
    IntPtr rejectedVec = NativeMethods.cv_VectorMat_New(new IntPtr[0], 0);
    using var ids = new Mat();

    try
    {
        // Detect markers
        detector.DetectMarkers(src, cornersVec, ids, rejectedVec);

        int detectedCount = NativeMethods.cv_VectorMat_Size(cornersVec);
        if (detectedCount == 0)
        {
            Console.WriteLine("No markers found.");
        }
        else
        {
            Console.WriteLine($"Detected {detectedCount} markers!");
        }
    }
    finally
    {
        // Clean up unmanaged vector allocations
        NativeMethods.cv_VectorMat_Delete(cornersVec);
        NativeMethods.cv_VectorMat_Delete(rejectedVec);
    }
}
```
