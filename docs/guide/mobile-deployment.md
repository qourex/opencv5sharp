# Mobile Deployment (Android & iOS)

OpenCV5Sharp features native support for executing computer vision tasks directly on mobile devices (Android and iOS). Because everything executes locally on the device, it provides secure, low-latency execution that works completely offline.

---

## 📋 Architectural Overview

To run high-performance image processing algorithms on mobile devices, OpenCV5Sharp compiles against native CPU vector extensions:

| Platform | Architecture | Vector Backend | Native Library Target |
| :--- | :--- | :--- | :--- |
| **Android** | `arm64-v8a` (64-bit) | ARM NEON | `libopencv5sharp_native.so`, `libopencv_world.so` |
| **iOS** | `arm64` (64-bit) | ARM NEON / Apple Accelerate | `libopencv5sharp_native.dylib`, `libopencv_world.dylib` (Framework) |

> [!WARNING]
> Only 64-bit mobile devices and simulators are supported. Attempting to build or run on 32-bit simulators or legacy devices will result in a `DllNotFoundException` or runtime execution failures.


---

## 📦 NuGet Installation

To target Android and/or iOS in your .NET MAUI or mobile applications, install the dedicated mobile package:
```bash
dotnet add package OpenCV5Sharp.Mobile
```

---

## 🤖 Android Integration

To configure your Android application for OpenCV5Sharp:

### 1. Workload Installation
Ensure that the Android development workload is installed:
```bash
dotnet workload install android
```

### 2. Permissions Configuration
If you plan to capture live frames using a device camera, you must request permissions in your `AndroidManifest.xml` (located under `Platforms/Android/`):
```xml
<uses-permission android:name="android.permission.CAMERA" />
<!-- If you plan to save images to the device gallery -->
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
```

---

## 🍎 iOS Integration

To configure your iOS application for OpenCV5Sharp:

### 1. Workload Installation
Ensure that the iOS development workload is installed:
```bash
dotnet workload install ios
```

### 2. Code-Signing Requirements
Physical iOS devices require all binaries and native `.dylib` libraries to be code-signed. The `OpenCV5Sharp.Mobile` NuGet package includes MSBuild targets that automatically extract, sign, and embed the native interop dynamic libraries into your `.app` bundle.
- Ensure you have a valid Apple Developer Profile configured in Visual Studio or Rider.
- For physical device debugging, configure your `Entitlements.plist` and provisioning profiles as you would for a standard iOS app.

---

## 💾 Resource Management & The Mobile Caching Workaround

### The Problem
On Android and iOS, raw files (such as ONNX deep learning models, templates, or camera calibration matrices) embedded in the application package are zipped inside the `.apk` or `.ipa` bundle. They do **not** exist as separate, physical files on the disk.

Because the native C++ OpenCV engine requires direct filesystem path strings (`char*`) to load assets (such as `Cv2.Imread()` or `Dnn.ReadNet()`), it cannot read files directly from inside the compiled bundle.

### The Solution: Extracting to Cache
On application startup, you must extract your packaged assets out of the application package and write them to the local device cache directory (`FileSystem.CacheDirectory` or `FileSystem.AppDataDirectory`). Once extracted, you can pass the physical path of the cached file to the SDK.

Here is the exact helper method used in our .NET MAUI sample:

```csharp
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

public async Task<string> PrepareTempFileFromAssetAsync(string assetName)
{
    // Define the destination target in the local CacheDirectory
    string targetPath = Path.Combine(FileSystem.CacheDirectory, assetName);
    
    // Check if we need to extract it
    if (File.Exists(targetPath))
    {
        File.Delete(targetPath);
    }

    // Open the read stream from the packaged app bundle
    using var stream = await FileSystem.OpenAppPackageFileAsync(assetName);
    
    // Write it to a physical file on disk
    using var outStream = File.OpenWrite(targetPath);
    await stream.CopyToAsync(outStream);
    
    // Return the physical file path that native interop can read
    return targetPath;
}
```

---

## ⚡ Performance Optimization Tips

- **Free Mat Objects Immediately**: Mobile devices have limited memory. Always wrap your `Mat` manipulations in `using` blocks to dispose of native memory resources immediately after use.
- **Run Heavy Tasks Async**: Run video capture loops and DNN inference passes on a background thread using `Task.Run` to prevent freezing the mobile user interface.
- **Isolate Target Workloads**: The OpenCV5Sharp NuGet package automatically filters out unnecessary target binaries during compiler staging, keeping your final `.apk` and `.ipa` download sizes as small as possible.
