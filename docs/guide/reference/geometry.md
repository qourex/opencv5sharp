# GEOMETRY Module API Reference

Complete documentation for the **GEOMETRY** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Geometry Documentation](https://docs.opencv.org/5.x/main_modules/geometry.html).

---
<div v-pre>

## 📦 Classes and Structs

### `MSTEdge`
**Inherits from**: `DisposableOpenCVObject`

*  Represents an edge in a graph for Minimum Spanning Tree (MST) computation.

**Detailed Remarks**:
* Each edge connects two nodes (source and target) and has an associated weight.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Source`** | `int` | Gets or sets the source property. |
| **`Target`** | `int` | Gets or sets the target property. |
| **`Weight`** | `double` | Gets or sets the weight property. |

---
### `Subdiv2D`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Constructors
* `new Subdiv2D()`
  * *Summary*: creates an empty Subdiv2D object.
* `new Subdiv2D(Rect rect)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `rect`: The rect parameter.
* `new Subdiv2D(Rect2F rect2f)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Parameter* `rect2f`: The rect2f parameter.

#### Methods
* `void InitDelaunay(Rect rect)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*     Creates a new empty Delaunay subdivision

*    * **Parameter** `rect`:  Rectangle that includes all of the 2D points that are to be added to the subdivision.

  * *Parameter* `rect`: The rect parameter.
* `void InitDelaunay(Rect2F rect)`
  * *Summary*: This is an overloaded member function, provided for convenience.
  * *Remarks*:

*     Creates a new empty Delaunay subdivision

*    * **Parameter** `rect`:  Rectangle that includes all of the 2d points that are to be added to the subdivision.

  * *Parameter* `rect`: The rect parameter.
* `int Insert(Point2f pt)`
  * *Summary*: Insert a single point into a Delaunay triangulation. * *    **pt** Point to insert. * *    The function inserts a single point into a subdivision and modifies the subdivision topology *    appropriately. If a point with the same coordinates exists already, no new point is added. *    **Returns**: the ID of the point. * *    **Note:** If the point is outside of the triangulation specified rect a runtime error is raised.
  * *Parameter* `pt`: The pt parameter.
  * *Returns*: The returned value.
* `void Insert(IntPtr ptvec)`
  * *Summary*: Insert multiple points into a Delaunay triangulation. * *    **ptvec** Points to insert. * *    The function inserts a vector of points into a subdivision and modifies the subdivision topology *    appropriately.
  * *Parameter* `ptvec`: The ptvec parameter.
* `int Locate(Point2f pt, int edge, int vertex)`
  * *Summary*: Returns the location of a point within a Delaunay triangulation. * *    **pt** Point to locate. *    **edge** Output edge that the point belongs to or is located to the right of it. *    **vertex** Optional output vertex the input point coincides with. * *    The function locates the input point within the subdivision and gives one of the triangle edges *    or vertices. * *    **Returns**: an integer which specify one of the following five cases for point location: *    -  The point falls into some facet. The function returns `PTLOC_INSIDE` and edge will contain one of *       edges of the facet. *    -  The point falls onto the edge. The function returns `PTLOC_ON_EDGE` and edge will contain this edge. *    -  The point coincides with one of the subdivision vertices. The function returns `PTLOC_VERTEX` and *       vertex will contain a pointer to the vertex. *    -  The point is outside the subdivision reference rectangle. The function returns `PTLOC_OUTSIDE_RECT` *       and no pointers are filled. *    -  One of input arguments is invalid. A runtime error is raised or, if silent or "parent" error *       processing mode is selected, `PTLOC_ERROR` is returned.
  * *Parameter* `pt`: The pt parameter.
  * *Parameter* `edge`: The edge parameter.
  * *Parameter* `vertex`: The vertex parameter.
  * *Returns*: The returned value.
* `int FindNearest(Point2f pt, IntPtr nearestPt)`
  * *Summary*: Finds the subdivision vertex closest to the given point. * *    **pt** Input point. *    **nearestPt** Output subdivision vertex point. * *    The function is another function that locates the input point within the subdivision. It finds the *    subdivision vertex that is the closest to the input point. It is not necessarily one of vertices *    of the facet containing the input point, though the facet (located using locate() ) is used as a *    starting point. * *    **Returns**: vertex ID.
  * *Parameter* `pt`: The pt parameter.
  * *Parameter* `nearestPt`: The nearestPt parameter.
  * *Returns*: The returned value.
* `void GetEdgeList(IntPtr edgeList)`
  * *Summary*: Returns a list of all edges. * *    **edgeList** Output vector. * *    The function gives each edge as a 4 numbers vector, where each two are one of the edge *    vertices. i.e. org_x = v[0], org_y = v[1], dst_x = v[2], dst_y = v[3].
  * *Parameter* `edgeList`: The edgeList parameter.
* `void GetLeadingEdgeList(IntPtr leadingEdgeList)`
  * *Summary*: Returns a list of the leading edge ID connected to each triangle. * *    **leadingEdgeList** Output vector. * *    The function gives one edge ID for each triangle.
  * *Parameter* `leadingEdgeList`: The leadingEdgeList parameter.
* `void GetTriangleList(IntPtr triangleList)`
  * *Summary*: Returns a list of all triangles. * *    **triangleList** Output vector. * *    The function gives each triangle as a 6 numbers vector, where each two are one of the triangle *    vertices. i.e. p1_x = v[0], p1_y = v[1], p2_x = v[2], p2_y = v[3], p3_x = v[4], p3_y = v[5].
  * *Parameter* `triangleList`: The triangleList parameter.
* `void GetVoronoiFacetList(IntPtr idx, IntPtr facetList, IntPtr facetCenters)`
  * *Summary*: Returns a list of all Voronoi facets. * *    **idx** Vector of vertices IDs to consider. For all vertices you can pass empty vector. *    **facetList** Output vector of the Voronoi facets. *    **facetCenters** Output vector of the Voronoi facets center points. *
  * *Parameter* `idx`: The idx parameter.
  * *Parameter* `facetList`: The facetList parameter.
  * *Parameter* `facetCenters`: The facetCenters parameter.
* `Point2f GetVertex(int vertex, IntPtr firstEdge)`
  * *Summary*: Returns vertex location from vertex ID. * *    **vertex** vertex ID. *    **firstEdge** Optional. The first edge ID which is connected to the vertex. *    **Returns**: vertex (x,y) *
  * *Parameter* `vertex`: The vertex parameter.
  * *Parameter* `firstEdge`: The firstEdge parameter.
  * *Returns*: The returned value.
* `int GetEdge(int edge, int nextEdgeType)`
  * *Summary*: Returns one of the edges related to the given edge. * *    **edge** Subdivision edge ID. *    **nextEdgeType** Parameter specifying which of the related edges to return. *    The following values are possible: *    -   NEXT_AROUND_ORG next around the edge origin ( eOnext on the picture below if e is the input edge) *    -   NEXT_AROUND_DST next around the edge vertex ( eDnext ) *    -   PREV_AROUND_ORG previous around the edge origin (reversed eRnext ) *    -   PREV_AROUND_DST previous around the edge destination (reversed eLnext ) *    -   NEXT_AROUND_LEFT next around the left facet ( eLnext ) *    -   NEXT_AROUND_RIGHT next around the right facet ( eRnext ) *    -   PREV_AROUND_LEFT previous around the left facet (reversed eOnext ) *    -   PREV_AROUND_RIGHT previous around the right facet (reversed eDnext ) * *     * *    **Returns**: edge ID related to the input edge.
  * *Parameter* `edge`: The edge parameter.
  * *Parameter* `nextEdgeType`: The nextEdgeType parameter.
  * *Returns*: The returned value.
* `int NextEdge(int edge)`
  * *Summary*: Returns next edge around the edge origin. * *    **edge** Subdivision edge ID. * *    **Returns**: an integer which is next edge ID around the edge origin: eOnext on the *    picture above if e is the input edge).
  * *Parameter* `edge`: The edge parameter.
  * *Returns*: The returned value.
* `int RotateEdge(int edge, int rotate)`
  * *Summary*: Returns another edge of the same quad-edge. * *    **edge** Subdivision edge ID. *    **rotate** Parameter specifying which of the edges of the same quad-edge as the input *    one to return. The following values are possible: *    -   0 - the input edge ( e on the picture below if e is the input edge) *    -   1 - the rotated edge ( eRot ) *    -   2 - the reversed edge (reversed e (in green)) *    -   3 - the reversed rotated edge (reversed eRot (in green)) * *    **Returns**: one of the edges ID of the same quad-edge as the input edge.
  * *Parameter* `edge`: The edge parameter.
  * *Parameter* `rotate`: The rotate parameter.
  * *Returns*: The returned value.
* `int SymEdge(int edge)`
  * *Summary*: Wrapper for OpenCV's native functionality.
  * *Parameter* `edge`: The edge parameter.
  * *Returns*: The returned value.
* `int EdgeOrg(int edge, IntPtr orgpt)`
  * *Summary*: Returns the edge origin. * *    **edge** Subdivision edge ID. *    **orgpt** Output vertex location. * *    **Returns**: vertex ID.
  * *Parameter* `edge`: The edge parameter.
  * *Parameter* `orgpt`: The orgpt parameter.
  * *Returns*: The returned value.
* `int EdgeDst(int edge, IntPtr dstpt)`
  * *Summary*: Returns the edge destination. * *    **edge** Subdivision edge ID. *    **dstpt** Output vertex location. * *    **Returns**: vertex ID.
  * *Parameter* `edge`: The edge parameter.
  * *Parameter* `dstpt`: The dstpt parameter.
  * *Returns*: The returned value.

---
### `UsacParams`
**Inherits from**: `DisposableOpenCVObject`

Wrapper for OpenCV's native functionality.

#### Properties
| Property | Type | Description |
| :--- | :--- | :--- |
| **`Confidence`** | `double` | Gets or sets the confidence property. |
| **`IsParallel`** | `bool` | Gets or sets the isParallel property. |
| **`LoIterations`** | `int` | Gets or sets the loIterations property. |
| **`LoMethod`** | `LocalOptimMethod` | Gets or sets the loMethod property. |
| **`LoSampleSize`** | `int` | Gets or sets the loSampleSize property. |
| **`MaxIterations`** | `int` | Gets or sets the maxIterations property. |
| **`NeighborsSearch`** | `NeighborSearchMethod` | Gets or sets the neighborsSearch property. |
| **`RandomGeneratorState`** | `int` | Gets or sets the randomGeneratorState property. |
| **`Sampler`** | `SamplingMethod` | Gets or sets the sampler property. |
| **`Score`** | `ScoreMethod` | Gets or sets the score property. |
| **`Threshold`** | `double` | Gets or sets the threshold property. |
| **`FinalPolisher`** | `PolishingMethod` | Gets or sets the final_polisher property. |
| **`FinalPolisherIterations`** | `int` | Gets or sets the final_polisher_iterations property. |

#### Constructors
* `new UsacParams()`
  * *Summary*: Wrapper for OpenCV's native functionality.

---
## ⚙️ Static Methods (Cv2)

### `Cv2.ApproxPolyDP`
**Signature**: `void ApproxPolyDP(Mat curve, Mat approxCurve, double epsilon, bool closed)`

Approximates a polygonal curve(s) with the specified precision. * T he function approxPolyDP approximates a curve or a p*olygon with another curve/polygon with less vertices so that the distance between them is less or equal to the specified precision. It uses the Douglas-Peucker algorithm <https://en.wikipedia.org/wiki/Ramer-Douglas-Peucker_algorithm>

**Parameters**:
* `curve`: Input vector of a 2D point stored in array or Mat
* `approxCurve`: Result of the approximation. The type should match the type of the input curve.
* `epsilon`: Parameter specifying the approximation accuracy. This is the maximum distance between the original curve and its approximation.
* `closed`: If true, the approximated curve is closed (its first and last vertices are connected). Otherwise, it is not closed.

---
### `Cv2.ApproxPolyN`
**Signature**: `void ApproxPolyN(Mat curve, Mat approxCurve, int nsides, float epsilon_percentage, bool ensure_convex)`

Approximates a polygon with a convex hull with a specified accuracy and number of sides. * T he approxPolyN function approximates a polygon with *a convex hull so that the difference between the contour area of the original contour and the new polygon is minimal. It uses a greedy algorithm for contracting two vertices into one in such a way that the additional area is minimal. Straight lines formed by each edge of the convex contour are drawn and the areas of the resulting triangles are considered. Each vertex will lie either on the original contour or outside it.

**Detailed Remarks**:
The algorithm based on the paper **Citation**:  LowIlie2003 .

**Parameters**:
* `curve`: Input vector of a 2D points stored in array or Mat, points must be float or integer.
* `approxCurve`: Result of the approximation. The type is vector of a 2D point (Point2f or Point) in array or Mat.
* `nsides`: The parameter defines the number of sides of the result polygon.
* `epsilon_percentage`: defines the percentage of the maximum of additional area. If it equals -1, it is not used. Otherwise algorithm stops if additional area is greater than contourArea(_curve) * percentage. If additional area exceeds the limit, algorithm returns as many vertices as there were at the moment the limit was exceeded.
* `ensure_convex`: If it is true, algorithm creates a convex hull of input contour. Otherwise input vector should be convex.

---
### `Cv2.MinAreaRect`
**Signature**: `RotatedRect? MinAreaRect(Mat points)`

Finds a rotated rectangle of the minimum area enclosing the input 2D point set. * * The function calculates and returns the minimum-area bounding rectangle (possibly rotated) for a * specified point set. The angle of rotation represents the angle between the line connecting the starting * and ending points (based on the clockwise order with greatest index for the corner with greatest formula) * and the horizontal axis. This angle always falls between formula because, if the object * rotates more than a rect angle, the next edge is used to measure the angle. The starting and ending points change * as the object rotates.Developer should keep in mind that the returned RotatedRect can contain negative * indices when data is close to the containing Mat element boundary. * * **points** Input array of 2D points, stored in [] or Mat

**Parameters**:
* `points`: The points parameter.

**Returns**: The returned value.

---
### `Cv2.BoxPoints`
**Signature**: `void BoxPoints(RotatedRect box, Mat points)`

Finds the four vertices of a rotated rect. Useful to draw the rotated rectangle. * * The function finds the four vertices of a rotated rectangle. The four vertices are returned * in clockwise order starting from the point with greatest formula. If two points have the * same formula coordinate the rightmost is the starting point. This function is useful to draw the * rectangle. You can directly use the RotatedRect.Points method. Please * visit the tutorial on Creating Bounding rotated boxes and ellipses * for contours for more information. * * **box** The input rotated rectangle. It may be the output of `minAreaRect`. * **points** The output array of four vertices of rectangles.

**Parameters**:
* `box`: The box parameter.
* `points`: The points parameter.

---
### `Cv2.MinEnclosingCircle`
**Signature**: `void MinEnclosingCircle(Mat points, Point2f center, float radius)`

Finds a circle of the minimum area enclosing a 2D point set. * * The function finds the minimal enclosing circle of a 2D point set using an iterative algorithm. * * **points** Input array of 2D points, stored in [] or Mat * **center** Output center of the circle. * **radius** Output radius of the circle.

