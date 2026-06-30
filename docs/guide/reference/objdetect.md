# OBJDETECT Module API Reference

Complete documentation for the **OBJDETECT** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Objdetect Documentation](https://docs.opencv.org/5.x/main_modules/objdetect.html).

---
<div v-pre>

## 📦 Classes and Structs

### `CirclesGridFinderParameters`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`DensityNeighborhoodSize`** | `Size2F` | Gets or sets the densityNeighborhoodSize property. |
| **`MinDensity`** | `float` | Gets or sets the minDensity property. |
| **`KmeansAttempts`** | `int` | Gets or sets the kmeansAttempts property. |
| **`MinDistanceToAddKeypoint`** | `int` | Gets or sets the minDistanceToAddKeypoint property. |
| **`KeypointScale`** | `int` | Gets or sets the keypointScale property. |
| **`MinGraphConfidence`** | `float` | Gets or sets the minGraphConfidence property. |
| **`VertexGain`** | `float` | Gets or sets the vertexGain property. |
| **`VertexPenalty`** | `float` | Gets or sets the vertexPenalty property. |
| **`ExistingVertexGain`** | `float` | Gets or sets the existingVertexGain property. |
| **`EdgeGain`** | `float` | Gets or sets the edgeGain property. |
| **`EdgePenalty`** | `float` | Gets or sets the edgePenalty property. |
| **`ConvexHullFactor`** | `float` | Gets or sets the convexHullFactor property. |
| **`MinRNGEdgeSwitchDist`** | `float` | Gets or sets the minRNGEdgeSwitchDist property. |
| **`GridType`** | `CirclesGridFinderParametersGridType` | Gets or sets the gridType property. |
| **`SquareSize`** | `float` | Gets or sets the squareSize property. |
| **`MaxRectifiedDistance`** | `float` | Gets or sets the maxRectifiedDistance property. |

#### Constructors
* `new CirclesGridFinderParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `FaceDetectorYN`
**Inherits from**: `DisposableOpenCVObject`

DNN-based face detector

**Detailed Remarks**:
model download link: https://github.com/opencv/opencv_zoo/tree/master/models/face_detection_yunet

#### Methods
* `void SetInputSize(Size input_size)`
  * *Summary*: Set the size for the network input, which overwrites the input size of creating model. Call this method when the size of input image does not match the input size when creating model * * **input_size** the size of the input image
  * *Parameter* `input_size`: The input_size parameter.
* `Size GetInputSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetScoreThreshold(float score_threshold)`
  * *Summary*: Set the score threshold to filter out bounding boxes of score less than the given value * * **score_threshold** threshold for filtering out bounding boxes
  * *Parameter* `score_threshold`: The score_threshold parameter.
* `float GetScoreThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetNMSThreshold(float nms_threshold)`
  * *Summary*: Set the Non-maximum-suppression threshold to suppress bounding boxes that have IoU greater than the given value * * **nms_threshold** threshold for NMS operation
  * *Parameter* `nms_threshold`: The nms_threshold parameter.
* `float GetNMSThreshold()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetTopK(int top_k)`
  * *Summary*: Set the number of bounding boxes preserved before NMS * * **top_k** the number of bounding boxes to preserve from top rank based on score
  * *Parameter* `top_k`: The top_k parameter.
* `int GetTopK()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Detect(Mat image, Mat faces)`
  * *Summary*: Detects faces in the input image. Following is an example output.
  * *Remarks*:

* 
*  * **Parameter** `image`:  an image to detect
*  * **Parameter** `faces`:  detection results stored in a 2D Mat of shape [num_faces, 15]
*  - 0-1: x, y of bbox top left corner
*  - 2-3: width, height of bbox
*  - 4-5: x, y of right eye (blue point in the example image)
*  - 6-7: x, y of left eye (red point in the example image)
*  - 8-9: x, y of nose tip (green point in the example image)
*  - 10-11: x, y of right corner of mouth (pink point in the example image)
*  - 12-13: x, y of left corner of mouth (yellow point in the example image)
*  - 14: face score

  * *Parameter* `image`: Input image.
  * *Parameter* `faces`: The faces parameter.
  * *Returns*: The returned value.
* `FaceDetectorYN? Create(string model, string config, Size input_size, float score_threshold, float nms_threshold, int top_k, int backend_id, int target_id)`
  * *Summary*: Creates an instance of face detector class with given parameters * *  **model** the path to the requested model *  **config** the path to the config file for compatibility, which is not requested for ONNX models *  **input_size** the size of the input image *  **score_threshold** the threshold to filter out bounding boxes of score smaller than the given value *  **nms_threshold** the threshold to suppress bounding boxes of IoU bigger than the given value *  **top_k** keep top K bboxes before NMS *  **backend_id** the id of backend *  **target_id** the id of target device
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
  * *Parameter* `input_size`: The input_size parameter.
  * *Parameter* `score_threshold`: The score_threshold parameter.
  * *Parameter* `nms_threshold`: The nms_threshold parameter.
  * *Parameter* `top_k`: The top_k parameter.
  * *Parameter* `backend_id`: The backend_id parameter.
  * *Parameter* `target_id`: The target_id parameter.
  * *Returns*: The returned value.
* `FaceDetectorYN? Create(string framework, IntPtr bufferModel, IntPtr bufferConfig, Size input_size, float score_threshold, float nms_threshold, int top_k, int backend_id, int target_id)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  * **Parameter** `framework`:  Name of origin framework
*  * **Parameter** `bufferModel`:  A buffer with a content of binary file with weights
*  * **Parameter** `bufferConfig`:  A buffer with a content of text file contains network configuration
*  * **Parameter** `input_size`:  the size of the input image
*  * **Parameter** `score_threshold`:  the threshold to filter out bounding boxes of score smaller than the given value
*  * **Parameter** `nms_threshold`:  the threshold to suppress bounding boxes of IoU bigger than the given value
*  * **Parameter** `top_k`:  keep top K bboxes before NMS
*  * **Parameter** `backend_id`:  the id of backend
*  * **Parameter** `target_id`:  the id of target device

  * *Parameter* `framework`: The framework parameter.
  * *Parameter* `bufferModel`: The bufferModel parameter.
  * *Parameter* `bufferConfig`: The bufferConfig parameter.
  * *Parameter* `input_size`: The input_size parameter.
  * *Parameter* `score_threshold`: The score_threshold parameter.
  * *Parameter* `nms_threshold`: The nms_threshold parameter.
  * *Parameter* `top_k`: The top_k parameter.
  * *Parameter* `backend_id`: The backend_id parameter.
  * *Parameter* `target_id`: The target_id parameter.
  * *Returns*: The returned value.

---
### `FaceRecognizerSF`
**Inherits from**: `DisposableOpenCVObject`

DNN-based face recognizer

**Detailed Remarks**:
model download link: https://github.com/opencv/opencv_zoo/tree/master/models/face_recognition_sface

#### Methods
* `void AlignCrop(Mat src_img, Mat face_box, Mat aligned_img)`
  * *Summary*: Aligns detected face with the source input image and crops it *  **src_img** input image *  **face_box** the detected face result from the input image *  **aligned_img** output aligned image
  * *Parameter* `src_img`: The src_img parameter.
  * *Parameter* `face_box`: The face_box parameter.
  * *Parameter* `aligned_img`: The aligned_img parameter.
* `void Feature(Mat aligned_img, Mat face_feature)`
  * *Summary*: Extracts face feature from aligned image *  **aligned_img** input aligned image *  **face_feature** output face feature
  * *Parameter* `aligned_img`: The aligned_img parameter.
  * *Parameter* `face_feature`: The face_feature parameter.
* `double Match(Mat face_feature1, Mat face_feature2, int dis_type)`
  * *Summary*: Calculates the distance between two face features *  **face_feature1** the first input feature *  **face_feature2** the second input feature of the same size and the same type as face_feature1 *  **dis_type** defines how to calculate the distance between two face features with optional values "FR_COSINE" or "FR_NORM_L2"
  * *Parameter* `face_feature1`: The face_feature1 parameter.
  * *Parameter* `face_feature2`: The face_feature2 parameter.
  * *Parameter* `dis_type`: The dis_type parameter.
  * *Returns*: The returned value.
* `FaceRecognizerSF? Create(string model, string config, int backend_id, int target_id)`
  * *Summary*: Creates an instance of this class with given parameters *  **model** the path of the onnx model used for face recognition *  **config** the path to the config file for compatibility, which is not requested for ONNX models *  **backend_id** the id of backend *  **target_id** the id of target device
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
  * *Parameter* `backend_id`: The backend_id parameter.
  * *Parameter* `target_id`: The target_id parameter.
  * *Returns*: The returned value.
* `FaceRecognizerSF? Create(string framework, IntPtr bufferModel, IntPtr bufferConfig, int backend_id, int target_id)`
  * *Summary*: *   Creates an instance of this class from a buffer containing the model weights and configuration.
  * *Remarks*:

*  * **Parameter** `framework`:  Name of the framework (ONNX, etc.)
*  * **Parameter** `bufferModel`:  A buffer containing the binary model weights.
*  * **Parameter** `bufferConfig`:  A buffer containing the network configuration.
*  * **Parameter** `backend_id`:  The id of the backend.
*  * **Parameter** `target_id`:  The id of the target device.

**Returns**: A pointer to the created instance of FaceRecognizerSF.

  * *Parameter* `framework`: The framework parameter.
  * *Parameter* `bufferModel`: The bufferModel parameter.
  * *Parameter* `bufferConfig`: The bufferConfig parameter.
  * *Parameter* `backend_id`: The backend_id parameter.
  * *Parameter* `target_id`: The target_id parameter.
  * *Returns*: The returned value.

---
### `GraphicalCodeDetector`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Methods
* `bool Detect(Mat img, Mat points)`
  * *Summary*: Detects graphical code in image and returns the quadrangle containing the code.
  * *Parameter* `img`: grayscale or color (BGR) image containing (or not) graphical code.
  * *Parameter* `points`: Output vector of vertices of the minimum-area quadrangle containing the code.
  * *Returns*: The returned value.
* `string? Decode(Mat img, Mat points, Mat? straight_code)`
  * *Summary*: Decodes graphical code in image once it's found by the detect() method.
  * *Remarks*:

Returns UTF8-encoded output string or empty string if the code cannot be decoded.

  * *Parameter* `img`: grayscale or color (BGR) image containing graphical code.
  * *Parameter* `points`: Quadrangle vertices found by detect() method (or some other algorithm).
  * *Parameter* `straight_code`: The optional output image containing binarized code, will be empty if not found.
  * *Returns*: The returned value.
* `string? DetectAndDecode(Mat img, Mat? points, Mat? straight_code)`
  * *Summary*: Both detects and decodes graphical code
  * *Parameter* `img`: grayscale or color (BGR) image containing graphical code.
  * *Parameter* `points`: optional output array of vertices of the found graphical code quadrangle, will be empty if not found.
  * *Parameter* `straight_code`: The optional output image containing binarized code
  * *Returns*: The returned value.
* `bool DetectMulti(Mat img, Mat points)`
  * *Summary*: Detects graphical codes in image and returns the vector of the quadrangles containing the codes.
  * *Parameter* `img`: grayscale or color (BGR) image containing (or not) graphical codes.
  * *Parameter* `points`: Output vector of vector of vertices of the minimum-area quadrangle containing the codes.
  * *Returns*: The returned value.
* `bool DecodeMulti(Mat img, Mat points, IntPtr decoded_info, IntPtr straight_code)`
  * *Summary*: Decodes graphical codes in image once it's found by the detect() method.
  * *Parameter* `img`: grayscale or color (BGR) image containing graphical codes.
  * *Parameter* `points`: vector of Quadrangle vertices found by detect() method (or some other algorithm).
  * *Parameter* `decoded_info`: UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
  * *Parameter* `straight_code`: The optional output vector of images containing binarized codes
  * *Returns*: The returned value.
* `bool DetectAndDecodeMulti(Mat img, IntPtr decoded_info, Mat? points, IntPtr straight_code)`
  * *Summary*: Both detects and decodes graphical codes
  * *Parameter* `img`: grayscale or color (BGR) image containing graphical codes.
  * *Parameter* `decoded_info`: UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded.
  * *Parameter* `points`: optional output vector of vertices of the found graphical code quadrangles. Will be empty if not found.
  * *Parameter* `straight_code`: The optional vector of images containing binarized codes - If there are QR codes encoded with a Structured Append mode on the image and all of them detected and decoded correctly, method writes a full message to position corresponds to 0-th code in a sequence. The rest of QR codes from the same sequence have empty string.
  * *Returns*: The returned value.

---
### `QRCodeDetector`
**Inherits from**: `GraphicalCodeDetector`

QR code detector.

#### Constructors
* `new QRCodeDetector()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `QRCodeDetector? SetEpsX(double epsX)`
  * *Summary*: sets the epsilon used during the horizontal scan of QR code stop marker detection.
  * *Parameter* `epsX`: Epsilon neighborhood, which allows you to determine the horizontal pattern of the scheme 1:1:3:1:1 according to QR code standard.
  * *Returns*: The returned value.
* `QRCodeDetector? SetEpsY(double epsY)`
  * *Summary*: sets the epsilon used during the vertical scan of QR code stop marker detection.
  * *Parameter* `epsY`: Epsilon neighborhood, which allows you to determine the vertical pattern of the scheme 1:1:3:1:1 according to QR code standard.
  * *Returns*: The returned value.
* `QRCodeDetector? SetUseAlignmentMarkers(bool useAlignmentMarkers)`
  * *Summary*: use markers to improve the position of the corners of the QR code * * alignmentMarkers using by default
  * *Parameter* `useAlignmentMarkers`: The useAlignmentMarkers parameter.
  * *Returns*: The returned value.
* `string? DecodeCurved(Mat img, Mat points, Mat? straight_qrcode)`
  * *Summary*: Decodes QR code on a curved surface in image once it's found by the detect() method.
  * *Remarks*:

Returns UTF8-encoded output string or empty string if the code cannot be decoded.

  * *Parameter* `img`: grayscale or color (BGR) image containing QR code.
  * *Parameter* `points`: Quadrangle vertices found by detect() method (or some other algorithm).
  * *Parameter* `straight_qrcode`: The optional output image containing rectified and binarized QR code
  * *Returns*: The returned value.
* `string? DetectAndDecodeCurved(Mat img, Mat? points, Mat? straight_qrcode)`
  * *Summary*: Both detects and decodes QR code on a curved surface
  * *Parameter* `img`: grayscale or color (BGR) image containing QR code.
  * *Parameter* `points`: optional output array of vertices of the found QR code quadrangle. Will be empty if not found.
  * *Parameter* `straight_qrcode`: The optional output image containing rectified and binarized QR code
  * *Returns*: The returned value.
* `IntPtr GetEncoding(int codeIdx)`
  * *Summary*: Returns a kind of encoding for the decoded info from the latest `decode` or `detectAndDecode` call
  * *Parameter* `codeIdx`: an index of the previously decoded QR code. When `decode` or `detectAndDecode` is used, valid value is zero. For `decodeMulti` or `detectAndDecodeMulti` use indices corresponding to the output order.
  * *Returns*: The returned value.

---
### `QRCodeDetectorAruco`
**Inherits from**: `GraphicalCodeDetector`

QR code detector based on Aruco markers detection code.

#### Constructors
* `new QRCodeDetectorAruco()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `IntPtr GetDetectorParameters()`
  * *Summary*: Detector parameters getter. See QRCodeDetectorAruco.Params
  * *Returns*: The returned value.
* `QRCodeDetectorAruco? SetDetectorParameters(IntPtr @params)`
  * *Summary*: Detector parameters setter. See QRCodeDetectorAruco.Params
  * *Parameter* `params`: The @params parameter.
  * *Returns*: The returned value.
* `IntPtr GetArucoParameters()`
  * *Summary*: Aruco detector parameters are used to search for the finder patterns.
  * *Returns*: The returned value.
* `void SetArucoParameters(IntPtr @params)`
  * *Summary*: Aruco detector parameters are used to search for the finder patterns.
  * *Parameter* `params`: The @params parameter.

---
### `QRCodeDetectorArucoParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`MinModuleSizeInPyramid`** | `float` | Gets or sets the minModuleSizeInPyramid property. |
| **`MaxRotation`** | `float` | Gets or sets the maxRotation property. |
| **`MaxModuleSizeMismatch`** | `float` | Gets or sets the maxModuleSizeMismatch property. |
| **`MaxTimingPatternMismatch`** | `float` | Gets or sets the maxTimingPatternMismatch property. |
| **`MaxPenalties`** | `float` | Gets or sets the maxPenalties property. |
| **`MaxColorsMismatch`** | `float` | Gets or sets the maxColorsMismatch property. |
| **`ScaleTimingPatternScore`** | `float` | Gets or sets the scaleTimingPatternScore property. |

#### Constructors
* `new QRCodeDetectorArucoParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `QRCodeEncoder`
**Inherits from**: `DisposableOpenCVObject`

QR code encoder.

#### Methods
* `QRCodeEncoder? Create(IntPtr parameters)`
  * *Summary*: Constructor
  * *Parameter* `parameters`: QR code encoder parameters QRCodeEncoder.Params
  * *Returns*: The returned value.
* `void Encode(string encoded_info, Mat qrcode)`
  * *Summary*: Generates QR code from input string.
  * *Parameter* `encoded_info`: Input string to encode.
  * *Parameter* `qrcode`: Generated QR code.
* `void EncodeStructuredAppend(string encoded_info, IntPtr qrcodes)`
  * *Summary*: Generates QR code from input string in Structured Append mode. The encoded message is splitting over a number of QR codes.
  * *Parameter* `encoded_info`: Input string to encode.
  * *Parameter* `qrcodes`: Vector of generated QR codes.

---
### `QRCodeEncoderParams`
**Inherits from**: `DisposableOpenCVObject`

QR code encoder parameters.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Version`** | `int` | Gets or sets the version property. |
| **`CorrectionLevel`** | `IntPtr` | Gets or sets the correction_level property. |
| **`Mode`** | `IntPtr` | Gets or sets the mode property. |
| **`StructureNumber`** | `int` | Gets or sets the structure_number property. |

#### Constructors
* `new QRCodeEncoderParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `ArucoArucoDetector`
**Inherits from**: `Algorithm`

The main functionality of ArucoDetector class is detection of markers in an image with detectMarkers() method. * * After detecting some markers in the image, you can try to find undetected markers from this dictionary with * refineDetectedMarkers() method. * * **See also:** DetectorParameters, RefineParameters

#### Constructors
* `new ArucoArucoDetector(ArucoDictionary? dictionary, ArucoDetectorParameters? detectorParams, ArucoRefineParameters? refineParams)`
  * *Summary*: Basic ArucoDetector constructor * * **dictionary** indicates the type of markers that will be searched * **detectorParams** marker detection parameters * **refineParams** marker refine detection parameters
  * *Parameter* `dictionary`: The dictionary parameter.
  * *Parameter* `detectorParams`: The detectorParams parameter.
  * *Parameter* `refineParams`: The refineParams parameter.
* `new ArucoArucoDetector(IntPtr dictionaries, ArucoDetectorParameters? detectorParams, ArucoRefineParameters? refineParams)`
  * *Summary*: ArucoDetector constructor for multiple dictionaries * * **dictionaries** indicates the type of markers that will be searched. Empty dictionaries will throw an error. * **detectorParams** marker detection parameters * **refineParams** marker refine detection parameters
  * *Parameter* `dictionaries`: The dictionaries parameter.
  * *Parameter* `detectorParams`: The detectorParams parameter.
  * *Parameter* `refineParams`: The refineParams parameter.

#### Methods
* `void DetectMarkers(Mat image, IntPtr corners, Mat ids, IntPtr rejectedImgPoints)`
  * *Summary*: Basic marker detection * * **image** input image * **corners** vector of detected marker corners. For each marker, its four corners * are provided, (e.g Point2f[][] ). For N detected markers, * the dimensions of this array is Nx4. The order of the corners is clockwise. * **ids** vector of identifiers of the detected markers. The identifier is of type int * (e.g. int[]). For N detected markers, the size of ids is also N. * The identifiers have the same order than the markers in the imgPoints array. * **rejectedImgPoints** contains the imgPoints of those squares whose inner code has not a * correct codification. Useful for debugging purposes. * * Performs marker detection in the input image. Only markers included in the first specified dictionary * are searched. For each detected marker, it returns the 2D position of its corner in the image * and its corresponding identifier. * Note that this function does not perform pose estimation. * **Note:** The function does not correct lens distortion or takes it into account. It's recommended to undistort * input image with corresponding camera model, if camera parameters are known * **See also:** undistort, estimatePoseSingleMarkers,  estimatePoseBoard
  * *Parameter* `image`: Input image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `ids`: The ids parameter.
  * *Parameter* `rejectedImgPoints`: The rejectedImgPoints parameter.
* `void DetectMarkersWithConfidence(Mat image, IntPtr corners, Mat ids, Mat markersConfidence, IntPtr rejectedImgPoints)`
  * *Summary*: Marker detection with confidence computation * * **image** input image * **corners** vector of detected marker corners. For each marker, its four corners * are provided, (e.g Point2f[][] ). For N detected markers, * the dimensions of this array is Nx4. The order of the corners is clockwise. * **ids** vector of identifiers of the detected markers. The identifier is of type int * (e.g. int[]). For N detected markers, the size of ids is also N. * The identifiers have the same order than the markers in the imgPoints array. * **markersConfidence** contains the normalized confidence [0;1] of the markers' detection, * defined as 1 minus the normalized uncertainty (percentage of incorrect pixel detections), * with 1 describing a pixel perfect detection. The confidence values are of type float * (e.g. float[]) * **rejectedImgPoints** contains the imgPoints of those squares whose inner code has not a * correct codification. Useful for debugging purposes. * * Performs marker detection in the input image. Only markers included in the first specified dictionary * are searched. For each detected marker, it returns the 2D position of its corner in the image * and its corresponding identifier. * Note that this function does not perform pose estimation. * **Note:** The function does not correct lens distortion or takes it into account. It's recommended to undistort * input image with corresponding camera model, if camera parameters are known * **See also:** undistort, estimatePoseSingleMarkers,  estimatePoseBoard
  * *Parameter* `image`: Input image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `ids`: The ids parameter.
  * *Parameter* `markersConfidence`: The markersConfidence parameter.
  * *Parameter* `rejectedImgPoints`: The rejectedImgPoints parameter.
* `void RefineDetectedMarkers(Mat image, ArucoBoard board, IntPtr detectedCorners, Mat detectedIds, IntPtr rejectedCorners, Mat? cameraMatrix, Mat? distCoeffs, Mat? recoveredIdxs)`
  * *Summary*: Refine not detected markers based on the already detected and the board layout * * **image** input image * **board** layout of markers in the board. * **detectedCorners** vector of already detected marker corners. * **detectedIds** vector of already detected marker identifiers. * **rejectedCorners** vector of rejected candidates during the marker detection process. * **cameraMatrix** optional input 3x3 floating-point camera matrix * formula * **distCoeffs** optional vector of distortion coefficients * formula of 4, 5, 8 or 12 elements * **recoveredIdxs** Optional array to returns the indexes of the recovered candidates in the * original rejectedCorners array. * * This function tries to find markers that were not detected in the basic detecMarkers function. * First, based on the current detected marker and the board layout, the function interpolates * the position of the missing markers. Then it tries to find correspondence between the reprojected * markers and the rejected candidates based on the minRepDistance and errorCorrectionRate parameters. * If camera parameters and distortion coefficients are provided, missing markers are reprojected * using projectPoint function. If not, missing marker projections are interpolated using global * homography, and all the marker corners in the board must have the same Z coordinate. * **Note:** This function assumes that the board only contains markers from one dictionary, so only the * first configured dictionary is used. It has to match the dictionary of the board to work properly.
  * *Parameter* `image`: Input image.
  * *Parameter* `board`: The board parameter.
  * *Parameter* `detectedCorners`: The detectedCorners parameter.
  * *Parameter* `detectedIds`: The detectedIds parameter.
  * *Parameter* `rejectedCorners`: The rejectedCorners parameter.
  * *Parameter* `cameraMatrix`: The cameraMatrix parameter.
  * *Parameter* `distCoeffs`: The distCoeffs parameter.
  * *Parameter* `recoveredIdxs`: The recoveredIdxs parameter.
* `void DetectMarkersMultiDict(Mat image, IntPtr corners, Mat ids, IntPtr rejectedImgPoints, Mat? dictIndices)`
  * *Summary*: Basic marker detection * * **image** input image * **corners** vector of detected marker corners. For each marker, its four corners * are provided, (e.g Point2f[][] ). For N detected markers, * the dimensions of this array is Nx4. The order of the corners is clockwise. * **ids** vector of identifiers of the detected markers. The identifier is of type int * (e.g. int[]). For N detected markers, the size of ids is also N. * The identifiers have the same order than the markers in the imgPoints array. * **rejectedImgPoints** contains the imgPoints of those squares whose inner code has not a * correct codification. Useful for debugging purposes. * **dictIndices** vector of dictionary indices for each detected marker. Use getDictionaries() to get the * list of corresponding dictionaries. * * Performs marker detection in the input image. Only markers included in the specific dictionaries * are searched. For each detected marker, it returns the 2D position of its corner in the image * and its corresponding identifier. * Note that this function does not perform pose estimation. * **Note:** The function does not correct lens distortion or takes it into account. It's recommended to undistort * input image with corresponding camera model, if camera parameters are known * **See also:** undistort, estimatePoseSingleMarkers,  estimatePoseBoard
  * *Parameter* `image`: Input image.
  * *Parameter* `corners`: The corners parameter.
  * *Parameter* `ids`: The ids parameter.
  * *Parameter* `rejectedImgPoints`: The rejectedImgPoints parameter.
  * *Parameter* `dictIndices`: The dictIndices parameter.
* `ArucoDictionary? GetDictionary()`
  * *Summary*: Returns first dictionary from internal list used for marker detection. * **Returns**: The first dictionary from the configured ArucoDetector.
  * *Returns*: The returned value.
* `void SetDictionary(ArucoDictionary dictionary)`
  * *Summary*: Sets and replaces the first dictionary in internal list to be used for marker detection. * * **dictionary** The new dictionary that will replace the first dictionary in the internal list.
  * *Parameter* `dictionary`: The dictionary parameter.
* `IntPtr GetDictionaries()`
  * *Summary*: Returns all dictionaries currently used for marker detection as a vector. * **Returns**: A Dictionary[] containing all dictionaries used by the ArucoDetector.
  * *Returns*: The returned value.
* `void SetDictionaries(IntPtr dictionaries)`
  * *Summary*: Sets the entire collection of dictionaries to be used for marker detection, replacing any existing dictionaries. * * **dictionaries** A Dictionary[] containing the new set of dictionaries to be used. * * Configures the ArucoDetector to use the provided vector of dictionaries for marker detection. * This method replaces any dictionaries that were previously set. * **Note:** Setting an empty vector of dictionaries will throw an error.
  * *Parameter* `dictionaries`: The dictionaries parameter.
* `ArucoDetectorParameters? GetDetectorParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetDetectorParameters(ArucoDetectorParameters detectorParameters)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `detectorParameters`: The detectorParameters parameter.
* `ArucoRefineParameters? GetRefineParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetRefineParameters(ArucoRefineParameters refineParameters)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `refineParameters`: The refineParameters parameter.
* `void Write(FileStorage fs, string name)`
  * *Summary*: simplified API for language bindings
  * *Parameter* `fs`: The fs parameter.
  * *Parameter* `name`: The name parameter.
* `void Read(FileNode fn)`
  * *Summary*: Reads algorithm parameters from a file storage
  * *Parameter* `fn`: The fn parameter.

---
### `ArucoBoard`
**Inherits from**: `DisposableOpenCVObject`

Board of ArUco markers * * A board is a set of markers in the 3D space with a common coordinate system. * The common form of a board of marker is a planar (2D) board, however any 3D layout can be used. * A Board object is composed by: * - The object points of the marker corners, i.e. their coordinates respect to the board system. * - The dictionary which indicates the type of markers of the board * - The identifier of all the markers in the board.

#### Constructors
* `new ArucoBoard(IntPtr objPoints, ArucoDictionary dictionary, Mat ids)`
  * *Summary*: Common Board constructor * * **objPoints** array of object points of all the marker corners in the board * **dictionary** the dictionary of markers employed for this board * **ids** vector of the identifiers of the markers in the board
  * *Parameter* `objPoints`: The objPoints parameter.
  * *Parameter* `dictionary`: The dictionary parameter.
  * *Parameter* `ids`: The ids parameter.

#### Methods
* `ArucoDictionary? GetDictionary()`
  * *Summary*: return the Dictionary of markers employed for this board
  * *Returns*: The returned value.
* `IntPtr GetObjPoints()`
  * *Summary*: return array of object points of all the marker corners in the board. * * Each marker include its 4 corners in this order: * -   objPoints[i][0] - left-top point of i-th marker * -   objPoints[i][1] - right-top point of i-th marker * -   objPoints[i][2] - right-bottom point of i-th marker * -   objPoints[i][3] - left-bottom point of i-th marker * * Markers are placed in a certain order - row by row, left to right in every row. For M markers, the size is Mx4.
  * *Returns*: The returned value.
* `IntPtr GetIds()`
  * *Summary*: vector of the identifiers of the markers in the board (should be the same size as objPoints) **Returns**: vector of the identifiers of the markers
  * *Returns*: The returned value.
* `IntPtr GetRightBottomCorner()`
  * *Summary*: get coordinate of the bottom right corner of the board, is set when calling the function create()
  * *Returns*: The returned value.
* `void MatchImagePoints(IntPtr detectedCorners, Mat detectedIds, Mat objPoints, Mat imgPoints)`
  * *Summary*: Given a board configuration and a set of detected markers, returns the corresponding * image points and object points, can be used in SolvePnP() * * **detectedCorners** List of detected marker corners of the board. * For Board and GridBoard the method expects Point2f[][] or Mat[] with Aruco marker corners. * For CharucoBoard the method expects Point2f[] or Mat with ChAruco corners (chess board corners matched with Aruco markers). * * **detectedIds** List of identifiers for each marker or charuco corner. * For any Board class the method expects int[] or Mat. * * **objPoints** Vector of marker points in the board coordinate space. * For any Board class the method expects Point3f[] objectPoints or Mat * * **imgPoints** Vector of marker points in the image coordinate space. * For any Board class the method expects Point2f[] objectPoints or Mat * * **See also:** SolvePnP
  * *Parameter* `detectedCorners`: The detectedCorners parameter.
  * *Parameter* `detectedIds`: The detectedIds parameter.
  * *Parameter* `objPoints`: The objPoints parameter.
  * *Parameter* `imgPoints`: The imgPoints parameter.
* `void GenerateImage(Size outSize, Mat img, int marginSize, int borderBits)`
  * *Summary*: Draw a planar board * * **outSize** size of the output image in pixels. * **img** output image with the board. The size of this image will be outSize * and the board will be on the center, keeping the board proportions. * **marginSize** minimum margins (in pixels) of the board in the output image * **borderBits** width of the marker borders. * * This function return the image of the board, ready to be printed.
  * *Parameter* `outSize`: The outSize parameter.
  * *Parameter* `img`: Input image.
  * *Parameter* `marginSize`: The marginSize parameter.
  * *Parameter* `borderBits`: The borderBits parameter.

---
### `ArucoCharucoBoard`
**Inherits from**: `ArucoBoard`

*  ChArUco board is a planar chessboard where the markers are placed inside the white squares of a chessboard.

**Detailed Remarks**:
* The benefits of ChArUco boards is that they provide both, ArUco markers versatility and chessboard corner precision,
* which is important for calibration and pose estimation. The board image can be drawn using generateImage() method.

#### Constructors
* `new ArucoCharucoBoard(Size size, float squareLength, float markerLength, ArucoDictionary dictionary, Mat? ids)`
  * *Summary*: CharucoBoard constructor * * **size** number of chessboard squares in x and y directions * **squareLength** squareLength chessboard square side length (normally in meters) * **markerLength** marker side length (same unit than squareLength) * **dictionary** dictionary of markers indicating the type of markers * **ids** array of id used markers * The first markers in the dictionary are used to fill the white chessboard squares.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `squareLength`: The squareLength parameter.
  * *Parameter* `markerLength`: The markerLength parameter.
  * *Parameter* `dictionary`: The dictionary parameter.
  * *Parameter* `ids`: The ids parameter.

#### Methods
* `void SetLegacyPattern(bool legacyPattern)`
  * *Summary*: set legacy chessboard pattern. * * Legacy setting creates chessboard patterns starting with a white box in the upper left corner * if there is an even row count of chessboard boxes, otherwise it starts with a black box. * This setting ensures compatibility to patterns created with OpenCV versions prior OpenCV 4.6.0. * See https://github.com/opencv/opencv/issues/23152. * * Default value: false.
  * *Parameter* `legacyPattern`: The legacyPattern parameter.
* `bool GetLegacyPattern()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Size GetChessboardSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `float GetSquareLength()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `float GetMarkerLength()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr GetChessboardCorners()`
  * *Summary*: get CharucoBoard.chessboardCorners
  * *Returns*: The returned value.
* `bool CheckCharucoCornersCollinear(Mat charucoIds)`
  * *Summary*: check whether the ChArUco markers are collinear * * **charucoIds** list of identifiers for each corner in charucoCorners per frame. **Returns**: bool value, 1 (true) if detected corners form a line, 0 (false) if they do not. * SolvePnP, calibration functions will fail if the corners are collinear (true). * * The number of ids in charucoIDs should be <= the number of chessboard corners in the board. * This functions checks whether the charuco corners are on a straight line (returns true, if so), or not (false). * Axis parallel, as well as diagonal and other straight lines detected.  Degenerate cases: * for number of charucoIDs <= 2,the function returns true.
  * *Parameter* `charucoIds`: The charucoIds parameter.
  * *Returns*: The returned value.

---
### `ArucoCharucoDetector`
**Inherits from**: `Algorithm`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new ArucoCharucoDetector(ArucoCharucoBoard board, ArucoCharucoParameters? charucoParams, ArucoDetectorParameters? detectorParams, ArucoRefineParameters? refineParams)`
  * *Summary*: Basic CharucoDetector constructor * * **board** ChAruco board * **charucoParams** charuco detection parameters * **detectorParams** marker detection parameters * **refineParams** marker refine detection parameters
  * *Parameter* `board`: The board parameter.
  * *Parameter* `charucoParams`: The charucoParams parameter.
  * *Parameter* `detectorParams`: The detectorParams parameter.
  * *Parameter* `refineParams`: The refineParams parameter.

#### Methods
* `ArucoCharucoBoard? GetBoard()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetBoard(ArucoCharucoBoard board)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `board`: The board parameter.
* `ArucoCharucoParameters? GetCharucoParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetCharucoParameters(ArucoCharucoParameters charucoParameters)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `charucoParameters`: The charucoParameters parameter.
* `ArucoDetectorParameters? GetDetectorParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetDetectorParameters(ArucoDetectorParameters detectorParameters)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `detectorParameters`: The detectorParameters parameter.
* `ArucoRefineParameters? GetRefineParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetRefineParameters(ArucoRefineParameters refineParameters)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `refineParameters`: The refineParameters parameter.
* `void DetectBoard(Mat image, Mat charucoCorners, Mat charucoIds, IntPtr markerCorners, Mat? markerIds)`
  * *Summary*: *  detect aruco markers and interpolate position of ChArUco board corners
  * *Remarks*:

* * **Parameter** `image`:  input image necessary for corner refinement. Note that markers are not detected and
* should be sent in corners and ids parameters.
* * **Parameter** `charucoCorners`:  interpolated chessboard corners.
* * **Parameter** `charucoIds`:  interpolated chessboard corners identifiers.
* * **Parameter** `markerCorners`:  vector of already detected markers corners. For each marker, its four
* corners are provided, (e.g Point2f[][] ). For N detected markers, the
* dimensions of this array should be Nx4. The order of the corners should be clockwise.
* If markerCorners and markerCorners are empty, the function detect aruco markers and ids.
* * **Parameter** `markerIds`:  list of identifiers for each marker in corners.
*  If markerCorners and markerCorners are empty, the function detect aruco markers and ids.

* This function receives the detected markers and returns the 2D position of the chessboard corners
* from a ChArUco board using the detected Aruco markers.

* If markerCorners and markerCorners are empty, the detectMarkers() will run and detect aruco markers and ids.

* If camera parameters are provided, the process is based in an approximated pose estimation, else it is based on local homography.
* Only visible corners are returned. For each corner, its corresponding identifier is also returned in charucoIds.
**See also**: *  findChessboardCorners
.: info Note
*  After OpenCV 4.6.0, there was an incompatible change in the ChArUco pattern generation algorithm for even row counts.
* Use aruco.CharucoBoard.setLegacyPattern() to ensure compatibility with patterns created using OpenCV versions prior to 4.6.0.
* For more information, see the issue: https://github.com/opencv/opencv/issues/23152
.:

  * *Parameter* `image`: Input image.
  * *Parameter* `charucoCorners`: The charucoCorners parameter.
  * *Parameter* `charucoIds`: The charucoIds parameter.
  * *Parameter* `markerCorners`: The markerCorners parameter.
  * *Parameter* `markerIds`: The markerIds parameter.
* `void DetectDiamonds(Mat image, IntPtr diamondCorners, Mat diamondIds, IntPtr markerCorners, Mat? markerIds)`
  * *Summary*: *  Detect ChArUco Diamond markers
  * *Remarks*:

* * **Parameter** `image`:  input image necessary for corner subpixel.
* * **Parameter** `diamondCorners`:  output list of detected diamond corners (4 corners per diamond). The order
* is the same than in marker corners: top left, top right, bottom right and bottom left. Similar
* format than the corners returned by detectMarkers (e.g Point2f[][] ).
* * **Parameter** `diamondIds`:  ids of the diamonds in diamondCorners. The id of each diamond is in fact of
* type Vec4i, so each diamond has 4 ids, which are the ids of the aruco markers composing the
* diamond.
* * **Parameter** `markerCorners`:  list of detected marker corners from detectMarkers function.
* If markerCorners and markerCorners are empty, the function detect aruco markers and ids.
* * **Parameter** `markerIds`:  list of marker ids in markerCorners.
* If markerCorners and markerCorners are empty, the function detect aruco markers and ids.

* This function detects Diamond markers from the previous detected ArUco markers. The diamonds
* are returned in the diamondCorners and diamondIds parameters. If camera calibration parameters
* are provided, the diamond search is based on reprojection. If not, diamond search is based on
* homography. Homography is faster than reprojection, but less accurate.

  * *Parameter* `image`: Input image.
  * *Parameter* `diamondCorners`: The diamondCorners parameter.
  * *Parameter* `diamondIds`: The diamondIds parameter.
  * *Parameter* `markerCorners`: The markerCorners parameter.
  * *Parameter* `markerIds`: The markerIds parameter.

---
### `ArucoCharucoParameters`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`CameraMatrix`** | `Mat?` | Gets or sets the cameraMatrix property. |
| **`DistCoeffs`** | `Mat?` | Gets or sets the distCoeffs property. |
| **`MinMarkers`** | `int` | Gets or sets the minMarkers property. |
| **`TryRefineMarkers`** | `bool` | Gets or sets the tryRefineMarkers property. |
| **`CheckMarkers`** | `bool` | Gets or sets the checkMarkers property. |

#### Constructors
* `new ArucoCharucoParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `ArucoDetectorParameters`
**Inherits from**: `DisposableOpenCVObject`

struct DetectorParameters is used by ArucoDetector

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`AdaptiveThreshWinSizeMin`** | `int` | Gets or sets the adaptiveThreshWinSizeMin property. |
| **`AdaptiveThreshWinSizeMax`** | `int` | Gets or sets the adaptiveThreshWinSizeMax property. |
| **`AdaptiveThreshWinSizeStep`** | `int` | Gets or sets the adaptiveThreshWinSizeStep property. |
| **`AdaptiveThreshConstant`** | `double` | Gets or sets the adaptiveThreshConstant property. |
| **`MinMarkerPerimeterRate`** | `double` | Gets or sets the minMarkerPerimeterRate property. |
| **`MaxMarkerPerimeterRate`** | `double` | Gets or sets the maxMarkerPerimeterRate property. |
| **`PolygonalApproxAccuracyRate`** | `double` | Gets or sets the polygonalApproxAccuracyRate property. |
| **`MinCornerDistanceRate`** | `double` | Gets or sets the minCornerDistanceRate property. |
| **`MinDistanceToBorder`** | `int` | Gets or sets the minDistanceToBorder property. |
| **`MinMarkerDistanceRate`** | `double` | Gets or sets the minMarkerDistanceRate property. |
| **`MinGroupDistance`** | `float` | Gets or sets the minGroupDistance property. |
| **`CornerRefinementMethod`** | `int` | Gets or sets the cornerRefinementMethod property. |
| **`CornerRefinementWinSize`** | `int` | Gets or sets the cornerRefinementWinSize property. |
| **`RelativeCornerRefinmentWinSize`** | `float` | Gets or sets the relativeCornerRefinmentWinSize property. |
| **`CornerRefinementMaxIterations`** | `int` | Gets or sets the cornerRefinementMaxIterations property. |
| **`CornerRefinementMinAccuracy`** | `double` | Gets or sets the cornerRefinementMinAccuracy property. |
| **`MarkerBorderBits`** | `int` | Gets or sets the markerBorderBits property. |
| **`PerspectiveRemovePixelPerCell`** | `int` | Gets or sets the perspectiveRemovePixelPerCell property. |
| **`PerspectiveRemoveIgnoredMarginPerCell`** | `double` | Gets or sets the perspectiveRemoveIgnoredMarginPerCell property. |
| **`MaxErroneousBitsInBorderRate`** | `double` | Gets or sets the maxErroneousBitsInBorderRate property. |
| **`MinOtsuStdDev`** | `double` | Gets or sets the minOtsuStdDev property. |
| **`ErrorCorrectionRate`** | `double` | Gets or sets the errorCorrectionRate property. |
| **`AprilTagQuadDecimate`** | `float` | Gets or sets the aprilTagQuadDecimate property. |
| **`AprilTagQuadSigma`** | `float` | Gets or sets the aprilTagQuadSigma property. |
| **`AprilTagMinClusterPixels`** | `int` | Gets or sets the aprilTagMinClusterPixels property. |
| **`AprilTagMaxNmaxima`** | `int` | Gets or sets the aprilTagMaxNmaxima property. |
| **`AprilTagCriticalRad`** | `float` | Gets or sets the aprilTagCriticalRad property. |
| **`AprilTagMaxLineFitMse`** | `float` | Gets or sets the aprilTagMaxLineFitMse property. |
| **`AprilTagMinWhiteBlackDiff`** | `int` | Gets or sets the aprilTagMinWhiteBlackDiff property. |
| **`AprilTagDeglitch`** | `int` | Gets or sets the aprilTagDeglitch property. |
| **`DetectInvertedMarker`** | `bool` | Gets or sets the detectInvertedMarker property. |
| **`UseAruco3Detection`** | `bool` | Gets or sets the useAruco3Detection property. |
| **`MinSideLengthCanonicalImg`** | `int` | Gets or sets the minSideLengthCanonicalImg property. |
| **`MinMarkerLengthRatioOriginalImg`** | `float` | Gets or sets the minMarkerLengthRatioOriginalImg property. |
| **`ValidBitIdThreshold`** | `float` | Gets or sets the validBitIdThreshold property. |

#### Constructors
* `new ArucoDetectorParameters()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `bool ReadDetectorParameters(FileNode fn)`
  * *Summary*: Read a new set of DetectorParameters from FileNode (use FileStorage.root()).
  * *Parameter* `fn`: The fn parameter.
  * *Returns*: The returned value.
* `bool WriteDetectorParameters(FileStorage fs, string? name)`
  * *Summary*: Write a set of DetectorParameters to FileStorage
  * *Parameter* `fs`: The fs parameter.
  * *Parameter* `name`: The name parameter.
  * *Returns*: The returned value.

---
### `ArucoDictionary`
**Inherits from**: `DisposableOpenCVObject`

Dictionary is a set of unique ArUco markers of the same size * * `bytesList` storing as 2-dimensions Mat with 4-th channels (CV_8UC4 type was used) and contains the marker codewords where: * - bytesList.rows is the dictionary size * - each marker is encoded using `nbytes = ceil(markerSize*markerSize/8.)` bytes * - each row contains all 4 rotations of the marker, so its length is `4*nbytes` * - the byte order in the bytesList[i] row: * `//bytes without rotation/bytes with rotation 1/bytes with rotation 2/bytes with rotation 3//` * So `bytesList.ptr(i)[k*nbytes + j]` is the j-th byte of i-th marker, in its k-th rotation. * **Note:** Python bindings generate matrix with shape of bytesList `dictionary_size x nbytes x 4`, * but it should be indexed like C++ version. 

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`BytesList`** | `Mat?` | Gets or sets the bytesList property. |
| **`MarkerSize`** | `int` | Gets or sets the markerSize property. |
| **`MaxCorrectionBits`** | `int` | Gets or sets the maxCorrectionBits property. |

#### Constructors
* `new ArucoDictionary()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new ArucoDictionary(Mat bytesList, int _markerSize, int maxcorr)`
  * *Summary*: Basic ArUco dictionary constructor * * **bytesList** bits for all ArUco markers in dictionary see memory layout in the class description * **_markerSize** ArUco marker size in units * **maxcorr** maximum number of bits that can be corrected
  * *Parameter* `bytesList`: The bytesList parameter.
  * *Parameter* `_markerSize`: The _markerSize parameter.
  * *Parameter* `maxcorr`: The maxcorr parameter.

#### Methods
* `bool ReadDictionary(FileNode fn)`
  * *Summary*: Read a new dictionary from FileNode. * * Dictionary example in YAML format:
 * nmarkers: 35
 * markersize: 6\n * maxCorrectionBits: 5\n * marker_0: "101011111011111001001001101100000000"\n * ...\n * marker_34: "011111010000111011111110110101100101"
  * *Parameter* `fn`: The fn parameter.
  * *Returns*: The returned value.
* `void WriteDictionary(FileStorage fs, string? name)`
  * *Summary*: Write a dictionary to FileStorage, format is the same as in readDictionary().
  * *Parameter* `fs`: The fs parameter.
  * *Parameter* `name`: The name parameter.
* `bool Identify(Mat onlyBits, int idx, int rotation, double maxCorrectionRate)`
  * *Summary*: Given a matrix of bits. Returns whether if marker is identified or not. * * Returns reference to the marker id in the dictionary (if any) and its rotation.
  * *Parameter* `onlyBits`: The onlyBits parameter.
  * *Parameter* `idx`: The idx parameter.
  * *Parameter* `rotation`: The rotation parameter.
  * *Parameter* `maxCorrectionRate`: The maxCorrectionRate parameter.
  * *Returns*: The returned value.
* `bool Identify(Mat onlyCellPixelRatio, int idx, int rotation, double maxCorrectionRate, float validBitIdThreshold)`
  * *Summary*: Given a matrix of pixel ratio raging from 0 to 1. Returns whether if marker is identified or not. * * Returns reference to the marker id in the dictionary (if any) and its rotation.
  * *Parameter* `onlyCellPixelRatio`: The onlyCellPixelRatio parameter.
  * *Parameter* `idx`: The idx parameter.
  * *Parameter* `rotation`: The rotation parameter.
  * *Parameter* `maxCorrectionRate`: The maxCorrectionRate parameter.
  * *Parameter* `validBitIdThreshold`: The validBitIdThreshold parameter.
  * *Returns*: The returned value.
* `int GetDistanceToId(Mat bits, int id, bool allRotations)`
  * *Summary*: Returns Hamming distance of the input bits to the specific id. * * If `allRotations` flag is set, the four possible marker rotations are considered
  * *Parameter* `bits`: The bits parameter.
  * *Parameter* `id`: The id parameter.
  * *Parameter* `allRotations`: The allRotations parameter.
  * *Returns*: The returned value.
* `void GenerateImageMarker(int id, int sidePixels, Mat _img, int borderBits)`
  * *Summary*: Generate a canonical marker image
  * *Parameter* `id`: The id parameter.
  * *Parameter* `sidePixels`: The sidePixels parameter.
  * *Parameter* `_img`: The _img parameter.
  * *Parameter* `borderBits`: The borderBits parameter.
* `Mat? GetByteListFromBits(Mat bits)`
  * *Summary*: Transform matrix of bits to list of bytes with 4 marker rotations
  * *Parameter* `bits`: The bits parameter.
  * *Returns*: The returned value.
* `Mat? GetBitsFromByteList(Mat byteList, int markerSize, int rotationId)`
  * *Summary*: Transform list of bytes to matrix of bits
  * *Parameter* `byteList`: The byteList parameter.
  * *Parameter* `markerSize`: The markerSize parameter.
  * *Parameter* `rotationId`: The rotationId parameter.
  * *Returns*: The returned value.
* `Mat? GetMarkerBits(int markerId, int rotationId)`
  * *Summary*: Get ground truth bits float
  * *Parameter* `markerId`: The markerId parameter.
  * *Parameter* `rotationId`: The rotationId parameter.
  * *Returns*: The returned value.

---
### `ArucoGridBoard`
**Inherits from**: `ArucoBoard`

Planar board with grid arrangement of markers * * More common type of board. All markers are placed in the same plane in a grid arrangement. * The board image can be drawn using generateImage() method.

#### Constructors
* `new ArucoGridBoard(Size size, float markerLength, float markerSeparation, ArucoDictionary dictionary, Mat? ids)`
  * *Summary*: *  GridBoard constructor
  * *Parameter* `size`: The size parameter.
  * *Parameter* `markerLength`: The markerLength parameter.
  * *Parameter* `markerSeparation`: The markerSeparation parameter.
  * *Parameter* `dictionary`: The dictionary parameter.
  * *Parameter* `ids`: The ids parameter.

#### Methods
* `Size GetGridSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `float GetMarkerLength()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `float GetMarkerSeparation()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `ArucoRefineParameters`
**Inherits from**: `DisposableOpenCVObject`

struct RefineParameters is used by ArucoDetector

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`MinRepDistance`** | `float` | Gets or sets the minRepDistance property. |
| **`ErrorCorrectionRate`** | `float` | Gets or sets the errorCorrectionRate property. |
| **`CheckAllOrders`** | `bool` | Gets or sets the checkAllOrders property. |

#### Constructors
* `new ArucoRefineParameters(float minRepDistance, float errorCorrectionRate, bool checkAllOrders)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `minRepDistance`: The minRepDistance parameter.
  * *Parameter* `errorCorrectionRate`: The errorCorrectionRate parameter.
  * *Parameter* `checkAllOrders`: The checkAllOrders parameter.

#### Methods
* `bool ReadRefineParameters(FileNode fn)`
  * *Summary*: Read a new set of RefineParameters from FileNode (use FileStorage.root()).
  * *Parameter* `fn`: The fn parameter.
  * *Returns*: The returned value.
* `bool WriteRefineParameters(FileStorage fs, string? name)`
  * *Summary*: Write a set of RefineParameters to FileStorage
  * *Parameter* `fs`: The fs parameter.
  * *Parameter* `name`: The name parameter.
  * *Returns*: The returned value.

---
### `BarcodeBarcodeDetector`
**Inherits from**: `GraphicalCodeDetector`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new BarcodeBarcodeDetector()`
  * *Summary*: Initialize the BarcodeDetector. Super resolution is disabled.
* `new BarcodeBarcodeDetector(string super_resolution_model_path)`
  * *Summary*: Initialize the BarcodeDetector with a Super Resolution model. * * Loads a Super Resolution DNN model in ONNX format, used to upscale small/low-quality * barcode crops before decoding for better quality. * * **Note:** Caffe models (`sr.prototxt` / `sr.caffemodel`) are no longer supported; convert * the model to ONNX (a converted `sr.onnx` is available from * https://github.com/WeChatCV/opencv_3rdparty/tree/wechat_qrcode). * * **super_resolution_model_path** path to a single-file ONNX Super Resolution model.
  * *Parameter* `super_resolution_model_path`: The super_resolution_model_path parameter.

#### Methods
* `bool DecodeWithType(Mat img, Mat points, IntPtr decoded_info, IntPtr decoded_type)`
  * *Summary*: Decodes barcode in image once it's found by the detect() method. * * **img** grayscale or color (BGR) image containing bar code. * **points** vector of rotated rectangle vertices found by detect() method (or some other algorithm). * For N detected barcodes, the dimensions of this array should be [N][4]. * Order of four points in Point2f[] is bottomLeft, topLeft, topRight, bottomRight. * **decoded_info** UTF8-encoded output vector of string or empty vector of string if the codes cannot be decoded. * **decoded_type** vector strings, specifies the type of these barcodes **Returns**: true if at least one valid barcode have been found
  * *Parameter* `img`: Input image.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `decoded_info`: The decoded_info parameter.
  * *Parameter* `decoded_type`: The decoded_type parameter.
  * *Returns*: The returned value.
* `bool DetectAndDecodeWithType(Mat img, IntPtr decoded_info, IntPtr decoded_type, Mat? points)`
  * *Summary*: Both detects and decodes barcode
  * *Remarks*:

* * **Parameter** `img`:  grayscale or color (BGR) image containing barcode.
* * **Parameter** `decoded_info`:  UTF8-encoded output vector of string(s) or empty vector of string if the codes cannot be decoded.
* * **Parameter** `decoded_type`:  vector of strings, specifies the type of these barcodes
* * **Parameter** `points`:  optional output vector of vertices of the found  barcode rectangle. Will be empty if not found.
**Returns**: true if at least one valid barcode have been found

  * *Parameter* `img`: Input image.
  * *Parameter* `decoded_info`: The decoded_info parameter.
  * *Parameter* `decoded_type`: The decoded_type parameter.
  * *Parameter* `points`: The points parameter.
  * *Returns*: The returned value.
* `double GetDownsamplingThreshold()`
  * *Summary*: Get detector downsampling threshold. * **Returns**: detector downsampling threshold
  * *Returns*: The returned value.
* `BarcodeBarcodeDetector? SetDownsamplingThreshold(double thresh)`
  * *Summary*: Set detector downsampling threshold. * * By default, the detect method resizes the input image to this limit if the smallest image size is is greater than the threshold. * Increasing this value can improve detection accuracy and the number of results at the expense of performance. * Correlates with detector scales. Setting this to a large value will disable downsampling. * **thresh** downsampling limit to apply (default 512) * **See also:** setDetectorScales
  * *Parameter* `thresh`: The thresh parameter.
  * *Returns*: The returned value.
* `void GetDetectorScales(IntPtr sizes)`
  * *Summary*: Returns detector box filter sizes. * * **sizes** output parameter for returning the sizes.
  * *Parameter* `sizes`: The sizes parameter.
* `BarcodeBarcodeDetector? SetDetectorScales(IntPtr sizes)`
  * *Summary*: Set detector box filter sizes. * * Adjusts the value and the number of box filters used in the detect step. * The filter sizes directly correlate with the expected line widths for a barcode. Corresponds to expected barcode distance. * If the downsampling limit is increased, filter sizes need to be adjusted in an inversely proportional way. * **sizes** box filter sizes, relative to minimum dimension of the image (default [0.01, 0.03, 0.06, 0.08])
  * *Parameter* `sizes`: The sizes parameter.
  * *Returns*: The returned value.
* `double GetGradientThreshold()`
  * *Summary*: Get detector gradient magnitude threshold. * **Returns**: detector gradient magnitude threshold.
  * *Returns*: The returned value.
* `BarcodeBarcodeDetector? SetGradientThreshold(double thresh)`
  * *Summary*: Set detector gradient magnitude threshold. * * Sets the coherence threshold for detected bounding boxes. * Increasing this value will generate a closer fitted bounding box width and can reduce false-positives. * Values between 16 and 1024 generally work, while too high of a value will remove valid detections. * **thresh** gradient magnitude threshold (default 64).
  * *Parameter* `thresh`: The thresh parameter.
  * *Returns*: The returned value.

---
### `MccCChecker`
**Inherits from**: `Algorithm`

CChecker

**Detailed Remarks**:
*  checker object

*     This class contains the information about the detected checkers,i.e, their
*     type, the corners of the chart, the color profile, the cost, centers chart,
*     etc.

#### Methods
* `MccCChecker? Create()`
  * *Summary*: Create a new CChecker object. * **Returns**: A pointer to the implementation of the CChecker
  * *Returns*: The returned value.
* `void SetTarget(MccColorChart _target)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_target`: The _target parameter.
* `void SetBox(IntPtr _box)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_box`: The _box parameter.
* `void SetChartsRGB(Mat _chartsRGB)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_chartsRGB`: The _chartsRGB parameter.
* `void SetChartsYCbCr(Mat _chartsYCbCr)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_chartsYCbCr`: The _chartsYCbCr parameter.
* `void SetCost(float _cost)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_cost`: The _cost parameter.
* `void SetCenter(Point2f _center)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_center`: The _center parameter.
* `MccColorChart GetTarget()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr GetBox()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr GetColorCharts()`
  * *Summary*: Computes and returns the coordinates of the central parts of the charts modules. * * This method computes transformation matrix from the checkers's coordinates (`CChecker.getBox()`) * and find by this the coordinates of the central parts of the charts modules. * It is used in `CCheckerDetector.draw()` and in `ChartsRGB` calculation.
  * *Returns*: The returned value.
* `Mat? GetChartsRGB(bool getStats)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `getStats`: The getStats parameter.
  * *Returns*: The returned value.
* `Mat? GetChartsYCbCr()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `float GetCost()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Point2f GetCenter()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `MccCCheckerDetector`
**Inherits from**: `Algorithm`

A class to find the positions of the ColorCharts in the image.

#### Methods
* `bool Process(Mat image, IntPtr regionsOfInterest, int nc)`
  * *Summary*: Find the ColorCharts in the given image. * * The found charts are not returned but instead stored in the * detector, these can be accessed later on using getBestColorChecker() * and getListColorChecker() * **image** image in color space BGR * **regionsOfInterest** regions of image to look for the chart, if *                          it is empty, charts are looked for in the *                          entire image * **nc** number of charts in the image, if you don't know the exact *           then keeping this number high helps. **Returns**: true if atleast one chart is detected otherwise false
  * *Parameter* `image`: Input image.
  * *Parameter* `regionsOfInterest`: The regionsOfInterest parameter.
  * *Parameter* `nc`: The nc parameter.
  * *Returns*: The returned value.
* `bool Process(Mat image, int nc)`
  * *Summary*: Find the ColorCharts in the given image. * * Differs from the above one only in the arguments. * * This version searches for the chart in the full image. * * The found charts are not returned but instead stored in the * detector, these can be accessed later on using getBestColorChecker() * and getListColorChecker() * **image** image in color space BGR * **nc** number of charts in the image, if you don't know the exact *           then keeping this number high helps. **Returns**: true if atleast one chart is detected otherwise false
  * *Parameter* `image`: Input image.
  * *Parameter* `nc`: The nc parameter.
  * *Returns*: The returned value.
* `IntPtr GetBestColorChecker()`
  * *Summary*: Get the best color checker. By the best it means the one *         detected with the highest confidence. **Returns**: checker A single colorchecker, if atleast one colorchecker *                 was detected, 'null' otherwise.
  * *Returns*: The returned value.
* `IntPtr GetListColorChecker()`
  * *Summary*: Get the list of all detected colorcheckers **Returns**: checkers vector of colorcheckers
  * *Returns*: The returned value.
* `MccCCheckerDetector? Create()`
  * *Summary*: Returns the implementation of the CCheckerDetector. *
  * *Returns*: The returned value.
* `void Draw(IntPtr checkers, Mat img, Scalar color, int thickness)`
  * *Summary*: Draws the checker to the given image. * **img** image in color space BGR * **checkers** The checkers which will be drawn by this object. * **color** The color by with which the squares of the checker *         will be drawn * **thickness** The thickness with which the sqaures will be *         drawn
  * *Parameter* `checkers`: The checkers parameter.
  * *Parameter* `img`: Input image.
  * *Parameter* `color`: Color value (BGR or BGRA).
  * *Parameter* `thickness`: Line thickness.
* `Mat? GetRefColors()`
  * *Summary*: Gets the reference color for chart.
  * *Returns*: The returned value.
* `void SetDetectionParams(MccDetectorParametersMCC @params)`
  * *Summary*: Sets the detection paramaters for mcc. * **params** DetectorParametersMCC structure containing detection configuration parameters.
  * *Parameter* `params`: The @params parameter.
* `void SetColorChartType(MccColorChart chartType)`
  * *Summary*: Sets the color chart type for MCC detection. * **chartType** ColorChart enum specifying the type of color chart to detect.
  * *Parameter* `chartType`: The chartType parameter.
* `MccDetectorParametersMCC? GetDetectionParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `MccColorChart GetColorChartType()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `MccDetectorParametersMCC`
**Inherits from**: `DisposableOpenCVObject`

struct DetectorParametersMCC is used by CCheckerDetector

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`AdaptiveThreshWinSizeMin`** | `int` | Gets or sets the adaptiveThreshWinSizeMin property. |
| **`AdaptiveThreshWinSizeMax`** | `int` | Gets or sets the adaptiveThreshWinSizeMax property. |
| **`AdaptiveThreshWinSizeStep`** | `int` | Gets or sets the adaptiveThreshWinSizeStep property. |
| **`AdaptiveThreshConstant`** | `double` | Gets or sets the adaptiveThreshConstant property. |
| **`MinContoursAreaRate`** | `double` | Gets or sets the minContoursAreaRate property. |
| **`MinContoursArea`** | `double` | Gets or sets the minContoursArea property. |
| **`ConfidenceThreshold`** | `double` | Gets or sets the confidenceThreshold property. |
| **`MinContourSolidity`** | `double` | Gets or sets the minContourSolidity property. |
| **`FindCandidatesApproxPolyDPEpsMultiplier`** | `double` | Gets or sets the findCandidatesApproxPolyDPEpsMultiplier property. |
| **`BorderWidth`** | `int` | Gets or sets the borderWidth property. |
| **`B0factor`** | `float` | Gets or sets the B0factor property. |
| **`MaxError`** | `float` | Gets or sets the maxError property. |
| **`MinContourPointsAllowed`** | `int` | Gets or sets the minContourPointsAllowed property. |
| **`MinContourLengthAllowed`** | `int` | Gets or sets the minContourLengthAllowed property. |
| **`MinInterContourDistance`** | `int` | Gets or sets the minInterContourDistance property. |
| **`MinInterCheckerDistance`** | `int` | Gets or sets the minInterCheckerDistance property. |
| **`MinImageSize`** | `int` | Gets or sets the minImageSize property. |
| **`MinGroupSize`** | `int` | Gets or sets the minGroupSize property. |

#### Constructors
* `new MccDetectorParametersMCC()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.FindChessboardCorners`
**Signature**: `bool FindChessboardCorners(Mat image, Size patternSize, Mat corners, int flags)`

Finds the positions of internal corners of the chessboard.

**Detailed Remarks**:
.: info Note
The function requires white space (like a square-thick border, the wider the better) around
the board to make the detection more robust in various environments. Otherwise, if there is no
border and the background is dark, the outer black squares cannot be segmented properly and so the
square grouping and ordering algorithm fails.
Use the checkerboard generator script (`tutorial_camera_calibration_pattern`)
to create the desired checkerboard pattern.
.:

**Parameters**:
* `image`: Source chessboard view. It must be an 8-bit grayscale or color image.
* `patternSize`: Number of inner corners per a chessboard row and column ( patternSize = Size(points_per_row,points_per_column) = Size(columns,rows) ).
* `corners`: Output array of detected corners.
* `flags`: Various operation flags that can be zero or a combination of the following values: -   `CALIB_CB_ADAPTIVE_THRESH` Use adaptive thresholding to convert the image to black and white, rather than a fixed threshold level (computed from the average image brightness). -   `CALIB_CB_NORMALIZE_IMAGE` Normalize the image gamma with EqualizeHist before applying fixed or adaptive thresholding. -   `CALIB_CB_FILTER_QUADS` Use additional criteria (like contour area, perimeter, square-like shape) to filter out false quads extracted at the contour retrieval stage. -   `CALIB_CB_FAST_CHECK` Run a fast check on the image that looks for chessboard corners, and shortcut the call if none is found. This can drastically speed up the call in the degenerate condition when no chessboard is observed. -   `CALIB_CB_PLAIN` All other flags are ignored. The input image is taken as is. No image processing is done to improve to find the checkerboard. This has the effect of speeding up the execution of the function but could lead to not recognizing the checkerboard if the image is not previously binarized in the appropriate manner.

**Returns**: True if all of the corners are found and placed in a certain order (row by row, left to right in every row). Otherwise, if the function fails to find all the corners or reorder them, it returns false. The function attempts to determine whether the input image is a view of the chessboard pattern and locate the internal chessboard corners. For example, a regular chessboard has 8 x 8 squares and 7 x 7 internal corners, that is, points where the black squares touch each other. The detected coordinates are approximate, and to determine their positions more accurately, the function calls `CornerSubPix`. You also may use the function `CornerSubPix` with different parameters if returned coordinates are not accurate enough. Sample usage of detecting and drawing chessboard corners:

```csharp
var patternSize = new Size(8, 6);
var gray = new Mat(); // source image
var corners = new Mat();
bool patternFound = Cv2.FindChessboardCorners(gray, patternSize, corners, 1 + 2 + 8);
if (patternFound)
{
    Cv2.CornerSubPix(gray, corners, new Size(11, 11), new Size(-1, -1), new TermCriteria(1 + 2, 30, 0.1));
}
using var img = new Mat();
Cv2.DrawChessboardCorners(img, patternSize, corners, patternFound);
```

---
### `Cv2.CheckChessboard`
**Signature**: `bool CheckChessboard(Mat img, Size size)`

Checks whether the image contains chessboard of the specific size or not.

**Parameters**:
* `img`: Source chessboard view.
* `size`: Size of the chessboard.

**Returns**: Whether a chessboard was found.

---
### `Cv2.FindChessboardCornersSB`
**Signature**: `bool FindChessboardCornersSB(Mat image, Size patternSize, Mat corners, int flags, Mat meta)`

Finds the positions of internal corners of the chessboard using a sector based approach.

**Detailed Remarks**:
.: info Note
The function requires a white boarder with roughly the same width as one
of the checkerboard fields around the whole board to improve the detection in
various environments. In addition, because of the localized radon
transformation it is beneficial to use round corners for the field corners
which are located on the outside of the board. The following figure illustrates
a sample checkerboard optimized for the detection. However, any other checkerboard
can be used as well.
Use the checkerboard generator script (`tutorial_camera_calibration_pattern`)
to create the corresponding checkerboard pattern:

.:

**Parameters**:
* `image`: Source chessboard view. It must be an 8-bit grayscale or color image.
* `patternSize`: Number of inner corners per a chessboard row and column ( patternSize = Size(points_per_row,points_per_column) = Size(columns,rows) ).
* `corners`: Output array of detected corners.
* `flags`: Various operation flags that can be zero or a combination of the following values: -   `CALIB_CB_NORMALIZE_IMAGE` Normalize the image gamma with EqualizeHist before detection. -   `CALIB_CB_EXHAUSTIVE` Run an exhaustive search to improve detection rate. -   `CALIB_CB_ACCURACY` Up sample input image to improve sub-pixel accuracy due to aliasing effects. -   `CALIB_CB_LARGER` The detected pattern is allowed to be larger than patternSize (see description). -   `CALIB_CB_MARKER` The detected pattern must have a marker (see description). This should be used if an accurate camera calibration is required.
* `meta`: Optional output array of detected corners (CV_8UC1 and size = Size(columns,rows)). Each entry stands for one corner of the pattern and can have one of the following values: -   0 = no meta data attached -   1 = left-top corner of a black cell -   2 = left-top corner of a white cell -   3 = left-top corner of a black cell with a white marker dot -   4 = left-top corner of a white cell with a black marker dot (pattern origin in case of markers otherwise first corner) The function is analog to `findChessboardCorners` but uses a localized radon transformation approximated by box filters being more robust to all sort of noise, faster on larger images and is able to directly return the sub-pixel position of the internal chessboard corners. The Method is based on the paper [duda2018] "Accurate Detection and Localization of Checkerboard Corners for Calibration" demonstrating that the returned sub-pixel positions are more accurate than the one returned by CornerSubPix allowing a precise camera calibration for demanding applications. In the case, the flags `CALIB_CB_LARGER` or `CALIB_CB_MARKER` are given, the result can be recovered from the optional meta array. Both flags are helpful to use calibration patterns exceeding the field of view of the camera. These oversized patterns allow more accurate calibrations as corners can be utilized, which are as close as possible to the image borders.  For a consistent coordinate system across all images, the optional marker (see image below) can be used to move the origin of the board to the location where the black circle is located.

**Returns**: The returned value.

---
### `Cv2.FindChessboardCornersSB`
**Signature**: `bool FindChessboardCornersSB(Mat image, Size patternSize, Mat corners, int flags)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: Input image.
* `patternSize`: The patternSize parameter.
* `corners`: The corners parameter.
* `flags`: Operation flags.

**Returns**: The returned value.

---
### `Cv2.EstimateChessboardSharpness`
**Signature**: `Scalar EstimateChessboardSharpness(Mat image, Size patternSize, Mat corners, float rise_distance, bool vertical, Mat? sharpness)`

Estimates the sharpness of a detected chessboard.

**Detailed Remarks**:
Image sharpness, as well as brightness, are a critical parameter for accuracte
camera calibration. For accessing these parameters for filtering out
problematic calibraiton images, this method calculates edge profiles by traveling from
black to white chessboard cell centers. Based on this, the number of pixels is
calculated required to transit from black to white. This width of the
transition area is a good indication of how sharp the chessboard is imaged
and should be below ~3.0 pixels.

**Parameters**:
* `image`: Gray image used to find chessboard corners
* `patternSize`: Size of a found chessboard pattern
* `corners`: Corners found by `findChessboardCornersSB`
* `rise_distance`: Rise distance 0.8 means 10% ... 90% of the final signal strength
* `vertical`: By default edge responses for horizontal lines are calculated
* `sharpness`: Optional output array with a sharpness value for calculated edge responses (see description) The optional sharpness array is of type CV_32FC1 and has for each calculated profile one row with the following five entries: * 0 = x coordinate of the underlying edge in the image * 1 = y coordinate of the underlying edge in the image * 2 = width of the transition area (sharpness) * 3 = signal strength in the black cell (min brightness) * 4 = signal strength in the white cell (max brightness)

**Returns**: Scalar(average sharpness, average min brightness, average max brightness,0)

---
### `Cv2.Find4QuadCornerSubpix`
**Signature**: `bool Find4QuadCornerSubpix(Mat img, Mat corners, Size region_size)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `img`: Input image.
* `corners`: The corners parameter.
* `region_size`: The region_size parameter.

**Returns**: The returned value.

---
### `Cv2.DrawChessboardCorners`
**Signature**: `void DrawChessboardCorners(Mat image, Size patternSize, Mat corners, bool patternWasFound)`

Renders the detected chessboard corners.

**Parameters**:
* `image`: Destination image. It must be an 8-bit color image.
* `patternSize`: Number of inner corners per a chessboard row and column (patternSize = Size(points_per_row,points_per_column)).
* `corners`: Array of detected corners, the output of `findChessboardCorners`.
* `patternWasFound`: Parameter indicating whether the complete board was found or not. The return value of `findChessboardCorners` should be passed here. The function draws individual chessboard corners detected either as red circles if the board was not found, or as colored corners connected with lines if the board was found.

---
### `Cv2.FindCirclesGrid`
**Signature**: `bool FindCirclesGrid(Mat image, Size patternSize, Mat centers, int flags, IntPtr blobDetector, CirclesGridFinderParameters parameters)`

Finds centers in the grid of circles.

**Detailed Remarks**:
.: info Note
The function requires white space (like a square-thick border, the wider the better) around
the board to make the detection more robust in various environments.
.:

**Parameters**:
* `image`: grid view of input circles; it must be an 8-bit grayscale or color image.
* `patternSize`: number of circles per row and column ( patternSize = Size(points_per_row, points_per_column) ).
* `centers`: output array of detected centers.
* `flags`: various operation flags that can be one of the following values: -   `CALIB_CB_SYMMETRIC_GRID` uses symmetric pattern of circles. -   `CALIB_CB_ASYMMETRIC_GRID` uses asymmetric pattern of circles. -   `CALIB_CB_CLUSTERING` uses a special algorithm for grid detection. It is more robust to perspective distortions but much more sensitive to background clutter.
* `blobDetector`: feature detector that finds blobs like dark circles on light background. If `blobDetector` is null then `image` represents Point2f array of candidates.
* `parameters`: struct for finding circles in a grid pattern. return True if all of the centers have been found and they have been placed in a certain order (row by row, left to right in every row). Otherwise, if the function fails to find all the corners or reorder them, it returns false. The function attempts to determine whether the input image contains a grid of circles. If it is, the function locates centers of the circles. Sample usage of detecting and drawing the centers of circles:

```csharp
var patternSize = new Size(7, 7);
var gray = new Mat(); // source image
var centers = new Mat();
bool patternFound = Cv2.FindCirclesGrid(gray, patternSize, centers, 0, null);
using var img = new Mat();
Cv2.DrawChessboardCorners(img, patternSize, centers, patternFound);
```

**Returns**: The returned value.

---
### `Cv2.FindCirclesGrid`
**Signature**: `bool FindCirclesGrid(Mat image, Size patternSize, Mat centers, int flags, IntPtr blobDetector)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: Input image.
* `patternSize`: The patternSize parameter.
* `centers`: The centers parameter.
* `flags`: Operation flags.
* `blobDetector`: The blobDetector parameter.

**Returns**: The returned value.

---
### `Cv2.ArucoDrawDetectedMarkers`
**Signature**: `void ArucoDrawDetectedMarkers(Mat image, IntPtr corners, Mat? ids, Scalar borderColor)`

Draw detected markers in image * * **image** input/output image. It must have 1 or 3 channels. The number of channels is not altered. * **corners** positions of marker corners on input image. * (e.g Point2f[][] ). For N detected markers, the dimensions of * this array should be Nx4. The order of the corners should be clockwise. * **ids** vector of identifiers for markers in markersCorners . * Optional, if not provided, ids are not painted. * **borderColor** color of marker borders. Rest of colors (text color and first corner color) * are calculated based on this one to improve visualization. * * Given an array of detected marker corners and its corresponding ids, this functions draws * the markers in the image. The marker borders are painted and the markers identifiers if provided. * Useful for debugging purposes.

**Parameters**:
* `image`: Input image.
* `corners`: The corners parameter.
* `ids`: The ids parameter.
* `borderColor`: The borderColor parameter.

---
### `Cv2.ArucoGenerateImageMarker`
**Signature**: `void ArucoGenerateImageMarker(ArucoDictionary dictionary, int id, int sidePixels, Mat img, int borderBits)`

Generate a canonical marker image * * **dictionary** dictionary of markers indicating the type of markers * **id** identifier of the marker that will be returned. It has to be a valid id in the specified dictionary. * **sidePixels** size of the image in pixels * **img** output image with the marker * **borderBits** width of the marker border. * * This function returns a marker image in its canonical form (i.e. ready to be printed)

**Parameters**:
* `dictionary`: The dictionary parameter.
* `id`: The id parameter.
* `sidePixels`: The sidePixels parameter.
* `img`: Input image.
* `borderBits`: The borderBits parameter.

---
### `Cv2.ArucoGetPredefinedDictionary`
**Signature**: `ArucoDictionary? ArucoGetPredefinedDictionary(int dict)`

Returns one of the predefined dictionaries referenced by DICT_*.

**Parameters**:
* `dict`: The dict parameter.

**Returns**: The returned value.

---
### `Cv2.ArucoExtendDictionary`
**Signature**: `ArucoDictionary? ArucoExtendDictionary(int nMarkers, int markerSize, ArucoDictionary? baseDictionary, int randomSeed)`

Extend base dictionary by new nMarkers * * **nMarkers** number of markers in the dictionary * **markerSize** number of bits per dimension of each markers * **baseDictionary** Include the markers in this dictionary at the beginning (optional) * **randomSeed** a user supplied seed for theRNG() * * This function creates a new dictionary composed by nMarkers markers and each markers composed * by markerSize x markerSize bits. If baseDictionary is provided, its markers are directly * included and the rest are generated based on them. If the size of baseDictionary is higher * than nMarkers, only the first nMarkers in baseDictionary are taken and no new marker is added.

**Parameters**:
* `nMarkers`: The nMarkers parameter.
* `markerSize`: The markerSize parameter.
* `baseDictionary`: The baseDictionary parameter.
* `randomSeed`: The randomSeed parameter.

**Returns**: The returned value.

---
### `Cv2.ArucoDrawDetectedCornersCharuco`
**Signature**: `void ArucoDrawDetectedCornersCharuco(Mat image, Mat charucoCorners, Mat? charucoIds, Scalar cornerColor)`

*  Draws a set of Charuco corners

**Detailed Remarks**:
* * **Parameter** `image`:  input/output image. It must have 1 or 3 channels. The number of channels is not
* altered.
* * **Parameter** `charucoCorners`:  vector of detected charuco corners
* * **Parameter** `charucoIds`:  list of identifiers for each corner in charucoCorners
* * **Parameter** `cornerColor`:  color of the square surrounding each corner

* This function draws a set of detected Charuco corners. If identifiers vector is provided, it also
* draws the id of each corner.

**Parameters**:
* `image`: Input image.
* `charucoCorners`: The charucoCorners parameter.
* `charucoIds`: The charucoIds parameter.
* `cornerColor`: The cornerColor parameter.

---
### `Cv2.ArucoDrawDetectedDiamonds`
**Signature**: `void ArucoDrawDetectedDiamonds(Mat image, IntPtr diamondCorners, Mat? diamondIds, Scalar borderColor)`

*  Draw a set of detected ChArUco Diamond markers

**Detailed Remarks**:
* * **Parameter** `image`:  input/output image. It must have 1 or 3 channels. The number of channels is not
* altered.
* * **Parameter** `diamondCorners`:  positions of diamond corners in the same format returned by
* detectCharucoDiamond(). (e.g Point2f[][] ). For N detected markers,
* the dimensions of this array should be Nx4. The order of the corners should be clockwise.
* * **Parameter** `diamondIds`:  vector of identifiers for diamonds in diamondCorners, in the same format
* returned by detectCharucoDiamond() (e.g. Vec4i[]).
* Optional, if not provided, ids are not painted.
* * **Parameter** `borderColor`:  color of marker borders. Rest of colors (text color and first corner color)
* are calculated based on this one.

* Given an array of detected diamonds, this functions draws them in the image. The marker borders
* are painted and the markers identifiers if provided.
* Useful for debugging purposes.

**Parameters**:
* `image`: Input image.
* `diamondCorners`: The diamondCorners parameter.
* `diamondIds`: The diamondIds parameter.
* `borderColor`: The borderColor parameter.

---
## 🔢 Enumerations

### `CirclesGridFinderParametersGridType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`SymmetricGrid`** | `0` | SymmetricGrid |
| **`AsymmetricGrid`** | `1` | AsymmetricGrid |

---
### `FaceRecognizerSFDisType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Cosine`** | `0` | Cosine |
| **`NormL2`** | `1` | NormL2 |

---
### `QRCodeEncoderCorrectionLevel`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`L`** | `0` | L |
| **`M`** | `1` | M |
| **`Q`** | `2` | Q |
| **`H`** | `3` | H |

---
### `QRCodeEncoderECIEncodings`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ShiftJis`** | `20` | ShiftJis |
| **`Utf8`** | `26` | Utf8 |

---
### `QRCodeEncoderEncodeMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Auto`** | `-1` | Auto |
| **`Numeric`** | `1` | Numeric |
| **`Alphanumeric`** | `2` | Alphanumeric |
| **`Byte`** | `4` | Byte |
| **`Eci`** | `7` | Eci |
| **`Kanji`** | `8` | Kanji |
| **`StructuredAppend`** | `3` | StructuredAppend |

---
### `ArucoCornerRefineMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Subpix`** | `1` | Subpix |
| **`Contour`** | `2` | Contour |
| **`Apriltag`** | `3` | Apriltag |

---
### `ArucoPredefinedDictionaryType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_4x450`** | `0` | _4x450 |
| **`_4x4100`** | `unchecked((int)(0 + 1))` | _4x4100 |
| **`_4x4250`** | `unchecked((int)(0 + 2))` | _4x4250 |
| **`_4x41000`** | `unchecked((int)(0 + 3))` | _4x41000 |
| **`_5x550`** | `unchecked((int)(0 + 4))` | _5x550 |
| **`_5x5100`** | `unchecked((int)(0 + 5))` | _5x5100 |
| **`_5x5250`** | `unchecked((int)(0 + 6))` | _5x5250 |
| **`_5x51000`** | `unchecked((int)(0 + 7))` | _5x51000 |
| **`_6x650`** | `unchecked((int)(0 + 8))` | _6x650 |
| **`_6x6100`** | `unchecked((int)(0 + 9))` | _6x6100 |
| **`_6x6250`** | `unchecked((int)(0 + 10))` | _6x6250 |
| **`_6x61000`** | `unchecked((int)(0 + 11))` | _6x61000 |
| **`_7x750`** | `unchecked((int)(0 + 12))` | _7x750 |
| **`_7x7100`** | `unchecked((int)(0 + 13))` | _7x7100 |
| **`_7x7250`** | `unchecked((int)(0 + 14))` | _7x7250 |
| **`_7x71000`** | `unchecked((int)(0 + 15))` | _7x71000 |
| **`ArucoOriginal`** | `unchecked((int)(0 + 16))` | ArucoOriginal |
| **`APRILTAG16h5`** | `unchecked((int)(0 + 17))` | APRILTAG16h5 |
| **`APRILTAG25h9`** | `unchecked((int)(0 + 18))` | APRILTAG25h9 |
| **`APRILTAG36h10`** | `unchecked((int)(0 + 19))` | APRILTAG36h10 |
| **`APRILTAG36h11`** | `unchecked((int)(0 + 20))` | APRILTAG36h11 |
| **`ARUCOMIP36h12`** | `unchecked((int)(0 + 21))` | ARUCOMIP36h12 |

---
### `MccColorChart`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Mcc24`** | `0` | Mcc24 |
| **`Sg140`** | `unchecked((int)(0 + 1))` | Sg140 |
| **`Vinyl18`** | `unchecked((int)(0 + 2))` | Vinyl18 |

---

</div>