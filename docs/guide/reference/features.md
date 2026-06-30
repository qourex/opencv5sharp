# FEATURES Module API Reference

Complete documentation for the **FEATURES** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Features Documentation](https://docs.opencv.org/5.x/main_modules/features.html).

---
<div v-pre>

## 📦 Classes and Structs

### `Aliked`
**Inherits from**: `Feature2D`

ALIKED feature detector and descriptor extractor.

**Detailed Remarks**:
ALIKED (A Lightweight Image KEYpoint Detector) is a CNN-based feature detector and descriptor
extractor, as described in **Citation**:  Zhao23 . It produces 128-dimensional float descriptors and
keypoints with sub-pixel accuracy.
The model expects RGB input [1,3,H,W] and internally converts BGR images to RGB.

#### Methods
* `Aliked? Create(string modelPath, IntPtr @params)`
  * *Summary*: Creates ALIKED from a model file path.
  * *Parameter* `modelPath`: Path to the ONNX model file.
  * *Parameter* `params`: ALIKED parameters.
  * *Returns*: The returned value.

---
### `AlikedParams`
**Inherits from**: `DisposableOpenCVObject`

Configuration parameters for the ALIKED feature detector and descriptor extractor.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`InputSize`** | `Size` | Gets or sets the inputSize property. |
| **`NormalizeDescriptors`** | `bool` | Gets or sets the normalizeDescriptors property. |
| **`Engine`** | `int` | Gets or sets the engine property. |
| **`Backend`** | `int` | Gets or sets the backend property. |
| **`Target`** | `int` | Gets or sets the target property. |

#### Constructors
* `new AlikedParams()`
  * *Summary*: Creates a new AlikedParams instance with default configuration values.

---
### `ANNIndex`
**Inherits from**: `DisposableOpenCVObject`

**************************************************************************************\

**Detailed Remarks**:
*                             Approximate Nearest Neighbors                              *
\***************************************************************************************

#### Methods
* `void AddItems(Mat features)`
  * *Summary*: Add feature vectors to index. * * **features** Matrix containing the feature vectors to index. The size of the matrix is num_features x feature_dimension.
  * *Parameter* `features`: The features parameter.
* `void Build(int trees)`
  * *Summary*: Build the index. * *  **trees** Number of trees in the index. If not provided, the number is determined automatically *  in a way that at most 2x as much memory as the features vectors take is used.
  * *Parameter* `trees`: The trees parameter.
* `void KnnSearch(Mat query, Mat indices, Mat dists, int knn, int search_k)`
  * *Summary*: Performs a K-nearest neighbor search for given query vector(s) using the index. * *  **query** The query vector(s). *  **indices** Matrix that will contain the indices of the K-nearest neighbors found, optional. *  **dists** Matrix that will contain the distances to the K-nearest neighbors found, optional. *  **knn** Number of nearest neighbors to search for. *  **search_k** The maximum number of nodes to inspect, which defaults to trees x knn if not provided.
  * *Parameter* `query`: The query parameter.
  * *Parameter* `indices`: The indices parameter.
  * *Parameter* `dists`: The dists parameter.
  * *Parameter* `knn`: The knn parameter.
  * *Parameter* `search_k`: The search_k parameter.
* `void Save(string filename, bool prefault)`
  * *Summary*: Save the index to disk and loads it. After saving, no more vectors can be added. * *  **filename** Filename of the index to be saved. *  **prefault** If prefault is set to true, it will pre-read the entire file into memory (using mmap *  with MAP_POPULATE). Default is false.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `prefault`: The prefault parameter.
* `void Load(string filename, bool prefault)`
  * *Summary*: Loads (mmaps) an index from disk. * *  **filename** Filename of the index to be loaded. *  **prefault** If prefault is set to true, it will pre-read the entire file into memory (using mmap *  with MAP_POPULATE). Default is false.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `prefault`: The prefault parameter.
* `int GetTreeNumber()`
  * *Summary*: Return the number of trees in the index.
  * *Returns*: The returned value.
* `int GetItemNumber()`
  * *Summary*: Return the number of feature vectors in the index.
  * *Returns*: The returned value.
* `bool SetOnDiskBuild(string filename)`
  * *Summary*: Prepare to build the index in the specified file instead of RAM (execute before adding * items, no need to save after build) * *  **filename** Filename of the index to be built.
  * *Parameter* `filename`: Path to the file.
  * *Returns*: The returned value.
* `void SetSeed(int seed)`
  * *Summary*: Initialize the random number generator with the given seed. Only necessary to pass this *  before adding the items. Will have no effect after calling build() or load(). * *  **seed** The given seed of the random number generator. Its value should be within the range of uint32_t.
  * *Parameter* `seed`: The seed parameter.
* `ANNIndex? Create(int dim, IntPtr distType)`
  * *Summary*: Creates an instance of annoy index class with given parameters * *  **dim** The dimension of the feature vector. *  **distType** Metric to calculate the distance between two feature vectors, can be DIST_EUCLIDEAN, DIST_MANHATTAN, DIST_ANGULAR, DIST_HAMMING, or DIST_DOTPRODUCT.
  * *Parameter* `dim`: The dim parameter.
  * *Parameter* `distType`: The distType parameter.
  * *Returns*: The returned value.

---
### `AffineFeature`
**Inherits from**: `Feature2D`

Class for implementing the wrapper which makes detectors and extractors to be affine invariant, described as ASIFT in [YM11] .

#### Methods
* `AffineFeature? Create(Feature2D backend, int maxTilt, int minTilt, float tiltStep, float rotateStepBase)`
  * *Summary*: Creates an AffineFeature instance wrapping the specified feature detector/extractor to make it affine invariant.
  * *Parameter* `backend`: The detector/extractor you want to use as backend.
  * *Parameter* `maxTilt`: The highest power index of tilt factor. 5 is used in the paper as tilt sampling range n.
  * *Parameter* `minTilt`: The lowest power index of tilt factor. 0 is used in the paper.
  * *Parameter* `tiltStep`: Tilt sampling step formula in Algorithm 1 in the paper.
  * *Parameter* `rotateStepBase`: Rotation sampling step factor b in Algorithm 1 in the paper.
  * *Returns*: The returned value.
* `void SetViewParams(IntPtr tilts, IntPtr rolls)`
  * *Summary*: Sets the affine simulation view parameters (tilt and rotation angles) for detection.
  * *Parameter* `tilts`: Vector of tilt factors to simulate.
  * *Parameter* `rolls`: Vector of rotation angles (in degrees) for each tilt level.
* `void GetViewParams(IntPtr tilts, IntPtr rolls)`
  * *Summary*: Retrieves the current affine simulation view parameters (tilt and rotation angles).
  * *Parameter* `tilts`: Output vector of tilt factors.
  * *Parameter* `rolls`: Output vector of rotation angles (in degrees) for each tilt level.
* `string? GetDefaultName()`
  * *Summary*: Returns the algorithm's default name identifier string.
  * *Returns*: The default name of the algorithm.

---
### `BFMatcher`
**Inherits from**: `DescriptorMatcher`

Brute-force descriptor matcher.

**Detailed Remarks**:
For each descriptor in the first set, this matcher finds the closest descriptor in the second set
by trying each one. This descriptor matcher supports masking permissible matches of descriptor
sets.

#### Constructors
* `new BFMatcher(int normType, bool crossCheck)`
  * *Summary*: Brute-force matcher constructor (obsolete). Please use BFMatcher.create() * *
  * *Parameter* `normType`: The normType parameter.
  * *Parameter* `crossCheck`: The crossCheck parameter.

#### Methods
* `BFMatcher? Create(int normType, bool crossCheck)`
  * *Summary*: Brute-force matcher create method.
  * *Parameter* `normType`: One of NORM_L1, NORM_L2, NORM_HAMMING, NORM_HAMMING2. L1 and L2 norms are preferable choices for SIFT and SURF descriptors, NORM_HAMMING should be used with ORB, BRISK and BRIEF, NORM_HAMMING2 should be used with ORB when WTA_K==3 or 4 (see ORB.ORB constructor description).
  * *Parameter* `crossCheck`: If it is false, this is will be default BFMatcher behaviour when it finds the k nearest neighbors for each query descriptor. If crossCheck==true, then the knnMatch() method with k=1 will only return pairs (i,j) such that for i-th query descriptor the j-th descriptor in the matcher's collection is the nearest and vice versa, i.e. the BFMatcher will only return consistent pairs. Such technique usually produces best results with minimal number of outliers when there are enough matches. This is alternative to the ratio test, used by D. Lowe in SIFT paper.
  * *Returns*: The returned value.

---
### `DescriptorMatcher`
**Inherits from**: `Algorithm`

Abstract base class for matching keypoint descriptors.

**Detailed Remarks**:
It has two groups of match methods: for matching descriptors of an image with another image or with
an image set.

#### Methods
* `void Add(IntPtr descriptors)`
  * *Summary*: Adds descriptors to train a CPU(trainDescCollectionis) or GPU(utrainDescCollectionis) descriptor collection.
  * *Remarks*:

If the collection is not empty, the new descriptors are added to existing train descriptors.

  * *Parameter* `descriptors`: Descriptors to add. Each descriptors[i] is a set of descriptors from the same train image.
* `IntPtr GetTrainDescriptors()`
  * *Summary*: Returns a constant link to the train descriptor collection trainDescCollection .
  * *Returns*: The returned value.
* `void Clear()`
  * *Summary*: Clears the train descriptor collections.
* `bool Empty()`
  * *Summary*: Returns true if there are no train descriptors in the both collections.
  * *Returns*: The returned value.
* `bool IsMaskSupported()`
  * *Summary*: Returns true if the descriptor matcher supports masking permissible matches.
  * *Returns*: The returned value.
* `void Train()`
  * *Summary*: Trains a descriptor matcher
  * *Remarks*:

Trains a descriptor matcher (for example, the flann index). In all methods to match, the method
train() is run every time before matching. Some descriptor matchers (for example, BruteForceMatcher)
have an empty implementation of this method. Other matchers really train their inner structures (for
example, FlannBasedMatcher trains Flann.Index ).

* `void Match(Mat queryDescriptors, Mat trainDescriptors, IntPtr matches, Mat? mask)`
  * *Summary*: Finds the best match for each descriptor from a query set.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `trainDescriptors`: Train set of descriptors. This set is not added to the train descriptors collection stored in the class object.
  * *Parameter* `matches`: Matches. If a query descriptor is masked out in mask , no match is added for this descriptor. So, matches size may be smaller than the query descriptors count.
  * *Parameter* `mask`: Mask specifying permissible matches between an input query and train matrices of descriptors. In the first variant of this method, the train descriptors are passed as an input argument. In the second variant of the method, train descriptors collection that was set by DescriptorMatcher.add is used. Optional mask (or masks) can be passed to specify which query and training descriptors can be matched. Namely, queryDescriptors[i] can be matched with trainDescriptors[j] only if `mask.At<byte>(i, j)` is non-zero.
* `void KnnMatch(Mat queryDescriptors, Mat trainDescriptors, IntPtr matches, int k, Mat? mask, bool compactResult)`
  * *Summary*: Finds the k best matches for each descriptor from a query set.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `trainDescriptors`: Train set of descriptors. This set is not added to the train descriptors collection stored in the class object.
  * *Parameter* `matches`: Matches. Each matches[i] is k or less matches for the same query descriptor.
  * *Parameter* `k`: Count of best matches found per each query descriptor or less if a query descriptor has less than k possible matches in total.
  * *Parameter* `mask`: Mask specifying permissible matches between an input query and train matrices of descriptors.
  * *Parameter* `compactResult`: Parameter used when the mask (or masks) is not empty. If compactResult is false, the matches vector has the same size as queryDescriptors rows. If compactResult is true, the matches vector does not contain matches for fully masked-out query descriptors. These extended variants of DescriptorMatcher.match methods find several best matches for each query descriptor. The matches are returned in the distance increasing order. See DescriptorMatcher.match for the details about query and train descriptors.
* `void RadiusMatch(Mat queryDescriptors, Mat trainDescriptors, IntPtr matches, float maxDistance, Mat? mask, bool compactResult)`
  * *Summary*: For each query descriptor, finds the training descriptors not farther than the specified distance.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `trainDescriptors`: Train set of descriptors. This set is not added to the train descriptors collection stored in the class object.
  * *Parameter* `matches`: Found matches.
  * *Parameter* `maxDistance`: Threshold for the distance between matched descriptors. Distance means here metric distance (e.g. Hamming distance), not the distance between coordinates (which is measured in Pixels)!
  * *Parameter* `mask`: Mask specifying permissible matches between an input query and train matrices of descriptors. For each query descriptor, the methods find such training descriptors that the distance between the query descriptor and the training descriptor is equal or smaller than maxDistance. Found matches are returned in the distance increasing order.
  * *Parameter* `compactResult`: Parameter used when the mask (or masks) is not empty. If compactResult is false, the matches vector has the same size as queryDescriptors rows. If compactResult is true, the matches vector does not contain matches for fully masked-out query descriptors.
* `void Match(Mat queryDescriptors, IntPtr matches, IntPtr masks)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `matches`: Matches. If a query descriptor is masked out in mask , no match is added for this descriptor. So, matches size may be smaller than the query descriptors count.
  * *Parameter* `masks`: Set of masks. Each masks[i] specifies permissible matches between the input query descriptors and stored train descriptors from the i-th image trainDescCollection[i].
* `void KnnMatch(Mat queryDescriptors, IntPtr matches, int k, IntPtr masks, bool compactResult)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `matches`: Matches. Each matches[i] is k or less matches for the same query descriptor.
  * *Parameter* `k`: Count of best matches found per each query descriptor or less if a query descriptor has less than k possible matches in total.
  * *Parameter* `masks`: Set of masks. Each masks[i] specifies permissible matches between the input query descriptors and stored train descriptors from the i-th image trainDescCollection[i].
  * *Parameter* `compactResult`: Parameter used when the mask (or masks) is not empty. If compactResult is false, the matches vector has the same size as queryDescriptors rows. If compactResult is true, the matches vector does not contain matches for fully masked-out query descriptors.
* `void RadiusMatch(Mat queryDescriptors, IntPtr matches, float maxDistance, IntPtr masks, bool compactResult)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `queryDescriptors`: Query set of descriptors.
  * *Parameter* `matches`: Found matches.
  * *Parameter* `maxDistance`: Threshold for the distance between matched descriptors. Distance means here metric distance (e.g. Hamming distance), not the distance between coordinates (which is measured in Pixels)!
  * *Parameter* `masks`: Set of masks. Each masks[i] specifies permissible matches between the input query descriptors and stored train descriptors from the i-th image trainDescCollection[i].
  * *Parameter* `compactResult`: Parameter used when the mask (or masks) is not empty. If compactResult is false, the matches vector has the same size as queryDescriptors rows. If compactResult is true, the matches vector does not contain matches for fully masked-out query descriptors.
* `void Write(string fileName)`
  * *Summary*: Serializes the descriptor matcher to a file.
  * *Parameter* `fileName`: Path to the output file.
* `void Read(string fileName)`
  * *Summary*: Deserializes the descriptor matcher from a file.
  * *Parameter* `fileName`: Path to the input file.
* `void Read(FileNode arg1)`
  * *Summary*: Deserializes the descriptor matcher from a FileNode.
  * *Parameter* `arg1`: The FileNode to read from.
* `DescriptorMatcher? Clone(bool emptyTrainData)`
  * *Summary*: Clones the matcher.
  * *Parameter* `emptyTrainData`: If emptyTrainData is false, the method creates a deep copy of the object, that is, copies both parameters and train data. If emptyTrainData is true, the method creates an object copy with the current parameters but with empty train data.
  * *Returns*: The returned value.
* `DescriptorMatcher? Create(string descriptorMatcherType)`
  * *Summary*: Creates a descriptor matcher of a given type with the default parameters (using default constructor).
  * *Parameter* `descriptorMatcherType`: Descriptor matcher type. Now the following matcher types are supported: -   `BruteForce` (it uses L2 ) -   `BruteForce-L1` -   `BruteForce-Hamming` -   `BruteForce-Hamming(2)` -   `FlannBased`
  * *Returns*: The returned value.
* `DescriptorMatcher? Create(IntPtr matcherType)`
  * *Summary*: Creates a descriptor matcher of a given type using the DescriptorMatcherMatcherType enum.
  * *Parameter* `matcherType`: The matcher type enum value (e.g., Flannbased, Bruteforce, BruteforceHamming).
  * *Returns*: The created descriptor matcher instance, or null on failure.
* `void Write(FileStorage fs, string name)`
  * *Summary*: Serializes the descriptor matcher to a FileStorage object under the given name.
  * *Parameter* `fs`: The FileStorage object to write to.
  * *Parameter* `name`: The name of the node in the FileStorage.

---
### `FastFeatureDetector`
**Inherits from**: `Feature2D`

Wrapping class for feature detection using the FAST method.

**Detailed Remarks**:
Check the corresponding tutorial for more details.

#### Methods
* `FastFeatureDetector? Create(int threshold, bool nonmaxSuppression, IntPtr type)`
  * *Summary*: Creates a FastFeatureDetector instance with the specified parameters.
  * *Parameter* `threshold`: Threshold on difference between intensity of the central pixel and pixels of a circle around this pixel.
  * *Parameter* `nonmaxSuppression`: If true, non-maximum suppression is applied to detected corners (keypoints).
  * *Parameter* `type`: One of the FastFeatureDetectorDetectorType neighborhood variants (_58, _712, _916).
  * *Returns*: The created FastFeatureDetector instance, or null on failure.
* `void SetThreshold(int threshold)`
  * *Summary*: Sets the intensity difference threshold for corner detection.
  * *Parameter* `threshold`: Threshold on difference between intensity of the central pixel and pixels of a circle around this pixel.
* `int GetThreshold()`
  * *Summary*: Returns the current intensity difference threshold.
  * *Returns*: The current threshold value.
* `void SetNonmaxSuppression(bool f)`
  * *Summary*: Enables or disables non-maximum suppression on detected corners.
  * *Parameter* `f`: True to enable non-maximum suppression, false to disable.
* `bool GetNonmaxSuppression()`
  * *Summary*: Returns whether non-maximum suppression is enabled.
  * *Returns*: True if non-maximum suppression is enabled.
* `void SetType(IntPtr type)`
  * *Summary*: Sets the FAST detector neighborhood type.
  * *Parameter* `type`: One of the FastFeatureDetectorDetectorType values (_58, _712, _916).
* `IntPtr GetType()`
  * *Summary*: Returns the current FAST detector neighborhood type.
  * *Returns*: The current FastFeatureDetectorDetectorType value.
* `string? GetDefaultName()`
  * *Summary*: Returns the algorithm's default name identifier string.
  * *Returns*: The default name of the algorithm.

---
### `Feature2D`
**Inherits from**: `Algorithm`

Abstract base class for 2D image feature detectors and descriptor extractors

#### Methods
* `void Detect(Mat image, IntPtr keypoints, Mat? mask)`
  * *Summary*: Detects keypoints in an image (first variant) or image set (second variant).
  * *Parameter* `image`: Image.
  * *Parameter* `keypoints`: The detected keypoints. In the second variant of the method keypoints[i] is a set of keypoints detected in images[i] .
  * *Parameter* `mask`: Mask specifying where to look for keypoints (optional). It must be a 8-bit integer matrix with non-zero values in the region of interest.
* `void Detect(IntPtr images, IntPtr keypoints, IntPtr masks)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `images`: Image set.
  * *Parameter* `keypoints`: The detected keypoints. In the second variant of the method keypoints[i] is a set of keypoints detected in images[i] .
  * *Parameter* `masks`: Masks for each input image specifying where to look for keypoints (optional). masks[i] is a mask for images[i].
* `void Compute(Mat image, IntPtr keypoints, Mat descriptors)`
  * *Summary*: Computes the descriptors for a set of keypoints detected in an image (first variant) or image set (second variant).
  * *Parameter* `image`: Image.
  * *Parameter* `keypoints`: Input collection of keypoints. Keypoints for which a descriptor cannot be computed are removed. Sometimes new keypoints can be added, for example: SIFT duplicates keypoint with several dominant orientations (for each orientation).
  * *Parameter* `descriptors`: Computed descriptors. In the second variant of the method descriptors[i] are descriptors computed for a keypoints[i]. Row j is the keypoints (or keypoints[i]) is the descriptor for keypoint j-th keypoint.
* `void Compute(IntPtr images, IntPtr keypoints, IntPtr descriptors)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `images`: Image set.
  * *Parameter* `keypoints`: Input collection of keypoints. Keypoints for which a descriptor cannot be computed are removed. Sometimes new keypoints can be added, for example: SIFT duplicates keypoint with several dominant orientations (for each orientation).
  * *Parameter* `descriptors`: Computed descriptors. In the second variant of the method descriptors[i] are descriptors computed for a keypoints[i]. Row j is the keypoints (or keypoints[i]) is the descriptor for keypoint j-th keypoint.
* `void DetectAndCompute(Mat image, Mat mask, IntPtr keypoints, Mat descriptors, bool useProvidedKeypoints)`
  * *Summary*: Detects keypoints and computes the descriptors
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `keypoints`: The keypoints parameter.
  * *Parameter* `descriptors`: The descriptors parameter.
  * *Parameter* `useProvidedKeypoints`: The useProvidedKeypoints parameter.
* `int DescriptorSize()`
  * *Summary*: Returns the descriptor size in bytes.
  * *Returns*: The descriptor size.
* `int DescriptorType()`
  * *Summary*: Returns the descriptor element type (e.g., CV_32F or CV_8U).
  * *Returns*: The descriptor type constant.
* `int DefaultNorm()`
  * *Summary*: Returns the default norm type used for matching this detector's descriptors (e.g., NORM_L2 or NORM_HAMMING).
  * *Returns*: The norm type constant.
* `void Write(string fileName)`
  * *Summary*: Serializes the feature detector/extractor parameters to a file.
  * *Parameter* `fileName`: Path to the output file.
* `void Read(string fileName)`
  * *Summary*: Deserializes the feature detector/extractor parameters from a file.
  * *Parameter* `fileName`: Path to the input file.
* `void Read(FileNode arg1)`
  * *Summary*: Deserializes the feature detector/extractor parameters from a FileNode.
  * *Parameter* `arg1`: The FileNode to read from.
* `bool Empty()`
  * *Summary*: Returns true if the feature detector/extractor is empty (has not been initialized).
  * *Returns*: True if empty.
* `string? GetDefaultName()`
  * *Summary*: Returns the algorithm's default name identifier string.
  * *Returns*: The default name of the algorithm.
* `void Write(FileStorage fs, string name)`
  * *Summary*: Serializes the feature detector/extractor parameters to a FileStorage object under the given name.
  * *Parameter* `fs`: The FileStorage object to write to.
  * *Parameter* `name`: The name of the node in the FileStorage.

---
### `GFTTDetector`
**Inherits from**: `Feature2D`

Wrapping class for feature detection using the goodFeaturesToTrack function. :

#### Methods
* `GFTTDetector? Create(int maxCorners, double qualityLevel, double minDistance, int blockSize, bool useHarrisDetector, double k)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxCorners`: The maxCorners parameter.
  * *Parameter* `qualityLevel`: The qualityLevel parameter.
  * *Parameter* `minDistance`: The minDistance parameter.
  * *Parameter* `blockSize`: The blockSize parameter.
  * *Parameter* `useHarrisDetector`: The useHarrisDetector parameter.
  * *Parameter* `k`: The k parameter.
  * *Returns*: The returned value.
* `GFTTDetector? Create(int maxCorners, double qualityLevel, double minDistance, int blockSize, int gradientSize, bool useHarrisDetector, double k)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxCorners`: The maxCorners parameter.
  * *Parameter* `qualityLevel`: The qualityLevel parameter.
  * *Parameter* `minDistance`: The minDistance parameter.
  * *Parameter* `blockSize`: The blockSize parameter.
  * *Parameter* `gradientSize`: The gradientSize parameter.
  * *Parameter* `useHarrisDetector`: The useHarrisDetector parameter.
  * *Parameter* `k`: The k parameter.
  * *Returns*: The returned value.
* `void SetMaxFeatures(int maxFeatures)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxFeatures`: The maxFeatures parameter.
* `int GetMaxFeatures()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetQualityLevel(double qlevel)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `qlevel`: The qlevel parameter.
* `double GetQualityLevel()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinDistance(double minDistance)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minDistance`: The minDistance parameter.
* `double GetMinDistance()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetBlockSize(int blockSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `blockSize`: The blockSize parameter.
* `int GetBlockSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetGradientSize(int gradientSize_)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `gradientSize_`: The gradientSize_ parameter.
* `int GetGradientSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetHarrisDetector(bool val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `bool GetHarrisDetector()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetK(double k)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `k`: The k parameter.
* `double GetK()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? GetDefaultName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `LightGlueMatcher`
**Inherits from**: `DescriptorMatcher`

LightGlue feature matcher.

**Detailed Remarks**:
LightGlue is a CNN-based feature matcher, as described in **Citation**:  Lindenberger23 . It takes
keypoint locations and descriptors from two images and directly predicts match pairs. Unlike
traditional matchers that compute descriptor distances, LightGlue uses attention mechanisms
to produce confidence scores for each potential match pair.
The matcher extends DescriptorMatcher and supports the standard match(), knnMatch(), and
radiusMatch() interfaces. Context (keypoints and image sizes) must be provided via
setPairInfo() before matching.

#### Methods
* `LightGlueMatcher? Create(string modelPath, float scoreThreshold, int backend, int target)`
  * *Summary*: Creates LightGlueMatcher from a model file path.
  * *Parameter* `modelPath`: Path to the ONNX model file.
  * *Parameter* `scoreThreshold`: Match confidence threshold.
  * *Parameter* `backend`: DNN backend
  * *Parameter* `target`: DNN target
  * *Returns*: The returned value.
* `void SetPairInfo(Mat queryKpts, Mat trainKpts, Size queryImageSize, Size trainImageSize)`
  * *Summary*: Sets the keypoint and image size context for the next match() call.
  * *Remarks*:

This provides the spatial context that LightGlue needs in addition to descriptors.
Must be called before match()/knnMatch()/radiusMatch() unless using automatic context
from in-process ALIKED instances.

  * *Parameter* `queryKpts`: Query image keypoints (Nx2 float matrix with x,y coordinates).
  * *Parameter* `trainKpts`: Train image keypoints (Nx2 float matrix with x,y coordinates).
  * *Parameter* `queryImageSize`: Size of the query image (width, height).
  * *Parameter* `trainImageSize`: Size of the train image (width, height).
* `void ClearPairInfo()`
  * *Summary*: Clears stored pair context information.

---
### `Mser`
**Inherits from**: `Feature2D`

Maximally stable extremal region extractor

**Detailed Remarks**:
The class encapsulates all the parameters of the MSER extraction algorithm (see [wiki
article](http://en.wikipedia.org/wiki/Maximally_stable_extremal_regions)).
- there are two different implementation of MSER: one for grey image, one for color image
- the grey image algorithm is taken from: **Citation**:  nister2008linear ;  the paper claims to be faster
than union-find method; it actually get 1.5~2m/s on my centrino L7200 1.2GHz laptop.
- the color image algorithm is taken from: **Citation**:  forssen2007maximally ; it should be much slower
than grey image method ( 3~4 times )

#### Methods
* `Mser? Create(int delta, int min_area, int max_area, double max_variation, double min_diversity, int max_evolution, double area_threshold, double min_margin, int edge_blur_size)`
  * *Summary*: Full constructor for MSER detector
  * *Parameter* `delta`: it compares formula
  * *Parameter* `min_area`: prune the area which smaller than minArea
  * *Parameter* `max_area`: prune the area which bigger than maxArea
  * *Parameter* `max_variation`: prune the area have similar size to its children
  * *Parameter* `min_diversity`: for color image, trace back to cut off mser with diversity less than min_diversity
  * *Parameter* `max_evolution`: for color image, the evolution steps
  * *Parameter* `area_threshold`: for color image, the area threshold to cause re-initialize
  * *Parameter* `min_margin`: for color image, ignore too small margin
  * *Parameter* `edge_blur_size`: for color image, the aperture size for edge blur
  * *Returns*: The returned value.
* `void DetectRegions(Mat image, IntPtr msers, IntPtr bboxes)`
  * *Summary*: Detect MSER regions
  * *Parameter* `image`: input image (8UC1, 8UC3 or 8UC4, must be greater or equal than 3x3)
  * *Parameter* `msers`: resulting list of point sets
  * *Parameter* `bboxes`: resulting bounding boxes
* `void SetDelta(int delta)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `delta`: The delta parameter.
* `int GetDelta()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinArea(int minArea)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minArea`: The minArea parameter.
* `int GetMinArea()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxArea(int maxArea)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxArea`: The maxArea parameter.
* `int GetMaxArea()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxVariation(double maxVariation)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxVariation`: The maxVariation parameter.
* `double GetMaxVariation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinDiversity(double minDiversity)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minDiversity`: The minDiversity parameter.
* `double GetMinDiversity()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMaxEvolution(int maxEvolution)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxEvolution`: The maxEvolution parameter.
* `int GetMaxEvolution()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetAreaThreshold(double areaThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `areaThreshold`: The areaThreshold parameter.
* `double GetAreaThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetMinMargin(double min_margin)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `min_margin`: The min_margin parameter.
* `double GetMinMargin()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetEdgeBlurSize(int edge_blur_size)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edge_blur_size`: The edge_blur_size parameter.
* `int GetEdgeBlurSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPass2Only(bool f)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `f`: The f parameter.
* `bool GetPass2Only()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? GetDefaultName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `Orb`
**Inherits from**: `Feature2D`

Class implementing the ORB (*oriented BRIEF*) keypoint detector and descriptor extractor

**Detailed Remarks**:
described in **Citation**:  RRKB11 . The algorithm uses FAST in pyramids to detect stable keypoints, selects
the strongest features using FAST or Harris response, finds their orientation using first-order
moments and computes the descriptors using BRIEF (where the coordinates of random point pairs (or
k-tuples) are rotated according to the measured orientation).

#### Methods
* `Orb? Create(int nfeatures, float scaleFactor, int nlevels, int edgeThreshold, int firstLevel, int WTA_K, IntPtr scoreType, int patchSize, int fastThreshold)`
  * *Summary*: The ORB constructor
  * *Parameter* `nfeatures`: The maximum number of features to retain.
  * *Parameter* `scaleFactor`: Pyramid decimation ratio, greater than 1. scaleFactor==2 means the classical pyramid, where each next level has 4x less pixels than the previous, but such a big scale factor will degrade feature matching scores dramatically. On the other hand, too close to 1 scale factor will mean that to cover certain scale range you will need more pyramid levels and so the speed will suffer.
  * *Parameter* `nlevels`: The number of pyramid levels. The smallest level will have linear size equal to input_image_linear_size/pow(scaleFactor, nlevels - firstLevel).
  * *Parameter* `edgeThreshold`: This is size of the border where the features are not detected. It should roughly match the patchSize parameter.
  * *Parameter* `firstLevel`: The level of pyramid to put source image to. Previous layers are filled with upscaled source image.
  * *Parameter* `WTA_K`: The number of points that produce each element of the oriented BRIEF descriptor. The default value 2 means the BRIEF where we take a random point pair and compare their brightnesses, so we get 0/1 response. Other possible values are 3 and 4. For example, 3 means that we take 3 random points (of course, those point coordinates are random, but they are generated from the pre-defined seed, so each element of BRIEF descriptor is computed deterministically from the pixel rectangle), find point of maximum brightness and output index of the winner (0, 1 or 2). Such output will occupy 2 bits, and therefore it will need a special variant of Hamming distance, denoted as NORM_HAMMING2 (2 bits per bin). When WTA_K=4, we take 4 random points to compute each bin (that will also occupy 2 bits with possible values 0, 1, 2 or 3).
  * *Parameter* `scoreType`: The default HARRIS_SCORE means that Harris algorithm is used to rank features (the score is written to KeyPoint.score and is used to retain best nfeatures features); FAST_SCORE is alternative value of the parameter that produces slightly less stable keypoints, but it is a little faster to compute.
  * *Parameter* `patchSize`: size of the patch used by the oriented BRIEF descriptor. Of course, on smaller pyramid layers the perceived image area covered by a feature will be larger.
  * *Parameter* `fastThreshold`: the fast threshold
  * *Returns*: The returned value.
* `void SetMaxFeatures(int maxFeatures)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxFeatures`: The maxFeatures parameter.
* `int GetMaxFeatures()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScaleFactor(double scaleFactor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `scaleFactor`: The scaleFactor parameter.
* `double GetScaleFactor()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNLevels(int nlevels)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nlevels`: The nlevels parameter.
* `int GetNLevels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetEdgeThreshold(int edgeThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edgeThreshold`: The edgeThreshold parameter.
* `int GetEdgeThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetFirstLevel(int firstLevel)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `firstLevel`: The firstLevel parameter.
* `int GetFirstLevel()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetWTAK(int wta_k)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `wta_k`: The wta_k parameter.
* `int GetWTAK()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScoreType(IntPtr scoreType)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `scoreType`: The scoreType parameter.
* `IntPtr GetScoreType()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetPatchSize(int patchSize)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `patchSize`: The patchSize parameter.
* `int GetPatchSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetFastThreshold(int fastThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `fastThreshold`: The fastThreshold parameter.
* `int GetFastThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? GetDefaultName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `Sift`
**Inherits from**: `Feature2D`

Class for extracting keypoints and computing descriptors using the Scale Invariant Feature Transform (SIFT) algorithm by D. Lowe [Lowe04] .

#### Methods
* `Sift? Create(int nfeatures, int nOctaveLayers, double contrastThreshold, double edgeThreshold, double sigma, bool enable_precise_upscale)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Remarks*:

.: info Note
The contrast threshold will be divided by nOctaveLayers when the filtering is applied. When
nOctaveLayers is set to default and if you want to use the value used in D. Lowe paper, 0.03, set
this argument to 0.09.
.:

  * *Parameter* `nfeatures`: The number of best features to retain. The features are ranked by their scores (measured in SIFT algorithm as the local contrast)
  * *Parameter* `nOctaveLayers`: The number of layers in each octave. 3 is the value used in D. Lowe paper. The number of octaves is computed automatically from the image resolution.
  * *Parameter* `contrastThreshold`: The contrast threshold used to filter out weak features in semi-uniform (low-contrast) regions. The larger the threshold, the less features are produced by the detector.
  * *Parameter* `edgeThreshold`: The threshold used to filter out edge-like features. Note that the its meaning is different from the contrastThreshold, i.e. the larger the edgeThreshold, the less features are filtered out (more features are retained).
  * *Parameter* `sigma`: The sigma of the Gaussian applied to the input image at the octave \#0. If your image is captured with a weak camera with soft lenses, you might want to reduce the number.
  * *Parameter* `enable_precise_upscale`: Whether to enable precise upscaling in the scale pyramid, which maps index formula to formula. This prevents localization bias. The option is disabled by default.
  * *Returns*: The returned value.
* `Sift? Create(int nfeatures, int nOctaveLayers, double contrastThreshold, double edgeThreshold, double sigma, int descriptorType, bool enable_precise_upscale)`
  * *Summary*: Create SIFT with specified descriptorType.
  * *Remarks*:

.: info Note
The contrast threshold will be divided by nOctaveLayers when the filtering is applied. When
nOctaveLayers is set to default and if you want to use the value used in D. Lowe paper, 0.03, set
this argument to 0.09.
.:

  * *Parameter* `nfeatures`: The number of best features to retain. The features are ranked by their scores (measured in SIFT algorithm as the local contrast)
  * *Parameter* `nOctaveLayers`: The number of layers in each octave. 3 is the value used in D. Lowe paper. The number of octaves is computed automatically from the image resolution.
  * *Parameter* `contrastThreshold`: The contrast threshold used to filter out weak features in semi-uniform (low-contrast) regions. The larger the threshold, the less features are produced by the detector.
  * *Parameter* `edgeThreshold`: The threshold used to filter out edge-like features. Note that the its meaning is different from the contrastThreshold, i.e. the larger the edgeThreshold, the less features are filtered out (more features are retained).
  * *Parameter* `sigma`: The sigma of the Gaussian applied to the input image at the octave \#0. If your image is captured with a weak camera with soft lenses, you might want to reduce the number.
  * *Parameter* `descriptorType`: The type of descriptors. Only CV_32F and CV_8U are supported.
  * *Parameter* `enable_precise_upscale`: Whether to enable precise upscaling in the scale pyramid, which maps index formula to formula. This prevents localization bias. The option is disabled by default.
  * *Returns*: The returned value.
* `string? GetDefaultName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNFeatures(int maxFeatures)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `maxFeatures`: The maxFeatures parameter.
* `int GetNFeatures()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNOctaveLayers(int nOctaveLayers)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `nOctaveLayers`: The nOctaveLayers parameter.
* `int GetNOctaveLayers()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetContrastThreshold(double contrastThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `contrastThreshold`: The contrastThreshold parameter.
* `double GetContrastThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetEdgeThreshold(double edgeThreshold)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edgeThreshold`: The edgeThreshold parameter.
* `double GetEdgeThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetSigma(double sigma)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `sigma`: The sigma parameter.
* `double GetSigma()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `SimpleBlobDetector`
**Inherits from**: `Feature2D`

Class for extracting blobs from an image. :

**Detailed Remarks**:
The class implements a simple algorithm for extracting blobs from an image:
1.  Convert the source image to binary images by applying thresholding with several thresholds from
minThreshold (inclusive) to maxThreshold (exclusive) with distance thresholdStep between
neighboring thresholds.
2.  Extract connected components from every binary image by findContours and calculate their
centers.
3.  Group centers from several binary images by their coordinates. Close centers form one group that
corresponds to one blob, which is controlled by the minDistBetweenBlobs parameter.
4.  From the groups, estimate final centers of blobs and their radiuses and return as locations and
sizes of keypoints.
This class performs several filtrations of returned blobs. You should set filterBy\* to true/false
to turn on/off corresponding filtration. Available filtrations:
-   **By color**. This filter compares the intensity of a binary image at the center of a blob to
blobColor. If they differ, the blob is filtered out. Use blobColor = 0 to extract dark blobs
and blobColor = 255 to extract light blobs.
-   **By area**. Extracted blobs have an area between minArea (inclusive) and maxArea (exclusive).
-   **By circularity**. Extracted blobs have circularity
(formula) between minCircularity (inclusive) and
maxCircularity (exclusive).
-   **By ratio of the minimum inertia to maximum inertia**. Extracted blobs have this ratio
between minInertiaRatio (inclusive) and maxInertiaRatio (exclusive).
-   **By convexity**. Extracted blobs have convexity (area / area of blob convex hull) between
minConvexity (inclusive) and maxConvexity (exclusive).
Default values of parameters are tuned to extract dark circular blobs.

#### Methods
* `SimpleBlobDetector? Create(IntPtr parameters)`
  * *Summary*: Flag to enable contour collection. If set to true, the detector will store the contours of the detected blobs in memory, which can be retrieved after the detect() call using getBlobContours().
  * *Remarks*:

.: info Note
Default value is false.
.:

  * *Parameter* `parameters`: The parameters parameter.
  * *Returns*: The returned value.
* `void SetParams(IntPtr @params)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `params`: The @params parameter.
* `IntPtr GetParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? GetDefaultName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr GetBlobContours()`
  * *Summary*: Returns the contours of the blobs detected during the last call to detect().
  * *Remarks*:

.: info Note
The `Params`.collectContours parameter must be set to true before calling
detect() for this method to return any data.
.:

  * *Returns*: The returned value.

---
### `SimpleBlobDetectorParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`ThresholdStep`** | `float` | Gets or sets the thresholdStep property. |
| **`MinThreshold`** | `float` | Gets or sets the minThreshold property. |
| **`MaxThreshold`** | `float` | Gets or sets the maxThreshold property. |
| **`MinRepeatability`** | `long` | Gets or sets the minRepeatability property. |
| **`MinDistBetweenBlobs`** | `float` | Gets or sets the minDistBetweenBlobs property. |
| **`FilterByColor`** | `bool` | Gets or sets the filterByColor property. |
| **`BlobColor`** | `byte` | Gets or sets the blobColor property. |
| **`FilterByArea`** | `bool` | Gets or sets the filterByArea property. |
| **`MinArea`** | `float` | Gets or sets the minArea property. |
| **`MaxArea`** | `float` | Gets or sets the maxArea property. |
| **`FilterByCircularity`** | `bool` | Gets or sets the filterByCircularity property. |
| **`MinCircularity`** | `float` | Gets or sets the minCircularity property. |
| **`MaxCircularity`** | `float` | Gets or sets the maxCircularity property. |
| **`FilterByInertia`** | `bool` | Gets or sets the filterByInertia property. |
| **`MinInertiaRatio`** | `float` | Gets or sets the minInertiaRatio property. |
| **`MaxInertiaRatio`** | `float` | Gets or sets the maxInertiaRatio property. |
| **`FilterByConvexity`** | `bool` | Gets or sets the filterByConvexity property. |
| **`MinConvexity`** | `float` | Gets or sets the minConvexity property. |
| **`MaxConvexity`** | `float` | Gets or sets the maxConvexity property. |
| **`CollectContours`** | `bool` | Gets or sets the collectContours property. |

#### Constructors
* `new SimpleBlobDetectorParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.GoodFeaturesToTrack`
**Signature**: `void GoodFeaturesToTrack(Mat image, Mat corners, int maxCorners, double qualityLevel, double minDistance, Mat? mask, int blockSize, bool useHarrisDetector, double k)`

Determines strong corners on an image.

**Detailed Remarks**:
The function finds the most prominent corners in the image or in the specified image region, as
described in **Citation**:  Shi94
-   Function calculates the corner quality measure at every source image pixel using the
`cornerMinEigenVal` or `cornerHarris` .
-   Function performs a non-maximum suppression (the local maximums in *3 x 3* neighborhood are
retained).
-   The corners with the minimal eigenvalue less than
formula are rejected.
-   The remaining corners are sorted by the quality measure in the descending order.
-   Function throws away each corner for which there is a stronger corner at a distance less than
maxDistance.
The function can be used to initialize a point-based tracker of an object.
.: info Note
If the function is called with different values A and B of the parameter qualityLevel , and
A \> B, the vector of returned corners with qualityLevel=A will be the prefix of the output vector
with qualityLevel=B .
**See also**: cornerMinEigenVal, cornerHarris, calcOpticalFlowPyrLK, estimateRigidTransform,
.:

**Parameters**:
* `image`: Input 8-bit or floating-point 32-bit, single-channel image.
* `corners`: Output vector of detected corners.
* `maxCorners`: Maximum number of corners to return. If there are more corners than are found, the strongest of them is returned. `maxCorners <= 0` implies that no limit on the maximum is set and all detected corners are returned.
* `qualityLevel`: Parameter characterizing the minimal accepted quality of image corners. The parameter value is multiplied by the best corner quality measure, which is the minimal eigenvalue (see `cornerMinEigenVal` ) or the Harris function response (see `cornerHarris` ). The corners with the quality measure less than the product are rejected. For example, if the best corner has the quality measure = 1500, and the qualityLevel=0.01 , then all the corners with the quality measure less than 15 are rejected.
* `minDistance`: Minimum possible Euclidean distance between the returned corners.
* `mask`: Optional region of interest. If the image is not empty (it needs to have the type CV_8UC1 and the same size as image ), it specifies the region in which the corners are detected.
* `blockSize`: Size of an average block for computing a derivative covariation matrix over each pixel neighborhood. See cornerEigenValsAndVecs .
* `useHarrisDetector`: Parameter indicating whether to use a Harris detector (see `cornerHarris`) or `cornerMinEigenVal`.
* `k`: Free parameter of the Harris detector.

---
### `Cv2.GoodFeaturesToTrack`
**Signature**: `void GoodFeaturesToTrack(Mat image, Mat corners, int maxCorners, double qualityLevel, double minDistance, Mat mask, int blockSize, int gradientSize, bool useHarrisDetector, double k)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `image`: Input image.
* `corners`: The corners parameter.
* `maxCorners`: The maxCorners parameter.
* `qualityLevel`: The qualityLevel parameter.
* `minDistance`: The minDistance parameter.
* `mask`: Optional operation mask.
* `blockSize`: The blockSize parameter.
* `gradientSize`: The gradientSize parameter.
* `useHarrisDetector`: The useHarrisDetector parameter.
* `k`: The k parameter.

---
### `Cv2.GoodFeaturesToTrack`
**Signature**: `void GoodFeaturesToTrack(Mat image, Mat corners, int maxCorners, double qualityLevel, double minDistance, Mat mask, Mat cornersQuality, int blockSize, int gradientSize, bool useHarrisDetector, double k)`

Same as above, but returns also quality measure of the detected corners.

**Parameters**:
* `image`: Input 8-bit or floating-point 32-bit, single-channel image.
* `corners`: Output vector of detected corners.
* `maxCorners`: Maximum number of corners to return. If there are more corners than are found, the strongest of them is returned. `maxCorners <= 0` implies that no limit on the maximum is set and all detected corners are returned.
* `qualityLevel`: Parameter characterizing the minimal accepted quality of image corners. The parameter value is multiplied by the best corner quality measure, which is the minimal eigenvalue (see `cornerMinEigenVal` ) or the Harris function response (see `cornerHarris` ). The corners with the quality measure less than the product are rejected. For example, if the best corner has the quality measure = 1500, and the qualityLevel=0.01 , then all the corners with the quality measure less than 15 are rejected.
* `minDistance`: Minimum possible Euclidean distance between the returned corners.
* `mask`: Region of interest. If the image is not empty (it needs to have the type CV_8UC1 and the same size as image ), it specifies the region in which the corners are detected.
* `cornersQuality`: Output vector of quality measure of the detected corners.
* `blockSize`: Size of an average block for computing a derivative covariation matrix over each pixel neighborhood. See cornerEigenValsAndVecs .
* `gradientSize`: Aperture parameter for the Sobel operator used for derivatives computation. See cornerEigenValsAndVecs .
* `useHarrisDetector`: Parameter indicating whether to use a Harris detector (see `cornerHarris`) or `cornerMinEigenVal`.
* `k`: Free parameter of the Harris detector.

---
### `Cv2.DrawKeypoints`
**Signature**: `void DrawKeypoints(Mat image, IntPtr keypoints, Mat outImage, Scalar color, DrawMatchesFlags flags)`

Draws keypoints.

**Detailed Remarks**:
.: info Note

Flags are specified via DrawMatchesFlags options,
DrawMatchesFlags.DrawRichKeypoints, DrawMatchesFlags.DrawOverOutimg,
DrawMatchesFlags.NotDrawSinglePoints
.:

**Parameters**:
* `image`: Source image.
* `keypoints`: Keypoints from the source image.
* `outImage`: Output image. Its content depends on the flags value defining what is drawn in the output image. See possible flags bit values below.
* `color`: Color of keypoints.
* `flags`: Flags setting drawing features. Possible flags bit values are defined by DrawMatchesFlags. See details above in drawMatches .

---
### `Cv2.DrawMatches`
**Signature**: `void DrawMatches(Mat img1, IntPtr keypoints1, Mat img2, IntPtr keypoints2, IntPtr matches1to2, Mat outImg, Scalar matchColor, Scalar singlePointColor, IntPtr matchesMask, DrawMatchesFlags flags)`

Draws the found matches of keypoints from two images.

**Parameters**:
* `img1`: First source image.
* `keypoints1`: Keypoints from the first source image.
* `img2`: Second source image.
* `keypoints2`: Keypoints from the second source image.
* `matches1to2`: Matches from the first image to the second one, which means that keypoints1[i] has a corresponding point in keypoints2[matches[i]] .
* `outImg`: Output image. Its content depends on the flags value defining what is drawn in the output image. See possible flags bit values below.
* `matchColor`: Color of matches (lines and connected keypoints). If matchColor==Scalar.all(-1) , the color is generated randomly.
* `singlePointColor`: Color of single keypoints (circles), which means that keypoints do not have the matches. If singlePointColor==Scalar.all(-1) , the color is generated randomly.
* `matchesMask`: Mask determining which matches are drawn. If the mask is empty, all matches are drawn.
* `flags`: Flags setting drawing features. Possible flags bit values are defined by DrawMatchesFlags. This function draws matches of keypoints from two images in the output image. Match is a line connecting two keypoints (circles). See DrawMatchesFlags.

---
### `Cv2.DrawMatches`
**Signature**: `void DrawMatches(Mat img1, IntPtr keypoints1, Mat img2, IntPtr keypoints2, IntPtr matches1to2, Mat outImg, int matchesThickness, Scalar matchColor, Scalar singlePointColor, IntPtr matchesMask, DrawMatchesFlags flags)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `img1`: The img1 parameter.
* `keypoints1`: The keypoints1 parameter.
* `img2`: The img2 parameter.
* `keypoints2`: The keypoints2 parameter.
* `matches1to2`: The matches1to2 parameter.
* `outImg`: The outImg parameter.
* `matchesThickness`: The matchesThickness parameter.
* `matchColor`: The matchColor parameter.
* `singlePointColor`: The singlePointColor parameter.
* `matchesMask`: The matchesMask parameter.
* `flags`: Operation flags.

---
## 🔢 Enumerations

### `ANNIndexDistance`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Euclidean`** | `0` | Euclidean |
| **`Manhattan`** | `1` | Manhattan |
| **`Angular`** | `2` | Angular |
| **`Hamming`** | `3` | Hamming |
| **`Dotproduct`** | `4` | Dotproduct |

---
### `DescriptorMatcherMatcherType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Flannbased`** | `1` | Flannbased |
| **`Bruteforce`** | `2` | Bruteforce |
| **`BruteforceL1`** | `3` | BruteforceL1 |
| **`BruteforceHamming`** | `4` | BruteforceHamming |
| **`BruteforceHamminglut`** | `5` | BruteforceHamminglut |
| **`BruteforceSl2`** | `6` | BruteforceSl2 |

---
### `DrawMatchesFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `0` | Default |
| **`DrawOverOutimg`** | `1` | DrawOverOutimg |
| **`NotDrawSinglePoints`** | `2` | NotDrawSinglePoints |
| **`DrawRichKeypoints`** | `4` | DrawRichKeypoints |

---
### `UnnamedEnum2FastFeatureDetector`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Threshold`** | `10000` | Threshold |
| **`NonmaxSuppression`** | `10001` | NonmaxSuppression |
| **`FastN`** | `10002` | FastN |

---
### `FastFeatureDetectorDetectorType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_58`** | `0` | _58 |
| **`_712`** | `1` | _712 |
| **`_916`** | `2` | _916 |

---
### `OrbScoreType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`HarrisScore`** | `0` | HarrisScore |
| **`FastScore`** | `1` | FastScore |

---

</div>