**Parameters**:
* `points`: The points parameter.
* `center`: The center parameter.
* `radius`: The radius parameter.

---
### `Cv2.MinEnclosingTriangle`
**Signature**: `double MinEnclosingTriangle(Mat points, Mat triangle)`

Finds a triangle of minimum area enclosing a 2D point set and returns its area. * * The function finds a triangle of minimum area enclosing the given set of 2D points and returns its * area. The output for a given 2D point set is shown in the image below. 2D points are depicted in *red* and the enclosing triangle in *yellow*. * *  * * The implementation of the algorithm is based on O'Rourke's [ORourke86] and Klee and Laskowski's * [KleeLaskowski85] papers. O'Rourke provides a formula algorithm for finding the minimal * enclosing triangle of a 2D convex polygon with n vertices. Since the `minEnclosingTriangle` function * takes a 2D point set as input an additional preprocessing step of computing the convex hull of the * 2D point set is required. The complexity of the `convexHull` function is formula which is higher * than formula. Thus the overall complexity of the function is formula. * * **points** Input array of 2D points with depth CV_32S or CV_32F, stored in [] or Mat * **triangle** Output vector of three 2D points defining the vertices of the triangle. The depth * of the OutputArray must be CV_32F.

**Parameters**:
* `points`: The points parameter.
* `triangle`: The triangle parameter.

**Returns**: The returned value.

---
### `Cv2.MinEnclosingConvexPolygon`
**Signature**: `double MinEnclosingConvexPolygon(Mat points, Mat polygon, int k)`

*  Finds a convex polygon of minimum area enclosing a 2D point set and returns its area.

**Detailed Remarks**:
* This function takes a given set of 2D points and finds the enclosing polygon with k vertices and minimal
* area. It takes the set of points and the parameter k as input and returns the area of the minimal
* enclosing polygon.

* The Implementation is based on a paper by Aggarwal, Chang and Yap **Citation**:  Aggarwal1985. They
* provide a formula algorithm for finding the minimal convex polygon with k
* vertices enclosing a 2D convex polygon with n vertices (k < n). Since the `minEnclosingConvexPolygon`
* function takes a 2D point set as input, an additional preprocessing step of computing the convex hull
* of the 2D point set is required. The complexity of the `convexHull` function is formula which
* is lower than formula. Thus the overall complexity of the function is
* formula.

* * **Parameter** `points`:    Input array of 2D points, stored in [] or Mat
* * **Parameter** `polygon`:   Output array of 2D points defining the vertices of the enclosing polygon
* * **Parameter** `k`:         Number of vertices of the output polygon

**Parameters**:
* `points`: The points parameter.
* `polygon`: The polygon parameter.
* `k`: The k parameter.

**Returns**: The returned value.

---
### `Cv2.Moments`
**Signature**: `Moments? Moments(Mat array, bool binaryImage)`

Calculates all of the moments up to the third order of a polygon or rasterized shape. * * The function computes moments, up to the 3rd order, of a vector shape or a rasterized shape. The * results are returned in the structure Moments. * * **array** Single channel raster image (CV_8U, CV_16U, CV_16S, CV_32F, CV_64F) or an array ( * formula or formula ) of 2D points (Point or Point2f). * **binaryImage** If it is true, all non-zero image pixels are treated as 1's. The parameter is * used for images only. * **Returns**: moments. * * **Note:**  * * **Note:** For contour-based moments, the zeroth-order moment `m00` represents * the contour area. * * If the input contour is degenerate (for example, a single point or all points * are collinear), the area is zero and therefore `m00` == 0. * * In this case, the centroid coordinates (`m10`/m00, `m01`/m00) are undefined * and must be handled explicitly by the caller. * * A common workaround is to compute the center using boundingRect() or by * averaging the input points. * * **See also:**  contourArea, arcLength

**Parameters**:
* `array`: The array parameter.
* `binaryImage`: The binaryImage parameter.

**Returns**: The returned value.

---
### `Cv2.HuMoments`
**Signature**: `void HuMoments(Moments m, Mat hu)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `m`: The m parameter.
* `hu`: The hu parameter.

---
### `Cv2.MatchShapes`
**Signature**: `double MatchShapes(Mat contour1, Mat contour2, int method, double parameter)`

Compares two shapes. * * The function compares two shapes. All three implemented methods use the Hu invariants (see `HuMoments`) * * **contour1** First contour or grayscale image. * **contour2** Second contour or grayscale image. * **method** Comparison method, see `ShapeMatchModes` * **parameter** Method-specific parameter (not supported now).

**Parameters**:
* `contour1`: The contour1 parameter.
* `contour2`: The contour2 parameter.
* `method`: The method parameter.
* `parameter`: The parameter parameter.

**Returns**: The returned value.

---
### `Cv2.ConvexHull`
**Signature**: `void ConvexHull(Mat points, Mat hull, bool clockwise, bool returnPoints)`

Finds the convex hull of a point set. * * The function convexHull finds the convex hull of a 2D point set using the Sklansky's algorithm [Sklansky82] * that has *O(N logN)* complexity in the current implementation. * * **points** Input 2D point set, stored in array or Mat. * **hull** Output convex hull. It is either an integer vector of indices or vector of points. In * the first case, the hull elements are 0-based indices of the convex hull points in the original * array (since the set of convex hull points is a subset of the original point set). In the second * case, hull elements are the convex hull points themselves. * **clockwise** Orientation flag. If it is true, the output convex hull is oriented clockwise. * Otherwise, it is oriented counter-clockwise. The assumed coordinate system has its X axis pointing * to the right, and its Y axis pointing upwards. * **returnPoints** Operation flag. In case of a matrix, when the flag is true, the function * returns convex hull points. Otherwise, it returns indices of the convex hull points. When the * output array is array, the flag is ignored, and the output depends on the type of the * vector: int[] implies returnPoints=false, Point[] implies * returnPoints=true. * * **Note:** `points` and `hull` should be different arrays, inplace processing isn't supported. * * Check the corresponding tutorial for more details. * * useful links: * * https://www.learnopencv.com/convex-hull-using-opencv-in-python-and-c/

**Parameters**:
* `points`: The points parameter.
* `hull`: The hull parameter.
* `clockwise`: The clockwise parameter.
* `returnPoints`: The returnPoints parameter.

---
### `Cv2.ConvexityDefects`
**Signature**: `void ConvexityDefects(Mat contour, Mat convexhull, Mat convexityDefects)`

Finds the convexity defects of a contour. * * The figure below displays convexity defects of a hand contour: * *  * * **contour** Input contour. * **convexhull** Convex hull obtained using convexHull that should contain indices of the contour * points that make the hull. * **convexityDefects** The output vector of convexity defects. In C++ and the new C# * interface each convexity defect is represented as 4-element integer vector (a.k.a. `Vec4i`): * (start_index, end_index, farthest_pt_index, fixpt_depth), where indices are 0-based indices * in the original contour of the convexity defect beginning, end and the farthest point, and * fixpt_depth is fixed-point approximation (with 8 fractional bits) of the distance between the * farthest contour point and the hull. That is, to get the floating-point value of the depth will be * fixpt_depth/256.0.

**Parameters**:
* `contour`: The contour parameter.
* `convexhull`: The convexhull parameter.
* `convexityDefects`: The convexityDefects parameter.

---
### `Cv2.IsContourConvex`
**Signature**: `bool IsContourConvex(Mat contour)`

Tests a contour convexity. * * The function tests whether the input contour is convex or not. The contour must be simple, that is, * without self-intersections. Otherwise, the function output is undefined. * * **contour** Input array of 2D points, stored in [] or Mat

**Parameters**:
* `contour`: The contour parameter.

**Returns**: The returned value.

---
### `Cv2.IntersectConvexConvex`
**Signature**: `float IntersectConvexConvex(Mat p1, Mat p2, Mat p12, bool handleNested)`

Finds intersection of two convex polygons * * **p1** First polygon * **p2** Second polygon * **p12** Output polygon describing the intersecting area * **handleNested** When true, an intersection is found if one of the polygons is fully enclosed in the other. * When false, no intersection is found. If the polygons share a side or the vertex of one polygon lies on an edge * of the other, they are not considered nested and an intersection will be found regardless of the value of handleNested. * * **Returns**: Area of intersecting polygon. May be negative, if algorithm has not converged, e.g. non-convex input. * * **Note:** intersectConvexConvex doesn't confirm that both polygons are convex and will return invalid results if they aren't.

**Parameters**:
* `p1`: The p1 parameter.
* `p2`: The p2 parameter.
* `p12`: The p12 parameter.
* `handleNested`: The handleNested parameter.

**Returns**: The returned value.

---
### `Cv2.FitEllipse`
**Signature**: `RotatedRect? FitEllipse(Mat points)`

Fits an ellipse around a set of 2D points. * * The function calculates the ellipse that fits (in a least-squares sense) a set of 2D points best of * all. It returns the rotated rectangle in which the ellipse is inscribed. The first algorithm described by [Fitzgibbon95] * is used. Developer should keep in mind that it is possible that the returned * ellipse/rotatedRect data contains negative indices, due to the data points being close to the * border of the containing Mat element. * * **points** Input 2D point set, stored in [] or Mat * * **Note:** Input point types are `Point` or `Point2f` and at least 5 points are required. * **Note:** `getClosestEllipsePoints` function can be used to compute the ellipse fitting error.

**Parameters**:
* `points`: The points parameter.

**Returns**: The returned value.

---
### `Cv2.FitEllipseAMS`
**Signature**: `RotatedRect? FitEllipseAMS(Mat points)`

Fits an ellipse around a set of 2D points. * * The function calculates the ellipse that fits a set of 2D points. * It returns the rotated rectangle in which the ellipse is inscribed. * The Approximate Mean Square (AMS) proposed by [Taubin1991] is used. * * For an ellipse, this basis set is formula, * which is a set of six free coefficients formula. * However, to specify an ellipse, all that is needed is five numbers; the major and minor axes lengths formula, * the position formula, and the orientation formula. This is because the basis set includes lines, * quadratics, parabolic and hyperbolic functions as well as elliptical functions as possible fits. * If the fit is found to be a parabolic or hyperbolic function then the standard `fitEllipse` method is used. * The AMS method restricts the fit to parabolic, hyperbolic and elliptical curves * by imposing the condition that formula where * the matrices formula and formula are the partial derivatives of the design matrix formula with * respect to x and y. The matrices are formed row by row applying the following to * each of the points in the set: * [see mathematical equations in OpenCV documentation] * The AMS method minimizes the cost function * [see mathematical equations in OpenCV documentation] * * The minimum cost is found by solving the generalized eigenvalue problem. * * [see mathematical equations in OpenCV documentation] * * **points** Input 2D point set, stored in [] or Mat * * **Note:** Input point types are `Point` or `Point2f` and at least 5 points are required. * **Note:** `getClosestEllipsePoints` function can be used to compute the ellipse fitting error.

**Parameters**:
* `points`: The points parameter.

**Returns**: The returned value.

---
### `Cv2.FitEllipseDirect`
**Signature**: `RotatedRect? FitEllipseDirect(Mat points)`

Fits an ellipse around a set of 2D points. * * The function calculates the ellipse that fits a set of 2D points. * It returns the rotated rectangle in which the ellipse is inscribed. * The Direct least square (Direct) method by [oy1998NumericallySD] is used. * * For an ellipse, this basis set is formula, * which is a set of six free coefficients formula. * However, to specify an ellipse, all that is needed is five numbers; the major and minor axes lengths formula, * the position formula, and the orientation formula. This is because the basis set includes lines, * quadratics, parabolic and hyperbolic functions as well as elliptical functions as possible fits. * The Direct method confines the fit to ellipses by ensuring that formula. * The condition imposed is that formula which satisfies the inequality * and as the coefficients can be arbitrarily scaled is not overly restrictive. * * [see mathematical equations in OpenCV documentation] * * The minimum cost is found by solving the generalized eigenvalue problem. * * [see mathematical equations in OpenCV documentation] * * The system produces only one positive eigenvalue formula which is chosen as the solution * with its eigenvector formula. These are used to find the coefficients * * [see mathematical equations in OpenCV documentation] * The scaling factor guarantees that  formula. * * **points** Input 2D point set, stored in [] or Mat * * **Note:** Input point types are `Point` or `Point2f` and at least 5 points are required. * **Note:** `getClosestEllipsePoints` function can be used to compute the ellipse fitting error.

**Parameters**:
* `points`: The points parameter.

**Returns**: The returned value.

---
### `Cv2.GetClosestEllipsePoints`
**Signature**: `void GetClosestEllipsePoints(RotatedRect ellipse_params, Mat points, Mat closest_pts)`

Compute for each 2d point the nearest 2d point located on a given ellipse. * * The function computes the nearest 2d location on a given ellipse for a vector of 2d points and is based on [Chatfield2017] code. * This function can be used to compute for instance the ellipse fitting error. * * **ellipse_params** Ellipse parameters * **points** Input 2d points * **closest_pts** For each 2d point, their corresponding closest 2d point located on a given ellipse * * **Note:** Input point types are `Point` or `Point2f` * **See also:** fitEllipse, fitEllipseAMS, fitEllipseDirect

**Parameters**:
* `ellipse_params`: The ellipse_params parameter.
* `points`: The points parameter.
* `closest_pts`: The closest_pts parameter.

---
### `Cv2.FitLine`
**Signature**: `void FitLine(Mat points, Mat line, int distType, double param, double reps, double aeps)`

Fits a line to a 2D or 3D point set. * * The function fitLine fits a line to a 2D or 3D point set by minimizing formula where * formula is a distance between the formula point, the line and formula is a distance function, one * of the following: * -  DIST_L2 * [see mathematical formula in OpenCV docs] * - DIST_L1 * [see mathematical formula in OpenCV docs] * - DIST_L12 * [see mathematical formula in OpenCV docs] * - DIST_FAIR * [see mathematical formula in OpenCV docs] * - DIST_WELSCH * [see mathematical formula in OpenCV docs] * - DIST_HUBER * [see mathematical formula in OpenCV docs] * * The algorithm is based on the M-estimator ( <https://en.wikipedia.org/wiki/M-estimator> ) technique * that iteratively fits the line using the weighted least-squares algorithm. After each iteration the * weights formula are adjusted to be inversely proportional to formula . * * **points** Input vector of 2D or 3D points, stored in [] or Mat. * **line** Output line parameters. In case of 2D fitting, it should be a vector of 4 elements * (like Vec4f) - (vx, vy, x0, y0), where (vx, vy) is a normalized vector collinear to the line and * (x0, y0) is a point on the line. In case of 3D fitting, it should be a vector of 6 elements (like * Vec6f) - (vx, vy, vz, x0, y0, z0), where (vx, vy, vz) is a normalized vector collinear to the line * and (x0, y0, z0) is a point on the line. * **distType** Distance used by the M-estimator, see `DistanceTypes` * **param** Numerical parameter ( C ) for some types of distances. If it is 0, an optimal value * is chosen. * **reps** Sufficient accuracy for the radius (distance between the coordinate origin and the line). * **aeps** Sufficient accuracy for the angle. 0.01 would be a good default value for reps and aeps.

**Parameters**:
* `points`: The points parameter.
* `line`: The line parameter.
* `distType`: The distType parameter.
* `param`: The param parameter.
* `reps`: The reps parameter.
* `aeps`: The aeps parameter.

---
### `Cv2.PointPolygonTest`
**Signature**: `double PointPolygonTest(Mat contour, Point2f pt, bool measureDist)`

Performs a point-in-contour test. * * The function determines whether the point is inside a contour, outside, or lies on an edge (or * coincides with a vertex). It returns positive (inside), negative (outside), or zero (on an edge) * value, correspondingly. When measureDist=false , the return value is +1, -1, and 0, respectively. * Otherwise, the return value is a signed distance between the point and the nearest contour edge. * * See below a sample output of the function where each image pixel is tested against the contour: * *  * * **contour** Input contour. * **pt** Point tested against the contour. * **measureDist** If true, the function estimates the signed distance from the point to the * nearest contour edge. Otherwise, the function only checks if the point is inside a contour or not.

**Parameters**:
* `contour`: The contour parameter.
* `pt`: The pt parameter.
* `measureDist`: The measureDist parameter.

**Returns**: The returned value.

---
### `Cv2.RotatedRectangleIntersection`
**Signature**: `int RotatedRectangleIntersection(RotatedRect rect1, RotatedRect rect2, Mat intersectingRegion)`

Finds out if there is any intersection between two rotated rectangles. * * If there is then the vertices of the intersecting region are returned as well. * * Below are some examples of intersection configurations. The hatched pattern indicates the * intersecting region and the red vertices are returned by the function. * *  * * **rect1** First rectangle * **rect2** Second rectangle * **intersectingRegion** The output array of the vertices of the intersecting region. It returns * at most 8 vertices. Stored as Point2f[] or Mat as Mx1 of type CV_32FC2. * **Returns**: One of `RectanglesIntersectTypes`

**Parameters**:
* `rect1`: The rect1 parameter.
* `rect2`: The rect2 parameter.
* `intersectingRegion`: The intersectingRegion parameter.

**Returns**: The returned value.

---
### `Cv2.ArcLength`
**Signature**: `double ArcLength(Mat curve, bool closed)`

Calculates a contour perimeter or a curve length. * * The function computes a curve length or a closed contour perimeter. * * **curve** Input array of 2D points, stored in array or Mat. * **closed** Flag indicating whether the curve is closed or not.

**Parameters**:
* `curve`: The curve parameter.
* `closed`: The closed parameter.

**Returns**: The returned value.

---
### `Cv2.ContourArea`
**Signature**: `double ContourArea(Mat contour, bool oriented)`

Calculates a contour area.

The function computes a contour area. Similarly to moments, the area is computed using the Green formula. Thus, the returned area and the number of non-zero pixels, if you draw the contour using `drawContours` or `fillPoly`, can be different. Also, the function will most certainly give wrong results for contours with self-intersections.

Example:
```csharp
Point[] contour = { new Point(0, 0), new Point(10, 0), new Point(10, 10), new Point(5, 4) };
// contourMat populated with 2D points:
using var contourMat = new Mat();
double area0 = Cv2.ContourArea(contourMat, false);
```

**contour** Input array of 2D points (contour vertices), stored in array or Mat.
**oriented** Oriented area flag. If it is true, the function returns a signed area value, depending on the contour orientation (clockwise or counter-clockwise). Using this feature you can determine orientation of a contour by taking the sign of an area. By default, the parameter is false, which means that the absolute value is returned.

**Parameters**:
* `contour`: The contour parameter.
* `oriented`: The oriented parameter.

**Returns**: The returned value.

---
### `Cv2.BoundingRect`
**Signature**: `Rect BoundingRect(Mat array)`

Calculates the up-right bounding rectangle of a point set or non-zero pixels of gray-scale image. * * The function calculates and returns the minimal up-right bounding rectangle for the specified point set or * non-zero pixels of gray-scale image. * * **array** Input gray-scale image or 2D point set, stored in array or Mat.

**Parameters**:
* `array`: The array parameter.

**Returns**: The returned value.

---
### `Cv2.GetRotationMatrix2D`
**Signature**: `Mat? GetRotationMatrix2D(Point2f center, double angle, double scale)`

Calculates an affine matrix of 2D rotation.

**Detailed Remarks**:
The function calculates the following matrix:
[see mathematical formula in OpenCV docs]
where
[see mathematical formula in OpenCV docs]
The transformation maps the rotation center to itself. If this is not the target, adjust the shift.
**See also**: getAffineTransform, warpAffine, transform

**Parameters**:
* `center`: Center of the rotation in the source image.
* `angle`: Rotation angle in degrees. Positive values mean counter-clockwise rotation (the coordinate origin is assumed to be the top-left corner).
* `scale`: Isotropic scale factor.

**Returns**: The returned value.

---
### `Cv2.InvertAffineTransform`
**Signature**: `void InvertAffineTransform(Mat M, Mat iM)`

Inverts an affine transformation. * * The function computes an inverse affine transformation represented by formula matrix M: * * [see mathematical formula in OpenCV docs] * * The result is also a formula matrix of the same type as M. * * **M** Original affine transformation. * **iM** Output reverse affine transformation.

**Parameters**:
* `M`: The M parameter.
* `iM`: The iM parameter.

---
### `Cv2.GetPerspectiveTransform`
**Signature**: `Mat? GetPerspectiveTransform(Mat src, Mat dst, int solveMethod)`

Calculates a perspective transform from four pairs of the corresponding points. * * The function calculates the formula matrix of a perspective transform so that: * * [see mathematical formula in OpenCV docs] * * where * * [see mathematical formula in OpenCV docs] * * **src** Coordinates of quadrangle vertices in the source image. * **dst** Coordinates of the corresponding quadrangle vertices in the destination image. * **solveMethod** method passed to solve (#DecompTypes) * * **See also:**  findHomography, warpPerspective, perspectiveTransform

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `solveMethod`: The solveMethod parameter.

**Returns**: The returned value.

---
### `Cv2.GetAffineTransform`
**Signature**: `Mat? GetAffineTransform(Mat src, Mat dst)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).

