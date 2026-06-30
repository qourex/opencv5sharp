# DNN Module API Reference

Complete documentation for the **DNN** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Dnn Documentation](https://docs.opencv.org/5.x/main_modules/dnn.html).

---
<div v-pre>

## 📦 Classes and Structs

### `DnnClassificationModel`
**Inherits from**: `DnnModel`

This class represents high-level API for classification models. * * ClassificationModel allows to set params for preprocessing input image. * ClassificationModel creates net from file with trained weights and config, * sets preprocessing input, runs forward pass and return top-1 prediction.

#### Constructors
* `new DnnClassificationModel(string model, string? config)`
  * *Summary*: *  Create classification model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
* `new DnnClassificationModel(DnnNet network)`
  * *Summary*: *  Create model from deep learning network.
  * *Parameter* `network`: The network parameter.

#### Methods
* `DnnClassificationModel? SetEnableSoftmaxPostProcessing(bool enable)`
  * *Summary*: *  Set enable/disable softmax post processing option.
  * *Remarks*:

* If this option is true, softmax is applied after forward inference within the classify() function
* to convert the confidences range to [0.0-1.0].
* This function allows you to toggle this behavior.
* Please turn true when not contain softmax layer in model.
* * **Parameter** `enable`:  Set enable softmax post processing within the classify() function.

  * *Parameter* `enable`: The enable parameter.
  * *Returns*: The returned value.
* `bool GetEnableSoftmaxPostProcessing()`
  * *Summary*: *  Get enable/disable softmax post processing option.
  * *Remarks*:

* This option defaults to false, softmax post processing is not applied within the classify() function.

  * *Returns*: The returned value.
* `void Classify(Mat frame, int classId, float conf)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `classId`: The classId parameter.
  * *Parameter* `conf`: The conf parameter.

---
### `DnnDetectionModel`
**Inherits from**: `DnnModel`

This class represents high-level API for object detection networks. * * DetectionModel allows to set params for preprocessing input image. * DetectionModel creates net from file with trained weights and config, * sets preprocessing input, runs forward pass and return result detections. * For DetectionModel SSD, Faster R-CNN, YOLO topologies are supported.

#### Constructors
* `new DnnDetectionModel(string model, string? config)`
  * *Summary*: *  Create detection model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
* `new DnnDetectionModel(DnnNet network)`
  * *Summary*: *  Create model from deep learning network.
  * *Parameter* `network`: The network parameter.

#### Methods
* `DnnDetectionModel? SetNmsAcrossClasses(bool value)`
  * *Summary*: *  nmsAcrossClasses defaults to false,
  * *Remarks*:

* such that when non max suppression is used during the detect() function, it will do so per-class.
* This function allows you to toggle this behaviour.
* * **Parameter** `value`:  The new value for nmsAcrossClasses

  * *Parameter* `value`: The value parameter.
  * *Returns*: The returned value.
* `bool GetNmsAcrossClasses()`
  * *Summary*: *  Getter for nmsAcrossClasses. This variable defaults to false,
  * *Remarks*:

* such that when non max suppression is used during the detect() function, it will do so only per-class

  * *Returns*: The returned value.
* `void Detect(Mat frame, IntPtr classIds, IntPtr confidences, IntPtr boxes, float confThreshold, float nmsThreshold)`
  * *Summary*: Given the `input` frame, create input blob, run net and return result detections. *  **frame**  The input image. *  **classIds** Class indexes in result detection. *  **confidences** A set of corresponding confidences. *  **boxes** A set of bounding boxes. *  **confThreshold** A threshold used to filter boxes by confidences. *  **nmsThreshold** A threshold used in non maximum suppression.
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `classIds`: The classIds parameter.
  * *Parameter* `confidences`: The confidences parameter.
  * *Parameter* `boxes`: The boxes parameter.
  * *Parameter* `confThreshold`: The confThreshold parameter.
  * *Parameter* `nmsThreshold`: The nmsThreshold parameter.

---
### `DnnDict`
**Inherits from**: `DisposableOpenCVObject`

Stub class representing Dnn.Dict.

#### Constructors
* `new DnnDict()`
  * *Summary*: Creates a new empty DnnDict instance.

---
### `DnnDictValue`
**Inherits from**: `DisposableOpenCVObject`

This struct stores the scalar value (or array) of one of the following type: double, String or int64. *  

#### Constructors
* `new DnnDictValue(int i)`
  * *Summary*: Creates a DnnDictValue from an integer value.
  * *Parameter* `i`: The integer value to store.
* `new DnnDictValue(double p)`
  * *Summary*: Creates a DnnDictValue from a double (real) value.
  * *Parameter* `p`: The double value to store.
* `new DnnDictValue(string s)`
  * *Summary*: Creates a DnnDictValue from a string value.
  * *Parameter* `s`: The string value to store.

#### Methods
* `bool IsInt()`
  * *Summary*: Checks whether the stored value is of integer type.
  * *Returns*: `true` if the stored value is an integer; otherwise `false`.
* `bool IsString()`
  * *Summary*: Checks whether the stored value is of string type.
  * *Returns*: `true` if the stored value is a string; otherwise `false`.
* `bool IsReal()`
  * *Summary*: Checks whether the stored value is of real (double) type.
  * *Returns*: `true` if the stored value is a real number; otherwise `false`.
* `int GetIntValue(int idx)`
  * *Summary*: Returns the stored value as an integer.
  * *Parameter* `idx`: Index of the value in the array (use 0 for scalar values).
  * *Returns*: The integer representation of the stored value at the given index.
* `double GetRealValue(int idx)`
  * *Summary*: Returns the stored value as a double.
  * *Parameter* `idx`: Index of the value in the array (use 0 for scalar values).
  * *Returns*: The double representation of the stored value at the given index.
* `string? GetStringValue(int idx)`
  * *Summary*: Returns the stored value as a string.
  * *Parameter* `idx`: Index of the value in the array (use 0 for scalar values).
  * *Returns*: The string representation of the stored value at the given index, or `null`.

---
### `DnnImage2BlobParams`
**Inherits from**: `DisposableOpenCVObject`

Processing params of image to blob. * * It includes all possible image processing operations and corresponding parameters. * * **See also:** blobFromImageWithParams * * **Note:** * The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor. * The order and usage of `scalefactor`, `size`, `mean`, `swapRB`, and `ddepth` are consistent * with the function of `blobFromImage`.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Scalefactor`** | `Scalar` | Gets or sets the scalefactor property. |
| **`Size`** | `Size` | Gets or sets the size property. |
| **`Mean`** | `Scalar` | Gets or sets the mean property. |
| **`SwapRB`** | `bool` | Gets or sets the swapRB property. |
| **`Ddepth`** | `int` | Gets or sets the ddepth property. |
| **`Datalayout`** | `DataLayout` | Gets or sets the datalayout property. |
| **`Paddingmode`** | `IntPtr` | Gets or sets the paddingmode property. |
| **`BorderValue`** | `Scalar` | Gets or sets the borderValue property. |

#### Constructors
* `new DnnImage2BlobParams()`
  * *Summary*: Creates a DnnImage2BlobParams with default preprocessing parameters.
* `new DnnImage2BlobParams(Scalar scalefactor, Size size, Scalar mean, bool swapRB, int ddepth, DataLayout datalayout, DnnImagePaddingMode mode, Scalar borderValue)`
  * *Summary*: Creates a DnnImage2BlobParams with the specified preprocessing parameters.
  * *Parameter* `scalefactor`: The scalefactor parameter.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `mean`: The mean parameter.
  * *Parameter* `swapRB`: The swapRB parameter.
  * *Parameter* `ddepth`: The ddepth parameter.
  * *Parameter* `datalayout`: The datalayout parameter.
  * *Parameter* `mode`: The mode parameter.
  * *Parameter* `borderValue`: The borderValue parameter.

#### Methods
* `Rect BlobRectToImageRect(Rect rBlob, Size size)`
  * *Summary*: Get rectangle coordinates in original image system from rectangle in blob coordinates. *  **rBlob** rect in blob coordinates. *  **size** original input image size. *  **Returns**: rectangle in original image coordinates.
  * *Parameter* `rBlob`: The rBlob parameter.
  * *Parameter* `size`: The size parameter.
  * *Returns*: The returned value.
* `void BlobRectsToImageRects(IntPtr rBlob, IntPtr rImg, Size size)`
  * *Summary*: Get rectangle coordinates in original image system from rectangle in blob coordinates. *  **rBlob** rect in blob coordinates. *  **rImg** result rect in image coordinates. *  **size** original input image size.
  * *Parameter* `rBlob`: The rBlob parameter.
  * *Parameter* `rImg`: The rImg parameter.
  * *Parameter* `size`: The size parameter.

---
### `DnnKeypointsModel`
**Inherits from**: `DnnModel`

This class represents high-level API for keypoints models * * KeypointsModel allows to set params for preprocessing input image. * KeypointsModel creates net from file with trained weights and config, * sets preprocessing input, runs forward pass and returns the x and y coordinates of each detected keypoint

#### Constructors
* `new DnnKeypointsModel(string model, string? config)`
  * *Summary*: *  Create keypoints model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
* `new DnnKeypointsModel(DnnNet network)`
  * *Summary*: *  Create model from deep learning network.
  * *Parameter* `network`: The network parameter.

#### Methods
* `IntPtr Estimate(Mat frame, float thresh)`
  * *Summary*: Given the `input` frame, create input blob, run net *  **frame**  The input image. *  **thresh** minimum confidence threshold to select a keypoint *  **Returns**: a vector holding the x and y coordinates of each detected keypoint *
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `thresh`: The thresh parameter.
  * *Returns*: The returned value.

---
### `DnnLayer`
**Inherits from**: `Algorithm`

This interface class allows to build new Layers - are building blocks of networks. * * Each class, derived from Layer, must implement forward() method to compute outputs. * Also before using the new layer into networks you must register your layer by using one of LayerFactory macros.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Blobs`** | `Mat[]` | Gets or sets the blobs property. |
| **`Name`** | `string?` | Gets or sets the name property. |
| **`Type`** | `string?` | Gets or sets the type property. |
| **`PreferableTarget`** | `int` | Gets or sets the preferableTarget property. |

#### Methods
* `void Finalize(IntPtr inputs, IntPtr outputs)`
  * *Summary*: Computes and sets internal parameters according to inputs, outputs and blobs. *  **inputs**  vector of already allocated input blobs *  **outputs** vector of already allocated output blobs * * This method is called after network has allocated all memory for input and output blobs * and before inferencing.
  * *Parameter* `inputs`: The inputs parameter.
  * *Parameter* `outputs`: The outputs parameter.
* `void Run(IntPtr inputs, IntPtr outputs, IntPtr internals)`
  * *Summary*: Allocates layer and computes output. *  *(Deprecated)* This method will be removed in the future release.
  * *Parameter* `inputs`: The inputs parameter.
  * *Parameter* `outputs`: The outputs parameter.
  * *Parameter* `internals`: The internals parameter.
* `int OutputNameToIndex(string outputName)`
  * *Summary*: Returns index of output blob in output array. *  **See also:** inputNameToIndex()
  * *Parameter* `outputName`: The outputName parameter.
  * *Returns*: The returned value.

---
### `DnnLayerParams`
**Inherits from**: `DisposableOpenCVObject`

Stub class representing Dnn.LayerParams.

#### Constructors
* `new DnnLayerParams()`
  * *Summary*: Creates a new empty DnnLayerParams instance for configuring layer parameters.

---
### `DnnModel`
**Inherits from**: `DisposableOpenCVObject`

This class is presented high-level API for neural networks. * * Model allows to set params for preprocessing input image. * Model creates net from file with trained weights and config, * sets preprocessing input and runs forward pass.

#### Constructors
* `new DnnModel(string model, string? config)`
  * *Summary*: *  Create model from deep learning network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
* `new DnnModel(DnnNet network)`
  * *Summary*: *  Create model from deep learning network.
  * *Parameter* `network`: The network parameter.

#### Methods
* `DnnModel? SetInputSize(Size size)`
  * *Summary*: Set input size for frame. *  **size** New input size. *  **Note:** If shape of the new blob less than 0, then frame size not change.
  * *Parameter* `size`: The size parameter.
  * *Returns*: The returned value.
* `DnnModel? SetInputSize(int width, int height)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  * **Parameter** `width`:  New input width.
*  * **Parameter** `height`:  New input height.

  * *Parameter* `width`: The width parameter.
  * *Parameter* `height`: The height parameter.
  * *Returns*: The returned value.
* `DnnModel? SetInputMean(Scalar mean)`
  * *Summary*: Set mean value for frame. *  **mean** Scalar with mean values which are subtracted from channels.
  * *Parameter* `mean`: The mean parameter.
  * *Returns*: The returned value.
* `DnnModel? SetInputScale(Scalar scale)`
  * *Summary*: Set scalefactor value for frame. *  **scale** Multiplier for frame values.
  * *Parameter* `scale`: The scale parameter.
  * *Returns*: The returned value.
* `DnnModel? SetInputCrop(bool crop)`
  * *Summary*: Set flag crop for frame. *  **crop** Flag which indicates whether image will be cropped after resize or not.
  * *Parameter* `crop`: The crop parameter.
  * *Returns*: The returned value.
* `DnnModel? SetInputSwapRB(bool swapRB)`
  * *Summary*: Set flag swapRB for frame. *  **swapRB** Flag which indicates that swap first and last channels.
  * *Parameter* `swapRB`: The swapRB parameter.
  * *Returns*: The returned value.
* `DnnModel? SetOutputNames(IntPtr outNames)`
  * *Summary*: Set output names for frame. *  **outNames** Names for output layers.
  * *Parameter* `outNames`: The outNames parameter.
  * *Returns*: The returned value.
* `void SetInputParams(double scale, Size size, Scalar mean, bool swapRB, bool crop)`
  * *Summary*: Set preprocessing parameters for frame. *  **size** New input size. *  **mean** Scalar with mean values which are subtracted from channels. *  **scale** Multiplier for frame values. *  **swapRB** Flag which indicates that swap first and last channels. *  **crop** Flag which indicates whether image will be cropped after resize or not. *  blob(n, c, y, x) = scale * resize( frame(y, x, c) ) - mean(c) )
  * *Parameter* `scale`: The scale parameter.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `mean`: The mean parameter.
  * *Parameter* `swapRB`: The swapRB parameter.
  * *Parameter* `crop`: The crop parameter.
