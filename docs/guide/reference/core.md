# CORE Module API Reference

Complete documentation for the **CORE** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Core Documentation](https://docs.opencv.org/5.x/main_modules/core.html).

---
<div v-pre>

## 📦 Classes and Structs

### `Point`
Represents a Point instance.

#### Constructors
* `new Point(int x, int y)`
  * *Summary*: X coordinate.
  * *Parameter* `x`: X coordinate.
  * *Parameter* `y`: Y coordinate.

---
### `Point2f`
Represents a Point2f instance.

#### Constructors
* `new Point2f(float x, float y)`
  * *Summary*: X coordinate.
  * *Parameter* `x`: X coordinate.
  * *Parameter* `y`: Y coordinate.

---
### `Size`
Represents a Size instance.

#### Constructors
* `new Size(int width, int height)`
  * *Summary*: Width of the size.
  * *Parameter* `width`: Width value.
  * *Parameter* `height`: Height value.

---
### `Size2F`
Represents a Size2F instance.

#### Constructors
* `new Size2F(float width, float height)`
  * *Summary*: Width of the size.
  * *Parameter* `width`: Width value.
  * *Parameter* `height`: Height value.

---
### `Rect`
Represents a Rect instance.

#### Constructors
* `new Rect(int x, int y, int w, int h)`
  * *Summary*: X coordinate of the top-left corner.
  * *Parameter* `x`: X coordinate.
  * *Parameter* `y`: Y coordinate.
  * *Parameter* `w`: Width.
  * *Parameter* `h`: Height.

---
### `Rect2F`
Represents a Rect2F instance.

#### Constructors
* `new Rect2F(float x, float y, float w, float h)`
  * *Summary*: X coordinate of the top-left corner.
  * *Parameter* `x`: X coordinate.
  * *Parameter* `y`: Y coordinate.
  * *Parameter* `w`: Width.
  * *Parameter* `h`: Height.

---
### `Range`
Represents a Range instance.

#### Constructors
* `new Range(int start, int end)`
  * *Summary*: Start of the range (inclusive).
  * *Parameter* `start`: Start of range (inclusive).
  * *Parameter* `end`: End of range (exclusive).

---
### `Scalar`
Represents a Scalar instance.

#### Constructors
* `new Scalar(double v0, double v1 = 0, double v2 = 0, double v3 = 0)`
  * *Summary*: First channel value (e.g., Blue in BGR).
  * *Parameter* `v0`: First channel value.
  * *Parameter* `v1`: Second channel value (default 0).
  * *Parameter* `v2`: Third channel value (default 0).
  * *Parameter* `v3`: Fourth channel value (default 0).

---
### `TermCriteria`
Represents a TermCriteria instance.

#### Constructors
* `new TermCriteria(int type, int maxCount, double epsilon)`
  * *Summary*: Termination criteria type (max iterations, epsilon, or both).
  * *Parameter* `type`: Criteria type flags.
  * *Parameter* `maxCount`: Maximum iteration count.
  * *Parameter* `epsilon`: Desired accuracy threshold.

---
### `Algorithm`
**Inherits from**: `DisposableOpenCVObject`

This is a base class for all more or less complex algorithms in OpenCV

**Detailed Remarks**:
especially for classes of algorithms, for which there can be multiple implementations. The examples
are stereo correspondence (for which there are algorithms like block matching, semi-global block
matching, graph-cut etc.), background subtraction (which can be done using mixture-of-gaussians
models, codebook-based algorithm etc.), optical flow (block matching, Lucas-Kanade, Horn-Schunck
etc.).
Here is example of SimpleBlobDetector use in your application via Algorithm interface:

#### Methods
* `void Clear()`
  * *Summary*: Clears the algorithm state
* `void Write(FileStorage fs)`
  * *Summary*: Stores algorithm parameters in a file storage
  * *Parameter* `fs`: The fs parameter.
* `void Write(FileStorage fs, string name)`
  * *Summary*: *
  * *Parameter* `fs`: The fs parameter.
  * *Parameter* `name`: The name parameter.
* `void Read(FileNode fn)`
  * *Summary*: Reads algorithm parameters from a file storage
  * *Parameter* `fn`: The fn parameter.
* `bool Empty()`
  * *Summary*: Returns true if the Algorithm is empty (e.g. in the very beginning or after unsuccessful read
  * *Returns*: The returned value.
* `void Save(string filename)`
  * *Summary*: Saves the algorithm to a file.
  * *Remarks*:

In order to make this method work, the derived class must implement Algorithm.Write(FileStorage& fs).

  * *Parameter* `filename`: Path to the file.
* `string? GetDefaultName()`
  * *Summary*: Returns the algorithm string identifier.
  * *Remarks*:

This string is used as top level xml/yml node tag when the object is saved to a file or string.

  * *Returns*: The returned value.

---
### `AsyncArray`
**Inherits from**: `DisposableOpenCVObject`

Returns result of asynchronous operations

**Detailed Remarks**:
Object has attached asynchronous state.
Assignment operator doesn't clone asynchronous state (it is shared between all instances).
Result can be fetched via get() method only once.

#### Constructors
* `new AsyncArray()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `void Release()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `void Get(Mat dst)`
  * *Summary*: Fetch the result.
  * *Remarks*:

.: info Note
Result or stored exception can be fetched only once.
.:

  * *Parameter* `dst`: Destination matrix or image (output).
* `bool Get(Mat dst, double timeoutNs)`
  * *Summary*: Retrieving the result with timeout
  * *Remarks*:

.: info Note
Result or stored exception can be fetched only once.
.:

  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `timeoutNs`: The timeoutNs parameter.
  * *Returns*: s true if result is ready, false if the timeout has expired
* `bool WaitFor(double timeoutNs)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `timeoutNs`: The timeoutNs parameter.
  * *Returns*: The returned value.
* `bool Valid()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `DMatch`
**Inherits from**: `DisposableOpenCVObject`

Class for matching keypoint descriptors

**Detailed Remarks**:
query descriptor index, train descriptor index, train image index, and distance between
descriptors.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`QueryIdx`** | `int` | Gets or sets the queryIdx property. |
| **`TrainIdx`** | `int` | Gets or sets the trainIdx property. |
| **`ImgIdx`** | `int` | Gets or sets the imgIdx property. |
| **`Distance`** | `float` | Gets or sets the distance property. |

#### Constructors
* `new DMatch()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new DMatch(int _queryIdx, int _trainIdx, float _distance)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_queryIdx`: The _queryIdx parameter.
  * *Parameter* `_trainIdx`: The _trainIdx parameter.
  * *Parameter* `_distance`: The _distance parameter.
* `new DMatch(int _queryIdx, int _trainIdx, int _imgIdx, float _distance)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `_queryIdx`: The _queryIdx parameter.
  * *Parameter* `_trainIdx`: The _trainIdx parameter.
  * *Parameter* `_imgIdx`: The _imgIdx parameter.
  * *Parameter* `_distance`: The _distance parameter.

---
### `FileNode`
**Inherits from**: `DisposableOpenCVObject`

File Storage Node class.

**Detailed Remarks**:
The node is used to store each and every element of the file storage opened for reading. When
XML/YAML file is read, it is first parsed and stored in the memory as a hierarchical collection of
nodes. Each node can be a "leaf" that is contain a single number or a string, or be a collection of
other nodes. There can be named collections (mappings) where each element has a name and it is
accessed by a name, and ordered collections (sequences) where elements do not have names but rather
accessed by index. Type of the file node can be determined using FileNode.Type method.
Note that file nodes are only used for navigating file storages opened for reading. When a file
storage is opened for writing, no data is stored in memory after it is written.

#### Constructors
* `new FileNode()`
  * *Summary*: The constructors.

#### Methods
* `FileNode? OperatorGet(string nodename)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `nodename`: Name of an element in the mapping node.
  * *Returns*: The returned value.
* `FileNode? OperatorGet(int i)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `i`: Index of an element in the sequence node.
  * *Returns*: The returned value.
* `IntPtr Keys()`
  * *Summary*: Returns keys of a mapping node.
  * *Returns*: s Keys of a mapping node.
* `int Type()`
  * *Summary*: Returns type of the node.
  * *Returns*: s Type of the node. See FileNode.Type
* `bool Empty()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsNone()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsSeq()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsMap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsInt()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsReal()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsString()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsNamed()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? Name()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Size()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long RawSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double Real()`
  * *Summary*: Internal method used when reading FileStorage.
  * *Remarks*:

Sets the type (int, real or string) and value of the previously created node.

  * *Returns*: The returned value.
* `string? String()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Mat? Mat()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `FileStorage`
**Inherits from**: `DisposableOpenCVObject`

XML/YAML/JSON file storage class that encapsulates all the information necessary for writing or reading data to/from a file.

#### Constructors
* `new FileStorage()`
  * *Summary*: The constructors.
* `new FileStorage(string filename, int flags, string? encoding)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `filename`: Path to the file.
  * *Parameter* `flags`: Operation flags.
  * *Parameter* `encoding`: The encoding parameter.

#### Methods
* `bool Open(string filename, int flags, string? encoding)`
  * *Summary*: Opens a file.
  * *Remarks*:

See description of parameters in FileStorage constructor. The method calls FileStorage.Release
before opening the file.

  * *Parameter* `filename`: Name of the file to open or the text string to read the data from. Extension of the file (.xml, .yml/.yaml or .json) determines its format (XML, YAML or JSON respectively). Also you can append .gz to work with compressed files, for example myHugeMatrix.xml.gz. You can also specify a compression level from 0 to 9 by appending it to the extension (e.g. ".gz0" for no compression, ".gz9" for high compression). The last digit will be truncated internally to write/read. (e.g. If "a.xml.gz9" is specified, "a.xml.gz" is used for the actual file name.) If both FileStorageMode.Write and FileStorageMode.Memory flags are specified, source is used just to specify the output file format (e.g. mydata.xml, .yml etc.). A file name can also contain parameters. You can use this format, "*?base64" (e.g. "file.json?base64" (case sensitive)), as an alternative to FileStorageMode.Base64 flag.
  * *Parameter* `flags`: Mode of operation. One of FileStorageMode
  * *Parameter* `encoding`: Encoding of the file. Note that UTF-16 XML encoding is not supported currently and you should use 8-bit encoding instead of it.
  * *Returns*: The returned value.
* `bool IsOpened()`
  * *Summary*: Checks whether the file is opened.
  * *Returns*: s true if the object is associated with the current file and false otherwise. It is a good practice to call this method after you tried to open a file.
* `void Release()`
  * *Summary*: Closes the file and releases all the memory buffers.
  * *Remarks*:

Call this method after all I/O operations with the storage are finished.

* `string? ReleaseAndGetString()`
  * *Summary*: Closes the file and releases all the memory buffers.
  * *Remarks*:

Call this method after all I/O operations with the storage are finished. If the storage was
opened for writing data and FileStorageMode.Write was specified

  * *Returns*: The returned value.
* `FileNode? GetFirstTopLevelNode()`
  * *Summary*: Returns the first element of the top-level mapping.
  * *Returns*: s The first element of the top-level mapping.
* `FileNode? Root(int streamidx)`
  * *Summary*: Returns the top-level mapping
  * *Parameter* `streamidx`: Zero-based index of the stream. In most cases there is only one stream in the file. However, YAML supports multiple streams and so there can be several.
  * *Returns*: s The top-level mapping.
* `FileNode? OperatorGet(string nodename)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `nodename`: The nodename parameter.
  * *Returns*: The returned value.
* `void Write(string name, int val)`
  * *Summary*: *  Simplified writing API to use with bindings.
  * *Remarks*:

* * **Parameter** `name`:  Name of the written object. When writing to sequences (a.k.a. "arrays"), pass an empty string.
* * **Parameter** `val`:  Value of the written object.

  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, bool val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, long val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, double val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, string val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, Mat val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void Write(string name, IntPtr val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `name`: The name parameter.
  * *Parameter* `val`: The val parameter.
* `void WriteComment(string comment, bool append)`
  * *Summary*: Writes a comment.
  * *Remarks*:

The function writes a comment into file storage. The comments are skipped when the storage is read.

  * *Parameter* `comment`: The written comment, single-line or multi-line
  * *Parameter* `append`: If true, the function tries to put the comment at the end of current line. Else if the comment is multi-line, or if it does not fit at the end of the current line, the comment starts a new line.
* `void StartWriteStruct(string name, int flags, string? typeName)`
  * *Summary*: Starts to write a nested structure (sequence or a mapping).
  * *Parameter* `name`: name of the structure. When writing to sequences (a.k.a. "arrays"), pass an empty string.
  * *Parameter* `flags`: type of the structure (FileNodeType.Map or FileNodeType.Seq (both with optional FileNodeType.Flow)).
  * *Parameter* `typeName`: optional name of the type you store. The effect of setting this depends on the storage format. I.e. if the format has a specification for storing type information, this parameter is used.
* `void EndWriteStruct()`
  * *Summary*: Finishes writing nested structure (should pair startWriteStruct())
* `int GetFormat()`
  * *Summary*: Returns the current format. * **Returns**: The current format, see FileStorageMode
  * *Returns*: The returned value.

---
### `KeyPoint`
**Inherits from**: `DisposableOpenCVObject`

Data structure for salient point detectors.

**Detailed Remarks**:
The class instance stores a keypoint, i.e. a point feature found by one of many available keypoint
detectors, such as Harris corner detector, `FAST`, StarDetector, SURF, SIFT etc.
The keypoint is characterized by the 2D position, scale (proportional to the diameter of the
neighborhood that needs to be taken into account), orientation and some other parameters. The
keypoint neighborhood is then analyzed by another algorithm that builds a descriptor (usually
represented as a feature vector). The keypoints representing the same object in different images
can then be matched using KDTree or another method.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Pt`** | `Point2f` | Gets or sets the pt property. |
| **`Size`** | `float` | Gets or sets the size property. |
| **`Angle`** | `float` | Gets or sets the angle property. |
| **`Response`** | `float` | Gets or sets the response property. |
| **`Octave`** | `int` | Gets or sets the octave property. |
| **`ClassId`** | `int` | Gets or sets the class_id property. |

#### Constructors
* `new KeyPoint()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new KeyPoint(float x, float y, float size, float angle, float response, int octave, int class_id)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `x`: x-coordinate of the keypoint
  * *Parameter* `y`: y-coordinate of the keypoint
  * *Parameter* `size`: keypoint diameter
  * *Parameter* `angle`: keypoint orientation
  * *Parameter* `response`: keypoint detector response on the keypoint (that is, strength of the keypoint)
  * *Parameter* `octave`: pyramid octave in which the keypoint has been detected
  * *Parameter* `class_id`: object id

#### Methods
* `void Convert(IntPtr keypoints, IntPtr points2f, IntPtr keypointIndexes)`
  * *Summary*: This method converts vector of keypoints to vector of points or the reverse, where each keypoint is
  * *Remarks*:

assigned the same size and the same orientation.

  * *Parameter* `keypoints`: Keypoints obtained from any feature detection algorithm like SIFT/SURF/ORB
  * *Parameter* `points2f`: Array of (x,y) coordinates of each keypoint
  * *Parameter* `keypointIndexes`: Array of indexes of keypoints to be converted to points. (Acts like a mask to convert only specified keypoints)
* `void Convert(IntPtr points2f, IntPtr keypoints, float size, float response, int octave, int class_id)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `points2f`: Array of (x,y) coordinates of each keypoint
  * *Parameter* `keypoints`: Keypoints obtained from any feature detection algorithm like SIFT/SURF/ORB
  * *Parameter* `size`: keypoint diameter
  * *Parameter* `response`: keypoint detector response on the keypoint (that is, strength of the keypoint)
  * *Parameter* `octave`: pyramid octave in which the keypoint has been detected
  * *Parameter* `class_id`: object id
* `float Overlap(KeyPoint kp1, KeyPoint kp2)`
  * *Summary*: This method computes overlap for pair of keypoints. Overlap is the ratio between area of keypoint
  * *Remarks*:

regions' intersection and area of keypoint regions' union (considering keypoint region as circle).
If they don't overlap, we get zero. If they coincide at same location with same size, we get 1.

  * *Parameter* `kp1`: First keypoint
  * *Parameter* `kp2`: Second keypoint
  * *Returns*: The returned value.

---
### `Mat`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Rows`** | `int` | Gets the rows property. |
| **`Cols`** | `int` | Gets the cols property. |
| **`Data`** | `IntPtr` | Gets the data property. |
| **`Step`** | `long` | Gets the step property. |

#### Constructors
* `new Mat()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new Mat(int rows, int cols, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
* `new Mat(int rows, int cols, int type, IntPtr data, long step)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `data`: The data parameter.
  * *Parameter* `step`: The step parameter.
* `new Mat(Mat m, Range rowRange, Range colRange)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
  * *Parameter* `rowRange`: The rowRange parameter.
  * *Parameter* `colRange`: The colRange parameter.
* `new Mat(Mat m, Rect roi)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
  * *Parameter* `roi`: The roi parameter.

#### Methods
* `void Create(int rows, int cols, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
* `void Release()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `Mat? Clone()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void CopyTo(Mat dst)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
* `void ConvertTo(Mat dst, int rtype, double alpha, double beta)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `rtype`: The rtype parameter.
  * *Parameter* `alpha`: The alpha parameter.
  * *Parameter* `beta`: The beta parameter.
* `int Type()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Depth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Channels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ElemSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool Empty()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Total()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsContinuous()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsSubmatrix()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `MatShape`
**Inherits from**: `DisposableOpenCVObject`

*  Represents shape of a matrix/tensor.

**Detailed Remarks**:
*   Previously, MatShape was defined as an alias of int[],
*   but now we use a special structure that provides a few extra benefits:
*   1. avoids any heap operations, since the shape is stored in a plain array. This reduces overhead of shape inference etc.
*   2. includes information about the layout, including the actual number of channels ('C') in the case of block layout.
*   3. distinguishes between empty shape (total() == 0) and 0-dimensional shape (dims == 0, but total() == 1).

#### Methods
* `bool Empty()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsScalar()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void Clear()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `void Erase(IntPtr where)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `where`: The where parameter.
* `int Channels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool HasSymbols()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `MatShape? Expand(MatShape another)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `another`: The another parameter.
  * *Returns*: The returned value.
* `MatShape? ToLayout(DataLayout newLayout, int C0)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `newLayout`: The newLayout parameter.
  * *Parameter* `C0`: The C0 parameter.
  * *Returns*: The returned value.

---
### `Moments`
**Inherits from**: `DisposableOpenCVObject`

struct returned by moments

**Detailed Remarks**:
The spatial moments formula are computed as:
[see mathematical formula in OpenCV docs]
The central moments formula are computed as:
[see mathematical formula in OpenCV docs]
where formula is the mass center:
[see mathematical formula in OpenCV docs]
The normalized central moments formula are computed as:
[see mathematical formula in OpenCV docs]
.: info Note

formula, formula
formula , hence the values are not
stored.
The moments of a contour are defined in the same way but computed using the Green's formula (see
<http://en.wikipedia.org/wiki/Green_theorem>). So, due to a limited raster resolution, the moments
computed for a contour are slightly different from the moments computed for the same rasterized
contour.
.:
.: info Note

Since the contour moments are computed using Green formula, you may get seemingly odd results for
contours with self-intersections, e.g. a zero area (m00) for butterfly-shaped contours.
.:

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`M00`** | `double` | Gets or sets the m00 property. |
| **`M10`** | `double` | Gets or sets the m10 property. |
| **`M01`** | `double` | Gets or sets the m01 property. |
| **`M20`** | `double` | Gets or sets the m20 property. |
| **`M11`** | `double` | Gets or sets the m11 property. |
| **`M02`** | `double` | Gets or sets the m02 property. |
| **`M30`** | `double` | Gets or sets the m30 property. |
| **`M21`** | `double` | Gets or sets the m21 property. |
| **`M12`** | `double` | Gets or sets the m12 property. |
| **`M03`** | `double` | Gets or sets the m03 property. |
| **`Mu20`** | `double` | Gets or sets the mu20 property. |
| **`Mu11`** | `double` | Gets or sets the mu11 property. |
| **`Mu02`** | `double` | Gets or sets the mu02 property. |
| **`Mu30`** | `double` | Gets or sets the mu30 property. |
| **`Mu21`** | `double` | Gets or sets the mu21 property. |
| **`Mu12`** | `double` | Gets or sets the mu12 property. |
| **`Mu03`** | `double` | Gets or sets the mu03 property. |
| **`Nu20`** | `double` | Gets or sets the nu20 property. |
| **`Nu11`** | `double` | Gets or sets the nu11 property. |
| **`Nu02`** | `double` | Gets or sets the nu02 property. |
| **`Nu30`** | `double` | Gets or sets the nu30 property. |
| **`Nu21`** | `double` | Gets or sets the nu21 property. |
| **`Nu12`** | `double` | Gets or sets the nu12 property. |
| **`Nu03`** | `double` | Gets or sets the nu03 property. |

---
### `RotatedRect`
**Inherits from**: `DisposableOpenCVObject`

The class represents rotated (i.e. not up-right) rectangles on a plane.

**Detailed Remarks**:
Each rectangle is specified by the center point (mass center), length of each side (represented by
`Size2f` structure) and the rotation angle in degrees.
The sample below demonstrates how to use RotatedRect:

**See also**: CamShift, fitEllipse, minAreaRect, CvBox2D

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Center`** | `Point2f` | Gets or sets the center property. |
| **`Size`** | `Size2F` | Gets or sets the size property. |
| **`Angle`** | `float` | Gets or sets the angle property. |

#### Constructors
* `new RotatedRect()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new RotatedRect(Point2f center, Size2F size, float angle)`
  * *Summary*: full constructor
  * *Parameter* `center`: The rectangle mass center.
  * *Parameter* `size`: Width and height of the rectangle.
  * *Parameter* `angle`: The rotation angle in a clockwise direction. When the angle is 0, 90, 180, 270 etc., the rectangle becomes an up-right rectangle.
* `new RotatedRect(Point2f point1, Point2f point2, Point2f point3)`
  * *Summary*: Any 3 end points of the RotatedRect. They must be given in order (either clockwise or
  * *Parameter* `point1`: The point1 parameter.
  * *Parameter* `point2`: The point2 parameter.
  * *Parameter* `point3`: The point3 parameter.

#### Methods
* `void Points(IntPtr pts)`
  * *Summary*: returns 4 vertices of the rotated rectangle
  * *Remarks*:

.: info Note
_Bottom_, _Top_, _Left_ and _Right_ sides refer to the original rectangle (angle is 0),
so after 180 degree rotation _bottomLeft_ point will be located at the top right corner of the
rectangle.
.:

  * *Parameter* `pts`: The points array for storing rectangle vertices. The order is _bottomLeft_, _topLeft_, topRight, bottomRight.
* `Rect BoundingRect()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Rect2F BoundingRect2f()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `TickMeter`
**Inherits from**: `DisposableOpenCVObject`

a Class to measure passing time.

**Detailed Remarks**:
The class computes passing time by counting the number of ticks per second. That is, the following code computes the
execution time in seconds:
It is also possible to compute the average time over multiple runs:
**See also**: Cv2.GetTickCount, Cv2.GetTickFrequency

#### Constructors
* `new TickMeter()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `void Start()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `void Stop()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `long GetTimeTicks()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetTimeMicro()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetTimeMilli()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetTimeSec()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long GetLastTimeTicks()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetLastTimeMicro()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetLastTimeMilli()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetLastTimeSec()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long GetCounter()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetFPS()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetAvgTimeSec()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `double GetAvgTimeMilli()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void Reset()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `CudaBufferPool`
**Inherits from**: `DisposableOpenCVObject`

BufferPool for use with CUDA streams

**Detailed Remarks**:
BufferPool utilizes Stream's allocator to create new buffers for GpuMat's. It is
only useful when enabled with `setBufferPoolUsage`.

```csharp

Cuda.SetBufferPoolUsage(true);

```

.: info Note
`setBufferPoolUsage` must be called *before* any Stream declaration.
Users may specify custom allocator for Stream and may implement their own stream based
functions utilizing the same underlying GPU memory management.
If custom allocator is not specified, BufferPool utilizes StackAllocator by
default. StackAllocator allocates a chunk of GPU device memory beforehand,
and when GpuMat is declared later on, it is given the pre-allocated memory.
This kind of strategy reduces the number of calls for memory allocating APIs
such as cudaMalloc or cudaMallocPitch.
Below is an example that utilizes BufferPool with StackAllocator:

```csharp
// See original OpenCV documentation for the CUDA BufferPool example.
// C# equivalent uses CudaBufferPool and CudaStream objects:
Cuda.SetBufferPoolUsage(true);
Cuda.SetBufferPoolConfig(Cuda.GetDevice(), 1024 * 1024 * 64, 2);
using var stream1 = new CudaStream();
using var stream2 = new CudaStream();
using var pool1 = new CudaBufferPool(stream1);
using var pool2 = new CudaBufferPool(stream2);
using var dSrc1 = pool1.GetBuffer(4096, 4096, MatType.CV_8UC1);
using var dDst1 = pool1.GetBuffer(4096, 4096, MatType.CV_8UC3);
using var dSrc2 = pool2.GetBuffer(1024, 1024, MatType.CV_8UC1);
using var dDst2 = pool2.GetBuffer(1024, 1024, MatType.CV_8UC3);
```

If we allocate another GpuMat on pool1 in the above example, it will be carried out by
the DefaultAllocator since the stack for pool1 is full.

```csharp
// Stack for pool1 is full, memory is allocated with DefaultAllocator
using var dAdd1 = pool1.GetBuffer(1024, 1024, MatType.CV_8UC1);
```

If a third stream is declared in the above example, allocating with `getBuffer`
within that stream will also be carried out by the DefaultAllocator because we've run out of
stacks.

```csharp
// Only 2 stacks were allocated, we've run out of stacks
using var stream3 = new CudaStream();
using var pool3 = new CudaBufferPool(stream3);
using var dSrc3 = pool3.GetBuffer(1024, 1024, MatType.CV_8UC1); // DefaultAllocator
```

.:
.: warning Warning
When utilizing StackAllocator, deallocation order is important.
Just like a stack, deallocation must be done in LIFO order. Below is an example of
erroneous usage that violates LIFO rule. If OpenCV is compiled in Debug mode, this
sample code will emit CV_Assert error.

```csharp
// Erroneous usage example — mat2 must be disposed before mat1 (LIFO order)
Cuda.SetBufferPoolUsage(true);
using var stream = new CudaStream();
using var pool = new CudaBufferPool(stream);
var mat1 = pool.GetBuffer(1024, 1024, MatType.CV_8UC1); // 1MB
var mat2 = pool.GetBuffer(1024, 1024, MatType.CV_8UC1); // 1MB
mat1.Dispose(); // ERROR: mat2 must be disposed before mat1
```

Since local variables are disposed in the reverse order of construction,
the code sample below satisfies the LIFO rule. Local GpuMat's are deallocated
and the corresponding memory is automatically returned to the pool for later usage.

```csharp
// Correct LIFO usage — dispose in reverse order
Cuda.SetBufferPoolUsage(true);
Cuda.SetBufferPoolConfig(Cuda.GetDevice(), 1024 * 1024 * 64, 2);
using var stream1 = new CudaStream();
using var stream2 = new CudaStream();
using var pool1 = new CudaBufferPool(stream1);
using var pool2 = new CudaBufferPool(stream2);
for (int i = 0; i < 10; i++)
{
    using var dSrc1 = pool1.GetBuffer(4096, 4096, MatType.CV_8UC1);
    using var dDst1 = pool1.GetBuffer(4096, 4096, MatType.CV_8UC3);
    using var dSrc2 = pool2.GetBuffer(1024, 1024, MatType.CV_8UC1);
    using var dDst2 = pool2.GetBuffer(1024, 1024, MatType.CV_8UC3);
    // 'using' declarations ensure LIFO disposal at end of scope
}
```
.:

#### Constructors
* `new CudaBufferPool(CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `stream`: The stream parameter.

#### Methods
* `CudaGpuMat? GetBuffer(int rows, int cols, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? GetBuffer(Size size, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `type`: The type parameter.
  * *Returns*: The returned value.
* `IntPtr GetAllocator()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `CudaDeviceInfo`
**Inherits from**: `DisposableOpenCVObject`

Class providing functionality for querying the specified GPU properties.

#### Constructors
* `new CudaDeviceInfo()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new CudaDeviceInfo(int device_id)`
  * *Summary*: The constructors.
  * *Parameter* `device_id`: System index of the CUDA device starting with 0. Constructs the DeviceInfo object for the specified device. If device_id parameter is missed, it constructs an object for the current device.

#### Methods
* `int DeviceID()`
  * *Summary*: Returns system index of the CUDA device starting with 0.
  * *Returns*: The returned value.
* `long TotalGlobalMem()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long SharedMemPerBlock()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int RegsPerBlock()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int WarpSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long MemPitch()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxThreadsPerBlock()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxThreadsDim()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxGridSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int ClockRate()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long TotalConstMem()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MajorVersion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MinorVersion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long TextureAlignment()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long TexturePitchAlignment()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MultiProcessorCount()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool KernelExecTimeoutEnabled()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool Integrated()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool CanMapHostMemory()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr ComputeMode()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxTexture1D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxTexture1DMipmap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxTexture1DLinear()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture2D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture2DMipmap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture2DLinear()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture2DGather()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture3D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxTextureCubemap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture1DLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTexture2DLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxTextureCubemapLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxSurface1D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxSurface2D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxSurface3D()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxSurface1DLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxSurface2DLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxSurfaceCubemap()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr MaxSurfaceCubemapLayered()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long SurfaceAlignment()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool ConcurrentKernels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool ECCEnabled()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PciBusID()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PciDeviceID()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PciDomainID()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool TccDriver()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int AsyncEngineCount()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool UnifiedAddressing()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MemoryClockRate()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MemoryBusWidth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int L2CacheSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxThreadsPerMultiProcessor()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void QueryMemory(long totalMemory, long freeMemory)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `totalMemory`: The totalMemory parameter.
  * *Parameter* `freeMemory`: The freeMemory parameter.
* `long FreeMemory()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long TotalMemory()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsCompatible()`
  * *Summary*: Checks the CUDA module and device compatibility.
  * *Remarks*:

This function returns true if the CUDA module can be run on the specified device. Otherwise, it
returns false .

  * *Returns*: The returned value.

---
### `CudaEvent`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Methods
* `void Record(CudaStream? stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `stream`: The stream parameter.
* `bool QueryIfComplete()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void WaitForCompletion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `float ElapsedTime(CudaEvent start, CudaEvent end)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `start`: The start parameter.
  * *Parameter* `end`: The end parameter.
  * *Returns*: The returned value.

---
### `CudaGpuData`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

---
### `CudaGpuMat`
**Inherits from**: `DisposableOpenCVObject`

Base storage class for GPU memory with reference counting.

**Detailed Remarks**:
Its interface matches the Mat interface with the following limitations:
-   no arbitrary dimensions support (only 2D)
-   no functions that return references to their data (because references on GPU are not valid for
CPU)
-   no expression templates technique support
Beware that the latter limitation may lead to overloaded matrix operators that cause memory
allocations. The GpuMat class is convertible to unmanaged memory pointers and unmanaged memory pointers so it can be
passed directly to the kernel.
.: info Note
In contrast with Mat, in most cases CudaGpuMat.IsContinuous == false . This means that rows are
aligned to a size depending on the hardware. Single-row GpuMat is always a continuous matrix.
.:
.: info Note
You are not recommended to leave static or global GpuMat variables allocated, that is, to rely
on its destructor. The destruction order of such variables and CUDA context is undefined. GPU memory
release function returns error if the CUDA context has been destroyed before.
Some member functions are described as a "Blocking Call" while some are described as a
"Non-Blocking Call". Blocking functions are synchronous to host. It is guaranteed that the GPU
operation is finished when the function returns. However, non-blocking functions are asynchronous to
host. Those functions may return even if the GPU operation is not finished.
Compared to their blocking counterpart, non-blocking functions accept Stream as an additional
argument. If a non-default stream is passed, the GPU operation may overlap with operations in other
streams.
**See also**: Mat
.:

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Step`** | `long` | Gets the step property. |

#### Constructors
* `new CudaGpuMat(int rows, int cols, int type, IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `allocator`: The allocator parameter.
* `new CudaGpuMat(Size size, int type, IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `allocator`: The allocator parameter.
* `new CudaGpuMat(int rows, int cols, int type, Scalar s, IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `s`: The s parameter.
  * *Parameter* `allocator`: The allocator parameter.
* `new CudaGpuMat(Size size, int type, Scalar s, IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `s`: The s parameter.
  * *Parameter* `allocator`: The allocator parameter.
* `new CudaGpuMat(CudaGpuMat m)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
* `new CudaGpuMat(CudaGpuMat m, Range rowRange, Range colRange)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
  * *Parameter* `rowRange`: The rowRange parameter.
  * *Parameter* `colRange`: The colRange parameter.
* `new CudaGpuMat(CudaGpuMat m, Rect roi)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
  * *Parameter* `roi`: The roi parameter.
* `new CudaGpuMat(Mat arr, IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arr`: The arr parameter.
  * *Parameter* `allocator`: The allocator parameter.

#### Methods
* `IntPtr DefaultAllocator()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetDefaultAllocator(IntPtr allocator)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `allocator`: The allocator parameter.
* `IntPtr GetStdAllocator()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void Create(int rows, int cols, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
* `void Create(Size size, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `type`: The type parameter.
* `void Release()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `void Swap(CudaGpuMat mat)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `mat`: The mat parameter.
* `void Upload(Mat arr)`
  * *Summary*: Performs data upload to GpuMat (Blocking call)
  * *Remarks*:

This function copies data from host memory to device memory. As being a blocking call, it is
guaranteed that the copy operation is finished when this function returns.

  * *Parameter* `arr`: The arr parameter.
* `void Upload(Mat arr, CudaStream stream)`
  * *Summary*: Performs data upload to GpuMat (Non-Blocking call)
  * *Remarks*:

This function copies data from host memory to device memory. As being a non-blocking call, this
function may return even if the copy operation is not finished.
The copy operation may be overlapped with operations in other non-default streams if `stream` is
not the default stream and `dst` is HostMem allocated with CudaHostMemAllocType.PageLocked option.

  * *Parameter* `arr`: The arr parameter.
  * *Parameter* `stream`: The stream parameter.
* `void Download(Mat dst)`
  * *Summary*: Performs data download from GpuMat (Blocking call)
  * *Remarks*:

This function copies data from device memory to host memory. As being a blocking call, it is
guaranteed that the copy operation is finished when this function returns.

  * *Parameter* `dst`: Destination matrix or image (output).
* `void Download(Mat dst, CudaStream stream)`
  * *Summary*: Performs data download from GpuMat (Non-Blocking call)
  * *Remarks*:

This function copies data from device memory to host memory. As being a non-blocking call, this
function may return even if the copy operation is not finished.
The copy operation may be overlapped with operations in other non-default streams if `stream` is
not the default stream and `dst` is HostMem allocated with CudaHostMemAllocType.PageLocked option.

  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `stream`: The stream parameter.
* `CudaGpuMat? Clone()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void CopyTo(CudaGpuMat dst)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
* `void CopyTo(CudaGpuMat dst, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `stream`: The stream parameter.
* `void CopyTo(CudaGpuMat dst, CudaGpuMat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `mask`: Optional operation mask.
* `void CopyTo(CudaGpuMat dst, CudaGpuMat mask, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `stream`: The stream parameter.
* `CudaGpuMat? SetTo(Scalar s)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `s`: The s parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? SetTo(Scalar s, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `s`: The s parameter.
  * *Parameter* `stream`: The stream parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? SetTo(Scalar s, Mat mask)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `s`: The s parameter.
  * *Parameter* `mask`: Optional operation mask.
  * *Returns*: The returned value.
* `CudaGpuMat? SetTo(Scalar s, Mat mask, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `s`: The s parameter.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `stream`: The stream parameter.
  * *Returns*: The returned value.
* `void ConvertTo(CudaGpuMat dst, int rtype)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `rtype`: The rtype parameter.
* `void ConvertTo(CudaGpuMat dst, int rtype, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `rtype`: The rtype parameter.
  * *Parameter* `stream`: The stream parameter.
* `void ConvertTo(CudaGpuMat dst, int rtype, double alpha, double beta)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `rtype`: The rtype parameter.
  * *Parameter* `alpha`: The alpha parameter.
  * *Parameter* `beta`: The beta parameter.
* `void ConvertTo(CudaGpuMat dst, int rtype, double alpha, double beta, CudaStream stream)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dst`: Destination matrix or image (output).
  * *Parameter* `rtype`: The rtype parameter.
  * *Parameter* `alpha`: The alpha parameter.
  * *Parameter* `beta`: The beta parameter.
  * *Parameter* `stream`: The stream parameter.
* `void AssignTo(CudaGpuMat m, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `m`: The m parameter.
  * *Parameter* `type`: The type parameter.
* `CudaGpuMat? Row(int y)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `y`: The y parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? Col(int x)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `x`: The x parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? RowRange(int startrow, int endrow)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `startrow`: The startrow parameter.
  * *Parameter* `endrow`: The endrow parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? RowRange(Range r)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `r`: The r parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? ColRange(int startcol, int endcol)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `startcol`: The startcol parameter.
  * *Parameter* `endcol`: The endcol parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? ColRange(Range r)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `r`: The r parameter.
  * *Returns*: The returned value.
* `CudaGpuMat? Reshape(int cn, int rows)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cn`: The cn parameter.
  * *Parameter* `rows`: The rows parameter.
  * *Returns*: The returned value.
* `void LocateROI(Size wholeSize, Point ofs)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `wholeSize`: The wholeSize parameter.
  * *Parameter* `ofs`: The ofs parameter.
* `CudaGpuMat? AdjustROI(int dtop, int dbottom, int dleft, int dright)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `dtop`: The dtop parameter.
  * *Parameter* `dbottom`: The dbottom parameter.
  * *Parameter* `dleft`: The dleft parameter.
  * *Parameter* `dright`: The dright parameter.
  * *Returns*: The returned value.
* `bool IsContinuous()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ElemSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ElemSize1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Type()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Depth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Channels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Step1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Size Size()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool Empty()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `IntPtr CudaPtr()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void UpdateContinuityFlag()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
### `CudaGpuMatAllocator`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

---
### `CudaGpuMatND`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

---
### `CudaHostMem`
**Inherits from**: `DisposableOpenCVObject`

Class with reference counting wrapping special memory type allocation functions from CUDA.

**Detailed Remarks**:
Its interface is also Mat-like but with additional memory type parameters.
-   **PAGE_LOCKED** sets a page locked memory type used commonly for fast and asynchronous
uploading/downloading data from/to GPU.
-   **SHARED** specifies a zero copy memory allocation that enables mapping the host memory to GPU
address space, if supported.
-   **WRITE_COMBINED** sets the write combined buffer that is not cached by CPU. Such buffers are
used to supply GPU with data when GPU only reads it. The advantage is a better CPU cache
utilization.
.: info Note
Allocation size of such memory types is usually limited. For more details, see *CUDA 2.2
Pinned Memory APIs* document or *CUDA C Programming Guide*.
.:

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Step`** | `long` | Gets the step property. |

#### Constructors
* `new CudaHostMem(int rows, int cols, int type, IntPtr alloc_type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `alloc_type`: The alloc_type parameter.
* `new CudaHostMem(Size size, int type, IntPtr alloc_type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `type`: The type parameter.
  * *Parameter* `alloc_type`: The alloc_type parameter.
* `new CudaHostMem(Mat arr, IntPtr alloc_type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `arr`: The arr parameter.
  * *Parameter* `alloc_type`: The alloc_type parameter.

#### Methods
* `void Swap(CudaHostMem b)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `b`: The b parameter.
* `CudaHostMem? Clone()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void Create(int rows, int cols, int type)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `type`: The type parameter.
* `CudaHostMem? Reshape(int cn, int rows)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cn`: The cn parameter.
  * *Parameter* `rows`: The rows parameter.
  * *Returns*: The returned value.
* `Mat? CreateMatHeader()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsContinuous()`
  * *Summary*: Maps CPU memory to GPU address space and creates the CudaGpuMat header without reference counting for it.
  * *Remarks*:

This can be done only if memory was allocated with the SHARED flag and if it is supported by the
hardware. Laptops often share video and CPU memory, so address spaces can be mapped, which
eliminates an extra copy.

  * *Returns*: The returned value.
* `long ElemSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ElemSize1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Type()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Depth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Channels()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Step1()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `Size Size()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool Empty()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `CudaStream`
**Inherits from**: `DisposableOpenCVObject`

This class encapsulates a queue of asynchronous calls.

**Detailed Remarks**:
.: info Note
Currently, you may face problems if an operation is enqueued twice with different data. Some
functions use the constant GPU memory, and next call may update the memory before the previous one
has been finished. But calling different operations asynchronously is safe because each operation
has its own constant buffer. Memory copy/upload/download/set operations to the buffers you hold are
also safe.
.:
.: info Note
The Stream class is not thread-safe. Please use different Stream objects for different CPU threads.

```csharp
// Thread 1
void Thread1()
{
    using var stream1 = new CudaStream();
    // Use stream1 for all CUDA operations in this thread
}
// Thread 2
void Thread2()
{
    using var stream2 = new CudaStream();
    // Use stream2 for all CUDA operations in this thread
}
```

.:
.: info Note
By default all CUDA routines are launched in CudaStream.Null() object, if the stream is not specified by user.
In multi-threading environment the stream objects must be passed explicitly (see previous note).
.:

#### Constructors
* `new CudaStream()`
  * *Summary*: Wrapper for OpenCV's native functionality.
* `new CudaStream(long cudaFlags)`
  * *Summary*: creates a new Stream using the cudaFlags argument to determine the behaviors of the stream
  * *Parameter* `cudaFlags`: The cudaFlags parameter.

#### Methods
* `bool QueryIfComplete()`
  * *Summary*: Returns true if the current stream queue is finished. Otherwise, it returns false.
  * *Returns*: The returned value.
* `void WaitForCompletion()`
  * *Summary*: Blocks the current CPU thread until all operations in the stream are complete.
* `void WaitEvent(CudaEvent @event)`
  * *Summary*: Makes a compute stream wait on an event.
  * *Parameter* `event`: The @event parameter.
* `CudaStream? Null()`
  * *Summary*: Adds a callback to be called on the host after all currently enqueued items in the stream have completed.
  * *Remarks*:

.: info Note
Callbacks must not make any CUDA API calls. Callbacks must not perform any synchronization
that may depend on outstanding device work or other callbacks that are not mandated to run earlier.
Callbacks without a mandated order (in independent streams) execute in undefined order and may be
serialized.
.:

  * *Returns*: The returned value.
* `IntPtr CudaPtr()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `CudaTargetArchs`
**Inherits from**: `DisposableOpenCVObject`

Class providing a set of static methods to check what NVIDIA\* card architecture the CUDA module was built for.

**Detailed Remarks**:
According to the CUDA C Programming Guide Version 3.2: "PTX code produced for some specific compute
capability can always be compiled to binary code of greater or equal compute capability".

#### Methods
* `bool Has(int major, int minor)`
  * *Summary*: There is a set of methods to check whether the module contains intermediate (PTX) or binary CUDA code for the given architecture(s):
  * *Parameter* `major`: Major compute capability version.
  * *Parameter* `minor`: Minor compute capability version.
  * *Returns*: The returned value.
* `bool HasPtx(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.
* `bool HasBin(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.
* `bool HasEqualOrLessPtx(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.
* `bool HasEqualOrGreater(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.
* `bool HasEqualOrGreaterPtx(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.
* `bool HasEqualOrGreaterBin(int major, int minor)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `major`: The major parameter.
  * *Parameter* `minor`: The minor parameter.
  * *Returns*: The returned value.

---
### `OclDevice`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new OclDevice()`
  * *Summary*: Wrapper for OpenCV's native functionality.

#### Methods
* `string? Name()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? Extensions()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsExtensionSupported(string extensionName)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `extensionName`: The extensionName parameter.
  * *Returns*: The returned value.
* `string? Version()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? VendorName()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? OpenCLCVersion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? OpenCLVersion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int DeviceVersionMajor()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int DeviceVersionMinor()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `string? DriverVersion()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int Type()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int AddressBits()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool Available()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool CompilerAvailable()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool LinkerAvailable()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int DoubleFPConfig()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int SingleFPConfig()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int HalfFPConfig()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool HasFP64()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool HasFP16()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool EndianLittle()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool ErrorCorrectionSupport()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int ExecutionCapabilities()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long GlobalMemCacheSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int GlobalMemCacheType()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int GlobalMemCacheLineSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long GlobalMemSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long LocalMemSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int LocalMemType()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool HostUnifiedMemory()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool ImageSupport()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool ImageFromBufferSupport()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IntelSubgroupsSupport()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Image2DMaxWidth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Image2DMaxHeight()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Image3DMaxWidth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Image3DMaxHeight()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long Image3DMaxDepth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ImageMaxBufferSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ImageMaxArraySize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int VendorID()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsAMD()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsIntel()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `bool IsNVidia()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxClockFrequency()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxComputeUnits()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxConstantArgs()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long MaxConstantBufferSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long MaxMemAllocSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long MaxParameterSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxReadImageArgs()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxWriteImageArgs()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxSamplers()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long MaxWorkGroupSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MaxWorkItemDims()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int MemBaseAddrAlign()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthChar()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthShort()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthInt()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthLong()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthFloat()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthDouble()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int NativeVectorWidthHalf()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthChar()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthShort()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthInt()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthLong()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthFloat()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthDouble()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `int PreferredVectorWidthHalf()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long PrintfBufferSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `long ProfilingTimerResolution()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `OclDevice? GetDefault()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `OclOpenCLExecutionContext`
**Inherits from**: `DisposableOpenCVObject`

Indicates if the image format is supported.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.BorderInterpolate`
**Signature**: `int BorderInterpolate(int p, int len, int borderType)`

Computes the source location of an extrapolated pixel.

**Detailed Remarks**:
The function computes and returns the coordinate of a donor pixel corresponding to the specified
extrapolated pixel when using the specified extrapolation border mode. For example, if you use
BORDER_WRAP mode in the horizontal direction, BORDER_REFLECT_101 in the vertical direction and
want to compute value of the "virtual" pixel Point(-5, 100) in a floating-point image img, it
looks like:

```csharp
// C# equivalent using Cv2.BorderInterpolate:
int row = Cv2.BorderInterpolate(100, img.Rows, BorderTypes.Reflect101);
int col = Cv2.BorderInterpolate(-5, img.Cols, BorderTypes.Wrap);
float val = img.At<float>(row, col);
```

Normally, the function is not called directly. It is used inside filtering functions and also in
CopyMakeBorder.
**See also**: CopyMakeBorder

**Parameters**:
* `p`: 0-based coordinate of the extrapolated pixel along one of the axes, likely \<0 or \>= len
* `len`: Length of the array along the corresponding axis.
* `borderType`: Border type, one of the `BorderTypes`, except for `BORDER_TRANSPARENT` and `BORDER_ISOLATED`. When borderType==#BORDER_CONSTANT, the function always returns -1, regardless of p and len.

**Returns**: The returned value.

---
### `Cv2.CopyMakeBorder`
**Signature**: `void CopyMakeBorder(Mat src, Mat dst, int top, int bottom, int left, int right, int borderType, Scalar value)`

Forms a border around an image.

**Detailed Remarks**:
The function copies the source image into the middle of the destination image. The areas to the
left, to the right, above and below the copied source image will be filled with extrapolated
pixels. This is not what filtering functions based on it do (they extrapolate pixels on-fly), but
what other more complex functions, including your own, may do to simplify image boundary handling.
The function supports the mode when src is already in the middle of dst . In this case, the
function does not copy src itself but simply constructs the border, for example:

```csharp
// let border be the same in all directions
int border = 2;
// constructs a larger image to fit both the image and the border
using var gray_buf = new Mat(rgb.Rows + border * 2, rgb.Cols + border * 2, rgb.Depth());
// select the middle part of it w/o copying data
using var gray_canvas = new Mat(); // Parent canvas image
using var gray = new Mat(gray_canvas, new Rect(border, border, rgb.Cols, rgb.Rows));
// convert image from RGB to grayscale
Cv2.CvtColor(rgb, gray, (int)ColorConversionCodes.Rgb2gray, 0, AlgorithmHint.Default);
// form a border in-place
Cv2.CopyMakeBorder(gray, gray_buf, border, border, border, border, (int)BorderTypes.Replicate, new Scalar(0));
```

.: info Note
When the source image is a part (ROI) of a bigger image, the function will try to use the
pixels outside of the ROI to form a border. To disable this feature and always do extrapolation, as
if src was not a ROI, use borderType | `BORDER_ISOLATED`.
**See also**: borderInterpolate
.:

**Parameters**:
* `src`: Source image.
* `dst`: Destination image of the same type as src and the size Size(src.cols+left+right, src.rows+top+bottom) .
* `top`: the top pixels
* `bottom`: the bottom pixels
* `left`: the left pixels
* `right`: Parameter specifying how many pixels in each direction from the source image rectangle to extrapolate. For example, top=1, bottom=1, left=1, right=1 mean that 1 pixel-wide border needs to be built.
* `borderType`: Border type. See borderInterpolate for details.
* `value`: Border value if borderType==BORDER_CONSTANT .

---
### `Cv2.Add`
**Signature**: `void Add(Mat src1, Mat src2, Mat dst, Mat? mask, int dtype)`

Calculates the per-element sum of two arrays or an array and a scalar.

**Detailed Remarks**:
The function add calculates:
- Sum of two arrays when both input arrays have the same size and the same number of channels:
[see mathematical formula in OpenCV docs]
- Sum of an array and a scalar when src2 is constructed from Scalar or has the same number of
elements as `src1.channels()`:
[see mathematical formula in OpenCV docs]
- Sum of a scalar and an array when src1 is constructed from Scalar or has the same number of
elements as `src2.channels()`:
[see mathematical formula in OpenCV docs]
where `I` is a multi-dimensional index of array elements. In case of multi-channel arrays, each
channel is processed independently.
The first function in the list above can be replaced with matrix expressions:

```csharp
dst = src1 + src2;
dst += src1; // equivalent to add(dst, src1, dst);

```

The input arrays and the output array can all have the same or different depths. For example, you
can add a 16-bit unsigned array to a 8-bit signed array and store the sum as a 32-bit
floating-point array. Depth of the output array is determined by the dtype parameter. In the second
and third cases above, as well as in the first case, when src1.depth() == src2.depth(), dtype can
be set to the default -1. In this case, the output array will have the same depth as the input
array, be it src1, src2 or both.
.: info Note
Saturation is not applied when the output array has the depth CV_32S. You may even get
result of an incorrect sign in the case of overflow.
`add(src,X)` means `add(src,(X,X,X,X))`.
`add(src,(X,))` means `add(src,(X,0,0,0))`.
**See also**: subtract, addWeighted, scaleAdd, Mat.ConvertTo
.:

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array that has the same size and number of channels as the input array(s); the depth is defined by dtype or src1/src2.
* `mask`: optional operation mask - CV_8U, CV_8S or CV_Bool single channel array, that specifies elements of the output array to be changed.
* `dtype`: optional depth of the output array (see the discussion below).

---
### `Cv2.Subtract`
**Signature**: `void Subtract(Mat src1, Mat src2, Mat dst, Mat? mask, int dtype)`

Calculates the per-element difference between two arrays or array and a scalar.

**Detailed Remarks**:
The function subtract calculates:
- Difference between two arrays, when both input arrays have the same size and the same number of
channels:
[see mathematical formula in OpenCV docs]
- Difference between an array and a scalar, when src2 is constructed from Scalar or has the same
number of elements as `src1.channels()`:
[see mathematical formula in OpenCV docs]
- Difference between a scalar and an array, when src1 is constructed from Scalar or has the same
number of elements as `src2.channels()`:
[see mathematical formula in OpenCV docs]
- The reverse difference between a scalar and an array in the case of `SubRS`:
[see mathematical formula in OpenCV docs]
where I is a multi-dimensional index of array elements. In case of multi-channel arrays, each
channel is processed independently.
The first function in the list above can be replaced with matrix expressions:

```csharp
dst = src1 - src2;
dst -= src1; // equivalent to subtract(dst, src1, dst);

```

The input arrays and the output array can all have the same or different depths. For example, you
can subtract to 8-bit unsigned arrays and store the difference in a 16-bit signed array. Depth of
the output array is determined by dtype parameter. In the second and third cases above, as well as
in the first case, when src1.depth() == src2.depth(), dtype can be set to the default -1. In this
case the output array will have the same depth as the input array, be it src1, src2 or both.
.: info Note
Saturation is not applied when the output array has the depth CV_32S. You may even get
result of an incorrect sign in the case of overflow.
`subtract(src,X)` means `subtract(src,(X,X,X,X))`.
`subtract(src,(X,))` means `subtract(src,(X,0,0,0))`.
**See also**: add, addWeighted, scaleAdd, Mat.ConvertTo
.:

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array of the same size and the same number of channels as the input array.
* `mask`: optional operation mask; this is CV_8U, CV8S or CV_Bool single channel array that specifies elements of the output array to be changed.
* `dtype`: optional depth of the output array

---
### `Cv2.Multiply`
**Signature**: `void Multiply(Mat src1, Mat src2, Mat dst, double scale, int dtype)`

Calculates the per-element scaled product of two arrays.

**Detailed Remarks**:
The function multiply calculates the per-element product of two arrays:
[see mathematical formula in OpenCV docs]
There is also a `MatrixExpressions` -friendly variant of the first function. See Mat.Mul .
For a not-per-element matrix product, see gemm .
.: info Note
Saturation is not applied when the output array has the depth
CV_32S. You may even get result of an incorrect sign in the case of
overflow.
`multiply(src,X)` means `multiply(src,(X,X,X,X))`.
`multiply(src,(X,))` means `multiply(src,(X,0,0,0))`.
**See also**: add, subtract, divide, scaleAdd, addWeighted, accumulate, accumulateProduct, accumulateSquare,
Mat.ConvertTo
.:

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size and the same type as src1.
* `dst`: output array of the same size and type as src1.
* `scale`: optional scale factor.
* `dtype`: optional depth of the output array

---
### `Cv2.Divide`
**Signature**: `void Divide(Mat src1, Mat src2, Mat dst, double scale, int dtype)`

Performs per-element division of two arrays or a scalar by an array.

**Detailed Remarks**:
The function divide divides one array by another:
[see mathematical formula in OpenCV docs]
or a scalar by an array when there is no src1 :
[see mathematical formula in OpenCV docs]
Different channels of multi-channel arrays are processed independently.
For integer types when src2(I) is zero, dst(I) will also be zero.
.: info Note
In case of floating point data there is no special defined behavior for zero src2(I) values.
Regular floating-point division is used.
Expect correct IEEE-754 behaviour for floating-point data (with NaN, Inf result values).
.:
.: info Note
Saturation is not applied when the output array has the depth CV_32S. You may even get
result of an incorrect sign in the case of overflow.
`divide(src,X)` means `divide(src,(X,X,X,X))`.
`divide(src,(X,))` means `divide(src,(X,0,0,0))`.
**See also**: multiply, add, subtract
.:

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size and type as src1.
* `dst`: output array of the same size and type as src2.
* `scale`: scalar factor.
* `dtype`: optional depth of the output array; if -1, dst will have depth src2.depth(), but in case of an array-by-array division, you can only pass -1 when src1.depth()==src2.depth().

---
### `Cv2.Divide`
**Signature**: `void Divide(double scale, Mat src2, Mat dst, int dtype)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `scale`: The scale parameter.
* `src2`: The src2 parameter.
* `dst`: Destination matrix or image (output).
* `dtype`: The dtype parameter.

---
### `Cv2.ScaleAdd`
**Signature**: `void ScaleAdd(Mat src1, double alpha, Mat src2, Mat dst)`

Calculates the sum of a scaled array and another array.

**Detailed Remarks**:
The function scaleAdd is one of the classical primitive linear algebra operations, known as DAXPY
or SAXPY in [BLAS](http://en.wikipedia.org/wiki/Basic_Linear_Algebra_Subprograms). It calculates
the sum of a scaled array and another array:
[see mathematical formula in OpenCV docs]
The function can also be emulated with a matrix expression, for example:

```csharp
using var A = new Mat(3, 3, 6); // CV_64FC1 = 6
// Perform matrix operations using Cv2 methods:
using var r1_double = A.Row(1) * 2;
using var r2 = A.Row(2);
using var r0 = A.Row(0);
Cv2.Add(r1_double, r2, r0);
```

**See also**: add, addWeighted, subtract, Mat.Dot, Mat.ConvertTo

**Parameters**:
* `src1`: first input array.
* `alpha`: scale factor for the first array.
* `src2`: second input array of the same size and type as src1.
* `dst`: output array of the same size and type as src1.

---
### `Cv2.AddWeighted`
**Signature**: `void AddWeighted(Mat src1, double alpha, Mat src2, double beta, double gamma, Mat dst, int dtype)`

Calculates the weighted sum of two arrays.

**Detailed Remarks**:
The function addWeighted calculates the weighted sum of two arrays as follows:
[see mathematical formula in OpenCV docs]
where I is a multi-dimensional index of array elements. In case of multi-channel arrays, each
channel is processed independently.
The function can be replaced with a matrix expression:

```csharp
dst = src1*alpha + src2*beta + gamma;

```

.: info Note
Saturation is not applied when the output array has the depth CV_32S. You may even get
result of an incorrect sign in the case of overflow.
**See also**: add, subtract, scaleAdd, Mat.ConvertTo
.:

**Parameters**:
* `src1`: first input array.
* `alpha`: weight of the first array elements.
* `src2`: second input array of the same size and channel number as src1.
* `beta`: weight of the second array elements.
* `gamma`: scalar added to each sum.
* `dst`: output array that has the same size and number of channels as the input arrays.
* `dtype`: optional depth of the output array; when both input arrays have the same depth, dtype can be set to -1, which will be equivalent to src1.depth().

---
### `Cv2.ConvertScaleAbs`
**Signature**: `void ConvertScaleAbs(Mat src, Mat dst, double alpha, double beta)`

Scales, calculates absolute values, and converts the result to 8-bit.

**Detailed Remarks**:
On each element of the input array, the function convertScaleAbs
performs three operations sequentially: scaling, taking an absolute
value, conversion to an unsigned 8-bit type:
[see mathematical formula in OpenCV docs]
In case of multi-channel arrays, the function processes each channel
independently. When the output is not 8-bit, the operation can be
emulated by calling the Mat.ConvertTo method (or by using matrix
expressions) and then by calculating an absolute value of the result.
For example:

```csharp
using var A = new Mat(30, 30, 5); // CV_32FC1 = 5
using var low = new Mat(30, 30, 5);
using var high = new Mat(30, 30, 5);
// Fill low/high boundary mats and call Randu:
Cv2.Randu(A, low, high);
using var B = (A * 5) + 3;
using var absB = Cv2.Abs(B);
```

**See also**: Mat.ConvertTo, Cv2.Abs(Mat)

**Parameters**:
* `src`: input array.
* `dst`: output array.
* `alpha`: optional scale factor.
* `beta`: optional delta added to the scaled values.

---
### `Cv2.Lut`
**Signature**: `void Lut(Mat src, Mat lut, Mat dst)`

Performs a look-up table transform of an array.

**Detailed Remarks**:
The function LUT fills the output array with values from the look-up table. Indices of the entries
are taken from the input array. That is, the function processes each element of src as follows:
[see mathematical formula in OpenCV docs]
where
[see mathematical formula in OpenCV docs]
**See also**: convertScaleAbs, Mat.ConvertTo

**Parameters**:
* `src`: input array of 8-bit or 16-bit integer elements.
* `lut`: look-up table of 256 elements (if src has depth CV_8U or CV_8S) or 65536 elements(if src has depth CV_16U or CV_16S); in case of multi-channel input array, the table should either have a single channel (in this case the same table is used for all channels) or the same number of channels as in the input array.
* `dst`: output array of the same size and number of channels as src, and the same depth as lut.

---
### `Cv2.Sum`
**Signature**: `Scalar Sum(Mat src)`

Calculates the sum of array elements.

**Detailed Remarks**:
The function sum calculates and returns the sum of array elements,
independently for each channel.
**See also**: countNonZero, mean, meanStdDev, norm, minMaxLoc, reduce

**Parameters**:
* `src`: input array that must have from 1 to 4 channels.

**Returns**: The returned value.

---
### `Cv2.HasNonZero`
**Signature**: `bool HasNonZero(Mat src)`

Checks for the presence of at least one non-zero array element.

**Detailed Remarks**:
The function returns whether there are non-zero elements in src
The function do not work with multi-channel arrays. If you need to check non-zero array
elements across all the channels, use Mat.Reshape first to reinterpret the array as
single-channel. Or you may extract the particular channel using either extractImageCOI, or
mixChannels, or split.
.: info Note

- CV_16F/CV_16BF/CV_Bool/CV_64U/CV_64S/CV_32U are not supported for src.
- If the location of non-zero array elements is important, `findNonZero` is helpful.
- If the count of non-zero array elements is important, `countNonZero` is helpful.
**See also**: mean, meanStdDev, norm, minMaxLoc, calcCovarMatrix
**See also**: findNonZero, countNonZero
.:

**Parameters**:
* `src`: single-channel array.

**Returns**: The returned value.

---
### `Cv2.CountNonZero`
**Signature**: `int CountNonZero(Mat src)`

Counts non-zero array elements.

**Detailed Remarks**:
The function returns the number of non-zero elements in src :
[see mathematical formula in OpenCV docs]
The function do not work with multi-channel arrays. If you need to count non-zero array
elements across all the channels, use Mat.Reshape first to reinterpret the array as
single-channel. Or you may extract the particular channel using either extractImageCOI, or
mixChannels, or split.
.: info Note

- CV_16F/CV_16BF/CV_Bool/CV_64U/CV_64S/CV_32U are not supported for src.
- If only whether there are non-zero elements is important, `hasNonZero` is helpful.
- If the location of non-zero array elements is important, `findNonZero` is helpful.
**See also**: mean, meanStdDev, norm, minMaxLoc, calcCovarMatrix
**See also**: findNonZero, hasNonZero
.:

**Parameters**:
* `src`: single-channel array.

**Returns**: The returned value.

---
### `Cv2.FindNonZero`
**Signature**: `void FindNonZero(Mat src, Mat idx)`

Returns the list of locations of non-zero pixels

**Detailed Remarks**:
Given a binary matrix (likely returned from an operation such
as threshold(), compare(), >, ==, etc, return all of
the non-zero indices as a Mat or Point[] (x,y)
For example:

```csharp
// Using Mat output:
using var binaryImage = new Mat(); // input, binary image
using var locations = new Mat();   // output, locations of non-zero pixels
Cv2.FindNonZero(binaryImage, locations);
```

or

```csharp
// Using Point[] output:
using var binaryImage = new Mat(); // input, binary image
Point[] locations = Cv2.FindNonZeroAsArray(binaryImage);
for (int i = 0; i < locations.Length; i++)
{
    Point pnt = locations[i];
    // ...
}
```

The function do not work with multi-channel arrays. If you need to find non-zero
elements across all the channels, use Mat.Reshape first to reinterpret the array as
single-channel. Or you may extract the particular channel using either extractImageCOI, or
mixChannels, or split.
.: info Note

- CV_16F/CV_16BF/CV_Bool/CV_64U/CV_64S/CV_32U are not supported for src.
- If only count of non-zero array elements is important, `countNonZero` is helpful.
- If only whether there are non-zero elements is important, `hasNonZero` is helpful.
**See also**: countNonZero, hasNonZero
.:

**Parameters**:
* `src`: single-channel array
* `idx`: the output array, type of Mat or Point[], corresponding to non-zero indices in the input

---
### `Cv2.Mean`
**Signature**: `Scalar Mean(Mat src, Mat? mask)`

Calculates an average (mean) of array elements.

**Detailed Remarks**:
The function mean calculates the mean value M of array elements,
independently for each channel, and return it:
[see mathematical formula in OpenCV docs]
When all the mask elements are 0's, the function returns Scalar.all(0)
**See also**: countNonZero, meanStdDev, norm, minMaxLoc

**Parameters**:
* `src`: input array that should have from 1 to 4 channels so that the result can be stored in Scalar_ .
* `mask`: optional operation mask ot type CV_8U, CV_8S or CV_Bool.

**Returns**: The returned value.

---
### `Cv2.MeanStdDev`
**Signature**: `void MeanStdDev(Mat src, Mat mean, Mat stddev, Mat? mask)`

Calculates a mean and standard deviation of array elements.

**Detailed Remarks**:
The function meanStdDev calculates the mean and the standard deviation M
of array elements independently for each channel and returns it via the
output parameters:
[see mathematical formula in OpenCV docs]
When all the mask elements are 0's, the function returns
mean=stddev=Scalar.all(0).
.: info Note
The calculated standard deviation is only the diagonal of the
complete normalized covariance matrix. If the full matrix is needed, you
can reshape the multi-channel array M x N to the single-channel array
M\*N x mtx.channels() (only possible when the matrix is continuous) and
then pass the matrix to calcCovarMatrix .
**See also**: countNonZero, mean, norm, minMaxLoc, calcCovarMatrix
.:

**Parameters**:
* `src`: input array that should have from 1 to 4 channels so that the results can be stored in Scalar_ 's.
* `mean`: output parameter: calculated mean value.
* `stddev`: output parameter: calculated standard deviation.
* `mask`: optional operation mask of type CV_8U, CV_8S or CV_Bool.

---
### `Cv2.Norm`
**Signature**: `double Norm(Mat src1, int normType, Mat? mask)`

Calculates the  absolute norm of an array.

**Detailed Remarks**:
This version of `norm` calculates the absolute norm of src1. The type of norm to calculate is specified using `NormTypes`.
As example for one array consider the function formula.
The formula and formula norm for the sample value formula
is calculated as follows
[see mathematical equations in OpenCV documentation]
and for formula the calculation is
[see mathematical equations in OpenCV documentation]
The following graphic shows all values for the three norm functions formula and formula.
It is notable that the formula norm forms the upper and the formula norm forms the lower border for the example function formula.

When the mask parameter is specified and it is not empty, the norm is
If normType is not specified, `NORM_L2` is used.
calculated only over the region specified by the mask.
Multi-channel input arrays are treated as single-channel arrays, that is,
the results for all channels are combined.
Hamming norms can only be calculated with CV_8U depth arrays.

**Parameters**:
* `src1`: first input array.
* `normType`: type of the norm (see `NormTypes`).
* `mask`: optional operation mask; it must have the same size as src1 and type CV_8UC1, CV_8SC1 or CV_BoolC1.

**Returns**: The returned value.

---
### `Cv2.Norm`
**Signature**: `double Norm(Mat src1, Mat src2, int normType, Mat? mask)`

Calculates an absolute difference norm or a relative difference norm.

**Detailed Remarks**:
This version of norm calculates the absolute difference norm
or the relative difference norm of arrays src1 and src2.
The type of norm to calculate is specified using `NormTypes`.

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size and the same type as src1.
* `normType`: type of the norm (see `NormTypes`).
* `mask`: optional operation mask; it must have the same size as src1 and type CV_8UC1, CV_8S1 or CV_BoolC1.

**Returns**: The returned value.

---
### `Cv2.Psnr`
**Signature**: `double Psnr(Mat src1, Mat src2, double R)`

Computes the Peak Signal-to-Noise Ratio (PSNR) image quality metric.

**Detailed Remarks**:
This function calculates the Peak Signal-to-Noise Ratio (PSNR) image quality metric in decibels (dB),
between two input arrays src1 and src2. The arrays must have the same type.
The PSNR is calculated as follows:

$$
\texttt{PSNR} = 10 \cdot \log_{10}{\left( \frac{R^2}{MSE} \right) }
$$

where R is the maximum integer value of depth (e.g. 255 in the case of CV_8U data)
and MSE is the mean squared error between the two arrays.

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size as src1.
* `R`: the maximum pixel value (255 by default)

**Returns**: The returned value.

---
### `Cv2.BatchDistance`
**Signature**: `void BatchDistance(Mat src1, Mat src2, Mat dist, int dtype, Mat nidx, int normType, int K, Mat? mask, int update, bool crosscheck)`

naive nearest neighbor finder

**Detailed Remarks**:
see http://en.wikipedia.org/wiki/Nearest_neighbor_search
Distance calculation between batch elements.

**Parameters**:
* `src1`: The src1 parameter.
* `src2`: The src2 parameter.
* `dist`: The dist parameter.
* `dtype`: The dtype parameter.
* `nidx`: The nidx parameter.
* `normType`: The normType parameter.
* `K`: The K parameter.
* `mask`: Optional operation mask.
* `update`: The update parameter.
* `crosscheck`: The crosscheck parameter.

---
### `Cv2.Normalize`
**Signature**: `void Normalize(Mat src, Mat dst, double alpha, double beta, int norm_type, int dtype, Mat? mask)`

Normalizes the norm or value range of an array.

**Detailed Remarks**:
The function normalize normalizes scale and shift the input array elements so that
[see mathematical formula in OpenCV docs]
(where p=Inf, 1 or 2) when normType=NORM_INF, NORM_L1, or NORM_L2, respectively; or so that
[see mathematical formula in OpenCV docs]
when normType=NORM_MINMAX (for dense arrays only). The optional mask specifies a sub-array to be
normalized. This means that the norm or min-n-max are calculated over the sub-array, and then this
sub-array is modified to be normalized. If you want to only use the mask to calculate the norm or
min-max but modify the whole array, you can use norm and Mat.ConvertTo.
In case of sparse matrices, only the non-zero values are analyzed and transformed. Because of this,
the range transformation for sparse matrices is not allowed since it can shift the zero level.
Possible usage with some positive example data:

```csharp
double[] positiveData = { 2.0, 8.0, 10.0 };
using var positiveMat = new Mat(1, 3, 6); // CV_64FC1 = 6
System.Runtime.InteropServices.Marshal.Copy(positiveData, 0, positiveMat.Data, positiveData.Length);
using var normalizedData_l1 = new Mat();
using var normalizedData_l2 = new Mat();
using var normalizedData_inf = new Mat();
using var normalizedData_minmax = new Mat();

// Norm to probability (total count)
Cv2.Normalize(positiveMat, normalizedData_l1, 1.0, 0.0, (int)NormTypes.L1);
// Norm to unit vector
Cv2.Normalize(positiveMat, normalizedData_l2, 1.0, 0.0, (int)NormTypes.L2);
// Norm to max element
Cv2.Normalize(positiveMat, normalizedData_inf, 1.0, 0.0, (int)NormTypes.Inf);
// Norm to range [0.0;1.0]
Cv2.Normalize(positiveMat, normalizedData_minmax, 1.0, 0.0, (int)NormTypes.Minmax);
```

.: info Note
Due to rounding issues, min-max normalization can result in values outside provided boundaries.
If exact range conformity is needed, following workarounds can be used:
- use double floating point precision (dtype = CV_64F)
- manually clip values (`max(res, left_bound, res)`, `min(res, right_bound, res)` or `Math.Clamp`)
**See also**: norm, Mat.ConvertTo, SparseMat.ConvertTo
.:

**Parameters**:
* `src`: input array.
* `dst`: output array of the same size as src .
* `alpha`: norm value to normalize to or the lower range boundary in case of the range normalization.
* `beta`: upper range boundary in case of the range normalization; it is not used for the norm normalization.
* `norm_type`: normalization type (see NormTypes).
* `dtype`: when negative, the output array has the same type as src; otherwise, it has the same number of channels as src and the depth =CV_MAT_DEPTH(dtype).
* `mask`: optional operation mask of type CV_8U, CV_8S or CV_Bool.

---
### `Cv2.MinMaxLoc`
**Signature**: `void MinMaxLoc(Mat src, IntPtr minVal, IntPtr maxVal, IntPtr minLoc, IntPtr maxLoc, Mat? mask)`

Finds the global minimum and maximum in an array.

**Detailed Remarks**:
The function minMaxLoc finds the minimum and maximum element values and their positions. The
extremums are searched across the whole array or, if mask is not an empty array, in the specified
array region.
If the input is multi-channel, you should omit the minLoc, maxLoc, and mask arguments
(i.e. leave them as null, null, and null respectively). These arguments are not
supported for multi-channel input arrays. If working with multi-channel input and you
need the minLoc, maxLoc, or mask arguments, then use Mat.Reshape first to reinterpret
the array as single-channel. Alternatively, you can extract the particular channel using either
extractImageCOI, mixChannels, or split.

functionality.
.: info Note
CV_16F/CV_16BF/CV_Bool/CV_64U/CV_64S/CV_32U are not supported for src.
**See also**: max, min, reduceArgMin, reduceArgMax, compare, inRange, extractImageCOI, mixChannels, split, Mat.Reshape
.:

**Parameters**:
* `src`: input single-channel array.
* `minVal`: pointer to the returned minimum value; null is used if not required.
* `maxVal`: pointer to the returned maximum value; null is used if not required.
* `minLoc`: pointer to the returned minimum location (in 2D case); null is used if not required.
* `maxLoc`: pointer to the returned maximum location (in 2D case); null is used if not required.
* `mask`: optional mask used to select a sub-array of type CV_8U, CV_8S or CV_Bool.

---
### `Cv2.ReduceArgMin`
**Signature**: `void ReduceArgMin(Mat src, Mat dst, int axis, bool lastIndex)`

*  Finds indices of min elements along provided axis

**Detailed Remarks**:
.: info Note
*
*      - If input or output array is not continuous, this function will create an internal copy.
*      - NaN handling is left unspecified, see patchNaNs().
*      - The returned index is always in bounds of input matrix.

* * **Parameter** `src`:  input single-channel array.
* * **Parameter** `dst`:  output array of type CV_32SC1 with the same dimensionality as src,
* except for axis being reduced - it should be set to 1.
* * **Parameter** `lastIndex`:  whether to get the index of first or last occurrence of min.
* * **Parameter** `axis`:  axis to reduce along.
**See also**: *  reduceArgMax, minMaxLoc, min, max, compare, reduce
.:

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `axis`: The axis parameter.
* `lastIndex`: The lastIndex parameter.

---
### `Cv2.ReduceArgMax`
**Signature**: `void ReduceArgMax(Mat src, Mat dst, int axis, bool lastIndex)`

*  Finds indices of max elements along provided axis

**Detailed Remarks**:
.: info Note
*
*      - If input or output array is not continuous, this function will create an internal copy.
*      - NaN handling is left unspecified, see patchNaNs().
*      - The returned index is always in bounds of input matrix.

* * **Parameter** `src`:  input single-channel array.
* * **Parameter** `dst`:  output array of type CV_32SC1 with the same dimensionality as src,
* except for axis being reduced - it should be set to 1.
* * **Parameter** `lastIndex`:  whether to get the index of first or last occurrence of max.
* * **Parameter** `axis`:  axis to reduce along.
**See also**: *  reduceArgMin, minMaxLoc, min, max, compare, reduce
.:

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `axis`: The axis parameter.
* `lastIndex`: The lastIndex parameter.

---
### `Cv2.Reduce`
**Signature**: `void Reduce(Mat src, Mat dst, int dim, int rtype, int dtype)`

Reduces a matrix to a vector.

**Detailed Remarks**:
The function `reduce` reduces the matrix to a vector by treating the matrix rows/columns as a set of
1D vectors and performing the specified operation on the vectors until a single row/column is
obtained. For example, the function can be used to compute horizontal and vertical projections of a
raster image. In case of `REDUCE_MAX` and `REDUCE_MIN`, the output image should have the same type as the source one.
In case of `REDUCE_SUM`, `REDUCE_SUM2` and `REDUCE_AVG`, the output may have a larger element bit-depth to preserve accuracy.
And multi-channel arrays are also supported in these two reduction modes.
The following code demonstrates its usage for a single channel matrix.
And the following code demonstrates its usage for a two-channel matrix.
**See also**: repeat, reduceArgMin, reduceArgMax

**Parameters**:
* `src`: input 2D matrix.
* `dst`: output vector. Its size and type is defined by dim and dtype parameters.
* `dim`: dimension index along which the matrix is reduced. 0 means that the matrix is reduced to a single row. 1 means that the matrix is reduced to a single column.
* `rtype`: reduction operation that could be one of `ReduceTypes`
* `dtype`: when negative, the output vector will have the same type as the input matrix, otherwise, its type will be CV_MAKE_TYPE(CV_MAT_DEPTH(dtype), src.channels()).

---
### `Cv2.Merge`
**Signature**: `void Merge(IntPtr mv, Mat dst)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `mv`: input vector of matrices to be merged; all the matrices in mv must have the same size and the same depth.
* `dst`: output array of the same size and the same depth as mv[0]; The number of channels will be the total number of channels in the matrix array.

---
### `Cv2.Split`
**Signature**: `void Split(Mat m, IntPtr mv)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `m`: input multi-channel array.
* `mv`: output vector of arrays; the arrays themselves are reallocated, if needed.

---
### `Cv2.MixChannels`
**Signature**: `void MixChannels(IntPtr src, IntPtr dst, IntPtr fromTo)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `src`: input array or vector of matrices; all of the matrices must have the same size and the same depth.
* `dst`: output array or vector of matrices; all the matrices **must be allocated**; their size and depth must be the same as in src[0].
* `fromTo`: array of index pairs specifying which channels are copied and where; fromTo[k\*2] is a 0-based index of the input channel in src, fromTo[k\*2+1] is an index of the output channel in dst; the continuous channel numbering is used: the first input image channels are indexed from 0 to src[0].channels()-1, the second input image channels are indexed from src[0].channels() to src[0].channels() + src[1].channels()-1, and so on, the same scheme is used for the output image channels; as a special case, when fromTo[k\*2] is negative, the corresponding output channel is filled with zero .

---
### `Cv2.ExtractChannel`
**Signature**: `void ExtractChannel(Mat src, Mat dst, int coi)`

Extracts a single channel from src (coi is 0-based index)

**Detailed Remarks**:
**See also**: mixChannels, split

**Parameters**:
* `src`: input array
* `dst`: output array
* `coi`: index of channel to extract

---
### `Cv2.InsertChannel`
**Signature**: `void InsertChannel(Mat src, Mat dst, int coi)`

Inserts a single channel to dst (coi is 0-based index)

**Detailed Remarks**:
**See also**: mixChannels, merge

**Parameters**:
* `src`: input array
* `dst`: output array
* `coi`: index of channel for insertion

---
### `Cv2.Flip`
**Signature**: `void Flip(Mat src, Mat dst, int flipCode)`

Flips a 2D array around vertical, horizontal, or both axes.

**Detailed Remarks**:
The function flip flips the array in one of three different ways (row
and column indices are 0-based):

$$\texttt{dst} _{ij} =
\left\{
\begin{array}{l l}
\texttt{src} _{\texttt{src.rows}-i-1,j} & if\;  \texttt{flipCode} = 0 \\
\texttt{src} _{i, \texttt{src.cols} -j-1} & if\;  \texttt{flipCode} > 0 \\
\texttt{src} _{ \texttt{src.rows} -i-1, \texttt{src.cols} -j-1} & if\; \texttt{flipCode} < 0 \\
\end{array}
\right.$$

The example scenarios of using the function are the following:
*   Vertical flipping of the image (flipCode == 0) to switch between
top-left and bottom-left image origin. This is a typical operation
in video processing on Microsoft Windows\* OS.
*   Horizontal flipping of the image with the subsequent horizontal
shift and absolute difference calculation to check for a
vertical-axis symmetry (flipCode \> 0).
*   Simultaneous horizontal and vertical flipping of the image with
the subsequent shift and absolute difference calculation to check
for a central symmetry (flipCode \< 0).
*   Reversing the order of point arrays (flipCode \> 0 or
flipCode == 0).
**See also**: transpose, repeat, completeSymm

**Parameters**:
* `src`: input array.
* `dst`: output array of the same size and type as src.
* `flipCode`: a flag to specify how to flip the array; 0 means flipping around the x-axis and positive value (for example, 1) means flipping around y-axis. Negative value (for example, -1) means flipping around both axes.

---
### `Cv2.FlipND`
**Signature**: `void FlipND(Mat src, Mat dst, int axis)`

Flips a n-dimensional at given axis *  **src** input array *  **dst** output array that has the same shape of src *  **axis** axis that performs a flip on. 0 <= axis < src.dims.

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `axis`: The axis parameter.

---
### `Cv2.Broadcast`
**Signature**: `void Broadcast(Mat src, Mat shape, Mat dst)`

Broadcast the given Mat to the given shape. * **src** input array * **shape** target shape. Should be a list of CV_32S numbers. Note that negative values are not supported. * **dst** output array that has the given shape

**Parameters**:
* `src`: Source matrix or image.
* `shape`: The shape parameter.
* `dst`: Destination matrix or image (output).

---
### `Cv2.Rotate`
**Signature**: `void Rotate(Mat src, Mat dst, int rotateCode)`

Rotates a 2D array in multiples of 90 degrees. The function rotate rotates the array in one of three different ways: *   Rotate by 90 degrees clockwise (rotateCode = ROTATE_90_CLOCKWISE). *   Rotate by 180 degrees clockwise (rotateCode = ROTATE_180). *   Rotate by 270 degrees clockwise (rotateCode = ROTATE_90_COUNTERCLOCKWISE).

**Detailed Remarks**:
**See also**: transpose, repeat, completeSymm, flip, RotateFlags

**Parameters**:
* `src`: input array.
* `dst`: output array of the same type as src.  The size is the same with ROTATE_180, and the rows and cols are switched for ROTATE_90_CLOCKWISE and ROTATE_90_COUNTERCLOCKWISE.
* `rotateCode`: an enum to specify how to rotate the array; see the enum `RotateFlags`

---
### `Cv2.Repeat`
**Signature**: `void Repeat(Mat src, int ny, int nx, Mat dst)`

Fills the output array with repeated copies of the input array.

**Detailed Remarks**:
The function repeat duplicates the input array one or more times along each of the two axes:
[see mathematical formula in OpenCV docs]
The second variant of the function is more convenient to use with `MatrixExpressions`.
**See also**: reduce

**Parameters**:
* `src`: input array to replicate.
* `ny`: Flag to specify how many times the `src` is repeated along the vertical axis.
* `nx`: Flag to specify how many times the `src` is repeated along the horizontal axis.
* `dst`: output array of the same type as `src`.

---
### `Cv2.Hconcat`
**Signature**: `void Hconcat(IntPtr src, Mat dst)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
```csharp
Mat[] matrices = {
    new Mat(4, 1, MatType.CV_8UC1, new Scalar(1)),
    new Mat(4, 1, MatType.CV_8UC1, new Scalar(2)),
    new Mat(4, 1, MatType.CV_8UC1, new Scalar(3))
};
using var result = new Mat();
Cv2.Hconcat(matrices, result);
// result:
// [1, 2, 3;
//  1, 2, 3;
//  1, 2, 3;
//  1, 2, 3]
```

**Parameters**:
* `src`: input array or vector of matrices. all of the matrices must have the same number of rows and the same depth.
* `dst`: output array. It has the same number of rows and depth as the src, and the sum of cols of the src. same depth.

---
### `Cv2.Vconcat`
**Signature**: `void Vconcat(IntPtr src, Mat dst)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
```csharp
Mat[] matrices = {
    new Mat(1, 4, MatType.CV_8UC1, new Scalar(1)),
    new Mat(1, 4, MatType.CV_8UC1, new Scalar(2)),
    new Mat(1, 4, MatType.CV_8UC1, new Scalar(3))
};
using var result = new Mat();
Cv2.Vconcat(matrices, result);
// result:
// [1, 1, 1, 1;
//  2, 2, 2, 2;
//  3, 3, 3, 3]
```

**Parameters**:
* `src`: input array or vector of matrices. all of the matrices must have the same number of cols and the same depth
* `dst`: output array. It has the same number of cols and depth as the src, and the sum of rows of the src. same depth.

---
### `Cv2.BitwiseAnd`
**Signature**: `void BitwiseAnd(Mat src1, Mat src2, Mat dst, Mat? mask)`

computes bitwise conjunction of the two arrays (dst = src1 & src2) Calculates the per-element bit-wise conjunction of two arrays or an array and a scalar.

**Detailed Remarks**:
The function bitwise_and calculates the per-element bit-wise logical conjunction for:
*   Two arrays when src1 and src2 have the same size:
[see mathematical formula in OpenCV docs]
*   An array and a scalar when src2 is constructed from Scalar or has
the same number of elements as `src1.channels()`:
[see mathematical formula in OpenCV docs]
*   A scalar and an array when src1 is constructed from Scalar or has
the same number of elements as `src2.channels()`:
[see mathematical formula in OpenCV docs]
In case of floating-point arrays, their machine-specific bit
representations (usually IEEE754-compliant) are used for the operation.
In case of multi-channel arrays, each channel is processed
independently. In the second and third cases above, the scalar is first
converted to the array type.

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array that has the same size and type as the input arrays.
* `mask`: optional operation mask, CV_8U, CV_8S or CV_Bool single channel array, that specifies elements of the output array to be changed.

---
### `Cv2.BitwiseOr`
**Signature**: `void BitwiseOr(Mat src1, Mat src2, Mat dst, Mat? mask)`

Calculates the per-element bit-wise disjunction of two arrays or an array and a scalar.

**Detailed Remarks**:
The function bitwise_or calculates the per-element bit-wise logical disjunction for:
*   Two arrays when src1 and src2 have the same size:
[see mathematical formula in OpenCV docs]
*   An array and a scalar when src2 is constructed from Scalar or has
the same number of elements as `src1.channels()`:
[see mathematical formula in OpenCV docs]
*   A scalar and an array when src1 is constructed from Scalar or has
the same number of elements as `src2.channels()`:
[see mathematical formula in OpenCV docs]
In case of floating-point arrays, their machine-specific bit
representations (usually IEEE754-compliant) are used for the operation.
In case of multi-channel arrays, each channel is processed
independently. In the second and third cases above, the scalar is first
converted to the array type.

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array that has the same size and type as the input arrays.
* `mask`: optional operation mask, CV_8U, CV_8S or CV_Bool single channel array, that specifies elements of the output array to be changed.

---
### `Cv2.BitwiseXor`
**Signature**: `void BitwiseXor(Mat src1, Mat src2, Mat dst, Mat? mask)`

Calculates the per-element bit-wise "exclusive or" operation on two arrays or an array and a scalar.

**Detailed Remarks**:
The function bitwise_xor calculates the per-element bit-wise logical "exclusive-or"
operation for:
*   Two arrays when src1 and src2 have the same size:
[see mathematical formula in OpenCV docs]
*   An array and a scalar when src2 is constructed from Scalar or has
the same number of elements as `src1.channels()`:
[see mathematical formula in OpenCV docs]
*   A scalar and an array when src1 is constructed from Scalar or has
the same number of elements as `src2.channels()`:
[see mathematical formula in OpenCV docs]
In case of floating-point arrays, their machine-specific bit
representations (usually IEEE754-compliant) are used for the operation.
In case of multi-channel arrays, each channel is processed
independently. In the 2nd and 3rd cases above, the scalar is first
converted to the array type.

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array that has the same size and type as the input arrays.
* `mask`: optional operation mask, CV_8U, CV_8S or CV_Bool single channel array, that specifies elements of the output array to be changed.

---
### `Cv2.BitwiseNot`
**Signature**: `void BitwiseNot(Mat src, Mat dst, Mat? mask)`

Inverts every bit of an array.

**Detailed Remarks**:
The function bitwise_not calculates per-element bit-wise inversion of the input
array:
[see mathematical formula in OpenCV docs]
In case of a floating-point input array, its machine-specific bit
representation (usually IEEE754-compliant) is used for the operation. In
case of multi-channel arrays, each channel is processed independently.

**Parameters**:
* `src`: input array.
* `dst`: output array that has the same size and type as the input array.
* `mask`: optional operation mask, CV_8U, CV_8S or CV_Bool single channel array, that specifies elements of the output array to be changed.

---
### `Cv2.Absdiff`
**Signature**: `void Absdiff(Mat src1, Mat src2, Mat dst)`

Calculates the per-element absolute difference between two arrays or between an array and a scalar.

**Detailed Remarks**:
The function absdiff calculates:
*   Absolute difference between two arrays when they have the same
size and type:
[see mathematical formula in OpenCV docs]
*   Absolute difference between an array and a scalar when the second
array is constructed from Scalar or has as many elements as the
number of channels in `src1`:
[see mathematical formula in OpenCV docs]
*   Absolute difference between a scalar and an array when the first
array is constructed from Scalar or has as many elements as the
number of channels in `src2`:
[see mathematical formula in OpenCV docs]
where I is a multi-dimensional index of array elements. In case of
multi-channel arrays, each channel is processed independently.
.: info Note
Saturation is not applied when the arrays have the depth CV_32S.
You may even get a negative value in the case of overflow.
`absdiff(src,X)` means `absdiff(src,(X,X,X,X))`.
`absdiff(src,(X,))` means `absdiff(src,(X,0,0,0))`.
**See also**: Cv2.Abs(Mat)
.:

**Parameters**:
* `src1`: first input array or a scalar.
* `src2`: second input array or a scalar.
* `dst`: output array that has the same size and type as input arrays.

---
### `Cv2.CopyTo`
**Signature**: `void CopyTo(Mat src, Mat dst, Mat mask)`

This is an overloaded member function, Copies the matrix to another one. When the operation mask is specified, if the Mat.Create call shown above reallocates the matrix, the newly allocated matrix is initialized with all zeros before copying the data.

**Parameters**:
* `src`: source matrix.
* `dst`: Destination matrix. If it does not have a proper size or type before the operation, it is reallocated.
* `mask`: Operation mask of the same size as \*this. Its non-zero elements indicate which matrix elements need to be copied. The mask has to be of type CV_8U, CV_8S or CV_Bool and can have 1 or multiple channels.

---
### `Cv2.InRange`
**Signature**: `void InRange(Mat src, Mat lowerb, Mat upperb, Mat dst)`

Checks if array elements lie between the elements of two other arrays.

**Detailed Remarks**:
The function checks the range as follows:
-   For every element of a single-channel input array:
[see mathematical formula in OpenCV docs]
-   For two-channel arrays:
[see mathematical formula in OpenCV docs]
-   and so forth.
That is, dst (I) is set to 255 (all 1 -bits) if src (I) is within the
specified 1D, 2D, 3D, ... box and 0 otherwise.
When the lower and/or upper boundary parameters are scalars, the indexes
(I) at lowerb and upperb in the above formulas should be omitted.

**Parameters**:
* `src`: first input array.
* `lowerb`: inclusive lower boundary array or a scalar.
* `upperb`: inclusive upper boundary array or a scalar.
* `dst`: output array of the same size as src and CV_8U type.

---
### `Cv2.Compare`
**Signature**: `void Compare(Mat src1, Mat src2, Mat dst, int cmpop)`

Performs the per-element comparison of two arrays or an array and scalar value.

**Detailed Remarks**:
The function compares:
*   Elements of two arrays when src1 and src2 have the same size:
[see mathematical formula in OpenCV docs]
*   Elements of src1 with a scalar src2 when src2 is constructed from
Scalar or has a single element:
[see mathematical formula in OpenCV docs]
*   src1 with elements of src2 when src1 is constructed from Scalar or
has a single element:
[see mathematical formula in OpenCV docs]
When the comparison result is true, the corresponding element of output
array is set to 255. The comparison operations can be replaced with the
equivalent matrix expressions:

```csharp
Mat dst1 = src1 >= src2;
Mat dst2 = src1 < 8;
...

```

**See also**: checkRange, min, max, threshold

**Parameters**:
* `src1`: first input array or a scalar; when it is an array, it must have a single channel.
* `src2`: second input array or a scalar; when it is an array, it must have a single channel.
* `dst`: output array of type ref CV_8U that has the same size and the same number of channels as the input arrays.
* `cmpop`: a flag, that specifies correspondence between the arrays (CmpTypes)

---
### `Cv2.Min`
**Signature**: `void Min(Mat src1, Mat src2, Mat dst)`

Calculates per-element minimum of two arrays or an array and a scalar.

**Detailed Remarks**:
The function min calculates the per-element minimum of two arrays:
[see mathematical formula in OpenCV docs]
or array and a scalar:
[see mathematical formula in OpenCV docs]
**See also**: max, compare, inRange, minMaxLoc

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size and type as src1.
* `dst`: output array of the same size and type as src1.

---
### `Cv2.Max`
**Signature**: `void Max(Mat src1, Mat src2, Mat dst)`

Calculates per-element maximum of two arrays or an array and a scalar.

**Detailed Remarks**:
The function max calculates the per-element maximum of two arrays:
[see mathematical formula in OpenCV docs]
or array and a scalar:
[see mathematical formula in OpenCV docs]
**See also**: min, compare, inRange, minMaxLoc, `MatrixExpressions`

**Parameters**:
* `src1`: first input array.
* `src2`: second input array of the same size and type as src1 .
* `dst`: output array of the same size and type as src1.

---
### `Cv2.Sqrt`
**Signature**: `void Sqrt(Mat src, Mat dst)`

Calculates a square root of array elements.

**Detailed Remarks**:
The function sqrt calculates a square root of each input array element.
In case of multi-channel arrays, each channel is processed
independently. The accuracy is approximately the same as of the built-in
Math.Sqrt .

**Parameters**:
* `src`: input floating-point array.
* `dst`: output array of the same size and type as src.

---
### `Cv2.Pow`
**Signature**: `void Pow(Mat src, double power, Mat dst)`

Raises every array element to a power.

**Detailed Remarks**:
The function pow raises every element of the input array to power :
[see mathematical formula in OpenCV docs]
So, for a non-integer power exponent, the absolute values of input array
elements are used. However, it is possible to get true values for
negative values using some extra operations. In the example below,
computing the 5th root of array src shows:

```csharp
using var mask = src.LessThan(0);
Cv2.Pow(src, 0.2, dst);
Cv2.Subtract(new Scalar(0), dst, dst, mask);
```

For some values of power, such as integer values, 0.5 and -0.5,
specialized faster algorithms are used.
Special values (NaN, Inf) are not handled.
**See also**: sqrt, exp, log, cartToPolar, polarToCart

**Parameters**:
* `src`: input array.
* `power`: exponent of power.
* `dst`: output array of the same size and type as src.

---
### `Cv2.Exp`
**Signature**: `void Exp(Mat src, Mat dst)`

Calculates the exponent of every array element.

**Detailed Remarks**:
The function exp calculates the exponent of every element of the input
array:
[see mathematical formula in OpenCV docs]
The maximum relative error is about 7e-6 for single-precision input and
less than 1e-10 for double-precision input. Currently, the function
converts denormalized values to zeros on output. Special values (NaN,
Inf) are not handled.
**See also**: log, cartToPolar, polarToCart, phase, pow, sqrt, magnitude

**Parameters**:
* `src`: input array.
* `dst`: output array of the same size and type as src.

---
### `Cv2.Log`
**Signature**: `void Log(Mat src, Mat dst)`

Calculates the natural logarithm of every array element.

**Detailed Remarks**:
The function log calculates the natural logarithm of every element of the input array:
[see mathematical formula in OpenCV docs]
Output on zero, negative and special (NaN, Inf) values is undefined.
**See also**: exp, cartToPolar, polarToCart, phase, pow, sqrt, magnitude

**Parameters**:
* `src`: input array.
* `dst`: output array of the same size and type as src .

---
### `Cv2.PolarToCart`
**Signature**: `void PolarToCart(Mat magnitude, Mat angle, Mat x, Mat y, bool angleInDegrees)`

Calculates x and y coordinates of 2D vectors from their magnitude and angle.

**Detailed Remarks**:
The function polarToCart calculates the Cartesian coordinates of each 2D
vector represented by the corresponding elements of magnitude and angle:
[see mathematical formula in OpenCV docs]
The relative accuracy of the estimated coordinates is about 1e-6.
**See also**: cartToPolar, magnitude, phase, exp, log, pow, sqrt

**Parameters**:
* `magnitude`: input floating-point array of magnitudes of 2D vectors; it can be an empty matrix (=Mat()), in this case, the function assumes that all the magnitudes are =1; if it is not empty, it must have the same size and type as angle.
* `angle`: input floating-point array of angles of 2D vectors.
* `x`: output array of x-coordinates of 2D vectors; it has the same size and type as angle.
* `y`: output array of y-coordinates of 2D vectors; it has the same size and type as angle.
* `angleInDegrees`: when true, the input angles are measured in degrees, otherwise, they are measured in radians.

---
### `Cv2.CartToPolar`
**Signature**: `void CartToPolar(Mat x, Mat y, Mat magnitude, Mat angle, bool angleInDegrees)`

Calculates the magnitude and angle of 2D vectors.

**Detailed Remarks**:
The function cartToPolar calculates either the magnitude, angle, or both
for every 2D vector (x(I),y(I)):
[see mathematical formula in OpenCV docs]
The angles are calculated with accuracy about 0.3 degrees. For the point
(0,0), the angle is set to 0.
**See also**: Sobel, Scharr

**Parameters**:
* `x`: array of x-coordinates; this must be a single-precision or double-precision floating-point array.
* `y`: array of y-coordinates, that must have the same size and same type as x.
* `magnitude`: output array of magnitudes of the same size and type as x.
* `angle`: output array of angles that has the same size and type as x; the angles are measured in radians (from 0 to 2\*Pi) or in degrees (0 to 360 degrees).
* `angleInDegrees`: a flag, indicating whether the angles are measured in radians (which is by default), or in degrees.

---
### `Cv2.Phase`
**Signature**: `void Phase(Mat x, Mat y, Mat angle, bool angleInDegrees)`

Calculates the rotation angle of 2D vectors.

**Detailed Remarks**:
The function phase calculates the rotation angle of each 2D vector that
is formed from the corresponding elements of x and y :
[see mathematical formula in OpenCV docs]
The angle estimation accuracy is about 0.3 degrees. When x(I)=y(I)=0 ,
the corresponding angle(I) is set to 0.

**Parameters**:
* `x`: input floating-point array of x-coordinates of 2D vectors.
* `y`: input array of y-coordinates of 2D vectors; it must have the same size and the same type as x.
* `angle`: output array of vector angles; it has the same size and same type as x .
* `angleInDegrees`: when true, the function calculates the angle in degrees, otherwise, they are measured in radians.

---
### `Cv2.Magnitude`
**Signature**: `void Magnitude(Mat x, Mat y, Mat magnitude)`

Calculates the magnitude of 2D vectors.

**Detailed Remarks**:
The function magnitude calculates the magnitude of 2D vectors formed
from the corresponding elements of x and y arrays:
[see mathematical formula in OpenCV docs]
**See also**: cartToPolar, polarToCart, phase, sqrt

**Parameters**:
* `x`: floating-point array of x-coordinates of the vectors.
* `y`: floating-point array of y-coordinates of the vectors; it must have the same size as x.
* `magnitude`: output array of the same size and type as x.

---
### `Cv2.CheckRange`
**Signature**: `bool CheckRange(Mat a, bool quiet, IntPtr pos, double minVal, double maxVal)`

Checks every element of an input array for invalid values.

**Detailed Remarks**:
The function checkRange checks that every array element is neither NaN nor infinite. When minVal \>
-DBL_MAX and maxVal \< DBL_MAX, the function also checks that each value is between minVal and
maxVal. In case of multi-channel arrays, each channel is processed independently. If some values
are out of range, position of the first outlier is stored in pos (when pos != null). Then, the
function either returns false (when quiet=true) or throws an exception.

**Parameters**:
* `a`: input array.
* `quiet`: a flag, indicating whether the functions quietly return false when the array elements are out of range or they throw an exception.
* `pos`: optional output parameter, when not null, must be a pointer to array of src.dims elements.
* `minVal`: inclusive lower boundary of valid values range.
* `maxVal`: exclusive upper boundary of valid values range.

**Returns**: The returned value.

---
### `Cv2.PatchNaNs`
**Signature**: `void PatchNaNs(Mat a, double val)`

Replaces NaNs (Not-a-Number values) in a matrix with the specified value.

**Detailed Remarks**:
This function modifies the input matrix in-place.
The input matrix must be of type `CV_32F` or `CV_64F`; other types are not supported.

**Parameters**:
* `a`: Input/output matrix (CV_32F or CV_64F type).
* `val`: Value used to replace NaNs (defaults to 0).

---
### `Cv2.FiniteMask`
**Signature**: `void FiniteMask(Mat src, Mat mask)`

Generates a mask of finite float values, i.e. not NaNs nor Infs.

**Detailed Remarks**:
An element is set to to 255 (all 1-bits) if all channels are finite.

**Parameters**:
* `src`: Input matrix, should contain float or double elements of 1 to 4 channels
* `mask`: Output matrix of the same size as input of type CV_8UC1

---
### `Cv2.Gemm`
**Signature**: `void Gemm(Mat src1, Mat src2, double alpha, Mat src3, double beta, Mat dst, int flags)`

Performs generalized matrix multiplication.

**Detailed Remarks**:
The function gemm performs generalized matrix multiplication similar to the
gemm functions in BLAS level 3. For example,
`gemm(src1, src2, alpha, src3, beta, dst, GEMM_1_T + GEMM_3_T)`
corresponds to
[see mathematical formula in OpenCV docs]
In case of complex (two-channel) data, performed a complex matrix
multiplication.
The function can be replaced with a matrix expression. For example, the
above call can be replaced with:

```csharp
dst = alpha * src1.T * src2 + beta * src3.T;

```

**See also**: mulTransposed, transform

**Parameters**:
* `src1`: first multiplied input matrix that could be real(CV_32FC1, CV_64FC1) or complex(CV_32FC2, CV_64FC2).
* `src2`: second multiplied input matrix of the same type as src1.
* `alpha`: weight of the matrix product.
* `src3`: third optional delta matrix added to the matrix product; it should have the same type as src1 and src2.
* `beta`: weight of src3.
* `dst`: output matrix; it has the proper size and the same type as input matrices.
* `flags`: operation flags (GemmFlags)

---
### `Cv2.MulTransposed`
**Signature**: `void MulTransposed(Mat src, Mat dst, bool aTa, Mat? delta, double scale, int dtype)`

Calculates the product of a matrix and its transposition.

**Detailed Remarks**:
The function mulTransposed calculates the product of src and its
transposition:
[see mathematical formula in OpenCV docs]
if aTa=true, and
[see mathematical formula in OpenCV docs]
otherwise. The function is used to calculate the covariance matrix. With
zero delta, it can be used as a faster substitute for general matrix
product A\*B when B=A'
**See also**: calcCovarMatrix, gemm, repeat, reduce

**Parameters**:
* `src`: input single-channel matrix. Note that unlike gemm, the function can multiply not only floating-point matrices.
* `dst`: output square matrix.
* `aTa`: Flag specifying the multiplication ordering. See the description below.
* `delta`: Optional delta matrix subtracted from src before the multiplication. When the matrix is empty ( delta=null ), it is assumed to be zero, that is, nothing is subtracted. If it has the same size as src, it is simply subtracted. Otherwise, it is "repeated" (see repeat ) to cover the full src and then subtracted. Type of the delta matrix, when it is not empty, must be the same as the type of created output matrix. See the dtype parameter description below.
* `scale`: Optional scale factor for the matrix product.
* `dtype`: Optional type of the output matrix. When it is negative, the output matrix will have the same type as src . Otherwise, it will be type=CV_MAT_DEPTH(dtype) that should be either CV_32F or CV_64F .

---
### `Cv2.Transpose`
**Signature**: `void Transpose(Mat src, Mat dst)`

Transposes a matrix.

**Detailed Remarks**:
The function transpose transposes the matrix src :
[see mathematical formula in OpenCV docs]
.: info Note
No complex conjugation is done in case of a complex matrix. It
should be done separately if needed.
.:

**Parameters**:
* `src`: input array.
* `dst`: output array of the same type as src.

---
### `Cv2.TransposeND`
**Signature**: `void TransposeND(Mat src, IntPtr order, Mat dst)`

Transpose for n-dimensional matrices. * * **Note:** Input should be continuous single-channel matrix. * **src** input array. * **order** a permutation of [0,1,..,N-1] where N is the number of axes of src. * The i'th axis of dst will correspond to the axis numbered order[i] of the input. * **dst** output array of the same type as src.

**Parameters**:
* `src`: Source matrix or image.
* `order`: The order parameter.
* `dst`: Destination matrix or image (output).

---
### `Cv2.Transform`
**Signature**: `void Transform(Mat src, Mat dst, Mat m)`

Performs the matrix transformation of every array element.

**Detailed Remarks**:
The function transform performs the matrix transformation of every
element of the array src and stores the results in dst :
[see mathematical formula in OpenCV docs]
(when m.cols=src.channels() ), or
[see mathematical formula in OpenCV docs]
(when m.cols=src.channels()+1 )
Every element of the N -channel array src is interpreted as N -element
vector that is transformed using the M x N or M x (N+1) matrix m to
M-element vector - the corresponding element of the output array dst .
The function may be used for geometrical transformation of
N -dimensional points, arbitrary linear color space transformation (such
as various kinds of RGB to YUV transforms), shuffling the image
channels, and so forth.
**See also**: perspectiveTransform, getAffineTransform, estimateAffine2D, warpAffine, warpPerspective

**Parameters**:
* `src`: input array that must have as many channels (1 to 4) as m.cols or m.cols-1.
* `dst`: output array of the same size and depth as src; it has as many channels as m.rows.
* `m`: transformation 2x2 or 2x3 floating-point matrix.

---
### `Cv2.PerspectiveTransform`
**Signature**: `void PerspectiveTransform(Mat src, Mat dst, Mat m)`

Performs the perspective matrix transformation of vectors.

**Detailed Remarks**:
The function perspectiveTransform transforms every element of src by
treating it as a 2D or 3D vector, in the following way:
[see mathematical formula in OpenCV docs]
where
[see mathematical formula in OpenCV docs]
and
[see mathematical formula in OpenCV docs]
Here a 3D vector transformation is shown. In case of a 2D vector
transformation, the z component is omitted.
.: info Note
The function transforms a sparse set of 2D or 3D vectors. If you
want to transform an image using perspective transformation, use
warpPerspective . If you have an inverse problem, that is, you want to
compute the most probable perspective transformation out of several
pairs of corresponding points, you can use getPerspectiveTransform or
findHomography .
**See also**: transform, warpPerspective, getPerspectiveTransform, findHomography
.:

**Parameters**:
* `src`: input two-channel or three-channel floating-point array; each element is a 2D/3D vector to be transformed.
* `dst`: output array of the same size and type as src.
* `m`: 3x3 or 4x4 floating-point transformation matrix.

---
### `Cv2.CompleteSymm`
**Signature**: `void CompleteSymm(Mat m, bool lowerToUpper)`

Copies the lower or the upper half of a square matrix to its another half.

**Detailed Remarks**:
The function completeSymm copies the lower or the upper half of a square matrix to
its another half. The matrix diagonal remains unchanged:
- formula for formula if
lowerToUpper=false
- formula for formula if
lowerToUpper=true
**See also**: flip, transpose

**Parameters**:
* `m`: input-output floating-point square matrix.
* `lowerToUpper`: operation flag; if true, the lower half is copied to the upper half. Otherwise, the upper half is copied to the lower half.

---
### `Cv2.SetIdentity`
**Signature**: `void SetIdentity(Mat mtx, Scalar s)`

Initializes a scaled identity matrix.

**Detailed Remarks**:
The function setIdentity initializes a scaled identity matrix:
[see mathematical formula in OpenCV docs]
The function can also be emulated using the matrix initializers and the
matrix expressions:

```csharp

using var A = Cv2.Eye(4, 3, 5) * 5; // CV_32FC1 = 5
// A will be set to [[5, 0, 0], [0, 5, 0], [0, 0, 5], [0, 0, 0]]

```

**See also**: Cv2.Zeros, Cv2.Ones, Mat.SetTo, Mat assignment

**Parameters**:
* `mtx`: matrix to initialize (not necessarily square).
* `s`: value to assign to diagonal elements.

---
### `Cv2.Determinant`
**Signature**: `double Determinant(Mat mtx)`

Returns the determinant of a square floating-point matrix.

**Detailed Remarks**:
The function determinant calculates and returns the determinant of the
specified matrix. For small matrices ( mtx.cols=mtx.rows\<=3 ), the
direct method is used. For larger matrices, the function uses LU
factorization with partial pivoting.
For symmetric positively-determined matrices, it is also possible to use
eigen decomposition to calculate the determinant.
**See also**: trace, invert, solve, eigen, `MatrixExpressions`

**Parameters**:
* `mtx`: input matrix that must have CV_32FC1 or CV_64FC1 type and square size.

**Returns**: The returned value.

---
### `Cv2.Trace`
**Signature**: `Scalar Trace(Mat mtx)`

Returns the trace of a matrix.

**Detailed Remarks**:
The function trace returns the sum of the diagonal elements of the
matrix mtx .
[see mathematical formula in OpenCV docs]

**Parameters**:
* `mtx`: input matrix.

**Returns**: The returned value.

---
### `Cv2.Invert`
**Signature**: `double Invert(Mat src, Mat dst, int flags)`

Finds the inverse or pseudo-inverse of a matrix.

**Detailed Remarks**:
The function invert inverts the matrix src and stores the result in dst
. When the matrix src is singular or non-square, the function calculates
the pseudo-inverse matrix (the dst matrix) so that norm(src\*dst - I) is
minimal, where I is an identity matrix.
In case of the `DECOMP_LU` method, the function returns non-zero value if
the inverse has been successfully calculated and 0 if src is singular.
In case of the `DECOMP_SVD` method, the function returns the inverse
condition number of src (the ratio of the smallest singular value to the
largest singular value) and 0 if src is singular. The SVD method
calculates a pseudo-inverse matrix if src is singular.
Similarly to `DECOMP_LU`, the method `DECOMP_CHOLESKY` works only with
non-singular square matrices that should also be symmetrical and
positively defined. In this case, the function stores the inverted
matrix in dst and returns non-zero. Otherwise, it returns 0.
**See also**: solve, SVD

**Parameters**:
* `src`: input floating-point M x N matrix.
* `dst`: output matrix of N x M size and the same type as src.
* `flags`: inversion method (DecompTypes)

**Returns**: The returned value.

---
### `Cv2.Solve`
**Signature**: `bool Solve(Mat src1, Mat src2, Mat dst, int flags)`

Solves one or more linear systems or least-squares problems.

**Detailed Remarks**:
The function solve solves a linear system or least-squares problem (the
latter is possible with SVD or QR methods, or by specifying the flag
`DECOMP_NORMAL` ):
[see mathematical formula in OpenCV docs]
If `DECOMP_LU` or `DECOMP_CHOLESKY` method is used, the function returns 1
if src1 (or formula ) is non-singular. Otherwise,
it returns 0. In the latter case, dst is not valid. Other methods find a
pseudo-solution in case of a singular left-hand side part.
.: info Note
If you want to find a unity-norm solution of an under-defined
singular system formula , the function solve
will not do the work. Use SVD.solveZ instead.
**See also**: invert, SVD, eigen
.:

**Parameters**:
* `src1`: input matrix on the left-hand side of the system.
* `src2`: input matrix on the right-hand side of the system.
* `dst`: output solution.
* `flags`: solution (matrix inversion) method (#DecompTypes)

**Returns**: The returned value.

---
### `Cv2.Sort`
**Signature**: `void Sort(Mat src, Mat dst, int flags)`

Sorts each row or each column of a matrix.

**Detailed Remarks**:
The function sort sorts each matrix row or each matrix column in
ascending or descending order. So you should pass two operation flags to
get desired behaviour. If you want to sort matrix rows or columns
lexicographically, you can use standard sorting function with the
proper comparison predicate.
**See also**: sortIdx, randShuffle

**Parameters**:
* `src`: input single-channel array.
* `dst`: output array of the same size and type as src.
* `flags`: operation flags, a combination of `SortFlags`

---
### `Cv2.SortIdx`
**Signature**: `void SortIdx(Mat src, Mat dst, int flags)`

Sorts each row or each column of a matrix.

**Detailed Remarks**:
The function sortIdx sorts each matrix row or each matrix column in the
ascending or descending order. So you should pass two operation flags to
get desired behaviour. Instead of reordering the elements themselves, it
stores the indices of sorted elements in the output array. For example:

```csharp
using var A = Cv2.Eye(3, 3, 5); // CV_32FC1 = 5
using var B = new Mat();
Cv2.SortIdx(A, B, (int)(SortFlags.EveryRow | SortFlags.Ascending));
```

**See also**: sort, randShuffle

**Parameters**:
* `src`: input single-channel array.
* `dst`: output integer array of the same size as src.
* `flags`: operation flags that could be a combination of SortFlags

---
### `Cv2.SolveCubic`
**Signature**: `int SolveCubic(Mat coeffs, Mat roots)`

Finds the real roots of a cubic equation.

**Detailed Remarks**:
The function solveCubic finds the real roots of a cubic equation:
-   if coeffs is a 4-element vector:
[see mathematical formula in OpenCV docs]
-   if coeffs is a 3-element vector:
[see mathematical formula in OpenCV docs]
The roots are stored in the roots array.

**Parameters**:
* `coeffs`: equation coefficients, an array of 3 or 4 elements.
* `roots`: output array of real roots that has 0, 1, 2 or 3 elements.

**Returns**: number of real roots. It can be -1 (all real numbers), 0, 1, 2 or 3.

---
### `Cv2.SolvePoly`
**Signature**: `double SolvePoly(Mat coeffs, Mat roots, int maxIters)`

Finds the real or complex roots of a polynomial equation.

**Detailed Remarks**:
The function solvePoly finds real and complex roots of a polynomial equation:
[see mathematical formula in OpenCV docs]

**Parameters**:
* `coeffs`: array of polynomial coefficients.
* `roots`: output (complex) array of roots.
* `maxIters`: maximum number of iterations the algorithm does.

**Returns**: The returned value.

---
### `Cv2.Eigen`
**Signature**: `bool Eigen(Mat src, Mat eigenvalues, Mat? eigenvectors)`

Calculates eigenvalues and eigenvectors of a symmetric matrix.

**Detailed Remarks**:
The function eigen calculates just eigenvalues, or eigenvalues and eigenvectors of the symmetric
matrix src:

```text
src * eigenvectors.Row(i).T() = eigenvalues[i] * eigenvectors.Row(i).T()
```

.: info Note
Use eigenNonSymmetric for calculation of real eigenvalues and eigenvectors of non-symmetric matrix.
**See also**: eigenNonSymmetric, completeSymm, PCA
.:

**Parameters**:
* `src`: input matrix that must have CV_32FC1 or CV_64FC1 type, square size and be symmetrical (src ^T^ == src).
* `eigenvalues`: output vector of eigenvalues of the same type as src; the eigenvalues are stored in the descending order.
* `eigenvectors`: output matrix of eigenvectors; it has the same size and type as src; the eigenvectors are stored as subsequent matrix rows, in the same order as the corresponding eigenvalues.

**Returns**: The returned value.

---
### `Cv2.EigenNonSymmetric`
**Signature**: `void EigenNonSymmetric(Mat src, Mat eigenvalues, Mat eigenvectors)`

Calculates eigenvalues and eigenvectors of a non-symmetric matrix (real eigenvalues only).

**Detailed Remarks**:
.: info Note
Assumes real eigenvalues.
The function calculates eigenvalues and eigenvectors (optional) of the square matrix src:

```text
src * eigenvectors.Row(i).T() = eigenvalues[i] * eigenvectors.Row(i).T()
```

**See also**: eigen
.:

**Parameters**:
* `src`: input matrix (CV_32FC1 or CV_64FC1 type).
* `eigenvalues`: output vector of eigenvalues (type is the same type as src).
* `eigenvectors`: output matrix of eigenvectors (type is the same type as src). The eigenvectors are stored as subsequent matrix rows, in the same order as the corresponding eigenvalues.

---
### `Cv2.CalcCovarMatrix`
**Signature**: `void CalcCovarMatrix(Mat samples, Mat covar, Mat mean, int flags, int ctype)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
.: info Note
use `COVAR_ROWS` or `COVAR_COLS` flag
.:

**Parameters**:
* `samples`: samples stored as rows/columns of a single matrix.
* `covar`: output covariance matrix of the type ctype and square size.
* `mean`: input or output (depending on the flags) array as the average value of the input vectors.
* `flags`: operation flags as a combination of `CovarFlags`
* `ctype`: type of the matrixl; it equals 'CV_64F' by default.

---
### `Cv2.PCACompute`
**Signature**: `void PCACompute(Mat data, Mat mean, Mat eigenvectors, int maxComponents)`

wrap PCA.operator()

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `maxComponents`: The maxComponents parameter.

---
### `Cv2.PCACompute`
**Signature**: `void PCACompute(Mat data, Mat mean, Mat eigenvectors, Mat eigenvalues, int maxComponents)`

wrap PCA.operator() and add eigenvalues output parameter

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `eigenvalues`: The eigenvalues parameter.
* `maxComponents`: The maxComponents parameter.

---
### `Cv2.PCACompute`
**Signature**: `void PCACompute(Mat data, Mat mean, Mat eigenvectors, double retainedVariance)`

wrap PCA.operator()

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `retainedVariance`: The retainedVariance parameter.

---
### `Cv2.PCACompute`
**Signature**: `void PCACompute(Mat data, Mat mean, Mat eigenvectors, Mat eigenvalues, double retainedVariance)`

wrap PCA.operator() and add eigenvalues output parameter

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `eigenvalues`: The eigenvalues parameter.
* `retainedVariance`: The retainedVariance parameter.

---
### `Cv2.PCAProject`
**Signature**: `void PCAProject(Mat data, Mat mean, Mat eigenvectors, Mat result)`

wrap PCA.project

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `result`: The result parameter.

---
### `Cv2.PCABackProject`
**Signature**: `void PCABackProject(Mat data, Mat mean, Mat eigenvectors, Mat result)`

wrap PCA.backProject

**Parameters**:
* `data`: The data parameter.
* `mean`: The mean parameter.
* `eigenvectors`: The eigenvectors parameter.
* `result`: The result parameter.

---
### `Cv2.SVDecomp`
**Signature**: `void SVDecomp(Mat src, Mat w, Mat u, Mat vt, int flags)`

wrap SVD.compute

**Parameters**:
* `src`: Source matrix or image.
* `w`: The w parameter.
* `u`: The u parameter.
* `vt`: The vt parameter.
* `flags`: Operation flags.

---
### `Cv2.SVBackSubst`
**Signature**: `void SVBackSubst(Mat w, Mat u, Mat vt, Mat rhs, Mat dst)`

wrap SVD.backSubst

**Parameters**:
* `w`: The w parameter.
* `u`: The u parameter.
* `vt`: The vt parameter.
* `rhs`: The rhs parameter.
* `dst`: Destination matrix or image (output).

---
### `Cv2.Mahalanobis`
**Signature**: `double Mahalanobis(Mat v1, Mat v2, Mat icovar)`

Calculates the Mahalanobis distance between two vectors.

**Detailed Remarks**:
The function Mahalanobis calculates and returns the weighted distance between two vectors:
[see mathematical formula in OpenCV docs]
The covariance matrix may be calculated using the `calcCovarMatrix` function and then inverted using
the invert function (preferably using the `DECOMP_SVD` method, as the most accurate).

**Parameters**:
* `v1`: first 1D input vector.
* `v2`: second 1D input vector.
* `icovar`: inverse covariance matrix.

**Returns**: The returned value.

---
### `Cv2.Dft`
**Signature**: `void Dft(Mat src, Mat dst, int flags, int nonzeroRows)`

Performs a forward or inverse Discrete Fourier transform of a 1D or 2D floating-point array.

**Detailed Remarks**:
The function dft performs one of the following:
-   Forward the Fourier transform of a 1D vector of N elements:
[see mathematical formula in OpenCV docs]
where formula and formula
-   Inverse the Fourier transform of a 1D vector of N elements:
[see mathematical formula in OpenCV docs]
where formula
-   Forward the 2D Fourier transform of a M x N matrix:
[see mathematical formula in OpenCV docs]
-   Inverse the 2D Fourier transform of a M x N matrix:
[see mathematical formula in OpenCV docs]
In case of real (single-channel) data, the output spectrum of the forward Fourier transform or input
spectrum of the inverse Fourier transform can be represented in a packed format called *CCS*
(complex-conjugate-symmetrical). It was borrowed from IPL (Intel\* Image Processing Library). Here
is how 2D *CCS* spectrum looks:
[see mathematical formula in OpenCV docs]
In case of 1D transform of a real vector, the output looks like the first row of the matrix above.
So, the function chooses an operation mode depending on the flags and size of the input array:
-   If `DFT_ROWS` is set or the input array has a single row or single column, the function
performs a 1D forward or inverse transform of each row of a matrix when `DFT_ROWS` is set.
Otherwise, it performs a 2D transform.
-   If the input array is real and `DFT_INVERSE` is not set, the function performs a forward 1D or
2D transform:
-   When `DFT_COMPLEX_OUTPUT` is set, the output is a complex matrix of the same size as
input.
-   When `DFT_COMPLEX_OUTPUT` is not set, the output is a real matrix of the same size as
input. In case of 2D transform, it uses the packed format as shown above. In case of a
single 1D transform, it looks like the first row of the matrix above. In case of
multiple 1D transforms (when using the `DFT_ROWS` flag), each row of the output matrix
looks like the first row of the matrix above.
-   If the input array is complex and either `DFT_INVERSE` or `DFT_REAL_OUTPUT` are not set, the
output is a complex array of the same size as input. The function performs a forward or
inverse 1D or 2D transform of the whole input array or each row of the input array
independently, depending on the flags DFT_INVERSE and DFT_ROWS.
-   When `DFT_INVERSE` is set and the input array is real, or it is complex but `DFT_REAL_OUTPUT`
is set, the output is a real array of the same size as input. The function performs a 1D or 2D
inverse transformation of the whole input array or each individual row, depending on the flags
`DFT_INVERSE` and `DFT_ROWS`.
If `DFT_SCALE` is set, the scaling is done after the transformation.
Unlike dct, the function supports arrays of arbitrary size. But only those arrays are processed
efficiently, whose sizes can be factorized in a product of small prime numbers (2, 3, and 5 in the
current implementation). Such an efficient DFT size can be calculated using the getOptimalDFTSize
method.
The sample below illustrates how to calculate a DFT-based convolution of two 2D real arrays:
An example on DFT-based convolution
To optimize this sample, consider the following approaches:
-   Since nonzeroRows != 0 is passed to the forward transform calls and since A and B are copied to
the top-left corners of tempA and tempB, respectively, it is not necessary to clear the whole
tempA and tempB. It is only necessary to clear the tempA.cols - A.cols ( tempB.cols - B.cols)
rightmost columns of the matrices.
-   This DFT-based convolution does not have to be applied to the whole big arrays, especially if B
is significantly smaller than A or vice versa. Instead, you can calculate convolution by parts.
To do this, you need to split the output array C into multiple tiles. For each tile, estimate
which parts of A and B are required to calculate convolution in this tile. If the tiles in C are
too small, the speed will decrease a lot because of repeated work. In the ultimate case, when
each tile in C is a single pixel, the algorithm becomes equivalent to the naive convolution
algorithm. If the tiles are too big, the temporary arrays tempA and tempB become too big and
there is also a slowdown because of bad cache locality. So, there is an optimal tile size
somewhere in the middle.
-   If different tiles in C can be calculated in parallel and, thus, the convolution is done by
parts, the loop can be threaded.
All of the above improvements have been implemented in `matchTemplate` and `filter2D` . Therefore, by
using them, you can get the performance even better than with the above theoretically optimal
implementation. Though, those two functions actually calculate cross-correlation, not convolution,
so you need to "flip" the second convolution operand B vertically and horizontally using flip .
.: info Note

-   An example using the discrete fourier transform can be found at
**See also**: dct, getOptimalDFTSize, mulSpectrums, filter2D, matchTemplate, flip, cartToPolar,
magnitude, phase
.:

**Parameters**:
* `src`: input array that could be real or complex.
* `dst`: output array whose size and type depends on the flags .
* `flags`: transformation flags, representing a combination of the `DftFlags`
* `nonzeroRows`: when the parameter is not zero, the function assumes that only the first nonzeroRows rows of the input array (#DFT_INVERSE is not set) or only the first nonzeroRows of the output array (#DFT_INVERSE is set) contain non-zeros, thus, the function can handle the rest of the rows more efficiently and save some time; this technique is very useful for calculating array cross-correlation or convolution using DFT.

---
### `Cv2.Idft`
**Signature**: `void Idft(Mat src, Mat dst, int flags, int nonzeroRows)`

Calculates the inverse Discrete Fourier Transform of a 1D or 2D array.

**Detailed Remarks**:
idft(src, dst, flags) is equivalent to dft(src, dst, flags | `DFT_INVERSE`) .
.: info Note
None of dft and idft scales the result by default. So, you should pass `DFT_SCALE` to one of
dft or idft explicitly to make these transforms mutually inverse.
**See also**: dft, dct, idct, mulSpectrums, getOptimalDFTSize
.:

**Parameters**:
* `src`: input floating-point real or complex array.
* `dst`: output array whose size and type depend on the flags.
* `flags`: operation flags (see dft and `DftFlags`).
* `nonzeroRows`: number of dst rows to process; the rest of the rows have undefined content (see the convolution sample in dft description.

---
### `Cv2.Dct`
**Signature**: `void Dct(Mat src, Mat dst, int flags)`

Performs a forward or inverse discrete Cosine transform of 1D or 2D array.

**Detailed Remarks**:
The function dct performs a forward or inverse discrete Cosine transform (DCT) of a 1D or 2D
floating-point array:
-   Forward Cosine transform of a 1D vector of N elements:
[see mathematical formula in OpenCV docs]
where
[see mathematical formula in OpenCV docs]
and
formula, formula for *j \> 0*.
-   Inverse Cosine transform of a 1D vector of N elements:
[see mathematical formula in OpenCV docs]
(since formula is an orthogonal matrix, formula )
-   Forward 2D Cosine transform of M x N matrix:
[see mathematical formula in OpenCV docs]
-   Inverse 2D Cosine transform of M x N matrix:
[see mathematical formula in OpenCV docs]
The function chooses the mode of operation by looking at the flags and size of the input array:
-   If (flags & `DCT_INVERSE`) == 0, the function does a forward 1D or 2D transform. Otherwise, it
is an inverse 1D or 2D transform.
-   If (flags & `DCT_ROWS`) != 0, the function performs a 1D transform of each row.
-   If the array is a single column or a single row, the function performs a 1D transform.
-   If none of the above is true, the function performs a 2D transform.
.: info Note
Currently dct supports even-size arrays (2, 4, 6 ...). For data analysis and approximation, you
can pad the array when necessary.
Also, the function performance depends very much, and not monotonically, on the array size (see
getOptimalDFTSize ). In the current implementation DCT of a vector of size N is calculated via DFT
of a vector of size N/2 . Thus, the optimal DCT size N1 \>= N can be calculated as:

```csharp

int GetOptimalDCTSize(int n) => 2 * Cv2.GetOptimalDFTSize((n + 1) / 2);
int n1 = GetOptimalDCTSize(n);

```

**See also**: dft, getOptimalDFTSize, idct
.:

**Parameters**:
* `src`: input floating-point array.
* `dst`: output array of the same size and type as src .
* `flags`: transformation flags as a combination of DftFlags (DCT_*)

---
### `Cv2.Idct`
**Signature**: `void Idct(Mat src, Mat dst, int flags)`

Calculates the inverse Discrete Cosine Transform of a 1D or 2D array.

**Detailed Remarks**:
idct(src, dst, flags) is equivalent to dct(src, dst, flags | DCT_INVERSE).
**See also**: dct, dft, idft, getOptimalDFTSize

**Parameters**:
* `src`: input floating-point single-channel array.
* `dst`: output array of the same size and type as src.
* `flags`: operation flags.

---
### `Cv2.MulSpectrums`
**Signature**: `void MulSpectrums(Mat a, Mat b, Mat c, int flags, bool conjB)`

Performs the per-element multiplication of two Fourier spectrums.

**Detailed Remarks**:
The function mulSpectrums performs the per-element multiplication of the two CCS-packed or complex
matrices that are results of a real or complex Fourier transform.
The function, together with dft and idft, may be used to calculate convolution (pass conjB=false )
or correlation (pass conjB=true ) of two arrays rapidly. When the arrays are complex, they are
simply multiplied (per element) with an optional conjugation of the second-array elements. When the
arrays are real, they are assumed to be CCS-packed (see dft for details).

**Parameters**:
* `a`: first input array.
* `b`: second input array of the same size and type as src1 .
* `c`: output array of the same size and type as src1 .
* `flags`: operation flags; currently, the only supported flag is DFT_ROWS, which indicates that each row of src1 and src2 is an independent 1D Fourier spectrum. If you do not want to use this flag, then simply add a `0` as value.
* `conjB`: optional flag that conjugates the second input array before the multiplication (true) or not (false).

---
### `Cv2.DivSpectrums`
**Signature**: `void DivSpectrums(Mat a, Mat b, Mat c, int flags, bool conjB)`

Performs the per-element division of the first Fourier spectrum by the second Fourier spectrum. * * The function divSpectrums performs the per-element division of the first array by the second array. * The arrays are CCS-packed or complex matrices that are results of a real or complex Fourier transform. * * **a** first input array. * **b** second input array of the same size and type as src1 . * **c** output array of the same size and type as src1 . * **flags** operation flags; currently, the only supported flag is DFT_ROWS, which indicates that * each row of src1 and src2 is an independent 1D Fourier spectrum. If you do not want to use this flag, then simply add a `0` as value. * **conjB** optional flag that conjugates the second input array before the multiplication (true) * or not (false).

**Parameters**:
* `a`: The a parameter.
* `b`: The b parameter.
* `c`: The c parameter.
* `flags`: Operation flags.
* `conjB`: The conjB parameter.

---
### `Cv2.GetOptimalDFTSize`
**Signature**: `int GetOptimalDFTSize(int vecsize)`

Returns the optimal DFT size for a given vector size.

**Detailed Remarks**:
DFT performance is not a monotonic function of a vector size. Therefore, when you calculate
convolution of two arrays or perform the spectral analysis of an array, it usually makes sense to
pad the input data with zeros to get a bit larger array that can be transformed much faster than the
original one. Arrays whose size is a power-of-two (2, 4, 8, 16, 32, ...) are the fastest to process.
Though, the arrays whose size is a product of 2's, 3's, and 5's (for example, 300 = 5\*5\*3\*2\*2)
are also processed quite efficiently.
The function getOptimalDFTSize returns the minimum number N that is greater than or equal to vecsize
so that the DFT of a vector of size N can be processed efficiently. In the current implementation N
= 2 ^p^ \* 3 ^q^ \* 5 ^r^ for some integer p, q, r.
The function returns a negative number if vecsize is too large (very close to INT_MAX ).
While the function cannot be used directly to estimate the optimal vector size for DCT transform
(since the current DCT implementation supports only even-size vectors), it can be easily processed
as getOptimalDFTSize((vecsize+1)/2)\*2.
**See also**: dft, dct, idft, idct, mulSpectrums

**Parameters**:
* `vecsize`: vector size.

**Returns**: The returned value.

---
### `Cv2.SetRNGSeed`
**Signature**: `void SetRNGSeed(int seed)`

Sets state of default random number generator.

**Detailed Remarks**:
The function setRNGSeed sets state of default random number generator to custom value.
**See also**: RNG, randu, randn

**Parameters**:
* `seed`: new state for default random number generator

---
### `Cv2.Randu`
**Signature**: `void Randu(Mat dst, Mat low, Mat high)`

Generates a single uniformly-distributed random number or an array of random numbers.

**Detailed Remarks**:
Non-template variant of the function fills the matrix dst with uniformly-distributed
random numbers from the specified range:
[see mathematical formula in OpenCV docs]
**See also**: RNG, randn, theRNG

**Parameters**:
* `dst`: output array of random numbers; the array must be pre-allocated.
* `low`: inclusive lower boundary of the generated random numbers.
* `high`: exclusive upper boundary of the generated random numbers.

---
### `Cv2.Randn`
**Signature**: `void Randn(Mat dst, Mat mean, Mat stddev)`

Fills the array with normally distributed random numbers.

**Detailed Remarks**:
The function randn fills the matrix dst with normally distributed random numbers with the specified
mean vector and the standard deviation matrix. The generated random numbers are clipped to fit the
value range of the output array data type.
**See also**: RNG, randu

**Parameters**:
* `dst`: output array of random numbers; the array must be pre-allocated and have 1 to 4 channels.
* `mean`: mean value (expectation) of the generated random numbers.
* `stddev`: standard deviation of the generated random numbers; it can be either a vector (in which case a diagonal standard deviation matrix is assumed) or a square matrix.

---
### `Cv2.RandShuffle`
**Signature**: `void RandShuffle(Mat dst, double iterFactor, IntPtr rng)`

Shuffles the array elements randomly.

**Detailed Remarks**:
The function randShuffle shuffles the specified 1D array by randomly choosing pairs of elements and
swapping them. The number of such swap operations will be dst.rows\*dst.cols\*iterFactor .
**See also**: RNG, sort

**Parameters**:
* `dst`: input/output numerical 1D array.
* `iterFactor`: scale factor that determines the number of random swap operations (see the details below).
* `rng`: optional random number generator used for shuffling; if it is zero, theRNG () is used instead.

---
### `Cv2.Kmeans`
**Signature**: `double Kmeans(Mat data, int K, Mat bestLabels, TermCriteria criteria, int attempts, int flags, Mat? centers)`

Finds centers of clusters and groups input samples around the clusters.

**Detailed Remarks**:
The function kmeans implements a k-means algorithm that finds the centers of cluster_count clusters
and groups the input samples around the clusters. As an output, formula contains a
0-based cluster index for the sample stored in the formula row of the samples matrix.

**Parameters**:
* `data`: Data for clustering. An array of N-Dimensional points with float coordinates is needed. Examples of this array can be: -   Mat points(count, 2, CV_32F); -   Mat points(count, 1, CV_32FC2); -   Mat points(1, count, CV_32FC2); -   Point2f[] points = new Point2f[sampleCount];
* `K`: Number of clusters to split the set by.
* `bestLabels`: Input/output integer array that stores the cluster indices for every sample.
* `criteria`: The algorithm termination criteria, that is, the maximum number of iterations and/or the desired accuracy. The accuracy is specified as criteria.epsilon. As soon as each of the cluster centers moves by less than criteria.epsilon on some iteration, the algorithm stops.
* `attempts`: Flag to specify the number of times the algorithm is executed using different initial labellings. The algorithm returns the labels that yield the best compactness (see the last function parameter).
* `flags`: Flag that can take values of KmeansFlags
* `centers`: Output matrix of the cluster centers, one row per each cluster center.

**Returns**: The function returns the compactness measure that is computed as [see mathematical formula in OpenCV docs] after every attempt. The best (minimum) value is chosen and the corresponding labels and the compactness value are returned by the function. Basically, you can use only the core of the function, set the number of attempts to 1, initialize labels each time using a custom algorithm, pass them with the ( flags = `KMEANS_USE_INITIAL_LABELS` ) flag, and then choose the best (most-compact) clustering.

---
### `Cv2.CubeRoot`
**Signature**: `float CubeRoot(float val)`

Computes the cube root of an argument.

**Detailed Remarks**:
The function cubeRoot computes formula. Negative arguments are handled correctly.
NaN and Inf are not handled. The accuracy approaches the maximum possible accuracy for
single-precision data.

**Parameters**:
* `val`: A function argument.

**Returns**: The returned value.

---
### `Cv2.FastAtan2`
**Signature**: `float FastAtan2(float y, float x)`

Calculates the angle of a 2D vector in degrees.

**Detailed Remarks**:
The function fastAtan2 calculates the full-range angle of an input 2D vector. The angle is measured
in degrees and varies from 0 to 360 degrees. The accuracy is about 0.3 degrees.

**Parameters**:
* `y`: y-coordinate of the vector.
* `x`: x-coordinate of the vector.

**Returns**: The returned value.

---
### `Cv2.IppUseIPP`
**Signature**: `bool IppUseIPP()`

proxy for hal.Cholesky

**Returns**: The returned value.

---
### `Cv2.IppSetUseIPP`
**Signature**: `void IppSetUseIPP(bool flag)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `flag`: The flag parameter.

---
### `Cv2.IppGetIppVersion`
**Signature**: `string? IppGetIppVersion()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.IppUseIPPNotExact`
**Signature**: `bool IppUseIPPNotExact()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.IppSetUseIPPNotExact`
**Signature**: `void IppSetUseIPPNotExact(bool flag)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `flag`: The flag parameter.

---
### `Cv2.CudaCreateContinuous`
**Signature**: `void CudaCreateContinuous(int rows, int cols, int type, Mat arr)`

Creates a continuous matrix.

**Parameters**:
* `rows`: Row count.
* `cols`: Column count.
* `type`: Type of the matrix.
* `arr`: Destination matrix. This parameter changes only if it has a proper type and area ( formula ). Matrix is called continuous if its elements are stored continuously, that is, without gaps at the end of each row.

---
### `Cv2.CudaEnsureSizeIsEnough`
**Signature**: `void CudaEnsureSizeIsEnough(int rows, int cols, int type, Mat arr)`

Ensures that the size of a matrix is big enough and the matrix has a proper type.

**Parameters**:
* `rows`: Minimum desired number of rows.
* `cols`: Minimum desired number of columns.
* `type`: Desired matrix type.
* `arr`: Destination matrix. The function does not reallocate memory if the matrix has proper attributes already.

---
### `Cv2.CudaCreateGpuMatFromCudaMemory`
**Signature**: `CudaGpuMat? CudaCreateGpuMatFromCudaMemory(int rows, int cols, int type, long cudaMemoryAddress, long step)`

Bindings overload to create a GpuMat from existing GPU memory.

**Detailed Remarks**:
.: info Note

.:

**Parameters**:
* `rows`: Row count.
* `cols`: Column count.
* `type`: Type of the matrix.
* `cudaMemoryAddress`: Address of the allocated GPU memory on the device. This does not allocate matrix data. Instead, it just initializes the matrix header that points to the specified `cudaMemoryAddress`, which means that no data is copied. This operation is very efficient and can be used to process external data using OpenCV functions. The external data is not automatically deallocated, so you should take care of it.
* `step`: Number of bytes each matrix row occupies. The value should include the padding bytes at the end of each row, if any. If the parameter is missing (set to AUTO_STEP ), no padding is assumed and the actual step is calculated as cols*elemSize(). See CudaGpuMat.ElemSize.

**Returns**: The returned value.

---
### `Cv2.CudaCreateGpuMatFromCudaMemory`
**Signature**: `CudaGpuMat? CudaCreateGpuMatFromCudaMemory(Size size, int type, long cudaMemoryAddress, long step)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
.: info Note

.:

**Parameters**:
* `size`: 2D array size: Size(cols, rows). In the Size() constructor, the number of rows and the number of columns go in the reverse order.
* `type`: Type of the matrix.
* `cudaMemoryAddress`: Address of the allocated GPU memory on the device. This does not allocate matrix data. Instead, it just initializes the matrix header that points to the specified `cudaMemoryAddress`, which means that no data is copied. This operation is very efficient and can be used to process external data using OpenCV functions. The external data is not automatically deallocated, so you should take care of it.
* `step`: Number of bytes each matrix row occupies. The value should include the padding bytes at the end of each row, if any. If the parameter is missing (set to AUTO_STEP ), no padding is assumed and the actual step is calculated as cols*elemSize(). See CudaGpuMat.ElemSize.

**Returns**: The returned value.

---
### `Cv2.CudaSetBufferPoolUsage`
**Signature**: `void CudaSetBufferPoolUsage(bool on)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `on`: The on parameter.

---
### `Cv2.CudaSetBufferPoolConfig`
**Signature**: `void CudaSetBufferPoolConfig(int deviceId, long stackSize, int stackCount)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `deviceId`: The deviceId parameter.
* `stackSize`: The stackSize parameter.
* `stackCount`: The stackCount parameter.

---
### `Cv2.CudaRegisterPageLocked`
**Signature**: `void CudaRegisterPageLocked(Mat m)`

Page-locks the memory of matrix and maps it for the device(s).

**Parameters**:
* `m`: Input matrix.

---
### `Cv2.CudaUnregisterPageLocked`
**Signature**: `void CudaUnregisterPageLocked(Mat m)`

Unmaps the memory of matrix and makes it pageable again.

**Parameters**:
* `m`: Input matrix.

---
### `Cv2.CudaWrapStream`
**Signature**: `CudaStream? CudaWrapStream(long cudaStreamMemoryAddress)`

Bindings overload to create a Stream object from the address stored in an existing CUDA Runtime API stream pointer (cudaStream_t).

**Detailed Remarks**:
.: info Note

.:

**Parameters**:
* `cudaStreamMemoryAddress`: Memory address stored in a CUDA Runtime API stream pointer (cudaStream_t). The created Stream object does not perform any allocation or deallocation and simply wraps existing raw CUDA Runtime API stream pointer.

**Returns**: The returned value.

---
### `Cv2.CudaGetCudaEnabledDeviceCount`
**Signature**: `int CudaGetCudaEnabledDeviceCount()`

Returns the number of installed CUDA-enabled devices.

**Detailed Remarks**:
Use this function before any other CUDA functions calls. If OpenCV is compiled without CUDA support,
this function returns 0. If the CUDA driver is not installed, or is incompatible, this function
returns -1.

**Returns**: The returned value.

---
### `Cv2.CudaSetDevice`
**Signature**: `void CudaSetDevice(int device)`

Sets a device and initializes it for the current thread.

**Parameters**:
* `device`: System index of a CUDA device starting with 0. If the call of this function is omitted, a default device is initialized at the fist CUDA usage.

---
### `Cv2.CudaGetDevice`
**Signature**: `int CudaGetDevice()`

Returns the current device index set by Cv2.Cuda.SetDevice or initialized by default.

**Returns**: The returned value.

---
### `Cv2.CudaResetDevice`
**Signature**: `void CudaResetDevice()`

Explicitly destroys and cleans up all resources associated with the current device in the current process.

**Detailed Remarks**:
Any subsequent API call to this device will reinitialize the device.

---
### `Cv2.CudaPrintCudaDeviceInfo`
**Signature**: `void CudaPrintCudaDeviceInfo(int device)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `device`: The device parameter.

---
### `Cv2.CudaPrintShortCudaDeviceInfo`
**Signature**: `void CudaPrintShortCudaDeviceInfo(int device)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `device`: The device parameter.

---
### `Cv2.OclHaveOpenCL`
**Signature**: `bool OclHaveOpenCL()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.OclUseOpenCL`
**Signature**: `bool OclUseOpenCL()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.OclHaveAmdBlas`
**Signature**: `bool OclHaveAmdBlas()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.OclHaveAmdFft`
**Signature**: `bool OclHaveAmdFft()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.OclSetUseOpenCL`
**Signature**: `void OclSetUseOpenCL(bool flag)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `flag`: The flag parameter.

---
### `Cv2.OclFinish`
**Signature**: `void OclFinish()`

Wrapper for OpenCV's native functionality.

---
### `Cv2.SolveLP`
**Signature**: `int SolveLP(Mat Func, Mat Constr, Mat z, double constr_eps)`

Solve given (non-integer) linear programming problem using the Simplex Algorithm (Simplex Method).

**Detailed Remarks**:
What we mean here by "linear programming problem" (or LP problem, for short) can be formulated as:

$$\mbox{Maximize } c\cdot x\\
\mbox{Subject to:}\\
Ax\leq b\\
x\geq 0$$

Where formula is fixed `1`-by-`n` row-vector, formula is fixed `m`-by-`n` matrix, formula is fixed `m`-by-`1`
column vector and formula is an arbitrary `n`-by-`1` column vector, which satisfies the constraints.
Simplex algorithm is one of many algorithms that are designed to handle this sort of problems
efficiently. Although it is not optimal in theoretical sense (there exist algorithms that can solve
any problem written as above in polynomial time, while simplex method degenerates to exponential
time for some special cases), it is well-studied, easy to implement and is shown to work well for
real-life purposes.
The particular implementation is taken almost verbatim from **Introduction to Algorithms, third
edition** by T. H. Cormen, C. E. Leiserson, R. L. Rivest and Clifford Stein. In particular, the
Bland's rule <http://en.wikipedia.org/wiki/Bland27s_rule> is used to prevent cycling.

**Parameters**:
* `Func`: This row-vector corresponds to formula in the LP problem formulation (see above). It should contain 32- or 64-bit floating point numbers. As a convenience, column-vector may be also submitted, in the latter case it is understood to correspond to formula.
* `Constr`: `m`-by-`n+1` matrix, whose rightmost column corresponds to formula in formulation above and the remaining to formula. It should contain 32- or 64-bit floating point numbers.
* `z`: The solution will be returned here as a column-vector - it corresponds to formula in the formulation above. It will contain 64-bit floating point numbers.
* `constr_eps`: allowed numeric disparity for constraints

**Returns**: One of SolveLPResult

---
### `Cv2.SolveLP`
**Signature**: `int SolveLP(Mat Func, Mat Constr, Mat z)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `Func`: The Func parameter.
* `Constr`: The Constr parameter.
* `z`: The z parameter.

**Returns**: The returned value.

---
### `Cv2.RectangleIntersectionArea`
**Signature**: `double RectangleIntersectionArea(IntPtr a, IntPtr b)`

Finds out if there is any intersection between two rectangles * * mainly useful for language bindings * **a** First rectangle * **b** Second rectangle **Returns**: the area of the intersection

**Parameters**:
* `a`: The a parameter.
* `b`: The b parameter.

**Returns**: The returned value.

---
### `Cv2.SetNumThreads`
**Signature**: `void SetNumThreads(int nthreads)`

OpenCV will try to set the number of threads for subsequent parallel regions.

**Detailed Remarks**:
If threads == 1, OpenCV will disable threading optimizations and run all it's functions
sequentially. Passing threads \< 0 will reset threads number to system default.
The function is not thread-safe. It must not be called in parallel region or concurrent threads.
OpenCV will try to run its functions with specified threads number, but some behaviour differs from
framework:
-   `TBB` - User-defined parallel constructions will run with the same threads number, if
another is not specified. If later on user creates his own scheduler, OpenCV will use it.
-   `OpenMP` - No special defined behaviour.
-   `Concurrency` - If threads == 1, OpenCV will disable threading optimizations and run its
functions sequentially.
-   `GCD` - Supports only values \<= 0.
-   `C=` - No special defined behaviour.
**See also**: getNumThreads, getThreadNum

**Parameters**:
* `nthreads`: Number of threads used by OpenCV.

---
### `Cv2.GetNumThreads`
**Signature**: `int GetNumThreads()`

Returns the number of threads used by OpenCV for parallel regions.

**Detailed Remarks**:
Always returns 1 if OpenCV is built without threading support.
The exact meaning of return value depends on the threading framework used by OpenCV library:
- `TBB` - The number of threads, that OpenCV will try to use for parallel regions. If there is
any tbb.thread_scheduler_init in user code conflicting with OpenCV, then function returns
default number of threads used by TBB library.
- `OpenMP` - An upper bound on the number of threads that could be used to form a new team.
- `Concurrency` - The number of threads, that OpenCV will try to use for parallel regions.
- `GCD` - Unsupported; returns the GCD thread pool limit (512) for compatibility.
- `C=` - The number of threads, that OpenCV will try to use for parallel regions, if before
called setNumThreads with threads \> 0, otherwise returns the number of logical CPUs,
available for the process.
**See also**: setNumThreads, getThreadNum

**Returns**: The returned value.

---
### `Cv2.GetThreadNum`
**Signature**: `int GetThreadNum()`

Returns the index of the currently executed thread within the current parallel region. Always returns 0 if called outside of parallel region.

**Detailed Remarks**:
*(Deprecated)* Current implementation doesn't corresponding to this documentation.
The exact meaning of the return value depends on the threading framework used by OpenCV library:
- `TBB` - Unsupported with current 4.1 TBB release. Maybe will be supported in future.
- `OpenMP` - The thread number, within the current team, of the calling thread.
- `Concurrency` - An ID for the virtual processor that the current context is executing on (0
for master thread and unique number for others, but not necessary 1,2,3,...).
- `GCD` - System calling thread's ID. Never returns 0 inside parallel region.
- `C=` - The index of the current parallel task.
**See also**: setNumThreads, getNumThreads

**Returns**: The returned value.

---
### `Cv2.GetBuildInformation`
**Signature**: `string? GetBuildInformation()`

Returns full configuration time cmake output.

**Detailed Remarks**:
Returned value is raw cmake output including version control system revision, compiler version,
compiler flags, enabled modules and third party libraries, etc. Output format depends on target
architecture.

**Returns**: The returned value.

---
### `Cv2.GetVersionString`
**Signature**: `string? GetVersionString()`

Returns library version string

**Detailed Remarks**:
For example "3.4.1-dev".
**See also**: getMajorVersion, getMinorVersion, getRevisionVersion

**Returns**: The returned value.

---
### `Cv2.GetVersionMajor`
**Signature**: `int GetVersionMajor()`

Returns major library version

**Returns**: The returned value.

---
### `Cv2.GetVersionMinor`
**Signature**: `int GetVersionMinor()`

Returns minor library version

**Returns**: The returned value.

---
### `Cv2.GetVersionRevision`
**Signature**: `int GetVersionRevision()`

Returns revision field of the library version

**Returns**: The returned value.

---
### `Cv2.GetTickCount`
**Signature**: `long GetTickCount()`

Returns the number of ticks.

**Detailed Remarks**:
The function returns the number of ticks after the certain event (for example, when the machine was
turned on). It can be used to initialize RNG or to measure a function execution time by reading the
tick count before and after the function call.
**See also**: Cv2.GetTickFrequency, TickMeter

**Returns**: The returned value.

---
### `Cv2.GetTickFrequency`
**Signature**: `double GetTickFrequency()`

Returns the number of ticks per second.

**Detailed Remarks**:
The function returns the number of ticks per second. That is, the following code computes the
execution time in seconds:

```csharp

double t = (double)Cv2.GetTickCount();
// do something ...
t = ((double)Cv2.GetTickCount() - t)/Cv2.GetTickFrequency();

```

**See also**: Cv2.GetTickCount, TickMeter

**Returns**: The returned value.

---
### `Cv2.GetCPUTickCount`
**Signature**: `long GetCPUTickCount()`

Returns the number of CPU ticks.

**Detailed Remarks**:
The function returns the current number of CPU ticks on some architectures (such as x86, x64,
PowerPC). On other platforms the function is equivalent to Cv2.GetTickCount. It can also be used for
very accurate time measurements, as well as for RNG initialization. Note that in case of multi-CPU
systems a thread, from which getCPUTickCount is called, can be suspended and resumed at another CPU
with its own counter. So, theoretically (and practically) the subsequent calls to the function do
not necessary return the monotonously increasing values. Also, since a modern CPU varies the CPU
frequency depending on the load, the number of CPU clocks spent in some code cannot be directly
converted to time units. Therefore, Cv2.GetTickCount is generally a preferable solution for measuring
execution time.

**Returns**: The returned value.

---
### `Cv2.CheckHardwareSupport`
**Signature**: `bool CheckHardwareSupport(int feature)`

Returns true if the specified feature is supported by the host hardware.

**Detailed Remarks**:
The function returns true if the host hardware supports the specified feature. When user calls
setUseOptimized(false), the subsequent calls to checkHardwareSupport() will return false until
setUseOptimized(true) is called. This way user can dynamically switch on and off the optimized code
in OpenCV.

**Parameters**:
* `feature`: The feature of interest, one of CpuFeatures

**Returns**: The returned value.

---
### `Cv2.GetHardwareFeatureName`
**Signature**: `string? GetHardwareFeatureName(int feature)`

Returns feature name by ID

**Detailed Remarks**:
Returns empty string if feature is not defined

**Parameters**:
* `feature`: The feature parameter.

**Returns**: The returned value.

---
### `Cv2.GetCPUFeaturesLine`
**Signature**: `string? GetCPUFeaturesLine()`

Returns list of CPU features enabled during compilation.

**Detailed Remarks**:
Returned value is a string containing space separated list of CPU features with following markers:
- no markers - baseline features
- prefix `*` - features enabled in dispatcher
- suffix `?` - features enabled but not available in HW
Example: `SSE SSE2 SSE3 *SSE4.1 *SSE4.2 *FP16 *AVX *AVX2 *AVX512-SKX?`

**Returns**: The returned value.

---
### `Cv2.GetNumberOfCPUs`
**Signature**: `int GetNumberOfCPUs()`

Returns the number of logical CPUs available for the process.

**Returns**: The returned value.

---
### `Cv2.GetDefaultAlgorithmHint`
**Signature**: `AlgorithmHint GetDefaultAlgorithmHint()`

Wrapper for OpenCV's native functionality.

**Returns**: The returned value.

---
### `Cv2.SetUseOptimized`
**Signature**: `void SetUseOptimized(bool onoff)`

Enables or disables the optimized code.

**Detailed Remarks**:
The function can be used to dynamically turn on and off optimized dispatched code (code that uses SSE4.2, AVX/AVX2,
and other instructions on the platforms that support it). It sets a global flag that is further
checked by OpenCV functions. Since the flag is not checked in the inner OpenCV loops, it is only
safe to call the function on the very top level in your application where you can be sure that no
other OpenCV function is currently executed.
By default, the optimized code is enabled unless you disable it in CMake. The current status can be
retrieved using useOptimized.

**Parameters**:
* `onoff`: The boolean flag specifying whether the optimized code should be used (onoff=true) or not (onoff=false).

---
### `Cv2.UseOptimized`
**Signature**: `bool UseOptimized()`

Returns the status of optimized code usage.

**Detailed Remarks**:
The function returns true if the optimized code is enabled. Otherwise, it returns false.

**Returns**: The returned value.

---
### `Cv2.SamplesFindFile`
**Signature**: `string? SamplesFindFile(string relative_path, bool required, bool silentMode)`

Try to find requested data file

**Detailed Remarks**:
Search directories:
1. Directories passed via `addSamplesDataSearchPath()`
2. OPENCV_SAMPLES_DATA_PATH_HINT environment variable
3. OPENCV_SAMPLES_DATA_PATH environment variable
If parameter value is not empty and nothing is found then stop searching.
4. Detects build/install path based on:
a. current working directory (CWD)
b. and/or binary module location (opencv_core/opencv_world, doesn't work with static linkage)
5. Scan `<source>/{,data,samples/data}` directories if build directory is detected or the current directory is in source tree.
6. Scan `<install>/share/OpenCV` directory if install directory is detected.
**See also**: utils.findDataFile

**Parameters**:
* `relative_path`: Relative path to data file
* `required`: Specify "file not found" handling. If true, function prints information message and raises OpenCVException. If false, function returns empty result
* `silentMode`: Disables messages

**Returns**: Returns path (absolute or relative to the current directory) or empty string if file is not found

---
### `Cv2.SamplesFindFileOrKeep`
**Signature**: `string? SamplesFindFileOrKeep(string relative_path, bool silentMode)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `relative_path`: The relative_path parameter.
* `silentMode`: The silentMode parameter.

**Returns**: The returned value.

---
### `Cv2.SamplesAddSamplesDataSearchPath`
**Signature**: `void SamplesAddSamplesDataSearchPath(string path)`

Override search data path by adding new search location

**Detailed Remarks**:
Use this only to override default behavior
Passed paths are used in LIFO order.

**Parameters**:
* `path`: Path to used samples data

---
### `Cv2.SamplesAddSamplesDataSearchSubDirectory`
**Signature**: `void SamplesAddSamplesDataSearchSubDirectory(string subdir)`

Append samples search data sub directory

**Detailed Remarks**:
General usage is to add OpenCV modules name (`<opencv_contrib>/modules/<name>/samples/data` -> `<name>/samples/data` + `modules/<name>/samples/data`).
Passed subdirectories are used in LIFO order.

**Parameters**:
* `subdir`: samples data sub directory

---
### `Cv2.ParallelSetParallelForBackend`
**Signature**: `bool ParallelSetParallelForBackend(string backendName, bool propagateNumThreads)`

Change OpenCV parallel_for backend * * **Note:** This call is not thread-safe. Consider calling this function from the `main()` before any other OpenCV processing functions (and without any other created threads).

**Parameters**:
* `backendName`: The backendName parameter.
* `propagateNumThreads`: The propagateNumThreads parameter.

**Returns**: The returned value.

---
### `Cv2.UtilsLoggingSetLogLevel`
**Signature**: `UtilsLoggingLogLevel UtilsLoggingSetLogLevel(UtilsLoggingLogLevel logLevel)`

Set global logging level

**Parameters**:
* `logLevel`: The logLevel parameter.

**Returns**: previous logging level

---
### `Cv2.UtilsLoggingGetLogLevel`
**Signature**: `UtilsLoggingLogLevel UtilsLoggingGetLogLevel()`

Get global logging level

**Returns**: The returned value.

---
## 🔢 Enumerations

### `AccessFlag`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Read`** | `unchecked((int)(1 << 24))` | Read |
| **`Write`** | `unchecked((int)(1 << 25))` | Write |
| **`Rw`** | `unchecked((int)(3 << 24))` | Rw |
| **`Mask`** | `unchecked((int)(Rw))` | Mask |
| **`Fast`** | `unchecked((int)(1 << 26))` | Fast |

---
### `AlgorithmHint`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `0` | Default |
| **`Accurate`** | `1` | Accurate |
| **`Approx`** | `2` | Approx |

---
### `BorderTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Constant`** | `0` | Constant |
| **`Replicate`** | `1` | Replicate |
| **`Reflect`** | `2` | Reflect |
| **`Wrap`** | `3` | Wrap |
| **`Reflect101`** | `4` | Reflect101 |
| **`Transparent`** | `5` | Transparent |
| **`Default`** | `unchecked((int)(Reflect101))` | Default |
| **`Isolated`** | `16` | Isolated |

---
### `CmpTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Eq`** | `0` | Eq |
| **`Gt`** | `1` | Gt |
| **`Ge`** | `2` | Ge |
| **`Lt`** | `3` | Lt |
| **`Le`** | `4` | Le |
| **`Ne`** | `5` | Ne |

---
### `CovarFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Scrambled`** | `0` | Scrambled |
| **`Normal`** | `1` | Normal |
| **`UseAvg`** | `2` | UseAvg |
| **`Scale`** | `4` | Scale |
| **`Rows`** | `8` | Rows |
| **`Cols`** | `16` | Cols |

---
### `DataLayout`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`DataLayoutUnknown`** | `0` | DataLayoutUnknown |
| **`DataLayoutNd`** | `1` | DataLayoutNd |
| **`DataLayoutNchw`** | `2` | DataLayoutNchw |
| **`DataLayoutNcdhw`** | `3` | DataLayoutNcdhw |
| **`DataLayoutNhwc`** | `4` | DataLayoutNhwc |
| **`DataLayoutNdhwc`** | `5` | DataLayoutNdhwc |
| **`DataLayoutPlanar`** | `6` | DataLayoutPlanar |
| **`DataLayoutBlock`** | `7` | DataLayoutBlock |
| **`DnnLayoutUnknown`** | `0` | DnnLayoutUnknown |
| **`DnnLayoutNd`** | `1` | DnnLayoutNd |
| **`DnnLayoutNchw`** | `2` | DnnLayoutNchw |
| **`DnnLayoutNcdhw`** | `3` | DnnLayoutNcdhw |
| **`DnnLayoutNhwc`** | `4` | DnnLayoutNhwc |
| **`DnnLayoutNdhwc`** | `5` | DnnLayoutNdhwc |
| **`DnnLayoutPlanar`** | `6` | DnnLayoutPlanar |
| **`DnnLayoutBlock`** | `7` | DnnLayoutBlock |

---
### `DecompTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Lu`** | `0` | Lu |
| **`Svd`** | `1` | Svd |
| **`Eig`** | `2` | Eig |
| **`Cholesky`** | `3` | Cholesky |
| **`Qr`** | `4` | Qr |
| **`Normal`** | `16` | Normal |

---
### `DftFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`DftInverse`** | `1` | DftInverse |
| **`DftScale`** | `2` | DftScale |
| **`DftRows`** | `4` | DftRows |
| **`DftComplexOutput`** | `16` | DftComplexOutput |
| **`DftRealOutput`** | `32` | DftRealOutput |
| **`DftComplexInput`** | `64` | DftComplexInput |
| **`DctInverse`** | `unchecked((int)(DftInverse))` | DctInverse |
| **`DctRows`** | `unchecked((int)(DftRows))` | DctRows |

---
### `ErrorCode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`StsOk`** | `0` | StsOk |
| **`StsBackTrace`** | `-1` | StsBackTrace |
| **`StsError`** | `-2` | StsError |
| **`StsInternal`** | `-3` | StsInternal |
| **`StsNoMem`** | `-4` | StsNoMem |
| **`StsBadArg`** | `-5` | StsBadArg |
| **`StsBadFunc`** | `-6` | StsBadFunc |
| **`StsNoConv`** | `-7` | StsNoConv |
| **`StsAutoTrace`** | `-8` | StsAutoTrace |
| **`HeaderIsNull`** | `-9` | HeaderIsNull |
| **`BadImageSize`** | `-10` | BadImageSize |
| **`BadOffset`** | `-11` | BadOffset |
| **`BadDataPtr`** | `-12` | BadDataPtr |
| **`BadStep`** | `-13` | BadStep |
| **`BadModelOrChSeq`** | `-14` | BadModelOrChSeq |
| **`BadNumChannels`** | `-15` | BadNumChannels |
| **`BadNumChannel1U`** | `-16` | BadNumChannel1U |
| **`BadDepth`** | `-17` | BadDepth |
| **`BadAlphaChannel`** | `-18` | BadAlphaChannel |
| **`BadOrder`** | `-19` | BadOrder |
| **`BadOrigin`** | `-20` | BadOrigin |
| **`BadAlign`** | `-21` | BadAlign |
| **`BadCallBack`** | `-22` | BadCallBack |
| **`BadTileSize`** | `-23` | BadTileSize |
| **`BadCOI`** | `-24` | BadCOI |
| **`BadROISize`** | `-25` | BadROISize |
| **`MaskIsTiled`** | `-26` | MaskIsTiled |
| **`StsNullPtr`** | `-27` | StsNullPtr |
| **`StsVecLengthErr`** | `-28` | StsVecLengthErr |
| **`StsFilterStructContentErr`** | `-29` | StsFilterStructContentErr |
| **`StsKernelStructContentErr`** | `-30` | StsKernelStructContentErr |
| **`StsFilterOffsetErr`** | `-31` | StsFilterOffsetErr |
| **`StsBadSize`** | `-201` | StsBadSize |
| **`StsDivByZero`** | `-202` | StsDivByZero |
| **`StsInplaceNotSupported`** | `-203` | StsInplaceNotSupported |
| **`StsObjectNotFound`** | `-204` | StsObjectNotFound |
| **`StsUnmatchedFormats`** | `-205` | StsUnmatchedFormats |
| **`StsBadFlag`** | `-206` | StsBadFlag |
| **`StsBadPoint`** | `-207` | StsBadPoint |
| **`StsBadMask`** | `-208` | StsBadMask |
| **`StsUnmatchedSizes`** | `-209` | StsUnmatchedSizes |
| **`StsUnsupportedFormat`** | `-210` | StsUnsupportedFormat |
| **`StsOutOfRange`** | `-211` | StsOutOfRange |
| **`StsParseError`** | `-212` | StsParseError |
| **`StsNotImplemented`** | `-213` | StsNotImplemented |
| **`StsBadMemBlock`** | `-214` | StsBadMemBlock |
| **`StsAssert`** | `-215` | StsAssert |
| **`GpuNotSupported`** | `-216` | GpuNotSupported |
| **`GpuApiCallError`** | `-217` | GpuApiCallError |
| **`OpenGlNotSupported`** | `-218` | OpenGlNotSupported |
| **`OpenGlApiCallError`** | `-219` | OpenGlApiCallError |
| **`OpenCLApiCallError`** | `-220` | OpenCLApiCallError |
| **`OpenCLDoubleNotSupported`** | `-221` | OpenCLDoubleNotSupported |
| **`OpenCLInitError`** | `-222` | OpenCLInitError |
| **`OpenCLNoAMDBlasFft`** | `-223` | OpenCLNoAMDBlasFft |

---
### `UnnamedEnum3FileNode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Int`** | `1` | Int |
| **`Real`** | `2` | Real |
| **`Float`** | `unchecked((int)(Real))` | Float |
| **`Str`** | `3` | Str |
| **`String`** | `unchecked((int)(Str))` | String |
| **`Seq`** | `4` | Seq |
| **`Map`** | `5` | Map |
| **`TypeMask`** | `7` | TypeMask |
| **`Flow`** | `8` | Flow |
| **`Uniform`** | `8` | Uniform |
| **`Empty`** | `16` | Empty |
| **`Named`** | `32` | Named |

---
### `FileStorageMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Read`** | `0` | Read |
| **`Write`** | `1` | Write |
| **`Append`** | `2` | Append |
| **`Memory`** | `4` | Memory |
| **`FormatMask`** | `unchecked((int)(7 << 3))` | FormatMask |
| **`FormatAuto`** | `0` | FormatAuto |
| **`FormatXml`** | `unchecked((int)(1 << 3))` | FormatXml |
| **`FormatYaml`** | `unchecked((int)(2 << 3))` | FormatYaml |
| **`FormatJson`** | `unchecked((int)(3 << 3))` | FormatJson |
| **`FormatYaml10`** | `unchecked((int)(4 << 3))` | FormatYaml10 |
| **`Base64`** | `64` | Base64 |
| **`WriteBase64`** | `unchecked((int)(Base64 | Write))` | WriteBase64 |

---
### `FileStorageState`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Undefined`** | `0` | Undefined |
| **`ValueExpected`** | `1` | ValueExpected |
| **`NameExpected`** | `2` | NameExpected |
| **`InsideMap`** | `4` | InsideMap |

---
### `FormatterFormatType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `0` | Default |
| **`Matlab`** | `1` | Matlab |
| **`Csv`** | `2` | Csv |
| **`Python`** | `3` | Python |
| **`Numpy`** | `4` | Numpy |
| **`C`** | `5` | C |

---
### `GemmFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_1T`** | `1` | _1T |
| **`_2T`** | `2` | _2T |
| **`_3T`** | `4` | _3T |

---
### `KmeansFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`RandomCenters`** | `0` | RandomCenters |
| **`PpCenters`** | `2` | PpCenters |
| **`UseInitialLabels`** | `1` | UseInitialLabels |

---
### `UnnamedEnum4Mat`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`MagicMask`** | `unchecked((int)(0xFFFF0000))` | MagicMask |
| **`TypeMask`** | `unchecked((int)(0x00000FFF))` | TypeMask |
| **`DepthMask`** | `7` | DepthMask |

---
### `UnnamedEnum5MatShape`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`MaxDims`** | `10` | MaxDims |

---
### `NormTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Inf`** | `1` | Inf |
| **`L1`** | `2` | L1 |
| **`L2`** | `4` | L2 |
| **`L2sqr`** | `5` | L2sqr |
| **`Hamming`** | `6` | Hamming |
| **`Hamming2`** | `7` | Hamming2 |
| **`TypeMask`** | `7` | TypeMask |
| **`Relative`** | `8` | Relative |
| **`Minmax`** | `32` | Minmax |

---
### `PcaFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`DataAsRow`** | `0` | DataAsRow |
| **`DataAsCol`** | `1` | DataAsCol |
| **`UseAvg`** | `2` | UseAvg |

---
### `Param`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Int`** | `0` | Int |
| **`Boolean`** | `1` | Boolean |
| **`Real`** | `2` | Real |
| **`String`** | `3` | String |
| **`Mat`** | `4` | Mat |
| **`MatVector`** | `5` | MatVector |
| **`Algorithm`** | `6` | Algorithm |
| **`Float`** | `7` | Float |
| **`UnsignedInt`** | `8` | UnsignedInt |
| **`Uint64`** | `9` | Uint64 |
| **`Uchar`** | `11` | Uchar |
| **`Scalar`** | `12` | Scalar |

---
### `QuatAssumeType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`NotUnit`** | `0` | NotUnit |
| **`Unit`** | `1` | Unit |

---
### `QuatEnumEulerAnglesType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`IntXyz`** | `0` | IntXyz |
| **`IntXzy`** | `1` | IntXzy |
| **`IntYxz`** | `2` | IntYxz |
| **`IntYzx`** | `3` | IntYzx |
| **`IntZxy`** | `4` | IntZxy |
| **`IntZyx`** | `5` | IntZyx |
| **`IntXyx`** | `6` | IntXyx |
| **`IntXzx`** | `7` | IntXzx |
| **`IntYxy`** | `8` | IntYxy |
| **`IntYzy`** | `9` | IntYzy |
| **`IntZxz`** | `10` | IntZxz |
| **`IntZyz`** | `11` | IntZyz |
| **`ExtXyz`** | `12` | ExtXyz |
| **`ExtXzy`** | `13` | ExtXzy |
| **`ExtYxz`** | `14` | ExtYxz |
| **`ExtYzx`** | `15` | ExtYzx |
| **`ExtZxy`** | `16` | ExtZxy |
| **`ExtZyx`** | `17` | ExtZyx |
| **`ExtXyx`** | `18` | ExtXyx |
| **`ExtXzx`** | `19` | ExtXzx |
| **`ExtYxy`** | `20` | ExtYxy |
| **`ExtYzy`** | `21` | ExtYzy |
| **`ExtZxz`** | `22` | ExtZxz |
| **`ExtZyz`** | `23` | ExtZyz |
| **`EulerAnglesMaxValue`** | `24` | EulerAnglesMaxValue |

---
### `UnnamedEnum6Rng`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Uniform`** | `0` | Uniform |
| **`Normal`** | `1` | Normal |

---
### `ReduceTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Sum`** | `0` | Sum |
| **`Avg`** | `1` | Avg |
| **`Max`** | `2` | Max |
| **`Min`** | `3` | Min |
| **`Sum2`** | `4` | Sum2 |

---
### `RotateFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_90Clockwise`** | `0` | _90Clockwise |
| **`_180`** | `1` | _180 |
| **`_90Counterclockwise`** | `2` | _90Counterclockwise |

---
### `SvdFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ModifyA`** | `1` | ModifyA |
| **`NoUv`** | `2` | NoUv |
| **`FullUv`** | `4` | FullUv |

---
### `SolveLPResult`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Lost`** | `-3` | Lost |
| **`Unbounded`** | `-2` | Unbounded |
| **`Unfeasible`** | `-1` | Unfeasible |
| **`Single`** | `0` | Single |
| **`Multi`** | `1` | Multi |

---
### `SortFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`EveryRow`** | `0` | EveryRow |
| **`EveryColumn`** | `1` | EveryColumn |
| **`Ascending`** | `0` | Ascending |
| **`Descending`** | `16` | Descending |

---
### `UnnamedEnum7SparseMat`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`MagicVal`** | `unchecked((int)(0x42FD0000))` | MagicVal |
| **`MaxDim`** | `32` | MaxDim |
| **`HashScale`** | `unchecked((int)(0x5bd1e995))` | HashScale |
| **`HashBit`** | `unchecked((int)(0x80000000))` | HashBit |

---
### `TermCriteriaType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Count`** | `1` | Count |
| **`MaxIter`** | `unchecked((int)(Count))` | MaxIter |
| **`Eps`** | `2` | Eps |

---
### `UnnamedEnum12UMat`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`MagicMask`** | `unchecked((int)(0xFFFF0000))` | MagicMask |
| **`TypeMask`** | `unchecked((int)(0x00000FFF))` | TypeMask |
| **`DepthMask`** | `7` | DepthMask |

---
### `UMatDataMemoryFlag`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`CopyOnMap`** | `1` | CopyOnMap |
| **`HostCopyObsolete`** | `2` | HostCopyObsolete |
| **`DeviceCopyObsolete`** | `4` | DeviceCopyObsolete |
| **`TempUmat`** | `8` | TempUmat |
| **`TempCopiedUmat`** | `24` | TempCopiedUmat |
| **`UserAllocated`** | `32` | UserAllocated |
| **`DeviceMemMapped`** | `64` | DeviceMemMapped |
| **`AsyncCleanup`** | `128` | AsyncCleanup |

---
### `UMatUsageFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`UsageDefault`** | `0` | UsageDefault |
| **`UsageAllocateHostMemory`** | `unchecked((int)(1 << 0))` | UsageAllocateHostMemory |
| **`UsageAllocateDeviceMemory`** | `unchecked((int)(1 << 1))` | UsageAllocateDeviceMemory |
| **`UsageAllocateSharedMemory`** | `unchecked((int)(1 << 2))` | UsageAllocateSharedMemory |
| **`UmatUsageFlags32bit`** | `unchecked((int)(0x7fffffff))` | UmatUsageFlags32bit |

---
### `InputArrayKindFlag`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`KindShift`** | `16` | KindShift |
| **`FixedType`** | `unchecked((int)(0x8000 << KindShift))` | FixedType |
| **`FixedSize`** | `unchecked((int)(0x4000 << KindShift))` | FixedSize |
| **`KindMask`** | `unchecked((int)(31 << KindShift))` | KindMask |
| **`None`** | `unchecked((int)(0 << KindShift))` | None |
| **`Mat`** | `unchecked((int)(1 << KindShift))` | Mat |
| **`Matx`** | `unchecked((int)(2 << KindShift))` | Matx |
| **`StdVector`** | `unchecked((int)(3 << KindShift))` | StdVector |
| **`StdVectorVector`** | `unchecked((int)(4 << KindShift))` | StdVectorVector |
| **`StdVectorMat`** | `unchecked((int)(5 << KindShift))` | StdVectorMat |
| **`OpenglBuffer`** | `unchecked((int)(7 << KindShift))` | OpenglBuffer |
| **`CudaHostMem`** | `unchecked((int)(8 << KindShift))` | CudaHostMem |
| **`CudaGpuMat`** | `unchecked((int)(9 << KindShift))` | CudaGpuMat |
| **`Umat`** | `unchecked((int)(10 << KindShift))` | Umat |
| **`StdVectorUmat`** | `unchecked((int)(11 << KindShift))` | StdVectorUmat |
| **`StdBoolVector`** | `unchecked((int)(12 << KindShift))` | StdBoolVector |
| **`StdVectorCudaGpuMat`** | `unchecked((int)(13 << KindShift))` | StdVectorCudaGpuMat |
| **`StdArrayMat`** | `unchecked((int)(15 << KindShift))` | StdArrayMat |
| **`CudaGpuMatnd`** | `unchecked((int)(16 << KindShift))` | CudaGpuMatnd |
| **`StdVectorCudaGpuMatNd`** | `unchecked((int)(17 << KindShift))` | StdVectorCudaGpuMatNd |

---
### `OutputArrayDepthMask`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`_8u`** | `unchecked((int)(1 << 0))` | _8u |
| **`_8s`** | `unchecked((int)(1 << 1))` | _8s |
| **`_16u`** | `unchecked((int)(1 << 2))` | _16u |
| **`_16s`** | `unchecked((int)(1 << 3))` | _16s |
| **`_32s`** | `unchecked((int)(1 << 4))` | _32s |
| **`_32f`** | `unchecked((int)(1 << 5))` | _32f |
| **`_64f`** | `unchecked((int)(1 << 6))` | _64f |
| **`_16f`** | `unchecked((int)(1 << 7))` | _16f |
| **`_16bf`** | `unchecked((int)(1 << 8))` | _16bf |
| **`Bool`** | `unchecked((int)(1 << 9))` | Bool |
| **`_64u`** | `unchecked((int)(1 << 10))` | _64u |
| **`_64s`** | `unchecked((int)(1 << 11))` | _64s |
| **`_32u`** | `unchecked((int)(1 << 12))` | _32u |
| **`All`** | `unchecked((int)(1 << 13 - 1))` | All |
| **`AllBut8s`** | `unchecked((int)(All & ~_8s))` | AllBut8s |
| **`All16f`** | `unchecked((int)(All))` | All16f |
| **`Flt`** | `unchecked((int)(_32f + _64f))` | Flt |

---
### `CudaDeviceInfoComputeMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ComputeModeDefault`** | `0` | ComputeModeDefault |
| **`ComputeModeExclusive`** | `1` | ComputeModeExclusive |
| **`ComputeModeProhibited`** | `2` | ComputeModeProhibited |
| **`ComputeModeExclusiveProcess`** | `3` | ComputeModeExclusiveProcess |

---
### `CudaEventCreateFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Default`** | `unchecked((int)(0x00))` | Default |
| **`BlockingSync`** | `unchecked((int)(0x01))` | BlockingSync |
| **`DisableTiming`** | `unchecked((int)(0x02))` | DisableTiming |
| **`Interprocess`** | `unchecked((int)(0x04))` | Interprocess |

---
### `CudaFeatureSet`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FeatureSetCompute10`** | `10` | FeatureSetCompute10 |
| **`FeatureSetCompute11`** | `11` | FeatureSetCompute11 |
| **`FeatureSetCompute12`** | `12` | FeatureSetCompute12 |
| **`FeatureSetCompute13`** | `13` | FeatureSetCompute13 |
| **`FeatureSetCompute20`** | `20` | FeatureSetCompute20 |
| **`FeatureSetCompute21`** | `21` | FeatureSetCompute21 |
| **`FeatureSetCompute30`** | `30` | FeatureSetCompute30 |
| **`FeatureSetCompute32`** | `32` | FeatureSetCompute32 |
| **`FeatureSetCompute35`** | `35` | FeatureSetCompute35 |
| **`FeatureSetCompute50`** | `50` | FeatureSetCompute50 |
| **`GlobalAtomics`** | `unchecked((int)(FeatureSetCompute11))` | GlobalAtomics |
| **`SharedAtomics`** | `unchecked((int)(FeatureSetCompute12))` | SharedAtomics |
| **`NativeDouble`** | `unchecked((int)(FeatureSetCompute13))` | NativeDouble |
| **`WarpShuffleFunctions`** | `unchecked((int)(FeatureSetCompute30))` | WarpShuffleFunctions |
| **`DynamicParallelism`** | `unchecked((int)(FeatureSetCompute35))` | DynamicParallelism |

---
### `CudaHostMemAllocType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`PageLocked`** | `1` | PageLocked |
| **`Shared`** | `2` | Shared |
| **`WriteCombined`** | `4` | WriteCombined |

---
### `DetailTestOp`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Custom`** | `0` | Custom |
| **`Eq`** | `1` | Eq |
| **`Ne`** | `2` | Ne |
| **`Le`** | `3` | Le |
| **`Lt`** | `4` | Lt |
| **`Ge`** | `5` | Ge |
| **`Gt`** | `6` | Gt |

---
### `InstrFlags`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Mapping`** | `unchecked((int)(0x01))` | Mapping |
| **`ExpandSameNames`** | `unchecked((int)(0x02))` | ExpandSameNames |

---
### `InstrImpl`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Plain`** | `0` | Plain |
| **`Ipp`** | `unchecked((int)(0 + 1))` | Ipp |
| **`Opencl`** | `unchecked((int)(0 + 2))` | Opencl |

---
### `InstrType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`General`** | `0` | General |
| **`Marker`** | `unchecked((int)(0 + 1))` | Marker |
| **`Wrapper`** | `unchecked((int)(0 + 2))` | Wrapper |
| **`Fun`** | `unchecked((int)(0 + 3))` | Fun |

---
### `UnnamedEnum17OclDevice`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`UnknownVendor`** | `0` | UnknownVendor |
| **`VendorAmd`** | `1` | VendorAmd |
| **`VendorIntel`** | `2` | VendorIntel |
| **`VendorNvidia`** | `3` | VendorNvidia |

---
### `UnnamedEnum18OclKernelArg`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Local`** | `1` | Local |
| **`ReadOnly`** | `2` | ReadOnly |
| **`WriteOnly`** | `4` | WriteOnly |
| **`ReadWrite`** | `6` | ReadWrite |
| **`Constant`** | `8` | Constant |
| **`PtrOnly`** | `16` | PtrOnly |
| **`NoSize`** | `256` | NoSize |

---
### `OclOclVectorStrategy`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Own`** | `0` | Own |
| **`Max`** | `1` | Max |
| **`Default`** | `unchecked((int)(Own))` | Default |

---
### `OglBufferAccess`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ReadOnly`** | `unchecked((int)(0x88B8))` | ReadOnly |
| **`WriteOnly`** | `unchecked((int)(0x88B9))` | WriteOnly |
| **`ReadWrite`** | `unchecked((int)(0x88BA))` | ReadWrite |

---
### `OglBufferTarget`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`ArrayBuffer`** | `unchecked((int)(0x8892))` | ArrayBuffer |
| **`ElementArrayBuffer`** | `unchecked((int)(0x8893))` | ElementArrayBuffer |
| **`PixelPackBuffer`** | `unchecked((int)(0x88EB))` | PixelPackBuffer |
| **`PixelUnpackBuffer`** | `unchecked((int)(0x88EC))` | PixelUnpackBuffer |

---
### `OglRenderModes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Points`** | `unchecked((int)(0x0000))` | Points |
| **`Lines`** | `unchecked((int)(0x0001))` | Lines |
| **`LineLoop`** | `unchecked((int)(0x0002))` | LineLoop |
| **`LineStrip`** | `unchecked((int)(0x0003))` | LineStrip |
| **`Triangles`** | `unchecked((int)(0x0004))` | Triangles |
| **`TriangleStrip`** | `unchecked((int)(0x0005))` | TriangleStrip |
| **`TriangleFan`** | `unchecked((int)(0x0006))` | TriangleFan |
| **`Quads`** | `unchecked((int)(0x0007))` | Quads |
| **`QuadStrip`** | `unchecked((int)(0x0008))` | QuadStrip |
| **`Polygon`** | `unchecked((int)(0x0009))` | Polygon |

---
### `OglTexture2DFormat`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`DepthComponent`** | `unchecked((int)(0x1902))` | DepthComponent |
| **`Rgb`** | `unchecked((int)(0x1907))` | Rgb |
| **`Rgba`** | `unchecked((int)(0x1908))` | Rgba |

---
### `UtilsLoggingLogLevel`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`LogLevelSilent`** | `0` | LogLevelSilent |
| **`LogLevelFatal`** | `1` | LogLevelFatal |
| **`LogLevelError`** | `2` | LogLevelError |
| **`LogLevelWarning`** | `3` | LogLevelWarning |
| **`LogLevelInfo`** | `4` | LogLevelInfo |
| **`LogLevelDebug`** | `5` | LogLevelDebug |
| **`LogLevelVerbose`** | `6` | LogLevelVerbose |
| **`EnumLogLevelForceInt`** | `2147483647` | EnumLogLevelForceInt |

---

</div>