**Returns**: The returned value.

---
### `Cv2.Rodrigues`
**Signature**: `void Rodrigues(Mat src, Mat dst, Mat? jacobian)`

Converts a rotation matrix to a rotation vector or vice versa.

**Detailed Remarks**:
.: info Note
More information about the computation of the derivative of a 3D rotation matrix with respect to its exponential coordinate
can be found in:
- A Compact Formula for the Derivative of a 3-D Rotation in Exponential Coordinates, Guillermo Gallego, Anthony J. Yezzi **Citation**:  Gallego2014ACF
.:
.: info Note
Useful information on SE(3) and Lie Groups can be found in:
- A tutorial on SE(3) transformation parameterizations and on-manifold optimization, Jose-Luis Blanco **Citation**:  blanco2010tutorial
- Lie Groups for 2D and 3D Transformation, Ethan Eade **Citation**:  Eade17
- A micro Lie theory for state estimation in robotics, Joan Solà, Jérémie Deray, Dinesh Atchuthan **Citation**:  Sol2018AML
.:

**Parameters**:
* `src`: Input rotation vector (3x1 or 1x3) or rotation matrix (3x3).
* `dst`: Output rotation matrix (3x3) or rotation vector (3x1 or 1x3), respectively.
* `jacobian`: Optional output Jacobian matrix, 3x9 or 9x3, which is a matrix of partial derivatives of the output array components with respect to the input array components. [see mathematical formula in OpenCV docs] Inverse transformation can be also done easily, since [see mathematical formula in OpenCV docs] A rotation vector is a convenient and most compact representation of a rotation matrix (since any rotation matrix has just 3 degrees of freedom). The representation is used in the global 3D geometry optimization procedures like `CalibrateCamera`, `stereoCalibrate`, or `SolvePnP` .

---
### `Cv2.FindHomography`
**Signature**: `Mat? FindHomography(Mat srcPoints, Mat dstPoints, int method, double ransacReprojThreshold, Mat? mask, int maxIters, double confidence)`

Finds a perspective transformation between two planes.

**Detailed Remarks**:
.: info Note
Whenever an formula matrix cannot be estimated, an empty one will be returned.
**See also**: 
getAffineTransform, estimateAffine2D, estimateAffinePartial2D, getPerspectiveTransform, warpPerspective,
perspectiveTransform
.:

**Parameters**:
* `srcPoints`: Coordinates of the points in the original plane, a matrix of the type CV_32FC2 or Point2f[] .
* `dstPoints`: Coordinates of the points in the target plane, a matrix of the type CV_32FC2 or a Point2f[] .
* `method`: Method used to compute a homography matrix. The following methods are possible: -   **0** - a regular method using all the points, i.e., the least squares method -   `RANSAC` - RANSAC-based robust method -   `LMEDS` - Least-Median robust method -   `RHO` - PROSAC-based robust method
* `ransacReprojThreshold`: Maximum allowed reprojection error to treat a point pair as an inlier (used in the RANSAC and RHO methods only). That is, if [see mathematical formula in OpenCV docs] then the point formula is considered as an outlier. If srcPoints and dstPoints are measured in pixels, it usually makes sense to set this parameter somewhere in the range of 1 to 10.
* `mask`: Optional output mask set by a robust method ( RANSAC or LMeDS ). Note that the input mask values are ignored.
* `maxIters`: The maximum number of RANSAC iterations.
* `confidence`: Confidence level, between 0 and 1. The function finds and returns the perspective transformation formula between the source and the destination planes: [see mathematical formula in OpenCV docs] so that the back-projection error [see mathematical formula in OpenCV docs] is minimized. If the parameter method is set to the default value 0, the function uses all the point pairs to compute an initial homography estimate with a simple least-squares scheme. However, if not all of the point pairs ( formula, formula ) fit the rigid perspective transformation (that is, there are some outliers), this initial estimate will be poor. In this case, you can use one of the three robust methods. The methods RANSAC, LMeDS and RHO try many different random subsets of the corresponding point pairs (of four pairs each, collinear pairs are discarded), estimate the homography matrix using this subset and a simple least-squares algorithm, and then compute the quality/goodness of the computed homography (which is the number of inliers for RANSAC or the least median re-projection error for LMeDS). The best subset is then used to produce the initial estimate of the homography matrix and the mask of inliers/outliers. Regardless of the method, robust or not, the computed homography matrix is refined further (using inliers only in case of a robust method) with the Levenberg-Marquardt method to reduce the re-projection error even more. The methods RANSAC and RHO can handle practically any ratio of outliers but need a threshold to distinguish inliers from outliers. The method LMeDS does not need any threshold but it works correctly only when there are more than 50% of inliers. Finally, if there are no outliers and the noise is rather small, use the default method (method=0). The function is used to find initial intrinsic and extrinsic matrices. Homography matrix is determined up to a scale. If formula is non-zero, the matrix is normalized so that formula.

**Returns**: The returned value.

---
### `Cv2.FindHomography`
**Signature**: `Mat? FindHomography(Mat srcPoints, Mat dstPoints, Mat mask, UsacParams @params)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `srcPoints`: The srcPoints parameter.
* `dstPoints`: The dstPoints parameter.
* `mask`: Optional operation mask.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.RQDecomp3x3`
**Signature**: `IntPtr RQDecomp3x3(Mat src, Mat mtxR, Mat mtxQ, Mat? Qx, Mat? Qy, Mat? Qz)`

Computes an RQ decomposition of 3x3 matrices.

**Parameters**:
* `src`: 3x3 input matrix.
* `mtxR`: Output 3x3 upper-triangular matrix.
* `mtxQ`: Output 3x3 orthogonal matrix.
* `Qx`: Optional output 3x3 rotation matrix around x-axis.
* `Qy`: Optional output 3x3 rotation matrix around y-axis.
* `Qz`: Optional output 3x3 rotation matrix around z-axis. The function computes a RQ decomposition using the given rotations. This function is used in `decomposeProjectionMatrix` to decompose the left 3x3 submatrix of a projection matrix into a camera and a rotation matrix. It optionally returns three rotation matrices, one for each axis, and the three Euler angles in degrees (as the return value) that could be used in OpenGL. Note, there is always more than one sequence of rotations about the three principal axes that results in the same orientation of an object, e.g. see [Slabaugh] . Returned three rotation matrices and corresponding three Euler angles are only one of the possible solutions.

**Returns**: The returned value.

---
### `Cv2.DecomposeProjectionMatrix`
**Signature**: `void DecomposeProjectionMatrix(Mat projMatrix, Mat cameraMatrix, Mat rotMatrix, Mat transVect, Mat? rotMatrixX, Mat? rotMatrixY, Mat? rotMatrixZ, Mat? eulerAngles)`

Decomposes a projection matrix into a rotation matrix and a camera intrinsic matrix.

**Parameters**:
* `projMatrix`: 3x4 input projection matrix P.
* `cameraMatrix`: Output 3x3 camera intrinsic matrix formula.
* `rotMatrix`: Output 3x3 external rotation matrix R.
* `transVect`: Output 4x1 translation vector T.
* `rotMatrixX`: Optional 3x3 rotation matrix around x-axis.
* `rotMatrixY`: Optional 3x3 rotation matrix around y-axis.
* `rotMatrixZ`: Optional 3x3 rotation matrix around z-axis.
* `eulerAngles`: Optional three-element vector containing three Euler angles of rotation in degrees. The function computes a decomposition of a projection matrix into a calibration and a rotation matrix and the position of a camera. It optionally returns three rotation matrices, one for each axis, and three Euler angles that could be used in OpenGL. Note, there is always more than one sequence of rotations about the three principal axes that results in the same orientation of an object, e.g. see [Slabaugh] . Returned three rotation matrices and corresponding three Euler angles are only one of the possible solutions. The function is based on `RQDecomp3x3` .

---
### `Cv2.MatMulDeriv`
**Signature**: `void MatMulDeriv(Mat A, Mat B, Mat dABdA, Mat dABdB)`

Computes partial derivatives of the matrix product for each multiplied matrix.

**Parameters**:
* `A`: First multiplied matrix.
* `B`: Second multiplied matrix.
* `dABdA`: First output derivative matrix d(A\*B)/dA of size formula .
* `dABdB`: Second output derivative matrix d(A\*B)/dB of size formula . The function computes partial derivatives of the elements of the matrix product formula with regard to the elements of each of the two input matrices. The function is used to compute the Jacobian matrices in `stereoCalibrate` but can also be used in any other similar optimization function.

---
### `Cv2.ComposeRT`
**Signature**: `void ComposeRT(Mat rvec1, Mat tvec1, Mat rvec2, Mat tvec2, Mat rvec3, Mat tvec3, Mat? dr3dr1, Mat? dr3dt1, Mat? dr3dr2, Mat? dr3dt2, Mat? dt3dr1, Mat? dt3dt1, Mat? dt3dr2, Mat? dt3dt2)`

Combines two rotation-and-shift transformations.

