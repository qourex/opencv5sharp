# PTCLOUD Module API Reference

Complete documentation for the **PTCLOUD** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Ptcloud Documentation](https://docs.opencv.org/5.x/main_modules/ptcloud.html).

---
<div v-pre>

## 📦 Classes and Structs

### `Octree`
**Inherits from**: `DisposableOpenCVObject`

Octree for 3D vision. * * In 3D vision filed, the Octree is used to process and accelerate the pointcloud data. The class Octree represents * the Octree data structure. Each Octree will have a fixed depth. The depth of Octree refers to the distance from * the root node to the leaf node.All OctreeNodes will not exceed this depth.Increasing the depth will increase * the amount of calculation exponentially. And the small number of depth refers low resolution of Octree. * Each node contains 8 children, which are used to divide the space cube into eight parts. Each octree node represents * a cube. And these eight children will have a fixed order, the order is described as follows: * * For illustration, assume, * * rootNode: origin == (0, 0, 0), size == 2 * * Then, * * children[0]: origin == (0, 0, 0), size == 1 * * children[1]: origin == (1, 0, 0), size == 1, along X-axis next to child 0 * * children[2]: origin == (0, 1, 0), size == 1, along Y-axis next to child 0 * * children[3]: origin == (1, 1, 0), size == 1, in X-Y plane * * children[4]: origin == (0, 0, 1), size == 1, along Z-axis next to child 0 * * children[5]: origin == (1, 0, 1), size == 1, in X-Z plane * * children[6]: origin == (0, 1, 1), size == 1, in Y-Z plane * * children[7]: origin == (1, 1, 1), size == 1, furthest from child 0

#### Methods
* `Octree? CreateWithDepth(int maxDepth, double size, IntPtr origin, bool withColors)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  Creates an empty Octree with given maximum depth

* * **Parameter** `maxDepth`:  The max depth of the Octree
* * **Parameter** `size`:  bounding box size for the Octree
* * **Parameter** `origin`:  Initial center coordinate
* * **Parameter** `withColors`:  Whether to keep per-point colors or not
**Returns**: resulting Octree

  * *Parameter* `maxDepth`: The maxDepth parameter.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `origin`: The origin parameter.
  * *Parameter* `withColors`: The withColors parameter.
  * *Returns*: The returned value.
* `Octree? CreateWithDepth(int maxDepth, Mat pointCloud, Mat? colors)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  Create an Octree from the PointCloud data with the specific maxDepth

* * **Parameter** `maxDepth`:  Max depth of the octree
* * **Parameter** `pointCloud`:  point cloud data, should be 3-channel float array
* * **Parameter** `colors`:  color attribute of point cloud in the same 3-channel float format
**Returns**: resulting Octree

  * *Parameter* `maxDepth`: The maxDepth parameter.
  * *Parameter* `pointCloud`: The pointCloud parameter.
  * *Parameter* `colors`: The colors parameter.
  * *Returns*: The returned value.
* `Octree? CreateWithResolution(double resolution, double size, IntPtr origin, bool withColors)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  Creates an empty Octree with given resolution

* * **Parameter** `resolution`:  The size of the octree leaf node
* * **Parameter** `size`:  bounding box size for the Octree
* * **Parameter** `origin`:  Initial center coordinate
* * **Parameter** `withColors`:  Whether to keep per-point colors or not
**Returns**: resulting Octree

  * *Parameter* `resolution`: The resolution parameter.
  * *Parameter* `size`: The size parameter.
  * *Parameter* `origin`: The origin parameter.
  * *Parameter* `withColors`: The withColors parameter.
  * *Returns*: The returned value.
* `Octree? CreateWithResolution(double resolution, Mat pointCloud, Mat? colors)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  Create an Octree from the PointCloud data with the specific resolution

* * **Parameter** `resolution`:  The size of the octree leaf node
* * **Parameter** `pointCloud`:  point cloud data, should be 3-channel float array
* * **Parameter** `colors`:  color attribute of point cloud in the same 3-channel float format
**Returns**: resulting octree

  * *Parameter* `resolution`: The resolution parameter.
  * *Parameter* `pointCloud`: The pointCloud parameter.
  * *Parameter* `colors`: The colors parameter.
  * *Returns*: The returned value.
* `bool InsertPoint(IntPtr point, IntPtr color)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*  Insert a point data with color to a OctreeNode.

* * **Parameter** `point`:  The point data in Point3f format.
* * **Parameter** `color`:  The color attribute of point in Point3f format.
**Returns**: Returns whether the insertion is successful.

  * *Parameter* `point`: The point parameter.
  * *Parameter* `color`: Color value (BGR or BGRA).
  * *Returns*: The returned value.
* `bool IsPointInBound(IntPtr point)`
  * *Summary*: Determine whether the point is within the space range of the specific cube. * * **point** The point coordinates. **Returns**: If point is in bound, return ture. Otherwise, false.
  * *Parameter* `point`: The point parameter.
  * *Returns*: The returned value.
* `bool Empty()`
  * *Summary*: Returns whether the Octree is empty (contains no data).
  * *Returns*: `true` if the Octree is empty; otherwise, `false`.
* `void Clear()`
  * *Summary*: Reset all octree parameter. * *  Clear all the nodes of the octree and initialize the parameters.
* `bool DeletePoint(IntPtr point)`
  * *Summary*: Delete a given point from the Octree. * * Delete the corresponding element from the pointList in the corresponding leaf node. If the leaf node * does not contain other points after deletion, this node will be deleted. In the same way, * its parent node may also be deleted if its last child is deleted. * **point** The point coordinates, comparison is epsilon-based **Returns**: return ture if the point is deleted successfully.
  * *Parameter* `point`: The point parameter.
  * *Returns*: The returned value.
* `void GetPointCloudByOctree(Mat restoredPointCloud, Mat? restoredColor)`
  * *Summary*: restore point cloud data from Octree. * * Restore the point cloud data from existing octree. The points in same leaf node will be seen as the same point. * This point is the center of the leaf node. If the resolution is small, it will work as a downSampling function. * **restoredPointCloud** The output point cloud data, can be replaced by null if not needed * **restoredColor** The color attribute of point cloud data, can be omitted if not needed
  * *Parameter* `restoredPointCloud`: The restoredPointCloud parameter.
  * *Parameter* `restoredColor`: The restoredColor parameter.
* `int RadiusNNSearch(IntPtr query, float radius, Mat points, Mat? squareDists)`
  * *Summary*: Radius Nearest Neighbor Search in Octree. * * Search all points that are less than or equal to radius. * And return the number of searched points. * **query** Query point. * **radius** Retrieved radius value. * **points** Point output. Contains searched points in 3-float format, and output vector is not in order, * can be replaced by null if not needed * **squareDists** Dist output. Contains searched squared distance in floats, and output vector is not in order, * can be omitted if not needed **Returns**: the number of searched points.
  * *Parameter* `query`: The query parameter.
  * *Parameter* `radius`: The radius parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `squareDists`: The squareDists parameter.
  * *Returns*: The returned value.
* `int RadiusNNSearch(IntPtr query, float radius, Mat points, Mat colors, Mat squareDists)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*   Radius Nearest Neighbor Search in Octree.

* Search all points that are less than or equal to radius.
* And return the number of searched points.
* * **Parameter** `query`:  Query point.
* * **Parameter** `radius`:  Retrieved radius value.
* * **Parameter** `points`:  Point output. Contains searched points in 3-float format, and output vector is not in order,
* can be replaced by null if not needed
* * **Parameter** `colors`:  Color output. Contains colors corresponding to points in pointSet, can be replaced by null if not needed
* * **Parameter** `squareDists`:  Dist output. Contains searched squared distance in floats, and output vector is not in order,
* can be replaced by null if not needed
**Returns**: the number of searched points.

  * *Parameter* `query`: The query parameter.
  * *Parameter* `radius`: The radius parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `colors`: The colors parameter.
  * *Parameter* `squareDists`: The squareDists parameter.
  * *Returns*: The returned value.
* `void KNNSearch(IntPtr query, int K, Mat points, Mat? squareDists)`
  * *Summary*: K Nearest Neighbor Search in Octree. * * Find the K nearest neighbors to the query point. * **query** Query point. * **K** amount of nearest neighbors to find * **points** Point output. Contains K points in 3-float format, arranged in order of distance from near to far, * can be replaced by null if not needed * **squareDists** Dist output. Contains K squared distance in floats, arranged in order of distance from near to far, * can be omitted if not needed
  * *Parameter* `query`: The query parameter.
  * *Parameter* `K`: The K parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `squareDists`: The squareDists parameter.
* `void KNNSearch(IntPtr query, int K, Mat points, Mat colors, Mat squareDists)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*   K Nearest Neighbor Search in Octree.

* Find the K nearest neighbors to the query point.
* * **Parameter** `query`:  Query point.
* * **Parameter** `K`:  amount of nearest neighbors to find
* * **Parameter** `points`:  Point output. Contains K points in 3-float format, arranged in order of distance from near to far,
* can be replaced by null if not needed
* * **Parameter** `colors`:  Color output. Contains colors corresponding to points in pointSet, can be replaced by null if not needed
* * **Parameter** `squareDists`:  Dist output. Contains K squared distance in floats, arranged in order of distance from near to far,
* can be replaced by null if not needed

  * *Parameter* `query`: The query parameter.
  * *Parameter* `K`: The K parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `colors`: The colors parameter.
  * *Parameter* `squareDists`: The squareDists parameter.

---
### `Odometry`
**Inherits from**: `DisposableOpenCVObject`

Computes camera motion estimation (visual odometry) between frames using depth and/or RGB data. Supports depth-only, RGB-only, and combined RGB-D odometry types, with configurable algorithm settings.

#### Constructors
* `new Odometry()`
  * *Summary*: Creates a new Odometry instance with default settings (depth-based odometry, common algorithm).
* `new Odometry(OdometryType otype)`
  * *Summary*: Creates a new Odometry instance with the specified odometry type.
  * *Parameter* `otype`: The type of odometry to use (Depth, Rgb, or RgbDepth).
* `new Odometry(OdometryType otype, OdometrySettings settings, OdometryAlgoType algtype)`
  * *Summary*: Creates a new Odometry instance with the specified odometry type, custom settings, and algorithm type.
  * *Parameter* `otype`: The type of odometry to use (Depth, Rgb, or RgbDepth).
  * *Parameter* `settings`: The custom odometry settings (camera matrix, iteration counts, thresholds, etc.).
  * *Parameter* `algtype`: The algorithm type to use (Common or Fast).

#### Methods
* `void PrepareFrame(OdometryFrame frame)`
  * *Summary*: Prepare frame for odometry calculation
  * *Remarks*:

* * **Parameter** `frame`:  odometry prepare this frame as src frame and dst frame simultaneously

  * *Parameter* `frame`: The frame parameter.
* `void PrepareFrames(OdometryFrame srcFrame, OdometryFrame dstFrame)`
  * *Summary*: Prepare frame for odometry calculation
  * *Remarks*:

* * **Parameter** `srcFrame`:  frame will be prepared as src frame ("original" image)
* * **Parameter** `dstFrame`:  frame will be prepared as dsr frame ("rotated" image)

  * *Parameter* `srcFrame`: The srcFrame parameter.
  * *Parameter* `dstFrame`: The dstFrame parameter.
* `bool Compute(OdometryFrame srcFrame, OdometryFrame dstFrame, Mat Rt)`
  * *Summary*: Compute Rigid Transformation between two frames so that Rt * src = dst
  * *Remarks*:

* Both frames, source and destination, should have been prepared by calling prepareFrame() first

* * **Parameter** `srcFrame`:  src frame ("original" image)
* * **Parameter** `dstFrame`:  dst frame ("rotated" image)
* * **Parameter** `Rt`:  Rigid transformation, which will be calculated, in form:
* { R_11 R_12 R_13 t_1
*   R_21 R_22 R_23 t_2
*   R_31 R_32 R_33 t_3
*   0    0    0    1  }
**Returns**: true on success, false if failed to find the transformation

  * *Parameter* `srcFrame`: The srcFrame parameter.
  * *Parameter* `dstFrame`: The dstFrame parameter.
  * *Parameter* `Rt`: The Rt parameter.
  * *Returns*: The returned value.
* `bool Compute(Mat srcDepth, Mat dstDepth, Mat Rt)`
  * *Summary*: *  Compute Rigid Transformation between two frames so that Rt * src = dst
  * *Remarks*:

* * **Parameter** `srcDepth`:  source depth ("original" image)
* * **Parameter** `dstDepth`:  destination depth ("rotated" image)
* * **Parameter** `Rt`:  Rigid transformation, which will be calculated, in form:
* { R_11 R_12 R_13 t_1
*   R_21 R_22 R_23 t_2
*   R_31 R_32 R_33 t_3
*   0    0    0    1  }
**Returns**: true on success, false if failed to find the transformation

  * *Parameter* `srcDepth`: The srcDepth parameter.
  * *Parameter* `dstDepth`: The dstDepth parameter.
  * *Parameter* `Rt`: The Rt parameter.
  * *Returns*: The returned value.
* `bool Compute(Mat srcDepth, Mat srcRGB, Mat dstDepth, Mat dstRGB, Mat Rt)`
  * *Summary*: *  Compute Rigid Transformation between two frames so that Rt * src = dst
  * *Remarks*:

* * **Parameter** `srcDepth`:  source depth ("original" image)
* * **Parameter** `srcRGB`:  source RGB
* * **Parameter** `dstDepth`:  destination depth ("rotated" image)
* * **Parameter** `dstRGB`:  destination RGB
* * **Parameter** `Rt`:  Rigid transformation, which will be calculated, in form:
* { R_11 R_12 R_13 t_1
*   R_21 R_22 R_23 t_2
*   R_31 R_32 R_33 t_3
*   0    0    0    1  }
**Returns**: true on success, false if failed to find the transformation

  * *Parameter* `srcDepth`: The srcDepth parameter.
  * *Parameter* `srcRGB`: The srcRGB parameter.
  * *Parameter* `dstDepth`: The dstDepth parameter.
  * *Parameter* `dstRGB`: The dstRGB parameter.
  * *Parameter* `Rt`: The Rt parameter.
  * *Returns*: The returned value.
* `RgbdNormals? GetNormalsComputer()`
  * *Summary*: *  Get the normals computer object used for normals calculation (if presented).
  * *Remarks*:

* The normals computer is generated at first need during prepareFrame when normals are required for the ICP algorithm
* but not presented by a user. Re-generated each time the related settings change or a new frame arrives with the different size.

  * *Returns*: The returned value.

---
### `OdometryFrame`
**Inherits from**: `DisposableOpenCVObject`

*  An object that keeps per-frame data for Odometry algorithms from user-provided images to algorithm-specific precalculated data.

**Detailed Remarks**:
* When not empty, it contains a depth image, a mask of valid pixels and a set of pyramids generated from that data.
* A BGR/Gray image and normals are optional.
* OdometryFrame is made to be used together with Odometry class to reuse precalculated data between Rt data calculations.
* A correct way to do that is to call Odometry.prepareFrames() on prev and next frames and then pass them to Odometry.compute() method.

#### Constructors
* `new OdometryFrame(Mat? depth, Mat? image, Mat? mask, Mat? normals)`
  * *Summary*: *  Construct a new OdometryFrame object. All non-empty images should have the same size.
  * *Parameter* `depth`: The depth parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `mask`: Optional operation mask.
  * *Parameter* `normals`: The normals parameter.

#### Methods
* `void GetImage(Mat image)`
  * *Summary*: *  Get the original user-provided BGR/Gray image
  * *Remarks*:

* * **Parameter** `image`:  Output image

  * *Parameter* `image`: Input image.
* `void GetGrayImage(Mat image)`
  * *Summary*: *  Get the gray image generated from the user-provided BGR/Gray image
  * *Remarks*:

* * **Parameter** `image`:  Output image

  * *Parameter* `image`: Input image.
* `void GetDepth(Mat depth)`
  * *Summary*: *  Get the original user-provided depth image
  * *Remarks*:

* * **Parameter** `depth`:  Output image

  * *Parameter* `depth`: The depth parameter.
* `void GetProcessedDepth(Mat depth)`
  * *Summary*: *  Get the depth image generated from the user-provided one after conversion, rescale or filtering for ICP algorithm needs
  * *Remarks*:

* * **Parameter** `depth`:  Output image

  * *Parameter* `depth`: The depth parameter.
* `void GetMask(Mat mask)`
  * *Summary*: *  Get the valid pixels mask generated for the ICP calculations intersected with the user-provided mask
  * *Remarks*:

* * **Parameter** `mask`:  Output image

  * *Parameter* `mask`: Optional operation mask.
* `void GetNormals(Mat normals)`
  * *Summary*: *  Get the normals image either generated for the ICP calculations or user-provided
  * *Remarks*:

* * **Parameter** `normals`:  Output image

  * *Parameter* `normals`: The normals parameter.
* `int GetPyramidLevels()`
  * *Summary*: *  Get the amount of levels in pyramids (all of them if not empty should have the same number of levels)
  * *Remarks*:

* or 0 if no pyramids were prepared yet

  * *Returns*: The returned value.
* `void GetPyramidAt(Mat img, OdometryFramePyramidType pyrType, long level)`
  * *Summary*: *  Get the image generated for the ICP calculations from one of the pyramids specified by pyrType. Returns empty image if
  * *Remarks*:

* the pyramid is empty or there's no such pyramid level

* * **Parameter** `img`:  Output image
* * **Parameter** `pyrType`:  Type of pyramid
* * **Parameter** `level`:  Level in the pyramid

  * *Parameter* `img`: Input image.
  * *Parameter* `pyrType`: The pyrType parameter.
  * *Parameter* `level`: The level parameter.

---
### `OdometrySettings`
**Inherits from**: `DisposableOpenCVObject`

Configuration settings for the Odometry algorithm, including camera matrix, iteration counts, depth thresholds, Sobel operator parameters, normal computation settings, and motion constraints.

#### Constructors
* `new OdometrySettings()`
  * *Summary*: Creates a new OdometrySettings instance with default values.

#### Methods
* `void SetCameraMatrix(Mat val)`
  * *Summary*: Sets the camera intrinsic matrix used for odometry computation.
  * *Parameter* `val`: The camera intrinsic matrix.
* `void GetCameraMatrix(Mat val)`
  * *Summary*: Returns the camera intrinsic matrix used for odometry computation.
  * *Parameter* `val`: Output matrix to receive the camera intrinsic matrix.
* `void SetIterCounts(Mat val)`
  * *Summary*: Sets the number of iterations for each pyramid level during odometry computation.
  * *Parameter* `val`: A Mat containing the iteration count for each level.
* `void GetIterCounts(Mat val)`
  * *Summary*: Returns the number of iterations for each pyramid level.
  * *Parameter* `val`: Output Mat to receive the iteration counts.
* `void SetMinDepth(float val)`
  * *Summary*: Sets the minimum depth value to be used in odometry computation. Points closer than this are discarded.
  * *Parameter* `val`: The minimum depth value in meters.
* `float GetMinDepth()`
  * *Summary*: Returns the minimum depth value used in odometry computation.
  * *Returns*: The minimum depth value in meters.
* `void SetMaxDepth(float val)`
  * *Summary*: Sets the maximum depth value to be used in odometry computation. Points farther than this are discarded.
  * *Parameter* `val`: The maximum depth value in meters.
* `float GetMaxDepth()`
  * *Summary*: Returns the maximum depth value used in odometry computation.
  * *Returns*: The maximum depth value in meters.
* `void SetMaxDepthDiff(float val)`
  * *Summary*: Sets the maximum allowed depth difference between corresponding points in source and destination frames.
  * *Parameter* `val`: The maximum depth difference value.
* `float GetMaxDepthDiff()`
  * *Summary*: Returns the maximum allowed depth difference between corresponding points.
  * *Returns*: The maximum depth difference value.
* `void SetMaxPointsPart(float val)`
  * *Summary*: Sets the maximum fraction of points used in the odometry computation.
  * *Parameter* `val`: The maximum fraction (0.0 to 1.0) of points to use.
* `float GetMaxPointsPart()`
  * *Summary*: Returns the maximum fraction of points used in the odometry computation.
  * *Returns*: The maximum fraction of points.
* `void SetSobelSize(int val)`
  * *Summary*: Sets the size of the Sobel kernel used for gradient computation in RGB odometry.
  * *Parameter* `val`: The Sobel kernel size.
* `int GetSobelSize()`
  * *Summary*: Returns the size of the Sobel kernel used for gradient computation.
  * *Returns*: The Sobel kernel size.
* `void SetSobelScale(double val)`
  * *Summary*: Sets the scale factor applied to the Sobel operator output.
  * *Parameter* `val`: The Sobel scale factor.
* `double GetSobelScale()`
  * *Summary*: Returns the scale factor applied to the Sobel operator output.
  * *Returns*: The Sobel scale factor.
* `void SetNormalWinSize(int val)`
  * *Summary*: Sets the window size used for normal vector computation.
  * *Parameter* `val`: The normal computation window size.
* `int GetNormalWinSize()`
  * *Summary*: Returns the window size used for normal vector computation.
  * *Returns*: The normal computation window size.
* `void SetNormalDiffThreshold(float val)`
  * *Summary*: Sets the threshold for normal vector differences used in point matching.
  * *Parameter* `val`: The normal difference threshold.
* `float GetNormalDiffThreshold()`
  * *Summary*: Returns the threshold for normal vector differences.
  * *Returns*: The normal difference threshold.
* `void SetNormalMethod(IntPtr nm)`
  * *Summary*: Sets the method used to compute surface normals.
  * *Parameter* `nm`: The normal computation method.
* `IntPtr GetNormalMethod()`
  * *Summary*: Returns the method used to compute surface normals.
  * *Returns*: The normal computation method.
* `void SetAngleThreshold(float val)`
  * *Summary*: Sets the angle threshold used to filter correspondences during odometry.
  * *Parameter* `val`: The angle threshold value.
* `float GetAngleThreshold()`
  * *Summary*: Returns the angle threshold used to filter correspondences.
  * *Returns*: The angle threshold value.
* `void SetMaxTranslation(float val)`
  * *Summary*: Sets the maximum allowed translation magnitude for the computed rigid transformation.
  * *Parameter* `val`: The maximum translation value.
* `float GetMaxTranslation()`
  * *Summary*: Returns the maximum allowed translation magnitude.
  * *Returns*: The maximum translation value.
* `void SetMaxRotation(float val)`
  * *Summary*: Sets the maximum allowed rotation angle for the computed rigid transformation.
  * *Parameter* `val`: The maximum rotation value.
* `float GetMaxRotation()`
  * *Summary*: Returns the maximum allowed rotation angle.
  * *Returns*: The maximum rotation value.
* `void SetMinGradientMagnitude(float val)`
  * *Summary*: Sets the minimum image gradient magnitude for a pixel to be used in RGB odometry.
  * *Parameter* `val`: The minimum gradient magnitude.
* `float GetMinGradientMagnitude()`
  * *Summary*: Returns the minimum image gradient magnitude for RGB odometry.
  * *Returns*: The minimum gradient magnitude.
* `void SetMinGradientMagnitudes(Mat val)`
  * *Summary*: Sets the per-pyramid-level minimum gradient magnitudes for RGB odometry.
  * *Parameter* `val`: A Mat containing the minimum gradient magnitude for each pyramid level.
* `void GetMinGradientMagnitudes(Mat val)`
  * *Summary*: Returns the per-pyramid-level minimum gradient magnitudes.
  * *Parameter* `val`: Output Mat to receive the minimum gradient magnitudes for each pyramid level.

---
### `RgbdNormals`
**Inherits from**: `DisposableOpenCVObject`

Object that can compute the normals in an image.

**Detailed Remarks**:
* It is an object as it can cache data for speed efficiency
* The implemented methods are either:
* - FALS (the fastest) and SRI from
* ``Fast and Accurate Computation of Surface Normals from Range Images``
* by H. Badino, D. Huber, Y. Park and T. Kanade
* - the normals with bilateral filtering on a depth image from
* ``Gradient Response Maps for Real-Time Detection of Texture-Less Objects``
* by S. Hinterstoisser, C. Cagniart, S. Ilic, P. Sturm, N. Navab, P. Fua, and V. Lepetit

#### Methods
* `RgbdNormals? Create(int rows, int cols, int depth, Mat? K, int window_size, float diff_threshold, IntPtr method)`
  * *Summary*: Creates new RgbdNormals object
  * *Remarks*:

* * **Parameter** `rows`:  the number of rows of the depth image normals will be computed on
* * **Parameter** `cols`:  the number of cols of the depth image normals will be computed on
* * **Parameter** `depth`:  the depth of the normals (only CV_32F or CV_64F)
* * **Parameter** `K`:  the calibration matrix to use
* * **Parameter** `window_size`:  the window size to compute the normals: can only be 1,3,5 or 7
* * **Parameter** `diff_threshold`:  threshold in depth difference, used in LINEMOD algirithm
* * **Parameter** `method`:  one of the methods to use: RGBD_NORMALS_METHOD_SRI, RGBD_NORMALS_METHOD_FALS

  * *Parameter* `rows`: The rows parameter.
  * *Parameter* `cols`: The cols parameter.
  * *Parameter* `depth`: The depth parameter.
  * *Parameter* `K`: The K parameter.
  * *Parameter* `window_size`: The window_size parameter.
  * *Parameter* `diff_threshold`: The diff_threshold parameter.
  * *Parameter* `method`: The method parameter.
  * *Returns*: The returned value.
* `void Apply(Mat points, Mat normals)`
  * *Summary*: Given a set of 3d points in a depth image, compute the normals at each point.
  * *Remarks*:

* * **Parameter** `points`:  a rows x cols x 3 matrix of CV_32F/CV64F or a rows x cols x 1 CV_U16S
* * **Parameter** `normals`:  a rows x cols x 3 matrix

  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
* `void Cache()`
  * *Summary*: Prepares cached data required for calculation
  * *Remarks*:

* If not called by user, called automatically at first calculation

* `int GetRows()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetRows(int val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `int GetCols()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetCols(int val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `int GetWindowSize()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void SetWindowSize(int val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `int GetDepth()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.
* `void GetK(Mat val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `void SetK(Mat val)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `val`: The val parameter.
* `IntPtr GetMethod()`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Returns*: The returned value.

---
### `TriangleRasterizeSettings`
**Inherits from**: `DisposableOpenCVObject`

*  Structure to keep settings for rasterization

#### Methods
* `TriangleRasterizeSettings? SetShadingType(TriangleShadingType st)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `st`: The st parameter.
  * *Returns*: The returned value.
* `TriangleRasterizeSettings? SetCullingMode(TriangleCullingMode cm)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `cm`: The cm parameter.
  * *Returns*: The returned value.
* `TriangleRasterizeSettings? SetGlCompatibleMode(TriangleGlCompatibleMode gm)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `gm`: The gm parameter.
  * *Returns*: The returned value.

---
### `Volume`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new Volume(VolumeType vtype, VolumeSettings? settings)`
  * *Summary*: Constructor of custom volume. * **vtype** the volume type [TSDF, HashTSDF, ColorTSDF]. * **settings** the custom settings for volume.
  * *Parameter* `vtype`: The vtype parameter.
  * *Parameter* `settings`: The settings parameter.

#### Methods
* `void Integrate(OdometryFrame frame, Mat pose)`
  * *Summary*: Integrates the input data to the volume.
  * *Remarks*:

Camera intrinsics are taken from volume settings structure.
* * **Parameter** `frame`:  the object from which to take depth and image data.
For color TSDF a depth data should be registered with color data, i.e. have the same intrinsics & camera pose.
This can be done using function registerDepth() from 3d module.
* * **Parameter** `pose`:  the pose of camera in global coordinates.

  * *Parameter* `frame`: The frame parameter.
  * *Parameter* `pose`: The pose parameter.
* `void Integrate(Mat depth, Mat pose)`
  * *Summary*: Integrates the input data to the volume.
  * *Remarks*:

Camera intrinsics are taken from volume settings structure.
* * **Parameter** `depth`:  the depth image.
* * **Parameter** `pose`:  the pose of camera in global coordinates.

  * *Parameter* `depth`: The depth parameter.
  * *Parameter* `pose`: The pose parameter.
* `void Integrate(Mat depth, Mat image, Mat pose)`
  * *Summary*: Integrates the input data to the volume.
  * *Remarks*:

Camera intrinsics are taken from volume settings structure.
* * **Parameter** `depth`:  the depth image.
* * **Parameter** `image`:  the color image (only for ColorTSDF).
For color TSDF a depth data should be registered with color data, i.e. have the same intrinsics & camera pose.
This can be done using function registerDepth() from 3d module.
* * **Parameter** `pose`:  the pose of camera in global coordinates.

  * *Parameter* `depth`: The depth parameter.
  * *Parameter* `image`: Input image.
  * *Parameter* `pose`: The pose parameter.
* `void Raycast(Mat cameraPose, Mat points, Mat normals)`
  * *Summary*: Renders the volume contents into an image. The resulting points and normals are in camera's coordinate system.
  * *Remarks*:

Rendered image size and camera intrinsics are taken from volume settings structure.
* * **Parameter** `cameraPose`:  the pose of camera in global coordinates.
* * **Parameter** `points`:  image to store rendered points.
* * **Parameter** `normals`:  image to store rendered normals corresponding to points.

  * *Parameter* `cameraPose`: The cameraPose parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
* `void Raycast(Mat cameraPose, Mat points, Mat normals, Mat colors)`
  * *Summary*: Renders the volume contents into an image. The resulting points and normals are in camera's coordinate system.
  * *Remarks*:

Rendered image size and camera intrinsics are taken from volume settings structure.
* * **Parameter** `cameraPose`:  the pose of camera in global coordinates.
* * **Parameter** `points`:  image to store rendered points.
* * **Parameter** `normals`:  image to store rendered normals corresponding to points.
* * **Parameter** `colors`:  image to store rendered colors corresponding to points (only for ColorTSDF).

  * *Parameter* `cameraPose`: The cameraPose parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
  * *Parameter* `colors`: The colors parameter.
* `void Raycast(Mat cameraPose, int height, int width, Mat K, Mat points, Mat normals)`
  * *Summary*: Renders the volume contents into an image. The resulting points and normals are in camera's coordinate system.
  * *Remarks*:

Rendered image size and camera intrinsics are taken from volume settings structure.
* * **Parameter** `cameraPose`:  the pose of camera in global coordinates.
* * **Parameter** `height`:  the height of result image
* * **Parameter** `width`:  the width of result image
* * **Parameter** `K`:  camera raycast intrinsics
* * **Parameter** `points`:  image to store rendered points.
* * **Parameter** `normals`:  image to store rendered normals corresponding to points.

  * *Parameter* `cameraPose`: The cameraPose parameter.
  * *Parameter* `height`: The height parameter.
  * *Parameter* `width`: The width parameter.
  * *Parameter* `K`: The K parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
* `void Raycast(Mat cameraPose, int height, int width, Mat K, Mat points, Mat normals, Mat colors)`
  * *Summary*: Renders the volume contents into an image. The resulting points and normals are in camera's coordinate system.
  * *Remarks*:

Rendered image size and camera intrinsics are taken from volume settings structure.
* * **Parameter** `cameraPose`:  the pose of camera in global coordinates.
* * **Parameter** `height`:  the height of result image
* * **Parameter** `width`:  the width of result image
* * **Parameter** `K`:  camera raycast intrinsics
* * **Parameter** `points`:  image to store rendered points.
* * **Parameter** `normals`:  image to store rendered normals corresponding to points.
* * **Parameter** `colors`:  image to store rendered colors corresponding to points (only for ColorTSDF).

  * *Parameter* `cameraPose`: The cameraPose parameter.
  * *Parameter* `height`: The height parameter.
  * *Parameter* `width`: The width parameter.
  * *Parameter* `K`: The K parameter.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
  * *Parameter* `colors`: The colors parameter.
* `void FetchNormals(Mat points, Mat normals)`
  * *Summary*: Extract the all data from volume. * **points** the input exist point. * **normals** the storage of normals (corresponding to input points) in the image.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
* `void FetchPointsNormals(Mat points, Mat normals)`
  * *Summary*: Extract the all data from volume. * **points** the storage of all points. * **normals** the storage of all normals, corresponding to points.
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
* `void FetchPointsNormalsColors(Mat points, Mat normals, Mat colors)`
  * *Summary*: Extract the all data from volume. * **points** the storage of all points. * **normals** the storage of all normals, corresponding to points. * **colors** the storage of all colors, corresponding to points (only for ColorTSDF).
  * *Parameter* `points`: The points parameter.
  * *Parameter* `normals`: The normals parameter.
  * *Parameter* `colors`: The colors parameter.
* `void Reset()`
  * *Summary*: clear all data in volume.
* `int GetVisibleBlocks()`
  * *Summary*: return visible blocks in volume.
  * *Returns*: The returned value.
* `long GetTotalVolumeUnits()`
  * *Summary*: return number of volume units in volume.
  * *Returns*: The returned value.
* `void GetBoundingBox(Mat bb, int precision)`
  * *Summary*: *  Gets bounding box in volume coordinates with given precision:
  * *Remarks*:

* VOLUME_UNIT - up to volume unit
* VOXEL - up to voxel (currently not supported)
* * **Parameter** `bb`:  6-float 1d array containing (min_x, min_y, min_z, max_x, max_y, max_z) in volume coordinates
* * **Parameter** `precision`:  bounding box calculation precision

  * *Parameter* `bb`: The bb parameter.
  * *Parameter* `precision`: The precision parameter.
* `void SetEnableGrowth(bool v)`
  * *Summary*: *  Enables or disables new volume unit allocation during integration.
  * *Remarks*:

* Makes sense for HashTSDF only.

  * *Parameter* `v`: The v parameter.
* `bool GetEnableGrowth()`
  * *Summary*: *  Returns if new volume units are allocated during integration or not.
  * *Remarks*:

* Makes sense for HashTSDF only.

  * *Returns*: The returned value.

---
### `VolumeSettings`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new VolumeSettings(VolumeType volumeType)`
  * *Summary*: Constructor of settings for custom Volume type. * **volumeType** volume type.
  * *Parameter* `volumeType`: The volumeType parameter.

#### Methods
* `void SetIntegrateWidth(int val)`
  * *Summary*: Sets the width of the image for integration. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `int GetIntegrateWidth()`
  * *Summary*: Returns the width of the image for integration.
  * *Returns*: The returned value.
* `void SetIntegrateHeight(int val)`
  * *Summary*: Sets the height of the image for integration. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `int GetIntegrateHeight()`
  * *Summary*: Returns the height of the image for integration.
  * *Returns*: The returned value.
* `void SetRaycastWidth(int val)`
  * *Summary*: Sets the width of the raycasted image, used when user does not provide it at raycast() call. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `int GetRaycastWidth()`
  * *Summary*: Returns the width of the raycasted image, used when user does not provide it at raycast() call.
  * *Returns*: The returned value.
* `void SetRaycastHeight(int val)`
  * *Summary*: Sets the height of the raycasted image, used when user does not provide it at raycast() call. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `int GetRaycastHeight()`
  * *Summary*: Returns the height of the raycasted image, used when user does not provide it at raycast() call.
  * *Returns*: The returned value.
* `void SetDepthFactor(float val)`
  * *Summary*: Sets depth factor, witch is the number for depth scaling. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `float GetDepthFactor()`
  * *Summary*: Returns depth factor, witch is the number for depth scaling.
  * *Returns*: The returned value.
* `void SetVoxelSize(float val)`
  * *Summary*: Sets the size of voxel. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `float GetVoxelSize()`
  * *Summary*: Returns the size of voxel.
  * *Returns*: The returned value.
* `void SetTsdfTruncateDistance(float val)`
  * *Summary*: Sets TSDF truncation distance. Distances greater than value from surface will be truncated to 1.0. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `float GetTsdfTruncateDistance()`
  * *Summary*: Returns TSDF truncation distance. Distances greater than value from surface will be truncated to 1.0.
  * *Returns*: The returned value.
* `void SetMaxDepth(float val)`
  * *Summary*: Sets threshold for depth truncation in meters. Truncates the depth greater than threshold to 0. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `float GetMaxDepth()`
  * *Summary*: Returns threshold for depth truncation in meters. Truncates the depth greater than threshold to 0.
  * *Returns*: The returned value.
* `void SetMaxWeight(int val)`
  * *Summary*: Sets max number of frames to integrate per voxel. Represents the max number of frames over which a running average of the TSDF is calculated for a voxel. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `int GetMaxWeight()`
  * *Summary*: Returns max number of frames to integrate per voxel. Represents the max number of frames over which a running average of the TSDF is calculated for a voxel.
  * *Returns*: The returned value.
* `void SetRaycastStepFactor(float val)`
  * *Summary*: Sets length of single raycast step. Describes the percentage of voxel length that is skipped per march. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `float GetRaycastStepFactor()`
  * *Summary*: Returns length of single raycast step. Describes the percentage of voxel length that is skipped per march.
  * *Returns*: The returned value.
* `void SetVolumePose(Mat val)`
  * *Summary*: Sets volume pose. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `void GetVolumePose(Mat val)`
  * *Summary*: Sets volume pose. * **val** output value.
  * *Parameter* `val`: The val parameter.
* `void SetVolumeResolution(Mat val)`
  * *Summary*: Resolution of voxel space. Number of voxels in each dimension. Applicable only for TSDF Volume. HashTSDF volume only supports equal resolution in all three dimensions. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `void GetVolumeResolution(Mat val)`
  * *Summary*: Resolution of voxel space. Number of voxels in each dimension. Applicable only for TSDF Volume. HashTSDF volume only supports equal resolution in all three dimensions. * **val** output value.
  * *Parameter* `val`: The val parameter.
* `void GetVolumeStrides(Mat val)`
  * *Summary*: Returns 3 integers representing strides by x, y and z dimension. Can be used to iterate over raw volume unit data. * **val** output value.
  * *Parameter* `val`: The val parameter.
* `void SetCameraIntegrateIntrinsics(Mat val)`
  * *Summary*: Sets intrinsics of camera for integrations. * Format of input: * [ fx  0 cx ] * [  0 fy cy ] * [  0  0  1 ] * where fx and fy are focus points of Ox and Oy axises, and cx and cy are central points of Ox and Oy axises. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `void GetCameraIntegrateIntrinsics(Mat val)`
  * *Summary*: Returns intrinsics of camera for integrations. * Format of output: * [ fx  0 cx ] * [  0 fy cy ] * [  0  0  1 ] * where fx and fy are focus points of Ox and Oy axises, and cx and cy are central points of Ox and Oy axises. * **val** output value.
  * *Parameter* `val`: The val parameter.
* `void SetCameraRaycastIntrinsics(Mat val)`
  * *Summary*: Sets camera intrinsics for raycast image which, used when user does not provide them at raycast() call. * Format of input: * [ fx  0 cx ] * [  0 fy cy ] * [  0  0  1 ] * where fx and fy are focus points of Ox and Oy axises, and cx and cy are central points of Ox and Oy axises. * **val** input value.
  * *Parameter* `val`: The val parameter.
* `void GetCameraRaycastIntrinsics(Mat val)`
  * *Summary*: Returns camera intrinsics for raycast image, used when user does not provide them at raycast() call. * Format of output: * [ fx  0 cx ] * [  0 fy cy ] * [  0  0  1 ] * where fx and fy are focus points of Ox and Oy axises, and cx and cy are central points of Ox and Oy axises. * **val** output value.
  * *Parameter* `val`: The val parameter.

---
### `DetailPoseGraph`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.LoadPointCloud`
**Signature**: `void LoadPointCloud(string filename, Mat vertices, Mat? normals, Mat? rgb)`

Loads a point cloud from a file. * * The function loads point cloud from the specified file and returns it. * If the cloud cannot be read, throws an error. * Vertex coordinates, normals and colors are returned as they are saved in the file * even if these arrays have different sizes and their elements do not correspond to each other * (which is typical for OBJ files for example) * * Currently, the following file formats are supported: * -  [Wavefront obj file *.obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file) * -  [Polygon File Format *.ply](https://en.wikipedia.org/wiki/PLY_(file_format)) * * **filename** Name of the file * **vertices** vertex coordinates, each value contains 3 floats * **normals** per-vertex normals, each value contains 3 floats * **rgb** per-vertex colors, each value contains 3 floats

**Parameters**:
* `filename`: Path to the file.
* `vertices`: The vertices parameter.
* `normals`: The normals parameter.
* `rgb`: The rgb parameter.

---
### `Cv2.SavePointCloud`
**Signature**: `void SavePointCloud(string filename, Mat vertices, Mat? normals, Mat? rgb)`

Saves a point cloud to a specified file. * * The function saves point cloud to the specified file. * File format is chosen based on the filename extension. * * **filename** Name of the file * **vertices** vertex coordinates, each value contains 3 floats * **normals** per-vertex normals, each value contains 3 floats * **rgb** per-vertex colors, each value contains 3 floats

**Parameters**:
* `filename`: Path to the file.
* `vertices`: The vertices parameter.
* `normals`: The normals parameter.
* `rgb`: The rgb parameter.

---
### `Cv2.LoadMesh`
**Signature**: `void LoadMesh(string filename, Mat vertices, IntPtr indices, Mat? normals, Mat? colors, Mat? texCoords)`

Loads a mesh from a file. * * The function loads mesh from the specified file and returns it. * If the mesh cannot be read, throws an error * Vertex attributes (i.e. space and texture coodinates, normals and colors) are returned in same-sized * arrays with corresponding elements having the same indices. * This means that if a face uses a vertex with a normal or a texture coordinate with different indices * (which is typical for OBJ files for example), this vertex will be duplicated for each face it uses. * * Currently, the following file formats are supported: * -  [Wavefront obj file *.obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file) (ONLY TRIANGULATED FACES) * -  [Polygon File Format *.ply](https://en.wikipedia.org/wiki/PLY_(file_format)) * **filename** Name of the file * **vertices** vertex coordinates, each value contains 3 floats * **indices** per-face list of vertices, each value is a vector of ints * **normals** per-vertex normals, each value contains 3 floats * **colors** per-vertex colors, each value contains 3 floats * **texCoords** per-vertex texture coordinates, each value contains 2 or 3 floats

**Parameters**:
* `filename`: Path to the file.
* `vertices`: The vertices parameter.
* `indices`: The indices parameter.
* `normals`: The normals parameter.
* `colors`: The colors parameter.
* `texCoords`: The texCoords parameter.

---
### `Cv2.SaveMesh`
**Signature**: `void SaveMesh(string filename, Mat vertices, IntPtr indices, Mat? normals, Mat? colors, Mat? texCoords)`

Saves a mesh to a specified file. * * The function saves mesh to the specified file. * File format is chosen based on the filename extension. * * **filename** Name of the file. * **vertices** vertex coordinates, each value contains 3 floats * **indices** per-face list of vertices, each value is a vector of ints * **normals** per-vertex normals, each value contains 3 floats * **colors** per-vertex colors, each value contains 3 floats * **texCoords** per-vertex texture coordinates, each value contains 2 or 3 floats

**Parameters**:
* `filename`: Path to the file.
* `vertices`: The vertices parameter.
* `indices`: The indices parameter.
* `normals`: The normals parameter.
* `colors`: The colors parameter.
* `texCoords`: The texCoords parameter.

---
### `Cv2.TriangleRasterize`
**Signature**: `void TriangleRasterize(Mat vertices, Mat indices, Mat colors, Mat colorBuf, Mat depthBuf, Mat world2cam, double fovY, double zNear, double zFar, TriangleRasterizeSettings? settings)`

Renders a set of triangles on a depth and color image * * Triangles can be drawn white (1.0, 1.0, 1.0), flat-shaded or with a color interpolation between vertices. * In flat-shaded mode the 1st vertex color of each triangle is used to fill the whole triangle. * * The world2cam is an inverted camera pose matrix in fact. It transforms vertices from world to * camera coordinate system. * * The camera coordinate system emulates the OpenGL's coordinate system having coordinate origin in a screen center, * X axis pointing right, Y axis pointing up and Z axis pointing towards the viewer * except that image is vertically flipped after the render. * This means that all visible objects are placed in z-negative area, or exactly in -zNear > z > -zFar since * zNear and zFar are positive. * For example, at fovY = PI/2 the point (0, 1, -1) will be projected to (width/2, 0) screen point, * (1, 0, -1) to (width/2 + height/2, height/2). Increasing fovY makes projection smaller and vice versa. * * The function does not create or clear output images before the rendering. This means that it can be used * for drawing over an existing image or for rendering a model into a 3D scene using pre-filled Z-buffer. * * Empty scene results in a depth buffer filled by the maximum value since every pixel is infinitely far from the camera. * Therefore, before rendering anything from scratch the depthBuf should be filled by zFar values (or by ones in INVDEPTH mode). * * There are special versions of this function named triangleRasterizeDepth and triangleRasterizeColor * for cases if a user needs a color image or a depth image alone; they may run slightly faster. * * **vertices** vertices coordinates array. Should contain values of CV_32FC3 type or a compatible one (e.g. Vec3f, etc.) * **indices** triangle vertices index array, 3 per triangle. Each index indicates a vertex in a vertices array. * Should contain CV_32SC3 values or compatible * **colors** per-vertex colors of CV_32FC3 type or compatible. Can be empty or the same size as vertices array. * If the values are out of [0; 1] range, the result correctness is not guaranteed * **colorBuf** an array representing the final rendered image. Should containt CV_32FC3 values and be the same size as depthBuf. * Not cleared before rendering, i.e. the content is reused as there is some pre-rendered scene. * **depthBuf** an array of floats containing resulting Z buffer. Should contain float values and be the same size as colorBuf. * Not cleared before rendering, i.e. the content is reused as there is some pre-rendered scene. * Empty scene corresponds to all values set to zFar (or to 1.0 in INVDEPTH mode) * **world2cam** a 4x3 or 4x4 float or double matrix containing inverted (sic!) camera pose * **fovY** field of view in vertical direction, given in radians * **zNear** minimum Z value to render, everything closer is clipped * **zFar** maximum Z value to render, everything farther is clipped * **settings** see TriangleRasterizeSettings. By default the smooth shading is on, * with CW culling and with disabled GL compatibility

**Parameters**:
* `vertices`: The vertices parameter.
* `indices`: The indices parameter.
* `colors`: The colors parameter.
* `colorBuf`: The colorBuf parameter.
* `depthBuf`: The depthBuf parameter.
* `world2cam`: The world2cam parameter.
* `fovY`: The fovY parameter.
* `zNear`: The zNear parameter.
* `zFar`: The zFar parameter.
* `settings`: The settings parameter.

---
### `Cv2.TriangleRasterizeDepth`
**Signature**: `void TriangleRasterizeDepth(Mat vertices, Mat indices, Mat depthBuf, Mat world2cam, double fovY, double zNear, double zFar, TriangleRasterizeSettings? settings)`

Overloaded version of triangleRasterize() with depth-only rendering * * **vertices** vertices coordinates array. Should contain values of CV_32FC3 type or a compatible one (e.g. Vec3f, etc.) * **indices** triangle vertices index array, 3 per triangle. Each index indicates a vertex in a vertices array. * Should contain CV_32SC3 values or compatible * **depthBuf** an array of floats containing resulting Z buffer. Should contain float values and be the same size as colorBuf. * Not cleared before rendering, i.e. the content is reused as there is some pre-rendered scene. * Empty scene corresponds to all values set to zFar (or to 1.0 in INVDEPTH mode) * **world2cam** a 4x3 or 4x4 float or double matrix containing inverted (sic!) camera pose * **fovY** field of view in vertical direction, given in radians * **zNear** minimum Z value to render, everything closer is clipped * **zFar** maximum Z value to render, everything farther is clipped * **settings** see TriangleRasterizeSettings. By default the smooth shading is on, * with CW culling and with disabled GL compatibility

**Parameters**:
* `vertices`: The vertices parameter.
* `indices`: The indices parameter.
* `depthBuf`: The depthBuf parameter.
* `world2cam`: The world2cam parameter.
* `fovY`: The fovY parameter.
* `zNear`: The zNear parameter.
* `zFar`: The zFar parameter.
* `settings`: The settings parameter.

---
### `Cv2.TriangleRasterizeColor`
**Signature**: `void TriangleRasterizeColor(Mat vertices, Mat indices, Mat colors, Mat colorBuf, Mat world2cam, double fovY, double zNear, double zFar, TriangleRasterizeSettings? settings)`

Overloaded version of triangleRasterize() with color-only rendering * * **vertices** vertices coordinates array. Should contain values of CV_32FC3 type or a compatible one (e.g. Vec3f, etc.) * **indices** triangle vertices index array, 3 per triangle. Each index indicates a vertex in a vertices array. * Should contain CV_32SC3 values or compatible * **colors** per-vertex colors of CV_32FC3 type or compatible. Can be empty or the same size as vertices array. * If the values are out of [0; 1] range, the result correctness is not guaranteed * **colorBuf** an array representing the final rendered image. Should containt CV_32FC3 values and be the same size as depthBuf. * Not cleared before rendering, i.e. the content is reused as there is some pre-rendered scene. * **world2cam** a 4x3 or 4x4 float or double matrix containing inverted (sic!) camera pose * **fovY** field of view in vertical direction, given in radians * **zNear** minimum Z value to render, everything closer is clipped * **zFar** maximum Z value to render, everything farther is clipped * **settings** see TriangleRasterizeSettings. By default the smooth shading is on, * with CW culling and with disabled GL compatibility

**Parameters**:
* `vertices`: The vertices parameter.
* `indices`: The indices parameter.
* `colors`: The colors parameter.
* `colorBuf`: The colorBuf parameter.
* `world2cam`: The world2cam parameter.
* `fovY`: The fovY parameter.
* `zNear`: The zNear parameter.
* `zFar`: The zFar parameter.
* `settings`: The settings parameter.

---
### `Cv2.RegisterDepth`
**Signature**: `void RegisterDepth(Mat unregisteredCameraMatrix, Mat registeredCameraMatrix, Mat registeredDistCoeffs, Mat Rt, Mat unregisteredDepth, Size outputImagePlaneSize, Mat registeredDepth, bool depthDilation)`

Registers depth data to an external camera

**Detailed Remarks**:
* Registration is performed by creating a depth cloud, transforming the cloud by
* the rigid body transformation between the cameras, and then projecting the
* transformed points into the RGB camera.

* uv_rgb = K_rgb * [R | t] * z * inv(K_ir) * uv_ir

* Currently does not check for negative depth values.

* * **Parameter** `unregisteredCameraMatrix`:  the camera matrix of the depth camera
* * **Parameter** `registeredCameraMatrix`:  the camera matrix of the external camera
* * **Parameter** `registeredDistCoeffs`:  the distortion coefficients of the external camera
* * **Parameter** `Rt`:  the rigid body transform between the cameras. Transforms points from depth camera frame to external camera frame.
* * **Parameter** `unregisteredDepth`:  the input depth data
* * **Parameter** `outputImagePlaneSize`:  the image plane dimensions of the external camera (width, height)
* * **Parameter** `registeredDepth`:  the result of transforming the depth into the external camera
* * **Parameter** `depthDilation`:  whether or not the depth is dilated to avoid holes and occlusion errors (optional)

**Parameters**:
* `unregisteredCameraMatrix`: The unregisteredCameraMatrix parameter.
* `registeredCameraMatrix`: The registeredCameraMatrix parameter.
* `registeredDistCoeffs`: The registeredDistCoeffs parameter.
* `Rt`: The Rt parameter.
* `unregisteredDepth`: The unregisteredDepth parameter.
* `outputImagePlaneSize`: The outputImagePlaneSize parameter.
* `registeredDepth`: The registeredDepth parameter.
* `depthDilation`: The depthDilation parameter.

---
### `Cv2.DepthTo3dSparse`
**Signature**: `void DepthTo3dSparse(Mat depth, Mat in_K, Mat in_points, Mat points3d)`

* **depth** the depth image

**Detailed Remarks**:
* * **Parameter** `in_K`: 
* * **Parameter** `in_points`:  the list of xy coordinates
* * **Parameter** `points3d`:  the resulting 3d points (point is represented by 4 chanels value [x, y, z, 0])

**Parameters**:
* `depth`: The depth parameter.
* `in_K`: The in_K parameter.
* `in_points`: The in_points parameter.
* `points3d`: The points3d parameter.

---
### `Cv2.DepthTo3d`
**Signature**: `void DepthTo3d(Mat depth, Mat K, Mat points3d, Mat? mask)`

Converts a depth image to 3d points. If the mask is empty then the resulting array has the same dimensions as `depth`,

**Detailed Remarks**:
* otherwise it is 1d vector containing mask-enabled values only.
* The coordinate system is x pointing left, y down and z away from the camera
* * **Parameter** `depth`:  the depth image (if given as short int CV_U, it is assumed to be the depth in millimeters
*              (as done with the Microsoft Kinect), otherwise, if given as CV_32F or CV_64F, it is assumed in meters)
* * **Parameter** `K`:  The calibration matrix
* * **Parameter** `points3d`:  the resulting 3d points (point is represented by 4 channels value [x, y, z, 0]). They are of the same depth as `depth` if it is CV_32F or CV_64F, and the
*        depth of `K` if `depth` is of depth CV_16U or CV_16S
* * **Parameter** `mask`:  the mask of the points to consider (can be empty)

**Parameters**:
* `depth`: The depth parameter.
* `K`: The K parameter.
* `points3d`: The points3d parameter.
* `mask`: Optional operation mask.

---
### `Cv2.RescaleDepth`
**Signature**: `void RescaleDepth(Mat @in, int type, Mat @out, double depth_factor)`

If the input image is of type CV_16UC1 (like the Kinect one), the image is converted to floats, divided

**Detailed Remarks**:
* by depth_factor to get a depth in meters, and the values 0 are converted to float.NaN
* Otherwise, the image is simply converted to floats
* * **Parameter** `in`:  the depth image (if given as short int CV_U, it is assumed to be the depth in millimeters
*              (as done with the Microsoft Kinect), it is assumed in meters)
* * **Parameter** `type`:  the desired output depth (CV_32F or CV_64F)
* * **Parameter** `out`:  The rescaled float depth image
* * **Parameter** `depth_factor`:  (optional) factor by which depth is converted to distance (by default = 1000.0 for Kinect sensor)

**Parameters**:
* `in`: The @in parameter.
* `type`: The type parameter.
* `out`: The @out parameter.
* `depth_factor`: The depth_factor parameter.

---
### `Cv2.WarpFrame`
**Signature**: `void WarpFrame(Mat depth, Mat image, Mat mask, Mat Rt, Mat cameraMatrix, Mat? warpedDepth, Mat? warpedImage, Mat? warpedMask)`

Warps depth or RGB-D image by reprojecting it in 3d, applying Rt transformation

**Detailed Remarks**:
* and then projecting it back onto the image plane.
* This function can be used to visualize the results of the Odometry algorithm.
* * **Parameter** `depth`:  Depth data, should be 1-channel CV_16U, CV_16S, CV_32F or CV_64F
* * **Parameter** `image`:  RGB image (optional), should be 1-, 3- or 4-channel CV_8U
* * **Parameter** `mask`:  Mask of used pixels (optional), should be CV_8UC1, CV_8SC1 or CV_BoolC1
* * **Parameter** `Rt`:  Rotation+translation matrix (3x4 or 4x4) to be applied to depth points
* * **Parameter** `cameraMatrix`:  Camera intrinsics matrix (3x3)
* * **Parameter** `warpedDepth`:  The warped depth data (optional)
* * **Parameter** `warpedImage`:  The warped RGB image (optional)
* * **Parameter** `warpedMask`:  The mask of valid pixels in warped image (optional)

**Parameters**:
* `depth`: The depth parameter.
* `image`: Input image.
* `mask`: Optional operation mask.
* `Rt`: The Rt parameter.
* `cameraMatrix`: The cameraMatrix parameter.
* `warpedDepth`: The warpedDepth parameter.
* `warpedImage`: The warpedImage parameter.
* `warpedMask`: The warpedMask parameter.

---
### `Cv2.FindPlanes`
**Signature**: `void FindPlanes(Mat points3d, Mat normals, Mat mask, Mat plane_coefficients, int block_size, int min_size, double threshold, double sensor_error_a, double sensor_error_b, double sensor_error_c, RgbdPlaneMethod method)`

Find the planes in a depth image

**Detailed Remarks**:
* * **Parameter** `points3d`:  the 3d points organized like the depth image: rows x cols with 3 channels
* * **Parameter** `normals`:  the normals for every point in the depth image; optional, can be empty
* * **Parameter** `mask`:  An image where each pixel is labeled with the plane it belongs to
*        and 255 if it does not belong to any plane
* * **Parameter** `plane_coefficients`:  the coefficients of the corresponding planes (a,b,c,d) such that ax+by+cz+d=0, norm(a,b,c)=1
*        and c < 0 (so that the normal points towards the camera)
* * **Parameter** `block_size`:  The size of the blocks to look at for a stable MSE
* * **Parameter** `min_size`:  The minimum size of a cluster to be considered a plane
* * **Parameter** `threshold`:  The maximum distance of a point from a plane to belong to it (in meters)
* * **Parameter** `sensor_error_a`:  coefficient of the sensor error. 0 by default, use 0.0075 for a Kinect
* * **Parameter** `sensor_error_b`:  coefficient of the sensor error. 0 by default
* * **Parameter** `sensor_error_c`:  coefficient of the sensor error. 0 by default
* * **Parameter** `method`:  The method to use to compute the planes.

**Parameters**:
* `points3d`: The points3d parameter.
* `normals`: The normals parameter.
* `mask`: Optional operation mask.
* `plane_coefficients`: The plane_coefficients parameter.
* `block_size`: The block_size parameter.
* `min_size`: The min_size parameter.
* `threshold`: The threshold parameter.
* `sensor_error_a`: The sensor_error_a parameter.
* `sensor_error_b`: The sensor_error_b parameter.
* `sensor_error_c`: The sensor_error_c parameter.
* `method`: The method parameter.

---
## 🔢 Enumerations

### `OdometryAlgoType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Common`** | `0` | Common |
| **`Fast`** | `1` | Fast |

---
### `OdometryFramePyramidType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`PyrImage`** | `0` | PyrImage |
| **`PyrDepth`** | `1` | PyrDepth |
| **`PyrMask`** | `2` | PyrMask |
| **`PyrCloud`** | `3` | PyrCloud |
| **`PyrDix`** | `4` | PyrDix |
| **`PyrDiy`** | `5` | PyrDiy |
| **`PyrTexmask`** | `6` | PyrTexmask |
| **`PyrNorm`** | `7` | PyrNorm |
| **`PyrNormmask`** | `8` | PyrNormmask |
| **`NPyramids`** | `unchecked((int)(8 + 1))` | NPyramids |

---
### `OdometryType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Depth`** | `0` | Depth |
| **`Rgb`** | `1` | Rgb |
| **`RgbDepth`** | `2` | RgbDepth |

---
### `RgbdNormalsRgbdNormalsMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Fals`** | `0` | Fals |
| **`Linemod`** | `1` | Linemod |
| **`Sri`** | `2` | Sri |
| **`CrossProduct`** | `3` | CrossProduct |

---
### `RgbdPlaneMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`RgbdPlaneMethodDefault`** | `0` | RgbdPlaneMethodDefault |

---
### `TriangleCullingMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Cw`** | `1` | Cw |
| **`Ccw`** | `2` | Ccw |

---
### `TriangleGlCompatibleMode`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Disabled`** | `0` | Disabled |
| **`Invdepth`** | `1` | Invdepth |

---
### `TriangleShadingType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`White`** | `0` | White |
| **`Flat`** | `1` | Flat |
| **`Shaded`** | `2` | Shaded |

---
### `VolumeBoundingBoxPrecision`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`VolumeUnit`** | `0` | VolumeUnit |
| **`Voxel`** | `1` | Voxel |

---
### `VolumeType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Tsdf`** | `0` | Tsdf |
| **`HashTSDF`** | `1` | HashTSDF |
| **`ColorTSDF`** | `2` | ColorTSDF |

---

</div>