* `void Predict(Mat frame, IntPtr outs)`
  * *Summary*: Given the `input` frame, create input blob, run net and return the output `blobs`. *  **frame**  The input image. *  **outs** Allocated output blobs, which will store results of the computation.
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `outs`: The outs parameter.
* `DnnModel? SetPreferableBackend(IntPtr backendId)`
  * *Summary*: Sets the preferred computation backend for the model.
  * *Parameter* `backendId`: Backend identifier (see `DnnBackend` enum).
  * *Returns*: The DnnModel instance for method chaining, or `null`.
* `DnnModel? SetPreferableTarget(IntPtr targetId)`
  * *Summary*: Sets the preferred target device for the model.
  * *Parameter* `targetId`: Target identifier (see `DnnTarget` enum).
  * *Returns*: The DnnModel instance for method chaining, or `null`.
* `DnnModel? EnableWinograd(bool useWinograd)`
  * *Summary*: Enables or disables the Winograd compute branch for 3x3 convolutions. Can speed up inference at a small loss of accuracy.
  * *Parameter* `useWinograd`: `true` to enable the Winograd compute branch; `false` to disable. Default is `true`.
  * *Returns*: The DnnModel instance for method chaining, or `null`.

---
### `DnnNet`
**Inherits from**: `DisposableOpenCVObject`

This class allows to create and manipulate comprehensive artificial neural networks. * * Neural network is presented as directed acyclic graph (DAG), where vertices are Layer instances, * and edges specify relationships between layers inputs and outputs. * * Each network layer has unique integer id and unique string name inside its network. * LayerId can store either layer name or layer id. * * This class supports reference counting of its instances, i. e. copies point to the same instance.

#### Constructors
* `new DnnNet()`
  * *Summary*: Creates a new empty DnnNet instance with no layers.

#### Methods
* `DnnNet? ReadFromModelOptimizer(string xml, string bin)`
  * *Summary*: Create a network from Intel's Model Optimizer intermediate representation (IR). *  **xml** XML configuration file with network's topology. *  **bin** Binary file with trained weights. *  Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine *  backend.
  * *Parameter* `xml`: The xml parameter.
  * *Parameter* `bin`: The bin parameter.
  * *Returns*: The returned value.
* `DnnNet? ReadFromModelOptimizer(IntPtr bufferModelConfig, IntPtr bufferWeights)`
  * *Summary*: Create a network from Intel's Model Optimizer in-memory buffers with intermediate representation (IR). *  **bufferModelConfig** buffer with model's configuration. *  **bufferWeights** buffer with model's trained weights. *  **Returns**: Net object.
  * *Parameter* `bufferModelConfig`: The bufferModelConfig parameter.
  * *Parameter* `bufferWeights`: The bufferWeights parameter.
  * *Returns*: The returned value.
* `bool Empty()`
  * *Summary*: Returns true if there are no layers in the network.
  * *Returns*: The returned value.
* `string? Dump()`
  * *Summary*: Dump net to String *  **Returns**: String with structure, hyperparameters, backend, target and fusion *  Call method after setInput(). To see correct backend, target and fusion run after forward().
  * *Returns*: The returned value.
* `void DumpToFile(string path)`
  * *Summary*: Dump net structure, hyperparameters, backend, target and fusion to dot file *  **path**   path to output file with .dot extension *  **See also:** dump()
  * *Parameter* `path`: The path parameter.