**Parameters**:
* `rvec1`: First rotation vector.
* `tvec1`: First translation vector.
* `rvec2`: Second rotation vector.
* `tvec2`: Second translation vector.
* `rvec3`: Output rotation vector of the superposition.
* `tvec3`: Output translation vector of the superposition.
* `dr3dr1`: Optional output derivative of rvec3 with regard to rvec1
* `dr3dt1`: Optional output derivative of rvec3 with regard to tvec1
* `dr3dr2`: Optional output derivative of rvec3 with regard to rvec2
* `dr3dt2`: Optional output derivative of rvec3 with regard to tvec2
* `dt3dr1`: Optional output derivative of tvec3 with regard to rvec1
* `dt3dt1`: Optional output derivative of tvec3 with regard to tvec1
* `dt3dr2`: Optional output derivative of tvec3 with regard to rvec2
* `dt3dt2`: Optional output derivative of tvec3 with regard to tvec2 The functions compute: [see mathematical formula in OpenCV docs] where formula denotes a rotation vector to a rotation matrix transformation, and formula denotes the inverse transformation. See `Rodrigues` for details. Also, the functions can compute the derivatives of the output vectors with regards to the input vectors (see `matMulDeriv` ). The functions are used inside `stereoCalibrate` but can also be used in your own code where Levenberg-Marquardt or another gradient-based solver is used to optimize a function that contains a matrix multiplication.

---
### `Cv2.ProjectPoints`
**Signature**: `void ProjectPoints(Mat objectPoints, Mat rvec, Mat tvec, Mat cameraMatrix, Mat distCoeffs, Mat imagePoints, Mat? jacobian, double aspectRatio)`

Projects 3D points to an image plane.

**Detailed Remarks**:
The function computes the 2D projections of 3D points to the image plane, given intrinsic and
extrinsic camera parameters. Optionally, the function computes Jacobians -matrices of partial
derivatives of image points coordinates (as functions of all the input parameters) with respect to
the particular parameters, intrinsic and/or extrinsic. The Jacobians are used during the global
optimization in `CalibrateCamera`, `SolvePnP`, and `stereoCalibrate`. The function itself
can also be used to compute a re-projection error, given the current intrinsic and extrinsic
parameters.
.: info Note
**Coordinate Systems:**
- **Input (`objectPoints`)**: 3D points in the **world coordinate frame**.
- **Output (`imagePoints`)**: 2D projections in **pixel coordinates** of the image plane, with distortion applied.
The coordinates formula are measured in pixels from the top-left corner of the image.
The transformation chain is: World coordinates → Camera coordinates (via rvec/tvec) → Normalized camera coordinates
→ Distortion applied → Pixel coordinates (via cameraMatrix).
.:
.: info Note
By setting rvec = tvec = formula, or by setting cameraMatrix to a 3x3 identity matrix,
or by passing zero distortion coefficients, one can get various useful partial cases of the
function. This means, one can compute the distorted coordinates for a sparse set of points or apply
a perspective transformation (and also compute the derivatives) in the ideal zero-distortion setup.
.:

**Parameters**:
* `objectPoints`: Array of object points expressed wrt. the world coordinate frame. A 3xN/Nx3 1-channel or 1xN/Nx1 3-channel (or Point3f[] ), where N is the number of points in the view.
* `rvec`: The rotation vector (`Rodrigues`) that, together with tvec, performs a change of basis from world to camera coordinate system, see `CalibrateCamera` for details.
* `tvec`: The translation vector, see parameter description above.
* `cameraMatrix`: Camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula . If the vector is empty, the zero distortion coefficients are assumed.
* `imagePoints`: Output array of image points in **pixel coordinates**, 1xN/Nx1 2-channel, or Point2f[] .
* `jacobian`: Optional output 2Nx(10+\<numDistCoeffs\>) jacobian matrix of derivatives of image points with respect to components of the rotation vector, translation vector, focal lengths, coordinates of the principal point and the distortion coefficients. In the old interface different components of the jacobian are returned via different output parameters.
* `aspectRatio`: Optional "fixed aspect ratio" parameter. If the parameter is not 0, the function assumes that the aspect ratio (formula) is fixed and correspondingly adjusts the jacobian matrix.

---
### `Cv2.ProjectPoints`
**Signature**: `void ProjectPoints(Mat objectPoints, Mat rvec, Mat tvec, Mat cameraMatrix, Mat distCoeffs, Mat imagePoints, Mat dpdr, Mat dpdt, Mat? dpdf, Mat? dpdc, Mat? dpdk, Mat? dpdo, double aspectRatio)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `rvec`: The rvec parameter.
* `tvec`: The tvec parameter.
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `imagePoints`: The imagePoints parameter.
* `dpdr`: The dpdr parameter.
* `dpdt`: The dpdt parameter.
* `dpdf`: The dpdf parameter.
* `dpdc`: The dpdc parameter.
* `dpdk`: The dpdk parameter.
* `dpdo`: The dpdo parameter.
* `aspectRatio`: The aspectRatio parameter.

---
### `Cv2.SolvePnP`
**Signature**: `bool SolvePnP(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int flags)`

Finds an object pose formula from 3D-2D point correspondences:

**Detailed Remarks**:
{ width=50% }
**See also**: `calib3d_SolvePnP`
This function returns the rotation and the translation vectors that transform a 3D point expressed in the object
coordinate frame to the camera coordinate frame, using different methods:
- P3P methods (`SOLVEPNP_P3P`, `SOLVEPNP_AP3P`): need 4 input points to return a unique solution.
- `SOLVEPNP_IPPE` Input points must be >= 4 and object points must be coplanar.
- `SOLVEPNP_IPPE_SQUARE` Special case suitable for marker pose estimation.
Number of input points must be 4. Object points must be defined in the following order:
- point 0: [-squareLength / 2,  squareLength / 2, 0]
- point 1: [ squareLength / 2,  squareLength / 2, 0]
- point 2: [ squareLength / 2, -squareLength / 2, 0]
- point 3: [-squareLength / 2, -squareLength / 2, 0]
- for all the other flags, number of input points must be >= 4 and object points can be in any configuration.
.: info Note

-   An example of how to use SolvePnP for planar augmented reality can be found at
-    where Mat, in order to use a subset of
it as, e.g., imagePoints, one must effectively copy it into a new array: imagePoints =
Mat
-   The minimum number of points is 4 in the general case. In the case of `SOLVEPNP_P3P` and `SOLVEPNP_AP3P`
methods, it is required to use exactly 4 points (the first 3 points are used to estimate all the solutions
of the P3P problem, the last one is used to retain the best solution that minimizes the reprojection error).
-   With `SOLVEPNP_ITERATIVE` method and `useExtrinsicGuess=true`, the minimum number of points is 3 (3 points
are sufficient to compute a pose but there are up to 4 solutions). The initial solution should be close to the
global solution to converge. The function returns true if some solution is found. User code is responsible for
solution quality assessment.
-   With `SOLVEPNP_IPPE` input points must be >= 4 and object points must be coplanar.
-   With `SOLVEPNP_IPPE_SQUARE` this is a special case suitable for marker pose estimation.
Number of input points must be 4. Object points must be defined in the following order:
- point 0: [-squareLength / 2,  squareLength / 2, 0]
- point 1: [ squareLength / 2,  squareLength / 2, 0]
- point 2: [ squareLength / 2, -squareLength / 2, 0]
- point 3: [-squareLength / 2, -squareLength / 2, 0]
-  With `SOLVEPNP_SQPNP` input points must be >= 3
.:

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can be also passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can be also passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvec`: Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system.
* `tvec`: Output translation vector.
* `useExtrinsicGuess`: Parameter used for `SOLVEPNP_ITERATIVE`. If true (1), the function uses the provided rvec and tvec values as initial approximations of the rotation and translation vectors, respectively, and further optimizes them.
* `flags`: Method for solving a PnP problem: see `calib3d_SolvePnP_flags` More information about Perspective-n-Points is described in `calib3d_SolvePnP`

**Returns**: The returned value.

---
### `Cv2.SolvePnPRansac`
**Signature**: `bool SolvePnPRansac(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int iterationsCount, float reprojectionError, double confidence, Mat? inliers, int flags)`

Finds an object pose formula from 3D-2D point correspondences using the RANSAC scheme to deal with bad matches.

**Detailed Remarks**:
{ width=50% }
**See also**: `calib3d_SolvePnP`
.: info Note

-   An example of how to use SolvePnPRansac for object detection can be found at
`tutorial_real_time_pose`
-   The default method used to estimate the camera pose for the Minimal Sample Sets step
is `SOLVEPNP_EPNP`. Exceptions are:
- if you choose `SOLVEPNP_P3P` or `SOLVEPNP_AP3P`, these methods will be used.
- if the number of input points is equal to 4, `SOLVEPNP_P3P` is used.
-   The method used to estimate the camera pose using all the inliers is defined by the
flags parameters unless it is equal to `SOLVEPNP_P3P` or `SOLVEPNP_AP3P`. In this case,
the method `SOLVEPNP_EPNP` will be used instead.
.:

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can be also passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can be also passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvec`: Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system.
* `tvec`: Output translation vector.
* `useExtrinsicGuess`: Parameter used for `SOLVEPNP_ITERATIVE`. If true (1), the function uses the provided rvec and tvec values as initial approximations of the rotation and translation vectors, respectively, and further optimizes them.
* `iterationsCount`: Number of iterations.
* `reprojectionError`: Inlier threshold value used by the RANSAC procedure. The parameter value is the maximum allowed distance between the observed and computed point projections to consider it an inlier.
* `confidence`: The probability that the algorithm produces a useful result.
* `inliers`: Output vector that contains indices of inliers in objectPoints and imagePoints .
* `flags`: Method for solving a PnP problem (see `SolvePnP` ). The function estimates an object pose given a set of object points, their corresponding image projections, as well as the camera intrinsic matrix and the distortion coefficients. This function finds such a pose that minimizes reprojection error, that is, the sum of squared distances between the observed projections imagePoints and the projected (using `projectPoints` ) objectPoints. The use of RANSAC makes the function resistant to outliers.

**Returns**: The returned value.

---
### `Cv2.SolvePnPRansac`
**Signature**: `bool SolvePnPRansac(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, Mat inliers, UsacParams? @params)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `rvec`: The rvec parameter.
* `tvec`: The tvec parameter.
* `inliers`: The inliers parameter.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.SolveP3P`
**Signature**: `int SolveP3P(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, int flags)`

Finds an object pose formula from **3** 3D-2D point correspondences.

**Detailed Remarks**:
{ width=50% }
**See also**: `calib3d_SolvePnP`
.: info Note

The solutions are sorted by reprojection errors (lowest to highest).
.:

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, 3x3 1-channel or 1x3/3x1 3-channel. Point3f[] can be also passed here.
* `imagePoints`: Array of corresponding image points, 3x2 1-channel or 1x3/3x1 2-channel. Point2f[] can be also passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvecs`: Output rotation vectors (see `Rodrigues` ) that, together with tvecs, brings points from the model coordinate system to the camera coordinate system. A P3P problem has up to 4 solutions.
* `tvecs`: Output translation vectors.
* `flags`: Method for solving a P3P problem: -   `SOLVEPNP_P3P` Method is based on the paper of Ding, Y., Yang, J., Larsson, V., Olsson, C., & Åstrom, K. "Revisiting the P3P Problem" ([ding2023revisiting]). -   `SOLVEPNP_AP3P` Method is based on the paper of T. Ke and S. Roumeliotis. "An Efficient Algebraic Solution to the Perspective-Three-Point Problem" ([Ke17]). The function estimates the object pose given 3 object points, their corresponding image projections, as well as the camera intrinsic matrix and the distortion coefficients.

**Returns**: The returned value.

---
### `Cv2.SolvePnPRefineLM`
**Signature**: `void SolvePnPRefineLM(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, TermCriteria criteria)`

Refine a pose (the translation and the rotation that transform a 3D point expressed in the object coordinate frame to the camera coordinate frame) from a 3D-2D point correspondences and starting from an initial solution.

**Detailed Remarks**:
**See also**: `calib3d_SolvePnP`

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can also be passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can also be passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvec`: Input/Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system. Input values are used as an initial solution.
* `tvec`: Input/Output translation vector. Input values are used as an initial solution.
* `criteria`: Criteria when to stop the Levenberg-Marquard iterative algorithm. The function refines the object pose given at least 3 object points, their corresponding image projections, an initial solution for the rotation and translation vector, as well as the camera intrinsic matrix and the distortion coefficients. The function minimizes the projection error with respect to the rotation and the translation vectors, according to a Levenberg-Marquardt iterative minimization [Madsen04] [Eade13] process.

---
### `Cv2.SolvePnPRefineVVS`
**Signature**: `void SolvePnPRefineVVS(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, TermCriteria criteria, double VVSlambda)`

Refine a pose (the translation and the rotation that transform a 3D point expressed in the object coordinate frame to the camera coordinate frame) from a 3D-2D point correspondences and starting from an initial solution.

**Detailed Remarks**:
**See also**: `calib3d_SolvePnP`

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can also be passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can also be passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvec`: Input/Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system. Input values are used as an initial solution.
* `tvec`: Input/Output translation vector. Input values are used as an initial solution.
* `criteria`: Criteria when to stop the Levenberg-Marquard iterative algorithm.
* `VVSlambda`: Gain for the virtual visual servoing control law, equivalent to the formula gain in the Damped Gauss-Newton formulation. The function refines the object pose given at least 3 object points, their corresponding image projections, an initial solution for the rotation and translation vector, as well as the camera intrinsic matrix and the distortion coefficients. The function minimizes the projection error with respect to the rotation and the translation vectors, using a virtual visual servoing (VVS) [Chaumette06] [Marchand16] scheme.

---
### `Cv2.SolvePnPGeneric`
**Signature**: `int SolvePnPGeneric(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, bool useExtrinsicGuess, int flags, Mat? rvec, Mat? tvec, Mat? reprojectionError)`

Finds an object pose formula from 3D-2D point correspondences.

**Detailed Remarks**:
{ width=50% }
**See also**: `calib3d_SolvePnP`
This function returns a list of all the possible solutions (a solution is a <rotation vector, translation vector>
couple), depending on the number of input points and the chosen method:
- P3P methods (`SOLVEPNP_P3P`, `SOLVEPNP_AP3P`): 3 or 4 input points. Number of returned solutions can be between 0 and 4 with 3 input points.
- `SOLVEPNP_IPPE` Input points must be >= 4 and object points must be coplanar. Returns 2 solutions.
- `SOLVEPNP_IPPE_SQUARE` Special case suitable for marker pose estimation.
Number of input points must be 4 and 2 solutions are returned. Object points must be defined in the following order:
- point 0: [-squareLength / 2,  squareLength / 2, 0]
- point 1: [ squareLength / 2,  squareLength / 2, 0]
- point 2: [ squareLength / 2, -squareLength / 2, 0]
- point 3: [-squareLength / 2, -squareLength / 2, 0]
- for all the other flags, number of input points must be >= 4 and object points can be in any configuration.
Only 1 solution is returned.
.: info Note

