# PHOTO Module API Reference

Complete documentation for the **PHOTO** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Photo Documentation](https://docs.opencv.org/5.x/main_modules/photo.html).

---
<div v-pre>

## 📦 Classes and Structs

### `AlignExposures`
**Inherits from**: `Algorithm`

The base class for algorithms that align images of the same scene with different exposures

#### Methods
* `void Process(IntPtr src, IntPtr dst, Mat times, Mat response)`
  * *Summary*: Aligns images
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: vector of aligned images
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.

---
### `AlignMTB`
**Inherits from**: `AlignExposures`

This algorithm converts images to median threshold bitmaps (1 for pixels brighter than median luminance and 0 otherwise) and than aligns the resulting bitmaps using bit operations.

**Detailed Remarks**:
It is invariant to exposure, so exposure values and camera response are not necessary.
In this implementation new image regions are filled with zeros.
For more information see **Citation**:  GW03 .

#### Methods
* `void Process(IntPtr src, IntPtr dst, Mat times, Mat response)`
  * *Summary*: Aligns images using the median threshold bitmap (MTB) method.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: vector of aligned images
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.
* `void Process(IntPtr src, IntPtr dst)`
  * *Summary*: Short version of process, that doesn't take extra arguments.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: vector of aligned images
* `Point CalculateShift(Mat img0, Mat img1)`
  * *Summary*: Calculates shift between two images, i. e. how to shift the second image to correspond it with the first.
  * *Parameter* `img0`: first image
  * *Parameter* `img1`: second image
  * *Returns*: A `Point` representing the (x, y) pixel shift needed to align the second image with the first.
* `void ShiftMat(Mat src, Mat dst, Point shift)`
  * *Summary*: Helper function, that shift Mat filling new regions with zeros.
  * *Parameter* `src`: input image
  * *Parameter* `dst`: result image
  * *Parameter* `shift`: shift value
* `void ComputeBitmaps(Mat img, Mat tb, Mat eb)`
  * *Summary*: Computes median threshold and exclude bitmaps of given image.
  * *Parameter* `img`: input image
  * *Parameter* `tb`: median threshold bitmap
  * *Parameter* `eb`: exclude bitmap
* `int GetMaxBits()`
  * *Summary*: Gets the logarithm to the base 2 of the maximal shift in each dimension used during alignment.
  * *Returns*: The current max bits value (e.g. 6 means up to 63 pixels shift).
* `void SetMaxBits(int max_bits)`
  * *Summary*: Sets the logarithm to the base 2 of the maximal shift in each dimension. Values of 5 and 6 are usually good enough (31 and 63 pixels shift respectively).
  * *Parameter* `max_bits`: logarithm to the base 2 of maximal shift in each dimension.
* `int GetExcludeRange()`
  * *Summary*: Gets the range for the exclusion bitmap that is constructed to suppress noise around the median value.
  * *Returns*: The current exclude range value.
* `void SetExcludeRange(int exclude_range)`
  * *Summary*: Sets the range for the exclusion bitmap that suppresses noise around the median value.
  * *Parameter* `exclude_range`: range for exclusion bitmap construction.
* `bool GetCut()`
  * *Summary*: Gets whether images are cut to the aligned region or new regions are filled with zeros.
  * *Returns*: `true` if images are cut, `false` if new regions are filled with zeros.
* `void SetCut(bool value)`
  * *Summary*: Sets whether to cut images to the aligned region or fill new regions with zeros.
  * *Parameter* `value`: if `true`, cuts images to the aligned region; if `false`, fills new regions with zeros.

---
### `CalibrateCRF`
**Inherits from**: `Algorithm`

The base class for camera response calibration algorithms.

#### Methods
* `void Process(IntPtr src, Mat dst, Mat times)`
  * *Summary*: Recovers inverse camera response.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: 256x1 matrix with inverse camera response function
  * *Parameter* `times`: vector of exposure time values for each image

---
### `CalibrateDebevec`
**Inherits from**: `CalibrateCRF`

Inverse camera response function is extracted for each brightness value by minimizing an objective function as linear system. Objective function is constructed using pixel values on the same position in all images, extra term is added to make the result smoother.

**Detailed Remarks**:
For more information see **Citation**:  DM97 .

#### Methods
* `float GetLambda()`
  * *Summary*: Gets the smoothness term weight used in the objective function.
  * *Returns*: The current lambda (smoothness) value.
* `void SetLambda(float lambda)`
  * *Summary*: Sets the smoothness term weight. Greater values produce smoother results, but can alter the response.
  * *Parameter* `lambda`: smoothness term weight.
* `int GetSamples()`
  * *Summary*: Gets the number of pixel locations used for calibration.
  * *Returns*: The current number of sample pixel locations.
* `void SetSamples(int samples)`
  * *Summary*: Sets the number of pixel locations to use for calibration.
  * *Parameter* `samples`: number of pixel locations to use.
* `bool GetRandom()`
  * *Summary*: Gets whether sample pixel locations are chosen at random.
  * *Returns*: `true` if sample pixel locations are chosen at random, `false` if they form a rectangular grid.
* `void SetRandom(bool random)`
  * *Summary*: Sets whether sample pixel locations are chosen at random or form a rectangular grid.
  * *Parameter* `random`: if `true`, sample pixel locations are chosen at random; if `false`, they form a rectangular grid.

---
### `CalibrateRobertson`
**Inherits from**: `CalibrateCRF`

Inverse camera response function is extracted for each brightness value by minimizing an objective function as linear system. This algorithm uses all image pixels.

**Detailed Remarks**:
For more information see **Citation**:  RB99 .

#### Methods
* `int GetMaxIter()`
  * *Summary*: Gets the maximal number of Gauss-Seidel solver iterations.
  * *Returns*: The current maximum number of iterations.
* `void SetMaxIter(int max_iter)`
  * *Summary*: Sets the maximal number of Gauss-Seidel solver iterations.
  * *Parameter* `max_iter`: maximal number of Gauss-Seidel solver iterations.
* `float GetThreshold()`
  * *Summary*: Gets the target difference between results of two successive minimization steps.
  * *Returns*: The current convergence threshold value.
* `void SetThreshold(float threshold)`
  * *Summary*: Sets the target difference between results of two successive steps of the minimization.
  * *Parameter* `threshold`: target difference between results of two successive minimization steps.
* `Mat? GetRadiance()`
  * *Summary*: Gets the computed radiance map from the calibration process.
  * *Returns*: The radiance Mat, or `null` if not computed.

---
### `MergeDebevec`
**Inherits from**: `MergeExposures`

The resulting HDR image is calculated as weighted average of the exposures considering exposure values and camera response.

**Detailed Remarks**:
For more information see **Citation**:  DM97 .

#### Methods
* `void Process(IntPtr src, Mat dst, Mat times, Mat response)`
  * *Summary*: Merges multiple exposure images into a single HDR image using the Debevec method.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result HDR image
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.
* `void Process(IntPtr src, Mat dst, Mat times)`
  * *Summary*: Short version of process that uses the default camera response function.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result HDR image
  * *Parameter* `times`: vector of exposure time values for each image

---
### `MergeExposures`
**Inherits from**: `Algorithm`

The base class algorithms that can merge exposure sequence to a single image.

#### Methods
* `void Process(IntPtr src, Mat dst, Mat times, Mat response)`
  * *Summary*: Merges images.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result image
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.

---
### `MergeMertens`
**Inherits from**: `MergeExposures`

Pixels are weighted using contrast, saturation and well-exposedness measures, than images are combined using laplacian pyramids.

**Detailed Remarks**:
The resulting image weight is constructed as weighted average of contrast, saturation and
well-exposedness measures.
The resulting image doesn't require tonemapping and can be converted to 8-bit image by multiplying
by 255, but it's recommended to apply gamma correction and/or linear tonemapping.
For more information see **Citation**:  MK07 .

#### Methods
* `void Process(IntPtr src, Mat dst, Mat times, Mat response)`
  * *Summary*: Merges multiple exposure images using the Mertens exposure fusion method.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result fused image
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.
* `void Process(IntPtr src, Mat dst)`
  * *Summary*: Short version of process, that doesn't take extra arguments.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result image
* `float GetContrastWeight()`
  * *Summary*: Gets the weight of the contrast measure used during exposure fusion.
  * *Returns*: The current contrast weight value.
* `void SetContrastWeight(float contrast_weiht)`
  * *Summary*: Sets the weight of the contrast measure used during exposure fusion.
  * *Parameter* `contrast_weiht`: contrast measure weight.
* `float GetSaturationWeight()`
  * *Summary*: Gets the weight of the saturation measure used during exposure fusion.
  * *Returns*: The current saturation weight value.
* `void SetSaturationWeight(float saturation_weight)`
  * *Summary*: Sets the weight of the saturation measure used during exposure fusion.
  * *Parameter* `saturation_weight`: saturation measure weight.
* `float GetExposureWeight()`
  * *Summary*: Gets the weight of the well-exposedness measure used during exposure fusion.
  * *Returns*: The current exposure weight value.
* `void SetExposureWeight(float exposure_weight)`
  * *Summary*: Sets the weight of the well-exposedness measure used during exposure fusion.
  * *Parameter* `exposure_weight`: well-exposedness measure weight.

---
### `MergeRobertson`
**Inherits from**: `MergeExposures`

The resulting HDR image is calculated as weighted average of the exposures considering exposure values and camera response.

**Detailed Remarks**:
For more information see **Citation**:  RB99 .

#### Methods
* `void Process(IntPtr src, Mat dst, Mat times, Mat response)`
  * *Summary*: Merges multiple exposure images into a single HDR image using the Robertson method.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result HDR image
  * *Parameter* `times`: vector of exposure time values for each image
  * *Parameter* `response`: 256x1 matrix with inverse camera response function for each pixel value, it should have the same number of channels as images.
* `void Process(IntPtr src, Mat dst, Mat times)`
  * *Summary*: Short version of process that uses the default camera response function.
  * *Parameter* `src`: vector of input images
  * *Parameter* `dst`: result HDR image
  * *Parameter* `times`: vector of exposure time values for each image

---
### `Tonemap`
**Inherits from**: `Algorithm`

Base class for tonemapping algorithms - tools that are used to map HDR image to 8-bit range.

#### Methods
* `void Process(Mat src, Mat dst)`
  * *Summary*: Tonemaps image
  * *Parameter* `src`: source image - CV_32FC3 Mat (float 32 bits 3 channels)
  * *Parameter* `dst`: destination image - CV_32FC3 Mat with values in [0, 1] range
* `float GetGamma()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetGamma(float gamma)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `gamma`: The gamma parameter.

---
### `TonemapDrago`
**Inherits from**: `Tonemap`

Adaptive logarithmic mapping is a fast global tonemapping algorithm that scales the image in logarithmic domain.

**Detailed Remarks**:
Since it's a global operator the same function is applied to all the pixels, it is controlled by the
bias parameter.
Optional saturation enhancement is possible as described in **Citation**:  FL02 .
For more information see **Citation**:  DM03 .

#### Methods
* `float GetSaturation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSaturation(float saturation)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `saturation`: The saturation parameter.
* `float GetBias()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetBias(float bias)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `bias`: The bias parameter.

---
### `TonemapMantiuk`
**Inherits from**: `Tonemap`

This algorithm transforms image to contrast using gradients on all levels of gaussian pyramid, transforms contrast values to HVS response and scales the response. After this the image is reconstructed from new contrast values.

**Detailed Remarks**:
For more information see **Citation**:  MM06 .

#### Methods
* `float GetScale()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScale(float scale)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `scale`: The scale parameter.
* `float GetSaturation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSaturation(float saturation)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `saturation`: The saturation parameter.

---
### `TonemapReinhard`
**Inherits from**: `Tonemap`

This is a global tonemapping operator that models human visual system.

**Detailed Remarks**:
Mapping function is controlled by adaptation parameter, that is computed using light adaptation and
color adaptation.
For more information see **Citation**:  RD05 .

#### Methods
* `float GetIntensity()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetIntensity(float intensity)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `intensity`: The intensity parameter.
* `float GetLightAdaptation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetLightAdaptation(float light_adapt)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `light_adapt`: The light_adapt parameter.
* `float GetColorAdaptation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetColorAdaptation(float color_adapt)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `color_adapt`: The color_adapt parameter.

---
### `CcmColorCorrectionModel`
**Inherits from**: `DisposableOpenCVObject`

Core class of ccm model

**Detailed Remarks**:
Produce a ColorCorrectionModel instance for inference

#### Constructors
* `new CcmColorCorrectionModel()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new CcmColorCorrectionModel(Mat src, int constColor)`
  * *Summary*: Color Correction Model
  * *Parameter* `src`: detected colors of ColorChecker patches; the color type is RGB not BGR, and the color values are in [0, 1];
  * *Parameter* `constColor`: the Built-in color card
* `new CcmColorCorrectionModel(Mat src, Mat colors, CcmColorSpace refColorSpace)`
  * *Summary*: Color Correction Model
  * *Parameter* `src`: detected colors of ColorChecker patches; the color type is RGB not BGR, and the color values are in [0, 1];
  * *Parameter* `colors`: the reference color values, the color values are in [0, 1].
  * *Parameter* `refColorSpace`: the corresponding color space If the color type is some RGB, the format is RGB not BGR;
* `new CcmColorCorrectionModel(Mat src, Mat colors, CcmColorSpace refColorSpace, Mat coloredPatchesMask)`
  * *Summary*: Color Correction Model
  * *Parameter* `src`: detected colors of ColorChecker patches; the color type is RGB not BGR, and the color values are in [0, 1];
  * *Parameter* `colors`: the reference color values, the color values are in [0, 1].
  * *Parameter* `refColorSpace`: the corresponding color space If the color type is some RGB, the format is RGB not BGR;
  * *Parameter* `coloredPatchesMask`: binary mask indicating which patches are colored (non-gray) patches

#### Methods
* `void SetColorSpace(CcmColorSpace cs)`
  * *Summary*: set ColorSpace
  * *Remarks*:

.: info Note
It should be some RGB color space;
Supported list of color cards:
- `COLOR_SPACE_SRGB`
- `COLOR_SPACE_ADOBE_RGB`
- `COLOR_SPACE_WIDE_GAMUT_RGB`
- `COLOR_SPACE_PRO_PHOTO_RGB`
- `COLOR_SPACE_DCI_P3_RGB`
- `COLOR_SPACE_APPLE_RGB`
- `COLOR_SPACE_REC_709_RGB`
- `COLOR_SPACE_REC_2020_RGB`
.:

  * *Parameter* `cs`: the absolute color space that detected colors convert to; default: `COLOR_SPACE_SRGB`
* `void SetCcmType(CcmCcmType ccmType)`
  * *Summary*: set ccmType
  * *Parameter* `ccmType`: the shape of color correction matrix(CCM); default: `CCM_LINEAR`
* `void SetDistance(CcmDistanceType distance)`
  * *Summary*: set Distance
  * *Parameter* `distance`: the type of color distance; default: `DISTANCE_CIE2000`
* `void SetLinearization(CcmLinearizationType linearizationType)`
  * *Summary*: set Linear
  * *Parameter* `linearizationType`: the method of linearization; default: `LINEARIZATION_GAMMA`
* `void SetLinearizationGamma(double gamma)`
  * *Summary*: set Gamma
  * *Remarks*:

.: info Note
only valid when linear is set to "gamma";
.:

  * *Parameter* `gamma`: the gamma value of gamma correction; default: 2.2;
* `void SetLinearizationDegree(int deg)`
  * *Summary*: set degree
  * *Remarks*:

.: info Note
only valid when linear is set to
- `LINEARIZATION_COLORPOLYFIT`
- `LINEARIZATION_GRAYPOLYFIT`
- `LINEARIZATION_COLORLOGPOLYFIT`
- `LINEARIZATION_GRAYLOGPOLYFIT`
.:

  * *Parameter* `deg`: the degree of linearization polynomial default: 3
* `void SetSaturatedThreshold(double lower, double upper)`
  * *Summary*: set SaturatedThreshold. The colors in the closed interval [lower, upper] are reserved to participate in the calculation of the loss function and initialization parameters
  * *Parameter* `lower`: the lower threshold to determine saturation; default: 0;
  * *Parameter* `upper`: the upper threshold to determine saturation; default: 0
* `void SetWeightsList(Mat weightsList)`
  * *Summary*: set WeightsList
  * *Parameter* `weightsList`: the list of weight of each color; default: empty array
* `void SetWeightCoeff(double weightsCoeff)`
  * *Summary*: set WeightCoeff
  * *Parameter* `weightsCoeff`: the exponent number of L* component of the reference color in CIE Lab color space; default: 0
* `void SetInitialMethod(CcmInitialMethodType initialMethodType)`
  * *Summary*: set InitialMethod
  * *Parameter* `initialMethodType`: the method of calculating CCM initial value; default: INITIAL_METHOD_LEAST_SQUARE
* `void SetMaxCount(int maxCount)`
  * *Summary*: set MaxCount
  * *Parameter* `maxCount`: used in MinProblemSolver-DownhillSolver; Terminal criteria to the algorithm; default: 5000;
* `void SetEpsilon(double epsilon)`
  * *Summary*: set Epsilon
  * *Parameter* `epsilon`: used in MinProblemSolver-DownhillSolver; Terminal criteria to the algorithm; default: 1e-4;
* `void SetRGB(bool rgb)`
  * *Summary*: Set whether the input image is in RGB color space
  * *Parameter* `rgb`: If true, the model expects input images in RGB format. If false, input is assumed to be in BGR (default).
* `Mat? Compute()`
  * *Summary*: make color correction
  * *Returns*: The returned value.
* `Mat? GetColorCorrectionMatrix()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetLoss()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Mat? GetSrcLinearRGB()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Mat? GetRefLinearRGB()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Mat? GetMask()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Mat? GetWeights()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void CorrectImage(Mat src, Mat dst, bool islinear)`
  * *Summary*: Applies color correction to the input image using a fitted color correction matrix. * * The conventional ranges for R, G, and B channel values are: -   0 to 255 for CV_8U images -   0 to 65535 for CV_16U images -   0 to 1 for CV_32F images
  * *Parameter* `src`: Input 8-bit, 16-bit unsigned or 32-bit float 3-channel image..
  * *Parameter* `dst`: Output image of the same size and datatype as src.
  * *Parameter* `islinear`: default false.
* `void Write(FileStorage fs)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `fs`: The fs parameter.
* `void Read(FileNode node)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `node`: The node parameter.

---
### `SegmentationIntelligentScissorsMB`
**Inherits from**: `DisposableOpenCVObject`

Intelligent Scissors image segmentation * * This class is used to find the path (contour) between two points * which can be used for image segmentation. * * Usage example: * *(see OpenCV documentation for examples)*

#### Constructors
* `new SegmentationIntelligentScissorsMB()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `SegmentationIntelligentScissorsMB? SetWeights(float weight_non_edge, float weight_gradient_direction, float weight_gradient_magnitude)`
  * *Summary*: Specify weights of feature functions * * Consider keeping weights normalized (sum of weights equals to 1.0) * Discrete dynamic programming (DP) goal is minimization of costs between pixels. * * **weight_non_edge** Specify cost of non-edge pixels (default: 0.43f) * **weight_gradient_direction** Specify cost of gradient direction function (default: 0.43f) * **weight_gradient_magnitude** Specify cost of gradient magnitude function (default: 0.14f)
  * *Parameter* `weight_non_edge`: The weight_non_edge parameter.
  * *Parameter* `weight_gradient_direction`: The weight_gradient_direction parameter.
  * *Parameter* `weight_gradient_magnitude`: The weight_gradient_magnitude parameter.
  * *Returns*: The returned value.
* `SegmentationIntelligentScissorsMB? SetGradientMagnitudeMaxLimit(float gradient_magnitude_threshold_max)`
  * *Summary*: Specify gradient magnitude max value threshold * * Zero limit value is used to disable gradient magnitude thresholding (default behavior, as described in original article). * Otherwize pixels with `gradient magnitude >= threshold` have zero cost. * * **Note:** Thresholding should be used for images with irregular regions (to avoid stuck on parameters from high-contract areas, like embedded logos). * * **gradient_magnitude_threshold_max** Specify gradient magnitude max value threshold (default: 0, disabled)
  * *Parameter* `gradient_magnitude_threshold_max`: The gradient_magnitude_threshold_max parameter.
  * *Returns*: The returned value.
* `SegmentationIntelligentScissorsMB? SetEdgeFeatureZeroCrossingParameters(float gradient_magnitude_min_value)`
  * *Summary*: Switch to "Laplacian Zero-Crossing" edge feature extractor and specify its parameters * * This feature extractor is used by default according to article. * * Implementation has additional filtering for regions with low-amplitude noise. * This filtering is enabled through parameter of minimal gradient amplitude (use some small value 4, 8, 16). * * **Note:** Current implementation of this feature extractor is based on processing of grayscale images (color image is converted to grayscale image first). * * **Note:** Canny edge detector is a bit slower, but provides better results (especially on color images): use setEdgeFeatureCannyParameters(). * * **gradient_magnitude_min_value** Minimal gradient magnitude value for edge pixels (default: 0, check is disabled)
  * *Parameter* `gradient_magnitude_min_value`: The gradient_magnitude_min_value parameter.
  * *Returns*: The returned value.
* `SegmentationIntelligentScissorsMB? SetEdgeFeatureCannyParameters(double threshold1, double threshold2, int apertureSize, bool L2gradient)`
  * *Summary*: Switch edge feature extractor to use Canny edge detector * * **Note:** "Laplacian Zero-Crossing" feature extractor is used by default (following to original article) * * **See also:** Canny
  * *Parameter* `threshold1`: First threshold for the hysteresis procedure.
  * *Parameter* `threshold2`: Second threshold for the hysteresis procedure.
  * *Parameter* `apertureSize`: Aperture size for the Sobel operator.
  * *Parameter* `L2gradient`: The L2gradient parameter.
  * *Returns*: The returned value.
* `SegmentationIntelligentScissorsMB? ApplyImage(Mat image)`
  * *Summary*: Specify input image and extract image features * * **image** input image. Type is `CV_8UC1` / `CV_8UC3`
  * *Parameter* `image`: Input image.
  * *Returns*: The returned value.
* `SegmentationIntelligentScissorsMB? ApplyImageFeatures(Mat non_edge, Mat gradient_direction, Mat gradient_magnitude, Mat? image)`
  * *Summary*: Specify custom features of input image * * Customized advanced variant of applyImage() call. * * **non_edge** Specify cost of non-edge pixels. Type is CV_8UC1. Expected values are `{0, 1}`. * **gradient_direction** Specify gradient direction feature. Type is CV_32FC2. Values are expected to be normalized: `x^2 + y^2 == 1` * **gradient_magnitude** Specify cost of gradient magnitude function: Type is CV_32FC1. Values should be in range `[0, 1]`. * **image** **Optional parameter**. Must be specified if subset of features is specified (non-specified features are calculated internally)
  * *Parameter* `non_edge`: The non_edge parameter.
  * *Parameter* `gradient_direction`: The gradient_direction parameter.
  * *Parameter* `gradient_magnitude`: The gradient_magnitude parameter.
  * *Parameter* `image`: Input image.
  * *Returns*: The returned value.
* `void BuildMap(Point sourcePt)`
  * *Summary*: Prepares a map of optimal paths for the given source point on the image * * **Note:** applyImage() / applyImageFeatures() must be called before this call * * **sourcePt** The source point used to find the paths
  * *Parameter* `sourcePt`: The sourcePt parameter.
* `void GetContour(Point targetPt, Mat contour, bool backward)`
  * *Summary*: Extracts optimal contour for the given target point on the image * * **Note:** buildMap() must be called before this call * * **targetPt** The target point * **contour** The list of pixels which contains optimal path between the source and the target points of the image. Type is CV_32SC2 (compatible with `Point[]`) * **backward** Flag to indicate reverse order of retrieved pixels (use "true" value to fetch points from the target to the source point)
  * *Parameter* `targetPt`: The targetPt parameter.
  * *Parameter* `contour`: The contour parameter.
  * *Parameter* `backward`: The backward parameter.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.Inpaint`
**Signature**: `void Inpaint(Mat src, Mat inpaintMask, Mat dst, double inpaintRadius, int flags)`

Restores the selected region in an image using the region neighborhood.

**Detailed Remarks**:
.: info Note

-   An example using the inpainting technique can be found at
.:

**Parameters**:
* `src`: Input 8-bit, 16-bit unsigned or 32-bit float 1-channel or 8-bit 3-channel image.
* `inpaintMask`: Inpainting mask, 8-bit 1-channel image. Non-zero pixels indicate the area that needs to be inpainted.
* `dst`: Output image with the same size and type as src .
* `inpaintRadius`: Radius of a circular neighborhood of each point inpainted that is considered by the algorithm.
* `flags`: Inpainting method that could be INPAINT_NS or INPAINT_TELEA The function reconstructs the selected image area from the pixel near the area boundary. The function may be used to remove dust and scratches from a scanned photo, or to remove undesirable objects from still images or video. See <http://en.wikipedia.org/wiki/Inpainting> for more details.

---
### `Cv2.FastNlMeansDenoising`
**Signature**: `void FastNlMeansDenoising(Mat src, Mat dst, float h, int templateWindowSize, int searchWindowSize)`

Perform image denoising using Non-local Means Denoising algorithm <http://www.ipol.im/pub/algo/bcm_non_local_means_denoising/> with several computational optimizations. Noise expected to be a gaussian white noise

**Parameters**:
* `src`: Input 8-bit 1-channel, 2-channel, 3-channel or 4-channel image.
* `dst`: Output image with the same size and type as src .
* `h`: Parameter regulating filter strength. Big h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise This function expected to be applied to grayscale images. For colored images look at fastNlMeansDenoisingColored. Advanced usage of this functions can be manual denoising of colored image in different colorspaces. Such approach is used in fastNlMeansDenoisingColored by converting image to CIELAB colorspace and then separately denoise L and AB components with different h parameter.
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels

---
### `Cv2.FastNlMeansDenoising`
**Signature**: `void FastNlMeansDenoising(Mat src, Mat dst, IntPtr h, int templateWindowSize, int searchWindowSize, int normType)`

Perform image denoising using Non-local Means Denoising algorithm <http://www.ipol.im/pub/algo/bcm_non_local_means_denoising/> with several computational optimizations. Noise expected to be a gaussian white noise

**Parameters**:
* `src`: Input 8-bit or 16-bit (only with NORM_L1) 1-channel, 2-channel, 3-channel or 4-channel image.
* `dst`: Output image with the same size and type as src .
* `h`: Array of parameters regulating filter strength, either one parameter applied to all channels or one per channel in dst. Big h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels
* `normType`: Type of norm used for weight calculation. Can be either NORM_L2 or NORM_L1 This function expected to be applied to grayscale images. For colored images look at fastNlMeansDenoisingColored. Advanced usage of this functions can be manual denoising of colored image in different colorspaces. Such approach is used in fastNlMeansDenoisingColored by converting image to CIELAB colorspace and then separately denoise L and AB components with different h parameter.

---
### `Cv2.FastNlMeansDenoisingColored`
**Signature**: `void FastNlMeansDenoisingColored(Mat src, Mat dst, float h, float hColor, int templateWindowSize, int searchWindowSize)`

Modification of fastNlMeansDenoising function for colored images

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst`: Output image with the same size and type as src .
* `h`: Parameter regulating filter strength for luminance component. Bigger h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `hColor`: The same as h but for color components. For most images value equals 10 will be enough to remove colored noise and do not distort colors The function converts image to CIELAB colorspace and then separately denoise L and AB components with given h parameters using fastNlMeansDenoising function.
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels

---
### `Cv2.FastNlMeansDenoisingMulti`
**Signature**: `void FastNlMeansDenoisingMulti(IntPtr srcImgs, Mat dst, int imgToDenoiseIndex, int temporalWindowSize, float h, int templateWindowSize, int searchWindowSize)`

Modification of fastNlMeansDenoising function for images sequence where consecutive images have been captured in small period of time. For example video. This version of the function is for grayscale images or for manual manipulation with colorspaces. See [Buades2005DenoisingIS] for more details (open access [here](https://static.aminer.org/pdf/PDF/000/317/196/spatio_temporal_wiener_filtering_of_image_sequences_using_a_parametric.pdf)).

**Parameters**:
* `srcImgs`: Input 8-bit 1-channel, 2-channel, 3-channel or 4-channel images sequence. All images should have the same type and size.
* `dst`: Output image with the same size and type as srcImgs images.
* `imgToDenoiseIndex`: Target image to denoise index in srcImgs sequence
* `temporalWindowSize`: Number of surrounding images to use for target image denoising. Should be odd. Images from imgToDenoiseIndex - temporalWindowSize / 2 to imgToDenoiseIndex + temporalWindowSize / 2 from srcImgs will be used to denoise srcImgs[imgToDenoiseIndex] image.
* `h`: Parameter regulating filter strength. Bigger h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels

---
### `Cv2.FastNlMeansDenoisingMulti`
**Signature**: `void FastNlMeansDenoisingMulti(IntPtr srcImgs, Mat dst, int imgToDenoiseIndex, int temporalWindowSize, IntPtr h, int templateWindowSize, int searchWindowSize, int normType)`

Modification of fastNlMeansDenoising function for images sequence where consecutive images have been captured in small period of time. For example video. This version of the function is for grayscale images or for manual manipulation with colorspaces. See [Buades2005DenoisingIS] for more details (open access [here](https://static.aminer.org/pdf/PDF/000/317/196/spatio_temporal_wiener_filtering_of_image_sequences_using_a_parametric.pdf)).

**Parameters**:
* `srcImgs`: Input 8-bit or 16-bit (only with NORM_L1) 1-channel, 2-channel, 3-channel or 4-channel images sequence. All images should have the same type and size.
* `dst`: Output image with the same size and type as srcImgs images.
* `imgToDenoiseIndex`: Target image to denoise index in srcImgs sequence
* `temporalWindowSize`: Number of surrounding images to use for target image denoising. Should be odd. Images from imgToDenoiseIndex - temporalWindowSize / 2 to imgToDenoiseIndex + temporalWindowSize / 2 from srcImgs will be used to denoise srcImgs[imgToDenoiseIndex] image.
* `h`: Array of parameters regulating filter strength, either one parameter applied to all channels or one per channel in dst. Big h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels
* `normType`: Type of norm used for weight calculation. Can be either NORM_L2 or NORM_L1

---
### `Cv2.FastNlMeansDenoisingColoredMulti`
**Signature**: `void FastNlMeansDenoisingColoredMulti(IntPtr srcImgs, Mat dst, int imgToDenoiseIndex, int temporalWindowSize, float h, float hColor, int templateWindowSize, int searchWindowSize)`

Modification of fastNlMeansDenoisingMulti function for colored images sequences

**Parameters**:
* `srcImgs`: Input 8-bit 3-channel images sequence. All images should have the same type and size.
* `dst`: Output image with the same size and type as srcImgs images.
* `imgToDenoiseIndex`: Target image to denoise index in srcImgs sequence
* `temporalWindowSize`: Number of surrounding images to use for target image denoising. Should be odd. Images from imgToDenoiseIndex - temporalWindowSize / 2 to imgToDenoiseIndex + temporalWindowSize / 2 from srcImgs will be used to denoise srcImgs[imgToDenoiseIndex] image.
* `h`: Parameter regulating filter strength for luminance component. Bigger h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise.
* `hColor`: The same as h but for color components. The function converts images to CIELAB colorspace and then separately denoise L and AB components with given h parameters using fastNlMeansDenoisingMulti function.
* `templateWindowSize`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `searchWindowSize`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater searchWindowsSize - greater denoising time. Recommended value 21 pixels

---
### `Cv2.DenoiseTVL1`
**Signature**: `void DenoiseTVL1(IntPtr observations, Mat result, double lambda, int niters)`

Primal-dual algorithm is an algorithm for solving special types of variational problems (that is, finding a function to minimize some functional). As the image denoising, in particular, may be seen as the variational problem, primal-dual algorithm then can be used to perform denoising and this is exactly what is implemented.

**Detailed Remarks**:
It should be noted, that this implementation was taken from the July 2013 blog entry
**Citation**:  MA13 , which also contained (slightly more general) ready-to-use source code.
Subsequently, that code was rewritten on C# with the usage of OpenCV5Sharp by Vadim Pisarevsky at the end
of July 2013 and finally it was slightly adapted by later authors.
Although the thorough discussion and justification of the algorithm involved may be found in
**Citation**:  ChambolleEtAl, it might make sense to skim over it here, following **Citation**:  MA13 . To begin
with, we consider the 1-byte gray-level images as the functions from the rectangular domain of
pixels (it may be seen as set
formula for some
formula) into formula. We shall denote the noised images as formula and with
this view, given some image formula of the same size, we may measure how bad it is by the formula
[see mathematical formula in OpenCV docs]
formula here denotes formula-norm and as you see, the first addend states that we want our
image to be smooth (ideally, having zero gradient, thus being constant) and the second states that
we want our result to be close to the observations we've got. If we treat formula as a function, this is
exactly the functional what we seek to minimize and here the Primal-Dual algorithm comes into play.

**Parameters**:
* `observations`: This array should contain one or more noised versions of the image that is to be restored.
* `result`: Here the denoised image will be stored. There is no need to do pre-allocation of storage space, as it will be automatically allocated, if necessary.
* `lambda`: Corresponds to formula in the formulas above. As it is enlarged, the smooth (blurred) images are treated more favorably than detailed (but maybe more noised) ones. Roughly speaking, as it becomes smaller, the result will be more blur but more sever outliers will be removed.
* `niters`: Number of iterations that the algorithm will run. Of course, as more iterations as better, but it is hard to quantitatively refine this statement, so just use the default and increase it if the results are poor.

---
### `Cv2.CreateTonemap`
**Signature**: `Tonemap? CreateTonemap(float gamma)`

Creates simple linear mapper with gamma correction

**Parameters**:
* `gamma`: positive value for gamma correction. Gamma value of 1.0 implies no correction, gamma equal to 2.2f is suitable for most displays. Generally gamma \> 1 brightens the image and gamma \< 1 darkens it.

**Returns**: The returned value.

---
### `Cv2.CreateTonemapDrago`
**Signature**: `TonemapDrago? CreateTonemapDrago(float gamma, float saturation, float bias)`

Creates TonemapDrago object

**Parameters**:
* `gamma`: gamma value for gamma correction. See createTonemap
* `saturation`: positive saturation enhancement value. 1.0 preserves saturation, values greater than 1 increase saturation and values less than 1 decrease it.
* `bias`: value for bias function in [0, 1] range. Values from 0.7 to 0.9 usually give best results, default value is 0.85.

**Returns**: The returned value.

---
### `Cv2.CreateTonemapReinhard`
**Signature**: `TonemapReinhard? CreateTonemapReinhard(float gamma, float intensity, float light_adapt, float color_adapt)`

Creates TonemapReinhard object

**Parameters**:
* `gamma`: gamma value for gamma correction. See createTonemap
* `intensity`: result intensity in [-8, 8] range. Greater intensity produces brighter results.
* `light_adapt`: light adaptation in [0, 1] range. If 1 adaptation is based only on pixel value, if 0 it's global, otherwise it's a weighted mean of this two cases.
* `color_adapt`: chromatic adaptation in [0, 1] range. If 1 channels are treated independently, if 0 adaptation level is the same for each channel.

**Returns**: The returned value.

---
### `Cv2.CreateTonemapMantiuk`
**Signature**: `TonemapMantiuk? CreateTonemapMantiuk(float gamma, float scale, float saturation)`

Creates TonemapMantiuk object

**Parameters**:
* `gamma`: gamma value for gamma correction. See createTonemap
* `scale`: contrast scale factor. HVS response is multiplied by this parameter, thus compressing dynamic range. Values from 0.6 to 0.9 produce best results.
* `saturation`: saturation enhancement value. See createTonemapDrago

**Returns**: The returned value.

---
### `Cv2.CreateAlignMTB`
**Signature**: `AlignMTB? CreateAlignMTB(int max_bits, int exclude_range, bool cut)`

Creates AlignMTB object

**Parameters**:
* `max_bits`: logarithm to the base 2 of maximal shift in each dimension. Values of 5 and 6 are usually good enough (31 and 63 pixels shift respectively).
* `exclude_range`: range for exclusion bitmap that is constructed to suppress noise around the median value.
* `cut`: if true cuts images, otherwise fills the new regions with zeros.

**Returns**: The returned value.

---
### `Cv2.CreateCalibrateDebevec`
**Signature**: `CalibrateDebevec? CreateCalibrateDebevec(int samples, float lambda, bool random)`

Creates CalibrateDebevec object

**Parameters**:
* `samples`: number of pixel locations to use
* `lambda`: smoothness term weight. Greater values produce smoother results, but can alter the response.
* `random`: if true sample pixel locations are chosen at random, otherwise they form a rectangular grid.

**Returns**: The returned value.

---
### `Cv2.CreateCalibrateRobertson`
**Signature**: `CalibrateRobertson? CreateCalibrateRobertson(int max_iter, float threshold)`

Creates CalibrateRobertson object

**Parameters**:
* `max_iter`: maximal number of Gauss-Seidel solver iterations.
* `threshold`: target difference between results of two successive steps of the minimization.

**Returns**: The returned value.

---
### `Cv2.CreateMergeDebevec`
**Signature**: `MergeDebevec? CreateMergeDebevec()`

Creates MergeDebevec object

**Returns**: The returned value.

---
### `Cv2.CreateMergeMertens`
**Signature**: `MergeMertens? CreateMergeMertens(float contrast_weight, float saturation_weight, float exposure_weight)`

Creates MergeMertens object

**Parameters**:
* `contrast_weight`: contrast measure weight. See MergeMertens.
* `saturation_weight`: saturation measure weight
* `exposure_weight`: well-exposedness measure weight

**Returns**: The returned value.

---
### `Cv2.CreateMergeRobertson`
**Signature**: `MergeRobertson? CreateMergeRobertson()`

Creates MergeRobertson object

**Returns**: The returned value.

---
### `Cv2.Decolor`
**Signature**: `void Decolor(Mat src, Mat grayscale, Mat color_boost)`

Transforms a color image to a grayscale image. It is a basic tool in digital printing, stylized black-and-white photograph rendering, and in many single channel image processing applications [CL12] .

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `grayscale`: Output 8-bit 1-channel image.
* `color_boost`: Output 8-bit 3-channel image. This function is to be applied on color images.

---
### `Cv2.SeamlessClone`
**Signature**: `void SeamlessClone(Mat src, Mat dst, Mat mask, Point p, Mat blend, int flags)`

Performs seamless cloning to blend a region from a source image into a destination image. This function is designed for local image editing, allowing changes restricted to a region (manually selected as the ROI) to be applied effortlessly and seamlessly. These changes can range from slight distortions to complete replacement by novel content [PM03].

**Parameters**:
* `src`: The source image (8-bit 3-channel), from which a region will be blended into the destination.
* `dst`: The destination image (8-bit 3-channel), where the src image will be blended.
* `mask`: A binary mask (8-bit, 1, 3, or 4-channel) specifying the region in the source image to blend. Non-zero pixels indicate the region to be blended. If an empty Mat is provided, a mask with all non-zero pixels is created internally.
* `p`: The point where the center of the src image is placed in the dst image.
* `blend`: The output image that stores the result of the seamless cloning. It has the same size and type as `dst`.
* `flags`: Flags that control the type of cloning method, can take values of `SeamlessCloneFlags`.

---
### `Cv2.ColorChange`
**Signature**: `void ColorChange(Mat src, Mat mask, Mat dst, float red_mul, float green_mul, float blue_mul)`

Given an original color image, two differently colored versions of this image can be mixed seamlessly.

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `mask`: Input 8-bit 1 or 3-channel image.
* `dst`: Output image with the same size and type as src .
* `red_mul`: R-channel multiply factor.
* `green_mul`: G-channel multiply factor.
* `blue_mul`: B-channel multiply factor. Multiplication factor is between .5 to 2.5.

---
### `Cv2.IlluminationChange`
**Signature**: `void IlluminationChange(Mat src, Mat mask, Mat dst, float alpha, float beta)`

Applying an appropriate non-linear transformation to the gradient field inside the selection and then integrating back with a Poisson solver, modifies locally the apparent illumination of an image.

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `mask`: Input 8-bit 1 or 3-channel image.
* `dst`: Output image with the same size and type as src.
* `alpha`: Value ranges between 0-2.
* `beta`: Value ranges between 0-2. This is useful to highlight under-exposed foreground objects or to reduce specular reflections.

---
### `Cv2.TextureFlattening`
**Signature**: `void TextureFlattening(Mat src, Mat mask, Mat dst, float low_threshold, float high_threshold, int kernel_size)`

By retaining only the gradients at edge locations, before integrating with the Poisson solver, one washes out the texture of the selected region, giving its contents a flat aspect. Here Canny Edge Detector is used.

**Detailed Remarks**:
.: info Note

The algorithm assumes that the color of the source image is close to that of the destination. This
assumption means that when the colors don't match, the source image color gets tinted toward the
color of the destination image.
.:

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `mask`: Input 8-bit 1 or 3-channel image.
* `dst`: Output image with the same size and type as src.
* `low_threshold`: Range from 0 to 100.
* `high_threshold`: Value \> 100.
* `kernel_size`: The size of the Sobel kernel to be used.

---
### `Cv2.EdgePreservingFilter`
**Signature**: `void EdgePreservingFilter(Mat src, Mat dst, int flags, float sigma_s, float sigma_r)`

Filtering is the fundamental operation in image and video processing. Edge-preserving smoothing filters are used in many different applications [EM11] .

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst`: Output 8-bit 3-channel image.
* `flags`: Edge preserving filters: RECURS_FILTER or NORMCONV_FILTER
* `sigma_s`: Range between 0 to 200.
* `sigma_r`: Range between 0 to 1.

---
### `Cv2.DetailEnhance`
**Signature**: `void DetailEnhance(Mat src, Mat dst, float sigma_s, float sigma_r)`

This filter enhances the details of a particular image.

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst`: Output image with the same size and type as src.
* `sigma_s`: Range between 0 to 200.
* `sigma_r`: Range between 0 to 1.

---
### `Cv2.PencilSketch`
**Signature**: `void PencilSketch(Mat src, Mat dst1, Mat dst2, float sigma_s, float sigma_r, float shade_factor)`

Pencil-like non-photorealistic line drawing

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst1`: Output 8-bit 1-channel image.
* `dst2`: Output image with the same size and type as src.
* `sigma_s`: Range between 0 to 200.
* `sigma_r`: Range between 0 to 1.
* `shade_factor`: Range between 0 to 0.1.

---
### `Cv2.Stylization`
**Signature**: `void Stylization(Mat src, Mat dst, float sigma_s, float sigma_r)`

Stylization aims to produce digital imagery with a wide variety of effects not focused on photorealism. Edge-aware filters are ideal for stylization, as they can abstract regions of low contrast while preserving, or enhancing, high-contrast features.

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst`: Output image with the same size and type as src.
* `sigma_s`: Range between 0 to 200.
* `sigma_r`: Range between 0 to 1.

---
### `Cv2.CorrectChromaticAberration`
**Signature**: `void CorrectChromaticAberration(Mat input_image, Mat coefficients, Mat output_image, Size image_size, int calib_degree, int bayer_pattern)`

Corrects lateral chromatic aberration in an image using polynomial distortion model.

**Detailed Remarks**:
This function loads polynomial calibration data from the specified file and applies
a channel‐specific warp to remove chromatic aberration.
If `input_image` has one channel, it is assumed to be a raw Bayer image and is
first demosaiced using `bayer_pattern`. If it has three channels, it is treated
as a BGR image and `bayer_pattern` is ignored.
Firstly, calibration needs to be done using apps/chromatic-aberration-calibration/ca_calibration.py on a photo of
a pattern of black discs on white background, included in opencv_extra/testdata/cv/cameracalibration/chromatic_aberration/chromatic_aberration_pattern_a3.png
Calibration and correction are based on the algorithm described in **Citation**:  rudakova2013precise.
The chromatic aberration is modeled as a polynomial of some degree in red and blue channels compared to green.
In calibration, a photo of many black discs on white background is used, and the displacements
between the centres of discs in red and blue channels compared to green are minimized. The coefficients
are then saved in a yaml file which can be used with this function to correct lateral chromatic aberration.
**See also**: loadChromaticAberrationParams, demosaicing

**Parameters**:
* `input_image`: Input BGR image to correct
* `coefficients`: Coefficient model
* `output_image`: Corrected BGR image
* `image_size`: Size of images for the calibration coefficient model
* `calib_degree`: Degree of the calibration coefficient model
* `bayer_pattern`: Bayer pattern code (e.g. COLOR_BayerBG2BGR) used for demosaicing when `input_image` has one channel; ignored otherwise.

---
### `Cv2.LoadChromaticAberrationParams`
**Signature**: `void LoadChromaticAberrationParams(FileNode node, Mat coeffMat, Size calib_size, int degree)`

Load chromatic-aberration calibration parameters from opened FileStorage. * R e*ads the red and blue polynomial coefficients from the specified file and packs them into a 4×N CV_32F matrix: row 0 = blue dx coefficients row 1 = blue dy coefficients row 2 = red  dx coefficients row 3 = red  dy coefficients

**Detailed Remarks**:
**See also**: correctChromaticAberration

**Parameters**:
* `node`: Node of opened FileStorage object.
* `coeffMat`: Output 4xN coefficient matrix (CV_32F).
* `calib_size`: Calibration image size read from file.
* `degree`: Polynomial degree inferred from N.

---
### `Cv2.CcmGammaCorrection`
**Signature**: `void CcmGammaCorrection(Mat src, Mat dst, double gamma)`

*  Applies gamma correction to the input image.

**Detailed Remarks**:
* * **Parameter** `src`:  Input image.
* * **Parameter** `dst`:  Output image.
* * **Parameter** `gamma`:  Gamma correction greater than zero.

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `gamma`: The gamma parameter.

---
### `Cv2.CudaNonLocalMeans`
**Signature**: `void CudaNonLocalMeans(CudaGpuMat src, CudaGpuMat dst, float h, int search_window, int block_size, int borderMode, CudaStream? stream)`

Performs pure non local means denoising without any simplification, and thus it is not fast.

**Detailed Remarks**:
**See also**: 
fastNlMeansDenoising

**Parameters**:
* `src`: Source image. Supports only CV_8UC1, CV_8UC2 and CV_8UC3.
* `dst`: Destination image.
* `h`: Filter sigma regulating filter strength for color.
* `search_window`: Size of search window.
* `block_size`: Size of block used for computing weights.
* `borderMode`: Border type. See borderInterpolate for details. BORDER_REFLECT101 , BORDER_REPLICATE , BORDER_CONSTANT , BORDER_REFLECT and BORDER_WRAP are supported for now.
* `stream`: Stream for the asynchronous version.

---
### `Cv2.CudaFastNlMeansDenoising`
**Signature**: `void CudaFastNlMeansDenoising(CudaGpuMat src, CudaGpuMat dst, float h, int search_window, int block_size, CudaStream? stream)`

Perform image denoising using Non-local Means Denoising algorithm <http://www.ipol.im/pub/algo/bcm_non_local_means_denoising> with several computational optimizations. Noise expected to be a gaussian white noise

**Detailed Remarks**:
**See also**: 
fastNlMeansDenoising

**Parameters**:
* `src`: Input 8-bit 1-channel, 2-channel or 3-channel image.
* `dst`: Output image with the same size and type as src .
* `h`: Parameter regulating filter strength. Big h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `search_window`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater search_window - greater denoising time. Recommended value 21 pixels
* `block_size`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `stream`: Stream for the asynchronous invocations. This function expected to be applied to grayscale images. For colored images look at FastNonLocalMeansDenoising.labMethod.

---
### `Cv2.CudaFastNlMeansDenoisingColored`
**Signature**: `void CudaFastNlMeansDenoisingColored(CudaGpuMat src, CudaGpuMat dst, float h_luminance, float photo_render, int search_window, int block_size, CudaStream? stream)`

Modification of fastNlMeansDenoising function for colored images

**Detailed Remarks**:
**See also**: 
fastNlMeansDenoisingColored

**Parameters**:
* `src`: Input 8-bit 3-channel image.
* `dst`: Output image with the same size and type as src .
* `h_luminance`: Parameter regulating filter strength. Big h value perfectly removes noise but also removes image details, smaller h value preserves details but also preserves some noise
* `photo_render`: float The same as h but for color components. For most images value equals 10 will be enough to remove colored noise and do not distort colors
* `search_window`: Size in pixels of the window that is used to compute weighted average for given pixel. Should be odd. Affect performance linearly: greater search_window - greater denoising time. Recommended value 21 pixels
* `block_size`: Size in pixels of the template patch that is used to compute weights. Should be odd. Recommended value 7 pixels
* `stream`: Stream for the asynchronous invocations. The function converts image to CIELAB colorspace and then separately denoise L and AB components with given h parameters using FastNonLocalMeansDenoising.simpleMethod function.

---
## 🔢 Enumerations

### `SeamlessCloneFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`NormalClone`** | `1` | NormalClone |
| **`MixedClone`** | `2` | MixedClone |
| **`MonochromeTransfer`** | `3` | MonochromeTransfer |
| **`NormalCloneWide`** | `9` | NormalCloneWide |
| **`MixedCloneWide`** | `10` | MixedCloneWide |
| **`MonochromeTransferWide`** | `11` | MonochromeTransferWide |

---
### `CcmCcmType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Linear`** | `0` | Linear |
| **`Affine`** | `1` | Affine |

---
### `CcmColorCheckerType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Macbeth`** | `0` | Macbeth |
| **`Vinyl`** | `1` | Vinyl |
| **`DigitalSg`** | `2` | DigitalSg |

---
### `CcmColorSpace`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Srgb`** | `0` | Srgb |
| **`Srgbl`** | `1` | Srgbl |
| **`AdobeRgb`** | `2` | AdobeRgb |
| **`AdobeRgbl`** | `3` | AdobeRgbl |
| **`WideGamutRgb`** | `4` | WideGamutRgb |
| **`WideGamutRgbl`** | `5` | WideGamutRgbl |
| **`ProPhotoRgb`** | `6` | ProPhotoRgb |
| **`ProPhotoRgbl`** | `7` | ProPhotoRgbl |
| **`DciP3Rgb`** | `8` | DciP3Rgb |
| **`DciP3Rgbl`** | `9` | DciP3Rgbl |
| **`AppleRgb`** | `10` | AppleRgb |
| **`AppleRgbl`** | `11` | AppleRgbl |
| **`Rec709Rgb`** | `12` | Rec709Rgb |
| **`Rec709Rgbl`** | `13` | Rec709Rgbl |
| **`Rec2020Rgb`** | `14` | Rec2020Rgb |
| **`Rec2020Rgbl`** | `15` | Rec2020Rgbl |
| **`XyzD652`** | `16` | XyzD652 |
| **`XyzD502`** | `17` | XyzD502 |
| **`XyzD6510`** | `18` | XyzD6510 |
| **`XyzD5010`** | `19` | XyzD5010 |
| **`XyzA2`** | `20` | XyzA2 |
| **`XyzA10`** | `21` | XyzA10 |
| **`XyzD552`** | `22` | XyzD552 |
| **`XyzD5510`** | `23` | XyzD5510 |
| **`XyzD752`** | `24` | XyzD752 |
| **`XyzD7510`** | `25` | XyzD7510 |
| **`XyzE2`** | `26` | XyzE2 |
| **`XyzE10`** | `27` | XyzE10 |
| **`LabD652`** | `28` | LabD652 |
| **`LabD502`** | `29` | LabD502 |
| **`LabD6510`** | `30` | LabD6510 |
| **`LabD5010`** | `31` | LabD5010 |
| **`LabA2`** | `32` | LabA2 |
| **`LabA10`** | `33` | LabA10 |
| **`LabD552`** | `34` | LabD552 |
| **`LabD5510`** | `35` | LabD5510 |
| **`LabD752`** | `36` | LabD752 |
| **`LabD7510`** | `37` | LabD7510 |
| **`LabE2`** | `38` | LabE2 |
| **`LabE10`** | `39` | LabE10 |

---
### `CcmDistanceType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Cie76`** | `0` | Cie76 |
| **`Cie94GraphicArts`** | `1` | Cie94GraphicArts |
| **`Cie94Textiles`** | `2` | Cie94Textiles |
| **`Cie2000`** | `3` | Cie2000 |
| **`Cmc1to1`** | `4` | Cmc1to1 |
| **`Cmc2to1`** | `5` | Cmc2to1 |
| **`Rgb`** | `6` | Rgb |
| **`Rgbl`** | `7` | Rgbl |

---
### `CcmInitialMethodType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`WhiteBalance`** | `0` | WhiteBalance |
| **`LeastSquare`** | `1` | LeastSquare |

---
### `CcmLinearizationType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Identity`** | `0` | Identity |
| **`Gamma`** | `1` | Gamma |
| **`Colorpolyfit`** | `2` | Colorpolyfit |
| **`Colorlogpolyfit`** | `3` | Colorlogpolyfit |
| **`Graypolyfit`** | `4` | Graypolyfit |
| **`Graylogpolyfit`** | `5` | Graylogpolyfit |

---

</div>