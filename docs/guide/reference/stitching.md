# STITCHING Module API Reference

Complete documentation for the **STITCHING** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Stitching Documentation](https://docs.opencv.org/5.x/main_modules/stitching.html).

---
<div v-pre>

## 📦 Classes and Structs

### `PyRotationWarper`
**Inherits from**: `DisposableOpenCVObject`

Rotation-based image warper that supports different projection types (e.g., spherical, cylindrical). Wraps OpenCV's `cv::PyRotationWarper`.

#### Constructors
* `new PyRotationWarper(string type, float scale)`
  * *Summary*: Creates a rotation warper with the specified projection type and scale.
  * *Parameter* `type`: Warper projection type name (e.g., "spherical", "cylindrical", "plane").
  * *Parameter* `scale`: Projected image scale (focal length of the camera).
* `new PyRotationWarper()`
  * *Summary*: Creates a default rotation warper with no projection type or scale set.

#### Methods
* `Point2F WarpPoint(Point2F pt, Mat K, Mat R)`
  * *Summary*: Projects the image point.
  * *Parameter* `pt`: Source point
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Returns*: Projected point
* `Point2F WarpPointBackward(Point2F pt, Mat K, Mat R)`
  * *Summary*: Projects the image point backward.
  * *Parameter* `pt`: Projected point
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Returns*: Backward-projected point
* `Rect BuildMaps(Size src_size, Mat K, Mat R, Mat xmap, Mat ymap)`
  * *Summary*: Builds the projection maps according to the given camera data.
  * *Parameter* `src_size`: Source image size
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Parameter* `xmap`: Projection map for the x axis
  * *Parameter* `ymap`: Projection map for the y axis
  * *Returns*: Projected image minimum bounding box
* `Point Warp(Mat src, Mat K, Mat R, int interp_mode, int border_mode, Mat dst)`
  * *Summary*: Projects the image.
  * *Parameter* `src`: Source image
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Parameter* `interp_mode`: Interpolation mode
  * *Parameter* `border_mode`: Border extrapolation mode
  * *Parameter* `dst`: Projected image
  * *Returns*: Project image top-left corner
* `void WarpBackward(Mat src, Mat K, Mat R, int interp_mode, int border_mode, Size dst_size, Mat dst)`
  * *Summary*: Projects the image backward.
  * *Parameter* `src`: Projected image
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Parameter* `interp_mode`: Interpolation mode
  * *Parameter* `border_mode`: Border extrapolation mode
  * *Parameter* `dst_size`: Backward-projected image size
  * *Parameter* `dst`: Backward-projected image
* `Rect WarpRoi(Size src_size, Mat K, Mat R)`
  * *Summary*: Calculates the minimum bounding box of the warped (projected) image region of interest.
  * *Parameter* `src_size`: Source image bounding box
  * *Parameter* `K`: Camera intrinsic parameters
  * *Parameter* `R`: Camera rotation matrix
  * *Returns*: Projected image minimum bounding box
* `float GetScale()`
  * *Summary*: Returns the current projected image scale (focal length).
  * *Returns*: The current scale value.
* `void SetScale(float arg1)`
  * *Summary*: Sets the projected image scale (focal length).
  * *Parameter* `arg1`: The new scale value to set.

---
### `Stitcher`
**Inherits from**: `DisposableOpenCVObject`

High level image stitcher.

**Detailed Remarks**:
It's possible to use this class without being aware of the entire stitching pipeline. However, to
be able to achieve higher stitching stability and quality of the final images at least being
familiar with the theory is recommended.
.: info Note

-   A basic example on image stitching can be found at

-   A detailed example on image stitching can be found at
.:

#### Methods
* `Stitcher? Create(FileStorageMode mode)`
  * *Summary*: Creates a Stitcher configured in one of the stitching modes.
  * *Parameter* `mode`: Scenario for stitcher operation. This is usually determined by source of images to stitch and their transformation. Default parameters will be chosen for operation in given scenario.
  * *Returns*: Stitcher class instance.
* `double RegistrationResol()`
  * *Summary*: Returns the registration resolution in megapixels.
  * *Returns*: The registration resolution in megapixels.
* `void SetRegistrationResol(double resol_mpx)`
  * *Summary*: Sets the registration resolution in megapixels.
  * *Parameter* `resol_mpx`: Resolution in megapixels for the registration step.
* `double SeamEstimationResol()`
  * *Summary*: Returns the seam estimation resolution in megapixels.
  * *Returns*: The seam estimation resolution in megapixels.
* `void SetSeamEstimationResol(double resol_mpx)`
  * *Summary*: Sets the seam estimation resolution in megapixels.
  * *Parameter* `resol_mpx`: Resolution in megapixels for the seam estimation step.
* `double CompositingResol()`
  * *Summary*: Returns the compositing resolution in megapixels.
  * *Returns*: The compositing resolution in megapixels.
* `void SetCompositingResol(double resol_mpx)`
  * *Summary*: Sets the compositing resolution in megapixels.
  * *Parameter* `resol_mpx`: Resolution in megapixels for the compositing step.
* `double PanoConfidenceThresh()`
  * *Summary*: Returns the confidence threshold for panorama result.
  * *Returns*: The panorama confidence threshold.
* `void SetPanoConfidenceThresh(double conf_thresh)`
  * *Summary*: Sets the confidence threshold for panorama result.
  * *Parameter* `conf_thresh`: Confidence threshold value. Images with lower match confidence are rejected.
* `bool WaveCorrection()`
  * *Summary*: Returns whether wave correction is enabled.
  * *Returns*: True if wave correction is enabled, false otherwise.
* `void SetWaveCorrection(bool flag)`
  * *Summary*: Sets whether wave correction should be applied.
  * *Parameter* `flag`: True to enable wave correction, false to disable.
* `InterpolationFlags InterpolationFlags()`
  * *Summary*: Returns the interpolation flags used for image warping.
  * *Returns*: The current interpolation flags.
* `void SetInterpolationFlags(InterpolationFlags interp_flags)`
  * *Summary*: Sets the interpolation flags used for image warping.
  * *Parameter* `interp_flags`: Interpolation flags to use during image warping.
* `StitcherStatus EstimateTransform(IntPtr images, IntPtr masks)`
  * *Summary*: These functions try to match the given images and to estimate rotations of each camera.
  * *Remarks*:

.: info Note
Use the functions only if you're aware of the stitching pipeline, otherwise use
Stitcher.stitch.
.:

  * *Parameter* `images`: Input images.
  * *Parameter* `masks`: Masks for each input image specifying where to look for keypoints (optional).
  * *Returns*: Status code.
* `StitcherStatus ComposePanorama(Mat pano)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `pano`: The pano parameter.
  * *Returns*: The returned value.
* `StitcherStatus ComposePanorama(IntPtr images, Mat pano)`
  * *Summary*: These functions try to compose the given images (or images stored internally from the other function calls) into the final pano under the assumption that the image transformations were estimated before.
  * *Remarks*:

.: info Note
Use the functions only if you're aware of the stitching pipeline, otherwise use
Stitcher.stitch.
.:

  * *Parameter* `images`: Input images.
  * *Parameter* `pano`: Final pano.
  * *Returns*: Status code.
* `StitcherStatus Stitch(IntPtr images, Mat pano)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `images`: The images parameter.
  * *Parameter* `pano`: The pano parameter.
  * *Returns*: The returned value.
* `StitcherStatus Stitch(IntPtr images, IntPtr masks, Mat pano)`
  * *Summary*: These functions try to stitch the given images.
  * *Parameter* `images`: Input images.
  * *Parameter* `masks`: Masks for each input image specifying where to look for keypoints (optional).
  * *Parameter* `pano`: Final pano.
  * *Returns*: Status code.
* `IntPtr Component()`
  * *Summary*: Returns indices of input images used in panorama stitching
  * *Returns*: The returned value.
* `IntPtr Cameras()`
  * *Summary*: Returns estimated camera parameters for all stitched images
  * *Returns*: The returned value.
* `double WorkScale()`
  * *Summary*: Returns the work scale used internally during the stitching pipeline.
  * *Returns*: The internal work scale value.

---
### `WarperCreator`
**Inherits from**: `DisposableOpenCVObject`

Image warper factories base class.

---
### `DetailAffineBasedEstimator`
**Inherits from**: `DetailEstimator`

Affine transformation based estimator.

**Detailed Remarks**:
This estimator uses pairwise transformations estimated by matcher to estimate
final transformation for each camera.
**See also**: Detail.HomographyBasedEstimator

#### Constructors
* `new DetailAffineBasedEstimator()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailAffineBestOf2NearestMatcher`
**Inherits from**: `DetailBestOf2NearestMatcher`

Features matcher similar to Detail.BestOf2NearestMatcher which finds two best matches for each feature and leaves the best one only if the ratio between descriptor distances is greater than the threshold match_conf.

**Detailed Remarks**:
Unlike Detail.BestOf2NearestMatcher this matcher uses affine
transformation (affine transformation estimate will be placed in matches_info).
**See also**: Detail.FeaturesMatcher Detail.BestOf2NearestMatcher

#### Constructors
* `new DetailAffineBestOf2NearestMatcher(bool full_affine, bool try_use_gpu, float match_conf, int num_matches_thresh1)`
  * *Summary*: Constructs a "best of 2 nearest" matcher that expects affine transformation between images
  * *Parameter* `full_affine`: whether to use full affine transformation with 6 degress of freedom or reduced transformation with 4 degrees of freedom using only rotation, translation and uniform scaling
  * *Parameter* `try_use_gpu`: Should try to use GPU or not
  * *Parameter* `match_conf`: Match distances ration threshold
  * *Parameter* `num_matches_thresh1`: Minimum number of matches required for the 2D affine transform estimation used in the inliers classification step

---
### `DetailBestOf2NearestMatcher`
**Inherits from**: `DetailFeaturesMatcher`

Features matcher which finds two best matches for each feature and leaves the best one only if the ratio between descriptor distances is greater than the threshold match_conf

**Detailed Remarks**:
**See also**: Detail.FeaturesMatcher

#### Constructors
* `new DetailBestOf2NearestMatcher(bool try_use_gpu, float match_conf, int num_matches_thresh1, int num_matches_thresh2, double matches_confidence_thresh)`
  * *Summary*: Constructs a "best of 2 nearest" matcher.
  * *Parameter* `try_use_gpu`: Should try to use GPU or not
  * *Parameter* `match_conf`: Match distances ration threshold
  * *Parameter* `num_matches_thresh1`: Minimum number of matches required for the 2D projective transform estimation used in the inliers classification step
  * *Parameter* `num_matches_thresh2`: Minimum number of matches required for the 2D projective transform re-estimation on inliers
  * *Parameter* `matches_confidence_thresh`: Matching confidence threshold to take the match into account. The threshold was determined experimentally and set to 3 by default.

#### Methods
* `void CollectGarbage()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `DetailBestOf2NearestMatcher? Create(bool try_use_gpu, float match_conf, int num_matches_thresh1, int num_matches_thresh2, double matches_confidence_thresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `try_use_gpu`: The try_use_gpu parameter.
  * *Parameter* `match_conf`: The match_conf parameter.
  * *Parameter* `num_matches_thresh1`: The num_matches_thresh1 parameter.
  * *Parameter* `num_matches_thresh2`: The num_matches_thresh2 parameter.
  * *Parameter* `matches_confidence_thresh`: The matches_confidence_thresh parameter.
  * *Returns*: The returned value.

---
### `DetailBestOf2NearestRangeMatcher`
**Inherits from**: `DetailBestOf2NearestMatcher`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new DetailBestOf2NearestRangeMatcher(int range_width, bool try_use_gpu, float match_conf, int num_matches_thresh1, int num_matches_thresh2)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `range_width`: The range_width parameter.
  * *Parameter* `try_use_gpu`: The try_use_gpu parameter.
  * *Parameter* `match_conf`: The match_conf parameter.
  * *Parameter* `num_matches_thresh1`: The num_matches_thresh1 parameter.
  * *Parameter* `num_matches_thresh2`: The num_matches_thresh2 parameter.

---
### `DetailBlender`
**Inherits from**: `DisposableOpenCVObject`

Base class for all blenders.

**Detailed Remarks**:
Simple blender which puts one image over another

#### Methods
* `DetailBlender? CreateDefault(int type, bool try_gpu)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `try_gpu`: The try_gpu parameter.
  * *Returns*: The returned value.
* `void Prepare(IntPtr corners, IntPtr sizes)`
  * *Summary*: Prepares the blender for blending.
  * *Parameter* `corners`: Source images top-left corners
  * *Parameter* `sizes`: Source image sizes
* `void Prepare(Rect dst_roi)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `dst_roi`: The dst_roi parameter.
* `void Feed(Mat img, Mat mask, Point tl)`
  * *Summary*: Processes the image.
  * *Parameter* `img`: Source image
  * *Parameter* `mask`: Source image mask
  * *Parameter* `tl`: Source image top-left corners
* `void Blend(Mat dst, Mat dst_mask)`
  * *Summary*: Blends and returns the final pano.
  * *Parameter* `dst`: Final pano
  * *Parameter* `dst_mask`: Final pano mask

---
### `DetailBlocksChannelsCompensator`
**Inherits from**: `DetailBlocksCompensator`

Exposure compensator which tries to remove exposure related artifacts by adjusting image block on each channel.

#### Constructors
* `new DetailBlocksChannelsCompensator(int bl_width, int bl_height, int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `bl_width`: The bl_width parameter.
  * *Parameter* `bl_height`: The bl_height parameter.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.

---
### `DetailBlocksCompensator`
**Inherits from**: `DetailExposureCompensator`

Exposure compensator which tries to remove exposure related artifacts by adjusting image blocks.

#### Methods
* `void Apply(int index, Point corner, Mat image, Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `index`: The index parameter.
  * *Parameter* `corner`: The corner parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
* `void GetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetNrFeeds(int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.
* `int GetNrFeeds()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSimilarityThreshold(double similarity_threshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `similarity_threshold`: The similarity_threshold parameter.
* `double GetSimilarityThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetBlockSize(int width, int height)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `width`: The width parameter.
  * *Parameter* `height`: The height parameter.
* `void SetBlockSize(Size size)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
* `Size GetBlockSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNrGainsFilteringIterations(int nr_iterations)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_iterations`: The nr_iterations parameter.
* `int GetNrGainsFilteringIterations()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailBlocksGainCompensator`
**Inherits from**: `DetailBlocksCompensator`

Exposure compensator which tries to remove exposure related artifacts by adjusting image block intensities, see [UES01] for details.

#### Constructors
* `new DetailBlocksGainCompensator(int bl_width, int bl_height)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `bl_width`: The bl_width parameter.
  * *Parameter* `bl_height`: The bl_height parameter.
* `new DetailBlocksGainCompensator(int bl_width, int bl_height, int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `bl_width`: The bl_width parameter.
  * *Parameter* `bl_height`: The bl_height parameter.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.

#### Methods
* `void Apply(int index, Point corner, Mat image, Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `index`: The index parameter.
  * *Parameter* `corner`: The corner parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
* `void GetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.

---
### `DetailBundleAdjusterAffine`
**Inherits from**: `DetailBundleAdjusterBase`

Bundle adjuster that expects affine transformation represented in homogeneous coordinates in R for each camera param. Implements camera parameters refinement algorithm which minimizes sum of the reprojection error squares

**Detailed Remarks**:
It estimates all transformation parameters. Refinement mask is ignored.
**See also**: AffineBasedEstimator AffineBestOf2NearestMatcher BundleAdjusterAffinePartial

#### Constructors
* `new DetailBundleAdjusterAffine()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailBundleAdjusterAffinePartial`
**Inherits from**: `DetailBundleAdjusterBase`

Bundle adjuster that expects affine transformation with 4 DOF represented in homogeneous coordinates in R for each camera param. Implements camera parameters refinement algorithm which minimizes sum of the reprojection error squares

**Detailed Remarks**:
It estimates all transformation parameters. Refinement mask is ignored.
**See also**: AffineBasedEstimator AffineBestOf2NearestMatcher BundleAdjusterAffine

#### Constructors
* `new DetailBundleAdjusterAffinePartial()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailBundleAdjusterBase`
**Inherits from**: `DetailEstimator`

Base class for all camera parameters refinement methods.

#### Methods
* `Mat? RefinementMask()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetRefinementMask(Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `mask`: Optional operation mask.
* `double ConfThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetConfThresh(double conf_thresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `conf_thresh`: The conf_thresh parameter.
* `TermCriteria TermCriteria()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetTermCriteria(TermCriteria term_criteria)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `term_criteria`: The term_criteria parameter.

---
### `DetailBundleAdjusterRay`
**Inherits from**: `DetailBundleAdjusterBase`

Implementation of the camera parameters refinement algorithm which minimizes sum of the distances between the rays passing through the camera center and a feature. :

**Detailed Remarks**:
It can estimate focal length. It ignores the refinement mask for now.

#### Constructors
* `new DetailBundleAdjusterRay()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailBundleAdjusterReproj`
**Inherits from**: `DetailBundleAdjusterBase`

Implementation of the camera parameters refinement algorithm which minimizes sum of the reprojection error squares

**Detailed Remarks**:
It can estimate focal length, aspect ratio, principal point.
You can affect only on them via the refinement mask.

#### Constructors
* `new DetailBundleAdjusterReproj()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailCameraParams`
**Inherits from**: `DisposableOpenCVObject`

Describes camera parameters.

**Detailed Remarks**:
.: info Note
Translation is assumed to be zero during the whole stitching pipeline. :
.:

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Focal`** | `double` | Gets or sets the focal property. |
| **`Aspect`** | `double` | Gets or sets the aspect property. |
| **`Ppx`** | `double` | Gets or sets the ppx property. |
| **`Ppy`** | `double` | Gets or sets the ppy property. |
| **`R`** | `Mat?` | Gets or sets the R property. |
| **`T`** | `Mat?` | Gets or sets the t property. |

#### Methods
* `Mat? K()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailChannelsCompensator`
**Inherits from**: `DetailExposureCompensator`

Exposure compensator which tries to remove exposure related artifacts by adjusting image intensities on each channel independently.

#### Constructors
* `new DetailChannelsCompensator(int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.

#### Methods
* `void Apply(int index, Point corner, Mat image, Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `index`: The index parameter.
  * *Parameter* `corner`: The corner parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
* `void GetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetNrFeeds(int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.
* `int GetNrFeeds()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSimilarityThreshold(double similarity_threshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `similarity_threshold`: The similarity_threshold parameter.
* `double GetSimilarityThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailDpSeamFinder`
**Inherits from**: `DetailSeamFinder`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new DetailDpSeamFinder(string costFunc)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `costFunc`: The costFunc parameter.

#### Methods
* `void SetCostFunction(string val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.

---
### `DetailEstimator`
**Inherits from**: `DisposableOpenCVObject`

Rotation estimator base class.

**Detailed Remarks**:
It takes features of all images, pairwise matches between all images and estimates rotations of all
cameras.
.: info Note
The coordinate system origin is implementation-dependent, but you can always normalize the
rotations in respect to the first camera, for instance. :
.:

#### Methods
* `bool Operator(IntPtr features, IntPtr pairwise_matches, IntPtr cameras)`
  * *Summary*: Estimates camera parameters.
  * *Parameter* `features`: Features of images
  * *Parameter* `pairwise_matches`: Pairwise matches of images
  * *Parameter* `cameras`: Estimated camera parameters
  * *Returns*: True in case of success, false otherwise

---
### `DetailExposureCompensator`
**Inherits from**: `DisposableOpenCVObject`

Base class for all exposure compensators.

#### Methods
* `DetailExposureCompensator? CreateDefault(int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `type`: The type parameter.
  * *Returns*: The returned value.
* `void Feed(IntPtr corners, IntPtr images, IntPtr masks)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `corners`: Source image top-left corners
  * *Parameter* `images`: Source images
  * *Parameter* `masks`: Image masks to update (second value in pair specifies the value which should be used to detect where image is)
* `void Apply(int index, Point corner, Mat image, Mat mask)`
  * *Summary*: Compensate exposure in the specified image.
  * *Parameter* `index`: Image index
  * *Parameter* `corner`: Image top-left corner
  * *Parameter* `image`: Image to process
  * *Parameter* `mask`: Image mask
* `void GetMatGains(IntPtr arg1)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arg1`: The arg1 parameter.
* `void SetMatGains(IntPtr arg1)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arg1`: The arg1 parameter.
* `void SetUpdateGain(bool b)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `b`: The b parameter.
* `bool GetUpdateGain()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailFeatherBlender`
**Inherits from**: `DetailBlender`

Simple blender which mixes images at its borders.

#### Constructors
* `new DetailFeatherBlender(float sharpness)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `sharpness`: The sharpness parameter.

#### Methods
* `float Sharpness()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSharpness(float val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `void Prepare(Rect dst_roi)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst_roi`: The dst_roi parameter.
* `void Feed(Mat img, Mat mask, Point tl)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `img`: Input image.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `tl`: The tl parameter.
* `void Blend(Mat dst, Mat dst_mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `dst_mask`: The dst_mask parameter.
* `Rect CreateWeightMaps(IntPtr masks, IntPtr corners, IntPtr weight_maps)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `masks`: The masks parameter.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `weight_maps`: The weight_maps parameter.
  * *Returns*: The returned value.

---
### `DetailFeaturesMatcher`
**Inherits from**: `DisposableOpenCVObject`

Feature matchers base class.

#### Methods
* `void Operator(DetailImageFeatures features1, DetailImageFeatures features2, DetailMatchesInfo matches_info)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `features1`: First image features
  * *Parameter* `features2`: Second image features
  * *Parameter* `matches_info`: Found matches
* `void Operator(IntPtr features, IntPtr pairwise_matches, IntPtr mask)`
  * *Summary*: Performs images matching.
  * *Remarks*:

**See also**: Detail.MatchesInfo

  * *Parameter* `features`: Features of the source images
  * *Parameter* `pairwise_matches`: Found pairwise matches
  * *Parameter* `mask`: Mask indicating which image pairs must be matched The function is parallelized with the TBB library.
* `bool IsThreadSafe()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: True, if it's possible to use the same matcher instance in parallel, false otherwise
* `void CollectGarbage()`
  * *Summary*: Frees unused memory allocated before if there is any.

---
### `DetailGainCompensator`
**Inherits from**: `DetailExposureCompensator`

Exposure compensator which tries to remove exposure related artifacts by adjusting image intensities, see [BL07] and [WJ10] for details.

#### Constructors
* `new DetailGainCompensator()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new DetailGainCompensator(int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.

#### Methods
* `void Apply(int index, Point corner, Mat image, Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `index`: The index parameter.
  * *Parameter* `corner`: The corner parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
* `void GetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetNrFeeds(int nr_feeds)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nr_feeds`: The nr_feeds parameter.
* `int GetNrFeeds()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSimilarityThreshold(double similarity_threshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `similarity_threshold`: The similarity_threshold parameter.
* `double GetSimilarityThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailGraphCutSeamFinder`
**Inherits from**: `DisposableOpenCVObject`

Minimum graph cut-based seam estimator. See details in [Kwatra03] .

#### Constructors
* `new DetailGraphCutSeamFinder(string cost_type, float terminal_cost, float bad_region_penalty)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cost_type`: The cost_type parameter.
  * *Parameter* `terminal_cost`: The terminal_cost parameter.
  * *Parameter* `bad_region_penalty`: The bad_region_penalty parameter.

#### Methods
* `void Find(IntPtr src, IntPtr corners, IntPtr masks)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `src`: Source matrix or image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `masks`: The masks parameter.

---
### `DetailHomographyBasedEstimator`
**Inherits from**: `DetailEstimator`

Homography based rotation estimator.

#### Constructors
* `new DetailHomographyBasedEstimator(bool is_focals_estimated)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `is_focals_estimated`: The is_focals_estimated parameter.

---
### `DetailImageFeatures`
**Inherits from**: `DisposableOpenCVObject`

Structure containing image keypoints and descriptors.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`ImgIdx`** | `int` | Gets or sets the img_idx property. |
| **`ImgSize`** | `Size` | Gets or sets the img_size property. |
| **`Keypoints`** | `IntPtr` | Gets or sets the keypoints property. |
| **`Descriptors`** | `IntPtr` | Gets or sets the descriptors property. |

#### Methods
* `IntPtr GetKeypoints()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailLightGlueFeaturesMatcher`
**Inherits from**: `DetailFeaturesMatcher`

Features matcher that adapts LightGlueMatcher (DescriptorMatcher) to the stitching pipeline's FeaturesMatcher interface.

**Detailed Remarks**:
This matcher uses DNN-based LightGlue for feature matching, requiring ALIKED-style
keypoints with spatial context for positional encoding.
**See also**: Detail.FeaturesMatcher LightGlueMatcher

#### Constructors
* `new DetailLightGlueFeaturesMatcher(LightGlueMatcher lgMatcher, int num_matches_thresh1, int num_matches_thresh2, double matches_confidence_thresh)`
  * *Summary*: Constructs a LightGlue features matcher.
  * *Parameter* `lgMatcher`: LightGlueMatcher instance for DNN-based matching
  * *Parameter* `num_matches_thresh1`: Minimum number of matches required for the 2D projective transform estimation used in the inliers classification step
  * *Parameter* `num_matches_thresh2`: Minimum number of matches required for the 2D projective transform re-estimation on inliers
  * *Parameter* `matches_confidence_thresh`: Matching confidence threshold to take the match into account.

#### Methods
* `void SetScoreThreshold(float thresh)`
  * *Summary*: Sets the LightGlue confidence threshold for filtering matches.
  * *Parameter* `thresh`: The thresh parameter.

---
### `DetailMatchesInfo`
**Inherits from**: `DisposableOpenCVObject`

Structure containing information about matches between two images.

**Detailed Remarks**:
It's assumed that there is a transformation between those images. Transformation may be
homography or affine transformation based on selected matcher.
**See also**: Detail.FeaturesMatcher

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`SrcImgIdx`** | `int` | Gets or sets the src_img_idx property. |
| **`DstImgIdx`** | `int` | Gets or sets the dst_img_idx property. |
| **`Matches`** | `IntPtr` | Gets or sets the matches property. |
| **`InliersMask`** | `IntPtr` | Gets or sets the inliers_mask property. |
| **`NumInliers`** | `int` | Gets or sets the num_inliers property. |
| **`H`** | `Mat?` | Gets or sets the H property. |
| **`Confidence`** | `double` | Gets or sets the confidence property. |

#### Methods
* `IntPtr GetMatches()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr GetInliers()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailMultiBandBlender`
**Inherits from**: `DetailBlender`

Blender which uses multi-band blending algorithm (see [BA83]).

#### Constructors
* `new DetailMultiBandBlender(int try_gpu, int num_bands, int weight_type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `try_gpu`: The try_gpu parameter.
  * *Parameter* `num_bands`: The num_bands parameter.
  * *Parameter* `weight_type`: The weight_type parameter.

#### Methods
* `int NumBands()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNumBands(int val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `void Prepare(Rect dst_roi)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst_roi`: The dst_roi parameter.
* `void Feed(Mat img, Mat mask, Point tl)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `img`: Input image.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `tl`: The tl parameter.
* `void Blend(Mat dst, Mat dst_mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `dst_mask`: The dst_mask parameter.

---
### `DetailNoBundleAdjuster`
**Inherits from**: `DetailBundleAdjusterBase`

Stub bundle adjuster that does nothing.

#### Constructors
* `new DetailNoBundleAdjuster()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `DetailNoExposureCompensator`
**Inherits from**: `DetailExposureCompensator`

Stub exposure compensator which does nothing.

#### Methods
* `void Apply(int arg1, Point arg2, Mat arg3, Mat arg4)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arg1`: The arg1 parameter.
  * *Parameter* `arg2`: The arg2 parameter.
  * *Parameter* `arg3`: The arg3 parameter.
  * *Parameter* `arg4`: The arg4 parameter.
* `void GetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.
* `void SetMatGains(IntPtr umv)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `umv`: The umv parameter.

---
### `DetailNoSeamFinder`
**Inherits from**: `DetailSeamFinder`

Stub seam estimator which does nothing.

#### Methods
* `void Find(IntPtr arg1, IntPtr arg2, IntPtr arg3)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arg1`: The arg1 parameter.
  * *Parameter* `arg2`: The arg2 parameter.
  * *Parameter* `arg3`: The arg3 parameter.

---
### `DetailPairwiseSeamFinder`
**Inherits from**: `DetailSeamFinder`

Base class for all pairwise seam estimators.

#### Methods
* `void Find(IntPtr src, IntPtr corners, IntPtr masks)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `src`: Source matrix or image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `masks`: The masks parameter.

---
### `DetailProjectorBase`
**Inherits from**: `DisposableOpenCVObject`

Base class for warping logic implementation.

---
### `DetailSeamFinder`
**Inherits from**: `DisposableOpenCVObject`

Base class for a seam estimator.

#### Methods
* `void Find(IntPtr src, IntPtr corners, IntPtr masks)`
  * *Summary*: Estimates seams.
  * *Parameter* `src`: Source images
  * *Parameter* `corners`: Source image top-left corners
  * *Parameter* `masks`: Source image masks to update
* `DetailSeamFinder? CreateDefault(int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `type`: The type parameter.
  * *Returns*: The returned value.

---
### `DetailSphericalProjector`
**Inherits from**: `DetailProjectorBase`

Extracts rotation and translation matrices from matrix H representing affine transformation in homogeneous coordinates

#### Methods
* `void MapForward(float x, float y, float u, float v)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `x`: The x parameter.
  * *Parameter* `y`: The y parameter.
  * *Parameter* `u`: The u parameter.
  * *Parameter* `v`: The v parameter.
* `void MapBackward(float u, float v, float x, float y)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `u`: The u parameter.
  * *Parameter* `v`: The v parameter.
  * *Parameter* `x`: The x parameter.
  * *Parameter* `y`: The y parameter.

---
### `DetailTimelapser`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Methods
* `DetailTimelapser? CreateDefault(int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `type`: The type parameter.
  * *Returns*: The returned value.
* `void Initialize(IntPtr corners, IntPtr sizes)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `sizes`: The sizes parameter.
* `void Process(Mat img, Mat mask, Point tl)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `img`: Input image.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `tl`: The tl parameter.
* `IntPtr GetDst()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DetailTimelapserCrop`
**Inherits from**: `DetailTimelapser`

Wrapper for OpenCV's native functionality.

---
### `DetailVoronoiSeamFinder`
**Inherits from**: `DetailPairwiseSeamFinder`

Voronoi diagram-based seam estimator.

#### Methods
* `void Find(IntPtr src, IntPtr corners, IntPtr masks)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `src`: Source matrix or image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `masks`: The masks parameter.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.DetailFocalsFromHomography`
**Signature**: `void DetailFocalsFromHomography(Mat H, double f0, double f1, bool f0_ok, bool f1_ok)`

Tries to estimate focal lengths from the given homography under the assumption that the camera undergoes rotations around its centre only.

**Parameters**:
* `H`: Homography.
* `f0`: Estimated focal length along X axis.
* `f1`: Estimated focal length along Y axis.
* `f0_ok`: True, if f0 was estimated successfully, false otherwise.
* `f1_ok`: True, if f1 was estimated successfully, false otherwise. See "Construction of Panoramic Image Mosaics with Global and Local Alignment" by Heung-Yeung Shum and Richard Szeliski.

---
### `Cv2.DetailCalibrateRotatingCamera`
**Signature**: `bool DetailCalibrateRotatingCamera(IntPtr Hs, Mat K)`

Estimates focal lengths for each given camera.

**Parameters**:
* `Hs`: The Hs parameter.
* `K`: The K parameter.

**Returns**: The returned value.

---
### `Cv2.DetailNormalizeUsingWeightMap`
**Signature**: `void DetailNormalizeUsingWeightMap(Mat weight, Mat src)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `weight`: The weight parameter.
* `src`: Source matrix or image.

---
### `Cv2.DetailCreateWeightMap`
**Signature**: `void DetailCreateWeightMap(Mat mask, float sharpness, Mat weight)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `mask`: Optional operation mask.
* `sharpness`: The sharpness parameter.
* `weight`: The weight parameter.

---
### `Cv2.DetailCreateLaplacePyr`
**Signature**: `void DetailCreateLaplacePyr(Mat img, int num_levels, IntPtr pyr)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `img`: Input image.
* `num_levels`: The num_levels parameter.
* `pyr`: The pyr parameter.

---
### `Cv2.DetailCreateLaplacePyrGpu`
**Signature**: `void DetailCreateLaplacePyrGpu(Mat img, int num_levels, IntPtr pyr)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `img`: Input image.
* `num_levels`: The num_levels parameter.
* `pyr`: The pyr parameter.

---
### `Cv2.DetailRestoreImageFromLaplacePyr`
**Signature**: `void DetailRestoreImageFromLaplacePyr(IntPtr pyr)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `pyr`: The pyr parameter.

---
### `Cv2.DetailRestoreImageFromLaplacePyrGpu`
**Signature**: `void DetailRestoreImageFromLaplacePyrGpu(IntPtr pyr)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `pyr`: The pyr parameter.

---
### `Cv2.DetailComputeImageFeatures`
**Signature**: `void DetailComputeImageFeatures(Feature2D featuresFinder, IntPtr images, IntPtr features, IntPtr masks)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `featuresFinder`: The featuresFinder parameter.
* `images`: The images parameter.
* `features`: The features parameter.
* `masks`: The masks parameter.

---
### `Cv2.DetailComputeImageFeatures`
**Signature**: `void DetailComputeImageFeatures(Feature2D featuresFinder, Mat image, DetailImageFeatures features, Mat? mask)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `featuresFinder`: The featuresFinder parameter.
* `image`: Input image.
* `features`: The features parameter.
* `mask`: Optional operation mask.

---
### `Cv2.DetailWaveCorrect`
**Signature**: `void DetailWaveCorrect(IntPtr rmats, DetailWaveCorrectKind kind)`

Tries to make panorama more horizontal (or vertical).

**Parameters**:
* `rmats`: Camera rotation matrices.
* `kind`: Correction kind, see Detail.WaveCorrectKind.

---
### `Cv2.DetailMatchesGraphAsString`
**Signature**: `string? DetailMatchesGraphAsString(IntPtr paths, IntPtr pairwise_matches, float conf_threshold)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `paths`: The paths parameter.
* `pairwise_matches`: The pairwise_matches parameter.
* `conf_threshold`: The conf_threshold parameter.

**Returns**: The returned value.

---
### `Cv2.DetailLeaveBiggestComponent`
**Signature**: `IntPtr DetailLeaveBiggestComponent(IntPtr features, IntPtr pairwise_matches, float conf_threshold)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `features`: The features parameter.
* `pairwise_matches`: The pairwise_matches parameter.
* `conf_threshold`: The conf_threshold parameter.

**Returns**: The returned value.

---
### `Cv2.DetailOverlapRoi`
**Signature**: `bool DetailOverlapRoi(Point tl1, Point tl2, Size sz1, Size sz2, Rect roi)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `tl1`: The tl1 parameter.
* `tl2`: The tl2 parameter.
* `sz1`: The sz1 parameter.
* `sz2`: The sz2 parameter.
* `roi`: The roi parameter.

**Returns**: The returned value.

---
### `Cv2.DetailResultRoi`
**Signature**: `Rect DetailResultRoi(IntPtr corners, IntPtr images)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `corners`: The corners parameter.
* `images`: The images parameter.

**Returns**: The returned value.

---
### `Cv2.DetailResultRoiIntersection`
**Signature**: `Rect DetailResultRoiIntersection(IntPtr corners, IntPtr sizes)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `corners`: The corners parameter.
* `sizes`: The sizes parameter.

**Returns**: The returned value.

---
### `Cv2.DetailResultTl`
**Signature**: `Point DetailResultTl(IntPtr corners)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `corners`: The corners parameter.

**Returns**: The returned value.

---
### `Cv2.DetailSelectRandomSubset`
**Signature**: `void DetailSelectRandomSubset(int count, int size, IntPtr subset)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `count`: The count parameter.
* `size`: The size parameter.
* `subset`: The subset parameter.

---
### `Cv2.DetailStitchingLogLevel`
**Signature**: `int DetailStitchingLogLevel()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
## 🔢 Enumerations

### `StitcherMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Panorama`** | `0` | Panorama |
| **`Scans`** | `1` | Scans |

---
### `StitcherStatus`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Ok`** | `0` | Ok |
| **`ErrNeedMoreImgs`** | `1` | ErrNeedMoreImgs |
| **`ErrHomographyEstFail`** | `2` | ErrHomographyEstFail |
| **`ErrCameraParamsAdjustFail`** | `3` | ErrCameraParamsAdjustFail |

---
### `UnnamedEnum13DetailBlender`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`No`** | `0` | No |
| **`Feather`** | `1` | Feather |
| **`MultiBand`** | `2` | MultiBand |

---
### `DetailDpSeamFinderCostFunction`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Color`** | `0` | Color |
| **`ColorGrad`** | `1` | ColorGrad |

---
### `UnnamedEnum14DetailExposureCompensator`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`No`** | `0` | No |
| **`Gain`** | `1` | Gain |
| **`GainBlocks`** | `2` | GainBlocks |
| **`Channels`** | `3` | Channels |
| **`ChannelsBlocks`** | `4` | ChannelsBlocks |

---
### `DetailGraphCutSeamFinderBaseCostType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Color`** | `0` | Color |
| **`ColorGrad`** | `1` | ColorGrad |

---
### `UnnamedEnum15DetailSeamFinder`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`No`** | `0` | No |
| **`VoronoiSeam`** | `1` | VoronoiSeam |
| **`DpSeam`** | `2` | DpSeam |

---
### `UnnamedEnum16DetailTimelapser`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`AsIs`** | `0` | AsIs |
| **`Crop`** | `1` | Crop |

---
### `DetailWaveCorrectKind`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Horiz`** | `0` | Horiz |
| **`Vert`** | `1` | Vert |
| **`Auto`** | `2` | Auto |

---

</div>