-   An example of how to use SolvePnP for planar augmented reality can be found at
-    where Mat, in order to use a subset of
it as, e.g., imagePoints, one must effectively copy it into a new array: imagePoints =
Mat
-   The minimum number of points is 4 in the general case. In the case of `SOLVEPNP_P3P` and `SOLVEPNP_AP3P`
methods, it is required to use exactly 4 points (the first 3 points are used to estimate all the solutions
of the P3P problem, the last one is used to retain the best solution that minimizes the reprojection error).
-   With `SOLVEPNP_ITERATIVE` method and `useExtrinsicGuess=true`, the minimum number of points is 3 (3 points
are sufficient to compute a pose but there are up to 4 solutions). The initial solution should be close to the
global solution to converge.
-   With `SOLVEPNP_IPPE` input points must be >= 4 and object points must be coplanar.
-   With `SOLVEPNP_IPPE_SQUARE` this is a special case suitable for marker pose estimation.
Number of input points must be 4. Object points must be defined in the following order:
- point 0: [-squareLength / 2,  squareLength / 2, 0]
- point 1: [ squareLength / 2,  squareLength / 2, 0]
- point 2: [ squareLength / 2, -squareLength / 2, 0]
- point 3: [-squareLength / 2, -squareLength / 2, 0]
-   With `SOLVEPNP_SQPNP` input points must be >= 3
.:

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can be also passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can be also passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `rvecs`: Vector of output rotation vectors (see `Rodrigues` ) that, together with tvecs, brings points from the model coordinate system to the camera coordinate system.
* `tvecs`: Vector of output translation vectors.
* `useExtrinsicGuess`: Parameter used for `SOLVEPNP_ITERATIVE`. If true (1), the function uses the provided rvec and tvec values as initial approximations of the rotation and translation vectors, respectively, and further optimizes them.
* `flags`: Method for solving a PnP problem: see `calib3d_SolvePnP_flags`
* `rvec`: Rotation vector used to initialize an iterative PnP refinement algorithm, when flag is `SOLVEPNP_ITERATIVE` and useExtrinsicGuess is set to true.
* `tvec`: Translation vector used to initialize an iterative PnP refinement algorithm, when flag is `SOLVEPNP_ITERATIVE` and useExtrinsicGuess is set to true.
* `reprojectionError`: Optional vector of reprojection error, that is the RMS error (formula) between the input image points and the 3D object points projected with the estimated pose. More information is described in `calib3d_SolvePnP`

**Returns**: The returned value.

---
### `Cv2.ConvertPointsToHomogeneous`
**Signature**: `void ConvertPointsToHomogeneous(Mat src, Mat dst, int dtype)`

Converts points from Euclidean to homogeneous space.

**Parameters**:
* `src`: Input vector of N-dimensional points.
* `dst`: Output vector of N+1-dimensional points.
* `dtype`: The desired output array depth (either CV_32F or CV_64F are currently supported). If it's -1, then it's set automatically to CV_32F or CV_64F, depending on the input depth. The function converts points from Euclidean to homogeneous space by appending 1's to the tuple of point coordinates. That is, each point (x1, x2, ..., xn) is converted to (x1, x2, ..., xn, 1).

---
### `Cv2.ConvertPointsFromHomogeneous`
**Signature**: `void ConvertPointsFromHomogeneous(Mat src, Mat dst, int dtype)`

Converts points from homogeneous to Euclidean space.

**Parameters**:
* `src`: Input vector of N-dimensional points.
* `dst`: Output vector of N-1-dimensional points.
* `dtype`: The desired output array depth (either CV_32F or CV_64F are currently supported). If it's -1, then it's set automatically to CV_32F or CV_64F, depending on the input depth. The function converts points homogeneous to Euclidean space using perspective projection. That is, each point (x1, x2, ... x(n-1), xn) is converted to (x1/xn, x2/xn, ..., x(n-1)/xn). When xn=0, the output point coordinates will be (0,0,0,...).

---
### `Cv2.FindFundamentalMat`
**Signature**: `Mat? FindFundamentalMat(Mat points1, Mat points2, int method, double ransacReprojThreshold, double confidence, int maxIters, Mat? mask)`

Calculates a fundamental matrix from the corresponding points in two images.

**Parameters**:
* `points1`: Array of N points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1 .
* `method`: Method for computing a fundamental matrix. -   `FM_7POINT` for a 7-point algorithm. formula -   `FM_8POINT` for an 8-point algorithm. formula -   `FM_RANSAC` for the RANSAC algorithm. formula -   `FM_LMEDS` for the LMedS algorithm. formula
* `ransacReprojThreshold`: Parameter used only for RANSAC. It is the maximum distance from a point to an epipolar line in pixels, beyond which the point is considered an outlier and is not used for computing the final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the point localization, image resolution, and the image noise.
* `confidence`: Parameter used for the RANSAC and LMedS methods only. It specifies a desirable level of confidence (probability) that the estimated matrix is correct.
* `maxIters`: The maximum number of robust method iterations. The epipolar geometry is described by the following equation: [see mathematical formula in OpenCV docs] where formula is a fundamental matrix, formula and formula are corresponding points in the first and the second images, respectively. The function calculates the fundamental matrix using one of four methods listed above and returns the found fundamental matrix. Normally just one matrix is found. But in case of the 7-point algorithm, the function may return up to 3 solutions ( formula matrix that stores all 3 matrices sequentially). The calculated fundamental matrix may be passed further to `computeCorrespondEpilines` that finds the epipolar lines corresponding to the specified points. It can also be passed to `stereoRectifyUncalibrated` to compute the rectification transformation. 

```csharp
// Example. Estimation of fundamental matrix using the RANSAC algorithm
using var points1 = new Mat();
using var points2 = new Mat();
using var fundamental_matrix = Cv2.FindFundamentalMat(points1, points2, (int)FundamentalMatMethod.FmRansac, 3.0, 0.99, null);
```
* `mask`: Optional operation mask.

**Returns**: The returned value.

---
### `Cv2.FindFundamentalMat`
**Signature**: `Mat? FindFundamentalMat(Mat points1, Mat points2, int method, double ransacReprojThreshold, double confidence, Mat? mask)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `points1`: The points1 parameter.
* `points2`: The points2 parameter.
* `method`: The method parameter.
* `ransacReprojThreshold`: The ransacReprojThreshold parameter.
* `confidence`: The confidence parameter.
* `mask`: Optional operation mask.

**Returns**: The returned value.

---
### `Cv2.FindFundamentalMat`
**Signature**: `Mat? FindFundamentalMat(Mat points1, Mat points2, Mat mask, UsacParams @params)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `points1`: The points1 parameter.
* `points2`: The points2 parameter.
* `mask`: Optional operation mask.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.FindEssentialMat`
**Signature**: `Mat? FindEssentialMat(Mat points1, Mat points2, Mat cameraMatrix, int method, double prob, double threshold, int maxIters, Mat? mask)`

Calculates an essential matrix from the corresponding points in two images.

**Parameters**:
* `points1`: Array of N (N \>= 5) 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1.
* `cameraMatrix`: Camera intrinsic matrix formula . Note that this function assumes that points1 and points2 are feature points from cameras with the same camera intrinsic matrix. If this assumption does not hold for your use case, use another function overload or `undistortPoints` with `P = NoArray()` for both cameras to transform image points to normalized image coordinates, which are valid for the identity camera intrinsic matrix. When passing these coordinates, pass the identity matrix for this parameter.
* `method`: Method for computing an essential matrix. -   `RANSAC` for the RANSAC algorithm. -   `LMEDS` for the LMedS algorithm.
* `prob`: Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of confidence (probability) that the estimated matrix is correct.
* `threshold`: Parameter used for RANSAC. It is the maximum distance from a point to an epipolar line in pixels, beyond which the point is considered an outlier and is not used for computing the final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the point localization, image resolution, and the image noise.
* `maxIters`: The maximum number of robust method iterations. This function estimates essential matrix based on the five-point algorithm solver in [Nister03] . [SteweniusCFS] is also a related. The epipolar geometry is described by the following equation: [see mathematical formula in OpenCV docs] where formula is an essential matrix, formula and formula are corresponding points in the first and the second images, respectively. The result of this function may be passed further to `DecomposeEssentialMat` or `recoverPose` to recover the relative pose between cameras.
* `mask`: Output array of N elements, every element of which is set to 0 for outliers and to 1 for the other points. The array is computed only in the RANSAC and LMedS methods.

**Returns**: The returned value.

---
### `Cv2.FindEssentialMat`
**Signature**: `Mat? FindEssentialMat(Mat points1, Mat points2, double focal, IntPtr pp, int method, double prob, double threshold, int maxIters, Mat? mask)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `points1`: Array of N (N \>= 5) 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1 .
* `focal`: focal length of the camera. Note that this function assumes that points1 and points2 are feature points from cameras with same focal length and principal point.
* `pp`: principal point of the camera.
* `method`: Method for computing a fundamental matrix. -   `RANSAC` for the RANSAC algorithm. -   `LMEDS` for the LMedS algorithm.
* `prob`: Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of confidence (probability) that the estimated matrix is correct.
* `threshold`: Parameter used for RANSAC. It is the maximum distance from a point to an epipolar line in pixels, beyond which the point is considered an outlier and is not used for computing the final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the point localization, image resolution, and the image noise.
* `maxIters`: The maximum number of robust method iterations. This function differs from the one above that it computes camera intrinsic matrix from focal length and principal point: [see mathematical formula in OpenCV documentation]
* `mask`: Output array of N elements, every element of which is set to 0 for outliers and to 1 for the other points. The array is computed only in the RANSAC and LMedS methods.

**Returns**: The returned value.

---
### `Cv2.FindEssentialMat`
**Signature**: `Mat? FindEssentialMat(Mat points1, Mat points2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, int method, double prob, double threshold, Mat? mask)`

Calculates an essential matrix from the corresponding points in two images from potentially two different cameras.

**Parameters**:
* `points1`: Array of N (N \>= 5) 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1.
* `cameraMatrix1`: Camera matrix for the first camera formula .
* `distCoeffs1`: Input vector of distortion coefficients for the first camera formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `cameraMatrix2`: Camera matrix for the second camera formula .
* `distCoeffs2`: Input vector of distortion coefficients for the second camera formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `method`: Method for computing an essential matrix. -   `RANSAC` for the RANSAC algorithm. -   `LMEDS` for the LMedS algorithm.
* `prob`: Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of confidence (probability) that the estimated matrix is correct.
* `threshold`: Parameter used for RANSAC. It is the maximum distance from a point to an epipolar line in pixels, beyond which the point is considered an outlier and is not used for computing the final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the point localization, image resolution, and the image noise.
* `mask`: Output array of N elements, every element of which is set to 0 for outliers and to 1 for the other points. The array is computed only in the RANSAC and LMedS methods. This function estimates essential matrix based on the five-point algorithm solver in [Nister03] . [SteweniusCFS] is also a related. The epipolar geometry is described by the following equation: [see mathematical formula in OpenCV docs] where formula is an essential matrix, formula and formula are corresponding points in the first and the second images, respectively. The result of this function may be passed further to `DecomposeEssentialMat` or  `recoverPose` to recover the relative pose between cameras.

**Returns**: The returned value.

---
### `Cv2.FindEssentialMat`
**Signature**: `Mat? FindEssentialMat(Mat points1, Mat points2, Mat cameraMatrix1, Mat cameraMatrix2, Mat dist_coeff1, Mat dist_coeff2, Mat mask, UsacParams @params)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `points1`: The points1 parameter.
* `points2`: The points2 parameter.
* `cameraMatrix1`: The cameraMatrix1 parameter.
* `cameraMatrix2`: The cameraMatrix2 parameter.
* `dist_coeff1`: The dist_coeff1 parameter.
* `dist_coeff2`: The dist_coeff2 parameter.
* `mask`: Optional operation mask.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.DecomposeEssentialMat`
**Signature**: `void DecomposeEssentialMat(Mat E, Mat R1, Mat R2, Mat t)`

Decompose an essential matrix to possible rotations and translation.

**Parameters**:
* `E`: The input essential matrix.
* `R1`: One possible rotation matrix.
* `R2`: Another possible rotation matrix.
* `t`: One possible translation. This function decomposes the essential matrix E using svd decomposition [HartleyZ00]. In general, four possible poses exist for the decomposition of E. They are formula, formula, formula, formula. If E gives the epipolar constraint formula between the image points formula in the first image and formula in second image, then any of the tuples formula, formula, formula, formula is a change of basis from the first camera's coordinate system to the second camera's coordinate system. However, by decomposing E, one can only get the direction of the translation. For this reason, the translation t is returned with unit length.

---
### `Cv2.RecoverPose`
**Signature**: `int RecoverPose(Mat points1, Mat points2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Mat E, Mat R, Mat t, int method, double prob, double threshold, Mat? mask)`

Recovers the relative camera rotation and the translation from corresponding points in two images from two different cameras, using chirality check. Returns the number of inliers that pass the check.

**Parameters**:
* `points1`: Array of N 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1 .
* `cameraMatrix1`: Input/output camera matrix for the first camera, the same as in `CalibrateCamera`. Furthermore, for the stereo case, additional flags may be used, see below.
* `distCoeffs1`: Input/output vector of distortion coefficients, the same as in `CalibrateCamera`.
* `cameraMatrix2`: Input/output camera matrix for the first camera, the same as in `CalibrateCamera`. Furthermore, for the stereo case, additional flags may be used, see below.
* `distCoeffs2`: Input/output vector of distortion coefficients, the same as in `CalibrateCamera`.
* `E`: The output essential matrix.
* `R`: Output rotation matrix. Together with the translation vector, this matrix makes up a tuple that performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Note that, in general, t can not be used for this tuple, see the parameter described below.
* `t`: Output translation vector. This vector is obtained by `DecomposeEssentialMat` and therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit length.
* `method`: Method for computing an essential matrix. -   `RANSAC` for the RANSAC algorithm. -   `LMEDS` for the LMedS algorithm.
* `prob`: Parameter used for the RANSAC or LMedS methods only. It specifies a desirable level of confidence (probability) that the estimated matrix is correct.
* `threshold`: Parameter used for RANSAC. It is the maximum distance from a point to an epipolar line in pixels, beyond which the point is considered an outlier and is not used for computing the final fundamental matrix. It can be set to something like 1-3, depending on the accuracy of the point localization, image resolution, and the image noise.
* `mask`: Input/output mask for inliers in points1 and points2. If it is not empty, then it marks inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to recover pose. In the output mask only inliers which pass the chirality check. This function decomposes an essential matrix using `DecomposeEssentialMat` and then verifies possible pose hypotheses by doing chirality check. The chirality check means that the triangulated 3D points should have positive depth. Some details can be found in [Nister03]. This function can be used to process the output E and mask from `FindEssentialMat`. In this scenario, points1 and points2 are the same input for FindEssentialMat.

```csharp
// Example. Estimation of camera pose
using var points1 = new Mat();
using var points2 = new Mat();
using var cameraMatrix1 = new Mat();
using var distCoeffs1 = new Mat();
using var cameraMatrix2 = new Mat();
using var distCoeffs2 = new Mat();
using var E = new Mat();
using var R = new Mat();
using var t = new Mat();
using var mask = new Mat();
Cv2.RecoverPose(points1, points2, cameraMatrix1, distCoeffs1, cameraMatrix2, distCoeffs2, E, R, t, mask);
```

**Returns**: The returned value.

