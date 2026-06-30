# IMGCODECS Module API Reference

Complete documentation for the **IMGCODECS** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Imgcodecs Documentation](https://docs.opencv.org/5.x/main_modules/imgcodecs.html).

---
<div v-pre>

## 📦 Classes and Structs

### `Animation`
**Inherits from**: `DisposableOpenCVObject`

Represents an animation with multiple frames. The `Animation` struct is designed to store and manage data for animated sequences such as those from animated formats (e.g., GIF, AVIF, APNG, WebP). It provides support for looping, background color settings, frame timing, and frame storage.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`LoopCount`** | `int` | Gets or sets the loop_count property. |
| **`Bgcolor`** | `Scalar` | Gets or sets the bgcolor property. |
| **`Durations`** | `int[]` | Gets or sets the durations property. |
| **`Frames`** | `Mat[]` | Gets or sets the frames property. |
| **`StillImage`** | `Mat?` | Gets or sets the still_image property. |

#### Constructors
* `new Animation(int loopCount, Scalar bgColor)`
  * *Summary*: Constructs an Animation object with optional loop count and background color.
  * *Parameter* `loopCount`: An integer representing the number of times the animation should loop: - `0` (default) indicates infinite looping, meaning the animation will replay continuously. - Positive values denote finite repeat counts, allowing the animation to play a limited number of times. - If a negative value or a value beyond the maximum of `0xffff` (65535) is provided, it is reset to `0` (infinite looping) to maintain valid bounds.
  * *Parameter* `bgColor`: A `Scalar` object representing the background color in BGR format: - Defaults to `Scalar()`, indicating an empty color (usually transparent if supported). - This background color provides a solid fill behind frames that have transparency, ensuring a consistent display appearance.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.Imread`
**Signature**: `Mat? Imread(string filename, int flags)`

Loads an image from a file.

**Detailed Remarks**:
The `imread` function loads an image from the specified file and returns OpenCV matrix. If the image cannot be
read (because of a missing file, improper permissions, or unsupported/invalid format), the function
returns an empty matrix.
Currently, the following file formats are supported:
-   Windows bitmaps - \*.bmp, \*.dib (always supported)
-   GIF files - \*.gif (always supported)
-   JPEG files - \*.jpeg, \*.jpg, \*.jpe (see the *Note* section)
-   JPEG 2000 files - \*.jp2 (see the *Note* section)
-   Portable Network Graphics - \*.png (see the *Note* section)
-   WebP - \*.webp (see the *Note* section)
-   AVIF - \*.avif (see the *Note* section)
-   Portable image format - \*.pbm, \*.pgm, \*.ppm, \*.pxm, \*.pnm (always supported)
-   PFM files - \*.pfm (see the *Note* section)
-   Sun rasters - \*.sr, \*.ras (always supported)
-   TIFF files - \*.tiff, \*.tif (see the *Note* section)
-   OpenEXR Image files - \*.exr (see the *Note* section)
-   Radiance HDR - \*.hdr, \*.pic (always supported)
-   Raster and Vector geospatial data supported by GDAL (see the *Note* section)
.: info Note

-   The function determines the type of an image by its content, not by the file extension.
-   In the case of color images, the decoded images will have the channels stored in **B G R** order.
-   When using IMREAD_GRAYSCALE, the codec's internal grayscale conversion will be used, if available.
Results may differ from the output of cvtColor().
-   On Microsoft Windows\* and Mac OS\*, the codecs shipped with OpenCV (libjpeg, libpng, libtiff,
and libjasper) are used by default. So, OpenCV can always read JPEGs, PNGs, and TIFFs. On Mac OS,
there is also an option to use native Mac OS image readers. However, beware that currently these
native image loaders give images with different pixel values because of the color management embedded
into Mac OS.
-   On Linux\*, BSD flavors, and other Unix-like open-source operating systems, OpenCV looks for
codecs supplied with the OS. Ensure the relevant packages are installed (including development
files, such as "libjpeg-dev" in Debian\* and Ubuntu\*) to get codec support, or turn
on the OPENCV_BUILD_3RDPARTY_LIBS flag in CMake.
-   If the *WITH_GDAL* flag is set to true in CMake and `IMREAD_LOAD_GDAL` is used to load the image,
the [GDAL](http://www.gdal.org) driver will be used to decode the image, supporting
[Raster](http://www.gdal.org/formats_list.html) and [Vector](http://www.gdal.org/ogr_formats.html) formats.
-   If EXIF information is embedded in the image file, the EXIF orientation will be taken into account,
and thus the image will be rotated accordingly unless the flags `IMREAD_IGNORE_ORIENTATION`
or `IMREAD_UNCHANGED` are passed.
-   Use the IMREAD_UNCHANGED flag to preserve the floating-point values from PFM images.
-   By default, the number of pixels must be less than 2^30. This limit can be changed by setting
the environment variable `OPENCV_IO_MAX_IMAGE_PIXELS`. See `tutorial_env_reference`.
.:

**Parameters**:
* `filename`: Name of the file to be loaded.
* `flags`: Flag that can take values of ImreadModes, default with IMREAD_COLOR_BGR.

**Returns**: The returned value.

---
### `Cv2.Imread`
**Signature**: `void Imread(string filename, Mat dst, int flags)`

Loads an image from a file.

**Detailed Remarks**:
This is an overloaded member function, provided for convenience. It differs from the above function only in what argument(s) it accepts and the return value.
.: info Note

The image passing through the img parameter can be pre-allocated. The memory is reused if the shape and the type match with the load image.
.:

**Parameters**:
* `filename`: Name of file to be loaded.
* `dst`: object in which the image will be loaded.
* `flags`: Flag that can take values of ImreadModes, default with IMREAD_COLOR_BGR.

---
### `Cv2.ImreadWithMetadata`
**Signature**: `Mat? ImreadWithMetadata(string filename, IntPtr metadataTypes, IntPtr metadata, int flags)`

Reads an image from a file along with associated metadata.

**Detailed Remarks**:
This function behaves similarly to imread(), loading an image from the specified file.
In addition to the image pixel data, it also attempts to extract any available metadata
embedded in the file (such as EXIF, XMP, etc.), depending on file format support.
.: info Note
In the case of color images, the decoded images will have the channels stored in **B G R** order.
.:

**Parameters**:
* `filename`: Name of the file to be loaded.
* `metadataTypes`: Output vector with types of metadata chunks returned in metadata, see ImageMetadataType.
* `metadata`: Output arrays of matrices or vector of matrices to store the retrieved metadata.
* `flags`: Flag that can take values of ImreadModes.

**Returns**: The loaded image as a Mat object. If the image cannot be read, the function returns an empty matrix.

---
### `Cv2.Imreadmulti`
**Signature**: `bool Imreadmulti(string filename, IntPtr mats, int flags)`

Loads a multi-page image from a file.

**Detailed Remarks**:
The function imreadmulti loads a multi-page image from the specified file into a vector of Mat objects.
.: info Note
The default flags value was changed from IMREAD_ANYCOLOR to IMREAD_COLOR_BGR for unification.
**See also**: imread
.:

**Parameters**:
* `filename`: Name of file to be loaded.
* `mats`: A vector of Mat objects holding each page.
* `flags`: Flag that can take values of ImreadModes, default with IMREAD_COLOR_BGR.

**Returns**: The returned value.

---
### `Cv2.Imreadmulti`
**Signature**: `bool Imreadmulti(string filename, IntPtr mats, int start, int count, int flags)`

Loads images of a multi-page image from a file.

**Detailed Remarks**:
The function imreadmulti loads a specified range from a multi-page image from the specified file into a vector of Mat objects.
**See also**: imread

**Parameters**:
* `filename`: Name of file to be loaded.
* `mats`: A vector of Mat objects holding each page.
* `start`: Start index of the image to load
* `count`: Count number of images to load
* `flags`: Flag that can take values of ImreadModes, default with IMREAD_ANYCOLOR.

**Returns**: The returned value.

---
### `Cv2.Imreadanimation`
**Signature**: `bool Imreadanimation(string filename, Animation animation, int start, int count)`

Loads frames from an animated image file into an Animation structure.

**Detailed Remarks**:
The function imreadanimation loads frames from an animated image file (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.

**Parameters**:
* `filename`: A string containing the path to the file.
* `animation`: A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
* `start`: The index of the first frame to load. This is optional and defaults to 0.
* `count`: The number of frames to load. This is optional and defaults to 32767.

**Returns**: Returns true if the file was successfully loaded and frames were extracted; returns false otherwise.

---
### `Cv2.Imdecodeanimation`
**Signature**: `bool Imdecodeanimation(Mat buf, Animation animation, int start, int count)`

Loads frames from an animated image buffer into an Animation structure.

**Detailed Remarks**:
The function imdecodeanimation loads frames from an animated image buffer (e.g., GIF, AVIF, APNG, WEBP) into the provided Animation struct.

**Parameters**:
* `buf`: A reference to a Mat containing the image buffer.
* `animation`: A reference to an Animation structure where the loaded frames will be stored. It should be initialized before the function is called.
* `start`: The index of the first frame to load. This is optional and defaults to 0.
* `count`: The number of frames to load. This is optional and defaults to 32767.

**Returns**: Returns true if the buffer was successfully loaded and frames were extracted; returns false otherwise.

---
### `Cv2.Imwriteanimation`
**Signature**: `bool Imwriteanimation(string filename, Animation animation, IntPtr @params)`

Saves an Animation to a specified file.

**Detailed Remarks**:
The function imwriteanimation saves the provided Animation data to the specified file in an animated format.
Supported formats depend on the implementation and may include formats like GIF, AVIF, APNG, or WEBP.

**Parameters**:
* `filename`: The name of the file where the animation will be saved. The file extension determines the format.
* `animation`: A constant reference to an Animation struct containing the frames and metadata to be saved.
* `params`: Optional format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ...). These parameters are used to specify additional options for the encoding process. Refer to `ImwriteFlags` for details on possible parameters.

**Returns**: Returns true if the animation was successfully saved; returns false otherwise.

---
### `Cv2.Imencodeanimation`
**Signature**: `bool Imencodeanimation(string ext, Animation animation, IntPtr buf, IntPtr @params)`

Encodes an Animation to a memory buffer.

**Detailed Remarks**:
The function imencodeanimation encodes the provided Animation data into a memory
buffer in an animated format. Supported formats depend on the implementation and
may include formats like GIF, AVIF, APNG, or WEBP.

**Parameters**:
* `ext`: The file extension that determines the format of the encoded data.
* `animation`: A constant reference to an Animation struct containing the frames and metadata to be encoded.
* `buf`: A reference to a vector of unsigned chars where the encoded data will be stored.
* `params`: Optional format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ...). These parameters are used to specify additional options for the encoding process. Refer to `ImwriteFlags` for details on possible parameters.

**Returns**: Returns true if the animation was successfully encoded; returns false otherwise.

---
### `Cv2.Imcount`
**Signature**: `long Imcount(string filename, int flags)`

Returns the number of images inside the given file

**Detailed Remarks**:
The function imcount returns the number of pages in a multi-page image (e.g. TIFF), the number of frames in an animation (e.g. AVIF), and 1 otherwise.
If the image cannot be decoded, 0 is returned.
.: info Note
The default flags value was changed from IMREAD_ANYCOLOR to IMREAD_COLOR_BGR for unification.
When IMREAD_LOAD_GDAL flag is used the return value will be 0 or 1 because OpenCV's GDAL decoder doesn't support multi-page reading yet.
.:

**Parameters**:
* `filename`: Name of file to be loaded.
* `flags`: Flag that can take values of ImreadModes, default with IMREAD_COLOR_BGR.

**Returns**: The returned value.

---
### `Cv2.Imwrite`
**Signature**: `bool Imwrite(string filename, Mat img, IntPtr @params)`

Saves an image to a specified file.

**Detailed Remarks**:
The function imwrite saves the image to the specified file. The image format is chosen based on the
filename extension (see imread for the list of extensions). In general, only 8-bit unsigned (CV_8U)
single-channel or 3-channel (with 'BGR' channel order) images
can be saved using this function, with these exceptions:
- With BMP encoder, 8-bit unsigned (CV_8U) images can be saved.
- BMP images with an alpha channel can be saved using this function.
To achieve this, create an 8-bit 4-channel (CV_8UC4) BGRA image, ensuring the alpha channel is the last component.
Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255.
OpenCV v4.13.0 or later use BI_BITFIELDS compression as default. See IMWRITE_BMP_COMPRESSION.
- With OpenEXR encoder, only 32-bit float (CV_32F) images can be saved. More than 4 channels can be saved. (imread can load it then.)
- 8-bit unsigned (CV_8U) images are not supported.
- With Radiance HDR encoder, non 64-bit float (CV_64F) images can be saved.
- All images will be converted to 32-bit float (CV_32F).
- With JPEG 2000 encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
- With JPEG XL encoder, 8-bit unsigned (CV_8U), 16-bit unsigned (CV_16U) and 32-bit float(CV_32F) images can be saved.
- JPEG XL images with an alpha channel can be saved using this function.
To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) / 32-bit float 4-channel (CV_32FC4) BGRA image, ensuring the alpha channel is the last component.
Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535/1.0.
- With PAM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
- With PNG encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
- PNG images with an alpha channel can be saved using this function.
To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) BGRA image, ensuring the alpha channel is the last component.
Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255/65535(see the code sample below).
- With PGM/PPM encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
- With TIFF encoder, 8-bit unsigned (CV_8U), 8-bit signed (CV_8S),
16-bit unsigned (CV_16U), 16-bit signed (CV_16S),
32-bit unsigned (CV_32U), 32-bit signed (CV_32S),
64-bit unsigned (CV_64U), 64-bit signed (CV_64S),
32-bit float (CV_32F) and 64-bit float (CV_64F) images can be saved.
- Multiple images (vector of Mat) can be saved in TIFF format (see the code sample below).
- 32-bit float 3-channel (CV_32FC3) TIFF images can be saved
using the LogLuv high dynamic range encoding (4 bytes per pixel) through TIFF_COMPRESSION_SGILOG or
(3 bytes per pixel) through TIFF_COMPRESSION_SGILOG24.
- Other compression schemes (LZW...) are supported as well for 32F depth, but the efficiency might not
be very good for the floating-point representation bit patterns.
- With GIF encoder, 8-bit unsigned (CV_8U) images can be saved.
- GIF images with an alpha channel can be saved using this function.
To achieve this, create an 8-bit 4-channel (CV_8UC4) BGRA image, ensuring the alpha channel is the last component.
Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255.
- 8-bit single-channel images (CV_8UC1) are not supported due to GIF's limitation to indexed color formats.
- With AVIF encoder, 8-bit unsigned (CV_8U) and 16-bit unsigned (CV_16U) images can be saved.
- CV_16U images can be saved as only 10-bit or 12-bit (not 16-bit). See IMWRITE_AVIF_DEPTH.
- AVIF images with an alpha channel can be saved using this function.
To achieve this, create an 8-bit 4-channel (CV_8UC4) / 16-bit 4-channel (CV_16UC4) BGRA image, ensuring the alpha channel is the last component.
Fully transparent pixels should have an alpha value of 0, while fully opaque pixels should have an alpha value of 255 (8-bit) / 1023 (10-bit) / 4095 (12-bit) (see the code sample below).
If the image format is not supported, the image will be converted to 8-bit unsigned (CV_8U) and saved that way.
If the format, depth or channel order is different, use
Mat.ConvertTo and cvtColor to convert it before saving. Or, use the universal FileStorage I/O
functions to save the image to XML or YAML format.
The sample below shows how to create a BGRA image, how to set custom compression parameters and save it to a PNG file.
It also demonstrates how to save multiple images in a TIFF file:
See example in OpenCV documentation. **Parameters**:
* `filename`: Name of the file.
* `img`: (Mat or vector of Mat) Image or Images to be saved.
* `params`: Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see ImwriteFlags

**Returns**: true if the image is successfully written to the specified file; false otherwise.

---
### `Cv2.ImwriteWithMetadata`
**Signature**: `bool ImwriteWithMetadata(string filename, Mat img, IntPtr metadataTypes, IntPtr metadata, IntPtr @params)`

Saves an image to a specified file with metadata

**Detailed Remarks**:
The function imwriteWithMetadata saves the image to the specified file. It does the same thing as imwrite, but additionally writes metadata if the corresponding format supports it.

**Parameters**:
* `filename`: Name of the file. As with imwrite, image format is determined by the file extension.
* `img`: (Mat or vector of Mat) Image or Images to be saved.
* `metadataTypes`: Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
* `metadata`: Vector of vectors or vector of matrices with chunks of metadata to store into the file
* `params`: Format-specific parameters encoded as pairs (paramId_1, paramValue_1, paramId_2, paramValue_2, ... .) see ImwriteFlags

**Returns**: The returned value.

---
### `Cv2.Imwritemulti`
**Signature**: `bool Imwritemulti(string filename, IntPtr img, IntPtr @params)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `filename`: Path to the file.
* `img`: Input image.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.Imdecode`
**Signature**: `Mat? Imdecode(Mat buf, int flags)`

Reads an image from a buffer in memory.

**Detailed Remarks**:
The function imdecode reads an image from the specified buffer in the memory. If the buffer is too short or
contains invalid data, the function returns an empty matrix ( Mat.Data == IntPtr.Zero ).
See imread for the list of supported formats and flags description.
.: info Note
In the case of color images, the decoded images will have the channels stored in **B G R** order.
.:

**Parameters**:
* `buf`: Input array or vector of bytes.
* `flags`: Flag that can take values of ImreadModes.

**Returns**: The returned value.

---
### `Cv2.ImdecodeWithMetadata`
**Signature**: `Mat? ImdecodeWithMetadata(Mat buf, IntPtr metadataTypes, IntPtr metadata, int flags)`

Reads an image from a memory buffer and extracts associated metadata.

**Detailed Remarks**:
This function decodes an image from the specified memory buffer. If the buffer is too short or
contains invalid data, the function returns an empty matrix ( Mat.Data == IntPtr.Zero ).
See imread for the list of supported formats and flags description.
.: info Note
In the case of color images, the decoded images will have the channels stored in **B G R** order.
.:

**Parameters**:
* `buf`: Input array or vector of bytes containing the encoded image data.
* `metadataTypes`: Output vector with types of metadata chucks returned in metadata, see ImageMetadataType
* `metadata`: Output arrays of matrices or vector of matrices to store the retrieved metadata
* `flags`: Flag that can take values of ImreadModes.

**Returns**: The decoded image as a Mat object. If decoding fails, the function returns an empty matrix.

---
### `Cv2.Imdecodemulti`
**Signature**: `bool Imdecodemulti(Mat buf, int flags, IntPtr mats, Range range)`

Reads a multi-page image from a buffer in memory.

**Detailed Remarks**:
The function imdecodemulti reads a multi-page image from the specified buffer in the memory. If the buffer is too short or
contains invalid data, the function returns false.
See imreadmulti for the list of supported formats and flags description.
.: info Note
In the case of color images, the decoded images will have the channels stored in **B G R** order.
.:

**Parameters**:
* `buf`: Input array or vector of bytes.
* `flags`: Flag that can take values of ImreadModes.
* `mats`: A vector of Mat objects holding each page, if more than one.
* `range`: A continuous selection of pages.

**Returns**: The returned value.

---
### `Cv2.Imencode`
**Signature**: `bool Imencode(string ext, Mat img, IntPtr buf, IntPtr @params)`

Encodes an image into a memory buffer.

**Detailed Remarks**:
The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
result. See imwrite for the list of supported formats and flags description.

**Parameters**:
* `ext`: File extension that defines the output format. Must include a leading period.
* `img`: Image to be compressed.
* `buf`: Output buffer resized to fit the compressed image.
* `params`: Format-specific parameters. See imwrite and ImwriteFlags.

**Returns**: The returned value.

---
### `Cv2.ImencodeWithMetadata`
**Signature**: `bool ImencodeWithMetadata(string ext, Mat img, IntPtr metadataTypes, IntPtr metadata, IntPtr buf, IntPtr @params)`

Encodes an image into a memory buffer.

**Detailed Remarks**:
The function imencode compresses the image and stores it in the memory buffer that is resized to fit the
result. See imwrite for the list of supported formats and flags description.

**Parameters**:
* `ext`: File extension that defines the output format. Must include a leading period.
* `img`: Image to be compressed.
* `metadataTypes`: Vector with types of metadata chucks stored in metadata to write, see ImageMetadataType.
* `metadata`: Vector of vectors or vector of matrices with chunks of metadata to store into the file
* `buf`: Output buffer resized to fit the compressed image.
* `params`: Format-specific parameters. See imwrite and ImwriteFlags.

**Returns**: The returned value.

---
### `Cv2.Imencodemulti`
**Signature**: `bool Imencodemulti(string ext, IntPtr imgs, IntPtr buf, IntPtr @params)`

Encodes array of images into a memory buffer.

**Detailed Remarks**:
The function is analog to imencode for in-memory multi-page image compression.
See imwrite for the list of supported formats and flags description.

**Parameters**:
* `ext`: File extension that defines the output format. Must include a leading period.
* `imgs`: Vector of images to be written.
* `buf`: Output buffer resized to fit the compressed data.
* `params`: Format-specific parameters. See imwrite and ImwriteFlags.

**Returns**: The returned value.

---
### `Cv2.HaveImageReader`
**Signature**: `bool HaveImageReader(string filename)`

Checks if the specified image file can be decoded by OpenCV.

**Detailed Remarks**:
The function haveImageReader checks if OpenCV is capable of reading the specified file.
This can be useful for verifying support for a given image format before attempting to load an image.
.: info Note
The function checks the availability of image codecs that are either built into OpenCV or dynamically loaded.
It does not load the image codec implementation and decode data, but uses signature check.
If the file cannot be opened or the format is unsupported, the function will return false.
**See also**: haveImageWriter, imread, imdecode
.:

**Parameters**:
* `filename`: The name of the file to be checked.

**Returns**: true if an image reader for the specified file is available and the file can be opened, false otherwise.

---
### `Cv2.HaveImageWriter`
**Signature**: `bool HaveImageWriter(string filename)`

Checks if the specified image file or specified file extension can be encoded by OpenCV.

**Detailed Remarks**:
The function haveImageWriter checks if OpenCV is capable of writing images with the specified file extension.
This can be useful for verifying support for a given image format before attempting to save an image.
.: info Note
The function checks the availability of image codecs that are either built into OpenCV or dynamically loaded.
It does not check for the actual existence of the file but rather the ability to write files of the given type.
**See also**: haveImageReader, imwrite, imencode
.:

**Parameters**:
* `filename`: The name of the file or the file extension (e.g., ".jpg", ".png"). It is recommended to provide the file extension rather than the full file name.

**Returns**: true if an image writer for the specified extension is available, false otherwise.

---
## 🔢 Enumerations

### `ImageMetadataType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Unknown`** | `-1` | Unknown |
| **`Exif`** | `0` | Exif |
| **`Xmp`** | `1` | Xmp |
| **`Iccp`** | `2` | Iccp |
| **`Cicp`** | `3` | Cicp |
| **`Max`** | `3` | Max |

---
### `ImreadModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Unchanged`** | `-1` | Unchanged |
| **`Grayscale`** | `0` | Grayscale |
| **`ColorBgr`** | `1` | ColorBgr |
| **`Color`** | `1` | Color |
| **`Anydepth`** | `2` | Anydepth |
| **`Anycolor`** | `4` | Anycolor |
| **`LoadGdal`** | `8` | LoadGdal |
| **`ReducedGrayscale2`** | `16` | ReducedGrayscale2 |
| **`ReducedColor2`** | `17` | ReducedColor2 |
| **`ReducedGrayscale4`** | `32` | ReducedGrayscale4 |
| **`ReducedColor4`** | `33` | ReducedColor4 |
| **`ReducedGrayscale8`** | `64` | ReducedGrayscale8 |
| **`ReducedColor8`** | `65` | ReducedColor8 |
| **`IgnoreOrientation`** | `128` | IgnoreOrientation |
| **`ColorRgb`** | `256` | ColorRgb |

---
### `ImwriteBMPCompressionFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Rgb`** | `0` | Rgb |
| **`Bitfields`** | `3` | Bitfields |

---
### `ImwriteEXRCompressionFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`No`** | `0` | No |
| **`Rle`** | `1` | Rle |
| **`Zips`** | `2` | Zips |
| **`Zip`** | `3` | Zip |
| **`Piz`** | `4` | Piz |
| **`Pxr24`** | `5` | Pxr24 |
| **`B44`** | `6` | B44 |
| **`B44a`** | `7` | B44a |
| **`Dwaa`** | `8` | Dwaa |
| **`Dwab`** | `9` | Dwab |

---
### `ImwriteEXRTypeFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Half`** | `1` | Half |
| **`Float`** | `2` | Float |

---
### `ImwriteFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`JpegQuality`** | `1` | JpegQuality |
| **`JpegProgressive`** | `2` | JpegProgressive |
| **`JpegOptimize`** | `3` | JpegOptimize |
| **`JpegRstInterval`** | `4` | JpegRstInterval |
| **`JpegLumaQuality`** | `5` | JpegLumaQuality |
| **`JpegChromaQuality`** | `6` | JpegChromaQuality |
| **`JpegSamplingFactor`** | `7` | JpegSamplingFactor |
| **`PngCompression`** | `16` | PngCompression |
| **`PngStrategy`** | `17` | PngStrategy |
| **`PngBilevel`** | `18` | PngBilevel |
| **`PngFilter`** | `19` | PngFilter |
| **`PngZlibbufferSize`** | `20` | PngZlibbufferSize |
| **`PxmBinary`** | `32` | PxmBinary |
| **`ExrType`** | `unchecked((int)(3 << 4 + 0))` | ExrType |
| **`ExrCompression`** | `unchecked((int)(3 << 4 + 1))` | ExrCompression |
| **`ExrDwaCompressionLevel`** | `unchecked((int)(3 << 4 + 2))` | ExrDwaCompressionLevel |
| **`WebpQuality`** | `64` | WebpQuality |
| **`WebpLosslessMode`** | `65` | WebpLosslessMode |
| **`HdrCompression`** | `unchecked((int)(5 << 4 + 0))` | HdrCompression |
| **`PamTupletype`** | `128` | PamTupletype |
| **`TiffResunit`** | `256` | TiffResunit |
| **`TiffXdpi`** | `257` | TiffXdpi |
| **`TiffYdpi`** | `258` | TiffYdpi |
| **`TiffCompression`** | `259` | TiffCompression |
| **`TiffRowsperstrip`** | `278` | TiffRowsperstrip |
| **`TiffPredictor`** | `317` | TiffPredictor |
| **`Jpeg2000CompressionX1000`** | `272` | Jpeg2000CompressionX1000 |
| **`AvifQuality`** | `512` | AvifQuality |
| **`AvifDepth`** | `513` | AvifDepth |
| **`AvifSpeed`** | `514` | AvifSpeed |
| **`JpegxlQuality`** | `640` | JpegxlQuality |
| **`JpegxlEffort`** | `641` | JpegxlEffort |
| **`JpegxlDistance`** | `642` | JpegxlDistance |
| **`JpegxlDecodingSpeed`** | `643` | JpegxlDecodingSpeed |
| **`BmpCompression`** | `768` | BmpCompression |
| **`GifLoop`** | `1024` | GifLoop |
| **`GifSpeed`** | `1025` | GifSpeed |
| **`GifQuality`** | `1026` | GifQuality |
| **`GifDither`** | `1027` | GifDither |
| **`GifTransparency`** | `1028` | GifTransparency |
| **`GifColortable`** | `1029` | GifColortable |

---
### `ImwriteGIFCompressionFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FastNoDither`** | `1` | FastNoDither |
| **`FastFloydDither`** | `2` | FastFloydDither |
| **`ColortableSize8`** | `3` | ColortableSize8 |
| **`ColortableSize16`** | `4` | ColortableSize16 |
| **`ColortableSize32`** | `5` | ColortableSize32 |
| **`ColortableSize64`** | `6` | ColortableSize64 |
| **`ColortableSize128`** | `7` | ColortableSize128 |
| **`ColortableSize256`** | `8` | ColortableSize256 |

---
### `ImwriteHDRCompressionFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Rle`** | `1` | Rle |

---
### `ImwriteJPEGSamplingFactorParams`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_411`** | `unchecked((int)(0x411111))` | _411 |
| **`_420`** | `unchecked((int)(0x221111))` | _420 |
| **`_422`** | `unchecked((int)(0x211111))` | _422 |
| **`_440`** | `unchecked((int)(0x121111))` | _440 |
| **`_444`** | `unchecked((int)(0x111111))` | _444 |

---
### `ImwritePAMFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Null`** | `0` | Null |
| **`Blackandwhite`** | `1` | Blackandwhite |
| **`Grayscale`** | `2` | Grayscale |
| **`GrayscaleAlpha`** | `3` | GrayscaleAlpha |
| **`Rgb`** | `4` | Rgb |
| **`RgbAlpha`** | `5` | RgbAlpha |

---
### `ImwritePNGFilterFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FilterNone`** | `8` | FilterNone |
| **`FilterSub`** | `16` | FilterSub |
| **`FilterUp`** | `32` | FilterUp |
| **`FilterAvg`** | `64` | FilterAvg |
| **`FilterPaeth`** | `128` | FilterPaeth |
| **`FastFilters`** | `unchecked((int)(FilterNone | FilterSub | FilterUp))` | FastFilters |
| **`AllFilters`** | `unchecked((int)(FastFilters | FilterAvg | FilterPaeth))` | AllFilters |

---
### `ImwritePNGFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `0` | Default |
| **`Filtered`** | `1` | Filtered |
| **`HuffmanOnly`** | `2` | HuffmanOnly |
| **`Rle`** | `3` | Rle |
| **`Fixed`** | `4` | Fixed |

---
### `ImwriteTiffCompressionFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `1` | None |
| **`Ccittrle`** | `2` | Ccittrle |
| **`Ccittfax3`** | `3` | Ccittfax3 |
| **`CcittT4`** | `3` | CcittT4 |
| **`Ccittfax4`** | `4` | Ccittfax4 |
| **`CcittT6`** | `4` | CcittT6 |
| **`Lzw`** | `5` | Lzw |
| **`Ojpeg`** | `6` | Ojpeg |
| **`Jpeg`** | `7` | Jpeg |
| **`T85`** | `9` | T85 |
| **`T43`** | `10` | T43 |
| **`Next`** | `32766` | Next |
| **`Ccittrlew`** | `32771` | Ccittrlew |
| **`Packbits`** | `32773` | Packbits |
| **`Thunderscan`** | `32809` | Thunderscan |
| **`It8ctpad`** | `32895` | It8ctpad |
| **`It8lw`** | `32896` | It8lw |
| **`It8mp`** | `32897` | It8mp |
| **`It8bl`** | `32898` | It8bl |
| **`Pixarfilm`** | `32908` | Pixarfilm |
| **`Pixarlog`** | `32909` | Pixarlog |
| **`Deflate`** | `32946` | Deflate |
| **`AdobeDeflate`** | `8` | AdobeDeflate |
| **`Dcs`** | `32947` | Dcs |
| **`Jbig`** | `34661` | Jbig |
| **`Sgilog`** | `34676` | Sgilog |
| **`Sgilog24`** | `34677` | Sgilog24 |
| **`Jp2000`** | `34712` | Jp2000 |
| **`Lerc`** | `34887` | Lerc |
| **`Lzma`** | `34925` | Lzma |
| **`Zstd`** | `50000` | Zstd |
| **`Webp`** | `50001` | Webp |
| **`Jxl`** | `50002` | Jxl |

---
### `ImwriteTiffPredictorFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `1` | None |
| **`Horizontal`** | `2` | Horizontal |
| **`Floatingpoint`** | `3` | Floatingpoint |

---
### `ImwriteTiffResolutionUnitFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `1` | None |
| **`Inch`** | `2` | Inch |
| **`Centimeter`** | `3` | Centimeter |

---
### `ImwriteWEBPLosslessMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Off`** | `0` | Off |
| **`On`** | `1` | On |
| **`PreserveColor`** | `2` | PreserveColor |

---

</div>