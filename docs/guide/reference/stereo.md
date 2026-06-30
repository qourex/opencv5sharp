# STEREO Module API Reference

Complete documentation for the **STEREO** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Stereo Documentation](https://docs.opencv.org/5.x/main_modules/stereo.html).

---
<div v-pre>

## 📦 Classes and Structs

### `StereoBM`
**Inherits from**: `StereoMatcher`

Class for computing stereo correspondence using the block matching algorithm, introduced and contributed to OpenCV by K. Konolige.

#### Methods
* `int GetPreFilterType()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPreFilterType(int preFilterType)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `preFilterType`: The preFilterType parameter.
* `int GetPreFilterSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPreFilterSize(int preFilterSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `preFilterSize`: The preFilterSize parameter.
* `int GetPreFilterCap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPreFilterCap(int preFilterCap)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `preFilterCap`: The preFilterCap parameter.
* `int GetTextureThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetTextureThreshold(int textureThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `textureThreshold`: The textureThreshold parameter.
* `int GetUniquenessRatio()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetUniquenessRatio(int uniquenessRatio)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `uniquenessRatio`: The uniquenessRatio parameter.
* `int GetSmallerBlockSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSmallerBlockSize(int blockSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `blockSize`: The blockSize parameter.
* `Rect GetROI1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetROI1(Rect roi1)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `roi1`: The roi1 parameter.
* `Rect GetROI2()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetROI2(Rect roi2)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `roi2`: The roi2 parameter.
* `StereoBM? Create(int numDisparities, int blockSize)`
  * *Summary*: Creates StereoBM object
  * *Parameter* `numDisparities`: the disparity search range. For each pixel algorithm will find the best disparity from 0 (default minimum disparity) to numDisparities. The search range can then be shifted by changing the minimum disparity.
  * *Parameter* `blockSize`: the linear size of the blocks compared by the algorithm. The size should be odd (as the block is centered at the current pixel). Larger block size implies smoother, though less accurate disparity map. Smaller block size gives more detailed disparity map, but there is higher chance for algorithm to find a wrong correspondence. The function create StereoBM object. You can then call StereoBM.compute() to compute disparity for a specific stereo pair.
  * *Returns*: The returned value.

---
### `StereoMatcher`
**Inherits from**: `Algorithm`

The base class for stereo correspondence algorithms.

#### Methods
* `void Compute(Mat left, Mat right, Mat disparity)`
  * *Summary*: Computes disparity map for the specified stereo pair
  * *Parameter* `left`: Left 8-bit single-channel image.
  * *Parameter* `right`: Right image of the same size and the same type as the left one.
  * *Parameter* `disparity`: Output disparity map. It has the same size as the input images. Some algorithms, like StereoBM or StereoSGBM compute 16-bit fixed-point disparity map (where each disparity value has 4 fractional bits), whereas other algorithms output 32-bit floating-point disparity map.
* `int GetMinDisparity()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinDisparity(int minDisparity)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minDisparity`: The minDisparity parameter.
* `int GetNumDisparities()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNumDisparities(int numDisparities)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `numDisparities`: The numDisparities parameter.
* `int GetBlockSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetBlockSize(int blockSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `blockSize`: The blockSize parameter.
* `int GetSpeckleWindowSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSpeckleWindowSize(int speckleWindowSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `speckleWindowSize`: The speckleWindowSize parameter.
* `int GetSpeckleRange()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSpeckleRange(int speckleRange)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `speckleRange`: The speckleRange parameter.
* `int GetDisp12MaxDiff()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetDisp12MaxDiff(int disp12MaxDiff)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `disp12MaxDiff`: The disp12MaxDiff parameter.

---
### `StereoSGBM`
**Inherits from**: `StereoMatcher`

The class implements the modified H. Hirschmuller algorithm [HH08] that differs from the original one as follows:

**Detailed Remarks**:
-   By default, the algorithm is single-pass, which means that you consider only 5 directions
instead of 8. Set mode=StereoSGBM.MODE_HH in createStereoSGBM to run the full variant of the
algorithm but beware that it may consume a lot of memory.
-   The algorithm matches blocks, not individual pixels. Though, setting blockSize=1 reduces the
blocks to single pixels.
-   Mutual information cost function is not implemented. Instead, a simpler Birchfield-Tomasi
sub-pixel metric from **Citation**:  BT98 is used. Though, the color images are supported as well.
-   Some pre- and post- processing steps from K. Konolige algorithm StereoBM are included, for
example: pre-filtering (StereoBM.PREFILTER_XSOBEL type) and post-filtering (uniqueness
check, quadratic interpolation and speckle filtering).

#### Methods
* `int GetPreFilterCap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPreFilterCap(int preFilterCap)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `preFilterCap`: The preFilterCap parameter.
* `int GetUniquenessRatio()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetUniquenessRatio(int uniquenessRatio)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `uniquenessRatio`: The uniquenessRatio parameter.
* `int GetP1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetP1(int P1)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `P1`: The P1 parameter.
* `int GetP2()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetP2(int P2)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `P2`: The P2 parameter.
* `int GetMode()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMode(int mode)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `mode`: The mode parameter.
* `StereoSGBM? Create(int minDisparity, int numDisparities, int blockSize, int P1, int P2, int disp12MaxDiff, int preFilterCap, int uniquenessRatio, int speckleWindowSize, int speckleRange, int mode)`
  * *Summary*: Creates StereoSGBM object
  * *Parameter* `minDisparity`: Minimum possible disparity value. Normally, it is zero but sometimes rectification algorithms can shift images, so this parameter needs to be adjusted accordingly.
  * *Parameter* `numDisparities`: Maximum disparity minus minimum disparity. The value is always greater than zero. In the current implementation, this parameter must be divisible by 16.
  * *Parameter* `blockSize`: Matched block size. It must be an odd number \>=1 . Normally, it should be somewhere in the 3..11 range.
  * *Parameter* `P1`: The first parameter controlling the disparity smoothness. See below.
  * *Parameter* `P2`: The second parameter controlling the disparity smoothness. The larger the values are, the smoother the disparity is. P1 is the penalty on the disparity change by plus or minus 1 between neighbor pixels. P2 is the penalty on the disparity change by more than 1 between neighbor pixels. The algorithm requires P2 \> P1 . See stereo_match.cpp sample where some reasonably good P1 and P2 values are shown (like 8\*number_of_image_channels\*blockSize\*blockSize and 32\*number_of_image_channels\*blockSize\*blockSize , respectively).
  * *Parameter* `disp12MaxDiff`: Maximum allowed difference (in integer pixel units) in the left-right disparity check. Set it to a non-positive value to disable the check.
  * *Parameter* `preFilterCap`: Truncation value for the prefiltered image pixels. The algorithm first computes x-derivative at each pixel and clips its value by [-preFilterCap, preFilterCap] interval. The result values are passed to the Birchfield-Tomasi pixel cost function.
  * *Parameter* `uniquenessRatio`: Margin in percentage by which the best (minimum) computed cost function value should "win" the second best value to consider the found match correct. Normally, a value within the 5-15 range is good enough.
  * *Parameter* `speckleWindowSize`: Maximum size of smooth disparity regions to consider their noise speckles and invalidate. Set it to 0 to disable speckle filtering. Otherwise, set it somewhere in the 50-200 range.
  * *Parameter* `speckleRange`: Maximum disparity variation within each connected component. If you do speckle filtering, set the parameter to a positive value, it will be implicitly multiplied by 16. Normally, 1 or 2 is good enough.
  * *Parameter* `mode`: Set it to StereoSGBM.MODE_HH to run the full-scale two-pass dynamic programming algorithm. It will consume O(W\*H\*numDisparities) bytes, which is large for 640x480 stereo and huge for HD-size pictures. By default, it is set to false . The first constructor initializes StereoSGBM with all the default parameters. So, you only have to set StereoSGBM.numDisparities at minimum. The second constructor enables you to set each parameter to a custom value.
  * *Returns*: The returned value.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.StereoRectify`
**Signature**: `void StereoRectify(Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Size imageSize, Mat R, Mat T, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, double alpha, Size newImageSize, IntPtr validPixROI1, IntPtr validPixROI2)`

Computes rectification transforms for each head of a calibrated stereo camera.

**Parameters**:
* `cameraMatrix1`: First camera intrinsic matrix.
* `distCoeffs1`: First camera distortion parameters.
* `cameraMatrix2`: Second camera intrinsic matrix.
* `distCoeffs2`: Second camera distortion parameters.
* `imageSize`: Size of the image used for stereo calibration.
* `R`: Rotation matrix from the coordinate system of the first camera to the second camera, see `stereoCalibrate`.
* `T`: Translation vector from the coordinate system of the first camera to the second camera, see `stereoCalibrate`.
* `R1`: Output 3x3 rectification transform (rotation matrix) for the first camera. This matrix brings points given in the unrectified first camera's coordinate system to points in the rectified first camera's coordinate system. In more technical terms, it performs a change of basis from the unrectified first camera's coordinate system to the rectified first camera's coordinate system.
* `R2`: Output 3x3 rectification transform (rotation matrix) for the second camera. This matrix brings points given in the unrectified second camera's coordinate system to points in the rectified second camera's coordinate system. In more technical terms, it performs a change of basis from the unrectified second camera's coordinate system to the rectified second camera's coordinate system.
* `P1`: Output 3x4 projection matrix in the new (rectified) coordinate systems for the first camera, i.e. it projects points given in the rectified first camera coordinate system into the rectified first camera's image.
* `P2`: Output 3x4 projection matrix in the new (rectified) coordinate systems for the second camera, i.e. it projects points given in the rectified first camera coordinate system into the rectified second camera's image.
* `Q`: Output formula disparity-to-depth mapping matrix (see `reprojectImageTo3D`).
* `flags`: Operation flags that may be zero or `STEREO_ZERO_DISPARITY` . If the flag is set, the function makes the principal points of each camera have the same pixel coordinates in the rectified views. And if the flag is not set, the function may still shift the images in the horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the useful image area.
* `alpha`: Free scaling parameter. If it is -1 or absent, the function performs the default scaling. Otherwise, the parameter should be between 0 and 1. alpha=0 means that the rectified images are zoomed and shifted so that only valid pixels are visible (no black areas after rectification). alpha=1 means that the rectified image is decimated and shifted so that all the pixels from the original images from the cameras are retained in the rectified images (no source image pixels are lost). Any intermediate value yields an intermediate result between those two extreme cases.
* `newImageSize`: New image resolution after rectification. The same size should be passed to `initUndistortRectifyMap` (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0) is passed (default), it is set to the original imageSize . Setting it to a larger value can help you preserve details in the original image, especially when there is a big radial distortion.
* `validPixROI1`: Optional output rectangles inside the rectified images where all the pixels are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller (see the picture below).
* `validPixROI2`: Optional output rectangles inside the rectified images where all the pixels are valid. If alpha=0 , the ROIs cover the whole images. Otherwise, they are likely to be smaller (see the picture below). The function computes the rotation matrices for each camera that (virtually) make both camera image planes the same plane. Consequently, this makes all the epipolar lines parallel and thus simplifies the dense stereo correspondence problem. The function takes the matrices computed by `stereoCalibrate` as input. As output, it provides two rotation matrices and also two projection matrices in the new coordinates. The function distinguishes the following two cases: -   **Horizontal stereo**: the first and the second camera views are shifted relative to each other mainly along the x-axis (with possible small vertical shift). In the rectified images, the corresponding epipolar lines in the left and right cameras are horizontal and have the same y-coordinate. P1 and P2 look like: [see mathematical formula in OpenCV documentation] [see mathematical formula in OpenCV documentation] [see mathematical formula in OpenCV documentation] where formula is a horizontal shift between the cameras and formula if `STEREO_ZERO_DISPARITY` is set. -   **Vertical stereo**: the first and the second camera views are shifted relative to each other mainly in the vertical direction (and probably a bit in the horizontal direction too). The epipolar lines in the rectified images are vertical and have the same x-coordinate. P1 and P2 look like: [see mathematical formula in OpenCV documentation] [see mathematical formula in OpenCV documentation] [see mathematical formula in OpenCV documentation] where formula is a vertical shift between the cameras and formula if `STEREO_ZERO_DISPARITY` is set. As you can see, the first three columns of P1 and P2 will effectively be the new "rectified" camera matrices. The matrices, together with R1 and R2 , can then be passed to `initUndistortRectifyMap` to initialize the rectification map for each camera. See below the screenshot from the stereo_calib.cpp sample. Some red horizontal lines pass through the corresponding image regions. This means that the images are well rectified, which is what most stereo correspondence algorithms rely on. The green rectangles are roi1 and roi2 . You see that their interiors are all valid pixels.

---
### `Cv2.StereoRectifyUncalibrated`
**Signature**: `bool StereoRectifyUncalibrated(Mat points1, Mat points2, Mat F, Size imgSize, Mat H1, Mat H2, double threshold)`

Computes a rectification transform for an uncalibrated stereo camera.

**Detailed Remarks**:
.: info Note

While the algorithm does not need to know the intrinsic parameters of the cameras, it heavily
depends on the epipolar geometry. Therefore, if the camera lenses have a significant distortion,
it would be better to correct it before computing the fundamental matrix and calling this
function. For example, distortion coefficients can be estimated for each head of stereo camera
separately by using `CalibrateCamera` . Then, the images can be corrected using `undistort` , or
just the point coordinates can be corrected with `undistortPoints` .
.:

**Parameters**:
* `points1`: Array of feature points in the first image.
* `points2`: The corresponding points in the second image. The same formats as in `findFundamentalMat` are supported.
* `F`: Input fundamental matrix. It can be computed from the same set of point pairs using `findFundamentalMat` .
* `imgSize`: Size of the image.
* `H1`: Output rectification homography matrix for the first image.
* `H2`: Output rectification homography matrix for the second image.
* `threshold`: Optional threshold used to filter out the outliers. If the parameter is greater than zero, all the point pairs that do not comply with the epipolar geometry (that is, the points for which formula ) are rejected prior to computing the homographies. Otherwise, all the points are considered inliers. The function computes the rectification transformations without knowing intrinsic parameters of the cameras and their relative position in the space, which explains the suffix "uncalibrated". Another related difference from `stereoRectify` is that the function outputs not the rectification transformations in the object (3D) space, but the planar perspective transformations encoded by the homography matrices H1 and H2 . The function implements the algorithm [Hartley99] .

**Returns**: The returned value.

---
### `Cv2.FisheyeStereoRectify`
**Signature**: `void FisheyeStereoRectify(Mat K1, Mat D1, Mat K2, Mat D2, Size imageSize, Mat R, Mat tvec, Mat R1, Mat R2, Mat P1, Mat P2, Mat Q, int flags, Size newImageSize, double balance, double fov_scale)`

Stereo rectification for fisheye camera model

**Parameters**:
* `K1`: First camera intrinsic matrix.
* `D1`: First camera distortion parameters.
* `K2`: Second camera intrinsic matrix.
* `D2`: Second camera distortion parameters.
* `imageSize`: Size of the image used for stereo calibration.
* `R`: Rotation matrix between the coordinate systems of the first and the second cameras.
* `tvec`: Translation vector between coordinate systems of the cameras.
* `R1`: Output 3x3 rectification transform (rotation matrix) for the first camera.
* `R2`: Output 3x3 rectification transform (rotation matrix) for the second camera.
* `P1`: Output 3x4 projection matrix in the new (rectified) coordinate systems for the first camera.
* `P2`: Output 3x4 projection matrix in the new (rectified) coordinate systems for the second camera.
* `Q`: Output formula disparity-to-depth mapping matrix (see reprojectImageTo3D ).
* `flags`: Operation flags that may be zero or `CALIB_ZERO_DISPARITY` . If the flag is set, the function makes the principal points of each camera have the same pixel coordinates in the rectified views. And if the flag is not set, the function may still shift the images in the horizontal or vertical direction (depending on the orientation of epipolar lines) to maximize the useful image area.
* `newImageSize`: New image resolution after rectification. The same size should be passed to `initUndistortRectifyMap` (see the stereo_calib.cpp sample in OpenCV samples directory). When (0,0) is passed (default), it is set to the original imageSize . Setting it to larger value can help you preserve details in the original image, especially when there is a big radial distortion.
* `balance`: Sets the new focal length in range between the min focal length and the max focal length. Balance is in range of [0, 1].
* `fov_scale`: Divisor for new focal length.

---
### `Cv2.FilterSpeckles`
**Signature**: `void FilterSpeckles(Mat img, double newVal, int maxSpeckleSize, double maxDiff, Mat? buf)`

Filters off small noise blobs (speckles) in the disparity map

**Parameters**:
* `img`: The input 16-bit signed disparity image
* `newVal`: The disparity value used to paint-off the speckles
* `maxSpeckleSize`: The maximum speckle size to consider it a speckle. Larger blobs are not affected by the algorithm
* `maxDiff`: Maximum difference between neighbor disparity pixels to put them into the same blob. Note that since StereoBM, StereoSGBM and may be other algorithms return a fixed-point disparity map, where disparity values are multiplied by 16, this scale factor should be taken into account when specifying this parameter value.
* `buf`: The optional temporary buffer to avoid memory allocation within the function.

---
### `Cv2.GetValidDisparityROI`
**Signature**: `Rect GetValidDisparityROI(Rect roi1, Rect roi2, int minDisparity, int numberOfDisparities, int blockSize)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `roi1`: The roi1 parameter.
* `roi2`: The roi2 parameter.
* `minDisparity`: The minDisparity parameter.
* `numberOfDisparities`: The numberOfDisparities parameter.
* `blockSize`: The blockSize parameter.

**Returns**: The returned value.

---
### `Cv2.ValidateDisparity`
**Signature**: `void ValidateDisparity(Mat disparity, Mat cost, int minDisparity, int numberOfDisparities, int disp12MaxDisp)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `disparity`: The disparity parameter.
* `cost`: The cost parameter.
* `minDisparity`: The minDisparity parameter.
* `numberOfDisparities`: The numberOfDisparities parameter.
* `disp12MaxDisp`: The disp12MaxDisp parameter.

---
### `Cv2.ReprojectImageTo3D`
**Signature**: `void ReprojectImageTo3D(Mat disparity, Mat _3dImage, Mat Q, bool handleMissingValues, int ddepth)`

Reprojects a disparity image to 3D space.

**Detailed Remarks**:
**See also**: 
To reproject a sparse set of points {(x,y,d),...} to 3D space, use perspectiveTransform.

**Parameters**:
* `disparity`: Input single-channel 8-bit unsigned, 16-bit signed, 32-bit signed or 32-bit floating-point disparity image. The values of 8-bit / 16-bit signed formats are assumed to have no fractional bits. If the disparity is 16-bit signed format, as computed by `StereoBM` or `StereoSGBM` and maybe other algorithms, it should be divided by 16 (and scaled to float) before being used here.
* `_3dImage`: Output 3-channel floating-point image of the same size as disparity. Each element of _3dImage(x,y) contains 3D coordinates of the point (x,y) computed from the disparity map. If one uses Q obtained by `stereoRectify`, then the returned points are represented in the first camera's rectified coordinate system.
* `Q`: formula perspective transformation matrix that can be obtained with `stereoRectify`.
* `handleMissingValues`: Indicates, whether the function should handle missing values (i.e. points where the disparity was not computed). If handleMissingValues=true, then pixels with the minimal disparity that corresponds to the outliers (see StereoMatcher.compute ) are transformed to 3D points with a very large Z value (currently set to 10000).
* `ddepth`: The optional output array depth. If it is -1, the output image will have CV_32F depth. ddepth can also be set to CV_16S, CV_32S or CV_32F. The function transforms a single-channel disparity map to a 3-channel image representing a 3D surface. That is, for each pixel (x,y) and the corresponding disparity d=disparity(x,y) , it computes: [see mathematical formula in OpenCV documentation]

---
## 🔢 Enumerations

### `UnnamedEnum8StereoBM`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`NormalizedResponse`** | `0` | NormalizedResponse |
| **`Xsobel`** | `1` | Xsobel |

---
### `UnnamedEnum9StereoMatcher`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Shift`** | `4` | Shift |
| **`Scale`** | `unchecked((int)(1 << Shift))` | Scale |

---
### `UnnamedEnum10StereoSGBM`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Sgbm`** | `0` | Sgbm |
| **`Hh`** | `1` | Hh |
| **`Sgbm3way`** | `2` | Sgbm3way |
| **`Hh4`** | `3` | Hh4 |

---

</div>