---
### `Cv2.RecoverPose`
**Signature**: `int RecoverPose(Mat E, Mat points1, Mat points2, Mat cameraMatrix, Mat R, Mat t, Mat? mask)`

Recovers the relative camera rotation and the translation from an estimated essential matrix and the corresponding points in two images, using chirality check. Returns the number of inliers that pass the check.

**Parameters**:
* `E`: The input essential matrix.
* `points1`: Array of N 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1 .
* `cameraMatrix`: Camera intrinsic matrix formula . Note that this function assumes that points1 and points2 are feature points from cameras with the same camera intrinsic matrix.
* `R`: Output rotation matrix. Together with the translation vector, this matrix makes up a tuple that performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Note that, in general, t can not be used for this tuple, see the parameter described below.
* `t`: Output translation vector. This vector is obtained by `DecomposeEssentialMat` and therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit length.
* `mask`: Input/output mask for inliers in points1 and points2. If it is not empty, then it marks inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to recover pose. In the output mask only inliers which pass the chirality check. This function decomposes an essential matrix using `DecomposeEssentialMat` and then verifies possible pose hypotheses by doing chirality check. The chirality check means that the triangulated 3D points should have positive depth. Some details can be found in [Nister03]. This function can be used to process the output E and mask from `FindEssentialMat`. In this scenario, points1 and points2 are the same input for `FindEssentialMat` 

```csharp
// Example. Estimation of camera pose using essential matrix
using var points1 = new Mat();
using var points2 = new Mat();
using var cameraMatrix = Cv2.Eye(3, 3, 6); // CV_64FC1 = 6
using var R = new Mat();
using var t = new Mat();
using var mask = new Mat();
using var E = Cv2.FindEssentialMat(points1, points2, cameraMatrix, (int)EssentialMatMethod.Ransac, 0.999, 1.0, mask);
Cv2.RecoverPose(E, points1, points2, cameraMatrix, R, t, mask);
```

**Returns**: The returned value.

---
### `Cv2.RecoverPose`
**Signature**: `int RecoverPose(Mat E, Mat points1, Mat points2, Mat R, Mat t, double focal, IntPtr pp, Mat? mask)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `E`: The input essential matrix.
* `points1`: Array of N 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1 .
* `R`: Output rotation matrix. Together with the translation vector, this matrix makes up a tuple that performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Note that, in general, t can not be used for this tuple, see the parameter description below.
* `t`: Output translation vector. This vector is obtained by `DecomposeEssentialMat` and therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit length.
* `focal`: Focal length of the camera. Note that this function assumes that points1 and points2 are feature points from cameras with same focal length and principal point.
* `pp`: principal point of the camera.
* `mask`: Input/output mask for inliers in points1 and points2. If it is not empty, then it marks inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to recover pose. In the output mask only inliers which pass the chirality check. This function differs from the one above that it computes camera intrinsic matrix from focal length and principal point: [see mathematical formula in OpenCV documentation]

**Returns**: The returned value.

---
### `Cv2.RecoverPose`
**Signature**: `int RecoverPose(Mat E, Mat points1, Mat points2, Mat cameraMatrix, Mat R, Mat t, double distanceThresh, Mat? mask, Mat? triangulatedPoints)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `E`: The input essential matrix.
* `points1`: Array of N 2D points from the first image. The point coordinates should be floating-point (single or double precision).
* `points2`: Array of the second image points of the same size and format as points1.
* `cameraMatrix`: Camera intrinsic matrix formula . Note that this function assumes that points1 and points2 are feature points from cameras with the same camera intrinsic matrix.
* `R`: Output rotation matrix. Together with the translation vector, this matrix makes up a tuple that performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Note that, in general, t can not be used for this tuple, see the parameter description below.
* `t`: Output translation vector. This vector is obtained by `DecomposeEssentialMat` and therefore is only known up to scale, i.e. t is the direction of the translation vector and has unit length.
* `distanceThresh`: threshold distance which is used to filter out far away points (i.e. infinite points).
* `mask`: Input/output mask for inliers in points1 and points2. If it is not empty, then it marks inliers in points1 and points2 for the given essential matrix E. Only these inliers will be used to recover pose. In the output mask only inliers which pass the chirality check.
* `triangulatedPoints`: 3D points which were reconstructed by triangulation. This function differs from the one above that it outputs the triangulated 3D point that are used for the chirality check.

**Returns**: The returned value.

---
### `Cv2.ComputeCorrespondEpilines`
**Signature**: `void ComputeCorrespondEpilines(Mat points, int whichImage, Mat F, Mat lines)`

For points in an image of a stereo pair, computes the corresponding epilines in the other image.

**Parameters**:
* `points`: Input points. formula or formula matrix of type CV_32FC2 or Point2f[] .
* `whichImage`: Index of the image (1 or 2) that contains the points .
* `F`: Fundamental matrix that can be estimated using `findFundamentalMat` or `stereoRectify` .
* `lines`: Output vector of the epipolar lines corresponding to the points in the other image. Each line formula is encoded by 3 numbers formula . For every point in one of the two images of a stereo pair, the function finds the equation of the corresponding epipolar line in the other image. From the fundamental matrix definition (see `findFundamentalMat` ), line formula in the second image for the point formula in the first image (when whichImage=1 ) is computed as: [see mathematical formula in OpenCV docs] And vice versa, when whichImage=2, formula is computed from formula as: [see mathematical formula in OpenCV docs] Line coefficients are defined up to a scale. They are normalized so that formula .

---
### `Cv2.TriangulatePoints`
**Signature**: `void TriangulatePoints(Mat projMatr1, Mat projMatr2, Mat projPoints1, Mat projPoints2, Mat points4D)`

This function reconstructs 3-dimensional points (in homogeneous coordinates) by using their observations with a stereo camera.

**Detailed Remarks**:
.: info Note

Keep in mind that all input data should be of float type in order for this function to work.
.:
.: info Note

If the projection matrices from `stereoRectify` are used, then the returned points are
represented in the first camera's rectified coordinate system.
**See also**: 
reprojectImageTo3D
.:

**Parameters**:
* `projMatr1`: 3x4 projection matrix of the first camera, i.e. this matrix projects 3D points given in the world's coordinate system into the first image.
* `projMatr2`: 3x4 projection matrix of the second camera, i.e. this matrix projects 3D points given in the world's coordinate system into the second image.
* `projPoints1`: 2xN array of feature points in the first image. It can be also an array of feature points or two-channel matrix of size 1xN or Nx1.
* `projPoints2`: 2xN array of corresponding points in the second image. It can be also an array of feature points or two-channel matrix of size 1xN or Nx1.
* `points4D`: 4xN array of reconstructed points in homogeneous coordinates. These points are returned in the world's coordinate system.

---
### `Cv2.CorrectMatches`
**Signature**: `void CorrectMatches(Mat F, Mat points1, Mat points2, Mat newPoints1, Mat newPoints2)`

Refines coordinates of corresponding points.

**Parameters**:
* `F`: 3x3 fundamental matrix.
* `points1`: 1xN array containing the first set of points.
* `points2`: 1xN array containing the second set of points.
* `newPoints1`: The optimized points1.
* `newPoints2`: The optimized points2. The function implements the Optimal Triangulation Method (see Multiple View Geometry [HartleyZ00] for details). For each given point correspondence points1[i] \<-\> points2[i], and a fundamental matrix F, it computes the corrected correspondences newPoints1[i] \<-\> newPoints2[i] that minimize the geometric error formula (where formula is the geometric distance between points formula and formula ) subject to the epipolar constraint formula .

---
### `Cv2.SampsonDistance`
**Signature**: `double SampsonDistance(Mat pt1, Mat pt2, Mat F)`

Calculates the Sampson Distance between two points.

**Detailed Remarks**:
The function sampsonDistance calculates and returns the first order approximation of the geometric error as:

$$
sd( \texttt{pt1} , \texttt{pt2} )=
\frac{(\texttt{pt2}^t \cdot \texttt{F} \cdot \texttt{pt1})^2}
{((\texttt{F} \cdot \texttt{pt1})(0))^2 +
((\texttt{F} \cdot \texttt{pt1})(1))^2 +
((\texttt{F}^t \cdot \texttt{pt2})(0))^2 +
((\texttt{F}^t \cdot \texttt{pt2})(1))^2}
$$

The fundamental matrix may be calculated using the `findFundamentalMat` function. See **Citation**:  HartleyZ00 11.4.3 for details.

**Parameters**:
* `pt1`: first homogeneous 2d point
* `pt2`: second homogeneous 2d point
* `F`: fundamental matrix

**Returns**: The computed Sampson distance.

---
### `Cv2.EstimateAffine3D`
**Signature**: `bool EstimateAffine3D(Mat src, Mat dst, Mat @out, Mat inliers, double ransacThreshold, double confidence)`

Computes an optimal affine transformation between two 3D point sets.

**Detailed Remarks**:
It computes

$$
\begin{bmatrix}
x\\
y\\
z\\
\end{bmatrix}
=
\begin{bmatrix}
a_{11} & a_{12} & a_{13}\\
a_{21} & a_{22} & a_{23}\\
a_{31} & a_{32} & a_{33}\\
\end{bmatrix}
\begin{bmatrix}
X\\
Y\\
Z\\
\end{bmatrix}
+
\begin{bmatrix}
b_1\\
b_2\\
b_3\\
\end{bmatrix}
$$

**Parameters**:
* `src`: First input 3D point set containing formula.
* `dst`: Second input 3D point set containing formula.
* `out`: Output 3D affine transformation matrix formula of the form [see mathematical formula in OpenCV documentation]
* `inliers`: Output vector indicating which points are inliers (1-inlier, 0-outlier).
* `ransacThreshold`: Maximum reprojection error in the RANSAC algorithm to consider a point as an inlier.
* `confidence`: Confidence level, between 0 and 1, for the estimated transformation. Anything between 0.95 and 0.99 is usually good enough. Values too close to 1 can slow down the estimation significantly. Values lower than 0.8-0.9 can result in an incorrectly estimated transformation.

**Returns**: Whether a solution was found. The function estimates an optimal 3D affine transformation between two 3D point sets using the RANSAC algorithm.

---
### `Cv2.EstimateAffine3D`
**Signature**: `Mat? EstimateAffine3D(Mat src, Mat dst, IntPtr scale, bool force_rotation)`

Computes an optimal affine transformation between two 3D point sets.

**Detailed Remarks**:
It computes formula minimizing formula
where formula is a 3x3 rotation matrix, formula is a 3x1 translation vector and formula is a
scalar size value. This is an implementation of the algorithm by Umeyama [Umeyama1991least] .
The estimated affine transform has a homogeneous scale which is a subclass of affine
transformations with 7 degrees of freedom. The paired point sets need to comprise at least 3
points each.

**Parameters**:
* `src`: First input 3D point set.
* `dst`: Second input 3D point set.
* `scale`: If null is passed, the scale parameter c will be assumed to be 1.0. Else the pointed-to variable will be set to the optimal scale.
* `force_rotation`: If true, the returned rotation will never be a reflection. This might be unwanted, e.g. when optimizing a transform between a right- and a left-handed coordinate system.

**Returns**: 3D affine transformation matrix formula of the form [see mathematical formula in OpenCV documentation]

---
### `Cv2.EstimateTranslation3D`
**Signature**: `bool EstimateTranslation3D(Mat src, Mat dst, Mat @out, Mat inliers, double ransacThreshold, double confidence)`

Computes an optimal translation between two 3D point sets. * * It computes * [see mathematical formula in OpenCV documentation] * * **src** First input 3D point set containing formula. * **dst** Second input 3D point set containing formula. * **out** Output 3D translation vector formula of the form * [see mathematical formula in OpenCV documentation] * **inliers** Output vector indicating which points are inliers (1-inlier, 0-outlier). * **ransacThreshold** Maximum reprojection error in the RANSAC algorithm to consider a point as * an inlier. * **confidence** Confidence level, between 0 and 1, for the estimated transformation. Anything * between 0.95 and 0.99 is usually good enough. Values too close to 1 can slow down the estimation * significantly. Values lower than 0.8-0.9 can result in an incorrectly estimated transformation. * **Returns**: Whether a translation was found. * * The function estimates an optimal 3D translation between two 3D point sets using the * RANSAC algorithm. *

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `out`: The @out parameter.
* `inliers`: The inliers parameter.
* `ransacThreshold`: The ransacThreshold parameter.
* `confidence`: The confidence parameter.

**Returns**: The returned value.

---
### `Cv2.EstimateAffine2D`
**Signature**: `Mat? EstimateAffine2D(Mat from, Mat to, Mat? inliers, int method, double ransacReprojThreshold, long maxIters, double confidence, long refineIters)`

Computes an optimal affine transformation between two 2D point sets.

**Detailed Remarks**:
It computes

$$
\begin{bmatrix}
x\\
y\\
\end{bmatrix}
=
\begin{bmatrix}
a_{11} & a_{12}\\
a_{21} & a_{22}\\
\end{bmatrix}
\begin{bmatrix}
X\\
Y\\
\end{bmatrix}
+
\begin{bmatrix}
b_1\\
b_2\\
\end{bmatrix}
$$

.: info Note

The RANSAC method can handle practically any ratio of outliers but needs a threshold to
distinguish inliers from outliers. The method LMeDS does not need any threshold but it works
correctly only when there are more than 50% of inliers.
**See also**: estimateAffinePartial2D, getAffineTransform
.:

**Parameters**:
* `from`: First input 2D point set containing formula.
* `to`: Second input 2D point set containing formula.
* `inliers`: Output vector indicating which points are inliers (1-inlier, 0-outlier).
* `method`: Robust method used to compute transformation. The following methods are possible: -   `RANSAC` - RANSAC-based robust method -   `LMEDS` - Least-Median robust method RANSAC is the default method.
* `ransacReprojThreshold`: Maximum reprojection error in the RANSAC algorithm to consider a point as an inlier. Applies only to RANSAC.
* `maxIters`: The maximum number of robust method iterations.
* `confidence`: Confidence level, between 0 and 1, for the estimated transformation. Anything between 0.95 and 0.99 is usually good enough. Values too close to 1 can slow down the estimation significantly. Values lower than 0.8-0.9 can result in an incorrectly estimated transformation.
* `refineIters`: Maximum number of iterations of refining algorithm (Levenberg-Marquardt). Passing 0 will disable refining, so the output matrix will be output of robust method.

**Returns**: Output 2D affine transformation matrix formula or empty matrix if transformation could not be estimated. The returned matrix has the following form: [see mathematical formula in OpenCV documentation] The function estimates an optimal 2D affine transformation between two 2D point sets using the selected robust algorithm. The computed transformation is then refined further (using only inliers) with the Levenberg-Marquardt method to reduce the re-projection error even more.