* `void DumpToPbtxt(string path)`
  * *Summary*: Dump net structure, hyperparameters, backend, target and fusion to pbtxt file *  **path**   path to output file with .pbtxt extension * *  Use Netron (https://netron.app) to open the target file to visualize the model. *  Call method after setInput(). To see correct backend, target and fusion run after forward().
  * *Parameter* `path`: The path parameter.
* `int AddLayer(string name, string type, int dtype, DnnLayerParams @params)`
  * *Summary*: Adds new layer to the net. *  **name**   unique name of the adding layer. *  **type**   typename of the adding layer (type must be registered in LayerRegister). *  **dtype**  datatype of output blobs. *  **params** parameters which will be used to initialize the creating layer. *  **Returns**: unique identifier of created layer, or -1 if a failure will happen.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `dtype`: The dtype parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: The returned value.
* `int AddLayerToPrev(string name, string type, int dtype, DnnLayerParams @params)`
  * *Summary*: Adds new layer and connects its first input to the first output of previously added layer. *  **See also:** addLayer()
  * *Parameter* `name`: The name parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `dtype`: The dtype parameter.
  * *Parameter* `params`: The @params parameter.
  * *Returns*: The returned value.
* `int GetLayerId(string layer)`
  * *Summary*: Converts string name of the layer to the integer identifier. *  **Returns**: id of the layer, or -1 if the layer wasn't found.
  * *Parameter* `layer`: The layer parameter.
  * *Returns*: The returned value.
* `IntPtr GetLayerNames()`
  * *Summary*: Returns the names of all layers in the network.
  * *Returns*: A pointer to a vector of layer name strings.
* `DnnLayer? GetLayer(int layerId)`
  * *Summary*: Returns pointer to layer with specified id or name which the network use.
  * *Parameter* `layerId`: The layerId parameter.
  * *Returns*: The returned value.
* `DnnLayer? GetLayer(string layerName)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  *(Deprecated)* Use int GetLayerId(string layer)

  * *Parameter* `layerName`: The layerName parameter.
  * *Returns*: The returned value.
* `DnnLayer? GetLayer(IntPtr layerId)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  *(Deprecated)* to be removed

  * *Parameter* `layerId`: The layerId parameter.
  * *Returns*: The returned value.
* `void Connect(string outPin, string inpPin)`
  * *Summary*: Connects output of the first layer to input of the second layer. *  **outPin** descriptor of the first layer output. *  **inpPin** descriptor of the second layer input. * * Descriptors have the following template <DFN><layer_name>[.input_number]</DFN>: * - the first part of the template <DFN>layer_name</DFN> is string name of the added layer. *   If this part is empty then the network input pseudo layer will be used; * - the second optional part of the template <DFN>input_number</DFN> *   is either number of the layer input, either label one. *   If this part is omitted then the first layer input will be used. * *  **See also:** setNetInputs(), Layer.inputNameToIndex(), Layer.outputNameToIndex()
  * *Parameter* `outPin`: The outPin parameter.
  * *Parameter* `inpPin`: The inpPin parameter.
* `int RegisterOutput(string outputName, int layerId, int outputPort)`
  * *Summary*: Registers network output with name * *  Function may create additional 'Identity' layer. * *  **outputName** identifier of the output *  **layerId** identifier of the second layer *  **outputPort** number of the second layer input * *  **Returns**: index of bound layer (the same as layerId or newly created)
  * *Parameter* `outputName`: The outputName parameter.
  * *Parameter* `layerId`: The layerId parameter.
  * *Parameter* `outputPort`: The outputPort parameter.
  * *Returns*: The returned value.
* `void SetInputsNames(IntPtr inputBlobNames)`
  * *Summary*: Sets outputs names of the network input pseudo layer. * * Each net always has special own the network input pseudo layer with id=0. * This layer stores the user blobs only and don't make any computations. * In fact, this layer provides the only way to pass user data into the network. * As any other layer, this layer can label its outputs and this function provides an easy way to do this.
  * *Parameter* `inputBlobNames`: The inputBlobNames parameter.
* `void SetInputShape(string inputName, MatShape shape)`
  * *Summary*: Specify shape of network input.
  * *Parameter* `inputName`: The inputName parameter.
  * *Parameter* `shape`: The shape parameter.
* `Mat? Forward(string? outputName)`
  * *Summary*: Runs forward pass to compute output of layer with name `outputName`. *  **outputName** name for layer which output is needed to get **Returns**: blob for first output of specified layer. *  By default runs forward pass for the whole network.
  * *Parameter* `outputName`: The outputName parameter.
  * *Returns*: The returned value.
* `AsyncArray? ForwardAsync(string? outputName)`
  * *Summary*: Runs forward pass to compute output of layer with name `outputName`. *  **outputName** name for layer which output is needed to get *  By default runs forward pass for the whole network. * *  This is an asynchronous version of Forward(string). *  Dnn.Backend.InferenceEngine backend is required.
  * *Parameter* `outputName`: The outputName parameter.
  * *Returns*: The returned value.
* `void Forward(IntPtr outputBlobs, string? outputName)`
  * *Summary*: Runs forward pass to compute output of layer with name `outputName`. *  **outputBlobs** contains all output blobs for specified layer. *  **outputName** name for layer which output is needed to get *  If `outputName` is empty, runs forward pass for the whole network.
  * *Parameter* `outputBlobs`: The outputBlobs parameter.
  * *Parameter* `outputName`: The outputName parameter.
* `void Forward(IntPtr outputBlobs, IntPtr outBlobNames)`
  * *Summary*: Runs forward pass to compute outputs of layers listed in `outBlobNames`. *  **outputBlobs** contains blobs for first outputs of specified layers. *  **outBlobNames** names for layers which outputs are needed to get
  * *Parameter* `outputBlobs`: The outputBlobs parameter.
  * *Parameter* `outBlobNames`: The outBlobNames parameter.
* `void SetPreferableBackend(int backendId)`
  * *Summary*: *  Ask network to use specific computation backend where it supported.
  * *Remarks*:

* * **Parameter** `backendId`:  backend identifier.
**See also**: *  Backend

  * *Parameter* `backendId`: The backendId parameter.
* `void SetPreferableTarget(int targetId)`
  * *Summary*: *  Ask network to make computations on specific target device.
  * *Remarks*:

* * **Parameter** `targetId`:  target identifier.
**See also**: *  Target

* List of supported combinations backend / target:
* |                        | DNN_BACKEND_OPENCV | DNN_BACKEND_INFERENCE_ENGINE |  DNN_BACKEND_CUDA |
* |------------------------|--------------------|------------------------------|-------------------|
* | DNN_TARGET_CPU         |                  + |                            + |                   |
* | DNN_TARGET_OPENCL      |                  + |                            + |                   |
* | DNN_TARGET_OPENCL_FP16 |                  + |                            + |                   |
* | DNN_TARGET_MYRIAD      |                    |                            + |                   |
* | DNN_TARGET_FPGA        |                    |                            + |                   |
* | DNN_TARGET_CUDA        |                    |                              |                 + |
* | DNN_TARGET_CUDA_FP16   |                    |                              |                 + |
* | DNN_TARGET_HDDL        |                    |                            + |                   |

  * *Parameter* `targetId`: The targetId parameter.
* `void FinalizeNet()`
  * *Summary*: Finalizes the network configuration and prepares it for inference. * * This method must be called after setting backend/target via * setPreferableBackend() and setPreferableTarget(), and before the first * forward() call. It creates the underlying execution session (e.g. ONNX * Runtime session) on the configured backend/target. If not called * explicitly, the first forward() will call it automatically. * * Calling finalizeNet() early lets you pay the one-time setup cost at a * predictable point and catch configuration errors before inference.
* `void SetTracingMode(DnnTracingMode tracingMode)`
  * *Summary*: *  Set the tracing mode
  * *Remarks*:

* * **Parameter** `tracingMode`:  the tracing mode, see DNN_TRACE_*

  * *Parameter* `tracingMode`: The tracingMode parameter.
* `DnnTracingMode GetTracingMode()`
  * *Summary*: *  Retrieve the current tracing mode
  * *Returns*: The returned value.
* `void SetProfilingMode(DnnProfilingMode profilingMode)`
  * *Summary*: *  Set the profiling mode
  * *Remarks*:

* * **Parameter** `profilingMode`:  the profiling mode, see DNN_PROFILE_*

  * *Parameter* `profilingMode`: The profilingMode parameter.
* `DnnProfilingMode GetProfilingMode()`
  * *Summary*: *  Retrieve the current profiling mode
  * *Returns*: The returned value.
* `DnnModelFormat GetModelFormat()`
  * *Summary*: *  Retrieve the current model format, see DNN_MODEL_*
  * *Returns*: The returned value.
* `void SetInput(Mat blob, string? name, double scalefactor, Scalar mean)`
  * *Summary*: Sets the new input value for the network *  **blob**        A new blob. Should have CV_32F or CV_8U depth. *  **name**        A name of input layer. *  **scalefactor** An optional normalization scale. *  **mean**        An optional mean subtraction values. *  **See also:** connect(String, String) to know format of the descriptor. * *  If scale or mean values are specified, a final input blob is computed *  as: * [see mathematical formula in OpenCV docs]
  * *Parameter* `blob`: The blob parameter.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `scalefactor`: The scalefactor parameter.
  * *Parameter* `mean`: The mean parameter.
* `void SetParam(int layer, int numParam, Mat blob)`
  * *Summary*: Sets the new value for the learned param of the layer. *  **layer** name or id of the layer. *  **numParam** index of the layer parameter in the Layer.blobs array. *  **blob** the new value. *  **See also:** Layer.blobs *  **Note:** If shape of the new blob differs from the previous shape, *  then the following forward pass may fail.
  * *Parameter* `layer`: The layer parameter.
  * *Parameter* `numParam`: The numParam parameter.
  * *Parameter* `blob`: The blob parameter.
* `void SetParam(string layerName, int numParam, Mat blob)`
  * *Summary*: Sets the parameter blob of a layer identified by its name or output tensor name. *  **layerName** layer name (classic engine) or raw ONNX output tensor name (ENGINE_NEW). *  **numParam** index of the constant weight input to update (0 = kernel, 1 = bias, etc.). *  **blob** the new parameter value.
  * *Parameter* `layerName`: The layerName parameter.
  * *Parameter* `numParam`: The numParam parameter.
  * *Parameter* `blob`: The blob parameter.
* `Mat? GetParam(int layer, int numParam)`
  * *Summary*: Returns parameter blob of the layer. *  **layer** name or id of the layer. *  **numParam** index of the layer parameter in the Layer.blobs array. *  **See also:** Layer.blobs
  * *Parameter* `layer`: The layer parameter.
  * *Parameter* `numParam`: The numParam parameter.
  * *Returns*: The returned value.
* `Mat? GetParam(string layerName, int numParam)`
  * *Summary*: Returns the parameter blob of a layer identified by its name. Equivalent to `GetParam(int, int)` but uses a string layer name.
  * *Parameter* `layerName`: The name of the layer.
  * *Parameter* `numParam`: Index of the layer parameter in the Layer.blobs array.
  * *Returns*: The parameter Mat blob, or `null` if not found.
* `IntPtr GetUnconnectedOutLayers()`
  * *Summary*: Returns indexes of layers with unconnected outputs. * * FIXIT: Rework API to registerOutput() approach, deprecate this call
  * *Returns*: The returned value.
* `IntPtr GetUnconnectedOutLayersNames()`
  * *Summary*: Returns names of layers with unconnected outputs. * * FIXIT: Rework API to registerOutput() approach, deprecate this call
  * *Returns*: The returned value.
* `void GetLayerShapes(IntPtr netInputShapes, IntPtr netInputTypes, int layerId, IntPtr inLayerShapes, IntPtr outLayerShapes)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

* The only overload of getLayerShapes that should be kept in 5.x

  * *Parameter* `netInputShapes`: The netInputShapes parameter.
  * *Parameter* `netInputTypes`: The netInputTypes parameter.
  * *Parameter* `layerId`: The layerId parameter.
  * *Parameter* `inLayerShapes`: The inLayerShapes parameter.
  * *Parameter* `outLayerShapes`: The outLayerShapes parameter.
* `long GetFLOPS(IntPtr netInputShapes, IntPtr netInputTypes)`
  * *Summary*: Computes FLOP for whole loaded model with specified input shapes. * **netInputShapes** vector of shapes for all net inputs. * **netInputTypes** vector of types for all net inputs. * **Returns**: computed FLOP.
  * *Parameter* `netInputShapes`: The netInputShapes parameter.
  * *Parameter* `netInputTypes`: The netInputTypes parameter.
  * *Returns*: The returned value.
* `void GetLayerTypes(IntPtr layersTypes)`
  * *Summary*: Returns list of types for layer used in model. * **layersTypes** output parameter for returning types.
  * *Parameter* `layersTypes`: The layersTypes parameter.
* `int GetLayersCount(string layerType)`
  * *Summary*: Returns count of layers of specified type. * **layerType** type. * **Returns**: count of layers
  * *Parameter* `layerType`: The layerType parameter.
  * *Returns*: The returned value.
* `void GetMemoryConsumption(IntPtr netInputShapes, IntPtr netInputTypes, long weights, long blobs)`
  * *Summary*: Computes bytes number which are required to store * all weights and intermediate blobs for model. * **netInputShapes** vector of shapes for all net inputs. * **netInputTypes** vector of types for all net inputs. * **weights** output parameter to store resulting bytes for weights. * **blobs** output parameter to store resulting bytes for intermediate blobs.
  * *Parameter* `netInputShapes`: The netInputShapes parameter.
  * *Parameter* `netInputTypes`: The netInputTypes parameter.
  * *Parameter* `weights`: The weights parameter.
  * *Parameter* `blobs`: The blobs parameter.
* `void EnableFusion(bool fusion)`
  * *Summary*: Enables or disables layer fusion in the network. * **fusion** true to enable the fusion, false to disable. The fusion is enabled by default.
  * *Parameter* `fusion`: The fusion parameter.
* `void EnableWinograd(bool useWinograd)`
  * *Summary*: Enables or disables the Winograd compute branch. The Winograd compute branch can speed up * 3x3 Convolution at a small loss of accuracy. * **useWinograd** true to enable the Winograd compute branch. The default is true.
  * *Parameter* `useWinograd`: The useWinograd parameter.
* `long GetPerfProfile(IntPtr timings)`
  * *Summary*: Returns overall time for inference and timings (in ticks) for layers. * * Indexes in returned vector correspond to layers ids. Some layers can be fused with others, * in this case zero ticks count will be return for that skipped layers. Supported by DNN_BACKEND_OPENCV on DNN_TARGET_CPU only. * * **timings** vector for tick timings for all layers. **Returns**: overall ticks for model inference.
  * *Parameter* `timings`: The timings parameter.
  * *Returns*: The returned value.
* `void EnableKVCache()`
  * *Summary*: Enables KV-Cache for all AttentionOnnxI layers
* `void DisableKVCache()`
  * *Summary*: Disables KV-Cache for all AttentionOnnxI layers
* `void ResetKVCache()`
  * *Summary*: Resets KV-Cache for all AttentionOnnxI layers
* `void GetPerfProfile(IntPtr names, IntPtr timems, IntPtr counts)`
  * *Summary*: Returns profiling data captured during the last forward pass. * * Entries are sorted by time in descending order. Empty vectors are returned * if profiling is disabled (DNN_PROFILE_NONE).
  * *Parameter* `names`: The names parameter.
  * *Parameter* `timems`: The timems parameter.
  * *Parameter* `counts`: The counts parameter.
* `void PrintPerfProfile()`
  * *Summary*: Prints the profile captured during the last forward pass in a formatted table using CV_LOG_INFO. * * In DNN_PROFILE_DETAILED mode, prints per-layer label, time, and percentage. * In DNN_PROFILE_SUMMARY mode, prints per-type count, time, and percentage. * Does nothing if profiling is disabled (DNN_PROFILE_NONE) or all timings are zero.

---
### `DnnSegmentationModel`
**Inherits from**: `DnnModel`

This class represents high-level API for segmentation  models * * SegmentationModel allows to set params for preprocessing input image. * SegmentationModel creates net from file with trained weights and config, * sets preprocessing input, runs forward pass and returns the class prediction for each pixel.

#### Constructors
* `new DnnSegmentationModel(string model, string? config)`
  * *Summary*: *  Create segmentation model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.
* `new DnnSegmentationModel(DnnNet network)`
  * *Summary*: *  Create model from deep learning network.
  * *Parameter* `network`: The network parameter.

#### Methods
* `void Segment(Mat frame, Mat mask)`
  * *Summary*: Given the `input` frame, create input blob, run net *  **frame**  The input image. *  **mask** Allocated class prediction for each pixel
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `mask`: Optional operation mask.

---
### `DnnTextDetectionModel`
**Inherits from**: `DnnModel`

Base class for text detection networks

#### Methods
* `void Detect(Mat frame, IntPtr detections, IntPtr confidences)`
  * *Summary*: Performs detection * * Given the input `frame`, prepare network input, run network inference, post-process network output and return result detections. * * Each result is quadrangle's 4 points in this order: * - bottom-left * - top-left * - top-right * - bottom-right * * Use getPerspectiveTransform function to retrieve image region without perspective transformations. * * **Note:** If DL model doesn't support that kind of output then result may be derived from detectTextRectangles() output. * * **frame** The input image * **detections** array with detections' quadrangles (4 points per result) * **confidences** array with detection confidences
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `detections`: The detections parameter.
  * *Parameter* `confidences`: The confidences parameter.
* `void Detect(Mat frame, IntPtr detections)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `detections`: The detections parameter.
* `void DetectTextRectangles(Mat frame, IntPtr detections, IntPtr confidences)`
  * *Summary*: Performs detection * * Given the input `frame`, prepare network input, run network inference, post-process network output and return result detections. * * Each result is rotated rectangle. * * **Note:** Result may be inaccurate in case of strong perspective transformations. * * **frame** the input image * **detections** array with detections' RotationRect results * **confidences** array with detection confidences
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `detections`: The detections parameter.
  * *Parameter* `confidences`: The confidences parameter.
* `void DetectTextRectangles(Mat frame, IntPtr detections)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `detections`: The detections parameter.

---
### `DnnTextDetectionModelDb`
**Inherits from**: `DnnTextDetectionModel`

This class represents high-level API for text detection DL networks compatible with DB model. * * Related publications: [liao2020real] * Paper: https://arxiv.org/abs/1911.08947 * For more information about the hyper-parameters setting, please refer to https://github.com/MhLiao/DB * * Configurable parameters: * - (float) binaryThreshold - The threshold of the binary map. It is usually set to 0.3. * - (float) polygonThreshold - The threshold of text polygons. It is usually set to 0.5, 0.6, and 0.7. Default is 0.5f * - (double) unclipRatio - The unclip ratio of the detected text region, which determines the output size. It is usually set to 2.0. * - (int) maxCandidates - The max number of the output results.

#### Constructors
* `new DnnTextDetectionModelDb(DnnNet network)`
  * *Summary*: *  Create text detection algorithm from deep learning network.
  * *Parameter* `network`: The network parameter.
* `new DnnTextDetectionModelDb(string model, string? config)`
  * *Summary*: *  Create text detection model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.

#### Methods
* `DnnTextDetectionModelDb? SetBinaryThreshold(float binaryThreshold)`
  * *Summary*: Sets the threshold for the binary map. Typically set to 0.3.
  * *Parameter* `binaryThreshold`: The binary map threshold value.
  * *Returns*: The DnnTextDetectionModelDb instance for method chaining, or `null`.
* `float GetBinaryThreshold()`
  * *Summary*: Gets the current binary map threshold value.
  * *Returns*: The current binary threshold.
* `DnnTextDetectionModelDb? SetPolygonThreshold(float polygonThreshold)`
  * *Summary*: Sets the threshold for text polygons. Typically set to 0.5, 0.6, or 0.7.
  * *Parameter* `polygonThreshold`: The polygon threshold value.
  * *Returns*: The DnnTextDetectionModelDb instance for method chaining, or `null`.
* `float GetPolygonThreshold()`
  * *Summary*: Gets the current text polygon threshold value.
  * *Returns*: The current polygon threshold.
* `DnnTextDetectionModelDb? SetUnclipRatio(double unclipRatio)`
  * *Summary*: Sets the unclip ratio of the detected text region, which determines the output size. Typically set to 2.0.
  * *Parameter* `unclipRatio`: The unclip ratio value.
  * *Returns*: The DnnTextDetectionModelDb instance for method chaining, or `null`.
* `double GetUnclipRatio()`
  * *Summary*: Gets the current unclip ratio value.
  * *Returns*: The current unclip ratio.
* `DnnTextDetectionModelDb? SetMaxCandidates(int maxCandidates)`
  * *Summary*: Sets the maximum number of output text detection results.
  * *Parameter* `maxCandidates`: The maximum number of candidate results.
  * *Returns*: The DnnTextDetectionModelDb instance for method chaining, or `null`.
* `int GetMaxCandidates()`
  * *Summary*: Gets the current maximum number of output text detection results.
  * *Returns*: The current max candidates value.

---
### `DnnTextDetectionModelEast`
**Inherits from**: `DnnTextDetectionModel`

This class represents high-level API for text detection DL networks compatible with EAST model. * * Configurable parameters: * - (float) confThreshold - used to filter boxes by confidences, default: 0.5f * - (float) nmsThreshold - used in non maximum suppression, default: 0.0f

#### Constructors
* `new DnnTextDetectionModelEast(DnnNet network)`
  * *Summary*: *  Create text detection algorithm from deep learning network
  * *Parameter* `network`: The network parameter.
* `new DnnTextDetectionModelEast(string model, string? config)`
  * *Summary*: *  Create text detection model from network represented in one of the supported formats.
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.

#### Methods
* `DnnTextDetectionModelEast? SetConfidenceThreshold(float confThreshold)`
  * *Summary*: *  Set the detection confidence threshold
  * *Remarks*:

* * **Parameter** `confThreshold`:  A threshold used to filter boxes by confidences

  * *Parameter* `confThreshold`: The confThreshold parameter.
  * *Returns*: The returned value.
* `float GetConfidenceThreshold()`
  * *Summary*: *  Get the detection confidence threshold
  * *Returns*: The returned value.
* `DnnTextDetectionModelEast? SetNMSThreshold(float nmsThreshold)`
  * *Summary*: *  Set the detection NMS filter threshold
  * *Remarks*:

* * **Parameter** `nmsThreshold`:  A threshold used in non maximum suppression

  * *Parameter* `nmsThreshold`: The nmsThreshold parameter.
  * *Returns*: The returned value.
* `float GetNMSThreshold()`
  * *Summary*: *  Get the detection confidence threshold
  * *Returns*: The returned value.

---
### `DnnTextRecognitionModel`
**Inherits from**: `DnnModel`

This class represents high-level API for text recognition networks. * * TextRecognitionModel allows to set params for preprocessing input image. * TextRecognitionModel creates net from file with trained weights and config, * sets preprocessing input, runs forward pass and return recognition result. * For TextRecognitionModel, CRNN-CTC is supported.

#### Constructors
* `new DnnTextRecognitionModel(DnnNet network)`
  * *Summary*: *  Create Text Recognition model from deep learning network
  * *Parameter* `network`: The network parameter.
* `new DnnTextRecognitionModel(string model, string? config)`
  * *Summary*: *  Create text recognition model from network represented in one of the supported formats
  * *Parameter* `model`: The model parameter.
  * *Parameter* `config`: The config parameter.

#### Methods
* `DnnTextRecognitionModel? SetDecodeType(string decodeType)`
  * *Summary*: *  Set the decoding method of translating the network output into string
  * *Remarks*:

* * **Parameter** `decodeType`:  The decoding method of translating the network output into string, currently supported type:
*    - `"CTC-greedy"` greedy decoding for the output of CTC-based methods
*    - `"CTC-prefix-beam-search"` Prefix beam search decoding for the output of CTC-based methods

  * *Parameter* `decodeType`: The decodeType parameter.
  * *Returns*: The returned value.
* `string? GetDecodeType()`
  * *Summary*: *  Get the decoding method
  * *Remarks*:

**Returns**: the decoding method

  * *Returns*: The returned value.
* `DnnTextRecognitionModel? SetDecodeOptsCTCPrefixBeamSearch(int beamSize, int vocPruneSize)`
  * *Summary*: *  Set the decoding method options for `"CTC-prefix-beam-search"` decode usage
  * *Remarks*:

* * **Parameter** `beamSize`:  Beam size for search
* * **Parameter** `vocPruneSize`:  Parameter to optimize big vocabulary search,
* only take top `vocPruneSize` tokens in each search step, `vocPruneSize` <= 0 stands for disable this prune.

  * *Parameter* `beamSize`: The beamSize parameter.
  * *Parameter* `vocPruneSize`: The vocPruneSize parameter.
  * *Returns*: The returned value.
* `DnnTextRecognitionModel? SetVocabulary(IntPtr vocabulary)`
  * *Summary*: *  Set the vocabulary for recognition.
  * *Remarks*:

* * **Parameter** `vocabulary`:  the associated vocabulary of the network.

  * *Parameter* `vocabulary`: The vocabulary parameter.
  * *Returns*: The returned value.
* `IntPtr GetVocabulary()`
  * *Summary*: *  Get the vocabulary for recognition.
  * *Remarks*:

**Returns**: vocabulary the associated vocabulary

  * *Returns*: The returned value.
* `string? Recognize(Mat frame)`
  * *Summary*: *  Given the `input` frame, create input blob, run net and return recognition result
  * *Remarks*:

* * **Parameter** `frame`:  The input image
**Returns**: The text recognition result

  * *Parameter* `frame`: The frame parameter.
  * *Returns*: The returned value.
* `void Recognize(Mat frame, IntPtr roiRects, IntPtr results)`
  * *Summary*: *  Given the `input` frame, create input blob, run net and return recognition result
  * *Remarks*:

* * **Parameter** `frame`:  The input image
* * **Parameter** `roiRects`:  List of text detection regions of interest (Rect, CV_32SC4). ROIs is be cropped as the network inputs
* * **Parameter** `results`:  A set of text recognition results.

  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `roiRects`: The roiRects parameter.
  * *Parameter* `results`: The results parameter.

---
### `DnnTokenizer`
**Inherits from**: `DisposableOpenCVObject`

*  High-level tokenizer wrapper for DNN usage.

**Detailed Remarks**:
* Provides a simple API to encode and decode tokens for LLMs.
* Models are loaded via Tokenizer.load().

* 
```csharp
using var tok = DnnTokenizer.Load("/path/to/model/");
IntPtr ids = tok.Encode("hello world");
string? text = tok.Decode(ids);
```

#### Methods
* `DnnTokenizer? Load(string model_config)`
  * *Summary*: *  Load a tokenizer from a model directory.
  * *Remarks*:

* Expects the directory to contain:
*  - `config.json` with field `model_type` with value "gpt2" or "gpt4".
*  - `tokenizer.json` produced by the corresponding model family.

* The argument is a path prefix; this function concatenates file
* names directly (e.g. `model_dir` + "config.json"), so `model_dir` must
* end with an appropriate path separator.

* * **Parameter** `model_config`:   Path to config.json for model.
**Returns**: A Tokenizer ready for use. Throws OpenCVException if files are missing or `model_type` is unsupported.

  * *Parameter* `model_config`: The model_config parameter.
  * *Returns*: The returned value.
* `IntPtr Encode(string text)`
  * *Summary*: *  Encode UTF-8 text to token ids (special tokens currently disabled).
  * *Remarks*:

* Calls the underlying `CoreBPE.encode` with an empty allowed-special set.

* * **Parameter** `text`:   UTF-8 input string.
**Returns**: Vector of token ids (32-bit ids narrowed to int for convenience).

  * *Parameter* `text`: The text parameter.
  * *Returns*: The returned value.
* `string? Decode(IntPtr tokens)`
  * *Summary*: Decodes token ids back to a UTF-8 string.
  * *Parameter* `tokens`: Pointer to a vector of token ids to decode.
  * *Returns*: The decoded UTF-8 string, or `null`.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.DnnGetAvailableTargets`
**Signature**: `IntPtr DnnGetAvailableTargets(IntPtr be)`

Retrieves the list of available hardware targets for a given DNN backend.

**Parameters**:
* `be`: The be parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromTensorflow`
**Signature**: `DnnNet? DnnReadNetFromTensorflow(string model, string? config, int engine, IntPtr extraOutputs)`

Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format. * **model**  path to the .pb file with binary protobuf description of the network architecture * **config** path to the .pbtxt file that contains text graph definition in protobuf format. *               Resulting Net object is built by text graph using weights from a binary one that *               let us make it more flexible. * **engine** select DNN engine to be used. With auto selection the new engine is used. * **extraOutputs** specify model outputs explicitly, in addition to the outputs the graph analyzer finds. * Please pay attention that the new DNN does not support non-CPU back-ends for now. * **Returns**: Net object.

**Parameters**:
* `model`: The model parameter.
* `config`: The config parameter.
* `engine`: The engine parameter.
* `extraOutputs`: The extraOutputs parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromTensorflow`
**Signature**: `DnnNet? DnnReadNetFromTensorflow(IntPtr bufferModel, IntPtr bufferConfig, int engine, IntPtr extraOutputs)`

Reads a network model stored in <a href="https://www.tensorflow.org/">TensorFlow</a> framework's format. * **bufferModel** buffer containing the content of the pb file * **bufferConfig** buffer containing the content of the pbtxt file * **engine** select DNN engine to be used. With auto selection the new engine is used. * **extraOutputs** specify model outputs explicitly, in addition to the outputs the graph analyzer finds. * Please pay attention that the new DNN does not support non-CPU back-ends for now. * **Returns**: Net object.

**Parameters**:
* `bufferModel`: The bufferModel parameter.
* `bufferConfig`: The bufferConfig parameter.
* `engine`: The engine parameter.
* `extraOutputs`: The extraOutputs parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromTFLite`
**Signature**: `DnnNet? DnnReadNetFromTFLite(string model, int engine)`

Reads a network model stored in <a href="https://www.tensorflow.org/lite">TFLite</a> framework's format. * **model**  path to the .tflite file with binary flatbuffers description of the network architecture * **engine** select DNN engine to be used. With auto selection the new engine is used first and falls back to classic. * Please pay attention that the new DNN does not support non-CPU back-ends for now. * **Returns**: Net object.

**Parameters**:
* `model`: The model parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromTFLite`
**Signature**: `DnnNet? DnnReadNetFromTFLite(IntPtr bufferModel, int engine)`

Reads a network model stored in <a href="https://www.tensorflow.org/lite">TFLite</a> framework's format. * **bufferModel** buffer containing the content of the tflite file * **engine** select DNN engine to be used. With auto selection the new engine is used first and falls back to classic. * Please pay attention that the new DNN does not support non-CPU back-ends for now. * **Returns**: Net object.

**Parameters**:
* `bufferModel`: The bufferModel parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNet`
**Signature**: `DnnNet? DnnReadNet(string model, string? config, string? framework, int engine)`

*  Read deep learning network represented in one of the supported formats.

**Detailed Remarks**:
* * **Parameter** `model`:  Binary file contains trained weights. The following file
*                  extensions are expected for models from different frameworks:
*                  * `*.pb` (TensorFlow, https://www.tensorflow.org/)
*                  * `*.bin` | `*.onnx` (OpenVINO, https://software.intel.com/openvino-toolkit)
*                  * `*.onnx` (ONNX, https://onnx.ai/)
* * **Parameter** `config`:  Text file contains network configuration. It could be a
*                   file with the following extensions:
*                  * `*.pbtxt` (TensorFlow, https://www.tensorflow.org/)
*                  * `*.xml` (OpenVINO, https://software.intel.com/openvino-toolkit)
* * **Parameter** `framework`:  Explicit framework name tag to determine a format.
* * **Parameter** `engine`:  select DNN engine to be used. With auto selection the new engine is used first and falls back to classic.
* Please pay attention that the new DNN does not support non-CPU back-ends for now.
* Use ENGINE_CLASSIC if you want to use other back-ends.
* **Returns**: Net object.

* This function automatically detects an origin framework of trained model
* and calls an appropriate function such `readNetFromTensorflow`, `readNetFromONNX`.
* An order of `model` and `config` arguments does not matter.

**Parameters**:
* `model`: The model parameter.
* `config`: The config parameter.
* `framework`: The framework parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNet`
**Signature**: `DnnNet? DnnReadNet(string framework, IntPtr bufferModel, IntPtr bufferConfig, int engine)`

*  Read deep learning network represented in one of the supported formats.

**Detailed Remarks**:
* This is an overloaded member function, provided for convenience.
*          It differs from the above function only in what argument(s) it accepts.
* * **Parameter** `framework`:     Name of origin framework.
* * **Parameter** `bufferModel`:   A buffer with a content of binary file with weights
* * **Parameter** `bufferConfig`:  A buffer with a content of text file contains network configuration.
* * **Parameter** `engine`:  select DNN engine to be used. With auto selection the new engine is used first and falls back to classic.
* Please pay attention that the new DNN does not support non-CPU back-ends for now.
* Use ENGINE_CLASSIC if you want to use other back-ends.
* **Returns**: Net object.

**Parameters**:
* `framework`: The framework parameter.
* `bufferModel`: The bufferModel parameter.
* `bufferConfig`: The bufferConfig parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromModelOptimizer`
**Signature**: `DnnNet? DnnReadNetFromModelOptimizer(string xml, string? bin)`

Load a network from Intel's Model Optimizer intermediate representation. *  **xml** XML configuration file with network's topology. *  **bin** Binary file with trained weights. *  **Returns**: Net object. *  Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine *  backend.

**Parameters**:
* `xml`: The xml parameter.
* `bin`: The bin parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromModelOptimizer`
**Signature**: `DnnNet? DnnReadNetFromModelOptimizer(IntPtr bufferModelConfig, IntPtr bufferWeights)`

Load a network from Intel's Model Optimizer intermediate representation. *  **bufferModelConfig** Buffer contains XML configuration with network's topology. *  **bufferWeights** Buffer contains binary data with trained weights. *  **Returns**: Net object. *  Networks imported from Intel's Model Optimizer are launched in Intel's Inference Engine *  backend.

**Parameters**:
* `bufferModelConfig`: The bufferModelConfig parameter.
* `bufferWeights`: The bufferWeights parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromONNX`
**Signature**: `DnnNet? DnnReadNetFromONNX(string onnxFile, int engine)`

Reads a network model <a href="https://onnx.ai/">ONNX</a>. *  **onnxFile** path to the .onnx file with text description of the network architecture. *  **engine** select DNN engine to be used. With auto selection the new engine is used first and falls back to classic. *  Please pay attention that the new DNN does not support non-CPU back-ends for now. *  **Returns**: Network object that ready to do forward, throw an exception in failure cases.

**Parameters**:
* `onnxFile`: The onnxFile parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadNetFromONNX`
**Signature**: `DnnNet? DnnReadNetFromONNX(IntPtr buffer, int engine)`

Reads a network model from <a href="https://onnx.ai/">ONNX</a> *         in-memory buffer. *  **buffer** in-memory buffer that stores the ONNX model bytes. *  **engine** select DNN engine to be used. With auto selection the new engine is used first and falls back to classic. *  Please pay attention that the new DNN does not support non-CPU back-ends for now. *  **Returns**: Network object that ready to do forward, throw an exception *        in failure cases.

**Parameters**:
* `buffer`: The buffer parameter.
* `engine`: The engine parameter.

**Returns**: The returned value.

---
### `Cv2.DnnReadTensorFromONNX`
**Signature**: `Mat? DnnReadTensorFromONNX(string path)`

Creates blob from .pb file. *  **path** to the .pb file with input tensor. *  **Returns**: Mat.

**Parameters**:
* `path`: The path parameter.

**Returns**: The returned value.

---
### `Cv2.DnnBlobFromImage`
**Signature**: `Mat? DnnBlobFromImage(Mat image, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop, int ddepth)`

Creates 4-dimensional blob from image. Optionally resizes and crops `image` from center, *  subtract `mean` values, scales values by `scalefactor`, swap Blue and Red channels. *  **image** input image (with 1-, 3- or 4-channels). *  **scalefactor** multiplier for `images` values. *  **size** spatial size for output image *  **mean** scalar with mean values which are subtracted from channels. Values are intended *  to be in (mean-R, mean-G, mean-B) order if `image` has BGR ordering and `swapRB` is true. *  **swapRB** flag which indicates that swap first and last channels *  in 3-channel image is necessary. *  **crop** flag which indicates whether image will be cropped after resize or not *  **ddepth** Depth of output blob. Choose CV_32F or CV_8U. *  if `crop` is true, input image is resized so one side after resize is equal to corresponding *  dimension in `size` and another one is equal or larger. Then, crop from the center is performed. *  If `crop` is false, direct resize without cropping and preserving aspect ratio is performed. *  **Returns**: 4-dimensional Mat with NCHW dimensions order. * * **Note:** * The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.

**Parameters**:
* `image`: Input image.
* `scalefactor`: The scalefactor parameter.
* `size`: The size parameter.
* `mean`: The mean parameter.
* `swapRB`: The swapRB parameter.
* `crop`: The crop parameter.
* `ddepth`: The ddepth parameter.

**Returns**: The returned value.

---
### `Cv2.DnnBlobFromImages`
**Signature**: `Mat? DnnBlobFromImages(IntPtr images, double scalefactor, Size size, Scalar mean, bool swapRB, bool crop, int ddepth)`

Creates 4-dimensional blob from series of images. Optionally resizes and *  crops `images` from center, subtract `mean` values, scales values by `scalefactor`, *  swap Blue and Red channels. *  **images** input images (all with 1-, 3- or 4-channels). *  **size** spatial size for output image *  **mean** scalar with mean values which are subtracted from channels. Values are intended *  to be in (mean-R, mean-G, mean-B) order if `image` has BGR ordering and `swapRB` is true. *  **scalefactor** multiplier for `images` values. *  **swapRB** flag which indicates that swap first and last channels *  in 3-channel image is necessary. *  **crop** flag which indicates whether image will be cropped after resize or not *  **ddepth** Depth of output blob. Choose CV_32F or CV_8U. *  if `crop` is true, input image is resized so one side after resize is equal to corresponding *  dimension in `size` and another one is equal or larger. Then, crop from the center is performed. *  If `crop` is false, direct resize without cropping and preserving aspect ratio is performed. *  **Returns**: 4-dimensional Mat with NCHW dimensions order. * * **Note:** * The order and usage of `scalefactor` and `mean` are (input - mean) * scalefactor.

**Parameters**:
* `images`: The images parameter.
* `scalefactor`: The scalefactor parameter.
* `size`: The size parameter.
* `mean`: The mean parameter.
* `swapRB`: The swapRB parameter.
* `crop`: The crop parameter.
* `ddepth`: The ddepth parameter.

**Returns**: The returned value.

---
### `Cv2.DnnBlobFromImageWithParams`
**Signature**: `Mat? DnnBlobFromImageWithParams(Mat image, DnnImage2BlobParams? param)`

Creates 4-dimensional blob from image with given params. * *  This function is an extension of `blobFromImage` to meet more image preprocess needs. *  Given input image and preprocessing parameters, and function outputs the blob. * *  **image** input image (all with 1-, 3- or 4-channels). *  **param** struct of Image2BlobParams, contains all parameters needed by processing of image to blob. **Returns**: 4-dimensional Mat.

**Parameters**:
* `image`: Input image.
* `param`: The param parameter.

**Returns**: The returned value.

---
### `Cv2.DnnBlobFromImageWithParams`
**Signature**: `void DnnBlobFromImageWithParams(Mat image, Mat blob, DnnImage2BlobParams? param)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `image`: Input image.
* `blob`: The blob parameter.
* `param`: The param parameter.

---
### `Cv2.DnnBlobFromImagesWithParams`
**Signature**: `Mat? DnnBlobFromImagesWithParams(IntPtr images, DnnImage2BlobParams? param)`

Creates 4-dimensional blob from series of images with given params. * *  This function is an extension of `blobFromImages` to meet more image preprocess needs. *  Given input image and preprocessing parameters, and function outputs the blob. * *  **images** input image (all with 1-, 3- or 4-channels). *  **param** struct of Image2BlobParams, contains all parameters needed by processing of image to blob. *  **Returns**: 4-dimensional Mat.

**Parameters**:
* `images`: The images parameter.
* `param`: The param parameter.

**Returns**: The returned value.

---
### `Cv2.DnnBlobFromImagesWithParams`
**Signature**: `void DnnBlobFromImagesWithParams(IntPtr images, Mat blob, DnnImage2BlobParams? param)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `images`: The images parameter.
* `blob`: The blob parameter.
* `param`: The param parameter.

---
### `Cv2.DnnImagesFromBlob`
**Signature**: `void DnnImagesFromBlob(Mat blob_, IntPtr images_)`

Parse a 4D blob and output the images it contains as 2D arrays through a simpler data structure *  (Mat[]). *  **blob_** 4 dimensional array (images, channels, height, width) in floating point precision (CV_32F) from *  which you would like to extract the images. *  **images_** array of 2D Mat containing the images extracted from the blob in floating point precision *  (CV_32F). They are non normalized neither mean added. The number of returned images equals the first dimension *  of the blob (batch size). Every image has a number of channels equals to the second dimension of the blob (depth).

**Parameters**:
* `blob_`: The blob_ parameter.
* `images_`: The images_ parameter.

---
### `Cv2.DnnWriteTextGraph`
**Signature**: `void DnnWriteTextGraph(string model, string output)`

Create a text representation for a binary network stored in protocol buffer format. *  **model**  A path to binary network. *  **output** A path to output text file to be created. * *  **Note:** To reduce output file size, trained weights are not included.

**Parameters**:
* `model`: The model parameter.
* `output`: The output parameter.

---
### `Cv2.DnnNMSBoxes`
**Signature**: `void DnnNMSBoxes(IntPtr bboxes, IntPtr scores, float score_threshold, float nms_threshold, IntPtr indices, float eta, int top_k)`

Performs non maximum suppression given boxes and corresponding scores.

**Detailed Remarks**:
* * **Parameter** `bboxes`:  a set of bounding boxes to apply NMS.
* * **Parameter** `scores`:  a set of corresponding confidences.
* * **Parameter** `score_threshold`:  a threshold used to filter boxes by score.
* * **Parameter** `nms_threshold`:  a threshold used in non maximum suppression.
* * **Parameter** `indices`:  the kept indices of bboxes after NMS.
* * **Parameter** `eta`:  a coefficient in adaptive threshold formula: formula.
* * **Parameter** `top_k`:  if `>0`, keep at most `top_k` picked indices.

**Parameters**:
* `bboxes`: The bboxes parameter.
* `scores`: The scores parameter.
* `score_threshold`: The score_threshold parameter.
* `nms_threshold`: The nms_threshold parameter.
* `indices`: The indices parameter.
* `eta`: The eta parameter.
* `top_k`: The top_k parameter.

---
### `Cv2.DnnNMSBoxesBatched`
**Signature**: `void DnnNMSBoxesBatched(IntPtr bboxes, IntPtr scores, IntPtr class_ids, float score_threshold, float nms_threshold, IntPtr indices, float eta, int top_k)`

Performs batched non maximum suppression on given boxes and corresponding scores across different classes.

**Detailed Remarks**:
* * **Parameter** `bboxes`:  a set of bounding boxes to apply NMS.
* * **Parameter** `scores`:  a set of corresponding confidences.
* * **Parameter** `class_ids`:  a set of corresponding class ids. Ids are integer and usually start from 0.
* * **Parameter** `score_threshold`:  a threshold used to filter boxes by score.
* * **Parameter** `nms_threshold`:  a threshold used in non maximum suppression.
* * **Parameter** `indices`:  the kept indices of bboxes after NMS.
* * **Parameter** `eta`:  a coefficient in adaptive threshold formula: formula.
* * **Parameter** `top_k`:  if `>0`, keep at most `top_k` picked indices.

**Parameters**:
* `bboxes`: The bboxes parameter.
* `scores`: The scores parameter.
* `class_ids`: The class_ids parameter.
* `score_threshold`: The score_threshold parameter.
* `nms_threshold`: The nms_threshold parameter.
* `indices`: The indices parameter.
* `eta`: The eta parameter.
* `top_k`: The top_k parameter.

---
### `Cv2.DnnSoftNMSBoxes`
**Signature**: `void DnnSoftNMSBoxes(IntPtr bboxes, IntPtr scores, IntPtr updated_scores, float score_threshold, float nms_threshold, IntPtr indices, long top_k, float sigma, DnnSoftNMSMethod method)`

Performs soft non maximum suppression given boxes and corresponding scores. * Reference: https://arxiv.org/abs/1704.04503 * **bboxes** a set of bounding boxes to apply Soft NMS. * **scores** a set of corresponding confidences. * **updated_scores** a set of corresponding updated confidences. * **score_threshold** a threshold used to filter boxes by score. * **nms_threshold** a threshold used in non maximum suppression. * **indices** the kept indices of bboxes after NMS. * **top_k** keep at most `top_k` picked indices. * **sigma** parameter of Gaussian weighting. * **method** Gaussian or linear. * **See also:** SoftNMSMethod

**Parameters**:
* `bboxes`: The bboxes parameter.
* `scores`: The scores parameter.
* `updated_scores`: The updated_scores parameter.
* `score_threshold`: The score_threshold parameter.
* `nms_threshold`: The nms_threshold parameter.
* `indices`: The indices parameter.
* `top_k`: The top_k parameter.
* `sigma`: The sigma parameter.
* `method`: The method parameter.

---
### `Cv2.DnnGetInferenceEngineBackendType`
**Signature**: `string? DnnGetInferenceEngineBackendType()`

Returns Inference Engine internal backend API. * * See values of `CV_DNN_BACKEND_INFERENCE_ENGINE_*` macros. * * `OPENCV_DNN_BACKEND_INFERENCE_ENGINE_TYPE` runtime parameter (environment variable) is ignored since 4.6.0. * * *(Deprecated)*

**Returns**: The returned value.

---
### `Cv2.DnnSetInferenceEngineBackendType`
**Signature**: `string? DnnSetInferenceEngineBackendType(string newBackendType)`

Specify Inference Engine internal backend API. * * See values of `CV_DNN_BACKEND_INFERENCE_ENGINE_*` macros. * * **Returns**: previous value of internal backend API * * *(Deprecated)*

**Parameters**:
* `newBackendType`: The newBackendType parameter.

**Returns**: The returned value.

---
### `Cv2.DnnResetMyriadDevice`
**Signature**: `void DnnResetMyriadDevice()`

Release a Myriad device (binded by OpenCV). * * Single Myriad device cannot be shared across multiple processes which uses * Inference Engine's Myriad plugin.

---
### `Cv2.DnnGetInferenceEngineVPUType`
**Signature**: `string? DnnGetInferenceEngineVPUType()`

Returns Inference Engine VPU type. * * See values of `CV_DNN_INFERENCE_ENGINE_VPU_TYPE_*` macros.

**Returns**: The returned value.

---
### `Cv2.DnnGetInferenceEngineCPUType`
**Signature**: `string? DnnGetInferenceEngineCPUType()`

Returns Inference Engine CPU type. * * Specify OpenVINO plugin: CPU or ARM.

**Returns**: The returned value.

---
### `Cv2.DnnReleaseHDDLPlugin`
**Signature**: `void DnnReleaseHDDLPlugin()`

Release a HDDL plugin.

---
## 🔢 Enumerations

### `DnnActivationType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Mish`** | `unchecked((int)(0 + 1))` | Mish |
| **`Swish`** | `unchecked((int)(0 + 2))` | Swish |
| **`Sigmoid`** | `unchecked((int)(0 + 3))` | Sigmoid |
| **`Tanh`** | `unchecked((int)(0 + 4))` | Tanh |
| **`Elu`** | `unchecked((int)(0 + 5))` | Elu |
| **`Hardswish`** | `unchecked((int)(0 + 6))` | Hardswish |
| **`Hardsigmoid`** | `unchecked((int)(0 + 7))` | Hardsigmoid |
| **`Gelu`** | `unchecked((int)(0 + 8))` | Gelu |
| **`GeluApprox`** | `unchecked((int)(0 + 9))` | GeluApprox |
| **`Relu`** | `unchecked((int)(0 + 10))` | Relu |
| **`Clip`** | `unchecked((int)(0 + 11))` | Clip |

---
### `DnnArgKind`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Empty`** | `0` | Empty |
| **`Const`** | `1` | Const |
| **`Input`** | `2` | Input |
| **`Output`** | `3` | Output |
| **`Temp`** | `4` | Temp |
| **`Pattern`** | `5` | Pattern |

---
### `DnnAutoPadding`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`SameUpper`** | `1` | SameUpper |
| **`SameLower`** | `2` | SameLower |
| **`Valid`** | `3` | Valid |

---
### `DnnBackend`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `0` | Default |
| **`InferenceEngine`** | `2` | InferenceEngine |
| **`Opencv`** | `unchecked((int)(2 + 1))` | Opencv |
| **`Vkcom`** | `unchecked((int)(2 + 2))` | Vkcom |
| **`Cuda`** | `unchecked((int)(2 + 3))` | Cuda |
| **`Webnn`** | `unchecked((int)(2 + 4))` | Webnn |
| **`Timvx`** | `unchecked((int)(2 + 5))` | Timvx |
| **`Cann`** | `unchecked((int)(2 + 6))` | Cann |

---
### `DnnEngineType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Classic`** | `1` | Classic |
| **`New`** | `2` | New |
| **`Auto`** | `3` | Auto |
| **`Ort`** | `4` | Ort |

---
### `DnnImagePaddingMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Null`** | `0` | Null |
| **`CropCenter`** | `1` | CropCenter |
| **`Letterbox`** | `2` | Letterbox |

---
### `DnnLossReduction`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Mean`** | `1` | Mean |
| **`Sum`** | `2` | Sum |

---
### `DnnModelFormat`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Generic`** | `0` | Generic |
| **`Onnx`** | `1` | Onnx |
| **`Tf`** | `2` | Tf |
| **`Tflite`** | `3` | Tflite |

---
### `DnnNaryEltwiseLayerOperation`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`And`** | `0` | And |
| **`Equal`** | `unchecked((int)(0 + 1))` | Equal |
| **`Greater`** | `unchecked((int)(0 + 2))` | Greater |
| **`GreaterEqual`** | `unchecked((int)(0 + 3))` | GreaterEqual |
| **`Less`** | `unchecked((int)(0 + 4))` | Less |
| **`LessEqual`** | `unchecked((int)(0 + 5))` | LessEqual |
| **`Or`** | `unchecked((int)(0 + 6))` | Or |
| **`Pow`** | `unchecked((int)(0 + 7))` | Pow |
| **`Xor`** | `unchecked((int)(0 + 8))` | Xor |
| **`Bitshift`** | `unchecked((int)(0 + 9))` | Bitshift |
| **`Max`** | `unchecked((int)(0 + 10))` | Max |
| **`Mean`** | `unchecked((int)(0 + 11))` | Mean |
| **`Min`** | `unchecked((int)(0 + 12))` | Min |
| **`Mod`** | `unchecked((int)(0 + 13))` | Mod |
| **`Fmod`** | `unchecked((int)(0 + 14))` | Fmod |
| **`Prod`** | `unchecked((int)(0 + 15))` | Prod |
| **`Sub`** | `unchecked((int)(0 + 16))` | Sub |
| **`Sum`** | `unchecked((int)(0 + 17))` | Sum |
| **`Add`** | `unchecked((int)(0 + 18))` | Add |
| **`Div`** | `unchecked((int)(0 + 19))` | Div |
| **`Where`** | `unchecked((int)(0 + 20))` | Where |
| **`BitwiseAnd`** | `unchecked((int)(0 + 21))` | BitwiseAnd |
| **`BitwiseOr`** | `unchecked((int)(0 + 22))` | BitwiseOr |
| **`BitwiseXor`** | `unchecked((int)(0 + 23))` | BitwiseXor |

---
### `DnnProfilingMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Summary`** | `1` | Summary |
| **`Detailed`** | `2` | Detailed |

---
### `DnnReduce2LayerReduceType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Max`** | `0` | Max |
| **`Min`** | `1` | Min |
| **`Mean`** | `2` | Mean |
| **`Sum`** | `3` | Sum |
| **`L1`** | `4` | L1 |
| **`L2`** | `5` | L2 |
| **`Prod`** | `6` | Prod |
| **`SumSquare`** | `7` | SumSquare |
| **`LogSum`** | `8` | LogSum |
| **`LogSumExp`** | `9` | LogSumExp |

---
### `DnnSoftNMSMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Linear`** | `1` | Linear |
| **`Gaussian`** | `2` | Gaussian |

---
### `DnnTarget`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Cpu`** | `0` | Cpu |
| **`Opencl`** | `unchecked((int)(0 + 1))` | Opencl |
| **`OpenclFp16`** | `unchecked((int)(0 + 2))` | OpenclFp16 |
| **`Myriad`** | `unchecked((int)(0 + 3))` | Myriad |
| **`Vulkan`** | `unchecked((int)(0 + 4))` | Vulkan |
| **`Fpga`** | `unchecked((int)(0 + 5))` | Fpga |
| **`Cuda`** | `unchecked((int)(0 + 6))` | Cuda |
| **`CudaFp16`** | `unchecked((int)(0 + 7))` | CudaFp16 |
| **`Hddl`** | `unchecked((int)(0 + 8))` | Hddl |
| **`Npu`** | `unchecked((int)(0 + 9))` | Npu |
| **`CpuFp16`** | `unchecked((int)(0 + 10))` | CpuFp16 |

---
### `DnnTracingMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`All`** | `1` | All |
| **`Op`** | `2` | Op |

---

</div>