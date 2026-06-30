# IMGPROC Module API Reference

Complete documentation for the **IMGPROC** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Imgproc Documentation](https://docs.opencv.org/5.x/main_modules/imgproc.html).

---
<div v-pre>

## 📦 Classes and Structs

### `Clahe`
**Inherits from**: `Algorithm`

Base class for Contrast Limited Adaptive Histogram Equalization.

#### Methods
* `void Apply(Mat src, Mat dst)`
  * *Summary*: Equalizes the histogram of a grayscale image using Contrast Limited Adaptive Histogram Equalization.
  * *Parameter* `src`: Source image of type CV_8UC1 or CV_16UC1.
  * *Parameter* `dst`: Destination image.
* `void SetClipLimit(double clipLimit)`
  * *Summary*: Sets threshold for contrast limiting.
  * *Parameter* `clipLimit`: threshold value.
* `double GetClipLimit()`
  * *Summary*: Returns the current threshold for contrast limiting.
  * *Returns*: The current clip limit value.
* `void SetTilesGridSize(Size tileGridSize)`
  * *Summary*: Sets size of grid for histogram equalization. Input image will be divided into equally sized rectangular tiles.
  * *Parameter* `tileGridSize`: defines the number of tiles in row and column.
* `Size GetTilesGridSize()`
  * *Summary*: Returns the current tile grid size used for histogram equalization.
  * *Returns*: The current tile grid size as a Size struct.
* `void SetBitShift(int bitShift)`
  * *Summary*: Sets bit shift parameter for histogram bins.
  * *Parameter* `bitShift`: bit shift value (default is 0).
* `int GetBitShift()`
  * *Summary*: Returns the bit shift parameter for histogram bins.
  * *Returns*: current bit shift value.
* `void CollectGarbage()`
  * *Summary*: Releases all internally allocated memory buffers.

---
### `Filter2DParams`
**Inherits from**: `DisposableOpenCVObject`

Parameters for the `Filter2D` convolution operation, encapsulating anchor, border type, depth, scale, and shift settings.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`AnchorX`** | `int` | Gets or sets the anchorX property. |
| **`AnchorY`** | `int` | Gets or sets the anchorY property. |
| **`BorderType`** | `int` | Gets or sets the borderType property. |
| **`Ddepth`** | `int` | Gets or sets the ddepth property. |
| **`Scale`** | `double` | Gets or sets the scale property. |
| **`Shift`** | `double` | Gets or sets the shift property. |

---
### `FontFace`
**Inherits from**: `DisposableOpenCVObject`

Wrapper on top of a truetype/opentype/etc font, i.e. Freetype's FT_Face.

**Detailed Remarks**:
The class is used to store the loaded fonts;
the font can then be passed to the functions
putText and GetTextSize.

#### Constructors
* `new FontFace()`
  * *Summary*: loads default font
* `new FontFace(string fontPathOrName)`
  * *Summary*: loads font at the specified path or with specified name.
  * *Parameter* `fontPathOrName`: either path to the custom font or the name of embedded font: "sans", "italic" or "uni". Empty fontPathOrName means the default embedded font.

#### Methods
* `bool Set(string fontPathOrName)`
  * *Summary*: loads new font face
  * *Parameter* `fontPathOrName`: The fontPathOrName parameter.
  * *Returns*: The returned value.
* `string? GetName()`
  * *Summary*: Returns the name of the currently loaded font.
  * *Returns*: The font name, or null if no font is loaded.
* `bool SetInstance(IntPtr @params)`
  * *Summary*: sets the current variable font instance.
  * *Parameter* `params`: The list of pairs key1, value1, key2, value2, ..., e.g. `myfont.setInstance({CV_FOURCC('w','g','h','t'), 400<<16, CV_FOURCC('s','l','n','t'), -(15<<16)});` Note that the parameter values are specified in 16.16 fixed-point format, that is, integer values need to be shifted by 16 (or multiplied by 65536).
  * *Returns*: The returned value.
* `bool GetInstance(IntPtr @params)`
  * *Summary*: Retrieves the current variable font instance parameters.
  * *Parameter* `params`: Output buffer to receive the list of key-value pairs defining the current font instance.
  * *Returns*: True if the instance parameters were retrieved successfully; false otherwise.

---
### `GeneralizedHough`
**Inherits from**: `Algorithm`

finds arbitrary template in the grayscale image using Generalized Hough Transform

#### Methods
* `void SetTemplate(Mat templ, Point templCenter)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `templ`: The templ parameter.
  * *Parameter* `templCenter`: The templCenter parameter.
* `void SetTemplate(Mat edges, Mat dx, Mat dy, Point templCenter)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edges`: The edges parameter.
  * *Parameter* `dx`: The dx parameter.
  * *Parameter* `dy`: The dy parameter.
  * *Parameter* `templCenter`: The templCenter parameter.
* `void Detect(Mat image, Mat positions, Mat? votes)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `image`: Input image.
  * *Parameter* `positions`: The positions parameter.
  * *Parameter* `votes`: The votes parameter.
* `void Detect(Mat edges, Mat dx, Mat dy, Mat positions, Mat? votes)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edges`: The edges parameter.
  * *Parameter* `dx`: The dx parameter.
  * *Parameter* `dy`: The dy parameter.
  * *Parameter* `positions`: The positions parameter.
  * *Parameter* `votes`: The votes parameter.
* `void SetCannyLowThresh(int cannyLowThresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cannyLowThresh`: The cannyLowThresh parameter.
* `int GetCannyLowThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetCannyHighThresh(int cannyHighThresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cannyHighThresh`: The cannyHighThresh parameter.
* `int GetCannyHighThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinDist(double minDist)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minDist`: The minDist parameter.
* `double GetMinDist()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetDp(double dp)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dp`: The dp parameter.
* `double GetDp()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxBufferSize(int maxBufferSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxBufferSize`: The maxBufferSize parameter.
* `int GetMaxBufferSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `GeneralizedHoughBallard`
**Inherits from**: `GeneralizedHough`

finds arbitrary template in the grayscale image using Generalized Hough Transform

**Detailed Remarks**:
Detects position only without translation and rotation **Citation**:  Ballard1981 .

#### Methods
* `void SetLevels(int levels)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `levels`: The levels parameter.
* `int GetLevels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetVotesThreshold(int votesThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `votesThreshold`: The votesThreshold parameter.
* `int GetVotesThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `GeneralizedHoughGuil`
**Inherits from**: `GeneralizedHough`

finds arbitrary template in the grayscale image using Generalized Hough Transform

**Detailed Remarks**:
Detects position, translation and rotation **Citation**:  Guil1999 .

#### Methods
* `void SetXi(double xi)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `xi`: The xi parameter.
* `double GetXi()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetLevels(int levels)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `levels`: The levels parameter.
* `int GetLevels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetAngleEpsilon(double angleEpsilon)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `angleEpsilon`: The angleEpsilon parameter.
* `double GetAngleEpsilon()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinAngle(double minAngle)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minAngle`: The minAngle parameter.
* `double GetMinAngle()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxAngle(double maxAngle)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxAngle`: The maxAngle parameter.
* `double GetMaxAngle()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetAngleStep(double angleStep)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `angleStep`: The angleStep parameter.
* `double GetAngleStep()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetAngleThresh(int angleThresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `angleThresh`: The angleThresh parameter.
* `int GetAngleThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinScale(double minScale)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minScale`: The minScale parameter.
* `double GetMinScale()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxScale(double maxScale)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxScale`: The maxScale parameter.
* `double GetMaxScale()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScaleStep(double scaleStep)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `scaleStep`: The scaleStep parameter.
* `double GetScaleStep()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScaleThresh(int scaleThresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `scaleThresh`: The scaleThresh parameter.
* `int GetScaleThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPosThresh(int posThresh)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `posThresh`: The posThresh parameter.
* `int GetPosThresh()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `LineSegmentDetector`
**Inherits from**: `Algorithm`

Line segment detector class

**Detailed Remarks**:
following the algorithm described at **Citation**:  Rafael12 .
.: info Note
Implementation has been removed from OpenCV version 3.4.6 to 3.4.15 and version 4.1.0 to 4.5.3 due original code license conflict.
restored again after [Computation of a NFA](https://github.com/rafael-grompone-von-gioi/binomial_nfa) code published under the MIT license.
.:

#### Methods
* `void Detect(Mat image, Mat lines, Mat? width, Mat? prec, Mat? nfa)`
  * *Summary*: Finds lines in the input image.
  * *Remarks*:

This is the output of the default parameters of the algorithm on the above shown image.

  * *Parameter* `image`: A grayscale (CV_8UC1) input image. If only a roi needs to be selected, use: `lsd_ptr-\>detect(image(roi), lines, ...); lines += Scalar(roi.x, roi.y, roi.x, roi.y);`
  * *Parameter* `lines`: A vector of Vec4f elements specifying the beginning and ending point of a line. Where Vec4f is (x1, y1, x2, y2), point 1 is the start, point 2 - end. Returned lines are strictly oriented depending on the gradient.
  * *Parameter* `width`: Vector of widths of the regions, where the lines are found. E.g. Width of line.
  * *Parameter* `prec`: Vector of precisions with which the lines are found.
  * *Parameter* `nfa`: Vector containing number of false alarms in the line region, with precision of 10%. The bigger the value, logarithmically better the detection. - -1 corresponds to 10 mean false alarms - 0 corresponds to 1 mean false alarm - 1 corresponds to 0.1 mean false alarms This vector will be calculated only when the objects type is `LSD_REFINE_ADV`.
* `void DrawSegments(Mat image, Mat lines)`
  * *Summary*: Draws the line segments on a given image.
  * *Parameter* `image`: The image, where the lines will be drawn. Should be bigger or equal to the image, where the lines were found.
  * *Parameter* `lines`: A vector of the lines that needed to be drawn.
* `int CompareSegments(Size size, Mat lines1, Mat lines2, Mat? image)`
  * *Summary*: Draws two groups of lines in blue and red, counting the non overlapping (mismatching) pixels.
  * *Parameter* `size`: The size of the image, where lines1 and lines2 were found.
  * *Parameter* `lines1`: The first group of lines that needs to be drawn. It is visualized in blue color.
  * *Parameter* `lines2`: The second group of lines. They visualized in red color.
  * *Parameter* `image`: Optional image, where the lines will be drawn. The image should be color(3-channel) in order for lines1 and lines2 to be drawn in the above mentioned colors.
  * *Returns*: The returned value.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.CreateLineSegmentDetector`
**Signature**: `LineSegmentDetector? CreateLineSegmentDetector(LineSegmentDetectorModes refine, double scale, double sigma_scale, double quant, double ang_th, double log_eps, double density_th, int n_bins)`

Creates a smart pointer to a LineSegmentDetector object and initializes it.

**Detailed Remarks**:
The LineSegmentDetector algorithm is defined using the standard values. Only advanced users may want
to edit those, as to tailor it for their own application.

**Parameters**:
* `refine`: The way found lines will be refined, see `LineSegmentDetectorModes`
* `scale`: The scale of the image that will be used to find the lines. Range (0..1].
* `sigma_scale`: Sigma for Gaussian filter. It is computed as sigma = sigma_scale/scale.
* `quant`: Bound to the quantization error on the gradient norm.
* `ang_th`: Gradient angle tolerance in degrees.
* `log_eps`: Detection threshold: -log10(NFA) \> log_eps. Used only when advance refinement is chosen.
* `density_th`: Minimal density of aligned region points in the enclosing rectangle.
* `n_bins`: Number of bins in pseudo-ordering of gradient modulus.

**Returns**: The returned value.

---
### `Cv2.GetGaussianKernel`
**Signature**: `Mat? GetGaussianKernel(int ksize, double sigma, int ktype)`

Returns Gaussian filter coefficients.

**Detailed Remarks**:
The function computes and returns the formula matrix of Gaussian filter
coefficients:
[see mathematical formula in OpenCV docs]
where formula and formula is the scale factor chosen so that formula.
Two of such generated kernels can be passed to sepFilter2D. Those functions automatically recognize
smoothing kernels (a symmetrical kernel with sum of weights equal to 1) and handle them accordingly.
You may also use the higher-level GaussianBlur.
**See also**: sepFilter2D, getDerivKernels, getStructuringElement, GaussianBlur

**Parameters**:
* `ksize`: Aperture size. It should be odd ( formula ) and positive.
* `sigma`: Gaussian standard deviation. If it is non-positive, it is computed from ksize as `sigma = 0.3*((ksize-1)*0.5 - 1) + 0.8`.
* `ktype`: Type of filter coefficients. It can be CV_32F or CV_64F .

**Returns**: The returned value.

---
### `Cv2.GetDerivKernels`
**Signature**: `void GetDerivKernels(Mat kx, Mat ky, int dx, int dy, int ksize, bool normalize, int ktype)`

Returns filter coefficients for computing spatial image derivatives.

**Detailed Remarks**:
The function computes and returns the filter coefficients for spatial image derivatives. When
`ksize=FILTER_SCHARR`, the Scharr formula kernels are generated (see `Scharr`). Otherwise, Sobel
kernels are generated (see `Sobel`). The filters are normally passed to `sepFilter2D` or to

**Parameters**:
* `kx`: Output matrix of row filter coefficients. It has the type ktype .
* `ky`: Output matrix of column filter coefficients. It has the type ktype .
* `dx`: Derivative order in respect of x.
* `dy`: Derivative order in respect of y.
* `ksize`: Aperture size. It can be FILTER_SCHARR, 1, 3, 5, or 7.
* `normalize`: Flag indicating whether to normalize (scale down) the filter coefficients or not. Theoretically, the coefficients should have the denominator formula. If you are going to filter floating-point images, you are likely to use the normalized kernels. But if you compute derivatives of an 8-bit image, store the results in a 16-bit image, and wish to preserve all the fractional bits, you may want to set normalize=false .
* `ktype`: Type of filter coefficients. It can be CV_32f or CV_64F .

---
### `Cv2.GetGaborKernel`
**Signature**: `Mat? GetGaborKernel(Size ksize, double sigma, double theta, double lambd, double gamma, double psi, int ktype)`

Returns Gabor filter coefficients.

**Detailed Remarks**:
For more details about gabor filter equations and parameters, see: [Gabor
Filter](https://en.wikipedia.org/wiki/Gabor_filter).

**Parameters**:
* `ksize`: Size of the filter returned.
* `sigma`: Standard deviation of the gaussian envelope.
* `theta`: Orientation of the normal to the parallel stripes of a Gabor function.
* `lambd`: Wavelength of the sinusoidal factor.
* `gamma`: Spatial aspect ratio.
* `psi`: Phase offset.
* `ktype`: Type of filter coefficients. It can be CV_32F or CV_64F .

**Returns**: The returned value.

---
### `Cv2.GetStructuringElement`
**Signature**: `Mat? GetStructuringElement(int shape, Size ksize, Point anchor)`

Returns a structuring element of the specified size and shape for morphological operations.

**Detailed Remarks**:
The function constructs and returns the structuring element that can be further passed to `erode`,
`dilate` or `morphologyEx`. But you can also construct an arbitrary binary mask yourself and use it as
the structuring element.

**Parameters**:
* `shape`: Element shape that could be one of `MorphShapes`
* `ksize`: Size of the structuring element.
* `anchor`: Anchor position within the element. The default value formula means that the anchor is at the center. Note that only the shape of a cross-shaped element depends on the anchor position. In other cases the anchor just regulates how much the result of the morphological operation is shifted.

**Returns**: The returned value.

---
### `Cv2.MedianBlur`
**Signature**: `void MedianBlur(Mat src, Mat dst, int ksize)`

Blurs an image using the median filter.

**Detailed Remarks**:
The function smoothes an image using the median filter with the `\texttt{ksize} \times
\texttt{ksize}` aperture. Each channel of a multi-channel image is processed independently.
In-place operation is supported.
.: info Note
The median filter uses `BORDER_REPLICATE` internally to cope with border pixels, see `BorderTypes`
**See also**: bilateralFilter, blur, boxFilter, GaussianBlur
.:

**Parameters**:
* `src`: input 1-, 3-, or 4-channel image; when ksize is 3 or 5, the image depth should be CV_8U, CV_16U, or CV_32F, for larger aperture sizes, it can only be CV_8U.
* `dst`: destination array of the same size and type as src.
* `ksize`: aperture linear size; it must be odd and greater than 1, for example: 3, 5, 7 ...

---
### `Cv2.GaussianBlur`
**Signature**: `void GaussianBlur(Mat src, Mat dst, Size ksize, double sigmaX, double sigmaY, int borderType, AlgorithmHint hint)`

Blurs an image using a Gaussian filter.

**Detailed Remarks**:
The function convolves the source image with the specified Gaussian kernel. In-place filtering is
supported.
**See also**: sepFilter2D, filter2D, blur, boxFilter, bilateralFilter, medianBlur

**Parameters**:
* `src`: input image; the image can have any number of channels, which are processed independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
* `dst`: output image of the same size and type as src.
* `ksize`: Gaussian kernel size. ksize.width and ksize.height can differ but they both must be positive and odd. Or, they can be zero's and then they are computed from sigma.
* `sigmaX`: Gaussian kernel standard deviation in X direction.
* `sigmaY`: Gaussian kernel standard deviation in Y direction; if sigmaY is zero, it is set to be equal to sigmaX, if both sigmas are zeros, they are computed from ksize.width and ksize.height, respectively (see `getGaussianKernel` for details); to fully control the result regardless of possible future modifications of all this semantics, it is recommended to specify all of ksize, sigmaX, and sigmaY.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.
* `hint`: Implementation modification flags. See `AlgorithmHint`

---
### `Cv2.BilateralFilter`
**Signature**: `void BilateralFilter(Mat src, Mat dst, int d, double sigmaColor, double sigmaSpace, int borderType)`

Applies the bilateral filter to an image.

**Detailed Remarks**:
The function applies bilateral filtering to the input image, as described in
https://homepages.inf.ed.ac.uk/rbf/CVonline/LOCAL_COPIES/MANDUCHI1/Bilateral_Filtering.html
bilateralFilter can reduce unwanted noise very well while keeping edges fairly sharp. However, it is
very slow compared to most filters.
_Sigma values_: For simplicity, you can set the 2 sigma values to be the same. If they are small (\<
10), the filter will not have much effect, whereas if they are large (\> 150), they will have a very
strong effect, making the image look "cartoonish".
_Filter size_: Large filters (d \> 5) are very slow, so it is recommended to use d=5 for real-time
applications, and perhaps d=9 for offline applications that need heavy noise filtering.
This filter does not work inplace.

**Parameters**:
* `src`: Source 8-bit or floating-point, 1-channel or 3-channel image.
* `dst`: Destination image of the same size and type as src .
* `d`: Diameter of each pixel neighborhood that is used during filtering. If it is non-positive, it is computed from sigmaSpace.
* `sigmaColor`: Filter sigma in the color space. A larger value of the parameter means that farther colors within the pixel neighborhood (see sigmaSpace) will be mixed together, resulting in larger areas of semi-equal color.
* `sigmaSpace`: Filter sigma in the coordinate space. A larger value of the parameter means that farther pixels will influence each other as long as their colors are close enough (see sigmaColor ). When d\>0, it specifies the neighborhood size regardless of sigmaSpace. Otherwise, d is proportional to sigmaSpace.
* `borderType`: border mode used to extrapolate pixels outside of the image, see `BorderTypes`

---
### `Cv2.BoxFilter`
**Signature**: `void BoxFilter(Mat src, Mat dst, int ddepth, Size ksize, Point anchor, bool normalize, int borderType)`

Blurs an image using the box filter.

**Detailed Remarks**:
The function smooths an image using the kernel:
[see mathematical formula in OpenCV docs]
where
[see mathematical formula in OpenCV docs]
Unnormalized box filter is useful for computing various integral characteristics over each pixel
neighborhood, such as covariance matrices of image derivatives (used in dense optical flow
algorithms, and so on). If you need to compute pixel sums over variable-size windows, use `integral`.
**See also**: blur, bilateralFilter, GaussianBlur, medianBlur, integral

**Parameters**:
* `src`: input image.
* `dst`: output image of the same size and type as src.
* `ddepth`: the output image depth (-1 to use src.depth()).
* `ksize`: blurring kernel size.
* `anchor`: anchor point; default value Point(-1,-1) means that the anchor is at the kernel center.
* `normalize`: flag, specifying whether the kernel is normalized by its area or not.
* `borderType`: border mode used to extrapolate pixels outside of the image, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.SqrBoxFilter`
**Signature**: `void SqrBoxFilter(Mat src, Mat dst, int ddepth, Size ksize, Point anchor, bool normalize, int borderType)`

Calculates the normalized sum of squares of the pixel values overlapping the filter.

**Detailed Remarks**:
For every pixel formula in the source image, the function calculates the sum of squares of those neighboring
pixel values which overlap the filter placed over the pixel formula.
The unnormalized square box filter can be useful in computing local image statistics such as the local
variance and standard deviation around the neighborhood of a pixel.
**See also**: boxFilter

**Parameters**:
* `src`: input image
* `dst`: output image of the same size and type as src
* `ddepth`: the output image depth (-1 to use src.depth())
* `ksize`: kernel size
* `anchor`: kernel anchor point. The default value of Point(-1, -1) denotes that the anchor is at the kernel center.
* `normalize`: flag, specifying whether the kernel is to be normalized by it's area or not.
* `borderType`: border mode used to extrapolate pixels outside of the image, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.Blur`
**Signature**: `void Blur(Mat src, Mat dst, Size ksize, Point anchor, int borderType)`

Blurs an image using the normalized box filter.

**Detailed Remarks**:
The function smooths an image using the kernel:
[see mathematical formula in OpenCV docs]
The call `blur(src, dst, ksize, anchor, borderType)` is equivalent to `boxFilter(src, dst, src.type(), ksize,
anchor, true, borderType)`.
**See also**: boxFilter, bilateralFilter, GaussianBlur, medianBlur

**Parameters**:
* `src`: input image; it can have any number of channels, which are processed independently, but the depth should be CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
* `dst`: output image of the same size and type as src.
* `ksize`: blurring kernel size.
* `anchor`: anchor point; default value Point(-1,-1) means that the anchor is at the kernel center.
* `borderType`: border mode used to extrapolate pixels outside of the image, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.StackBlur`
**Signature**: `void StackBlur(Mat src, Mat dst, Size ksize)`

Blurs an image using the stackBlur.

**Detailed Remarks**:
The function applies and stackBlur to an image.
stackBlur can generate similar results as Gaussian blur, and the time consumption does not increase with the increase of kernel size.
It creates a kind of moving stack of colors whilst scanning through the image. Thereby it just has to add one new block of color to the right side
of the stack and remove the leftmost color. The remaining colors on the topmost layer of the stack are either added on or reduced by one,
depending on if they are on the right or on the left side of the stack. The only supported borderType is BORDER_REPLICATE.
Original paper was proposed by Mario Klingemann, which can be found https://underdestruction.com/2004/02/25/stackblur-2004.

**Parameters**:
* `src`: input image. The number of channels can be arbitrary, but the depth should be one of CV_8U, CV_16U, CV_16S or CV_32F.
* `dst`: output image of the same size and type as src.
* `ksize`: stack-blurring kernel size. The ksize.width and ksize.height can differ but they both must be positive and odd.

---
### `Cv2.Filter2D`
**Signature**: `void Filter2D(Mat src, Mat dst, int ddepth, Mat kernel, Point anchor, double delta, int borderType)`

Convolves an image with the kernel.

**Detailed Remarks**:
The function applies an arbitrary linear filter to an image. In-place operation is supported. When
the aperture is partially outside the image, the function interpolates outlier pixel values
according to the specified border mode.
The function does actually compute correlation, not the convolution:
[see mathematical formula in OpenCV docs]
That is, the kernel is not mirrored around the anchor point. If you need a real convolution, flip
the kernel using `flip` and set the new anchor to `(kernel.cols - anchor.x - 1, kernel.rows -
anchor.y - 1)`.
The function uses the DFT-based algorithm in case of sufficiently large kernels (~`11 x 11` or
larger) and the direct algorithm for small kernels.
**See also**: sepFilter2D, dft, matchTemplate

**Parameters**:
* `src`: input image.
* `dst`: output image of the same size and the same number of channels as src.
* `ddepth`: desired depth of the destination image, see combinations
* `kernel`: convolution kernel (or rather a correlation kernel), a single-channel floating point matrix; if you want to apply different kernels to different channels, split the image into separate color planes using split and process them individually.
* `anchor`: anchor of the kernel that indicates the relative position of a filtered point within the kernel; the anchor should lie within the kernel; default value (-1,-1) means that the anchor is at the kernel center.
* `delta`: optional value added to the filtered pixels before storing them in dst.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.Filter2D`
**Signature**: `void Filter2D(Mat src, Mat dst, Mat kernel, Filter2DParams? @params)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `kernel`: The kernel parameter.
* `params`: The @params parameter.

---
### `Cv2.SepFilter2D`
**Signature**: `void SepFilter2D(Mat src, Mat dst, int ddepth, Mat kernelX, Mat kernelY, Point anchor, double delta, int borderType)`

Applies a separable linear filter to an image.

**Detailed Remarks**:
The function applies a separable linear filter to the image. That is, first, every row of src is
filtered with the 1D kernel kernelX. Then, every column of the result is filtered with the 1D
kernel kernelY. The final result shifted by delta is stored in dst .
**See also**: filter2D, Sobel, GaussianBlur, boxFilter, blur

**Parameters**:
* `src`: Source image.
* `dst`: Destination image of the same size and the same number of channels as src .
* `ddepth`: Destination image depth, see combinations
* `kernelX`: Coefficients for filtering each row.
* `kernelY`: Coefficients for filtering each column.
* `anchor`: Anchor position within the kernel. The default value formula means that the anchor is at the kernel center.
* `delta`: Value added to the filtered results before storing them.
* `borderType`: Pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.Sobel`
**Signature**: `void Sobel(Mat src, Mat dst, int ddepth, int dx, int dy, int ksize, double scale, double delta, int borderType)`

Calculates the first, second, third, or mixed image derivatives using an extended Sobel operator.

**Detailed Remarks**:
In all cases except one, the formula separable kernel is used to
calculate the derivative. When formula, the formula or formula
kernel is used (that is, no Gaussian smoothing is done). `ksize = 1` can only be used for the first
or the second x- or y- derivatives.
There is also the special value `ksize = `FILTER_SCHARR` (-1)` that corresponds to the formula Scharr
filter that may give more accurate results than the formula Sobel. The Scharr aperture is
[see mathematical formula in OpenCV docs]
for the x-derivative, or transposed for the y-derivative.
The function calculates an image derivative by convolving the image with the appropriate kernel:
[see mathematical formula in OpenCV docs]
The Sobel operators combine Gaussian smoothing and differentiation, so the result is more or less
resistant to the noise. Most often, the function is called with ( xorder = 1, yorder = 0, ksize = 3)
or ( xorder = 0, yorder = 1, ksize = 3) to calculate the first x- or y- image derivative. The first
case corresponds to a kernel of:
[see mathematical formula in OpenCV docs]
The second case corresponds to a kernel of:
[see mathematical formula in OpenCV docs]
**See also**: Scharr, Laplacian, sepFilter2D, filter2D, GaussianBlur, cartToPolar

**Parameters**:
* `src`: input image.
* `dst`: output image of the same size and the same number of channels as src .
* `ddepth`: output image depth, see combinations; in the case of 8-bit input images it will result in truncated derivatives.
* `dx`: order of the derivative x.
* `dy`: order of the derivative y.
* `ksize`: size of the extended Sobel kernel; it must be 1, 3, 5, or 7.
* `scale`: optional scale factor for the computed derivative values; by default, no scaling is applied (see `getDerivKernels` for details).
* `delta`: optional delta value that is added to the results prior to storing them in dst.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.SpatialGradient`
**Signature**: `void SpatialGradient(Mat src, Mat dx, Mat dy, int ksize, int borderType)`

Calculates the first order image derivative in both x and y using a Sobel operator

**Detailed Remarks**:
Equivalent to calling:

```csharp
Cv2.Sobel(src, dx, 3, 1, 0, 3); // CV_16SC1 = 3
Cv2.Sobel(src, dy, 3, 0, 1, 3);
```

**See also**: Sobel

**Parameters**:
* `src`: input image.
* `dx`: output image with first-order derivative in x.
* `dy`: output image with first-order derivative in y.
* `ksize`: size of Sobel kernel. It must be 3.
* `borderType`: pixel extrapolation method, see `BorderTypes`. Only `BORDER_DEFAULT`=#BORDER_REFLECT_101 and `BORDER_REPLICATE` are supported.

---
### `Cv2.Scharr`
**Signature**: `void Scharr(Mat src, Mat dst, int ddepth, int dx, int dy, double scale, double delta, int borderType)`

Calculates the first x- or y- image derivative using Scharr operator.

**Detailed Remarks**:
The function computes the first x- or y- spatial image derivative using the Scharr operator. The
call
[see mathematical formula in OpenCV docs]
is equivalent to
[see mathematical formula in OpenCV docs]
**See also**: cartToPolar

**Parameters**:
* `src`: input image.
* `dst`: output image of the same size and the same number of channels as src.
* `ddepth`: output image depth, see combinations
* `dx`: order of the derivative x.
* `dy`: order of the derivative y.
* `scale`: optional scale factor for the computed derivative values; by default, no scaling is applied (see `getDerivKernels` for details).
* `delta`: optional delta value that is added to the results prior to storing them in dst.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.Laplacian`
**Signature**: `void Laplacian(Mat src, Mat dst, int ddepth, int ksize, double scale, double delta, int borderType)`

Calculates the Laplacian of an image.

**Detailed Remarks**:
The function calculates the Laplacian of the source image by adding up the second x and y
derivatives calculated using the Sobel operator:
[see mathematical formula in OpenCV docs]
This is done when `ksize > 1`. When `ksize == 1`, the Laplacian is computed by filtering the image
with the following formula aperture:
[see mathematical formula in OpenCV docs]
**See also**: Sobel, Scharr

**Parameters**:
* `src`: Source image.
* `dst`: Destination image of the same size and the same number of channels as src .
* `ddepth`: Desired depth of the destination image, see combinations.
* `ksize`: Aperture size used to compute the second-derivative filters. See `getDerivKernels` for details. The size must be positive and odd.
* `scale`: Optional scale factor for the computed Laplacian values. By default, no scaling is applied. See `getDerivKernels` for details.
* `delta`: Optional delta value that is added to the results prior to storing them in dst .
* `borderType`: Pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.Canny`
**Signature**: `void Canny(Mat image, Mat edges, double threshold1, double threshold2, int apertureSize, bool L2gradient)`

Finds edges in an image using the Canny algorithm [Canny86] .

**Detailed Remarks**:
The function finds edges in the input image and marks them in the output map edges using the
Canny algorithm. The smallest value between threshold1 and threshold2 is used for edge linking. The
largest value is used to find initial segments of strong edges. See
<https://en.wikipedia.org/wiki/Canny_edge_detector>

**Parameters**:
* `image`: 8-bit input image.
* `edges`: output edge map; single channels 8-bit image, which has the same size as image .
* `threshold1`: first threshold for the hysteresis procedure.
* `threshold2`: second threshold for the hysteresis procedure.
* `apertureSize`: aperture size for the Sobel operator.
* `L2gradient`: a flag, indicating whether a more accurate formula norm formula should be used to calculate the image gradient magnitude ( L2gradient=true ), or whether the default formula norm formula is enough ( L2gradient=false ).

---
### `Cv2.Canny`
**Signature**: `void Canny(Mat dx, Mat dy, Mat edges, double threshold1, double threshold2, bool L2gradient)`

**Detailed Remarks**:
Finds edges in an image using the Canny algorithm with custom image gradient.

**Parameters**:
* `dx`: 16-bit x derivative of input image (CV_16SC1 or CV_16SC3).
* `dy`: 16-bit y derivative of input image (same type as dx).
* `edges`: output edge map; single channels 8-bit image, which has the same size as image .
* `threshold1`: first threshold for the hysteresis procedure.
* `threshold2`: second threshold for the hysteresis procedure.
* `L2gradient`: a flag, indicating whether a more accurate formula norm formula should be used to calculate the image gradient magnitude ( L2gradient=true ), or whether the default formula norm formula is enough ( L2gradient=false ).

---
### `Cv2.CornerMinEigenVal`
**Signature**: `void CornerMinEigenVal(Mat src, Mat dst, int blockSize, int ksize, int borderType)`

Calculates the minimal eigenvalue of gradient matrices for corner detection.

**Detailed Remarks**:
The function is similar to cornerEigenValsAndVecs but it calculates and stores only the minimal
eigenvalue of the covariance matrix of derivatives, that is, formula in terms
of the formulae in the cornerEigenValsAndVecs description.

**Parameters**:
* `src`: Input single-channel 8-bit or floating-point image.
* `dst`: Image to store the minimal eigenvalues. It has the type CV_32FC1 and the same size as src .
* `blockSize`: Neighborhood size (see the details on `cornerEigenValsAndVecs` ).
* `ksize`: Aperture parameter for the Sobel operator.
* `borderType`: Pixel extrapolation method. See `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.CornerHarris`
**Signature**: `void CornerHarris(Mat src, Mat dst, int blockSize, int ksize, double k, int borderType)`

Harris corner detector.

**Detailed Remarks**:
The function runs the Harris corner detector on the image. Similarly to cornerMinEigenVal and
cornerEigenValsAndVecs , for each pixel formula it calculates a formula gradient covariance
matrix formula over a formula neighborhood. Then, it
computes the following characteristic:
[see mathematical formula in OpenCV docs]
Corners in the image can be found as the local maxima of this response map.

**Parameters**:
* `src`: Input single-channel 8-bit or floating-point image.
* `dst`: Image to store the Harris detector responses. It has the type CV_32FC1 and the same size as src .
* `blockSize`: Neighborhood size (see the details on `cornerEigenValsAndVecs` ).
* `ksize`: Aperture parameter for the Sobel operator.
* `k`: Harris detector free parameter. See the formula above.
* `borderType`: Pixel extrapolation method. See `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.CornerEigenValsAndVecs`
**Signature**: `void CornerEigenValsAndVecs(Mat src, Mat dst, int blockSize, int ksize, int borderType)`

Calculates eigenvalues and eigenvectors of image blocks for corner detection.

**Detailed Remarks**:
For every pixel formula , the function cornerEigenValsAndVecs considers a blockSize formula blockSize
neighborhood formula . It calculates the covariation matrix of derivatives over the neighborhood as:
[see mathematical formula in OpenCV docs]
where the derivatives are computed using the Sobel operator.
After that, it finds eigenvectors and eigenvalues of formula and stores them in the destination image as
formula where
-   formula are the non-sorted eigenvalues of formula
-   formula are the eigenvectors corresponding to formula
-   formula are the eigenvectors corresponding to formula
The output of the function can be used for robust edge or corner detection.
**See also**: cornerMinEigenVal, cornerHarris, preCornerDetect

**Parameters**:
* `src`: Input single-channel 8-bit or floating-point image.
* `dst`: Image to store the results. It has the same size as src and the type CV_32FC(6) .
* `blockSize`: Neighborhood size (see details below).
* `ksize`: Aperture parameter for the Sobel operator.
* `borderType`: Pixel extrapolation method. See `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.PreCornerDetect`
**Signature**: `void PreCornerDetect(Mat src, Mat dst, int ksize, int borderType)`

Calculates a feature map for corner detection.

**Detailed Remarks**:
The function calculates the complex spatial derivative-based function of the source image
[see mathematical formula in OpenCV docs]
where formula,formula are the first image derivatives, formula,formula are the second image
derivatives, and formula is the mixed derivative.
The corners can be found as local maximums of the functions, as shown below:

```csharp
using var corners = new Mat();
using var dilated_corners = new Mat();
Cv2.PreCornerDetect(image, corners, 3, (int)BorderTypes.Default);
// dilation with 3x3 rectangular structuring element
Cv2.Dilate(corners, dilated_corners, new Mat(), new Point(-1, -1), 1, (int)BorderTypes.Constant, new Scalar(0));
using var corner_mask = corners.Equal(dilated_corners);
```

**Parameters**:
* `src`: Source single-channel 8-bit of floating-point image.
* `dst`: Output image that has the type CV_32F and the same size as src .
* `ksize`: Aperture size of the Sobel .
* `borderType`: Pixel extrapolation method. See `BorderTypes`. `BORDER_WRAP` is not supported.

---
### `Cv2.CornerSubPix`
**Signature**: `void CornerSubPix(Mat image, Mat corners, Size winSize, Size zeroZone, TermCriteria criteria)`

Refines the corner locations.

**Detailed Remarks**:
The function iterates to find the sub-pixel accurate location of corners or radial saddle
points as described in **Citation**:  forstner1987fast, and as shown on the figure below.

Sub-pixel accurate corner locator is based on the observation that every vector from the center formula
to a point formula located within a neighborhood of formula is orthogonal to the image gradient at formula
subject to image and measurement noise. Consider the expression:
[see mathematical formula in OpenCV docs]
where formula is an image gradient at one of the points formula in a neighborhood of formula . The
value of formula is to be found so that formula is minimized. A system of equations may be set up
with formula set to zero:
[see mathematical formula in OpenCV docs]
where the gradients are summed within a neighborhood ("search window") of formula . Calling the first
gradient term formula and the second gradient term formula gives:
[see mathematical formula in OpenCV docs]
The algorithm sets the center of the neighborhood window at this new center formula and then iterates
until the center stays within a set threshold.

**Parameters**:
* `image`: Input single-channel, 8-bit or float image.
* `corners`: Initial coordinates of the input corners and refined coordinates provided for output.
* `winSize`: Half of the side length of the search window. For example, if winSize=Size(5,5) , then a formula search window is used.
* `zeroZone`: Half of the size of the dead region in the middle of the search zone over which the summation in the formula below is not done. It is used sometimes to avoid possible singularities of the autocorrelation matrix. The value of (-1,-1) indicates that there is no such a size.
* `criteria`: Criteria for termination of the iterative process of corner refinement. That is, the process of corner position refinement stops either after criteria.maxCount iterations or when the corner position moves by less than criteria.epsilon on some iteration.

---
### `Cv2.HoughLines`
**Signature**: `void HoughLines(Mat image, Mat lines, double rho, double theta, int threshold, double srn, double stn, double min_theta, double max_theta, bool use_edgeval)`

Finds lines in a binary image using the standard Hough transform.

**Detailed Remarks**:
The function implements the standard or standard multi-scale Hough transform algorithm for line
detection. See <https://homepages.inf.ed.ac.uk/rbf/HIPR2/hough.htm> for a good explanation of Hough
transform.

**Parameters**:
* `image`: 8-bit, single-channel binary source image. The image may be modified by the function.
* `lines`: Output vector of lines. Each line is represented by a 2 or 3 element vector formula or formula, where formula is the distance from the coordinate origin formula (top-left corner of the image), formula is the line rotation angle in radians ( formula ), and formula is the value of accumulator.
* `rho`: Distance resolution of the accumulator in pixels.
* `theta`: Angle resolution of the accumulator in radians.
* `threshold`: Accumulator threshold parameter. Only those lines are returned that get enough votes ( formula ).
* `srn`: For the multi-scale Hough transform, it is a divisor for the distance resolution rho. The coarse accumulator distance resolution is rho and the accurate accumulator resolution is rho/srn. If both srn=0 and stn=0, the classical Hough transform is used. Otherwise, both these parameters should be positive.
* `stn`: For the multi-scale Hough transform, it is a divisor for the distance resolution theta.
* `min_theta`: For standard and multi-scale Hough transform, minimum angle to check for lines. Must fall between 0 and max_theta.
* `max_theta`: For standard and multi-scale Hough transform, an upper bound for the angle. Must fall between min_theta and CV_PI. The actual maximum angle in the accumulator may be slightly less than max_theta, depending on the parameters min_theta and theta.
* `use_edgeval`: True if you want to use weighted Hough transform.

---
### `Cv2.HoughLinesP`
**Signature**: `void HoughLinesP(Mat image, Mat lines, double rho, double theta, int threshold, double minLineLength, double maxLineGap)`

Finds line segments in a binary image using the probabilistic Hough transform.

**Detailed Remarks**:
The function implements the probabilistic Hough transform algorithm for line detection, described
in **Citation**:  Matas00
See the line detection example below:
See example in OpenCV documentation. This is a sample picture the function parameters have been tuned for:

And this is the output of the above program in case of the probabilistic Hough transform:

**See also**: LineSegmentDetector

**Parameters**:
* `image`: 8-bit, single-channel binary source image. The image may be modified by the function.
* `lines`: Output vector of lines. Each line is represented by a 4-element vector formula , where formula and formula are the ending points of each detected line segment.
* `rho`: Distance resolution of the accumulator in pixels.
* `theta`: Angle resolution of the accumulator in radians.
* `threshold`: Accumulator threshold parameter. Only those lines are returned that get enough votes ( formula ).
* `minLineLength`: Minimum line length. Line segments shorter than that are rejected.
* `maxLineGap`: Maximum allowed gap between points on the same line to link them.

---
### `Cv2.HoughLinesPointSet`
**Signature**: `void HoughLinesPointSet(Mat point, Mat lines, int lines_max, int threshold, double min_rho, double max_rho, double rho_step, double min_theta, double max_theta, double theta_step)`

Finds lines in a set of points using the standard Hough transform.

**Detailed Remarks**:
The function finds lines in a set of points using a modification of the Hough transform.
See example in OpenCV documentation. **Parameters**:
* `point`: Input vector of points. Each vector must be encoded as a Point vector formula. Type must be CV_32FC2 or CV_32SC2.
* `lines`: Output vector of found lines. Each vector is encoded as a Vec3d[] formula. The larger the value of 'votes', the higher the reliability of the Hough line.
* `lines_max`: Max count of Hough lines.
* `threshold`: Accumulator threshold parameter. Only those lines are returned that get enough votes ( formula ).
* `min_rho`: Minimum value for formula for the accumulator (Note: formula can be negative. The absolute value formula is the distance of a line to the origin.).
* `max_rho`: Maximum value for formula for the accumulator.
* `rho_step`: Distance resolution of the accumulator.
* `min_theta`: Minimum angle value of the accumulator in radians.
* `max_theta`: Upper bound for the angle value of the accumulator in radians. The actual maximum angle may be slightly less than max_theta, depending on the parameters min_theta and theta_step.
* `theta_step`: Angle resolution of the accumulator in radians.

---
### `Cv2.HoughCircles`
**Signature**: `void HoughCircles(Mat image, Mat circles, int method, double dp, double minDist, double param1, double param2, int minRadius, int maxRadius)`

Finds circles in a grayscale image using the Hough transform.

**Detailed Remarks**:
The function finds circles in a grayscale image using a modification of the Hough transform.
Example: :
See example in OpenCV documentation. .: info Note
Usually the function detects the centers of circles well. However, it may fail to find correct
radii. You can assist to the function by specifying the radius range ( minRadius and maxRadius ) if
you know it. Or, in the case of `HOUGH_GRADIENT` method you may set maxRadius to a negative number
to return centers only without radius search, and find the correct radius using an additional procedure.
It also helps to smooth image a bit unless it's already soft. For example,
GaussianBlur() with 7x7 kernel and 1.5x1.5 sigma or similar blurring may help.
**See also**: fitEllipse, minEnclosingCircle
.:

**Parameters**:
* `image`: 8-bit, single-channel, grayscale input image.
* `circles`: Output vector of found circles. Each vector is encoded as  3 or 4 element floating-point vector formula or formula .
* `method`: Detection method, see `HoughModes`. The available methods are `HOUGH_GRADIENT` and `HOUGH_GRADIENT_ALT`.
* `dp`: Inverse ratio of the accumulator resolution to the image resolution. For example, if dp=1 , the accumulator has the same resolution as the input image. If dp=2 , the accumulator has half as big width and height. For `HOUGH_GRADIENT_ALT` the recommended value is dp=1.5, unless some small very circles need to be detected.
* `minDist`: Minimum distance between the centers of the detected circles. If the parameter is too small, multiple neighbor circles may be falsely detected in addition to a true one. If it is too large, some circles may be missed.
* `param1`: First method-specific parameter. In case of `HOUGH_GRADIENT` and `HOUGH_GRADIENT_ALT`, it is the higher threshold of the two passed to the Canny edge detector (the lower one is twice smaller). Note that `HOUGH_GRADIENT_ALT` uses `Scharr` algorithm to compute image derivatives, so the threshold value should normally be higher, such as 300 or normally exposed and contrasty images.
* `param2`: Second method-specific parameter. In case of `HOUGH_GRADIENT`, it is the accumulator threshold for the circle centers at the detection stage. The smaller it is, the more false circles may be detected. Circles, corresponding to the larger accumulator values, will be returned first. In the case of `HOUGH_GRADIENT_ALT` algorithm, this is the circle "perfectness" measure. The closer it to 1, the better shaped circles algorithm selects. In most cases 0.9 should be fine. If you want get better detection of small circles, you may decrease it to 0.85, 0.8 or even less. But then also try to limit the search range [minRadius, maxRadius] to avoid many false circles.
* `minRadius`: Minimum circle radius.
* `maxRadius`: Maximum circle radius. If <= 0, uses the maximum image dimension. If < 0, `HOUGH_GRADIENT` returns centers without finding the radius. `HOUGH_GRADIENT_ALT` always computes circle radiuses.

---
### `Cv2.Erode`
**Signature**: `void Erode(Mat src, Mat dst, Mat kernel, Point anchor, int iterations, int borderType, Scalar borderValue)`

Erodes an image by using a specific structuring element.

**Detailed Remarks**:
The function erodes the source image using the specified structuring element that determines the
shape of a pixel neighborhood over which the minimum is taken:
[see mathematical formula in OpenCV docs]
The function supports the in-place mode. Erosion can be applied several ( iterations ) times. In
case of multi-channel images, each channel is processed independently.
**See also**: dilate, morphologyEx, getStructuringElement

**Parameters**:
* `src`: input image; the number of channels can be arbitrary, but the depth should be one of CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
* `dst`: output image of the same size and type as src.
* `kernel`: structuring element used for erosion; if `kernel=Mat()`, a `3 x 3` rectangular structuring element is used. Kernel can be created using `getStructuringElement`.
* `anchor`: position of the anchor within the element; default value (-1, -1) means that the anchor is at the element center.
* `iterations`: number of times erosion is applied.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.
* `borderValue`: border value in case of a constant border

---
### `Cv2.Dilate`
**Signature**: `void Dilate(Mat src, Mat dst, Mat kernel, Point anchor, int iterations, int borderType, Scalar borderValue)`

Dilates an image by using a specific structuring element.

**Detailed Remarks**:
The function dilates the source image using the specified structuring element that determines the
shape of a pixel neighborhood over which the maximum is taken:
[see mathematical formula in OpenCV docs]
The function supports the in-place mode. Dilation can be applied several ( iterations ) times. In
case of multi-channel images, each channel is processed independently.
**See also**: erode, morphologyEx, getStructuringElement

**Parameters**:
* `src`: input image; the number of channels can be arbitrary, but the depth should be one of CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
* `dst`: output image of the same size and type as src.
* `kernel`: structuring element used for dilation; if `kernel=Mat()`, a `3 x 3` rectangular structuring element is used. Kernel can be created using `getStructuringElement`
* `anchor`: position of the anchor within the element; default value (-1, -1) means that the anchor is at the element center.
* `iterations`: number of times dilation is applied.
* `borderType`: pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.
* `borderValue`: border value in case of a constant border

---
### `Cv2.MorphologyEx`
**Signature**: `void MorphologyEx(Mat src, Mat dst, int op, Mat kernel, Point anchor, int iterations, int borderType, Scalar borderValue)`

Performs advanced morphological transformations.

**Detailed Remarks**:
The function morphologyEx can perform advanced morphological transformations using an erosion and dilation as
basic operations.
Any of the operations can be done in-place. In case of multi-channel images, each channel is
processed independently.
**See also**: dilate, erode, getStructuringElement
.: info Note
The number of iterations is the number of times erosion or dilatation operation will be applied.
For instance, an opening operation (`MORPH_OPEN`) with two iterations is equivalent to apply
successively: erode -> erode -> dilate -> dilate (and not erode -> dilate -> erode -> dilate).
.:

**Parameters**:
* `src`: Source image. The number of channels can be arbitrary. The depth should be one of CV_8U, CV_16U, CV_16S, CV_32F or CV_64F.
* `dst`: Destination image of the same size and type as source image.
* `op`: Type of a morphological operation, see `MorphTypes`
* `kernel`: Structuring element. It can be created using `getStructuringElement`.
* `anchor`: Anchor position with the kernel. Negative values mean that the anchor is at the kernel center.
* `iterations`: Number of times erosion and dilation are applied.
* `borderType`: Pixel extrapolation method, see `BorderTypes`. `BORDER_WRAP` is not supported.
* `borderValue`: Border value in case of a constant border. The default value has a special meaning.

---
### `Cv2.Resize`
**Signature**: `void Resize(Mat src, Mat dst, Size dsize, double fx, double fy, int interpolation)`

Resizes an image.

**Detailed Remarks**:
The function resize resizes the image src down to or up to the specified size. Note that the
initial dst type or size are not taken into account. Instead, the size and type are derived from
the `src`,`dsize`,`fx`, and `fy`. If you want to resize src so that it fits the pre-created dst,
you may call the function as follows:

```csharp
// explicitly specify dsize=dst.size(); fx and fy will be computed from that.
Cv2.Resize(src, dst, new Size(dst.Cols, dst.Rows), 0, 0, interpolation);
```

If you want to decimate the image by factor of 2 in each direction, you can call the function this
way:

```csharp
// specify fx and fy and let the function compute the destination image size.
Cv2.Resize(src, dst, new Size(0, 0), 0.5, 0.5, interpolation);
```

To shrink an image, it will generally look best with `INTER_AREA` interpolation, whereas to
enlarge an image, it will generally look best with `INTER_CUBIC` (slow) or `INTER_LINEAR`
(faster but still looks OK).
**See also**: warpAffine, warpPerspective, remap

**Parameters**:
* `src`: input image.
* `dst`: output image; it has the size dsize (when it is non-zero) or the size computed from src.size(), fx, and fy; the type of dst is the same as of src.
* `dsize`: output image size; if it equals zero (null), it is computed as: [see mathematical formula in OpenCV docs] Either dsize or both fx and fy must be non-zero.
* `fx`: scale factor along the horizontal axis; when it equals 0, it is computed as [see mathematical formula in OpenCV docs]
* `fy`: scale factor along the vertical axis; when it equals 0, it is computed as [see mathematical formula in OpenCV docs]
* `interpolation`: interpolation method, see `InterpolationFlags`

---
### `Cv2.WarpAffine`
**Signature**: `void WarpAffine(Mat src, Mat dst, Mat M, Size dsize, int flags, int borderMode, Scalar borderValue, AlgorithmHint hint)`

Applies an affine transformation to an image.

**Detailed Remarks**:
The function warpAffine transforms the source image using the specified matrix:
[see mathematical formula in OpenCV docs]
when the flag `WARP_INVERSE_MAP` is set. Otherwise, the transformation is first inverted
with `invertAffineTransform` and then put in the formula above instead of M. The function cannot
operate in-place.
**See also**: warpPerspective, resize, remap, getRectSubPix, transform

**Parameters**:
* `src`: input image.
* `dst`: output image that has the size dsize and the same type as src .
* `M`: formula transformation matrix.
* `dsize`: size of the output image.
* `flags`: combination of interpolation methods (see `InterpolationFlags`) and the optional flag `WARP_INVERSE_MAP` that means that M is the inverse transformation ( formula ).
* `borderMode`: pixel extrapolation method (see `BorderTypes`); when borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image corresponding to the "outliers" in the source image are not modified by the function.
* `borderValue`: value used in case of a constant border; by default, it is 0.
* `hint`: Implementation modification flags. Set `ALGO_HINT_APPROX` to use FP16 precision (if available) for linear calculation for faster speed. See `AlgorithmHint`.

---
### `Cv2.WarpPerspective`
**Signature**: `void WarpPerspective(Mat src, Mat dst, Mat M, Size dsize, int flags, int borderMode, Scalar borderValue, AlgorithmHint hint)`

Applies a perspective transformation to an image.

**Detailed Remarks**:
The function warpPerspective transforms the source image using the specified matrix:

$$\texttt{dst} (x,y) =  \texttt{src} \left ( \frac{M_{11} x + M_{12} y + M_{13}}{M_{31} x + M_{32} y + M_{33}} ,
\frac{M_{21} x + M_{22} y + M_{23}}{M_{31} x + M_{32} y + M_{33}} \right )$$

when the flag `WARP_INVERSE_MAP` is set. Otherwise, the transformation is first inverted with invert
and then put in the formula above instead of M. The function cannot operate in-place.
**See also**: warpAffine, resize, remap, getRectSubPix, perspectiveTransform

**Parameters**:
* `src`: input image.
* `dst`: output image that has the size dsize and the same type as src .
* `M`: formula transformation matrix.
* `dsize`: size of the output image.
* `flags`: combination of interpolation methods (#INTER_LINEAR or `INTER_NEAREST`) and the optional flag `WARP_INVERSE_MAP`, that sets M as the inverse transformation ( formula ).
* `borderMode`: pixel extrapolation method (#BORDER_CONSTANT or `BORDER_REPLICATE`).
* `borderValue`: value used in case of a constant border; by default, it equals 0.
* `hint`: Implementation modification flags. Set `ALGO_HINT_APPROX` to use FP16 precision (if available) for linear calculation for faster speed. See `AlgorithmHint`.

---
### `Cv2.Remap`
**Signature**: `void Remap(Mat src, Mat dst, Mat map1, Mat map2, int interpolation, int borderMode, Scalar borderValue, AlgorithmHint hint)`

Applies a generic geometrical transformation to an image.

**Detailed Remarks**:
The function remap transforms the source image using the specified map:
[see mathematical formula in OpenCV docs]
with the WARP_RELATIVE_MAP flag :
[see mathematical formula in OpenCV docs]
where values of pixels with non-integer coordinates are computed using one of available
interpolation methods. formula and formula can be encoded as separate floating-point maps
in formula and formula respectively, or interleaved floating-point maps of formula in
formula, or fixed-point maps created by using `convertMaps`. The reason you might want to
convert from floating to fixed-point representations of a map is that they can yield much faster
(\~2x) remapping operations. In the converted case, formula contains pairs (cvFloor(x),
cvFloor(y)) and formula contains indices in a table of interpolation coefficients.
This function cannot operate in-place.
.: info Note

Due to current implementation limitations the size of an input and output images should be less than 32767x32767.
.:

**Parameters**:
* `src`: Source image.
* `dst`: Destination image. It has the same size as map1 and the same type as src .
* `map1`: The first map of either (x,y) points or just x values having the type CV_16SC2 , CV_32FC1, or CV_32FC2. See `convertMaps` for details on converting a floating point representation to fixed-point for speed.
* `map2`: The second map of y values having the type CV_16UC1, CV_32FC1, or none (empty map if map1 is (x,y) points), respectively.
* `interpolation`: Interpolation method (see `InterpolationFlags`). The methods `INTER_AREA` `INTER_LINEAR_EXACT` and `INTER_NEAREST_EXACT` are not supported by this function. The extra flag WARP_RELATIVE_MAP can be ORed to the interpolation method (e.g. INTER_LINEAR | WARP_RELATIVE_MAP)
* `borderMode`: Pixel extrapolation method (see `BorderTypes`). When borderMode=#BORDER_TRANSPARENT, it means that the pixels in the destination image that corresponds to the "outliers" in the source image are not modified by the function.
* `borderValue`: Value used in case of a constant border. By default, it is 0.
* `hint`: Implementation modification flags. Set `ALGO_HINT_APPROX` to use FP16 precision (if available) for linear calculation for faster speed. See `AlgorithmHint`.

---
### `Cv2.ConvertMaps`
**Signature**: `void ConvertMaps(Mat map1, Mat map2, Mat dstmap1, Mat dstmap2, int dstmap1type, bool nninterpolation)`

Converts image transformation maps from one representation to another.

**Detailed Remarks**:
The function converts a pair of maps for remap from one representation to another. The following
options ( (map1.type(), map2.type()) formula (dstmap1.type(), dstmap2.type()) ) are
supported:
- formula. This is the
most frequently used conversion operation, in which the original floating-point maps (see `remap`)
are converted to a more compact and much faster fixed-point representation. The first output array
contains the rounded coordinates and the second array (created only when nninterpolation=false )
contains indices in the interpolation tables.
- formula. The same as above but
the original maps are stored in one 2-channel matrix.
- Reverse conversion. Obviously, the reconstructed floating-point maps will not be exactly the same
as the originals.
**See also**: remap, undistort, initUndistortRectifyMap

**Parameters**:
* `map1`: The first input map of type CV_16SC2, CV_32FC1, or CV_32FC2 .
* `map2`: The second input map of type CV_16UC1, CV_32FC1, or none (empty matrix), respectively.
* `dstmap1`: The first output map that has the type dstmap1type and the same size as src .
* `dstmap2`: The second output map.
* `dstmap1type`: Type of the first output map that should be CV_16SC2, CV_32FC1, or CV_32FC2 .
* `nninterpolation`: Flag indicating whether the fixed-point maps are used for the nearest-neighbor or for a more complex interpolation.

---
### `Cv2.Undistort`
**Signature**: `void Undistort(Mat src, Mat dst, Mat cameraMatrix, Mat distCoeffs, Mat? newCameraMatrix)`

Transforms an image to compensate for lens distortion.

**Detailed Remarks**:
The function transforms an image to compensate radial and tangential lens distortion.
The function is simply a combination of `initUndistortRectifyMap` (with unity R ) and `remap`
(with bilinear interpolation). See the former function for details of the transformation being
performed.
Those pixels in the destination image, for which there is no correspondent pixels in the source
image, are filled with zeros (black color).
A particular subset of the source image that will be visible in the corrected image can be regulated
by newCameraMatrix. You can use `getOptimalNewCameraMatrix` to compute the appropriate
newCameraMatrix depending on your requirements.
The camera matrix and the distortion parameters can be determined using `CalibrateCamera`. If
the resolution of images is different from the resolution used at the calibration stage, `f_x,
f_y, c_xformulac_y` need to be scaled accordingly, while the distortion coefficients remain
the same.

**Parameters**:
* `src`: Input (distorted) image.
* `dst`: Output (corrected) image that has the same size and type as src .
* `cameraMatrix`: Input camera matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `newCameraMatrix`: Camera matrix of the distorted image. By default, it is the same as cameraMatrix but you may additionally scale and shift the result by using a different matrix.

---
### `Cv2.InitUndistortRectifyMap`
**Signature**: `void InitUndistortRectifyMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, Size size, int m1type, Mat map1, Mat map2)`

Computes the undistortion and rectification transformation map.

**Detailed Remarks**:
The function computes the joint undistortion and rectification transformation and represents the
result in the form of maps for `remap`. The undistorted image looks like original, as if it is
captured with a camera using the camera matrix =newCameraMatrix and zero distortion. In case of a
monocular camera, newCameraMatrix is usually equal to cameraMatrix, or it can be computed by
`getOptimalNewCameraMatrix` for a better control over scaling. In case of a stereo camera,
newCameraMatrix is normally set to P1 or P2 computed by `stereoRectify` .
Also, this new camera is oriented differently in the coordinate space, according to R. That, for
example, helps to align two heads of a stereo camera so that the epipolar lines on both images
become horizontal and have the same y- coordinate (in case of a horizontally aligned stereo camera).
The function actually builds the maps for the inverse mapping algorithm that is used by `remap`. That
is, for each pixel formula in the destination (corrected and rectified) image, the function
computes the corresponding coordinates in the source image (that is, in the original image from
camera). The following process is applied:

$$
\begin{array}{l}
x  \leftarrow (u - {c'}_x)/{f'}_x  \\
y  \leftarrow (v - {c'}_y)/{f'}_y  \\
{[X\,Y\,W]} ^T  \leftarrow R^{-1}\cdot[x \, y \, 1]^T  \\
x'  \leftarrow X/W  \\
y'  \leftarrow Y/W  \\
r^2  \leftarrow x'^2 + y'^2 \\
x''  \leftarrow x' \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}
+ 2p_1 x' y' + p_2(r^2 + 2 x'^2)  + s_1 r^2 + s_2 r^4\\
y''  \leftarrow y' \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}
+ p_1 (r^2 + 2 y'^2) + 2 p_2 x' y' + s_3 r^2 + s_4 r^4 \\
s\begin{bmatrix} x''' \\ y''' \\ 1 \end{bmatrix} =
\vecthreethree{R_{33}(\tau_x, \tau_y)}{0}{-R_{13}((\tau_x, \tau_y)}
{0}{R_{33}(\tau_x, \tau_y)}{-R_{23}(\tau_x, \tau_y)}
{0}{0}{1} R(\tau_x, \tau_y) \begin{bmatrix} x'' \\ y'' \\ 1 \end{bmatrix}\\
map_x(u,v)  \leftarrow x''' f_x + c_x  \\
map_y(u,v)  \leftarrow y''' f_y + c_y
\end{array}
$$

where formula
are the distortion coefficients.
In case of a stereo camera, this function is called twice: once for each camera head, after
`stereoRectify`, which in its turn is called after `stereoCalibrate`. But if the stereo camera
was not calibrated, it is still possible to compute the rectification transformations directly from
the fundamental matrix using `stereoRectifyUncalibrated`. For each camera, the function computes
homography H as the rectification transformation in a pixel domain, not a rotation matrix R in 3D
space. R can be computed from H as
[see mathematical formula in OpenCV docs]
where cameraMatrix can be chosen arbitrarily.

**Parameters**:
* `cameraMatrix`: Input camera matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `R`: Optional rectification transformation in the object space (3x3 matrix). R1 or R2 , computed by `stereoRectify` can be passed here. If the matrix is empty, the identity transformation is assumed. In `initUndistortRectifyMap` R assumed to be an identity matrix.
* `newCameraMatrix`: New camera matrix formula.
* `size`: Undistorted image size.
* `m1type`: Type of the first output map that can be CV_32FC1, CV_32FC2 or CV_16SC2, see `convertMaps`
* `map1`: The first output map.
* `map2`: The second output map.

---
### `Cv2.InitInverseRectificationMap`
**Signature**: `void InitInverseRectificationMap(Mat cameraMatrix, Mat distCoeffs, Mat R, Mat newCameraMatrix, Size size, int m1type, Mat map1, Mat map2)`

Computes the projection and inverse-rectification transformation map. In essense, this is the inverse of `initUndistortRectifyMap` to accomodate stereo-rectification of projectors ('inverse-cameras') in projector-camera pairs.

**Detailed Remarks**:
The function computes the joint projection and inverse rectification transformation and represents the
result in the form of maps for `remap`. The projected image looks like a distorted version of the original which,
once projected by a projector, should visually match the original. In case of a monocular camera, newCameraMatrix
is usually equal to cameraMatrix, or it can be computed by
`getOptimalNewCameraMatrix` for a better control over scaling. In case of a projector-camera pair,
newCameraMatrix is normally set to P1 or P2 computed by `stereoRectify` .
The projector is oriented differently in the coordinate space, according to R. In case of projector-camera pairs,
this helps align the projector (in the same manner as `initUndistortRectifyMap` for the camera) to create a stereo-rectified pair. This
allows epipolar lines on both images to become horizontal and have the same y-coordinate (in case of a horizontally aligned projector-camera pair).
The function builds the maps for the inverse mapping algorithm that is used by `remap`. That
is, for each pixel formula in the destination (projected and inverse-rectified) image, the function
computes the corresponding coordinates in the source image (that is, in the original digital image). The following process is applied:

$$
\begin{array}{l}
\text{newCameraMatrix}\\
x  \leftarrow (u - {c'}_x)/{f'}_x  \\
y  \leftarrow (v - {c'}_y)/{f'}_y  \\
\\\text{Undistortion}
\\\scriptsize{\textit{though equation shown is for radial undistortion, function implements undistortPoints()}}\\
r^2  \leftarrow x^2 + y^2 \\
\theta \leftarrow \frac{1 + k_1 r^2 + k_2 r^4 + k_3 r^6}{1 + k_4 r^2 + k_5 r^4 + k_6 r^6}\\
x' \leftarrow \frac{x}{\theta} \\
y'  \leftarrow \frac{y}{\theta} \\
\\\text{Rectification}\\
{[X\,Y\,W]} ^T  \leftarrow R\cdot[x' \, y' \, 1]^T  \\
x''  \leftarrow X/W  \\
y''  \leftarrow Y/W  \\
\\\text{cameraMatrix}\\
map_x(u,v)  \leftarrow x'' f_x + c_x  \\
map_y(u,v)  \leftarrow y'' f_y + c_y
\end{array}
$$

where formula
are the distortion coefficients vector distCoeffs.
In case of a stereo-rectified projector-camera pair, this function is called for the projector while `initUndistortRectifyMap` is called for the camera head.
This is done after `stereoRectify`, which in turn is called after `stereoCalibrate`. If the projector-camera pair
is not calibrated, it is still possible to compute the rectification transformations directly from
the fundamental matrix using `stereoRectifyUncalibrated`. For the projector and camera, the function computes
homography H as the rectification transformation in a pixel domain, not a rotation matrix R in 3D
space. R can be computed from H as
[see mathematical formula in OpenCV docs]
where cameraMatrix can be chosen arbitrarily.

**Parameters**:
* `cameraMatrix`: Input camera matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `R`: Optional rectification transformation in the object space (3x3 matrix). R1 or R2, computed by `stereoRectify` can be passed here. If the matrix is empty, the identity transformation is assumed.
* `newCameraMatrix`: New camera matrix formula.
* `size`: Distorted image size.
* `m1type`: Type of the first output map. Can be CV_32FC1, CV_32FC2 or CV_16SC2, see `convertMaps`
* `map1`: The first output map for `remap`.
* `map2`: The second output map for `remap`.

---
### `Cv2.FisheyeInitUndistortRectifyMap`
**Signature**: `void FisheyeInitUndistortRectifyMap(Mat K, Mat D, Mat R, Mat P, Size size, int m1type, Mat map1, Mat map2)`

Computes undistortion and rectification maps for image transform by remap(). If D is empty zero distortion is used, if R or P is empty identity matrixes are used.

**Parameters**:
* `K`: Camera intrinsic matrix formula.
* `D`: Input vector of distortion coefficients formula.
* `R`: Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3 1-channel or 1x1 3-channel
* `P`: New camera intrinsic matrix (3x3) or new projection matrix (3x4)
* `size`: Undistorted image size.
* `m1type`: Type of the first output map that can be CV_32FC1 or CV_16SC2 . See convertMaps() for details.
* `map1`: The first output map.
* `map2`: The second output map.

---
### `Cv2.FisheyeUndistortImage`
**Signature**: `void FisheyeUndistortImage(Mat distorted, Mat undistorted, Mat K, Mat D, Mat? Knew, Size new_size)`

Transforms an image to compensate for fisheye lens distortion.

**Parameters**:
* `distorted`: image with fisheye lens distortion.
* `undistorted`: Output image with compensated fisheye lens distortion.
* `K`: Camera intrinsic matrix formula.
* `D`: Input vector of distortion coefficients formula.
* `Knew`: Camera intrinsic matrix of the distorted image. By default, it is the identity matrix but you may additionally scale and shift the result by using a different matrix.
* `new_size`: the new size The function transforms an image to compensate radial and tangential lens distortion. The function is simply a combination of Cv2.fisheye.initUndistortRectifyMap (with unity R ) and remap (with bilinear interpolation). See the former function for details of the transformation being performed. See below the results of undistortImage. -   a\) result of undistort of perspective camera model (all possible coefficients (k_1, k_2, k_3, k_4, k_5, k_6) of distortion were optimized under calibration) -   b\) result of Cv2.fisheye.undistortImage of fisheye camera model (all possible coefficients (k_1, k_2, k_3, k_4) of fisheye distortion were optimized under calibration) -   c\) original image was captured with fisheye lens Pictures a) and b) almost the same. But if we consider points of image located far from the center of image, we can notice that on image a) these points are distorted.

---
### `Cv2.GetRectSubPix`
**Signature**: `void GetRectSubPix(Mat image, Size patchSize, Point2f center, Mat patch, int patchType)`

Retrieves a pixel rectangle from an image with sub-pixel accuracy.

**Detailed Remarks**:
The function getRectSubPix extracts pixels from src:
[see mathematical formula in OpenCV docs]
where the values of the pixels at non-integer coordinates are retrieved using bilinear
interpolation. Every channel of multi-channel images is processed independently. Also
the image should be a single channel or three channel image. While the center of the
rectangle must be inside the image, parts of the rectangle may be outside.
**See also**: warpAffine, warpPerspective

**Parameters**:
* `image`: Source image.
* `patchSize`: Size of the extracted patch.
* `center`: Floating point coordinates of the center of the extracted rectangle within the source image. The center must be inside the image.
* `patch`: Extracted patch that has the size patchSize and the same number of channels as src .
* `patchType`: Depth of the extracted pixels. By default, they have the same depth as src .

---
### `Cv2.WarpPolar`
**Signature**: `void WarpPolar(Mat src, Mat dst, Size dsize, Point2f center, double maxRadius, int flags)`

Remaps an image to polar or semilog-polar coordinates space

**Detailed Remarks**:
Transform the source image using the following transformation:

$$
dst(\rho , \phi ) = src(x,y)
$$

where

$$
\begin{array}{l}
\vec{I} = (x - center.x, \;y - center.y) \\
\phi = Kangle \cdot \texttt{angle} (\vec{I}) \\
\rho = \left\{\begin{matrix}
Klin \cdot \texttt{magnitude} (\vec{I}) & default \\
Klog \cdot log_e(\texttt{magnitude} (\vec{I})) & if \; semilog \\
\end{matrix}\right.
\end{array}
$$

and

$$
\begin{array}{l}
Kangle = dsize.height / 2\Pi \\
Klin = dsize.width / maxRadius \\
Klog = dsize.width / log_e(maxRadius) \\
\end{array}
$$


 Linear vs semilog mapping
Polar mapping can be linear or semi-log. Add one of `WarpPolarMode` to `flags` to specify the polar mapping mode.
Linear is the default mode.
The semilog mapping emulates the human "foveal" vision that permit very high acuity on the line of sight (central vision)
in contrast to peripheral vision where acuity is minor.

 Option on `dsize`:
- if both values in `dsize <=0 ` (default),
the destination image will have (almost) same area of source bounding circle:

$$\begin{array}{l}
dsize.area  \leftarrow (maxRadius^2 \cdot \Pi) \\
dsize.width = \texttt{cvRound}(maxRadius) \\
dsize.height = \texttt{cvRound}(maxRadius \cdot \Pi) \\
\end{array}$$

- if only `dsize.height <= 0`,
the destination image area will be proportional to the bounding circle area but scaled by `Kx * Kx`:

$$\begin{array}{l}
dsize.height = \texttt{cvRound}(dsize.width \cdot \Pi) \\
\end{array}
$$

- if both values in `dsize > 0 `,
the destination image will have the given size therefore the area of the bounding circle will be scaled to `dsize`.

 Reverse mapping
You can get reverse mapping adding `WARP_INVERSE_MAP` to `flags`
In addition, to calculate the original coordinate from a polar mapped coordinate formula:
.: info Note

-  The function can not operate in-place.
-  To calculate magnitude and angle in degrees `cartToPolar` is used internally thus angles are measured from 0 to 360 with accuracy about 0.3 degrees.
-  This function uses `remap`. Due to current implementation limitations the size of an input and output images should be less than 32767x32767.
**See also**: remap
.:

**Parameters**:
* `src`: Source image.
* `dst`: Destination image. It will have same type as src.
* `dsize`: The destination image size (see description for valid options).
* `center`: The transformation center.
* `maxRadius`: The radius of the bounding circle to transform. It determines the inverse magnitude scale parameter too.
* `flags`: A combination of interpolation methods, `InterpolationFlags` + `WarpPolarMode`. - Add `WARP_POLAR_LINEAR` to select linear polar mapping (default) - Add `WARP_POLAR_LOG` to select semilog polar mapping - Add `WARP_INVERSE_MAP` for reverse mapping.

---
### `Cv2.Integral`
**Signature**: `void Integral(Mat src, Mat sum, Mat sqsum, Mat tilted, int sdepth, int sqdepth)`

Calculates the integral of an image.

**Detailed Remarks**:
The function calculates one or more integral images for the source image as follows:
[see mathematical formula in OpenCV docs]
[see mathematical formula in OpenCV docs]
[see mathematical formula in OpenCV docs]
Using these integral images, you can calculate sum, mean, and standard deviation over a specific
up-right or rotated rectangular region of the image in a constant time, for example:
[see mathematical formula in OpenCV docs]
It makes possible to do a fast blurring or fast block correlation with a variable window size, for
example. In case of multi-channel images, sums for each channel are accumulated independently.
As a practical example, the next figure shows the calculation of the integral of a straight
rectangle Rect(4,4,3,2) and of a tilted rectangle Rect(5,1,2,3) . The selected pixels in the
original image are shown, as well as the relative pixels in the integral images sum and tilted .

**Parameters**:
* `src`: input image as formula, 8-bit or floating-point (32f or 64f).
* `sum`: integral image as formula , 32-bit integer or floating-point (32f or 64f).
* `sqsum`: integral image for squared pixel values; it is formula, double-precision floating-point (64f) array.
* `tilted`: integral for the image rotated by 45 degrees; it is formula array with the same data type as sum.
* `sdepth`: desired depth of the integral and the tilted integral images, CV_32S, CV_32F, or CV_64F.
* `sqdepth`: desired depth of the integral image of squared pixel values, CV_32F or CV_64F.

---
### `Cv2.Integral`
**Signature**: `void Integral(Mat src, Mat sum, int sdepth)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `src`: Source matrix or image.
* `sum`: The sum parameter.
* `sdepth`: The sdepth parameter.

---
### `Cv2.Integral`
**Signature**: `void Integral(Mat src, Mat sum, Mat sqsum, int sdepth, int sqdepth)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `src`: Source matrix or image.
* `sum`: The sum parameter.
* `sqsum`: The sqsum parameter.
* `sdepth`: The sdepth parameter.
* `sqdepth`: The sqdepth parameter.

---
### `Cv2.Accumulate`
**Signature**: `void Accumulate(Mat src, Mat dst, Mat? mask)`

Adds an image to the accumulator image.

**Detailed Remarks**:
The function adds src or some of its elements to dst :
[see mathematical formula in OpenCV docs]
The function supports multi-channel images. Each channel is processed independently.
The function accumulate can be used, for example, to collect statistics of a scene background
viewed by a still camera and for the further foreground-background segmentation.
**See also**: accumulateSquare, accumulateProduct, accumulateWeighted

**Parameters**:
* `src`: Input image of type CV_8UC(n), CV_16UC(n), CV_32FC(n) or CV_64FC(n), where n is a positive integer.
* `dst`: Accumulator image with the same number of channels as input image, and a depth of CV_32F or CV_64F.
* `mask`: Optional operation mask.

---
### `Cv2.AccumulateSquare`
**Signature**: `void AccumulateSquare(Mat src, Mat dst, Mat? mask)`

Adds the square of a source image to the accumulator image.

**Detailed Remarks**:
The function adds the input image src or its selected region, raised to a power of 2, to the
accumulator dst :
[see mathematical formula in OpenCV docs]
The function supports multi-channel images. Each channel is processed independently.
**See also**: accumulateSquare, accumulateProduct, accumulateWeighted

**Parameters**:
* `src`: Input image as 1- or 3-channel, 8-bit or 32-bit floating point.
* `dst`: Accumulator image with the same number of channels as input image, 32-bit or 64-bit floating-point.
* `mask`: Optional operation mask.

---
### `Cv2.AccumulateProduct`
**Signature**: `void AccumulateProduct(Mat src1, Mat src2, Mat dst, Mat? mask)`

Adds the per-element product of two input images to the accumulator image.

**Detailed Remarks**:
The function adds the product of two images or their selected regions to the accumulator dst :
[see mathematical formula in OpenCV docs]
The function supports multi-channel images. Each channel is processed independently.
**See also**: accumulate, accumulateSquare, accumulateWeighted

**Parameters**:
* `src1`: First input image, 1- or 3-channel, 8-bit or 32-bit floating point.
* `src2`: Second input image of the same type and the same size as src1 .
* `dst`: Accumulator image with the same number of channels as input images, 32-bit or 64-bit floating-point.
* `mask`: Optional operation mask.

---
### `Cv2.AccumulateWeighted`
**Signature**: `void AccumulateWeighted(Mat src, Mat dst, double alpha, Mat? mask)`

Updates a running average.

**Detailed Remarks**:
The function calculates the weighted sum of the input image src and the accumulator dst so that dst
becomes a running average of a frame sequence:
[see mathematical formula in OpenCV docs]
That is, alpha regulates the update speed (how fast the accumulator "forgets" about earlier images).
The function supports multi-channel images. Each channel is processed independently.
**See also**: accumulate, accumulateSquare, accumulateProduct

**Parameters**:
* `src`: Input image as 1- or 3-channel, 8-bit or 32-bit floating point.
* `dst`: Accumulator image with the same number of channels as input image, 32-bit or 64-bit floating-point.
* `alpha`: Weight of the input image.
* `mask`: Optional operation mask.

---
### `Cv2.PhaseCorrelate`
**Signature**: `IntPtr PhaseCorrelate(Mat src1, Mat src2, Mat? window, IntPtr response)`

The function is used to detect translational shifts that occur between two images.

**Detailed Remarks**:
The operation takes advantage of the Fourier shift theorem for detecting the translational shift in
the frequency domain. It can be used for fast image registration as well as motion estimation. For
more information please see <https://en.wikipedia.org/wiki/Phase_correlation>
Calculates the cross-power spectrum of two supplied source arrays. The arrays are padded if needed
with getOptimalDFTSize.
The function performs the following equations:
- First it applies a Hanning window to each image to remove possible edge effects, if it's provided
by user. See `createHanningWindow` and <https://en.wikipedia.org/wiki/Hann_function>. This window may
be cached until the array size changes to speed up processing time.
- Next it computes the forward DFTs of each source array:
[see mathematical formula in OpenCV docs]
where formula is the forward DFT.
- It then computes the cross-power spectrum of each frequency domain array:
[see mathematical formula in OpenCV docs]
- Next the cross-correlation is converted back into the time domain via the inverse DFT:
[see mathematical formula in OpenCV docs]
- Finally, it computes the peak location and computes a 5x5 weighted centroid around the peak to
achieve sub-pixel accuracy.
[see mathematical formula in OpenCV docs]
- If non-zero, the response parameter is computed as the sum of the elements of r within the 5x5
centroid around the peak location. It is normalized to a maximum of 1 (meaning there is a single
peak) and will be smaller when there are multiple peaks.
**See also**: dft, getOptimalDFTSize, idft, mulSpectrums createHanningWindow

**Parameters**:
* `src1`: Source floating point array (CV_32FC1 or CV_64FC1)
* `src2`: Source floating point array (CV_32FC1 or CV_64FC1)
* `window`: Floating point array with windowing coefficients to reduce edge effects (optional).
* `response`: Signal power within the 5x5 centroid around the peak, between 0 and 1 (optional).

**Returns**: s detected phase shift (sub-pixel) between the two arrays.

---
### `Cv2.PhaseCorrelateIterative`
**Signature**: `IntPtr PhaseCorrelateIterative(Mat src1, Mat src2, int L2size, int maxIters)`

Detects translational shifts between two images.

**Detailed Remarks**:
This function extends the standard `phaseCorrelate` method by improving sub-pixel accuracy
through iterative shift refinement in the phase-correlation space, as described in
**Citation**:  hrazdira2020iterative.
**See also**: phaseCorrelate, dft, idft, createHanningWindow

**Parameters**:
* `src1`: Source floating point array (CV_32FC1 or CV_64FC1)
* `src2`: Source floating point array (CV_32FC1 or CV_64FC1)
* `L2size`: The size of the correlation neighborhood used by the iterative shift refinement algorithm.
* `maxIters`: The maximum number of iterations the iterative refinement algorithm will run.

**Returns**: s detected sub-pixel shift between the two arrays.

---
### `Cv2.CreateHanningWindow`
**Signature**: `void CreateHanningWindow(Mat dst, Size winSize, int type)`

This function computes a Hanning window coefficients in two dimensions.

**Detailed Remarks**:
See (https://en.wikipedia.org/wiki/Hann_function) and (https://en.wikipedia.org/wiki/Window_function)
for more information.
An example is shown below:

```csharp
// create hanning window of size 100x100 and type CV_32F
using var hann = new Mat();
Cv2.CreateHanningWindow(hann, new Size(100, 100), 5); // CV_32FC1 = 5
```

**Parameters**:
* `dst`: Destination array to place Hann coefficients in
* `winSize`: The window size specifications (both width and height must be > 1)
* `type`: Created array type

---
### `Cv2.Threshold`
**Signature**: `double Threshold(Mat src, Mat dst, double thresh, double maxval, int type)`

Applies a fixed-level threshold to each array element.

**Detailed Remarks**:
The function applies fixed-level thresholding to a multiple-channel array. The function is typically
used to get a bi-level (binary) image out of a grayscale image ( `compare` could be also used for
this purpose) or for removing a noise, that is, filtering out pixels with too small or too large
values. There are several types of thresholding supported by the function. They are determined by
type parameter.
Also, the special values `THRESH_OTSU` or `THRESH_TRIANGLE` may be combined with one of the
above values. In these cases, the function determines the optimal threshold value using the Otsu's
or Triangle algorithm and uses it instead of the specified thresh.
.: info Note
Currently, the Otsu's method is implemented only for CV_8UC1 and CV_16UC1 images,
and the Triangle's method is implemented only for CV_8UC1 images.
**See also**: thresholdWithMask, adaptiveThreshold, findContours, compare, min, max
.:

**Parameters**:
* `src`: input array (multiple-channel, CV_8U, CV_16S, CV_16U, CV_32F or CV_64F).
* `dst`: output array of the same size  and type and the same number of channels as src.
* `thresh`: threshold value.
* `maxval`: maximum value to use with the `THRESH_BINARY` and `THRESH_BINARY_INV` thresholding types.
* `type`: thresholding type (see `ThresholdTypes`).

**Returns**: the computed threshold value if Otsu's or Triangle methods used.

---
### `Cv2.ThresholdWithMask`
**Signature**: `double ThresholdWithMask(Mat src, Mat dst, Mat mask, double thresh, double maxval, int type)`

Same as `threshold`, but with an optional mask

**Detailed Remarks**:
.: info Note
If the mask is empty, `thresholdWithMask` is equivalent to `threshold`.
If the mask is not empty, dst *must* be of the same size and type as src, so that
outliers pixels are left as-is
**See also**: threshold, adaptiveThreshold, findContours, compare, min, max
.:

**Parameters**:
* `src`: input array (multiple-channel, 8-bit or 32-bit floating point).
* `dst`: output array of the same size  and type and the same number of channels as src.
* `mask`: optional mask (same size as src, 8-bit).
* `thresh`: threshold value.
* `maxval`: maximum value to use with the `THRESH_BINARY` and `THRESH_BINARY_INV` thresholding types.
* `type`: thresholding type (see `ThresholdTypes`).

**Returns**: the computed threshold value if Otsu's or Triangle methods used.

---
### `Cv2.AdaptiveThreshold`
**Signature**: `void AdaptiveThreshold(Mat src, Mat dst, double maxValue, int adaptiveMethod, int thresholdType, int blockSize, double C)`

Applies an adaptive threshold to an array.

**Detailed Remarks**:
The function transforms a grayscale image to a binary image according to the formulae:
-   **THRESH_BINARY**
[see mathematical formula in OpenCV docs]
-   **THRESH_BINARY_INV**
[see mathematical formula in OpenCV docs]
where formula is a threshold calculated individually for each pixel (see adaptiveMethod parameter).
The function can process the image in-place.
**See also**: threshold, blur, GaussianBlur

**Parameters**:
* `src`: Source 8-bit single-channel image.
* `dst`: Destination image of the same size and the same type as src.
* `maxValue`: Non-zero value assigned to the pixels for which the condition is satisfied
* `adaptiveMethod`: Adaptive thresholding algorithm to use, see `AdaptiveThresholdTypes`. The `BORDER_REPLICATE` | `BORDER_ISOLATED` is used to process boundaries.
* `thresholdType`: Thresholding type that must be either `THRESH_BINARY` or `THRESH_BINARY_INV`, see `ThresholdTypes`.
* `blockSize`: Size of a pixel neighborhood that is used to calculate a threshold value for the pixel: 3, 5, 7, and so on.
* `C`: Constant subtracted from the mean or weighted mean (see the details below). Normally, it is positive but may be zero or negative as well.

---
### `Cv2.PyrDown`
**Signature**: `void PyrDown(Mat src, Mat dst, Size dstsize, int borderType)`

Blurs an image and downsamples it.

**Detailed Remarks**:
By default, size of the output image is computed as `Size((src.cols+1)/2, (src.rows+1)/2)`, but in
any case, the following conditions should be satisfied:
[see mathematical formula in OpenCV docs]
The function performs the downsampling step of the Gaussian pyramid construction. First, it
convolves the source image with the kernel:
[see mathematical formula in OpenCV docs]
Then, it downsamples the image by rejecting even rows and columns.

**Parameters**:
* `src`: input image.
* `dst`: output image; it has the specified size and the same type as src.
* `dstsize`: size of the output image.
* `borderType`: Pixel extrapolation method, see `BorderTypes` (#BORDER_CONSTANT isn't supported)

---
### `Cv2.PyrUp`
**Signature**: `void PyrUp(Mat src, Mat dst, Size dstsize, int borderType)`

Upsamples an image and then blurs it.

**Detailed Remarks**:
By default, size of the output image is computed as `Size(src.cols\*2, (src.rows\*2)`, but in any
case, the following conditions should be satisfied:
[see mathematical formula in OpenCV docs]
The function performs the upsampling step of the Gaussian pyramid construction, though it can
actually be used to construct the Laplacian pyramid. First, it upsamples the source image by
injecting even zero rows and columns and then convolves the result with the same kernel as in
pyrDown multiplied by 4.

**Parameters**:
* `src`: input image.
* `dst`: output image. It has the specified size and the same type as src .
* `dstsize`: size of the output image.
* `borderType`: Pixel extrapolation method, see `BorderTypes` (only `BORDER_DEFAULT` is supported)

---
### `Cv2.CalcHist`
**Signature**: `void CalcHist(IntPtr images, IntPtr channels, Mat mask, Mat hist, IntPtr histSize, IntPtr ranges, bool accumulate)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
this variant supports only uniform histograms.
ranges argument is either empty vector or a flattened vector of histSize.size()*2 elements
(histSize.size() element pairs). The first and second elements of each pair specify the lower and
upper boundaries.

**Parameters**:
* `images`: The images parameter.
* `channels`: The channels parameter.
* `mask`: Optional operation mask.
* `hist`: The hist parameter.
* `histSize`: The histSize parameter.
* `ranges`: The ranges parameter.
* `accumulate`: The accumulate parameter.

---
### `Cv2.CalcBackProject`
**Signature**: `void CalcBackProject(IntPtr images, IntPtr channels, Mat hist, Mat dst, IntPtr ranges, double scale)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `images`: The images parameter.
* `channels`: The channels parameter.
* `hist`: The hist parameter.
* `dst`: Destination matrix or image (output).
* `ranges`: The ranges parameter.
* `scale`: The scale parameter.

---
### `Cv2.CompareHist`
**Signature**: `double CompareHist(Mat H1, Mat H2, int method)`

Compares two histograms.

**Detailed Remarks**:
The function compareHist compares two dense or two sparse histograms using the specified method.
The function returns formula .
While the function works well with 1-, 2-, 3-dimensional dense histograms, it may not be suitable
for high-dimensional sparse histograms. In such histograms, because of aliasing and sampling
problems, the coordinates of non-zero histogram bins can slightly shift. To compare such histograms
or more general sparse configurations of weighted points, consider using the `EMD` function.

**Parameters**:
* `H1`: First compared histogram.
* `H2`: Second compared histogram of the same size as H1 .
* `method`: Comparison method, see `HistCompMethods`

**Returns**: The returned value.

---
### `Cv2.EqualizeHist`
**Signature**: `void EqualizeHist(Mat src, Mat dst)`

Equalizes the histogram of a grayscale image.

**Detailed Remarks**:
The function equalizes the histogram of the input image using the following algorithm:
- Calculate the histogram formula for src .
- Normalize the histogram so that the sum of histogram bins is 255.
- Compute the integral of the histogram:
[see mathematical formula in OpenCV docs]
- Transform the image using formula as a look-up table: formula
The algorithm normalizes the brightness and increases the contrast of the image.

**Parameters**:
* `src`: Source 8-bit single channel image.
* `dst`: Destination image of the same size and type as src .

---
### `Cv2.CreateCLAHE`
**Signature**: `Clahe? CreateCLAHE(double clipLimit, Size tileGridSize)`

Creates a smart pointer to a CLAHE class and initializes it.

**Parameters**:
* `clipLimit`: Threshold for contrast limiting.
* `tileGridSize`: Size of grid for histogram equalization. Input image will be divided into equally sized rectangular tiles. tileGridSize defines the number of tiles in row and column.

**Returns**: The returned value.

---
### `Cv2.WrapperEMD`
**Signature**: `float WrapperEMD(Mat signature1, Mat signature2, int distType, Mat? cost, IntPtr lowerBound, Mat? flow)`

Computes the "minimal work" distance between two weighted point configurations.

**Detailed Remarks**:
The function computes the earth mover distance and/or a lower boundary of the distance between the
two weighted point configurations. One of the applications described in **Citation**:  RubnerSept98,
**Citation**:  Rubner2000 is multi-dimensional histogram comparison for image retrieval. EMD is a transportation
problem that is solved using some modification of a simplex algorithm, thus the complexity is
exponential in the worst case, though, on average it is much faster. In the case of a real metric
the lower boundary can be calculated even faster (using linear-time algorithm) and it can be used
to determine roughly whether the two signatures are far enough so that they cannot relate to the
same object.

**Parameters**:
* `signature1`: First signature, a formula floating-point matrix. Each row stores the point weight followed by the point coordinates. The matrix is allowed to have a single column (weights only) if the user-defined cost matrix is used. The weights must be non-negative and have at least one non-zero value.
* `signature2`: Second signature of the same format as signature1 , though the number of rows may be different. The total weights may be different. In this case an extra "dummy" point is added to either signature1 or signature2. The weights must be non-negative and have at least one non-zero value.
* `distType`: Used metric. See `DistanceTypes`.
* `cost`: User-defined formula cost matrix. Also, if a cost matrix is used, lower boundary lowerBound cannot be calculated because it needs a metric function.
* `lowerBound`: Optional input/output parameter: lower boundary of a distance between the two signatures that is a distance between mass centers. The lower boundary may not be calculated if the user-defined cost matrix is used, the total weights of point configurations are not equal, or if the signatures consist of weights only (the signature matrices have a single column). You **must** initialize \*lowerBound . If the calculated distance between mass centers is greater or equal to \*lowerBound (it means that the signatures are far enough), the function does not calculate EMD. In any case \*lowerBound is set to the calculated distance between mass centers on return. Thus, if you want to calculate both distance between mass centers and EMD, \*lowerBound should be set to 0.
* `flow`: Resultant formula flow matrix: formula is a flow from formula -th point of signature1 to formula -th point of signature2 .

**Returns**: The returned value.

---
### `Cv2.Watershed`
**Signature**: `void Watershed(Mat image, Mat markers)`

Performs a marker-based image segmentation using the watershed algorithm.

**Detailed Remarks**:
The function implements one of the variants of watershed, non-parametric marker-based segmentation
algorithm, described in **Citation**:  Meyer92 .
Before passing the image to the function, you have to roughly outline the desired regions in the
image markers with positive (\>0) indices. So, every region is represented as one or more connected
components with the pixel values 1, 2, 3, and so on. Such markers can be retrieved from a binary
mask using `findContours` and `drawContours` (see the watershed.cpp demo). The markers are "seeds" of
the future image regions. All the other pixels in markers , whose relation to the outlined regions
is not known and should be defined by the algorithm, should be set to 0's. In the function output,
each pixel in markers is set to a value of the "seed" components or to -1 at boundaries between the
regions.
.: info Note
Any two neighbor connected components are not necessarily separated by a watershed boundary
(-1's pixels); for example, they can touch each other in the initial marker image passed to the
function.
**See also**: findContours
.:

**Parameters**:
* `image`: Input 8-bit 3-channel image.
* `markers`: Input/output 32-bit single-channel image (map) of markers. It should have the same size as image .

---
### `Cv2.PyrMeanShiftFiltering`
**Signature**: `void PyrMeanShiftFiltering(Mat src, Mat dst, double sp, double sr, int maxLevel, TermCriteria termcrit)`

Performs initial step of meanshift segmentation of an image.

**Detailed Remarks**:
The function implements the filtering stage of meanshift segmentation, that is, the output of the
function is the filtered "posterized" image with color gradients and fine-grain texture flattened.
At every pixel (X,Y) of the input image (or down-sized input image, see below) the function executes
meanshift iterations, that is, the pixel (X,Y) neighborhood in the joint space-color hyperspace is
considered:
[see mathematical formula in OpenCV docs]
where (R,G,B) and (r,g,b) are the vectors of color components at (X,Y) and (x,y), respectively
(though, the algorithm does not depend on the color space used, so any 3-component color space can
be used instead). Over the neighborhood the average spatial value (X',Y') and average color vector
(R',G',B') are found and they act as the neighborhood center on the next iteration:
[see mathematical formula in OpenCV docs]
After the iterations over, the color components of the initial pixel (that is, the pixel from where
the iterations started) are set to the final value (average color at the last iteration):
[see mathematical formula in OpenCV docs]
When maxLevel \> 0, the gaussian pyramid of maxLevel+1 levels is built, and the above procedure is
run on the smallest layer first. After that, the results are propagated to the larger layer and the
iterations are run again only on those pixels where the layer colors differ by more than sr from the
lower-resolution layer of the pyramid. That makes boundaries of color regions sharper. Note that the
results will be actually different from the ones obtained by running the meanshift procedure on the
whole original image (i.e. when maxLevel==0).

**Parameters**:
* `src`: The source 8-bit, 3-channel image.
* `dst`: The destination image of the same format and the same size as the source.
* `sp`: The spatial window radius.
* `sr`: The color window radius.
* `maxLevel`: Maximum level of the pyramid for the segmentation.
* `termcrit`: Termination criteria: when to stop meanshift iterations.

---
### `Cv2.GrabCut`
**Signature**: `void GrabCut(Mat img, Mat mask, Rect rect, Mat bgdModel, Mat fgdModel, int iterCount, int mode)`

Runs the GrabCut algorithm.

**Detailed Remarks**:
The function implements the [GrabCut image segmentation algorithm](https://en.wikipedia.org/wiki/GrabCut).

**Parameters**:
* `img`: Input 8-bit 3-channel image.
* `mask`: Input/output 8-bit single-channel mask. The mask is initialized by the function when mode is set to `GC_INIT_WITH_RECT`. Its elements may have one of the `GrabCutClasses`.
* `rect`: ROI containing a segmented object. The pixels outside of the ROI are marked as "obvious background". The parameter is only used when mode==#GC_INIT_WITH_RECT .
* `bgdModel`: Temporary array for the background model. Do not modify it while you are processing the same image.
* `fgdModel`: Temporary arrays for the foreground model. Do not modify it while you are processing the same image.
* `iterCount`: Number of iterations the algorithm should make before returning the result. Note that the result can be refined with further calls with mode==#GC_INIT_WITH_MASK or mode==GC_EVAL .
* `mode`: Operation mode that could be one of the `GrabCutModes`

---
### `Cv2.DistanceTransform`
**Signature**: `void DistanceTransform(Mat src, Mat dst, Mat labels, int distanceType, int maskSize, int labelType)`

Calculates the distance to the closest zero pixel for each pixel of the source image.

**Detailed Remarks**:
The function distanceTransform calculates the approximate or precise distance from every binary
image pixel to the nearest zero pixel. For zero image pixels, the distance will obviously be zero.
When maskSize == `DIST_MASK_PRECISE` and distanceType == `DIST_L2` , the function runs the
algorithm described in **Citation**:  Felzenszwalb04 . This algorithm is parallelized with the TBB library.
In other cases, the algorithm **Citation**:  Borgefors86 is used. This means that for a pixel the function
finds the shortest path to the nearest zero pixel consisting of basic shifts: horizontal, vertical,
diagonal, or knight's move (the latest is available for a formula mask). The overall
distance is calculated as a sum of these basic distances. Since the distance function should be
symmetric, all of the horizontal and vertical shifts must have the same cost (denoted as a ), all
the diagonal shifts must have the same cost (denoted as `b`), and all knight's moves must have the
same cost (denoted as `c`). For the `DIST_C` and `DIST_L1` types, the distance is calculated
precisely, whereas for `DIST_L2` (Euclidean distance) the distance can be calculated only with a
relative error (a formula mask gives more accurate results). For `a`,`b`, and `c`, OpenCV
uses the values suggested in the original paper:
- DIST_L1: `a = 1, b = 2`
- DIST_L2:
- `3 x 3`: `a=0.955, b=1.3693`
- `5 x 5`: `a=1, b=1.4, c=2.1969`
- DIST_C: `a = 1, b = 1`
Typically, for a fast, coarse distance estimation `DIST_L2`, a formula mask is used. For a
more accurate distance estimation `DIST_L2`, a formula mask or the precise algorithm is used.
Note that both the precise and the approximate algorithms are linear on the number of pixels.
This variant of the function does not only compute the minimum distance for each pixel formula
but also identifies the nearest connected component consisting of zero pixels
(labelType==`DIST_LABEL_CCOMP`) or the nearest zero pixel (labelType==`DIST_LABEL_PIXEL`). Index of the
component/pixel is stored in `labels(x, y)`. When labelType==`DIST_LABEL_CCOMP`, the function
automatically finds connected components of zero pixels in the input image and marks them with
distinct labels. When labelType==`DIST_LABEL_PIXEL`, the function scans through the input image and
marks all the zero pixels with distinct labels.
In this mode, the complexity is still linear. That is, the function provides a very fast way to
compute the Voronoi diagram for a binary image. Currently, the second variant can use only the
approximate distance transform algorithm, i.e. maskSize=`DIST_MASK_PRECISE` is not supported
yet.

**Parameters**:
* `src`: 8-bit, single-channel (binary) source image.
* `dst`: Output image with calculated distances. It is a 8-bit or 32-bit floating-point, single-channel image of the same size as src.
* `labels`: Output 2D array of labels (the discrete Voronoi diagram). It has the type CV_32SC1 and the same size as src.
* `distanceType`: Type of distance, see `DistanceTypes`
* `maskSize`: Size of the distance transform mask, see `DistanceTransformMasks`. `DIST_MASK_PRECISE` is not supported by this variant. In case of the `DIST_L1` or `DIST_C` distance type, the parameter is forced to 3 because a formula mask gives the same result as `5\times 5` or any larger aperture.
* `labelType`: Type of the label array to build, see `DistanceTransformLabelTypes`.

---
### `Cv2.DistanceTransform`
**Signature**: `void DistanceTransform(Mat src, Mat dst, int distanceType, int maskSize, int dstType)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `src`: 8-bit, single-channel (binary) source image.
* `dst`: Output image with calculated distances. It is a 8-bit or 32-bit floating-point, single-channel image of the same size as src .
* `distanceType`: Type of distance, see `DistanceTypes`
* `maskSize`: Size of the distance transform mask, see `DistanceTransformMasks`. In case of the `DIST_L1` or `DIST_C` distance type, the parameter is forced to 3 because a formula mask gives the same result as formula or any larger aperture.
* `dstType`: Type of output image. It can be CV_8U or CV_32F. Type CV_8U can be used only for the first variant of the function and distanceType == `DIST_L1`.

---
### `Cv2.FloodFill`
**Signature**: `int FloodFill(Mat image, Mat mask, Point seedPoint, Scalar newVal, IntPtr rect, Scalar loDiff, Scalar upDiff, int flags)`

Fills a connected component with the given color.

**Detailed Remarks**:
The function floodFill fills a connected component starting from the seed point with the specified
color. The connectivity is determined by the color/brightness closeness of the neighbor pixels. The
pixel at formula is considered to belong to the repainted domain if:
- in case of a grayscale image and floating range
[see mathematical formula in OpenCV docs]
- in case of a grayscale image and fixed range
[see mathematical formula in OpenCV docs]
- in case of a color image and floating range
[see mathematical formula in OpenCV docs]
[see mathematical formula in OpenCV docs]
and
[see mathematical formula in OpenCV docs]
- in case of a color image and fixed range
[see mathematical formula in OpenCV docs]
[see mathematical formula in OpenCV docs]
and
[see mathematical formula in OpenCV docs]
where formula is the value of one of pixel neighbors that is already known to belong to the
component. That is, to be added to the connected component, a color/brightness of the pixel should
be close enough to:
- Color/brightness of one of its neighbors that already belong to the connected component in case
of a floating range.
- Color/brightness of the seed point in case of a fixed range.
Use these functions to either mark a connected component with the specified color in-place, or build
a mask and then extract the contour, or copy the region to another image, and so on.
.: info Note
Since the mask is larger than the filled image, a pixel formula in image corresponds to the
pixel formula in the mask .
**See also**: findContours
.:

**Parameters**:
* `image`: Input/output 1- or 3-channel, 8-bit, or floating-point image. It is modified by the function unless the `FLOODFILL_MASK_ONLY` flag is set in the second variant of the function. See the details below.
* `mask`: Operation mask that should be a single-channel 8-bit image, 2 pixels wider and 2 pixels taller than image. If an empty Mat is passed it will be created automatically. Since this is both an input and output parameter, you must take responsibility of initializing it. Flood-filling cannot go across non-zero pixels in the input mask. For example, an edge detector output can be used as a mask to stop filling at edges. On output, pixels in the mask corresponding to filled pixels in the image are set to 1 or to the specified value in flags as described below. Additionally, the function fills the border of the mask with ones to simplify internal processing. It is therefore possible to use the same mask in multiple calls to the function to make sure the filled areas do not overlap.
* `seedPoint`: Starting point.
* `newVal`: New value of the repainted domain pixels.
* `rect`: Optional output parameter set by the function to the minimum bounding rectangle of the repainted domain.
* `loDiff`: Maximal lower brightness/color difference between the currently observed pixel and one of its neighbors belonging to the component, or a seed pixel being added to the component.
* `upDiff`: Maximal upper brightness/color difference between the currently observed pixel and one of its neighbors belonging to the component, or a seed pixel being added to the component.
* `flags`: Operation flags. The first 8 bits contain a connectivity value. The default value of 4 means that only the four nearest neighbor pixels (those that share an edge) are considered. A connectivity value of 8 means that the eight nearest neighbor pixels (those that share a corner) will be considered. The next 8 bits (8-16) contain a value between 1 and 255 with which to fill the mask (the default value is 1). For example, 4 | ( 255 \<\< 8 ) will consider 4 nearest neighbours and fill the mask with a value of 255. The following additional options occupy higher bits and therefore may be further combined with the connectivity and mask fill values using bit-wise or (|), see `FloodFillFlags`.

**Returns**: The returned value.

---
### `Cv2.BlendLinear`
**Signature**: `void BlendLinear(Mat src1, Mat src2, Mat weights1, Mat weights2, Mat dst)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
variant without `mask` parameter

**Parameters**:
* `src1`: The src1 parameter.
* `src2`: The src2 parameter.
* `weights1`: The weights1 parameter.
* `weights2`: The weights2 parameter.
* `dst`: Destination matrix or image (output).

---
### `Cv2.CvtColor`
**Signature**: `void CvtColor(Mat src, Mat dst, int code, int dstCn, AlgorithmHint hint)`

Converts an image from one color space to another.

**Detailed Remarks**:
The function converts an input image from one color space to another. In case of a transformation
to-from RGB color space, the order of the channels should be specified explicitly (RGB or BGR). Note
that the default color format in OpenCV is often referred to as RGB but it is actually BGR (the
bytes are reversed). So the first byte in a standard (24-bit) color image will be an 8-bit Blue
component, the second byte will be Green, and the third byte will be Red. The fourth, fifth, and
sixth bytes would then be the second pixel (Blue, then Green, then Red), and so on.
The conventional ranges for R, G, and B channel values are:
-   0 to 255 for CV_8U images
-   0 to 65535 for CV_16U images
-   0 to 1 for CV_32F images
In case of linear transformations, the range does not matter. But in case of a non-linear
transformation, an input RGB image should be normalized to the proper value range to get the correct
results, for example, for RGB formula L\*u\*v\* transformation. For example, if you have a
32-bit floating-point image directly converted from an 8-bit image without any scaling, then it will
have the 0..255 value range instead of 0..1 assumed by the function. So, before calling `cvtColor` ,
you need first to scale the image down:

```csharp
img.ConvertTo(img, -1, 1.0 / 255.0);
Cv2.CvtColor(img, img, (int)ColorConversionCodes.Bgr2luv, 0, AlgorithmHint.Default);
```

If you use `cvtColor` with 8-bit images, the conversion will have some information lost. For many
applications, this will not be noticeable but it is recommended to use 32-bit images in applications
that need the full range of colors or that convert an image before an operation and then convert
back.
If conversion adds the alpha channel, its value will set to the maximum of corresponding channel
range: 255 for CV_8U, 65535 for CV_16U, 1 for CV_32F.
.: info Note
The source image (src) must be of an appropriate type for the desired color conversion. see ColorConversionCodes
**See also**: `imgproc_color_conversions`
.:

**Parameters**:
* `src`: input image: 8-bit unsigned, 16-bit unsigned ( CV_16UC... ), or single-precision floating-point.
* `dst`: output image of the same size and depth as src.
* `code`: color space conversion code (see `ColorConversionCodes`).
* `dstCn`: number of channels in the destination image; if the parameter is 0, the number of the channels is derived automatically from src and code.
* `hint`: Implementation modification flags. See `AlgorithmHint`

---
### `Cv2.CvtColorTwoPlane`
**Signature**: `void CvtColorTwoPlane(Mat src1, Mat src2, Mat dst, int code, AlgorithmHint hint)`

Converts an image from one color space to another where the source image is stored in two planes.

**Detailed Remarks**:
This function only supports YUV420 to RGB conversion as of now.

**Parameters**:
* `src1`: 8-bit image (#CV_8U) of the Y plane.
* `src2`: image containing interleaved U/V plane.
* `dst`: output image.
* `code`: Specifies the type of conversion. It can take any of the following values: - `COLOR_YUV2BGR_NV12` - `COLOR_YUV2RGB_NV12` - `COLOR_YUV2BGRA_NV12` - `COLOR_YUV2RGBA_NV12` - `COLOR_YUV2BGR_NV21` - `COLOR_YUV2RGB_NV21` - `COLOR_YUV2BGRA_NV21` - `COLOR_YUV2RGBA_NV21`
* `hint`: Implementation modification flags. See `AlgorithmHint`

---
### `Cv2.Demosaicing`
**Signature**: `void Demosaicing(Mat src, Mat dst, int code, int dstCn)`

main function for all demosaicing processes

**Detailed Remarks**:
.: info Note
The source image (src) must be of an appropriate type for the desired color conversion. see ColorConversionCodes
**See also**: cvtColor
.:

**Parameters**:
* `src`: input image: 8-bit unsigned or 16-bit unsigned.
* `dst`: output image of the same size and depth as src.
* `code`: Color space conversion code (see the description below).
* `dstCn`: number of channels in the destination image; if the parameter is 0, the number of the channels is derived automatically from src and code. The function can do the following transformations: -   Demosaicing using bilinear interpolation `COLOR_BayerBG2BGR` , `COLOR_BayerGB2BGR` , `COLOR_BayerRG2BGR` , `COLOR_BayerGR2BGR` `COLOR_BayerBG2GRAY` , `COLOR_BayerGB2GRAY` , `COLOR_BayerRG2GRAY` , `COLOR_BayerGR2GRAY` -   Demosaicing using Variable Number of Gradients. `COLOR_BayerBG2BGR_VNG` , `COLOR_BayerGB2BGR_VNG` , `COLOR_BayerRG2BGR_VNG` , `COLOR_BayerGR2BGR_VNG` -   Edge-Aware Demosaicing. `COLOR_BayerBG2BGR_EA` , `COLOR_BayerGB2BGR_EA` , `COLOR_BayerRG2BGR_EA` , `COLOR_BayerGR2BGR_EA` -   Demosaicing with alpha channel `COLOR_BayerBG2BGRA` , `COLOR_BayerGB2BGRA` , `COLOR_BayerRG2BGRA` , `COLOR_BayerGR2BGRA`

---
### `Cv2.MatchTemplate`
**Signature**: `void MatchTemplate(Mat image, Mat templ, Mat result, int method, Mat? mask)`

Compares a template against overlapped image regions.

**Detailed Remarks**:
The function slides through image , compares the overlapped patches of size formula against
templ using the specified method and stores the comparison results in result . `TemplateMatchModes`
describes the formulae for the available comparison methods ( formula denotes image, formula
template, formula result, formula the optional mask ). The summation is done over template and/or
the image patch: formula
After the function finishes the comparison, the best matches can be found as global minimums (when
`TM_SQDIFF` was used) or maximums (when `TM_CCORR` or `TM_CCOEFF` was used) using the
`minMaxLoc` function. In case of a color image, template summation in the numerator and each sum in
the denominator is done over all of the channels and separate mean values are used for each channel.
That is, the function can take a color template and a color image. The result will still be a
single-channel image, which is easier to analyze.

**Parameters**:
* `image`: Image where the search is running. It must be 8-bit or 32-bit floating-point.
* `templ`: Searched template. It must be not greater than the source image and have the same data type.
* `result`: Map of comparison results. It must be single-channel 32-bit floating-point. If image is formula and templ is formula , then result is formula .
* `method`: Parameter specifying the comparison method, see `TemplateMatchModes`
* `mask`: Optional mask. It must have the same size as templ. It must either have the same number of channels as template or only one channel, which is then used for all template and image channels. If the data type is `CV_8U`, the mask is interpreted as a binary mask, meaning only elements where mask is nonzero are used and are kept unchanged independent of the actual mask value (weight equals 1). For data type `CV_32F`, the mask values are used as weights. The exact formulas are documented in `TemplateMatchModes`.

---
### `Cv2.ConnectedComponents`
**Signature**: `int ConnectedComponents(Mat image, Mat labels, int connectivity, int ltype, int ccltype)`

computes the connected components labeled image of boolean image

**Detailed Remarks**:
image with 4 or 8 way connectivity - returns N, the total number of labels [0, N-1] where 0
represents the background label. ltype specifies the output label image type, an important
consideration based on the total number of labels or alternatively the total number of pixels in
the source image. ccltype specifies the connected components labeling algorithm to use, currently
Bolelli (Spaghetti) **Citation**:  Bolelli2019, Grana (BBDT) **Citation**:  Grana2010 and Wu's (SAUF) **Citation**:  Wu2009 algorithms
are supported, see the `ConnectedComponentsAlgorithmsTypes` for details. Note that SAUF algorithm forces
a row major ordering of labels while Spaghetti and BBDT do not.
This function uses parallel version of the algorithms if at least one allowed
parallel framework is enabled and if the rows of the image are at least twice the number returned by `getNumberOfCPUs`.

**Parameters**:
* `image`: the 8-bit single-channel image to be labeled
* `labels`: destination labeled image
* `connectivity`: 8 or 4 for 8-way or 4-way connectivity respectively
* `ltype`: output image label type. Currently CV_32S and CV_16U are supported.
* `ccltype`: connected components algorithm type (see the `ConnectedComponentsAlgorithmsTypes`).

**Returns**: The returned value.

---
### `Cv2.ConnectedComponents`
**Signature**: `int ConnectedComponents(Mat image, Mat labels, int connectivity, int ltype)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: the 8-bit single-channel image to be labeled
* `labels`: destination labeled image
* `connectivity`: 8 or 4 for 8-way or 4-way connectivity respectively
* `ltype`: output image label type. Currently CV_32S and CV_16U are supported.

**Returns**: The returned value.

---
### `Cv2.ConnectedComponentsWithStats`
**Signature**: `int ConnectedComponentsWithStats(Mat image, Mat labels, Mat stats, Mat centroids, int connectivity, int ltype, int ccltype)`

computes the connected components labeled image of boolean image and also produces a statistics output for each label

**Detailed Remarks**:
image with 4 or 8 way connectivity - returns N, the total number of labels [0, N-1] where 0
represents the background label. ltype specifies the output label image type, an important
consideration based on the total number of labels or alternatively the total number of pixels in
the source image. ccltype specifies the connected components labeling algorithm to use, currently
Bolelli (Spaghetti) **Citation**:  Bolelli2019, Grana (BBDT) **Citation**:  Grana2010 and Wu's (SAUF) **Citation**:  Wu2009 algorithms
are supported, see the `ConnectedComponentsAlgorithmsTypes` for details. Note that SAUF algorithm forces
a row major ordering of labels while Spaghetti and BBDT do not.
This function uses parallel version of the algorithms (statistics included) if at least one allowed
parallel framework is enabled and if the rows of the image are at least twice the number returned by `getNumberOfCPUs`.

**Parameters**:
* `image`: the 8-bit single-channel image to be labeled
* `labels`: destination labeled image
* `stats`: statistics output for each label, including the background label. Statistics are accessed via stats(label, COLUMN) where COLUMN is one of `ConnectedComponentsTypes`, selecting the statistic. The data type is CV_32S.
* `centroids`: centroid output for each label, including the background label. Centroids are accessed via centroids(label, 0) for x and centroids(label, 1) for y. The data type CV_64F.
* `connectivity`: 8 or 4 for 8-way or 4-way connectivity respectively
* `ltype`: output image label type. Currently CV_32S and CV_16U are supported.
* `ccltype`: connected components algorithm type (see `ConnectedComponentsAlgorithmsTypes`).

**Returns**: The returned value.

---
### `Cv2.ConnectedComponentsWithStats`
**Signature**: `int ConnectedComponentsWithStats(Mat image, Mat labels, Mat stats, Mat centroids, int connectivity, int ltype)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: the 8-bit single-channel image to be labeled
* `labels`: destination labeled image
* `stats`: statistics output for each label, including the background label. Statistics are accessed via stats(label, COLUMN) where COLUMN is one of `ConnectedComponentsTypes`, selecting the statistic. The data type is CV_32S.
* `centroids`: centroid output for each label, including the background label. Centroids are accessed via centroids(label, 0) for x and centroids(label, 1) for y. The data type CV_64F.
* `connectivity`: 8 or 4 for 8-way or 4-way connectivity respectively
* `ltype`: output image label type. Currently CV_32S and CV_16U are supported.

**Returns**: The returned value.

---
### `Cv2.FindContours`
**Signature**: `void FindContours(Mat image, IntPtr contours, Mat hierarchy, int mode, int method, Point offset)`

Finds contours in a binary image.

**Detailed Remarks**:
The function retrieves contours from the binary image. The contours
are a useful tool for shape analysis and object detection and recognition. See squares.cpp in the
OpenCV sample directory.
.: info Note
Since OpenCV 4.14, when mode is `RETR_LIST` and no hierarchy is requested, this function
automatically uses the TRUCO parallel algorithm **Citation**:  TRUCO2026, a scalable lock-free method for
contour extraction. In all other cases, the sequential **Citation**:  Suzuki85 algorithm is used.
.:
.: info Note
Since opencv 3.2 source image is not modified by this function.
.:
.: info Note
The hierarchy array maps contour relationships to access hierarchical elements of i-th contour.
.:

**Parameters**:
* `image`: Source, an 8-bit single-channel image. Non-zero pixels are treated as 1's. Zero pixels remain 0's, so the image is treated as binary . You can use `compare`, `inRange`, `threshold` , `adaptiveThreshold`, `Canny`, and others to create a binary image out of a grayscale or color one. If mode equals to `RETR_CCOMP` or `RETR_FLOODFILL`, the input can also be a 32-bit integer image of labels (CV_32SC1).
* `contours`: Detected contours. Each contour is stored as a vector of points (e.g. Point[][]).
* `hierarchy`: Optional output vector (e.g. Vec4i[]), containing information about the image topology. It has as many elements as the number of contours. For each i-th contour contours[i], the elements hierarchy[i][0] , hierarchy[i][1] , hierarchy[i][2] , and hierarchy[i][3] are set to 0-based indices in contours of the next and previous contours at the same hierarchical level, the first child contour and the parent contour, respectively. If for the contour i there are no next, previous, parent, or nested contours, the corresponding elements of hierarchy[i] will be negative.
* `mode`: Contour retrieval mode, see `RetrievalModes`
* `method`: Contour approximation method, see `ContourApproximationModes`
* `offset`: Optional offset by which every contour point is shifted. This is useful if the contours are extracted from the image ROI and then they should be analyzed in the whole image context.

---
### `Cv2.FindContoursLinkRuns`
**Signature**: `void FindContoursLinkRuns(Mat image, IntPtr contours, Mat hierarchy)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: Input image.
* `contours`: The contours parameter.
* `hierarchy`: The hierarchy parameter.

---
### `Cv2.FindContoursLinkRuns`
**Signature**: `void FindContoursLinkRuns(Mat image, IntPtr contours)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `image`: Input image.
* `contours`: The contours parameter.

---
### `Cv2.CreateGeneralizedHoughBallard`
**Signature**: `GeneralizedHoughBallard? CreateGeneralizedHoughBallard()`

Creates a smart pointer to a GeneralizedHoughBallard class and initializes it.

**Returns**: The returned value.

---
### `Cv2.CreateGeneralizedHoughGuil`
**Signature**: `GeneralizedHoughGuil? CreateGeneralizedHoughGuil()`

Creates a smart pointer to a GeneralizedHoughGuil class and initializes it.

**Returns**: The returned value.

---
### `Cv2.ApplyColorMap`
**Signature**: `void ApplyColorMap(Mat src, Mat dst, int colormap)`

Applies a GNU Octave/MATLAB equivalent colormap on a given image.

**Parameters**:
* `src`: The source image, grayscale or colored of type CV_8UC1 or CV_8UC3. If CV_8UC3, then the CV_8UC1 image is generated internally using COLOR_BGR2GRAY.
* `dst`: The result is the colormapped source image. Note: Mat.Create is called on dst.
* `colormap`: The colormap to apply, see `ColormapTypes`

---
### `Cv2.ApplyColorMap`
**Signature**: `void ApplyColorMap(Mat src, Mat dst, Mat userColor)`

Applies a user colormap on a given image.

**Parameters**:
* `src`: The source image, grayscale or colored of type CV_8UC1 or CV_8UC3. If CV_8UC3, then the CV_8UC1 image is generated internally using COLOR_BGR2GRAY.
* `dst`: The result is the colormapped source image of the same number of channels as userColor. Note: Mat.Create is called on dst.
* `userColor`: The colormap to apply of type CV_8UC1 or CV_8UC3 and size 256

---
### `Cv2.Line`
**Signature**: `void Line(Mat img, Point pt1, Point pt2, Scalar color, int thickness, int lineType, int shift)`

Draws a line segment connecting two points.

**Detailed Remarks**:
The function line draws the line segment between pt1 and pt2 points in the image. The line is
clipped by the image boundaries. For non-antialiased lines with integer coordinates, the 8-connected
or 4-connected Bresenham algorithm is used. Thick lines are drawn with rounding endings. Antialiased
lines are drawn using Gaussian filtering.

**Parameters**:
* `img`: Image.
* `pt1`: First point of the line segment.
* `pt2`: Second point of the line segment.
* `color`: Line color.
* `thickness`: Line thickness.
* `lineType`: Type of the line. See `LineTypes`.
* `shift`: Number of fractional bits in the point coordinates.

---
### `Cv2.ArrowedLine`
**Signature**: `void ArrowedLine(Mat img, Point pt1, Point pt2, Scalar color, int thickness, int line_type, int shift, double tipLength)`

Draws an arrow segment pointing from the first point to the second one.

**Detailed Remarks**:
The function arrowedLine draws an arrow between pt1 and pt2 points in the image. See also `line`.

**Parameters**:
* `img`: Image.
* `pt1`: The point the arrow starts from.
* `pt2`: The point the arrow points to.
* `color`: Line color.
* `thickness`: Line thickness.
* `line_type`: Type of the line. See `LineTypes`
* `shift`: Number of fractional bits in the point coordinates.
* `tipLength`: The length of the arrow tip in relation to the arrow length

---
### `Cv2.DrawFrameAxes`
**Signature**: `void DrawFrameAxes(Mat image, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, float length, int thickness)`

Draw axes of the world/object coordinate system from pose estimation. **See also:** SolvePnP * * **image** Input/output image. It must have 1 or 3 channels. The number of channels is not altered. * **cameraMatrix** Input 3x3 floating-point matrix of camera intrinsic parameters. * formula * **distCoeffs** Input vector of distortion coefficients * formula. If the vector is empty, the zero distortion coefficients are assumed. * **rvec** Rotation vector (see `Rodrigues` ) that, together with tvec, brings points from * the model coordinate system to the camera coordinate system. * **tvec** Translation vector. * **length** Length of the painted axes in the same unit than tvec (usually in meters). * **thickness** Line thickness of the painted axes. * * This function draws the axes of the world/object coordinate system w.r.t. to the camera frame. * OX is drawn in red, OY in green and OZ in blue.

**Parameters**:
* `image`: Input image.
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `rvec`: The rvec parameter.
* `tvec`: The tvec parameter.
* `length`: The length parameter.
* `thickness`: Line thickness.

---
### `Cv2.Rectangle`
**Signature**: `void Rectangle(Mat img, Point pt1, Point pt2, Scalar color, int thickness, int lineType, int shift)`

Draws a simple, thick, or filled up-right rectangle.

**Detailed Remarks**:
The function rectangle draws a rectangle outline or a filled rectangle whose two opposite corners
are pt1 and pt2.

**Parameters**:
* `img`: Image.
* `pt1`: Vertex of the rectangle.
* `pt2`: Vertex of the rectangle opposite to pt1 .
* `color`: Rectangle color or brightness (grayscale image).
* `thickness`: Thickness of lines that make up the rectangle. Negative values, like `FILLED`, mean that the function has to draw a filled rectangle.
* `lineType`: Type of the line. See `LineTypes`
* `shift`: Number of fractional bits in the point coordinates.

---
### `Cv2.Rectangle`
**Signature**: `void Rectangle(Mat img, Rect rec, Scalar color, int thickness, int lineType, int shift)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
use `rec` parameter as alternative specification of the drawn rectangle: `r.tl() and
r.br()-Point(1,1)` are opposite corners

**Parameters**:
* `img`: Input image.
* `rec`: The rec parameter.
* `color`: Color value (BGR or BGRA).
* `thickness`: Line thickness.
* `lineType`: Type of the line (see LineTypes).
* `shift`: Number of fractional bits in coordinates.

---
### `Cv2.Circle`
**Signature**: `void Circle(Mat img, Point center, int radius, Scalar color, int thickness, int lineType, int shift)`

Draws a circle.

**Detailed Remarks**:
The function circle draws a simple or filled circle with a given center and radius.

**Parameters**:
* `img`: Image where the circle is drawn.
* `center`: Center of the circle.
* `radius`: Radius of the circle.
* `color`: Circle color.
* `thickness`: Thickness of the circle outline, if positive. Negative values, like `FILLED`, mean that a filled circle is to be drawn.
* `lineType`: Type of the circle boundary. See `LineTypes`
* `shift`: Number of fractional bits in the coordinates of the center and in the radius value.

---
### `Cv2.Ellipse`
**Signature**: `void Ellipse(Mat img, Point center, Size axes, double angle, double startAngle, double endAngle, Scalar color, int thickness, int lineType, int shift)`

Draws a simple or thick elliptic arc or fills an ellipse sector.

**Detailed Remarks**:
The function ellipse with more parameters draws an ellipse outline, a filled ellipse, an elliptic
arc, or a filled ellipse sector. The drawing code uses general parametric form.
A piecewise-linear curve is used to approximate the elliptic arc
boundary. If you need more control of the ellipse rendering, you can retrieve the curve using
`ellipse2Poly` and then render it with `polylines` or fill it with `fillPoly`. If you use the first
variant of the function and want to draw the whole ellipse, not an arc, pass `startAngle=0` and
`endAngle=360`. If `startAngle` is greater than `endAngle`, they are swapped. The figure below explains
the meaning of the parameters to draw the blue arc.

**Parameters**:
* `img`: Image.
* `center`: Center of the ellipse.
* `axes`: Half of the size of the ellipse main axes.
* `angle`: Ellipse rotation angle in degrees.
* `startAngle`: Starting angle of the elliptic arc in degrees.
* `endAngle`: Ending angle of the elliptic arc in degrees.
* `color`: Ellipse color.
* `thickness`: Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that a filled ellipse sector is to be drawn.
* `lineType`: Type of the ellipse boundary. See `LineTypes`
* `shift`: Number of fractional bits in the coordinates of the center and values of axes.

---
### `Cv2.Ellipse`
**Signature**: `void Ellipse(Mat img, RotatedRect box, Scalar color, int thickness, int lineType)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `img`: Image.
* `box`: Alternative ellipse representation via RotatedRect. This means that the function draws an ellipse inscribed in the rotated rectangle.
* `color`: Ellipse color.
* `thickness`: Thickness of the ellipse arc outline, if positive. Otherwise, this indicates that a filled ellipse sector is to be drawn.
* `lineType`: Type of the ellipse boundary. See `LineTypes`

---
### `Cv2.DrawMarker`
**Signature**: `void DrawMarker(Mat img, Point position, Scalar color, int markerType, int markerSize, int thickness, int line_type)`

Draws a marker on a predefined position in an image.

**Detailed Remarks**:
The function drawMarker draws a marker on a given position in the image. For the moment several
marker types are supported, see `MarkerTypes` for more information.

**Parameters**:
* `img`: Image.
* `position`: The point where the crosshair is positioned.
* `color`: Line color.
* `markerType`: The specific type of marker you want to use, see `MarkerTypes`
* `markerSize`: The length of the marker axis [default = 20 pixels]
* `thickness`: Line thickness.
* `line_type`: Type of the line, See `LineTypes`

---
### `Cv2.FillConvexPoly`
**Signature**: `void FillConvexPoly(Mat img, Mat points, Scalar color, int lineType, int shift)`

Fills a convex polygon.

**Detailed Remarks**:
The function fillConvexPoly draws a filled convex polygon. This function is much faster than the
function `fillPoly` . It can fill not only convex polygons but any monotonic polygon without
self-intersections, that is, a polygon whose contour intersects every horizontal line (scan line)
twice at the most (though, its top-most and/or the bottom edge could be horizontal).

**Parameters**:
* `img`: Image.
* `points`: Polygon vertices.
* `color`: Polygon color.
* `lineType`: Type of the polygon boundaries. See `LineTypes`
* `shift`: Number of fractional bits in the vertex coordinates.

---
### `Cv2.FillPoly`
**Signature**: `void FillPoly(Mat img, IntPtr pts, Scalar color, int lineType, int shift, Point offset)`

Fills the area bounded by one or more polygons.

**Detailed Remarks**:
The function fillPoly fills an area bounded by several polygonal contours. The function can fill
complex areas, for example, areas with holes, contours with self-intersections (some of their
parts), and so forth.

**Parameters**:
* `img`: Image.
* `pts`: Array of polygons where each polygon is represented as an array of points.
* `color`: Polygon color.
* `lineType`: Type of the polygon boundaries. See `LineTypes`
* `shift`: Number of fractional bits in the vertex coordinates.
* `offset`: Optional offset of all points of the contours.

---
### `Cv2.Polylines`
**Signature**: `void Polylines(Mat img, IntPtr pts, bool isClosed, Scalar color, int thickness, int lineType, int shift)`

Draws several polygonal curves.

**Parameters**:
* `img`: Image.
* `pts`: Array of polygonal curves.
* `isClosed`: Flag indicating whether the drawn polylines are closed or not. If they are closed, the function draws a line from the last vertex of each curve to its first vertex.
* `color`: Polyline color.
* `thickness`: Thickness of the polyline edges.
* `lineType`: Type of the line segments. See `LineTypes`
* `shift`: Number of fractional bits in the vertex coordinates. The function polylines draws one or more polygonal curves.

---
### `Cv2.DrawContours`
**Signature**: `void DrawContours(Mat image, IntPtr contours, int contourIdx, Scalar color, int thickness, int lineType, Mat? hierarchy, int maxLevel, Point offset)`

Draws contours outlines or filled contours.

**Detailed Remarks**:
The function draws contour outlines in the image if formula or fills the area
bounded by the contours if formula . The example below shows how to retrieve
connected components from the binary image and label them: :
See example in OpenCV documentation. .: info Note
When thickness=`FILLED`, the function is designed to handle connected components with holes correctly
even when no hierarchy data is provided. This is done by analyzing all the outlines together
using even-odd rule. This may give incorrect results if you have a joint collection of separately retrieved
contours. In order to solve this problem, you need to call `drawContours` separately for each sub-group
of contours, or iterate over the collection using contourIdx parameter.
.:

**Parameters**:
* `image`: Destination image.
* `contours`: All the input contours. Each contour is stored as a point vector.
* `contourIdx`: Parameter indicating a contour to draw. If it is negative, all the contours are drawn.
* `color`: Color of the contours.
* `thickness`: Thickness of lines the contours are drawn with. If it is negative (for example, thickness=#FILLED ), the contour interiors are drawn.
* `lineType`: Line connectivity. See `LineTypes`
* `hierarchy`: Optional information about hierarchy. It is only needed if you want to draw only some of the contours (see maxLevel ).
* `maxLevel`: Maximal level for drawn contours. If it is 0, only the specified contour is drawn. If it is 1, the function draws the contour(s) and all the nested contours. If it is 2, the function draws the contours, all the nested contours, all the nested-to-nested contours, and so on. This parameter is only taken into account when there is hierarchy available.
* `offset`: Optional contour shift parameter. Shift all the drawn contours by the specified formula .

---
### `Cv2.ClipLine`
**Signature**: `bool ClipLine(Rect imgRect, Point pt1, Point pt2)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `imgRect`: Image rectangle.
* `pt1`: First line point.
* `pt2`: Second line point.

**Returns**: The returned value.

---
### `Cv2.Ellipse2Poly`
**Signature**: `void Ellipse2Poly(Point center, Size axes, int angle, int arcStart, int arcEnd, int delta, IntPtr pts)`

Approximates an elliptic arc with a polyline.

**Detailed Remarks**:
The function ellipse2Poly computes the vertices of a polyline that approximates the specified
elliptic arc. It is used by `ellipse`. If `arcStart` is greater than `arcEnd`, they are swapped.

**Parameters**:
* `center`: Center of the arc.
* `axes`: Half of the size of the ellipse main axes. See `ellipse` for details.
* `angle`: Rotation angle of the ellipse in degrees. See `ellipse` for details.
* `arcStart`: Starting angle of the elliptic arc in degrees.
* `arcEnd`: Ending angle of the elliptic arc in degrees.
* `delta`: Angle between the subsequent polyline vertices. It defines the approximation accuracy.
* `pts`: Output vector of polyline vertices.

---
### `Cv2.PutText`
**Signature**: `void PutText(Mat img, string text, Point org, int fontFace, double fontScale, Scalar color, int thickness, int lineType, bool bottomLeftOrigin)`

Draws a text string.

**Detailed Remarks**:
The function putText renders the specified text string in the image. Symbols that cannot be rendered
using the specified font are replaced by question marks. See `GetTextSize` for a text rendering code
example.
The `fontScale` parameter is a scale factor that is multiplied by the base font size:
- When scale > 1, the text is magnified.
- When 0 < scale < 1, the text is minimized.
- When scale < 0, the text is mirrored or reversed.

**Parameters**:
* `img`: Image.
* `text`: Text string to be drawn.
* `org`: Bottom-left corner of the text string in the image.
* `fontFace`: Font type, see `HersheyFonts`.
* `fontScale`: Font scale factor that is multiplied by the font-specific base size.
* `color`: Text color.
* `thickness`: Thickness of the lines used to draw a text.
* `lineType`: Line type. See `LineTypes`
* `bottomLeftOrigin`: When true, the image data origin is at the bottom-left corner. Otherwise, it is at the top-left corner.

---
### `Cv2.GetTextSize`
**Signature**: `Size GetTextSize(string text, int fontFace, double fontScale, int thickness, IntPtr baseLine)`

Calculates the width and height of a text string.

**Detailed Remarks**:
The function GetTextSize calculates and returns the size of a box that contains the specified text.
That is, the following code renders some text, the tight box surrounding it, and the baseline: :

```csharp
string text = "Funny text inside the box";
int fontFace = (int)HersheyFonts.ScriptSimplex;
double fontScale = 2;
int thickness = 3;
using var img = new Mat(600, 800, 16, new Scalar(0)); // CV_8UC3 = 16
IntPtr baselinePtr = System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(int));
try
{
    System.Runtime.InteropServices.Marshal.WriteInt32(baselinePtr, 0);
    Size textSize = Cv2.GetTextSize(text, fontFace, fontScale, thickness, baselinePtr);
    int baseline = System.Runtime.InteropServices.Marshal.ReadInt32(baselinePtr) + thickness;
    // center the text
    Point textOrg = new Point((img.Cols - textSize.Width) / 2, (img.Rows + textSize.Height) / 2);
    // draw the box
    Cv2.Rectangle(img, textOrg + new Point(0, baseline), textOrg + new Point(textSize.Width, -textSize.Height), new Scalar(0, 0, 255), 1, (int)LineTypes.Link8, 0);
    // ... and the baseline first
    Cv2.Line(img, textOrg + new Point(0, thickness), textOrg + new Point(textSize.Width, thickness), new Scalar(0, 0, 255), 1, (int)LineTypes.Link8, 0);
    // then put the text itself
    Cv2.PutText(img, text, textOrg, fontFace, fontScale, new Scalar(255, 255, 255), thickness, 8, false);
}
finally
{
    System.Runtime.InteropServices.Marshal.FreeHGlobal(baselinePtr);
}
```

**See also**: putText

**Parameters**:
* `text`: Input text string.
* `fontFace`: Font to use, see `HersheyFonts`.
* `fontScale`: Font scale factor that is multiplied by the font-specific base size.
* `thickness`: Thickness of lines used to render the text. See `putText` for details.
* `baseLine`: The baseLine parameter.

**Returns**: The size of a box that contains the specified text.

---
### `Cv2.GetFontScaleFromHeight`
**Signature**: `double GetFontScaleFromHeight(int fontFace, int pixelHeight, int thickness)`

Calculates the font-specific size to use to achieve a given height in pixels.

**Detailed Remarks**:
**See also**: putText

**Parameters**:
* `fontFace`: Font to use, see HersheyFonts.
* `pixelHeight`: Pixel height to compute the fontScale for
* `thickness`: Thickness of lines used to render the text.See putText for details.

**Returns**: The fontSize to use for putText

---
### `Cv2.PutText`
**Signature**: `Point PutText(Mat img, string text, Point org, Scalar color, FontFace fface, int size, int weight, PutTextFlags flags, Range wrap)`

Draws a text string using specified font.

**Detailed Remarks**:
The function putText renders the specified text string in the image. Symbols that cannot be rendered
using the specified font are replaced by question marks. See `GetTextSize` for a text rendering code
example. The function returns the coordinates in pixels from where the text can be continued.

**Parameters**:
* `img`: Image.
* `text`: Text string to be drawn.
* `org`: Bottom-left corner of the first character of the printed text (see PUT_TEXT_ALIGN_... though)
* `color`: Text color.
* `fface`: The font to use for the text
* `size`: Font size in pixels (by default) or pts
* `weight`: Font weight, 100..1000, where 100 is "thin" font, 400 is "regular", 600 is "semibold", 800 is "bold" and beyond that is "black". The parameter is ignored if the font is not a variable font or if it does not provide variation along 'wght' axis. If the weight is 0, then the weight, currently set via setInstance(), is used.
* `flags`: Various flags, see PUT_TEXT_...
* `wrap`: The optional text wrapping range: In the case of left-to-right (LTR) text if the printed character would cross wrap.end boundary, the "cursor" is set to wrap.start. In the case of right-to-left (RTL) text it's vice versa. If the parameters is not set, [org.x, img.cols] is used for LTR text and [0, org.x] is for RTL one.

**Returns**: The returned value.

---
### `Cv2.GetTextSize`
**Signature**: `Rect GetTextSize(Size imgsize, string text, Point org, FontFace fface, int size, int weight, PutTextFlags flags, Range wrap)`

Calculates the bounding rect for the text

**Detailed Remarks**:
The function GetTextSize calculates and returns the size of a box that contains the specified text.
That is, the following code renders some text, the tight box surrounding it, and the baseline: :

**Parameters**:
* `imgsize`: Size of the target image, can be empty
* `text`: Text string to be drawn.
* `org`: Bottom-left corner of the first character of the printed text (see PUT_TEXT_ALIGN_... though)
* `fface`: The font to use for the text
* `size`: Font size in pixels (by default) or pts
* `weight`: Font weight, 100..1000, where 100 is "thin" font, 400 is "regular", 600 is "semibold", 800 is "bold" and beyond that is "black". The default weight means "400" for variable-weight fonts or whatever "default" weight the used font provides.
* `flags`: Various flags, see PUT_TEXT_...
* `wrap`: The optional text wrapping range; see `putText`.

**Returns**: The returned value.

---
### `Cv2.HoughLinesWithAccumulator`
**Signature**: `void HoughLinesWithAccumulator(Mat image, Mat lines, double rho, double theta, int threshold, double srn, double stn, double min_theta, double max_theta, bool use_edgeval)`

Finds lines in a binary image using the standard Hough transform and retrieves the accumulator. 

**See also:** HoughLines

**Parameters**:
* `image`: Input image.
* `lines`: The lines parameter.
* `rho`: The rho parameter.
* `theta`: The theta parameter.
* `threshold`: The threshold parameter.
* `srn`: The srn parameter.
* `stn`: The stn parameter.
* `min_theta`: The min_theta parameter.
* `max_theta`: The max_theta parameter.
* `use_edgeval`: The use_edgeval parameter.

---
### `Cv2.HoughCirclesWithAccumulator`
**Signature**: `void HoughCirclesWithAccumulator(Mat image, Mat circles, int method, double dp, double minDist, double param1, double param2, int minRadius, int maxRadius)`

Finds circles in a grayscale image using the Hough transform and retrieves the accumulator. 

**See also:** HoughCircles

**Parameters**:
* `image`: Input image.
* `circles`: The circles parameter.
* `method`: The method parameter.
* `dp`: The dp parameter.
* `minDist`: The minDist parameter.
* `param1`: The param1 parameter.
* `param2`: The param2 parameter.
* `minRadius`: The minRadius parameter.
* `maxRadius`: The maxRadius parameter.

---
## 🔢 Enumerations

### `AdaptiveThresholdTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`MeanC`** | `0` | MeanC |
| **`GaussianC`** | `1` | GaussianC |

---
### `ColorConversionCodes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Bgr2bgra`** | `0` | Bgr2bgra |
| **`Rgb2rgba`** | `unchecked((int)(Bgr2bgra))` | Rgb2rgba |
| **`Bgra2bgr`** | `1` | Bgra2bgr |
| **`Rgba2rgb`** | `unchecked((int)(Bgra2bgr))` | Rgba2rgb |
| **`Bgr2rgba`** | `2` | Bgr2rgba |
| **`Rgb2bgra`** | `unchecked((int)(Bgr2rgba))` | Rgb2bgra |
| **`Rgba2bgr`** | `3` | Rgba2bgr |
| **`Bgra2rgb`** | `unchecked((int)(Rgba2bgr))` | Bgra2rgb |
| **`Bgr2rgb`** | `4` | Bgr2rgb |
| **`Rgb2bgr`** | `unchecked((int)(Bgr2rgb))` | Rgb2bgr |
| **`Bgra2rgba`** | `5` | Bgra2rgba |
| **`Rgba2bgra`** | `unchecked((int)(Bgra2rgba))` | Rgba2bgra |
| **`Bgr2gray`** | `6` | Bgr2gray |
| **`Rgb2gray`** | `7` | Rgb2gray |
| **`Gray2bgr`** | `8` | Gray2bgr |
| **`Gray2rgb`** | `unchecked((int)(Gray2bgr))` | Gray2rgb |
| **`Gray2bgra`** | `9` | Gray2bgra |
| **`Gray2rgba`** | `unchecked((int)(Gray2bgra))` | Gray2rgba |
| **`Bgra2gray`** | `10` | Bgra2gray |
| **`Rgba2gray`** | `11` | Rgba2gray |
| **`Bgr2bgr565`** | `12` | Bgr2bgr565 |
| **`Rgb2bgr565`** | `13` | Rgb2bgr565 |
| **`Bgr5652bgr`** | `14` | Bgr5652bgr |
| **`Bgr5652rgb`** | `15` | Bgr5652rgb |
| **`Bgra2bgr565`** | `16` | Bgra2bgr565 |
| **`Rgba2bgr565`** | `17` | Rgba2bgr565 |
| **`Bgr5652bgra`** | `18` | Bgr5652bgra |
| **`Bgr5652rgba`** | `19` | Bgr5652rgba |
| **`Gray2bgr565`** | `20` | Gray2bgr565 |
| **`Bgr5652gray`** | `21` | Bgr5652gray |
| **`Bgr2bgr555`** | `22` | Bgr2bgr555 |
| **`Rgb2bgr555`** | `23` | Rgb2bgr555 |
| **`Bgr5552bgr`** | `24` | Bgr5552bgr |
| **`Bgr5552rgb`** | `25` | Bgr5552rgb |
| **`Bgra2bgr555`** | `26` | Bgra2bgr555 |
| **`Rgba2bgr555`** | `27` | Rgba2bgr555 |
| **`Bgr5552bgra`** | `28` | Bgr5552bgra |
| **`Bgr5552rgba`** | `29` | Bgr5552rgba |
| **`Gray2bgr555`** | `30` | Gray2bgr555 |
| **`Bgr5552gray`** | `31` | Bgr5552gray |
| **`Bgr2xyz`** | `32` | Bgr2xyz |
| **`Rgb2xyz`** | `33` | Rgb2xyz |
| **`Xyz2bgr`** | `34` | Xyz2bgr |
| **`Xyz2rgb`** | `35` | Xyz2rgb |
| **`BGR2YCrCb`** | `36` | BGR2YCrCb |
| **`RGB2YCrCb`** | `37` | RGB2YCrCb |
| **`YCrCb2BGR`** | `38` | YCrCb2BGR |
| **`YCrCb2RGB`** | `39` | YCrCb2RGB |
| **`Bgr2hsv`** | `40` | Bgr2hsv |
| **`Rgb2hsv`** | `41` | Rgb2hsv |
| **`BGR2Lab`** | `44` | BGR2Lab |
| **`RGB2Lab`** | `45` | RGB2Lab |
| **`BGR2Luv`** | `50` | BGR2Luv |
| **`RGB2Luv`** | `51` | RGB2Luv |
| **`Bgr2hls`** | `52` | Bgr2hls |
| **`Rgb2hls`** | `53` | Rgb2hls |
| **`Hsv2bgr`** | `54` | Hsv2bgr |
| **`Hsv2rgb`** | `55` | Hsv2rgb |
| **`Lab2BGR`** | `56` | Lab2BGR |
| **`Lab2RGB`** | `57` | Lab2RGB |
| **`Luv2BGR`** | `58` | Luv2BGR |
| **`Luv2RGB`** | `59` | Luv2RGB |
| **`Hls2bgr`** | `60` | Hls2bgr |
| **`Hls2rgb`** | `61` | Hls2rgb |
| **`Bgr2hsvFull`** | `66` | Bgr2hsvFull |
| **`Rgb2hsvFull`** | `67` | Rgb2hsvFull |
| **`Bgr2hlsFull`** | `68` | Bgr2hlsFull |
| **`Rgb2hlsFull`** | `69` | Rgb2hlsFull |
| **`Hsv2bgrFull`** | `70` | Hsv2bgrFull |
| **`Hsv2rgbFull`** | `71` | Hsv2rgbFull |
| **`Hls2bgrFull`** | `72` | Hls2bgrFull |
| **`Hls2rgbFull`** | `73` | Hls2rgbFull |
| **`LBGR2Lab`** | `74` | LBGR2Lab |
| **`LRGB2Lab`** | `75` | LRGB2Lab |
| **`LBGR2Luv`** | `76` | LBGR2Luv |
| **`LRGB2Luv`** | `77` | LRGB2Luv |
| **`Lab2LBGR`** | `78` | Lab2LBGR |
| **`Lab2LRGB`** | `79` | Lab2LRGB |
| **`Luv2LBGR`** | `80` | Luv2LBGR |
| **`Luv2LRGB`** | `81` | Luv2LRGB |
| **`Bgr2yuv`** | `82` | Bgr2yuv |
| **`Rgb2yuv`** | `83` | Rgb2yuv |
| **`Yuv2bgr`** | `84` | Yuv2bgr |
| **`Yuv2rgb`** | `85` | Yuv2rgb |
| **`Yuv2rgbNv12`** | `90` | Yuv2rgbNv12 |
| **`Yuv2bgrNv12`** | `91` | Yuv2bgrNv12 |
| **`Yuv2rgbNv21`** | `92` | Yuv2rgbNv21 |
| **`Yuv2bgrNv21`** | `93` | Yuv2bgrNv21 |
| **`YUV420sp2RGB`** | `unchecked((int)(Yuv2rgbNv21))` | YUV420sp2RGB |
| **`YUV420sp2BGR`** | `unchecked((int)(Yuv2bgrNv21))` | YUV420sp2BGR |
| **`Yuv2rgbaNv12`** | `94` | Yuv2rgbaNv12 |
| **`Yuv2bgraNv12`** | `95` | Yuv2bgraNv12 |
| **`Yuv2rgbaNv21`** | `96` | Yuv2rgbaNv21 |
| **`Yuv2bgraNv21`** | `97` | Yuv2bgraNv21 |
| **`YUV420sp2RGBA`** | `unchecked((int)(Yuv2rgbaNv21))` | YUV420sp2RGBA |
| **`YUV420sp2BGRA`** | `unchecked((int)(Yuv2bgraNv21))` | YUV420sp2BGRA |
| **`Yuv2rgbYv12`** | `98` | Yuv2rgbYv12 |
| **`Yuv2bgrYv12`** | `99` | Yuv2bgrYv12 |
| **`Yuv2rgbIyuv`** | `100` | Yuv2rgbIyuv |
| **`Yuv2bgrIyuv`** | `101` | Yuv2bgrIyuv |
| **`Yuv2rgbI420`** | `unchecked((int)(Yuv2rgbIyuv))` | Yuv2rgbI420 |
| **`Yuv2bgrI420`** | `unchecked((int)(Yuv2bgrIyuv))` | Yuv2bgrI420 |
| **`YUV420p2RGB`** | `unchecked((int)(Yuv2rgbYv12))` | YUV420p2RGB |
| **`YUV420p2BGR`** | `unchecked((int)(Yuv2bgrYv12))` | YUV420p2BGR |
| **`Yuv2rgbaYv12`** | `102` | Yuv2rgbaYv12 |
| **`Yuv2bgraYv12`** | `103` | Yuv2bgraYv12 |
| **`Yuv2rgbaIyuv`** | `104` | Yuv2rgbaIyuv |
| **`Yuv2bgraIyuv`** | `105` | Yuv2bgraIyuv |
| **`Yuv2rgbaI420`** | `unchecked((int)(Yuv2rgbaIyuv))` | Yuv2rgbaI420 |
| **`Yuv2bgraI420`** | `unchecked((int)(Yuv2bgraIyuv))` | Yuv2bgraI420 |
| **`YUV420p2RGBA`** | `unchecked((int)(Yuv2rgbaYv12))` | YUV420p2RGBA |
| **`YUV420p2BGRA`** | `unchecked((int)(Yuv2bgraYv12))` | YUV420p2BGRA |
| **`Yuv2gray420`** | `106` | Yuv2gray420 |
| **`Yuv2grayNv21`** | `unchecked((int)(Yuv2gray420))` | Yuv2grayNv21 |
| **`Yuv2grayNv12`** | `unchecked((int)(Yuv2gray420))` | Yuv2grayNv12 |
| **`Yuv2grayYv12`** | `unchecked((int)(Yuv2gray420))` | Yuv2grayYv12 |
| **`Yuv2grayIyuv`** | `unchecked((int)(Yuv2gray420))` | Yuv2grayIyuv |
| **`Yuv2grayI420`** | `unchecked((int)(Yuv2gray420))` | Yuv2grayI420 |
| **`YUV420sp2GRAY`** | `unchecked((int)(Yuv2gray420))` | YUV420sp2GRAY |
| **`YUV420p2GRAY`** | `unchecked((int)(Yuv2gray420))` | YUV420p2GRAY |
| **`Yuv2rgbUyvy`** | `107` | Yuv2rgbUyvy |
| **`Yuv2bgrUyvy`** | `108` | Yuv2bgrUyvy |
| **`Yuv2rgbY422`** | `unchecked((int)(Yuv2rgbUyvy))` | Yuv2rgbY422 |
| **`Yuv2bgrY422`** | `unchecked((int)(Yuv2bgrUyvy))` | Yuv2bgrY422 |
| **`Yuv2rgbUynv`** | `unchecked((int)(Yuv2rgbUyvy))` | Yuv2rgbUynv |
| **`Yuv2bgrUynv`** | `unchecked((int)(Yuv2bgrUyvy))` | Yuv2bgrUynv |
| **`Yuv2rgbaUyvy`** | `111` | Yuv2rgbaUyvy |
| **`Yuv2bgraUyvy`** | `112` | Yuv2bgraUyvy |
| **`Yuv2rgbaY422`** | `unchecked((int)(Yuv2rgbaUyvy))` | Yuv2rgbaY422 |
| **`Yuv2bgraY422`** | `unchecked((int)(Yuv2bgraUyvy))` | Yuv2bgraY422 |
| **`Yuv2rgbaUynv`** | `unchecked((int)(Yuv2rgbaUyvy))` | Yuv2rgbaUynv |
| **`Yuv2bgraUynv`** | `unchecked((int)(Yuv2bgraUyvy))` | Yuv2bgraUynv |
| **`Yuv2rgbYuy2`** | `115` | Yuv2rgbYuy2 |
| **`Yuv2bgrYuy2`** | `116` | Yuv2bgrYuy2 |
| **`Yuv2rgbYvyu`** | `117` | Yuv2rgbYvyu |
| **`Yuv2bgrYvyu`** | `118` | Yuv2bgrYvyu |
| **`Yuv2rgbYuyv`** | `unchecked((int)(Yuv2rgbYuy2))` | Yuv2rgbYuyv |
| **`Yuv2bgrYuyv`** | `unchecked((int)(Yuv2bgrYuy2))` | Yuv2bgrYuyv |
| **`Yuv2rgbYunv`** | `unchecked((int)(Yuv2rgbYuy2))` | Yuv2rgbYunv |
| **`Yuv2bgrYunv`** | `unchecked((int)(Yuv2bgrYuy2))` | Yuv2bgrYunv |
| **`Yuv2rgbaYuy2`** | `119` | Yuv2rgbaYuy2 |
| **`Yuv2bgraYuy2`** | `120` | Yuv2bgraYuy2 |
| **`Yuv2rgbaYvyu`** | `121` | Yuv2rgbaYvyu |
| **`Yuv2bgraYvyu`** | `122` | Yuv2bgraYvyu |
| **`Yuv2rgbaYuyv`** | `unchecked((int)(Yuv2rgbaYuy2))` | Yuv2rgbaYuyv |
| **`Yuv2bgraYuyv`** | `unchecked((int)(Yuv2bgraYuy2))` | Yuv2bgraYuyv |
| **`Yuv2rgbaYunv`** | `unchecked((int)(Yuv2rgbaYuy2))` | Yuv2rgbaYunv |
| **`Yuv2bgraYunv`** | `unchecked((int)(Yuv2bgraYuy2))` | Yuv2bgraYunv |
| **`Yuv2grayUyvy`** | `123` | Yuv2grayUyvy |
| **`Yuv2grayYuy2`** | `124` | Yuv2grayYuy2 |
| **`Yuv2grayY422`** | `unchecked((int)(Yuv2grayUyvy))` | Yuv2grayY422 |
| **`Yuv2grayUynv`** | `unchecked((int)(Yuv2grayUyvy))` | Yuv2grayUynv |
| **`Yuv2grayYvyu`** | `unchecked((int)(Yuv2grayYuy2))` | Yuv2grayYvyu |
| **`Yuv2grayYuyv`** | `unchecked((int)(Yuv2grayYuy2))` | Yuv2grayYuyv |
| **`Yuv2grayYunv`** | `unchecked((int)(Yuv2grayYuy2))` | Yuv2grayYunv |
| **`RGBA2mRGBA`** | `125` | RGBA2mRGBA |
| **`MRGBA2RGBA`** | `126` | MRGBA2RGBA |
| **`Rgb2yuvI420`** | `127` | Rgb2yuvI420 |
| **`Bgr2yuvI420`** | `128` | Bgr2yuvI420 |
| **`Rgb2yuvIyuv`** | `unchecked((int)(Rgb2yuvI420))` | Rgb2yuvIyuv |
| **`Bgr2yuvIyuv`** | `unchecked((int)(Bgr2yuvI420))` | Bgr2yuvIyuv |
| **`Rgba2yuvI420`** | `129` | Rgba2yuvI420 |
| **`Bgra2yuvI420`** | `130` | Bgra2yuvI420 |
| **`Rgba2yuvIyuv`** | `unchecked((int)(Rgba2yuvI420))` | Rgba2yuvIyuv |
| **`Bgra2yuvIyuv`** | `unchecked((int)(Bgra2yuvI420))` | Bgra2yuvIyuv |
| **`Rgb2yuvYv12`** | `131` | Rgb2yuvYv12 |
| **`Bgr2yuvYv12`** | `132` | Bgr2yuvYv12 |
| **`Rgba2yuvYv12`** | `133` | Rgba2yuvYv12 |
| **`Bgra2yuvYv12`** | `134` | Bgra2yuvYv12 |
| **`BayerBG2BGR`** | `46` | BayerBG2BGR |
| **`BayerGB2BGR`** | `47` | BayerGB2BGR |
| **`BayerRG2BGR`** | `48` | BayerRG2BGR |
| **`BayerGR2BGR`** | `49` | BayerGR2BGR |
| **`BayerRGGB2BGR`** | `unchecked((int)(BayerBG2BGR))` | BayerRGGB2BGR |
| **`BayerGRBG2BGR`** | `unchecked((int)(BayerGB2BGR))` | BayerGRBG2BGR |
| **`BayerBGGR2BGR`** | `unchecked((int)(BayerRG2BGR))` | BayerBGGR2BGR |
| **`BayerGBRG2BGR`** | `unchecked((int)(BayerGR2BGR))` | BayerGBRG2BGR |
| **`BayerRGGB2RGB`** | `unchecked((int)(BayerBGGR2BGR))` | BayerRGGB2RGB |
| **`BayerGRBG2RGB`** | `unchecked((int)(BayerGBRG2BGR))` | BayerGRBG2RGB |
| **`BayerBGGR2RGB`** | `unchecked((int)(BayerRGGB2BGR))` | BayerBGGR2RGB |
| **`BayerGBRG2RGB`** | `unchecked((int)(BayerGRBG2BGR))` | BayerGBRG2RGB |
| **`BayerBG2RGB`** | `unchecked((int)(BayerRG2BGR))` | BayerBG2RGB |
| **`BayerGB2RGB`** | `unchecked((int)(BayerGR2BGR))` | BayerGB2RGB |
| **`BayerRG2RGB`** | `unchecked((int)(BayerBG2BGR))` | BayerRG2RGB |
| **`BayerGR2RGB`** | `unchecked((int)(BayerGB2BGR))` | BayerGR2RGB |
| **`BayerBG2GRAY`** | `86` | BayerBG2GRAY |
| **`BayerGB2GRAY`** | `87` | BayerGB2GRAY |
| **`BayerRG2GRAY`** | `88` | BayerRG2GRAY |
| **`BayerGR2GRAY`** | `89` | BayerGR2GRAY |
| **`BayerRGGB2GRAY`** | `unchecked((int)(BayerBG2GRAY))` | BayerRGGB2GRAY |
| **`BayerGRBG2GRAY`** | `unchecked((int)(BayerGB2GRAY))` | BayerGRBG2GRAY |
| **`BayerBGGR2GRAY`** | `unchecked((int)(BayerRG2GRAY))` | BayerBGGR2GRAY |
| **`BayerGBRG2GRAY`** | `unchecked((int)(BayerGR2GRAY))` | BayerGBRG2GRAY |
| **`BayerBG2BGRVNG`** | `62` | BayerBG2BGRVNG |
| **`BayerGB2BGRVNG`** | `63` | BayerGB2BGRVNG |
| **`BayerRG2BGRVNG`** | `64` | BayerRG2BGRVNG |
| **`BayerGR2BGRVNG`** | `65` | BayerGR2BGRVNG |
| **`BayerRGGB2BGRVNG`** | `unchecked((int)(BayerBG2BGRVNG))` | BayerRGGB2BGRVNG |
| **`BayerGRBG2BGRVNG`** | `unchecked((int)(BayerGB2BGRVNG))` | BayerGRBG2BGRVNG |
| **`BayerBGGR2BGRVNG`** | `unchecked((int)(BayerRG2BGRVNG))` | BayerBGGR2BGRVNG |
| **`BayerGBRG2BGRVNG`** | `unchecked((int)(BayerGR2BGRVNG))` | BayerGBRG2BGRVNG |
| **`BayerRGGB2RGBVNG`** | `unchecked((int)(BayerBGGR2BGRVNG))` | BayerRGGB2RGBVNG |
| **`BayerGRBG2RGBVNG`** | `unchecked((int)(BayerGBRG2BGRVNG))` | BayerGRBG2RGBVNG |
| **`BayerBGGR2RGBVNG`** | `unchecked((int)(BayerRGGB2BGRVNG))` | BayerBGGR2RGBVNG |
| **`BayerGBRG2RGBVNG`** | `unchecked((int)(BayerGRBG2BGRVNG))` | BayerGBRG2RGBVNG |
| **`BayerBG2RGBVNG`** | `unchecked((int)(BayerRG2BGRVNG))` | BayerBG2RGBVNG |
| **`BayerGB2RGBVNG`** | `unchecked((int)(BayerGR2BGRVNG))` | BayerGB2RGBVNG |
| **`BayerRG2RGBVNG`** | `unchecked((int)(BayerBG2BGRVNG))` | BayerRG2RGBVNG |
| **`BayerGR2RGBVNG`** | `unchecked((int)(BayerGB2BGRVNG))` | BayerGR2RGBVNG |
| **`BayerBG2BGREA`** | `135` | BayerBG2BGREA |
| **`BayerGB2BGREA`** | `136` | BayerGB2BGREA |
| **`BayerRG2BGREA`** | `137` | BayerRG2BGREA |
| **`BayerGR2BGREA`** | `138` | BayerGR2BGREA |
| **`BayerRGGB2BGREA`** | `unchecked((int)(BayerBG2BGREA))` | BayerRGGB2BGREA |
| **`BayerGRBG2BGREA`** | `unchecked((int)(BayerGB2BGREA))` | BayerGRBG2BGREA |
| **`BayerBGGR2BGREA`** | `unchecked((int)(BayerRG2BGREA))` | BayerBGGR2BGREA |
| **`BayerGBRG2BGREA`** | `unchecked((int)(BayerGR2BGREA))` | BayerGBRG2BGREA |
| **`BayerRGGB2RGBEA`** | `unchecked((int)(BayerBGGR2BGREA))` | BayerRGGB2RGBEA |
| **`BayerGRBG2RGBEA`** | `unchecked((int)(BayerGBRG2BGREA))` | BayerGRBG2RGBEA |
| **`BayerBGGR2RGBEA`** | `unchecked((int)(BayerRGGB2BGREA))` | BayerBGGR2RGBEA |
| **`BayerGBRG2RGBEA`** | `unchecked((int)(BayerGRBG2BGREA))` | BayerGBRG2RGBEA |
| **`BayerBG2RGBEA`** | `unchecked((int)(BayerRG2BGREA))` | BayerBG2RGBEA |
| **`BayerGB2RGBEA`** | `unchecked((int)(BayerGR2BGREA))` | BayerGB2RGBEA |
| **`BayerRG2RGBEA`** | `unchecked((int)(BayerBG2BGREA))` | BayerRG2RGBEA |
| **`BayerGR2RGBEA`** | `unchecked((int)(BayerGB2BGREA))` | BayerGR2RGBEA |
| **`BayerBG2BGRA`** | `139` | BayerBG2BGRA |
| **`BayerGB2BGRA`** | `140` | BayerGB2BGRA |
| **`BayerRG2BGRA`** | `141` | BayerRG2BGRA |
| **`BayerGR2BGRA`** | `142` | BayerGR2BGRA |
| **`BayerRGGB2BGRA`** | `unchecked((int)(BayerBG2BGRA))` | BayerRGGB2BGRA |
| **`BayerGRBG2BGRA`** | `unchecked((int)(BayerGB2BGRA))` | BayerGRBG2BGRA |
| **`BayerBGGR2BGRA`** | `unchecked((int)(BayerRG2BGRA))` | BayerBGGR2BGRA |
| **`BayerGBRG2BGRA`** | `unchecked((int)(BayerGR2BGRA))` | BayerGBRG2BGRA |
| **`BayerRGGB2RGBA`** | `unchecked((int)(BayerBGGR2BGRA))` | BayerRGGB2RGBA |
| **`BayerGRBG2RGBA`** | `unchecked((int)(BayerGBRG2BGRA))` | BayerGRBG2RGBA |
| **`BayerBGGR2RGBA`** | `unchecked((int)(BayerRGGB2BGRA))` | BayerBGGR2RGBA |
| **`BayerGBRG2RGBA`** | `unchecked((int)(BayerGRBG2BGRA))` | BayerGBRG2RGBA |
| **`BayerBG2RGBA`** | `unchecked((int)(BayerRG2BGRA))` | BayerBG2RGBA |
| **`BayerGB2RGBA`** | `unchecked((int)(BayerGR2BGRA))` | BayerGB2RGBA |
| **`BayerRG2RGBA`** | `unchecked((int)(BayerBG2BGRA))` | BayerRG2RGBA |
| **`BayerGR2RGBA`** | `unchecked((int)(BayerGB2BGRA))` | BayerGR2RGBA |
| **`Rgb2yuvUyvy`** | `143` | Rgb2yuvUyvy |
| **`Bgr2yuvUyvy`** | `144` | Bgr2yuvUyvy |
| **`Rgb2yuvY422`** | `unchecked((int)(Rgb2yuvUyvy))` | Rgb2yuvY422 |
| **`Bgr2yuvY422`** | `unchecked((int)(Bgr2yuvUyvy))` | Bgr2yuvY422 |
| **`Rgb2yuvUynv`** | `unchecked((int)(Rgb2yuvUyvy))` | Rgb2yuvUynv |
| **`Bgr2yuvUynv`** | `unchecked((int)(Bgr2yuvUyvy))` | Bgr2yuvUynv |
| **`Rgba2yuvUyvy`** | `145` | Rgba2yuvUyvy |
| **`Bgra2yuvUyvy`** | `146` | Bgra2yuvUyvy |
| **`Rgba2yuvY422`** | `unchecked((int)(Rgba2yuvUyvy))` | Rgba2yuvY422 |
| **`Bgra2yuvY422`** | `unchecked((int)(Bgra2yuvUyvy))` | Bgra2yuvY422 |
| **`Rgba2yuvUynv`** | `unchecked((int)(Rgba2yuvUyvy))` | Rgba2yuvUynv |
| **`Bgra2yuvUynv`** | `unchecked((int)(Bgra2yuvUyvy))` | Bgra2yuvUynv |
| **`Rgb2yuvYuy2`** | `147` | Rgb2yuvYuy2 |
| **`Bgr2yuvYuy2`** | `148` | Bgr2yuvYuy2 |
| **`Rgb2yuvYvyu`** | `149` | Rgb2yuvYvyu |
| **`Bgr2yuvYvyu`** | `150` | Bgr2yuvYvyu |
| **`Rgb2yuvYuyv`** | `unchecked((int)(Rgb2yuvYuy2))` | Rgb2yuvYuyv |
| **`Bgr2yuvYuyv`** | `unchecked((int)(Bgr2yuvYuy2))` | Bgr2yuvYuyv |
| **`Rgb2yuvYunv`** | `unchecked((int)(Rgb2yuvYuy2))` | Rgb2yuvYunv |
| **`Bgr2yuvYunv`** | `unchecked((int)(Bgr2yuvYuy2))` | Bgr2yuvYunv |
| **`Rgba2yuvYuy2`** | `151` | Rgba2yuvYuy2 |
| **`Bgra2yuvYuy2`** | `152` | Bgra2yuvYuy2 |
| **`Rgba2yuvYvyu`** | `153` | Rgba2yuvYvyu |
| **`Bgra2yuvYvyu`** | `154` | Bgra2yuvYvyu |
| **`Rgba2yuvYuyv`** | `unchecked((int)(Rgba2yuvYuy2))` | Rgba2yuvYuyv |
| **`Bgra2yuvYuyv`** | `unchecked((int)(Bgra2yuvYuy2))` | Bgra2yuvYuyv |
| **`Rgba2yuvYunv`** | `unchecked((int)(Rgba2yuvYuy2))` | Rgba2yuvYunv |
| **`Bgra2yuvYunv`** | `unchecked((int)(Bgra2yuvYuy2))` | Bgra2yuvYunv |
| **`ColorcvtMax`** | `155` | ColorcvtMax |

---
### `ColormapTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Autumn`** | `0` | Autumn |
| **`Bone`** | `1` | Bone |
| **`Jet`** | `2` | Jet |
| **`Winter`** | `3` | Winter |
| **`Rainbow`** | `4` | Rainbow |
| **`Ocean`** | `5` | Ocean |
| **`Summer`** | `6` | Summer |
| **`Spring`** | `7` | Spring |
| **`Cool`** | `8` | Cool |
| **`Hsv`** | `9` | Hsv |
| **`Pink`** | `10` | Pink |
| **`Hot`** | `11` | Hot |
| **`Parula`** | `12` | Parula |
| **`Magma`** | `13` | Magma |
| **`Inferno`** | `14` | Inferno |
| **`Plasma`** | `15` | Plasma |
| **`Viridis`** | `16` | Viridis |
| **`Cividis`** | `17` | Cividis |
| **`Twilight`** | `18` | Twilight |
| **`TwilightShifted`** | `19` | TwilightShifted |
| **`Turbo`** | `20` | Turbo |
| **`Deepgreen`** | `21` | Deepgreen |

---
### `ConnectedComponentsAlgorithmsTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `-1` | Default |
| **`Wu`** | `0` | Wu |
| **`Grana`** | `1` | Grana |
| **`Bolelli`** | `2` | Bolelli |
| **`Sauf`** | `3` | Sauf |
| **`Bbdt`** | `4` | Bbdt |
| **`Spaghetti`** | `5` | Spaghetti |

---
### `ConnectedComponentsTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Left`** | `0` | Left |
| **`Top`** | `1` | Top |
| **`Width`** | `2` | Width |
| **`Height`** | `3` | Height |
| **`Area`** | `4` | Area |
| **`Max`** | `5` | Max |

---
### `ContourApproximationModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ChainCode`** | `0` | ChainCode |
| **`ChainApproxNone`** | `1` | ChainApproxNone |
| **`ChainApproxSimple`** | `2` | ChainApproxSimple |
| **`ChainApproxTc89L1`** | `3` | ChainApproxTc89L1 |
| **`ChainApproxTc89Kcos`** | `4` | ChainApproxTc89Kcos |
| **`LinkRuns`** | `5` | LinkRuns |

---
### `DistanceTransformLabelTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Ccomp`** | `0` | Ccomp |
| **`Pixel`** | `1` | Pixel |

---
### `DistanceTransformMasks`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_3`** | `3` | _3 |
| **`_5`** | `5` | _5 |
| **`Precise`** | `0` | Precise |

---
### `FloodFillFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FixedRange`** | `unchecked((int)(1 << 16))` | FixedRange |
| **`MaskOnly`** | `unchecked((int)(1 << 17))` | MaskOnly |

---
### `GrabCutClasses`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Bgd`** | `0` | Bgd |
| **`Fgd`** | `1` | Fgd |
| **`PrBgd`** | `2` | PrBgd |
| **`PrFgd`** | `3` | PrFgd |

---
### `GrabCutModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`InitWithRect`** | `0` | InitWithRect |
| **`InitWithMask`** | `1` | InitWithMask |
| **`Eval`** | `2` | Eval |
| **`EvalFreezeModel`** | `3` | EvalFreezeModel |

---
### `HersheyFonts`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`HersheySimplex`** | `0` | HersheySimplex |
| **`HersheyPlain`** | `1` | HersheyPlain |
| **`HersheyDuplex`** | `2` | HersheyDuplex |
| **`HersheyComplex`** | `3` | HersheyComplex |
| **`HersheyTriplex`** | `4` | HersheyTriplex |
| **`HersheyComplexSmall`** | `5` | HersheyComplexSmall |
| **`HersheyScriptSimplex`** | `6` | HersheyScriptSimplex |
| **`HersheyScriptComplex`** | `7` | HersheyScriptComplex |
| **`Italic`** | `16` | Italic |

---
### `HistCompMethods`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Correl`** | `0` | Correl |
| **`Chisqr`** | `1` | Chisqr |
| **`Intersect`** | `2` | Intersect |
| **`Bhattacharyya`** | `3` | Bhattacharyya |
| **`Hellinger`** | `unchecked((int)(Bhattacharyya))` | Hellinger |
| **`ChisqrAlt`** | `4` | ChisqrAlt |
| **`KlDiv`** | `5` | KlDiv |

---
### `HoughModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Standard`** | `0` | Standard |
| **`Probabilistic`** | `1` | Probabilistic |
| **`MultiScale`** | `2` | MultiScale |
| **`Gradient`** | `3` | Gradient |
| **`GradientAlt`** | `4` | GradientAlt |

---
### `InterpolationFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`InterNearest`** | `0` | InterNearest |
| **`InterLinear`** | `1` | InterLinear |
| **`InterCubic`** | `2` | InterCubic |
| **`InterArea`** | `3` | InterArea |
| **`InterLanczos4`** | `4` | InterLanczos4 |
| **`InterLinearExact`** | `5` | InterLinearExact |
| **`InterNearestExact`** | `6` | InterNearestExact |
| **`InterMax`** | `7` | InterMax |
| **`WarpFillOutliers`** | `8` | WarpFillOutliers |
| **`WarpInverseMap`** | `16` | WarpInverseMap |
| **`WarpRelativeMap`** | `32` | WarpRelativeMap |

---
### `InterpolationMasks`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Bits`** | `5` | Bits |
| **`Bits2`** | `unchecked((int)(Bits * 2))` | Bits2 |
| **`TabSize`** | `unchecked((int)(1 << Bits))` | TabSize |
| **`TabSize2`** | `unchecked((int)(TabSize * TabSize))` | TabSize2 |

---
### `LineSegmentDetectorModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Std`** | `1` | Std |
| **`Adv`** | `2` | Adv |

---
### `LineTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Filled`** | `-1` | Filled |
| **`Line4`** | `4` | Line4 |
| **`Line8`** | `8` | Line8 |
| **`LineAa`** | `16` | LineAa |

---
### `MarkerTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Cross`** | `0` | Cross |
| **`TiltedCross`** | `1` | TiltedCross |
| **`Star`** | `2` | Star |
| **`Diamond`** | `3` | Diamond |
| **`Square`** | `4` | Square |
| **`TriangleUp`** | `5` | TriangleUp |
| **`TriangleDown`** | `6` | TriangleDown |

---
### `MorphShapes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Rect`** | `0` | Rect |
| **`Cross`** | `1` | Cross |
| **`Ellipse`** | `2` | Ellipse |
| **`Diamond`** | `3` | Diamond |

---
### `MorphTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Erode`** | `0` | Erode |
| **`Dilate`** | `1` | Dilate |
| **`Open`** | `2` | Open |
| **`Close`** | `3` | Close |
| **`Gradient`** | `4` | Gradient |
| **`Tophat`** | `5` | Tophat |
| **`Blackhat`** | `6` | Blackhat |
| **`Hitmiss`** | `7` | Hitmiss |

---
### `PutTextFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`AlignLeft`** | `0` | AlignLeft |
| **`AlignCenter`** | `1` | AlignCenter |
| **`AlignRight`** | `2` | AlignRight |
| **`AlignMask`** | `3` | AlignMask |
| **`OriginTl`** | `0` | OriginTl |
| **`OriginBl`** | `32` | OriginBl |
| **`Wrap`** | `128` | Wrap |

---
### `RetrievalModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`External`** | `0` | External |
| **`List`** | `1` | List |
| **`Ccomp`** | `2` | Ccomp |
| **`Tree`** | `3` | Tree |
| **`Floodfill`** | `4` | Floodfill |

---
### `ShapeMatchModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`I1`** | `1` | I1 |
| **`I2`** | `2` | I2 |
| **`I3`** | `3` | I3 |

---
### `SpecialFilter`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FilterScharr`** | `-1` | FilterScharr |

---
### `TemplateMatchModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Sqdiff`** | `0` | Sqdiff |
| **`SqdiffNormed`** | `1` | SqdiffNormed |
| **`Ccorr`** | `2` | Ccorr |
| **`CcorrNormed`** | `3` | CcorrNormed |
| **`Ccoeff`** | `4` | Ccoeff |
| **`CcoeffNormed`** | `5` | CcoeffNormed |

---
### `ThresholdTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Binary`** | `0` | Binary |
| **`BinaryInv`** | `1` | BinaryInv |
| **`Trunc`** | `2` | Trunc |
| **`Tozero`** | `3` | Tozero |
| **`TozeroInv`** | `4` | TozeroInv |
| **`Mask`** | `7` | Mask |
| **`Otsu`** | `8` | Otsu |
| **`Triangle`** | `16` | Triangle |
| **`Dryrun`** | `128` | Dryrun |

---
### `UndistortTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Ortho`** | `0` | Ortho |
| **`Eqrect`** | `1` | Eqrect |

---
### `WarpPolarMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Linear`** | `0` | Linear |
| **`Log`** | `256` | Log |

---

</div>