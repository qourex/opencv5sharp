# VIDEO Module API Reference

Complete documentation for the **VIDEO** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Video Documentation](https://docs.opencv.org/5.x/main_modules/video.html).

---
<div v-pre>

## 📦 Classes and Structs

### `BackgroundSubtractor`
**Inherits from**: `Algorithm`

Base class for background/foreground segmentation. :

**Detailed Remarks**:
The class is only used to define the common interface for the whole family of background/foreground
segmentation algorithms.

#### Methods
* `void Apply(Mat image, Mat fgmask, double learningRate)`
  * *Summary*: Computes a foreground mask.
  * *Parameter* `image`: Next video frame.
  * *Parameter* `fgmask`: The output foreground mask as an 8-bit binary image.
  * *Parameter* `learningRate`: The value between 0 and 1 that indicates how fast the background model is learnt. Negative parameter value makes the algorithm to use some automatically chosen learning rate. 0 means that the background model is not updated at all, 1 means that the background model is completely reinitialized from the last frame.
* `void Apply(Mat image, Mat knownForegroundMask, Mat fgmask, double learningRate)`
  * *Summary*: Computes a foreground mask with known foreground mask input.
  * *Remarks*:

.: info Note
This method has a default virtual implementation that throws a "not impemented" error.
Foreground masking may not be supported by all background subtractors.
.:

  * *Parameter* `image`: Next video frame. Floating point frame will be used without scaling and should be in range formula.
  * *Parameter* `knownForegroundMask`: The mask for inputting already known foreground, allows model to ignore pixels.
  * *Parameter* `fgmask`: The output foreground mask as an 8-bit binary image.
  * *Parameter* `learningRate`: The value between 0 and 1 that indicates how fast the background model is learnt. Negative parameter value makes the algorithm to use some automatically chosen learning rate. 0 means that the background model is not updated at all, 1 means that the background model is completely reinitialized from the last frame.
* `void GetBackgroundImage(Mat backgroundImage)`
  * *Summary*: Computes a background image.
  * *Remarks*:

.: info Note
Sometimes the background image can be very blurry, as it contain the average background
statistics.
.:

  * *Parameter* `backgroundImage`: The output background image.

---
### `BackgroundSubtractorKNN`
**Inherits from**: `BackgroundSubtractor`

K-nearest neighbours - based Background/Foreground Segmentation Algorithm.

**Detailed Remarks**:
The class implements the K-nearest neighbours background subtraction described in **Citation**:  Zivkovic2006 .
Very efficient if number of foreground pixels is low.

#### Methods
* `int GetHistory()`
  * *Summary*: Returns the number of last frames that affect the background model
  * *Returns*: The returned value.
* `void SetHistory(int history)`
  * *Summary*: Sets the number of last frames that affect the background model
  * *Parameter* `history`: The history parameter.
* `int GetNSamples()`
  * *Summary*: Returns the number of data samples in the background model
  * *Returns*: The returned value.
* `void SetNSamples(int _nN)`
  * *Summary*: Sets the number of data samples in the background model.
  * *Remarks*:

The model needs to be reinitialized to reserve memory.

  * *Parameter* `_nN`: The _nN parameter.
* `double GetDist2Threshold()`
  * *Summary*: Returns the threshold on the squared distance between the pixel and the sample
  * *Remarks*:

The threshold on the squared distance between the pixel and the sample to decide whether a pixel is
close to a data sample.

  * *Returns*: The returned value.
* `void SetDist2Threshold(double _dist2Threshold)`
  * *Summary*: Sets the threshold on the squared distance
  * *Parameter* `_dist2Threshold`: The _dist2Threshold parameter.
* `int GetkNNSamples()`
  * *Summary*: Returns the number of neighbours, the k in the kNN.
  * *Remarks*:

K is the number of samples that need to be within dist2Threshold in order to decide that that
pixel is matching the kNN background model.

  * *Returns*: The returned value.
* `void SetkNNSamples(int _nkNN)`
  * *Summary*: Sets the k in the kNN. How many nearest neighbours need to match.
  * *Parameter* `_nkNN`: The _nkNN parameter.
* `bool GetDetectShadows()`
  * *Summary*: Returns the shadow detection flag
  * *Remarks*:

If true, the algorithm detects shadows and marks them. See createBackgroundSubtractorKNN for
details.

  * *Returns*: The returned value.
* `void SetDetectShadows(bool detectShadows)`
  * *Summary*: Enables or disables shadow detection
  * *Parameter* `detectShadows`: The detectShadows parameter.
* `int GetShadowValue()`
  * *Summary*: Returns the shadow value
  * *Remarks*:

Shadow value is the value used to mark shadows in the foreground mask. Default value is 127. Value 0
in the mask always means background, 255 means foreground.

  * *Returns*: The returned value.
* `void SetShadowValue(int value)`
  * *Summary*: Sets the shadow value
  * *Parameter* `value`: The value parameter.
* `double GetShadowThreshold()`
  * *Summary*: Returns the shadow threshold
  * *Remarks*:

A shadow is detected if pixel is a darker version of the background. The shadow threshold (Tau in
the paper) is a threshold defining how much darker the shadow can be. Tau= 0.5 means that if a pixel
is more than twice darker then it is not shadow. See Prati, Mikic, Trivedi and Cucchiara,
Detecting Moving Shadows...*, IEEE PAMI,2003.

  * *Returns*: The returned value.
* `void SetShadowThreshold(double threshold)`
  * *Summary*: Sets the shadow threshold
  * *Parameter* `threshold`: The threshold parameter.

---
### `BackgroundSubtractorMOG2`
**Inherits from**: `BackgroundSubtractor`

Gaussian Mixture-based Background/Foreground Segmentation Algorithm.

**Detailed Remarks**:
The class implements the Gaussian mixture model background subtraction described in **Citation**:  Zivkovic2004
and **Citation**:  Zivkovic2006 .

#### Methods
* `int GetHistory()`
  * *Summary*: Returns the number of last frames that affect the background model
  * *Returns*: The returned value.
* `void SetHistory(int history)`
  * *Summary*: Sets the number of last frames that affect the background model
  * *Parameter* `history`: The history parameter.
* `int GetNMixtures()`
  * *Summary*: Returns the number of gaussian components in the background model
  * *Returns*: The returned value.
* `void SetNMixtures(int nmixtures)`
  * *Summary*: Sets the number of gaussian components in the background model.
  * *Remarks*:

The model needs to be reinitialized to reserve memory.

  * *Parameter* `nmixtures`: The nmixtures parameter.
* `double GetBackgroundRatio()`
  * *Summary*: Returns the "background ratio" parameter of the algorithm
  * *Remarks*:

If a foreground pixel keeps semi-constant value for about backgroundRatio\*history frames, it's
considered background and added to the model as a center of a new component. It corresponds to TB
parameter in the paper.

  * *Returns*: The returned value.
* `void SetBackgroundRatio(double ratio)`
  * *Summary*: Sets the "background ratio" parameter of the algorithm
  * *Parameter* `ratio`: The ratio parameter.
* `double GetVarThreshold()`
  * *Summary*: Returns the variance threshold for the pixel-model match
  * *Remarks*:

The main threshold on the squared Mahalanobis distance to decide if the sample is well described by
the background model or not. Related to Cthr from the paper.

  * *Returns*: The returned value.
* `void SetVarThreshold(double varThreshold)`
  * *Summary*: Sets the variance threshold for the pixel-model match
  * *Parameter* `varThreshold`: The varThreshold parameter.
* `double GetVarThresholdGen()`
  * *Summary*: Returns the variance threshold for the pixel-model match used for new mixture component generation
  * *Remarks*:

Threshold for the squared Mahalanobis distance that helps decide when a sample is close to the
existing components (corresponds to Tg in the paper). If a pixel is not close to any component, it
is considered foreground or added as a new component. 3 sigma =\> Tg=3\*3=9 is default. A smaller Tg
value generates more components. A higher Tg value may result in a small number of components but
they can grow too large.

  * *Returns*: The returned value.
* `void SetVarThresholdGen(double varThresholdGen)`
  * *Summary*: Sets the variance threshold for the pixel-model match used for new mixture component generation
  * *Parameter* `varThresholdGen`: The varThresholdGen parameter.
* `double GetVarInit()`
  * *Summary*: Returns the initial variance of each gaussian component
  * *Returns*: The returned value.
* `void SetVarInit(double varInit)`
  * *Summary*: Sets the initial variance of each gaussian component
  * *Parameter* `varInit`: The varInit parameter.
* `double GetVarMin()`
  * *Summary*: Returns the minimum value for the variance of each Gaussian component
  * *Returns*: The returned value.
* `void SetVarMin(double varMin)`
  * *Summary*: Sets the minimum value for the variance of each Gaussian component
  * *Parameter* `varMin`: The minimum variance value.
* `double GetVarMax()`
  * *Summary*: Returns the maximum value for the variance of each Gaussian component
  * *Returns*: The returned value.
* `void SetVarMax(double varMax)`
  * *Summary*: Sets the maximum value for the variance of each Gaussian component
  * *Parameter* `varMax`: The maximum variance value.
* `double GetComplexityReductionThreshold()`
  * *Summary*: Returns the complexity reduction threshold
  * *Remarks*:

This parameter defines the number of samples needed to accept to prove the component exists. CT=0.05
is a default value for all the samples. By setting CT=0 you get an algorithm very similar to the
standard Stauffer&Grimson algorithm.

  * *Returns*: The returned value.
* `void SetComplexityReductionThreshold(double ct)`
  * *Summary*: Sets the complexity reduction threshold
  * *Parameter* `ct`: The ct parameter.
* `bool GetDetectShadows()`
  * *Summary*: Returns the shadow detection flag
  * *Remarks*:

If true, the algorithm detects shadows and marks them. See createBackgroundSubtractorMOG2 for
details.

  * *Returns*: The returned value.
* `void SetDetectShadows(bool detectShadows)`
  * *Summary*: Enables or disables shadow detection
  * *Parameter* `detectShadows`: The detectShadows parameter.
* `int GetShadowValue()`
  * *Summary*: Returns the shadow value
  * *Remarks*:

Shadow value is the value used to mark shadows in the foreground mask. Default value is 127. Value 0
in the mask always means background, 255 means foreground.

  * *Returns*: The returned value.
* `void SetShadowValue(int value)`
  * *Summary*: Sets the shadow value
  * *Parameter* `value`: The value parameter.
* `double GetShadowThreshold()`
  * *Summary*: Returns the shadow threshold
  * *Remarks*:

A shadow is detected if pixel is a darker version of the background. The shadow threshold (Tau in
the paper) is a threshold defining how much darker the shadow can be. Tau= 0.5 means that if a pixel
is more than twice darker then it is not shadow. See Prati, Mikic, Trivedi and Cucchiara,
Detecting Moving Shadows...*, IEEE PAMI,2003.

  * *Returns*: The returned value.
* `void SetShadowThreshold(double threshold)`
  * *Summary*: Sets the shadow threshold
  * *Parameter* `threshold`: The threshold parameter.
* `void Apply(Mat image, Mat fgmask, double learningRate)`
  * *Summary*: Computes a foreground mask.
  * *Parameter* `image`: Next video frame. Floating point frame will be used without scaling and should be in range formula.
  * *Parameter* `fgmask`: The output foreground mask as an 8-bit binary image.
  * *Parameter* `learningRate`: The value between 0 and 1 that indicates how fast the background model is learnt. Negative parameter value makes the algorithm to use some automatically chosen learning rate. 0 means that the background model is not updated at all, 1 means that the background model is completely reinitialized from the last frame.
* `void Apply(Mat image, Mat knownForegroundMask, Mat fgmask, double learningRate)`
  * *Summary*: Computes a foreground mask and skips known foreground in evaluation.
  * *Parameter* `image`: Next video frame. Floating point frame will be used without scaling and should be in range formula.
  * *Parameter* `knownForegroundMask`: The mask for inputting already known foreground, allows model to ignore pixels.
  * *Parameter* `fgmask`: The output foreground mask as an 8-bit binary image.
  * *Parameter* `learningRate`: The value between 0 and 1 that indicates how fast the background model is learnt. Negative parameter value makes the algorithm to use some automatically chosen learning rate. 0 means that the background model is not updated at all, 1 means that the background model is completely reinitialized from the last frame.

---
### `DISOpticalFlow`
**Inherits from**: `DenseOpticalFlow`

DIS optical flow algorithm.

**Detailed Remarks**:
This class implements the Dense Inverse Search (DIS) optical flow algorithm. More
details about the algorithm can be found at **Citation**:  Kroeger2016 . Includes three presets with preselected
parameters to provide reasonable trade-off between speed and quality. However, even the slowest preset is
still relatively fast, use DeepFlow if you need better quality and don't care about speed.
This implementation includes several additional features compared to the algorithm described in the paper,
including spatial propagation of flow vectors (`getUseSpatialPropagation`), as well as an option to
utilize an initial flow approximation passed to `calc` (which is, essentially, temporal propagation,
if the previous frame's flow field is passed).

#### Methods
* `int GetFinestScale()`
  * *Summary*: Finest level of the Gaussian pyramid on which the flow is computed (zero level corresponds to the original image resolution). The final flow is obtained by bilinear upscaling.
  * *Remarks*:

**See also**: setFinestScale

  * *Returns*: The returned value.
* `void SetFinestScale(int val)`
  * *Summary*: Sets the finest level of the Gaussian pyramid on which the flow is computed. **See also:** GetFinestScale
  * *Parameter* `val`: The finest scale value.
* `void SetCoarsestScale(int val)`
  * *Summary*: Sets the coarsest scale
  * *Parameter* `val`: Coarsest level of the Gaussian pyramid on which the flow is computed. If set to -1, the auto-computed coarsest scale will be used.
* `int GetCoarsestScale()`
  * *Summary*: Gets the coarsest scale
  * *Returns*: The returned value.
* `int GetPatchSize()`
  * *Summary*: Size of an image patch for matching (in pixels). Normally, default 8x8 patches work well enough in most cases.
  * *Remarks*:

**See also**: setPatchSize

  * *Returns*: The returned value.
* `void SetPatchSize(int val)`
  * *Summary*: Sets the size of an image patch for matching (in pixels). **See also:** GetPatchSize
  * *Parameter* `val`: The patch size value.
* `int GetPatchStride()`
  * *Summary*: Stride between neighbor patches. Must be less than patch size. Lower values correspond to higher flow quality.
  * *Remarks*:

**See also**: setPatchStride

  * *Returns*: The returned value.
* `void SetPatchStride(int val)`
  * *Summary*: Sets the stride between neighbor patches. **See also:** GetPatchStride
  * *Parameter* `val`: The patch stride value.
* `int GetGradientDescentIterations()`
  * *Summary*: Maximum number of gradient descent iterations in the patch inverse search stage. Higher values may improve quality in some cases.
  * *Remarks*:

**See also**: setGradientDescentIterations

  * *Returns*: The returned value.
* `void SetGradientDescentIterations(int val)`
  * *Summary*: Sets the maximum number of gradient descent iterations in the patch inverse search stage. **See also:** GetGradientDescentIterations
  * *Parameter* `val`: The number of gradient descent iterations.
* `int GetVariationalRefinementIterations()`
  * *Summary*: Number of fixed point iterations of variational refinement per scale. Set to zero to disable variational refinement completely. Higher values will typically result in more smooth and high-quality flow.
  * *Remarks*:

**See also**: setGradientDescentIterations

  * *Returns*: The returned value.
* `void SetVariationalRefinementIterations(int val)`
  * *Summary*: Sets the number of fixed point iterations of variational refinement per scale. **See also:** GetVariationalRefinementIterations
  * *Parameter* `val`: The number of variational refinement iterations.
* `float GetVariationalRefinementAlpha()`
  * *Summary*: Weight of the smoothness term
  * *Remarks*:

**See also**: setVariationalRefinementAlpha

  * *Returns*: The returned value.
* `void SetVariationalRefinementAlpha(float val)`
  * *Summary*: Sets the weight of the smoothness term. **See also:** GetVariationalRefinementAlpha
  * *Parameter* `val`: The smoothness term weight.
* `float GetVariationalRefinementDelta()`
  * *Summary*: Weight of the color constancy term
  * *Remarks*:

**See also**: setVariationalRefinementDelta

  * *Returns*: The returned value.
* `void SetVariationalRefinementDelta(float val)`
  * *Summary*: Sets the weight of the color constancy term. **See also:** GetVariationalRefinementDelta
  * *Parameter* `val`: The color constancy term weight.
* `float GetVariationalRefinementGamma()`
  * *Summary*: Weight of the gradient constancy term
  * *Remarks*:

**See also**: setVariationalRefinementGamma

  * *Returns*: The returned value.
* `void SetVariationalRefinementGamma(float val)`
  * *Summary*: Sets the weight of the gradient constancy term. **See also:** GetVariationalRefinementGamma
  * *Parameter* `val`: The gradient constancy term weight.
* `float GetVariationalRefinementEpsilon()`
  * *Summary*: Norm value shift for robust penalizer
  * *Remarks*:

**See also**: setVariationalRefinementEpsilon

  * *Returns*: The returned value.
* `void SetVariationalRefinementEpsilon(float val)`
  * *Summary*: Sets the norm value shift for robust penalizer. **See also:** GetVariationalRefinementEpsilon
  * *Parameter* `val`: The epsilon value for the robust penalizer.
* `bool GetUseMeanNormalization()`
  * *Summary*: Whether to use mean-normalization of patches when computing patch distance. It is turned on by default as it typically provides a noticeable quality boost because of increased robustness to illumination variations. Turn it off if you are certain that your sequence doesn't contain any changes in illumination.
  * *Remarks*:

**See also**: setUseMeanNormalization

  * *Returns*: The returned value.
* `void SetUseMeanNormalization(bool val)`
  * *Summary*: Sets whether to use mean-normalization of patches when computing patch distance. **See also:** GetUseMeanNormalization
  * *Parameter* `val`: True to enable mean-normalization.
* `bool GetUseSpatialPropagation()`
  * *Summary*: Whether to use spatial propagation of good optical flow vectors. This option is turned on by default, as it tends to work better on average and can sometimes help recover from major errors introduced by the coarse-to-fine scheme employed by the DIS optical flow algorithm. Turning this option off can make the output flow field a bit smoother, however.
  * *Remarks*:

**See also**: setUseSpatialPropagation

  * *Returns*: The returned value.
* `void SetUseSpatialPropagation(bool val)`
  * *Summary*: Sets whether to use spatial propagation of good optical flow vectors. **See also:** GetUseSpatialPropagation
  * *Parameter* `val`: True to enable spatial propagation.
* `DISOpticalFlow? Create(int preset)`
  * *Summary*: Creates an instance of DISOpticalFlow
  * *Parameter* `preset`: one of PRESET_ULTRAFAST, PRESET_FAST and PRESET_MEDIUM
  * *Returns*: The returned value.

---
### `DenseOpticalFlow`
**Inherits from**: `Algorithm`

Base class for dense optical flow algorithms

#### Methods
* `void Calc(Mat I0, Mat I1, Mat flow)`
  * *Summary*: Calculates an optical flow.
  * *Parameter* `I0`: first 8-bit single-channel input image.
  * *Parameter* `I1`: second input image of the same size and the same type as prev.
  * *Parameter* `flow`: computed flow image that has the same size as prev and type CV_32FC2.
* `void CollectGarbage()`
  * *Summary*: Releases all inner buffers.

---
### `ECCParameters`
**Inherits from**: `DisposableOpenCVObject`

struct ECCParameters is used by findTransformECCMultiScale

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`MotionType`** | `int` | Gets or sets the motionType property. |
| **`Criteria`** | `TermCriteria` | Gets or sets the criteria property. |
| **`ItersPerLevel`** | `int[]` | Gets or sets the itersPerLevel property. |
| **`GaussFiltSize`** | `int` | Gets or sets the gaussFiltSize property. |
| **`Nlevels`** | `int` | Gets or sets the nlevels property. |
| **`Interpolation`** | `int` | Gets or sets the interpolation property. |

#### Constructors
* `new ECCParameters()`
  * *Summary*: Creates a new ECCParameters instance with default values.

---
### `FarnebackOpticalFlow`
**Inherits from**: `DenseOpticalFlow`

Class computing a dense optical flow using the Gunnar Farneback's algorithm.

#### Methods
* `int GetNumLevels()`
  * *Summary*: Returns the number of pyramid layers including the initial image
  * *Returns*: The returned value.
* `void SetNumLevels(int numLevels)`
  * *Summary*: Sets the number of pyramid layers including the initial image
  * *Parameter* `numLevels`: The number of pyramid levels.
* `double GetPyrScale()`
  * *Summary*: Returns the image scale (<1) used to build pyramids for each image
  * *Returns*: The returned value.
* `void SetPyrScale(double pyrScale)`
  * *Summary*: Sets the image scale (<1) used to build pyramids for each image; 0.5 means a classical pyramid
  * *Parameter* `pyrScale`: The pyramid scale factor.
* `bool GetFastPyramids()`
  * *Summary*: Returns whether fast pyramids are enabled
  * *Returns*: The returned value.
* `void SetFastPyramids(bool fastPyramids)`
  * *Summary*: Enables or disables fast pyramids
  * *Parameter* `fastPyramids`: True to enable fast pyramids.
* `int GetWinSize()`
  * *Summary*: Returns the averaging window size
  * *Returns*: The returned value.
* `void SetWinSize(int winSize)`
  * *Summary*: Sets the averaging window size; larger values increase robustness to noise but yield more blurred motion field
  * *Parameter* `winSize`: The averaging window size.
* `int GetNumIters()`
  * *Summary*: Returns the number of iterations the algorithm does at each pyramid level
  * *Returns*: The returned value.
* `void SetNumIters(int numIters)`
  * *Summary*: Sets the number of iterations the algorithm does at each pyramid level
  * *Parameter* `numIters`: The number of iterations.
* `int GetPolyN()`
  * *Summary*: Returns the size of the pixel neighborhood used to find polynomial expansion; typically 5 or 7
  * *Returns*: The returned value.
* `void SetPolyN(int polyN)`
  * *Summary*: Sets the size of the pixel neighborhood used to find polynomial expansion in each pixel
  * *Parameter* `polyN`: The polynomial expansion neighborhood size (typically 5 or 7).
* `double GetPolySigma()`
  * *Summary*: Returns the standard deviation of the Gaussian used to smooth derivatives for the polynomial expansion
  * *Returns*: The returned value.
* `void SetPolySigma(double polySigma)`
  * *Summary*: Sets the standard deviation of the Gaussian used to smooth derivatives for the polynomial expansion; for polyN=5 use 1.1, for polyN=7 use 1.5
  * *Parameter* `polySigma`: The Gaussian standard deviation.
* `int GetFlags()`
  * *Summary*: Returns the operation flags (e.g., OPTFLOW_USE_INITIAL_FLOW, OPTFLOW_FARNEBACK_GAUSSIAN)
  * *Returns*: The returned value.
* `void SetFlags(int flags)`
  * *Summary*: Sets the operation flags (e.g., OPTFLOW_USE_INITIAL_FLOW, OPTFLOW_FARNEBACK_GAUSSIAN)
  * *Parameter* `flags`: Operation flags.
* `FarnebackOpticalFlow? Create(int numLevels, double pyrScale, bool fastPyramids, int winSize, int numIters, int polyN, double polySigma, int flags)`
  * *Summary*: Creates an instance of FarnebackOpticalFlow with the specified parameters
  * *Parameter* `numLevels`: Number of pyramid layers including the initial image.
  * *Parameter* `pyrScale`: Image scale (<1) to build pyramids; 0.5 means a classical pyramid.
  * *Parameter* `fastPyramids`: Whether to use fast pyramids.
  * *Parameter* `winSize`: Averaging window size.
  * *Parameter* `numIters`: Number of iterations the algorithm does at each pyramid level.
  * *Parameter* `polyN`: Size of the pixel neighborhood used for polynomial expansion (typically 5 or 7).
  * *Parameter* `polySigma`: Standard deviation of the Gaussian used to smooth derivatives for the polynomial expansion.
  * *Parameter* `flags`: Operation flags (e.g., OPTFLOW_USE_INITIAL_FLOW, OPTFLOW_FARNEBACK_GAUSSIAN).
  * *Returns*: The returned value.

---
### `KalmanFilter`
**Inherits from**: `DisposableOpenCVObject`

Kalman filter class.

**Detailed Remarks**:
The class implements a standard Kalman filter <http://en.wikipedia.org/wiki/Kalman_filter>,
**Citation**:  Welch95 . However, you can modify transitionMatrix, controlMatrix, and measurementMatrix to get
an extended Kalman filter functionality.
.: info Note
In C API when CvKalman\* kalmanFilter structure is not needed anymore, it should be released
with cvReleaseKalman(&kalmanFilter)
.:

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`StatePre`** | `Mat?` | Gets or sets the statePre property. |
| **`StatePost`** | `Mat?` | Gets or sets the statePost property. |
| **`TransitionMatrix`** | `Mat?` | Gets or sets the transitionMatrix property. |
| **`ControlMatrix`** | `Mat?` | Gets or sets the controlMatrix property. |
| **`MeasurementMatrix`** | `Mat?` | Gets or sets the measurementMatrix property. |
| **`ProcessNoiseCov`** | `Mat?` | Gets or sets the processNoiseCov property. |
| **`MeasurementNoiseCov`** | `Mat?` | Gets or sets the measurementNoiseCov property. |
| **`ErrorCovPre`** | `Mat?` | Gets or sets the errorCovPre property. |
| **`Gain`** | `Mat?` | Gets or sets the gain property. |
| **`ErrorCovPost`** | `Mat?` | Gets or sets the errorCovPost property. |

#### Constructors
* `new KalmanFilter()`
  * *Summary*: Creates a default KalmanFilter instance.
* `new KalmanFilter(int dynamParams, int measureParams, int controlParams, int type)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `dynamParams`: Dimensionality of the state.
  * *Parameter* `measureParams`: Dimensionality of the measurement.
  * *Parameter* `controlParams`: Dimensionality of the control vector.
  * *Parameter* `type`: Type of the created matrices that should be CV_32F or CV_64F.

#### Methods
* `Mat? Predict(Mat? control)`
  * *Summary*: Computes a predicted state.
  * *Parameter* `control`: The optional input control
  * *Returns*: The returned value.
* `Mat? Correct(Mat measurement)`
  * *Summary*: Updates the predicted state from the measurement.
  * *Parameter* `measurement`: The measured system parameters
  * *Returns*: The returned value.

---
### `SparseOpticalFlow`
**Inherits from**: `Algorithm`

Base interface for sparse optical flow algorithms.

#### Methods
* `void Calc(Mat prevImg, Mat nextImg, Mat prevPts, Mat nextPts, Mat status, Mat? err)`
  * *Summary*: Calculates a sparse optical flow.
  * *Parameter* `prevImg`: First input image.
  * *Parameter* `nextImg`: Second input image of the same size and the same type as prevImg.
  * *Parameter* `prevPts`: Vector of 2D points for which the flow needs to be found.
  * *Parameter* `nextPts`: Output vector of 2D points containing the calculated new positions of input features in the second image.
  * *Parameter* `status`: Output status vector. Each element of the vector is set to 1 if the flow for the corresponding features has been found. Otherwise, it is set to 0.
  * *Parameter* `err`: Optional output vector that contains error response for each point (inverse confidence).

---
### `SparsePyrLKOpticalFlow`
**Inherits from**: `SparseOpticalFlow`

Class used for calculating a sparse optical flow.

**Detailed Remarks**:
The class can calculate an optical flow for a sparse feature set using the
iterative Lucas-Kanade method with pyramids.
**See also**: calcOpticalFlowPyrLK

#### Methods
* `Size GetWinSize()`
  * *Summary*: Returns the search window size at each pyramid level
  * *Returns*: The returned value.
* `void SetWinSize(Size winSize)`
  * *Summary*: Sets the search window size at each pyramid level
  * *Parameter* `winSize`: The search window size.
* `int GetMaxLevel()`
  * *Summary*: Returns the 0-based maximal pyramid level number
  * *Returns*: The returned value.
* `void SetMaxLevel(int maxLevel)`
  * *Summary*: Sets the 0-based maximal pyramid level number
  * *Parameter* `maxLevel`: The maximal pyramid level number.
* `TermCriteria GetTermCriteria()`
  * *Summary*: Returns the termination criteria of the iterative search algorithm
  * *Returns*: The returned value.
* `void SetTermCriteria(TermCriteria crit)`
  * *Summary*: Sets the termination criteria of the iterative search algorithm
  * *Parameter* `crit`: The termination criteria.
* `int GetFlags()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetFlags(int flags)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `flags`: Operation flags.
* `double GetMinEigThreshold()`
  * *Summary*: Returns the minimum eigen value threshold; features with smaller values are filtered out
  * *Returns*: The returned value.
* `void SetMinEigThreshold(double minEigThreshold)`
  * *Summary*: Sets the minimum eigen value threshold; features with smaller values are filtered out
  * *Parameter* `minEigThreshold`: The minimum eigen value threshold.
* `SparsePyrLKOpticalFlow? Create(Size winSize, int maxLevel, TermCriteria crit, int flags, double minEigThreshold)`
  * *Summary*: Creates an instance of SparsePyrLKOpticalFlow with the specified parameters
  * *Parameter* `winSize`: Search window size at each pyramid level.
  * *Parameter* `maxLevel`: 0-based maximal pyramid level number.
  * *Parameter* `crit`: Termination criteria of the iterative search algorithm.
  * *Parameter* `flags`: Operation flags (e.g., OPTFLOW_USE_INITIAL_FLOW, OPTFLOW_LK_GET_MIN_EIGENVALS).
  * *Parameter* `minEigThreshold`: Minimum eigen value threshold for filtering out features.
  * *Returns*: The returned value.

---
### `Tracker`
**Inherits from**: `DisposableOpenCVObject`

Base abstract class for the long-term tracker

#### Methods
* `void Init(Mat image, Rect boundingBox)`
  * *Summary*: Initialize the tracker with a known bounding box that surrounded the target
  * *Parameter* `image`: The initial frame
  * *Parameter* `boundingBox`: The initial bounding box
* `bool Update(Mat image, Rect boundingBox)`
  * *Summary*: Update the tracker, find the new most likely bounding box for the target
  * *Parameter* `image`: The current frame
  * *Parameter* `boundingBox`: The bounding box that represent the new target location, if true was returned, not modified otherwise
  * *Returns*: True means that target was located and false means that tracker cannot locate target in current frame. Note, that latter *does not* imply that tracker has failed, maybe target is indeed missing from the frame (say, out of sight)
* `float GetTrackingScore()`
  * *Summary*: Return tracking score
  * *Returns*: The returned value.

---
### `TrackerDaSiamRPN`
**Inherits from**: `Tracker`

DaSiamRPN (Distractor-aware Siamese Region Proposal Network) tracker based on deep learning.

#### Methods
* `TrackerDaSiamRPN? Create(IntPtr parameters)`
  * *Summary*: Constructor
  * *Parameter* `parameters`: DaSiamRPN parameters TrackerDaSiamRPN.Params
  * *Returns*: The returned value.

---
### `TrackerDaSiamRPNParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Model`** | `string?` | Gets or sets the model property. |
| **`KernelCls1`** | `string?` | Gets or sets the kernel_cls1 property. |
| **`KernelR1`** | `string?` | Gets or sets the kernel_r1 property. |
| **`Backend`** | `int` | Gets or sets the backend property. |
| **`Target`** | `int` | Gets or sets the target property. |

#### Constructors
* `new TrackerDaSiamRPNParams()`
  * *Summary*: Creates a new TrackerDaSiamRPNParams instance with default values.

---
### `TrackerMIL`
**Inherits from**: `Tracker`

The MIL algorithm trains a classifier in an online manner to separate the object from the background.

**Detailed Remarks**:
Multiple Instance Learning avoids the drift problem for a robust tracking. The implementation is
based on **Citation**:  MIL .
Original code can be found here <http://vision.ucsd.edu/~bbabenko/project_miltrack.shtml>

#### Methods
* `TrackerMIL? Create(IntPtr parameters)`
  * *Summary*: Create MIL tracker instance *  **parameters** MIL parameters TrackerMIL.Params
  * *Parameter* `parameters`: The parameters parameter.
  * *Returns*: The returned value.

---
### `TrackerMILParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`SamplerInitInRadius`** | `float` | Gets or sets the samplerInitInRadius property. |
| **`SamplerInitMaxNegNum`** | `int` | Gets or sets the samplerInitMaxNegNum property. |
| **`SamplerSearchWinSize`** | `float` | Gets or sets the samplerSearchWinSize property. |
| **`SamplerTrackInRadius`** | `float` | Gets or sets the samplerTrackInRadius property. |
| **`SamplerTrackMaxPosNum`** | `int` | Gets or sets the samplerTrackMaxPosNum property. |
| **`SamplerTrackMaxNegNum`** | `int` | Gets or sets the samplerTrackMaxNegNum property. |
| **`FeatureSetNumFeatures`** | `int` | Gets or sets the featureSetNumFeatures property. |

#### Constructors
* `new TrackerMILParams()`
  * *Summary*: Creates a new TrackerMILParams instance with default values.

---
### `TrackerNano`
**Inherits from**: `Tracker`

the Nano tracker is a super lightweight dnn-based general object tracking. * *  Nano tracker is much faster and extremely lightweight due to special model structure, the whole model size is about 1.9 MB. *  Nano tracker needs two models: one for feature extraction (backbone) and the another for localization (neckhead). *  Model download link: https://github.com/HonglinChu/SiamTrackers/tree/master/NanoTrack/models/nanotrackv2 *  Original repo is here: https://github.com/HonglinChu/NanoTrack *  Author: HongLinChu, 1628464345@qq.com

#### Methods
* `TrackerNano? Create(IntPtr parameters)`
  * *Summary*: Constructor
  * *Parameter* `parameters`: NanoTrack parameters TrackerNano.Params
  * *Returns*: The returned value.

---
### `TrackerNanoParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Backbone`** | `string?` | Gets or sets the backbone property. |
| **`Neckhead`** | `string?` | Gets or sets the neckhead property. |
| **`Backend`** | `int` | Gets or sets the backend property. |
| **`Target`** | `int` | Gets or sets the target property. |

#### Constructors
* `new TrackerNanoParams()`
  * *Summary*: Creates a new TrackerNanoParams instance with default values.

---
### `TrackerVit`
**Inherits from**: `Tracker`

the VIT tracker is a super lightweight dnn-based general object tracking. * *  VIT tracker is much faster and extremely lightweight due to special model structure, the model file is about 767KB. *  Model download link: https://github.com/opencv/opencv_zoo/tree/main/models/object_tracking_vittrack *  Author: PengyuLiu, 1872918507@qq.com

#### Methods
* `TrackerVit? Create(IntPtr parameters)`
  * *Summary*: Constructor
  * *Parameter* `parameters`: vit tracker parameters TrackerVit.Params
  * *Returns*: The returned value.

---
### `TrackerVitParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Net`** | `string?` | Gets or sets the net property. |
| **`Backend`** | `int` | Gets or sets the backend property. |
| **`Target`** | `int` | Gets or sets the target property. |
| **`Meanvalue`** | `Scalar` | Gets or sets the meanvalue property. |
| **`Stdvalue`** | `Scalar` | Gets or sets the stdvalue property. |
| **`TrackingScoreThreshold`** | `float` | Gets or sets the tracking_score_threshold property. |

#### Constructors
* `new TrackerVitParams()`
  * *Summary*: Creates a new TrackerVitParams instance with default values.

---
### `VariationalRefinement`
**Inherits from**: `DenseOpticalFlow`

Variational optical flow refinement

**Detailed Remarks**:
This class implements variational refinement of the input flow field, i.e.
it uses input flow to initialize the minimization of the following functional:
formula,
where formula are color constancy, gradient constancy and smoothness terms
respectively. formula is a robust penalizer to limit the
influence of outliers. A complete formulation and a description of the minimization
procedure can be found in **Citation**:  Brox2004

#### Methods
* `void CalcUV(Mat I0, Mat I1, Mat flow_u, Mat flow_v)`
  * *Summary*: `calc` function overload to handle separate horizontal (u) and vertical (v) flow components (to avoid extra splits/merges)
  * *Parameter* `I0`: First input image.
  * *Parameter* `I1`: Second input image of the same size and the same type as I0.
  * *Parameter* `flow_u`: Horizontal component of the optical flow.
  * *Parameter* `flow_v`: Vertical component of the optical flow.
* `int GetFixedPointIterations()`
  * *Summary*: Number of outer (fixed-point) iterations in the minimization procedure.
  * *Remarks*:

**See also**: setFixedPointIterations

  * *Returns*: The returned value.
* `void SetFixedPointIterations(int val)`
  * *Summary*: Sets the number of outer (fixed-point) iterations in the minimization procedure. **See also:** GetFixedPointIterations
  * *Parameter* `val`: The number of fixed-point iterations.
* `int GetSorIterations()`
  * *Summary*: Number of inner successive over-relaxation (SOR) iterations in the minimization procedure to solve the respective linear system.
  * *Remarks*:

**See also**: setSorIterations

  * *Returns*: The returned value.
* `void SetSorIterations(int val)`
  * *Summary*: Sets the number of inner SOR iterations in the minimization procedure. **See also:** GetSorIterations
  * *Parameter* `val`: The number of SOR iterations.
* `float GetOmega()`
  * *Summary*: Relaxation factor in SOR
  * *Remarks*:

**See also**: setOmega

  * *Returns*: The returned value.
* `void SetOmega(float val)`
  * *Summary*: Sets the relaxation factor in SOR. **See also:** GetOmega
  * *Parameter* `val`: The SOR relaxation factor.
* `float GetAlpha()`
  * *Summary*: Weight of the smoothness term
  * *Remarks*:

**See also**: setAlpha

  * *Returns*: The returned value.
* `void SetAlpha(float val)`
  * *Summary*: Sets the weight of the smoothness term. **See also:** GetAlpha
  * *Parameter* `val`: The smoothness term weight.
* `float GetDelta()`
  * *Summary*: Weight of the color constancy term
  * *Remarks*:

**See also**: setDelta

  * *Returns*: The returned value.
* `void SetDelta(float val)`
  * *Summary*: Sets the weight of the color constancy term. **See also:** GetDelta
  * *Parameter* `val`: The color constancy term weight.
* `float GetGamma()`
  * *Summary*: Weight of the gradient constancy term
  * *Remarks*:

**See also**: setGamma

  * *Returns*: The returned value.
* `void SetGamma(float val)`
  * *Summary*: Sets the weight of the gradient constancy term. **See also:** GetGamma
  * *Parameter* `val`: The gradient constancy term weight.
* `float GetEpsilon()`
  * *Summary*: Norm value shift for robust penalizer
  * *Remarks*:

**See also**: setEpsilon

  * *Returns*: The returned value.
* `void SetEpsilon(float val)`
  * *Summary*: Sets the norm value shift for robust penalizer. **See also:** GetEpsilon
  * *Parameter* `val`: The epsilon value for the robust penalizer.
* `VariationalRefinement? Create()`
  * *Summary*: Creates an instance of VariationalRefinement
  * *Returns*: The returned value.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.CreateBackgroundSubtractorMOG2`
**Signature**: `BackgroundSubtractorMOG2? CreateBackgroundSubtractorMOG2(int history, double varThreshold, bool detectShadows)`

Creates MOG2 Background Subtractor

**Parameters**:
* `history`: Length of the history.
* `varThreshold`: Threshold on the squared Mahalanobis distance between the pixel and the model to decide whether a pixel is well described by the background model. This parameter does not affect the background update.
* `detectShadows`: If true, the algorithm will detect shadows and mark them. It decreases the speed a bit, so if you do not need this feature, set the parameter to false.

**Returns**: The returned value.

---
### `Cv2.CreateBackgroundSubtractorKNN`
**Signature**: `BackgroundSubtractorKNN? CreateBackgroundSubtractorKNN(int history, double dist2Threshold, bool detectShadows)`

Creates KNN Background Subtractor

**Parameters**:
* `history`: Length of the history.
* `dist2Threshold`: Threshold on the squared distance between the pixel and the sample to decide whether a pixel is close to that sample. This parameter does not affect the background update.
* `detectShadows`: If true, the algorithm will detect shadows and mark them. It decreases the speed a bit, so if you do not need this feature, set the parameter to false.

**Returns**: The returned value.

---
### `Cv2.CamShift`
**Signature**: `RotatedRect? CamShift(Mat probImage, Rect window, TermCriteria criteria)`

Finds an object center, size, and orientation.

**Parameters**:
* `probImage`: Back projection of the object histogram. See calcBackProject.
* `window`: Initial search window.
* `criteria`: Stop criteria for the underlying meanShift. returns (in old interfaces) Number of iterations CAMSHIFT took to converge The function implements the CAMSHIFT object tracking algorithm [Bradski98] . First, it finds an object center using meanShift and then adjusts the window size and finds the optimal rotation. The function returns the rotated rectangle structure that includes the object position, size, and orientation. The next position of the search window can be obtained with RotatedRect.boundingRect() See the OpenCV sample camshiftdemo.c that tracks colored objects.

**Returns**: The returned value.

---
### `Cv2.MeanShift`
**Signature**: `int MeanShift(Mat probImage, Rect window, TermCriteria criteria)`

Finds an object on a back projection image.

**Parameters**:
* `probImage`: Back projection of the object histogram. See calcBackProject for details.
* `window`: Initial search window.
* `criteria`: Stop criteria for the iterative search algorithm. returns :   Number of iterations CAMSHIFT took to converge. The function implements the iterative object search algorithm. It takes the input back projection of an object and the initial position. The mass center in window of the back projection image is computed and the search window center shifts to the mass center. The procedure is repeated until the specified number of iterations criteria.maxCount is done or until the window center shifts by less than criteria.epsilon. The algorithm is used inside CamShift and, unlike CamShift , the search window size or orientation do not change during the search. You can simply pass the output of calcBackProject to this function. But better results can be obtained if you pre-filter the back projection and remove the noise. For example, you can do this by retrieving connected components with findContours , throwing away contours with small area ( contourArea ), and rendering the remaining contours with drawContours.

**Returns**: The returned value.

---
### `Cv2.BuildOpticalFlowPyramid`
**Signature**: `int BuildOpticalFlowPyramid(Mat img, IntPtr pyramid, Size winSize, int maxLevel, bool withDerivatives, int pyrBorder, int derivBorder, bool tryReuseInputImage)`

Constructs the image pyramid which can be passed to calcOpticalFlowPyrLK.

**Parameters**:
* `img`: 8-bit input image.
* `pyramid`: output pyramid.
* `winSize`: window size of optical flow algorithm. Must be not less than winSize argument of calcOpticalFlowPyrLK. It is needed to calculate required padding for pyramid levels.
* `maxLevel`: 0-based maximal pyramid level number.
* `withDerivatives`: set to precompute gradients for the every pyramid level. If pyramid is constructed without the gradients then calcOpticalFlowPyrLK will calculate them internally.
* `pyrBorder`: the border mode for pyramid layers.
* `derivBorder`: the border mode for gradients.
* `tryReuseInputImage`: put ROI of input image into the pyramid if possible. You can pass false to force data copying.

**Returns**: number of levels in constructed pyramid. Can be less than maxLevel.

---
### `Cv2.CalcOpticalFlowPyrLK`
**Signature**: `void CalcOpticalFlowPyrLK(Mat prevImg, Mat nextImg, Mat prevPts, Mat nextPts, Mat status, Mat err, Size winSize, int maxLevel, TermCriteria criteria, int flags, double minEigThreshold)`

Calculates an optical flow for a sparse feature set using the iterative Lucas-Kanade method with pyramids.

**Detailed Remarks**:
.: info Note
Some examples:
-   An example using the Lucas-Kanade optical flow algorithm can be found at
.:

**Parameters**:
* `prevImg`: first 8-bit input image or pyramid constructed by buildOpticalFlowPyramid.
* `nextImg`: second input image or pyramid of the same size and the same type as prevImg.
* `prevPts`: vector of 2D points for which the flow needs to be found; point coordinates must be single-precision floating-point numbers.
* `nextPts`: output vector of 2D points (with single-precision floating-point coordinates) containing the calculated new positions of input features in the second image; when OPTFLOW_USE_INITIAL_FLOW flag is passed, the vector must have the same size as in the input.
* `status`: output status vector (of unsigned chars); each element of the vector is set to 1 if the flow for the corresponding features has been found, otherwise, it is set to 0.
* `err`: output vector of errors; each element of the vector is set to an error for the corresponding feature, type of the error measure can be set in flags parameter; if the flow wasn't found then the error is not defined (use the status parameter to find such cases).
* `winSize`: size of the search window at each pyramid level.
* `maxLevel`: 0-based maximal pyramid level number; if set to 0, pyramids are not used (single level), if set to 1, two levels are used, and so on; if pyramids are passed to input then algorithm will use as many levels as pyramids have but no more than maxLevel.
* `criteria`: parameter, specifying the termination criteria of the iterative search algorithm (after the specified maximum number of iterations criteria.maxCount or when the search window moves by less than criteria.epsilon.
* `flags`: operation flags: -   **OPTFLOW_USE_INITIAL_FLOW** uses initial estimations, stored in nextPts; if the flag is not set, then prevPts is copied to nextPts and is considered the initial estimate. -   **OPTFLOW_LK_GET_MIN_EIGENVALS** use minimum eigen values as an error measure (see minEigThreshold description); if the flag is not set, then L1 distance between patches around the original and a moved point, divided by number of pixels in a window, is used as a error measure.
* `minEigThreshold`: the algorithm calculates the minimum eigen value of a 2x2 normal matrix of optical flow equations (this matrix is called a spatial gradient matrix in [Bouguet00]), divided by number of pixels in a window; if this value is less than minEigThreshold, then a corresponding feature is filtered out and its flow is not processed, so it allows to remove bad points and get a performance boost. The function implements a sparse iterative version of the Lucas-Kanade optical flow in pyramids. See [Bouguet00] . The function is parallelized with the TBB library.

---
### `Cv2.CalcOpticalFlowFarneback`
**Signature**: `void CalcOpticalFlowFarneback(Mat prev, Mat next, Mat flow, double pyr_scale, int levels, int winsize, int iterations, int poly_n, double poly_sigma, int flags)`

Computes a dense optical flow using the Gunnar Farneback's algorithm.

**Detailed Remarks**:
.: info Note
Some examples:
-   An example using the optical flow algorithm described by Gunnar Farneback can be found at
.:

**Parameters**:
* `prev`: first 8-bit single-channel input image.
* `next`: second input image of the same size and the same type as prev.
* `flow`: computed flow image that has the same size as prev and type CV_32FC2.
* `pyr_scale`: parameter, specifying the image scale (\<1) to build pyramids for each image; pyr_scale=0.5 means a classical pyramid, where each next layer is twice smaller than the previous one.
* `levels`: number of pyramid layers including the initial image; levels=1 means that no extra layers are created and only the original images are used.
* `winsize`: averaging window size; larger values increase the algorithm robustness to image noise and give more chances for fast motion detection, but yield more blurred motion field.
* `iterations`: number of iterations the algorithm does at each pyramid level.
* `poly_n`: size of the pixel neighborhood used to find polynomial expansion in each pixel; larger values mean that the image will be approximated with smoother surfaces, yielding more robust algorithm and more blurred motion field, typically poly_n =5 or 7.
* `poly_sigma`: standard deviation of the Gaussian that is used to smooth derivatives used as a basis for the polynomial expansion; for poly_n=5, you can set poly_sigma=1.1, for poly_n=7, a good value would be poly_sigma=1.5.
* `flags`: operation flags that can be a combination of the following: -   **OPTFLOW_USE_INITIAL_FLOW** uses the input flow as an initial flow approximation. -   **OPTFLOW_FARNEBACK_GAUSSIAN** uses the Gaussian formula filter instead of a box filter of the same size for optical flow estimation; usually, this option gives z more accurate flow than with a box filter, at the cost of lower speed; normally, winsize for a Gaussian window should be set to a larger value to achieve the same level of robustness. The function finds an optical flow for each prev pixel using the [Farneback2003] algorithm so that [see mathematical formula in OpenCV docs]

---
### `Cv2.ComputeECC`
**Signature**: `double ComputeECC(Mat templateImage, Mat inputImage, Mat? inputMask)`

Computes the Enhanced Correlation Coefficient (ECC) value between two images

**Detailed Remarks**:
The Enhanced Correlation Coefficient (ECC) is a normalized measure of similarity between two images **Citation**:  EP08.
The result lies in the range [-1, 1], where 1 corresponds to perfect similarity (modulo affine shift and scale),
0 indicates no correlation, and -1 indicates perfect negative correlation.
For single-channel images, the ECC is defined as:

$$
\mathrm{ECC}(I, T) = \frac{\sum_{x} (I(x) - \mu_I)(T(x) - \mu_T)}
{\sqrt{\sum_{x} (I(x) - \mu_I)^2} \cdot \sqrt{\sum_{x} (T(x) - \mu_T)^2}}
$$

For multi-channel images (e.g., 3-channel RGB), the formula generalizes to:

$$
\mathrm{ECC}(I, T) =
\frac{\sum_{x} \sum_{c=1}^{C} (I_c(x) - \mu_{I_c})(T_c(x) - \mu_{T_c})}
{\sqrt{\sum_{x} \sum_{c=1}^{C} (I_c(x) - \mu_{I_c})^2} \cdot
\sqrt{\sum_{x} \sum_{c=1}^{C} (T_c(x) - \mu_{T_c})^2}}
$$

Where:
- formula are the values of channel formula at spatial location formula,
- formula are the mean values of channel formula over the masked region (if provided),
- formula is the number of channels (only 1 and 3 are currently supported),
- The sums run over all pixels formula in the image domain (optionally restricted by mask).
**See also**: findTransformECC

**Parameters**:
* `templateImage`: Input template image; must have either 1 or 3 channels and be of type CV_8U, CV_16U, CV_32F, or CV_64F.
* `inputImage`: Input image to be compared with the template; must have the same type and number of channels as templateImage.
* `inputMask`: Optional single-channel mask to specify the valid region of interest in inputImage and templateImage.

**Returns**: The ECC similarity coefficient in the range [-1, 1].

---
### `Cv2.FindTransformECC`
**Signature**: `double FindTransformECC(Mat templateImage, Mat inputImage, Mat warpMatrix, int motionType, TermCriteria criteria, Mat inputMask, int gaussFiltSize)`

Finds the geometric transform (warp) between two images in terms of the ECC criterion [EP08] .

**Detailed Remarks**:
**See also**: 
computeECC, estimateAffine2D, estimateAffinePartial2D, findHomography

**Parameters**:
* `templateImage`: 1 or 3 channel template image; CV_8U, CV_16U, CV_32F, CV_64F type.
* `inputImage`: input image which should be warped with the final warpMatrix in order to provide an image similar to templateImage, same type as templateImage.
* `warpMatrix`: floating-point formula or formula mapping matrix (warp).
* `motionType`: parameter, specifying the type of motion: -   **MOTION_TRANSLATION** sets a translational motion model; warpMatrix is formula with the first formula part being the unity matrix and the rest two parameters being estimated. -   **MOTION_EUCLIDEAN** sets a Euclidean (rigid) transformation as motion model; three parameters are estimated; warpMatrix is formula. -   **MOTION_AFFINE** sets an affine motion model (DEFAULT); six parameters are estimated; warpMatrix is formula. -   **MOTION_HOMOGRAPHY** sets a homography as a motion model; eight parameters are estimated;\`warpMatrix\` is formula.
* `criteria`: parameter, specifying the termination criteria of the ECC algorithm; criteria.epsilon defines the threshold of the increment in the correlation coefficient between two iterations (a negative criteria.epsilon makes criteria.maxcount the only termination criterion). Default values are shown in the declaration above.
* `inputMask`: An optional single channel mask to indicate valid values of inputImage.
* `gaussFiltSize`: An optional value indicating size of gaussian blur filter; (DEFAULT: 5) The function estimates the optimum transformation (warpMatrix) with respect to ECC criterion ([EP08]), that is [see mathematical formula in OpenCV docs] where [see mathematical formula in OpenCV docs] (the equation holds with homogeneous coordinates for homography). It returns the final enhanced correlation coefficient, that is the correlation coefficient between the template image and the final warped input image. When a formula matrix is given with motionType =0, 1 or 2, the third row is ignored. Unlike findHomography and estimateRigidTransform, the function findTransformECC implements an area-based alignment that builds on intensity similarities. In essence, the function updates the initial transformation that roughly aligns the images. If this information is missing, the identity warp (unity matrix) is used as an initialization. Note that if images undergo strong displacements/rotations, an initial transformation that roughly aligns the images is necessary (e.g., a simple euclidean/similarity transform that allows for the images showing the same image content approximately). Use inverse warping in the second image to take an image close to the first one, i.e. use the flag WARP_INVERSE_MAP with warpAffine or warpPerspective. See also the OpenCV sample image_alignment.cpp that demonstrates the use of the function. Note that the function throws an exception if algorithm does not converges.

**Returns**: The returned value.

---
### `Cv2.FindTransformECC`
**Signature**: `double FindTransformECC(Mat templateImage, Mat inputImage, Mat warpMatrix, int motionType, TermCriteria criteria, Mat? inputMask)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `templateImage`: The templateImage parameter.
* `inputImage`: The inputImage parameter.
* `warpMatrix`: The warpMatrix parameter.
* `motionType`: The motionType parameter.
* `criteria`: The criteria parameter.
* `inputMask`: The inputMask parameter.

**Returns**: The returned value.

---
### `Cv2.FindTransformECCWithMask`
**Signature**: `double FindTransformECCWithMask(Mat templateImage, Mat inputImage, Mat templateMask, Mat inputMask, Mat warpMatrix, int motionType, TermCriteria criteria, int gaussFiltSize)`

Finds the geometric transform (warp) between two images in terms of the ECC criterion [EP08] using validity masks for both the template and the input images.

**Detailed Remarks**:
This function extends findTransformECC() by adding a mask for the template image.
The Enhanced Correlation Coefficient is evaluated only over pixels that are valid in both images:
on each iteration inputMask is warped into the template frame and combined with templateMask, and
only the intersection of these masks contributes to the objective function.
**See also**: 
findTransformECC, computeECC, estimateAffine2D, estimateAffinePartial2D, findHomography

**Parameters**:
* `templateImage`: 1 or 3 channel template image; CV_8U, CV_16U, CV_32F, CV_64F type.
* `inputImage`: input image which should be warped with the final warpMatrix in order to provide an image similar to templateImage, same type as templateImage.
* `templateMask`: single-channel 8-bit mask for templateImage indicating valid pixels to be used in the alignment. Must have the same size as templateImage.
* `inputMask`: single-channel 8-bit mask for inputImage indicating valid pixels before warping. Must have the same size as inputImage.
* `warpMatrix`: floating-point formula or formula mapping matrix (warp).
* `motionType`: parameter, specifying the type of motion: -   **MOTION_TRANSLATION** sets a translational motion model; warpMatrix is formula with the first formula part being the unity matrix and the rest two parameters being estimated. -   **MOTION_EUCLIDEAN** sets a Euclidean (rigid) transformation as motion model; three parameters are estimated; warpMatrix is formula. -   **MOTION_AFFINE** sets an affine motion model (DEFAULT); six parameters are estimated; warpMatrix is formula. -   **MOTION_HOMOGRAPHY** sets a homography as a motion model; eight parameters are estimated; warpMatrix is formula.
* `criteria`: parameter, specifying the termination criteria of the ECC algorithm; criteria.epsilon defines the threshold of the increment in the correlation coefficient between two iterations (a negative criteria.epsilon makes criteria.maxcount the only termination criterion). Default values are shown in the declaration above.
* `gaussFiltSize`: size of the Gaussian blur filter used for smoothing images and masks before computing the alignment (DEFAULT: 5).

**Returns**: The returned value.

---
### `Cv2.FindTransformECCMultiScale`
**Signature**: `double FindTransformECCMultiScale(Mat reference, Mat sample, Mat warpMatrix, ECCParameters? eccParams, Mat? referenceMask, Mat? sampleMask)`

Finds the geometric transform (warp) between two images in terms of the ECC criterion [EP08]. Uses pyramids.

**Detailed Remarks**:
**See also**: 
computeECC, estimateAffine2D, estimateAffinePartial2D, findHomography

**Parameters**:
* `reference`: Single channel reference image; CV_8U, CV_16U, CV_32F, CV_64F type.
* `sample`: sample image which should be warped with the final warpMatrix in order to provide an image similar to reference, same type as reference.
* `warpMatrix`: floating-point formula or formula mapping matrix (warp).
* `eccParams`: List of the algorithm parameters. See ECCParameters for details.
* `referenceMask`: An optional single channel mask to indicate valid values of reference.
* `sampleMask`: An optional single channel mask to indicate valid values of sample. The function estimates the optimum transformation (warpMatrix) with respect to ECC criterion ([EP08]), that is [see mathematical formula in OpenCV docs] where [see mathematical formula in OpenCV docs] (the equation holds with homogeneous coordinates for homography). It returns the final enhanced correlation coefficient, that is the correlation coefficient between the template image and the final warped input image. When a formula matrix is given with motionType =0, 1 or 2, the third row is ignored. Unlike findHomography and estimateRigidTransform, the function findTransformECCMultiScale implements an area-based alignment that builds on intensity similarities. In essence, the function updates the initial transformation that roughly aligns the images. If this information is missing, the identity warp (unity matrix) is used as an initialization. Note that if images undergo strong displacements/rotations, an initial transformation that roughly aligns the images is necessary (e.g., a simple euclidean/similarity transform that allows for the images showing the same image content approximately). Use inverse warping in the second image to take an image close to the first one, i.e. use the flag WARP_INVERSE_MAP with warpAffine or warpPerspective. See also the OpenCV sample image_alignment.cpp that demonstrates the use of the function. Note that the function throws an exception if algorithm does not converges. Unlike findTransformECC, the findTransformECCMultiScale uses pyramids, making function more stable and able to handle correctly more sophisticated cases.

**Returns**: The returned value.

---
### `Cv2.ReadOpticalFlow`
**Signature**: `Mat? ReadOpticalFlow(string path)`

Read a .flo file

**Parameters**:
* `path`: Path to the file to be loaded The function readOpticalFlow loads a flow field from a file and returns it as a single matrix. Resulting Mat has a type CV_32FC2 - floating-point, 2-channel. First channel corresponds to the flow in the horizontal direction (u), second - vertical (v).

**Returns**: The returned value.

---
### `Cv2.WriteOpticalFlow`
**Signature**: `bool WriteOpticalFlow(string path, Mat flow)`

Write a .flo to disk

**Parameters**:
* `path`: Path to the file to be written
* `flow`: Flow field to be stored The function stores a flow field in a file, returns true on success, false otherwise. The flow field must be a 2-channel, floating-point matrix (CV_32FC2). First channel corresponds to the flow in the horizontal direction (u), second - vertical (v).

**Returns**: The returned value.

---
## 🔢 Enumerations

### `UnnamedEnum1DISOpticalFlow`
Specifies preset options for the DIS optical flow algorithm, controlling the trade-off between speed and quality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Ultrafast`** | `0` | Ultrafast |
| **`Fast`** | `1` | Fast |
| **`Medium`** | `2` | Medium |

---
### `DetailTrackerSamplerCSCMode`
Specifies the sampling mode for the tracker's current state classifier (CSC).

| Member | Value | Description |
| :--- | :--- | :--- |
| **`InitPos`** | `1` | InitPos |
| **`InitNeg`** | `2` | InitNeg |
| **`TrackPos`** | `3` | TrackPos |
| **`TrackNeg`** | `4` | TrackNeg |
| **`Detect`** | `5` | Detect |

---

</div>