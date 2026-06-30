# VIDEOIO Module API Reference

Complete documentation for the **VIDEOIO** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Videoio Documentation](https://docs.opencv.org/5.x/main_modules/videoio.html).

---
<div v-pre>

## 📦 Classes and Structs

### `VideoCapture`
**Inherits from**: `DisposableOpenCVObject`

Class for video capturing from video files, image sequences or cameras.

**Detailed Remarks**:
The class provides the managed C# API for capturing video from cameras or for reading video files and image sequences.
Here is how the class can be used:

#### Constructors
* `new VideoCapture()`
  * *Summary*: Default constructor
* `new VideoCapture(string filename, int apiPreference)`
  * *Summary*: This is an overloaded member function, provided for convenience. Opens a video file or a capturing device or an IP video stream for video capturing with API Preference
  * *Parameter* `filename`: it can be: - name of video file (eg. `video.avi`) - or image sequence (eg. `img_02d.jpg`, which will read samples like `img_00.jpg, img_01.jpg, img_02.jpg, ...`) - or URL of video stream (eg. `protocol://host:port/script_name?script_params|auth`) - or GStreamer pipeline string in gst-launch tool format in case if GStreamer is used as backend Note that each video stream or IP camera feed has its own URL scheme. Please refer to the documentation of source stream to know the right URL.
  * *Parameter* `apiPreference`: preferred Capture API backends to use. Can be used to enforce a specific reader implementation if multiple are available: e.g. CAP_FFMPEG or CAP_IMAGES or CAP_DSHOW.
* `new VideoCapture(string filename, int apiPreference, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience. Opens a video file or a capturing device or an IP video stream for video capturing with API Preference and parameters
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `params`: The @params parameter.
* `new VideoCapture(int index, int apiPreference)`
  * *Summary*: This is an overloaded member function, provided for convenience. Opens a camera for video capturing
  * *Parameter* `index`: id of the video capturing device to open. To open default camera using default backend just pass 0. (to backward compatibility usage of camera_id + domain_offset (CAP_*) is valid when apiPreference is CAP_ANY)
  * *Parameter* `apiPreference`: preferred Capture API backends to use. Can be used to enforce a specific reader implementation if multiple are available: e.g. CAP_DSHOW or CAP_MSMF or CAP_V4L.
* `new VideoCapture(int index, int apiPreference, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience. Opens a camera for video capturing with API Preference and parameters
  * *Parameter* `index`: The index parameter.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `params`: The @params parameter.

#### Methods
* `bool Open(string filename, int apiPreference)`
  * *Summary*: Opens a video file or a capturing device or an IP video stream for video capturing. This is an overloaded member function, provided for convenience.
  * *Remarks*:

Parameters are same as the constructor VideoCapture(string filename, int apiPreference)

  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Returns*: `true` if the file has been successfully opened The method first calls VideoCapture.release to close the already opened file or camera.
* `bool Open(string filename, int apiPreference, IntPtr @params)`
  * *Summary*: Opens a video file or a capturing device or an IP video stream for video capturing with API Preference and parameters This is an overloaded member function, provided for convenience.
  * *Remarks*:

The `params` parameter allows to specify extra parameters encoded as pairs `(paramId_1, paramValue_1, paramId_2, paramValue_2, ...)`.
See VideoCaptureProperties

  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: `true` if the file has been successfully opened The method first calls VideoCapture.release to close the already opened file or camera.
* `bool Open(int index, int apiPreference)`
  * *Summary*: Opens a camera for video capturing This is an overloaded member function, provided for convenience.
  * *Remarks*:

Parameters are same as the constructor VideoCapture(int index, int apiPreference = CAP_ANY)

  * *Parameter* `index`: The index parameter.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Returns*: `true` if the camera has been successfully opened. The method first calls VideoCapture.release to close the already opened file or camera.
* `bool Open(int index, int apiPreference, IntPtr @params)`
  * *Summary*: Opens a camera for video capturing with API Preference and parameters This is an overloaded member function, provided for convenience.
  * *Remarks*:

The `params` parameter allows to specify extra parameters encoded as pairs `(paramId_1, paramValue_1, paramId_2, paramValue_2, ...)`.
See VideoCaptureProperties

  * *Parameter* `index`: The index parameter.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: `true` if the camera has been successfully opened. The method first calls VideoCapture.release to close the already opened file or camera.
* `bool IsOpened()`
  * *Summary*: Returns true if video capturing has been initialized already.
  * *Remarks*:

If the previous call to VideoCapture constructor or VideoCapture.open() succeeded, the method returns
true.

  * *Returns*: `true` if video capturing has been initialized, `false` otherwise.
* `void Release()`
  * *Summary*: Closes video file or capturing device.
  * *Remarks*:

The method is automatically called by subsequent VideoCapture.open and by VideoCapture
destructor.
The C function also deallocates memory and clears \*capture pointer.

* `bool Grab()`
  * *Summary*: Grabs the next frame from video file or capturing device.
  * *Returns*: `true` (non-zero) in the case of success. The method/function grabs the next frame from video file or camera and returns true (non-zero) in the case of success. The primary use of the function is in multi-camera environments, especially when the cameras do not have hardware synchronization. That is, you call VideoCapture.Grab() for each camera and after that call the slower method VideoCapture.Retrieve() to decode and get frame from each camera. This way the overhead on demosaicing or motion jpeg decompression etc. is eliminated and the retrieved frames from different cameras will be closer in time. Also, when a connected camera is multi-head (for example, a stereo camera or a Kinect device), the correct way of retrieving data from it is to call VideoCapture.Grab() first and then call VideoCapture.Retrieve() one or more times with different values of the channel parameter. `tutorial_kinect_openni`
* `bool Retrieve(Mat image, int flag)`
  * *Summary*: Decodes and returns the grabbed video frame.
  * *Remarks*:

**See also**: read()

  * *Parameter* `image`: Input image.
  * *Parameter* `flag`: it could be a frame index or a driver specific flag
  * *Returns*: `false` if no frames has been grabbed The method decodes and returns the just grabbed frame. If no frames has been grabbed (camera has been disconnected, or there are no more frames in video file), the method returns false and the function returns an empty image (with Mat, test it with Mat.Empty()).
* `bool Read(Mat image)`
  * *Summary*: Grabs, decodes and returns the next video frame.
  * *Parameter* `image`: Input image.
  * *Returns*: `false` if no frames has been grabbed The method/function combines VideoCapture.Grab() and VideoCapture.Retrieve() in one call. This is the most convenient method for reading video files or capturing data from decode and returns the just grabbed frame. If no frames has been grabbed (camera has been disconnected, or there are no more frames in video file), the method returns false and the function returns empty image (with Mat, test it with Mat.Empty()).
* `bool Set(int propId, double value)`
  * *Summary*: Sets a property in the VideoCapture.
  * *Remarks*:

.: info Note
Even if it returns `true` this doesn't ensure that the property
value has been accepted by the capture device. See note in VideoCapture.get()
.:

  * *Parameter* `propId`: Property identifier from VideoCaptureProperties (eg. CAP_PROP_POS_MSEC, CAP_PROP_POS_FRAMES, ...) or one from `videoio_flags_others`
  * *Parameter* `value`: Value of the property.
  * *Returns*: `true` if the property is supported by backend used by the VideoCapture instance.
* `double Get(int propId)`
  * *Summary*: Returns the specified VideoCapture property
  * *Remarks*:

.: info Note
Reading / writing properties involves many layers. Some unexpected result might happens
along this chain.

```text
VideoCapture -> API Backend -> Operating System -> Device Driver -> Device Hardware
```

The returned value might be different from what really used by the device or it could be encoded
using device dependent rules (eg. steps or percentage). Effective behaviour depends from device
driver and API Backend
.:

  * *Parameter* `propId`: Property identifier from VideoCaptureProperties (eg. CAP_PROP_POS_MSEC, CAP_PROP_POS_FRAMES, ...) or one from `videoio_flags_others`
  * *Returns*: Value for the specified property. Value CAP_PROP_UNKNOWN is returned when querying a property that is not supported by the backend used by the VideoCapture instance.
* `string? GetBackendName()`
  * *Summary*: Returns used backend API name
  * *Remarks*:

.: info Note
Stream should be opened.
.:

  * *Returns*: The backend API name as a string, or `null` if the stream is not opened.
* `void SetExceptionMode(bool enable)`
  * *Summary*: Switches exceptions mode
  * *Remarks*:

* methods raise exceptions if not successful instead of returning an error code

  * *Parameter* `enable`: If `true`, methods raise exceptions if not successful instead of returning an error code.
* `bool GetExceptionMode()`
  * *Summary*: Returns the current exceptions mode of the VideoCapture.
  * *Returns*: `true` if exceptions mode is enabled, `false` otherwise.
* `bool WaitAny(IntPtr streams, IntPtr readyIndex, long timeoutNs)`
  * *Summary*: Wait for ready frames from VideoCapture.
  * *Parameter* `streams`: input video streams
  * *Parameter* `readyIndex`: stream indexes with grabbed frames (ready to use .retrieve() to fetch actual frame)
  * *Parameter* `timeoutNs`: number of nanoseconds (0 - infinite)
  * *Returns*: `true` if streamReady is not empty **Throws**: Exception Exception on stream errors (check .isOpened() to filter out malformed streams) or VideoCapture type is not supported The primary use of the function is in multi-camera environments. The method fills the ready state vector, grabs video frame, if camera is ready. After this call use VideoCapture.Retrieve() to decode and fetch frame data.

---
### `VideoWriter`
**Inherits from**: `DisposableOpenCVObject`

Video writer class.

**Detailed Remarks**:
The class provides the managed C# API for writing video files or image sequences.

#### Constructors
* `new VideoWriter()`
  * *Summary*: Default constructors
* `new VideoWriter(string filename, int fourcc, double fps, Size frameSize, bool isColor)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Name of the output video file.
  * *Parameter* `fourcc`: 4-character code of codec used to compress the frames. For example, VideoWriter.fourcc('P','I','M','1') is a MPEG-1 codec, VideoWriter.fourcc('M','J','P','G') is a motion-jpeg codec etc. List of codes can be obtained at [MSDN](https://docs.microsoft.com/en-us/windows/win32/medfound/video-fourccs) page or with this [page](https://fourcc.org/codecs.php) of the fourcc site for a more complete list). FFMPEG backend with MP4 container natively uses other values as fourcc code: see [ObjectType](http://mp4ra.org/#/codecs), so you may receive a warning message from OpenCV about fourcc code conversion.
  * *Parameter* `fps`: Framerate of the created video stream.
  * *Parameter* `frameSize`: Size of the video frames.
  * *Parameter* `isColor`: If it is not zero, the encoder will expect and encode color frames, otherwise it will work with grayscale frames. @b Tips: - With some backends `fourcc=-1` pops up the codec selection dialog from the system. - To save image sequence use a proper filename (eg. `img_02d.jpg`) and `fourcc=0` OR `fps=0`. Use uncompressed image format (eg. `img_02d.BMP`) to save raw frames. - Most codecs are lossy. If you want lossless video file you need to use a lossless codecs (eg. FFMPEG FFV1, Huffman HFYU, Lagarith LAGS, etc...) - If FFMPEG is enabled, using `codec=0; fps=0;` you can create an uncompressed (raw) video file. - If FFMPEG is used, we allow frames of odd width or height, but in this case we truncate the rightmost column/the bottom row. Probably, this should be handled more elegantly, but some internal functions inside FFMPEG swscale require even width/height.
* `new VideoWriter(string filename, int apiPreference, int fourcc, double fps, Size frameSize, bool isColor)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `isColor`: The isColor parameter.
* `new VideoWriter(string filename, int fourcc, double fps, Size frameSize, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `params`: The @params parameter.
* `new VideoWriter(string filename, int apiPreference, int fourcc, double fps, Size frameSize, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `params`: The @params parameter.

#### Methods
* `bool Open(string filename, int fourcc, double fps, Size frameSize, bool isColor)`
  * *Summary*: Initializes or reinitializes video writer.
  * *Remarks*:

The method opens video writer. Parameters are the same as in the constructor
VideoWriter.VideoWriter.

  * *Parameter* `filename`: Path to the file.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `isColor`: The isColor parameter.
  * *Returns*: `true` if video writer has been successfully initialized The method first calls VideoWriter.release to close the already opened file.
* `bool Open(string filename, int apiPreference, int fourcc, double fps, Size frameSize, bool isColor)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `isColor`: The isColor parameter.
  * *Returns*: `true` if video writer has been successfully initialized. The method first calls VideoWriter.release to close the already opened file.
* `bool Open(string filename, int fourcc, double fps, Size frameSize, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: `true` if video writer has been successfully initialized. The method first calls VideoWriter.release to close the already opened file.
* `bool Open(string filename, int apiPreference, int fourcc, double fps, Size frameSize, IntPtr @params)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `apiPreference`: The apiPreference parameter.
  * *Parameter* `fourcc`: The fourcc parameter.
  * *Parameter* `fps`: The fps parameter.
  * *Parameter* `frameSize`: The frameSize parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: `true` if video writer has been successfully initialized. The method first calls VideoWriter.release to close the already opened file.
* `bool IsOpened()`
  * *Summary*: Returns true if video writer has been successfully initialized.
  * *Returns*: `true` if video writer has been successfully initialized, `false` otherwise.
* `void Release()`
  * *Summary*: Closes the video writer.
  * *Remarks*:

The method is automatically called by subsequent VideoWriter.open and by the VideoWriter
destructor.

* `bool Write(Mat image)`
  * *Summary*: Writes the next video frame
  * *Parameter* `image`: The written frame. In general, color images are expected in BGR format. The function/method writes the specified image to video file. It must have the same size as has been specified when opening the video writer.
  * *Returns*: `true` if the frame was written successfully by the underlying backend, `false` otherwise (for example, on network errors when streaming, encoder failures, or unsupported input frames). Backends that do not surface per-frame status from their native API report `true` on best-effort success.
* `bool Set(int propId, double value)`
  * *Summary*: Sets a property in the VideoWriter.
  * *Parameter* `propId`: Property identifier from VideoWriterProperties (eg. VIDEOWRITER_PROP_QUALITY) or one of `videoio_flags_others`
  * *Parameter* `value`: Value of the property.
  * *Returns*: `true` if the property is supported by the backend used by the VideoWriter instance.
* `double Get(int propId)`
  * *Summary*: Returns the specified VideoWriter property
  * *Parameter* `propId`: Property identifier from VideoWriterProperties (eg. VIDEOWRITER_PROP_QUALITY) or one of `videoio_flags_others`
  * *Returns*: Value for the specified property. Value 0 is returned when querying a property that is not supported by the backend used by the VideoWriter instance.
* `int Fourcc(byte c1, byte c2, byte c3, byte c4)`
  * *Summary*: Concatenates 4 chars to a fourcc code
  * *Parameter* `c1`: The c1 parameter.
  * *Parameter* `c2`: The c2 parameter.
  * *Parameter* `c3`: The c3 parameter.
  * *Parameter* `c4`: The c4 parameter.
  * *Returns*: a fourcc code This static method constructs the fourcc code of the codec to be used in the constructor VideoWriter.VideoWriter or VideoWriter.open.
* `string? GetBackendName()`
  * *Summary*: Returns used backend API name
  * *Remarks*:

.: info Note
Stream should be opened.
.:

  * *Returns*: The backend API name as a string, or `null` if the stream is not opened.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.VideoioRegistryGetBackendName`
**Signature**: `string? VideoioRegistryGetBackendName(VideoCaptureAPIs api)`

Returns backend API name or "UnknownVideoAPI(xxx)"

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`)

**Returns**: The backend API name string, or `"UnknownVideoAPI(xxx)"` if the backend is not recognized.

---
### `Cv2.VideoioRegistryGetBackends`
**Signature**: `IntPtr VideoioRegistryGetBackends()`

Returns list of all available backends

**Returns**: A pointer to the list of all available backend IDs.

---
### `Cv2.VideoioRegistryGetCameraBackends`
**Signature**: `IntPtr VideoioRegistryGetCameraBackends()`

Returns list of available backends which works via `VideoCapture(int index)`

**Returns**: A pointer to the list of available camera backend IDs.

---
### `Cv2.VideoioRegistryGetStreamBackends`
**Signature**: `IntPtr VideoioRegistryGetStreamBackends()`

Returns list of available backends which works via `VideoCapture(filename)`

**Returns**: A pointer to the list of available stream backend IDs.

---
### `Cv2.VideoioRegistryGetStreamBufferedBackends`
**Signature**: `IntPtr VideoioRegistryGetStreamBufferedBackends()`

Returns list of available backends which works via `VideoCapture(buffer)`

**Returns**: A pointer to the list of available buffered-stream backend IDs.

---
### `Cv2.VideoioRegistryGetWriterBackends`
**Signature**: `IntPtr VideoioRegistryGetWriterBackends()`

Returns list of available backends which works via `VideoWriter()`

**Returns**: A pointer to the list of available writer backend IDs.

---
### `Cv2.VideoioRegistryHasBackend`
**Signature**: `bool VideoioRegistryHasBackend(VideoCaptureAPIs api)`

Returns true if backend is available

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to check availability for.

**Returns**: `true` if the specified backend is available, `false` otherwise.

---
### `Cv2.VideoioRegistryIsBackendBuiltIn`
**Signature**: `bool VideoioRegistryIsBackendBuiltIn(VideoCaptureAPIs api)`

Returns true if backend is built in (false if backend is used as plugin)

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to check.

**Returns**: `true` if the backend is built in, `false` if it is used as a plugin.

---
### `Cv2.VideoioRegistryGetCameraBackendPluginVersion`
**Signature**: `string? VideoioRegistryGetCameraBackendPluginVersion(VideoCaptureAPIs api, int version_ABI, int version_API)`

Returns description and ABI/API version of videoio plugin's camera interface

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to query.
* `version_ABI`: output ABI version of the plugin's camera interface.
* `version_API`: output API version of the plugin's camera interface.

**Returns**: The description string of the plugin's camera interface, or `null` if unavailable.

---
### `Cv2.VideoioRegistryGetStreamBackendPluginVersion`
**Signature**: `string? VideoioRegistryGetStreamBackendPluginVersion(VideoCaptureAPIs api, int version_ABI, int version_API)`

Returns description and ABI/API version of videoio plugin's stream capture interface

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to query.
* `version_ABI`: output ABI version of the plugin's stream capture interface.
* `version_API`: output API version of the plugin's stream capture interface.

**Returns**: The description string of the plugin's stream capture interface, or `null` if unavailable.

---
### `Cv2.VideoioRegistryGetStreamBufferedBackendPluginVersion`
**Signature**: `string? VideoioRegistryGetStreamBufferedBackendPluginVersion(VideoCaptureAPIs api, int version_ABI, int version_API)`

Returns description and ABI/API version of videoio plugin's buffer capture interface

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to query.
* `version_ABI`: output ABI version of the plugin's buffer capture interface.
* `version_API`: output API version of the plugin's buffer capture interface.

**Returns**: The description string of the plugin's buffer capture interface, or `null` if unavailable.

---
### `Cv2.VideoioRegistryGetWriterBackendPluginVersion`
**Signature**: `string? VideoioRegistryGetWriterBackendPluginVersion(VideoCaptureAPIs api, int version_ABI, int version_API)`

Returns description and ABI/API version of videoio plugin's writer interface

**Parameters**:
* `api`: backend ID (`VideoCaptureAPIs`) to query.
* `version_ABI`: output ABI version of the plugin's writer interface.
* `version_API`: output API version of the plugin's writer interface.

**Returns**: The description string of the plugin's writer interface, or `null` if unavailable.

---
## 🔢 Enumerations

### `UnnamedEnum0`
Specifies pixel format options for video I/O operations.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Gray`** | `0` | Grayscale pixel format |
| **`Rgba`** | `1` | RGBA (Red, Green, Blue, Alpha) pixel format |
| **`Bgr`** | `2` | BGR (Blue, Green, Red) pixel format |
| **`Yuv444p`** | `3` | YUV 4:4:4 planar pixel format |

---
### `Codecs`
Specifies available video codec identifiers.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Mjpeg`** | `0` | Motion JPEG codec |

---
### `StreamType`
Specifies stream type identifiers for video I/O operations.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Db`** | `0` | Data buffer stream |
| **`Dc`** | `1` | Data capture stream |
| **`Pc`** | `2` | Processed capture stream |
| **`Wb`** | `3` | Write buffer stream |

---
### `VideoAccelerationType`
Specifies hardware video acceleration types for decoding and encoding.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | No hardware acceleration |
| **`Any`** | `1` | Use any available hardware acceleration |
| **`D3d11`** | `2` | DirectX 11 hardware acceleration |
| **`Vaapi`** | `3` | VA-API (Video Acceleration API, Linux) |
| **`Mfx`** | `4` | Intel Media SDK / oneVPL (MFX) acceleration |
| **`Drm`** | `5` | DRM (Direct Rendering Manager) acceleration |

---
### `VideoCaptureAPIs`
Specifies video capture backend API identifiers.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Any`** | `0` | Auto-detect backend |
| **`V4l`** | `200` | Video4Linux backend |
| **`V4l2`** | `unchecked((int)(V4l))` | Video4Linux2 backend (alias for V4l) |
| **`Firewire`** | `300` | IEEE 1394 / FireWire backend |
| **`Fireware`** | `unchecked((int)(Firewire))` | Alias for Firewire |
| **`Ieee1394`** | `unchecked((int)(Firewire))` | Alias for Firewire (IEEE 1394) |
| **`Dc1394`** | `unchecked((int)(Firewire))` | Alias for Firewire (libdc1394) |
| **`Cmu1394`** | `unchecked((int)(Firewire))` | Alias for Firewire (CMU1394) |
| **`Dshow`** | `700` | DirectShow backend (Windows) |
| **`Pvapi`** | `800` | PvAPI / Prosilica GigE SDK backend |
| **`Android`** | `1000` | Android native camera backend |
| **`Xiapi`** | `1100` | XIMEA Camera API backend |
| **`Avfoundation`** | `1200` | AVFoundation backend (macOS/iOS) |
| **`Msmf`** | `1400` | Microsoft Media Foundation backend (Windows) |
| **`Winrt`** | `1410` | WinRT / Microsoft Windows Runtime backend |
| **`Intelperc`** | `1500` | Intel Perceptual Computing SDK backend |
| **`Realsense`** | `1500` | Intel RealSense backend (same value as Intelperc) |
| **`Openni2`** | `1600` | OpenNI2 backend for depth sensors |
| **`Openni2Asus`** | `1610` | OpenNI2 backend for ASUS Xtion sensors |
| **`Openni2Astra`** | `1620` | OpenNI2 backend for Orbbec Astra sensors |
| **`Gphoto2`** | `1700` | gPhoto2 backend for digital cameras |
| **`Gstreamer`** | `1800` | GStreamer multimedia framework backend |
| **`Ffmpeg`** | `1900` | FFmpeg backend |
| **`Images`** | `2000` | Image sequence backend (e.g. `img_%02d.jpg`) |
| **`Aravis`** | `2100` | Aravis GigE Vision SDK backend |
| **`OpencvMjpeg`** | `2200` | OpenCV built-in Motion JPEG codec |
| **`IntelMfx`** | `2300` | Intel Media SDK / oneVPL backend |
| **`Xine`** | `2400` | Xine multimedia library backend |
| **`Ueye`** | `2500` | IDS uEye camera SDK backend |
| **`Obsensor`** | `2600` | Orbbec depth/RGB sensor backend |

---
### `VideoCaptureOBSensorDataType`
Specifies data types for OBSensor (Orbbec) video capture devices.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`DepthMap`** | `0` | Depth map data from the sensor |
| **`BgrImage`** | `1` | BGR color image from the sensor |
| **`IrImage`** | `2` | Infrared image from the sensor |

---
### `VideoCaptureOBSensorGenerators`
Specifies stream generator flags for OBSensor (Orbbec) video capture devices.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`DepthGenerator`** | `unchecked((int)(1 << 29))` | Depth stream generator flag |
| **`ImageGenerator`** | `unchecked((int)(1 << 28))` | Color image stream generator flag |
| **`IrGenerator`** | `unchecked((int)(1 << 27))` | Infrared stream generator flag |
| **`GeneratorsMask`** | `unchecked((int)(DepthGenerator + ImageGenerator + IrGenerator))` | Combined bitmask of all generator flags |

---
### `VideoCaptureOBSensorProperties`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`IntrinsicFx`** | `26001` | Camera intrinsic focal length in X |
| **`IntrinsicFy`** | `26002` | Camera intrinsic focal length in Y |
| **`IntrinsicCx`** | `26003` | Camera intrinsic principal point X |
| **`IntrinsicCy`** | `26004` | Camera intrinsic principal point Y |
| **`RgbPosMsec`** | `26005` | RGB stream position in milliseconds |
| **`DepthPosMsec`** | `26006` | Depth stream position in milliseconds |
| **`DepthWidth`** | `26007` | Width of the depth frame |
| **`DepthHeight`** | `26008` | Height of the depth frame |
| **`DepthFps`** | `26009` | Framerate of the depth stream |
| **`ColorDistortionK1`** | `26010` | Color lens radial distortion coefficient k1 |
| **`ColorDistortionK2`** | `26011` | Color lens radial distortion coefficient k2 |
| **`ColorDistortionK3`** | `26012` | Color lens radial distortion coefficient k3 |
| **`ColorDistortionK4`** | `26013` | Color lens radial distortion coefficient k4 |
| **`ColorDistortionK5`** | `26014` | Color lens radial distortion coefficient k5 |
| **`ColorDistortionK6`** | `26015` | Color lens radial distortion coefficient k6 |
| **`ColorDistortionP1`** | `26016` | Color lens tangential distortion coefficient p1 |
| **`ColorDistortionP2`** | `26017` | Color lens tangential distortion coefficient p2 |

---
### `VideoCaptureProperties`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Unknown`** | `-1` | Unknown / unsupported property |
| **`PosMsec`** | `0` | Current position in milliseconds |
| **`PosFrames`** | `1` | 0-based index of the next frame to decode/capture |
| **`PosAviRatio`** | `2` | Relative position (0 = start, 1 = end) |
| **`FrameWidth`** | `3` | Width of the video frames |
| **`FrameHeight`** | `4` | Height of the video frames |
| **`Fps`** | `5` | Frame rate (frames per second) |
| **`Fourcc`** | `6` | 4-character codec code |
| **`FrameCount`** | `7` | Total number of frames in the video |
| **`Format`** | `8` | Format of the Mat returned by retrieve() |
| **`Mode`** | `9` | Backend-specific mode value |
| **`Brightness`** | `10` | Brightness of the image (camera only) |
| **`Contrast`** | `11` | Contrast of the image (camera only) |
| **`Saturation`** | `12` | Saturation of the image (camera only) |
| **`Hue`** | `13` | Hue of the image (camera only) |
| **`Gain`** | `14` | Gain of the image (camera only) |
| **`Exposure`** | `15` | Exposure setting (camera only) |
| **`ConvertRgb`** | `16` | Whether images are converted to RGB |
| **`WhiteBalanceBlueU`** | `17` | White balance blue-U component |
| **`Rectification`** | `18` | Rectification flag for stereo cameras |
| **`Monochrome`** | `19` | Monochrome capture mode |
| **`Sharpness`** | `20` | Sharpness of the image (camera only) |
| **`AutoExposure`** | `21` | Auto-exposure mode (camera only) |
| **`Gamma`** | `22` | Gamma correction |
| **`Temperature`** | `23` | Color temperature |
| **`Trigger`** | `24` | Trigger mode (camera only) |
| **`TriggerDelay`** | `25` | Trigger delay (camera only) |
| **`WhiteBalanceRedV`** | `26` | White balance red-V component |
| **`Zoom`** | `27` | Camera zoom level |
| **`Focus`** | `28` | Camera focus setting |
| **`Guid`** | `29` | GUID of the camera device |
| **`IsoSpeed`** | `30` | ISO speed (camera only) |
| **`Backlight`** | `32` | Backlight compensation |
| **`Pan`** | `33` | Camera pan position |
| **`Tilt`** | `34` | Camera tilt position |
| **`Roll`** | `35` | Camera roll position |
| **`Iris`** | `36` | Camera iris setting |
| **`Settings`** | `37` | Pop up camera/video settings dialog |
| **`Buffersize`** | `38` | Internal buffer size |
| **`Autofocus`** | `39` | Autofocus mode (camera only) |
| **`SarNum`** | `40` | Sample aspect ratio numerator |
| **`SarDen`** | `41` | Sample aspect ratio denominator |
| **`Backend`** | `42` | Current backend identifier |
| **`Channel`** | `43` | Video input / channel number |
| **`AutoWb`** | `44` | Auto white balance |
| **`WbTemperature`** | `45` | White balance color temperature |
| **`CodecPixelFormat`** | `46` | Codec pixel format (fourcc code) |
| **`Bitrate`** | `47` | Video bitrate in kbits/s |
| **`OrientationMeta`** | `48` | Orientation metadata from the file |
| **`OrientationAuto`** | `49` | Auto-rotate frames based on metadata |
| **`HwAcceleration`** | `50` | Hardware acceleration type (see VideoAccelerationType) |
| **`HwDevice`** | `51` | Hardware device index for acceleration |
| **`HwAccelerationUseOpencl`** | `52` | Use OpenCL for hardware acceleration |
| **`OpenTimeoutMsec`** | `53` | Open timeout in milliseconds |
| **`ReadTimeoutMsec`** | `54` | Read timeout in milliseconds |
| **`StreamOpenTimeUsec`** | `55` | Time to open the stream in microseconds |
| **`VideoTotalChannels`** | `56` | Total number of video channels |
| **`VideoStream`** | `57` | Index of the selected video stream |
| **`AudioStream`** | `58` | Index of the selected audio stream |
| **`AudioPos`** | `59` | Current audio position |
| **`AudioShiftNsec`** | `60` | Audio shift in nanoseconds |
| **`AudioDataDepth`** | `61` | Audio data bit depth |
| **`AudioSamplesPerSecond`** | `62` | Audio sample rate (samples/second) |
| **`AudioBaseIndex`** | `63` | Base index of audio streams |
| **`AudioTotalChannels`** | `64` | Total number of audio channels |
| **`AudioTotalStreams`** | `65` | Total number of audio streams |
| **`AudioSynchronize`** | `66` | Enable audio-video synchronization |
| **`LrfHasKeyFrame`** | `67` | Whether the last read frame has a key frame |
| **`CodecExtradataIndex`** | `68` | Index for codec extradata |
| **`FrameType`** | `69` | Type of the current frame (I, P, B, etc.) |
| **`NThreads`** | `70` | Number of threads for decoding |
| **`Pts`** | `71` | Presentation timestamp of the current frame |
| **`DtsDelay`** | `72` | DTS (Decode Timestamp) delay |
| **`ImageSeqStart`** | `73` | Start index for image sequence |

---
### `VideoWriterProperties`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Unknown`** | `-1` | Unknown / unsupported property |
| **`Quality`** | `1` | Quality setting (0–100) for JPEG-based codecs |
| **`Framebytes`** | `2` | Size of the encoded frame in bytes (read-only) |
| **`Nstripes`** | `3` | Number of stripes for parallel encoding |
| **`IsColor`** | `4` | Whether the output is a color video |
| **`Depth`** | `5` | Depth (bit depth) of the output frames |
| **`HwAcceleration`** | `6` | Hardware acceleration type (see VideoAccelerationType) |
| **`HwDevice`** | `7` | Hardware device index for acceleration |
| **`HwAccelerationUseOpencl`** | `8` | Use OpenCL for hardware acceleration |
| **`RawVideo`** | `9` | Write raw video without compression |
| **`KeyInterval`** | `10` | Interval between key frames |
| **`KeyFlag`** | `11` | Flag indicating whether current frame is a key frame |
| **`Pts`** | `12` | Presentation timestamp of the current frame |
| **`DtsDelay`** | `13` | DTS (Decode Timestamp) delay |
| **`ColorSpace`** | `14` | Output color space |
| **`EnableAlpha`** | `15` | Enable alpha channel in the output |

---

</div>