---
### `Cv2.EstimateAffine2D`
**Signature**: `Mat? EstimateAffine2D(Mat pts1, Mat pts2, Mat inliers, UsacParams @params)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `pts1`: The pts1 parameter.
* `pts2`: The pts2 parameter.
* `inliers`: The inliers parameter.
* `params`: The @params parameter.

**Returns**: The returned value.

---
### `Cv2.EstimateAffinePartial2D`
**Signature**: `Mat? EstimateAffinePartial2D(Mat from, Mat to, Mat? inliers, int method, double ransacReprojThreshold, long maxIters, double confidence, long refineIters)`

Computes an optimal limited affine transformation with 4 degrees of freedom between two 2D point sets.

**Detailed Remarks**:
.: info Note

The RANSAC method can handle practically any ratio of outliers but need a threshold to
distinguish inliers from outliers. The method LMeDS does not need any threshold but it works
correctly only when there are more than 50% of inliers.
**See also**: estimateAffine2D, getAffineTransform
.:

**Parameters**:
* `from`: First input 2D point set.
* `to`: Second input 2D point set.
* `inliers`: Output vector indicating which points are inliers.
* `method`: Robust method used to compute transformation. The following methods are possible: -   `RANSAC` - RANSAC-based robust method -   `LMEDS` - Least-Median robust method RANSAC is the default method.
* `ransacReprojThreshold`: Maximum reprojection error in the RANSAC algorithm to consider a point as an inlier. Applies only to RANSAC.
* `maxIters`: The maximum number of robust method iterations.
* `confidence`: Confidence level, between 0 and 1, for the estimated transformation. Anything between 0.95 and 0.99 is usually good enough. Values too close to 1 can slow down the estimation significantly. Values lower than 0.8-0.9 can result in an incorrectly estimated transformation.
* `refineIters`: Maximum number of iterations of refining algorithm (Levenberg-Marquardt). Passing 0 will disable refining, so the output matrix will be output of robust method.

**Returns**: Output 2D affine transformation (4 degrees of freedom) matrix formula or empty matrix if transformation could not be estimated. The function estimates an optimal 2D affine transformation with 4 degrees of freedom limited to combinations of translation, rotation, and uniform scaling. Uses the selected algorithm for robust estimation. The computed transformation is then refined further (using only inliers) with the Levenberg-Marquardt method to reduce the re-projection error even more. Estimated transformation matrix is: [see mathematical formula in OpenCV documentation] Where formula is the rotation angle, formula the scaling factor and formula are translations in formula axes respectively.

---
### `Cv2.EstimateTranslation2D`
**Signature**: `IntPtr EstimateTranslation2D(Mat from, Mat to, Mat? inliers, int method, double ransacReprojThreshold, long maxIters, double confidence, long refineIters)`

Computes a pure 2D translation between two 2D point sets.

**Detailed Remarks**:
It computes

$$
\begin{bmatrix}
x\\
y
\end{bmatrix}
=
\begin{bmatrix}
1 & 0\\
0 & 1
\end{bmatrix}
\begin{bmatrix}
X\\
Y
\end{bmatrix}
+
\begin{bmatrix}
t_x\\
t_y
\end{bmatrix}.
$$

.: info Note

The RANSAC method can handle practically any ratio of outliers but needs a threshold to
distinguish inliers from outliers. The method LMeDS does not need any threshold but works
correctly only when there are more than 50% inliers.
**See also**: estimateAffine2D, estimateAffinePartial2D, getAffineTransform
.:

**Parameters**:
* `from`: First input 2D point set containing formula.
* `to`: Second input 2D point set containing formula.
* `inliers`: Output vector indicating which points are inliers (1-inlier, 0-outlier).
* `method`: Robust method used to compute the transformation. The following methods are possible: -   `RANSAC` - RANSAC-based robust method -   `LMEDS` - Least-Median robust method RANSAC is the default method.
* `ransacReprojThreshold`: Maximum reprojection error in the RANSAC algorithm to consider a point as an inlier. Applies only to RANSAC.
* `maxIters`: The maximum number of robust method iterations.
* `confidence`: Confidence level, between 0 and 1, for the estimated transformation. Anything between 0.95 and 0.99 is usually good enough. Values too close to 1 can slow down the estimation significantly. Values lower than 0.8–0.9 can result in an incorrectly estimated transformation.
* `refineIters`: Maximum number of iterations of the refining algorithm. For pure translation the least-squares solution on inliers is closed-form, so passing 0 is recommended (no additional refine).

**Returns**: A 2D translation vector formula as `Vec2d`. If the translation could not be estimated, both components are set to NaN and, if `inliers` is provided, the mask is filled with zeros. 
 Converting to a 2x3 transformation matrix:

```csharp
// Example estimation:
using var from = new Mat();
using var to = new Mat();
using var inliers = new Mat();
var t = Cv2.EstimateTranslation2D(from, to, inliers, 1, 3.0, 2000, 0.99, 0);
```
The function estimates a pure 2D translation between two 2D point sets using the selected robust algorithm. Inliers are determined by the reprojection error threshold.

---
### `Cv2.DecomposeHomographyMat`
**Signature**: `int DecomposeHomographyMat(Mat H, Mat K, IntPtr rotations, IntPtr translations, IntPtr normals)`

Decompose a homography matrix to rotation(s), translation(s) and plane normal(s).

**Parameters**:
* `H`: The input homography matrix between two images.
* `K`: The input camera intrinsic matrix.
* `rotations`: Array of rotation matrices.
* `translations`: Array of translation matrices.
* `normals`: Array of plane normal matrices. This function extracts relative camera motion between two views of a planar object and returns up to four mathematical solution tuples of rotation, translation, and plane normal. The decomposition of the homography matrix H is described in detail in [Malis2007]. If the homography H, induced by the plane, gives the constraint [see mathematical formula in OpenCV docs] on the source image points formula and the destination image points formula, then the tuple of rotations[k] and translations[k] is a change of basis from the source camera's coordinate system to the destination camera's coordinate system. However, by decomposing H, one can only get the translation normalized by the (typically unknown) depth of the scene, i.e. its direction but with normalized length. If point correspondences are available, at least two solutions may further be invalidated, by applying positive depth constraint, i.e. all points must be in front of the camera.

**Returns**: The returned value.

---
### `Cv2.FilterHomographyDecompByVisibleRefpoints`
**Signature**: `void FilterHomographyDecompByVisibleRefpoints(IntPtr rotations, IntPtr normals, Mat beforePoints, Mat afterPoints, Mat possibleSolutions, Mat? pointsMask)`

Filters homography decompositions based on additional information.

**Parameters**:
* `rotations`: Vector of rotation matrices.
* `normals`: Vector of plane normal matrices.
* `beforePoints`: Vector of (rectified) visible reference points before the homography is applied
* `afterPoints`: Vector of (rectified) visible reference points after the homography is applied
* `possibleSolutions`: Vector of int indices representing the viable solution set after filtering
* `pointsMask`: optional Mat/Vector of CV_8U, CV_8S or CV_Bool type representing the mask for the inliers as given by the `findHomography` function This function is intended to filter the output of the `decomposeHomographyMat` based on additional information as described in [Malis2007] . The summary of the method: the `decomposeHomographyMat` function returns 2 unique solutions and their "opposites" for a total of 4 solutions. If we have access to the sets of points visible in the camera frame before and after the homography transformation is applied, we can determine which are the true potential solutions and which are the opposites by verifying which homographies are consistent with all visible reference points being in front of the camera. The inputs are left unchanged; the filtered solution set is returned as indices into the existing one.

---
### `Cv2.CalibrationMatrixValues`
**Signature**: `void CalibrationMatrixValues(Mat cameraMatrix, Size imageSize, double apertureWidth, double apertureHeight, double fovx, double fovy, double focalLength, IntPtr principalPoint, double aspectRatio)`

Computes useful camera characteristics from the camera intrinsic matrix. * * **cameraMatrix** Input camera intrinsic matrix that can be estimated by `CalibrateCamera` or * `stereoCalibrate` . * **imageSize** Input image size in pixels. * **apertureWidth** Physical width in mm of the sensor. * **apertureHeight** Physical height in mm of the sensor. * **fovx** Output field of view in degrees along the horizontal sensor axis. * **fovy** Output field of view in degrees along the vertical sensor axis. * **focalLength** Focal length of the lens in mm. * **principalPoint** Principal point in mm. * **aspectRatio** formula * * The function computes various useful camera characteristics from the previously estimated camera * matrix. * * **Note:** *   Do keep in mind that the unity measure 'mm' stands for whatever unit of measure one chooses for *    the chessboard pitch (it can thus be any value).

**Parameters**:
* `cameraMatrix`: The cameraMatrix parameter.
* `imageSize`: The imageSize parameter.
* `apertureWidth`: The apertureWidth parameter.
* `apertureHeight`: The apertureHeight parameter.
* `fovx`: The fovx parameter.
* `fovy`: The fovy parameter.
* `focalLength`: The focalLength parameter.
* `principalPoint`: The principalPoint parameter.
* `aspectRatio`: The aspectRatio parameter.

---
### `Cv2.GetDefaultNewCameraMatrix`
**Signature**: `Mat? GetDefaultNewCameraMatrix(Mat cameraMatrix, Size imgsize, bool centerPrincipalPoint)`

Returns the default new camera matrix.

**Detailed Remarks**:
The function returns the camera matrix that is either an exact copy of the input cameraMatrix (when
centerPrinicipalPoint=false ), or the modified one (when centerPrincipalPoint=true).
In the latter case, the new camera matrix will be:
[see mathematical formula in OpenCV docs]
where formula and formula are formula and formula elements of cameraMatrix, respectively.
By default, the undistortion functions in OpenCV (see `initUndistortRectifyMap`, `undistort`) do not
move the principal point. However, when you work with stereo, it is important to move the principal
points in both views to the same y-coordinate (which is required by most of stereo correspondence
algorithms), and may be to the same x-coordinate too. So, you can form the new camera matrix for
each view where the principal points are located at the center.

**Parameters**:
* `cameraMatrix`: Input camera matrix.
* `imgsize`: Camera view image size in pixels.
* `centerPrincipalPoint`: Location of the principal point in the new camera matrix. The parameter indicates whether this location should be at the image center or not.

**Returns**: The returned value.

---
### `Cv2.GetOptimalNewCameraMatrix`
**Signature**: `Mat? GetOptimalNewCameraMatrix(Mat cameraMatrix, Mat distCoeffs, Size imageSize, double alpha, Size newImgSize, IntPtr validPixROI, bool centerPrincipalPoint)`

Returns the new camera intrinsic matrix based on the free scaling parameter.

**Parameters**:
* `cameraMatrix`: Input camera intrinsic matrix.
* `distCoeffs`: Input vector of distortion coefficients formula. If the vector is null/empty, the zero distortion coefficients are assumed.
* `imageSize`: Original image size.
* `alpha`: Free scaling parameter between 0 (when all the pixels in the undistorted image are valid) and 1 (when all the source image pixels are retained in the undistorted image). See `stereoRectify` for details.
* `newImgSize`: Image size after rectification. By default, it is set to imageSize .
* `validPixROI`: Optional output rectangle that outlines all-good-pixels region in the undistorted image. See roi1, roi2 description in `stereoRectify` .
* `centerPrincipalPoint`: Optional flag that indicates whether in the new camera intrinsic matrix the principal point should be at the image center or not. By default, the principal point is chosen to best fit a subset of the source image (determined by alpha) to the corrected image.

**Returns**: new_camera_matrix Output new camera intrinsic matrix. The function computes and returns the optimal new camera intrinsic matrix based on the free scaling parameter. By varying this parameter, you may retrieve only sensible pixels alpha=0 , keep all the original image pixels if there is valuable information in the corners alpha=1 , or get something in between. When alpha\>0 , the undistorted result is likely to have some black pixels corresponding to "virtual" pixels outside of the captured distorted image. The original camera intrinsic matrix, distortion coefficients, the computed new camera intrinsic matrix, and newImageSize should be passed to `initUndistortRectifyMap` to produce the maps for `remap` .

---
### `Cv2.UndistortPoints`
**Signature**: `void UndistortPoints(Mat src, Mat dst, Mat cameraMatrix, Mat distCoeffs, Mat? R, Mat? P, TermCriteria criteria)`

Computes the ideal point coordinates from the observed point coordinates.

**Detailed Remarks**:
The function is similar to `undistort` and `initUndistortRectifyMap` but it operates on a
sparse set of points instead of a raster image. Also the function performs a reverse transformation
to  `projectPoints`. In case of a 3D object, it does not reconstruct its 3D coordinates, but for a
planar object, it does, up to a translation vector, if the proper R is specified.
For each observed point coordinate formula the function computes:

$$
\begin{array}{l}
x^{"}  \leftarrow (u - c_x)/f_x  \\
y^{"}  \leftarrow (v - c_y)/f_y  \\
(x',y') = undistort(x^{"},y^{"}, \texttt{distCoeffs}) \\
{[X\,Y\,W]} ^T  \leftarrow R\cdot[x' \, y' \, 1]^T  \\
x  \leftarrow X/W  \\
y  \leftarrow Y/W  \\
\text{only performed if P is specified:} \\
u'  \leftarrow x {f'}_x + {c'}_x  \\
v'  \leftarrow y {f'}_y + {c'}_y
\end{array}
$$

where *undistort* is an approximate iterative algorithm that estimates the normalized original
point coordinates out of the normalized distorted point coordinates ("normalized" means that the
coordinates do not depend on the camera matrix).
The function can be used for both a stereo camera head or a monocular camera (when R is empty).
.: info Note
**Coordinate Systems:**
- **Input (`src`)**: Points are expected in **pixel coordinates** of the distorted image, i.e.,
coordinates formula measured in pixels from the top-left corner of the image.
- **Output (`dst`)**: The coordinate system of output points depends on parameter `P`:
- If `P` is provided (not empty): Output points are in **pixel coordinates** of the rectified/undistorted image plane, using the camera matrix `P`.
- If `P` is empty or identity: Output points are in **normalized camera coordinates** (also called "normalized image coordinates"),
which are dimensionless coordinates formula in the camera's focal plane, related to pixel coordinates by:
formula and formula. These normalized coordinates are independent of the camera's intrinsic parameters and are useful for 3D reconstruction or epipolar geometry.
.:

**Parameters**:
* `src`: Observed point coordinates in **pixel coordinates** of the distorted image, 2xN/Nx2 1-channel or 1xN/Nx1 2-channel (CV_32FC2 or CV_64FC2) (or Point2f[] ).
* `dst`: Output ideal point coordinates (1xN/Nx1 2-channel or Point2f[] ) after undistortion and reverse perspective transformation. If matrix P is identity or omitted, dst will contain normalized point coordinates.
* `cameraMatrix`: Camera matrix formula .
* `distCoeffs`: Input vector of distortion coefficients formula of 4, 5, 8, 12 or 14 elements. If the vector is null/empty, the zero distortion coefficients are assumed.
* `R`: Rectification transformation in the object space (3x3 matrix). R1 or R2 computed by `stereoRectify` can be passed here. If the matrix is empty, the identity transformation is used.
* `P`: New camera matrix (3x3) or new projection matrix (3x4) formula. P1 or P2 computed by `stereoRectify` can be passed here. If the matrix is empty, the identity new camera matrix is used and output will be in normalized coordinates.
* `criteria`: termination criteria for the iterative point undistortion algorithm

---
### `Cv2.UndistortImagePoints`
**Signature**: `void UndistortImagePoints(Mat src, Mat dst, Mat cameraMatrix, Mat distCoeffs, TermCriteria arg1)`

*  Compute undistorted image points position

**Detailed Remarks**:
* * **Parameter** `src`:  Observed points position, 2xN/Nx2 1-channel or 1xN/Nx1 2-channel (CV_32FC2 or CV_64FC2) (or Point2f[] ).
* * **Parameter** `dst`:  Output undistorted points position (1xN/Nx1 2-channel or Point2f[] ).
* * **Parameter** `cameraMatrix`:  Camera matrix formula .
* * **Parameter** `distCoeffs`:  Distortion coefficients

**Parameters**:
* `src`: Source matrix or image.
* `dst`: Destination matrix or image (output).
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `arg1`: The arg1 parameter.

---
### `Cv2.FisheyeProjectPoints`
**Signature**: `void FisheyeProjectPoints(Mat objectPoints, Mat imagePoints, Mat rvec, Mat tvec, Mat K, Mat D, double alpha, Mat? jacobian)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `rvec`: The rvec parameter.
* `tvec`: The tvec parameter.
* `K`: The K parameter.
* `D`: The D parameter.
* `alpha`: The alpha parameter.
* `jacobian`: The jacobian parameter.

---
### `Cv2.FisheyeDistortPoints`
**Signature**: `void FisheyeDistortPoints(Mat undistorted, Mat distorted, Mat K, Mat D, double alpha)`

Distorts 2D points using fisheye model.

**Parameters**:
* `undistorted`: Array of object points, 1xN/Nx1 2-channel (or Point2f[] ), where N is the number of points in the view.
* `distorted`: Output array of image points, 1xN/Nx1 2-channel, or Point2f[] . Note that the function assumes the camera intrinsic matrix of the undistorted points to be identity. This means if you want to distort image points you have to multiply them with formula or use another function overload.
* `K`: Camera intrinsic matrix formula.
* `D`: Input vector of distortion coefficients formula.
* `alpha`: The skew coefficient.

---
### `Cv2.FisheyeDistortPoints`
**Signature**: `void FisheyeDistortPoints(Mat undistorted, Mat distorted, Mat Kundistorted, Mat K, Mat D, double alpha)`

This is an overloaded member function, provided for convenience.

**Detailed Remarks**:
Overload of distortPoints function to handle cases when undistorted points are got with non-identity
camera matrix, e.g. output of `estimateNewCameraMatrixForUndistortRectify`.
**See also**: estimateNewCameraMatrixForUndistortRectify

**Parameters**:
* `undistorted`: Array of object points, 1xN/Nx1 2-channel (or Point2f[] ), where N is the number of points in the view.
* `distorted`: Output array of image points, 1xN/Nx1 2-channel, or Point2f[] .
* `Kundistorted`: Camera intrinsic matrix used as new camera matrix for undistortion.
* `K`: Camera intrinsic matrix formula.
* `D`: Input vector of distortion coefficients formula.
* `alpha`: The skew coefficient.

---
### `Cv2.FisheyeUndistortPoints`
**Signature**: `void FisheyeUndistortPoints(Mat distorted, Mat undistorted, Mat K, Mat D, Mat? R, Mat? P, TermCriteria criteria)`

Undistorts 2D points using fisheye camera model

**Detailed Remarks**:
This function performs undistortion for fisheye camera models, which use a different distortion model
compared to the standard pinhole camera model used by `undistortPoints`. The fisheye model is suitable
for wide-angle cameras.
The function transforms points from the distorted fisheye image to undistorted coordinates, optionally
applying a rectification transformation (R) and projecting to a new image plane (P).
.: info Note
**Coordinate Systems:**
- **Input (`distorted`)**: Points are expected in **pixel coordinates** of the distorted fisheye image,
i.e., coordinates measured in pixels from the top-left corner of the image.
- **Output (`undistorted`)**: The coordinate system depends on parameter `P`:
- If `P` is provided (not empty): Output points are in **pixel coordinates** of the rectified/undistorted
image plane, using the camera matrix `P`.
- If `P` is empty or identity: Output points are in **normalized camera coordinates** (normalized image coordinates),
which are dimensionless coordinates in the camera's focal plane, independent of intrinsic parameters.
.:
.: info Note
**Fisheye vs. Standard Model:**
Use this function (Cv2.fisheye.undistortPoints) for fisheye cameras (wide-angle lenses).
For standard pinhole cameras, use `undistortPoints` instead. The fisheye model uses a different distortion
parameterization (4 coefficients) compared to the standard model (4-14 coefficients).
.:

**Parameters**:
* `distorted`: Array of distorted point coordinates in **pixel coordinates** of the fisheye image, 1xN/Nx1 2-channel (or Point2f[] ), where N is the number of points in the view.
* `undistorted`: Output array of undistorted image points, 1xN/Nx1 2-channel, or Point2f[] . The coordinate system depends on parameter P (see above).
* `K`: Camera intrinsic matrix formula of the fisheye camera.
* `D`: Input vector of fisheye distortion coefficients formula (must contain exactly 4 coefficients).
* `R`: Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3 1-channel or 1x1 3-channel. If empty, the identity transformation is used.
* `P`: New camera intrinsic matrix (3x3) or new projection matrix (3x4). If empty or identity, output will be in normalized camera coordinates.
* `criteria`: Termination criteria for the iterative undistortion algorithm.

---
### `Cv2.FisheyeEstimateNewCameraMatrixForUndistortRectify`
**Signature**: `void FisheyeEstimateNewCameraMatrixForUndistortRectify(Mat K, Mat D, Size image_size, Mat R, Mat P, double balance, Size new_size, double fov_scale)`

Estimates new camera intrinsic matrix for undistortion or rectification.

**Parameters**:
* `K`: Camera intrinsic matrix formula.
* `D`: Input vector of distortion coefficients formula.
* `image_size`: Size of the image
* `R`: Rectification transformation in the object space: 3x3 1-channel, or vector: 3x1/1x3 1-channel or 1x1 3-channel
* `P`: New camera intrinsic matrix (3x3) or new projection matrix (3x4)
* `balance`: Sets the new focal length in range between the min focal length and the max focal length. Balance is in range of [0, 1].
* `new_size`: the new size
* `fov_scale`: Divisor for new focal length.

---
### `Cv2.FisheyeSolvePnP`
**Signature**: `bool FisheyeSolvePnP(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int flags, TermCriteria criteria)`

Finds an object pose from 3D-2D point correspondences for fisheye camera model.

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can also be passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can also be passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients (4x1/1x4).
* `rvec`: Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system.
* `tvec`: Output translation vector.
* `useExtrinsicGuess`: Parameter used for `SOLVEPNP_ITERATIVE`. If true (1), the function uses the provided rvec and tvec values as initial approximations of the rotation and translation vectors, respectively, and further optimizes them.
* `flags`: Method for solving a PnP problem: see `calib3d_SolvePnP_flags` This function returns the rotation and the translation vectors that transform a 3D point expressed in the object coordinate frame to the camera coordinate frame, using different methods: - P3P methods (`SOLVEPNP_P3P`, `SOLVEPNP_AP3P`): need 4 input points to return a unique solution. - `SOLVEPNP_IPPE` Input points must be >= 4 and object points must be coplanar. - `SOLVEPNP_IPPE_SQUARE` Special case suitable for marker pose estimation. Number of input points must be 4. Object points must be defined in the following order: - point 0: [-squareLength / 2,  squareLength / 2, 0] - point 1: [ squareLength / 2,  squareLength / 2, 0] - point 2: [ squareLength / 2, -squareLength / 2, 0] - point 3: [-squareLength / 2, -squareLength / 2, 0] - for all the other flags, number of input points must be >= 4 and object points can be in any configuration.
* `criteria`: Termination criteria for internal undistortPoints call. The function internally undistorts points with `undistortPoints` and call `SolvePnP`, thus the input are very similar. Check there and Perspective-n-Points is described in `calib3d_SolvePnP` for more information.

**Returns**: The returned value.

---
### `Cv2.FisheyeSolvePnPRansac`
**Signature**: `bool FisheyeSolvePnPRansac(Mat objectPoints, Mat imagePoints, Mat cameraMatrix, Mat distCoeffs, Mat rvec, Mat tvec, bool useExtrinsicGuess, int iterationsCount, float reprojectionError, double confidence, Mat? inliers, int flags, TermCriteria criteria)`

Finds an object pose from 3D-2D point correspondences using the RANSAC scheme for fisheye camera moodel.

**Parameters**:
* `objectPoints`: Array of object points in the object coordinate space, Nx3 1-channel or 1xN/Nx1 3-channel, where N is the number of points. Point3D[] can be also passed here.
* `imagePoints`: Array of corresponding image points, Nx2 1-channel or 1xN/Nx1 2-channel, where N is the number of points. Point2D[] can be also passed here.
* `cameraMatrix`: Input camera intrinsic matrix formula .
* `distCoeffs`: Input vector of distortion coefficients (4x1/1x4).
* `rvec`: Output rotation vector (see `Rodrigues` ) that, together with tvec, brings points from the model coordinate system to the camera coordinate system.
* `tvec`: Output translation vector.
* `useExtrinsicGuess`: Parameter used for `SOLVEPNP_ITERATIVE`. If true (1), the function uses the provided rvec and tvec values as initial approximations of the rotation and translation vectors, respectively, and further optimizes them.
* `iterationsCount`: Number of iterations.
* `reprojectionError`: Inlier threshold value used by the RANSAC procedure. The parameter value is the maximum allowed distance between the observed and computed point projections to consider it an inlier.
* `confidence`: The probability that the algorithm produces a useful result.
* `inliers`: Output vector that contains indices of inliers in objectPoints and imagePoints .
* `flags`: Method for solving a PnP problem: see `calib3d_SolvePnP_flags`
* `criteria`: Termination criteria for internal undistortPoints call. The function interally undistorts points with `undistortPoints` and call `SolvePnP`, thus the input are very similar. More information about Perspective-n-Points is described in `calib3d_SolvePnP` for more information.

**Returns**: The returned value.

---
### `Cv2.BuildMST`
**Signature**: `bool BuildMST(int numNodes, IntPtr inputEdges, IntPtr resultingEdges, MSTAlgorithm algorithm, int root)`

*  Builds a Minimum Spanning Tree (MST) using the specified algorithm (see `MSTAlgorithm`).

**Detailed Remarks**:
* Supports graphs with negative edge weights. Self-loop edges (edges where source and target are the
* same) are ignored. If multiple edges exist between the same pair of nodes, only the one with the
* lowest weight is considered. If the graph is disconnected or input is invalid, the function
* returns false.

.: info Note
*  The `root` parameter is ignored for algorithms that do not require a starting node.
.:
.: info Note
*  Additional MST algorithms may be supported in the future via the `algorithm` parameter
* (see `MSTAlgorithm`).

* * **Parameter** `numNodes`:  Number of nodes in the graph (must be greater than 0).
* * **Parameter** `inputEdges`:  Input vector of edges representing the graph.
* * **Parameter** `resultingEdges`:  Output vector to store the edges of the resulting MST.
* * **Parameter** `algorithm`:  Specifies which algorithm to use to compute the MST (see `MSTAlgorithm`).
* * **Parameter** `root`:  Starting node for the MST algorithm (only used for certain algorithms).
**Returns**: true if a valid MST was successfully built; false otherwise.
* **Throws**: Error (StsBadArg) if an invalid algorithm is specified.
.:

**Parameters**:
* `numNodes`: The numNodes parameter.
* `inputEdges`: The inputEdges parameter.
* `resultingEdges`: The resultingEdges parameter.
* `algorithm`: The algorithm parameter.
* `root`: The root parameter.

**Returns**: The returned value.

---
## 🔢 Enumerations

### `DistanceTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`User`** | `-1` | User |
| **`L1`** | `1` | L1 |
| **`L2`** | `2` | L2 |
| **`C`** | `3` | C |
| **`L12`** | `4` | L12 |
| **`Fair`** | `5` | Fair |
| **`Welsch`** | `6` | Welsch |
| **`Huber`** | `7` | Huber |

---
### `LocalOptimMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Null`** | `0` | Null |
| **`InnerLo`** | `1` | InnerLo |
| **`InnerAndIterLo`** | `2` | InnerAndIterLo |
| **`Gc`** | `3` | Gc |
| **`Sigma`** | `4` | Sigma |

---
### `MSTAlgorithm`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Prim`** | `0` | Prim |
| **`Kruskal`** | `1` | Kruskal |

---
### `MatrixType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Auto`** | `0` | Auto |
| **`Dense`** | `1` | Dense |
| **`Sparse`** | `2` | Sparse |

---
### `NeighborSearchMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FlannKnn`** | `0` | FlannKnn |
| **`Grid`** | `1` | Grid |
| **`FlannRadius`** | `2` | FlannRadius |

---
### `PolishingMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`NonePolisher`** | `0` | NonePolisher |
| **`LsqPolisher`** | `1` | LsqPolisher |
| **`Magsac`** | `2` | Magsac |
| **`CovPolisher`** | `3` | CovPolisher |

---
### `RectanglesIntersectTypes`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`None`** | `0` | None |
| **`Partial`** | `1` | Partial |
| **`Full`** | `2` | Full |

---
### `SacMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`SacMethodRansac`** | `0` | SacMethodRansac |

---
### `SacModelType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Plane`** | `0` | Plane |
| **`Sphere`** | `1` | Sphere |

---
### `SamplingMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Uniform`** | `0` | Uniform |
| **`ProgressiveNapsac`** | `1` | ProgressiveNapsac |
| **`Napsac`** | `2` | Napsac |
| **`Prosac`** | `3` | Prosac |

---
### `ScoreMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Ransac`** | `0` | Ransac |
| **`Msac`** | `1` | Msac |
| **`Magsac`** | `2` | Magsac |
| **`Lmeds`** | `3` | Lmeds |

---
### `SolvePnPMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Iterative`** | `0` | Iterative |
| **`Epnp`** | `1` | Epnp |
| **`P3p`** | `2` | P3p |
| **`Ap3p`** | `3` | Ap3p |
| **`Ippe`** | `4` | Ippe |
| **`IppeSquare`** | `5` | IppeSquare |
| **`Sqpnp`** | `6` | Sqpnp |
| **`MaxCount`** | `unchecked((int)(6 + 1))` | MaxCount |

---
### `UnnamedEnum11Subdiv2D`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`NextAroundOrg`** | `unchecked((int)(0x00))` | NextAroundOrg |
| **`NextAroundDst`** | `unchecked((int)(0x22))` | NextAroundDst |
| **`PrevAroundOrg`** | `unchecked((int)(0x11))` | PrevAroundOrg |
| **`PrevAroundDst`** | `unchecked((int)(0x33))` | PrevAroundDst |
| **`NextAroundLeft`** | `unchecked((int)(0x13))` | NextAroundLeft |
| **`NextAroundRight`** | `unchecked((int)(0x31))` | NextAroundRight |
| **`PrevAroundLeft`** | `unchecked((int)(0x20))` | PrevAroundLeft |
| **`PrevAroundRight`** | `unchecked((int)(0x02))` | PrevAroundRight |

---
### `VariableType`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Linear`** | `0` | Linear |
| **`So3`** | `1` | So3 |
| **`Se3`** | `2` | Se3 |

---

</div>