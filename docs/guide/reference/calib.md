# CALIB Module API Reference

Complete documentation for the **CALIB** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Calib Documentation](https://docs.opencv.org/5.x/main_modules/calib.html).

---
<div v-pre>

## ⚙️ Static Methods (Cv2)

### `Cv2.InitCameraMatrix2D`
**Signature**: `Mat? InitCameraMatrix2D(IntPtr objectPoints, IntPtr imagePoints, Size imageSize, double aspectRatio)`

Finds an initial camera intrinsic matrix from 3D-2D point correspondences.

**Parameters**:
* `objectPoints`: Vector of vectors of the calibration pattern points in the calibration pattern coordinate space. In the old interface all the per-view vectors are concatenated. See `CalibrateCamera` for details.
* `imagePoints`: Vector of vectors of the projections of the calibration pattern points. In the old interface all the per-view vectors are concatenated.
* `imageSize`: Image size in pixels used to initialize the principal point.
* `aspectRatio`: If it is zero or negative, both formula and formula are estimated independently. Otherwise, formula . The function estimates and returns an initial camera intrinsic matrix for the camera calibration process. Currently, the function only supports planar calibration patterns, which are patterns where each object point has z-coordinate =0.

**Returns**: The returned value.

---
### `Cv2.CalibrateCamera`
**Signature**: `double CalibrateCamera(IntPtr objectPoints, IntPtr imagePoints, Size imageSize, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat perViewErrors, int flags, TermCriteria criteria)`

Finds the camera intrinsic and extrinsic parameters from several views of a calibration pattern.

**Detailed Remarks**:
.: info Note

If you use a non-square (i.e. non-N-by-N) grid and `findChessboardCorners` for calibration,
and `CalibrateCamera` returns bad values (zero distortion coefficients, formula and
formula very far from the image center, and/or large differences between formula and
formula (ratios of 10:1 or more)), then you are probably using patternSize=cvSize(rows,cols)
instead of using patternSize=cvSize(cols,rows) in `findChessboardCorners`.
.:
.: info Note

The function may throw exceptions, if unsupported combination of parameters is provided or
the system is underconstrained.
**See also**: 
CalibrateCameraRO, findChessboardCorners, SolvePnP, initCameraMatrix2D, stereoCalibrate,
undistort
.:

**Parameters**:
* `objectPoints`: In the new interface it is a arrays of matrices of calibration pattern points in the calibration pattern coordinate space (e.g. Vec3f[][]). The outer vector contains as many elements as the number of pattern views. If the same calibration pattern is shown in each view and it is fully visible, all the vectors will be the same. Although, it is possible to use partially occluded patterns or even different patterns in different views. Then, the vectors will be different. Although the points are 3D, they all lie in the calibration pattern's XY coordinate plane (thus 0 in the Z-coordinate), if the used calibration pattern is a planar rig. In the old interface all the vectors of object points from different views are concatenated together.
* `imagePoints`: In the new interface it is a arrays of matrices of the projections of calibration pattern points (e.g. Vec2f[][]). imagePoints.size() and objectPoints.size(), and imagePoints[i].size() and objectPoints[i].size() for each i, must be equal, respectively. In the old interface all the vectors of object points from different views are concatenated together.
* `imageSize`: Size of the image used only to initialize the camera intrinsic matrix.
* `cameraMatrix`: Input/output 3x3 floating-point camera intrinsic matrix formula . If `CALIB_USE_INTRINSIC_GUESS` and/or `CALIB_FIX_ASPECT_RATIO`, `CALIB_FIX_PRINCIPAL_POINT` or `CALIB_FIX_FOCAL_LENGTH` are specified, some or all of fx, fy, cx, cy must be initialized before calling the function.
* `distCoeffs`: Input/output vector of distortion coefficients formula.
* `rvecs`: Output vector of rotation vectors (`Rodrigues` ) estimated for each pattern view (e.g. Mat[]>). That is, each i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter description) brings the calibration pattern from the object coordinate space (in which object points are specified) to the camera coordinate space. In more technical terms, the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space to camera coordinate space. Due to its duality, this tuple is equivalent to the position of the calibration pattern with respect to the camera coordinate space.
* `tvecs`: Output vector of translation vectors estimated for each pattern view, see parameter description above.
* `stdDeviationsIntrinsics`: Output vector of standard deviations estimated for intrinsic parameters. Order of deviations values: `(f_x, f_y, c_x, c_y, k_1, k_2, p_1, p_2, k_3, k_4, k_5, k_6 , s_1, s_2, s_3, s_4, \tau_x, \tau_y)` If one of parameters is not estimated, it's deviation is equals to zero.
* `stdDeviationsExtrinsics`: Output vector of standard deviations estimated for extrinsic parameters. Order of deviations values: formula where M is the number of pattern views. formula are concatenated 1x3 vectors.
* `perViewErrors`: Output vector of the RMS re-projection error estimated for each pattern view.
* `flags`: Different flags that may be zero or a combination of the following values: -   `CALIB_USE_INTRINSIC_GUESS` cameraMatrix contains valid initial values of fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image center ( imageSize is used), and focal distances are computed in a least-squares fashion. Note, that if intrinsic parameters are known, there is no need to use this function just to estimate extrinsic parameters. Use `SolvePnP` instead. -   `CALIB_DISABLE_SCHUR_COMPLEMENT` Disable Schur complement and use the Bouguet calibration engine ([Zhang2000], [BouguetMCT]). -   `CALIB_FIX_PRINCIPAL_POINT` The principal point is not changed during the global optimization. It stays at the center or at a different location specified when `CALIB_USE_INTRINSIC_GUESS` is set too. -   `CALIB_FIX_ASPECT_RATIO` The functions consider only fy as a free parameter. The ratio fx/fy stays the same as in the input cameraMatrix . When `CALIB_USE_INTRINSIC_GUESS` is not set, the actual input values of fx and fy are ignored, only their ratio is computed and used further. -   `CALIB_ZERO_TANGENT_DIST` Tangential distortion coefficients formula are set to zeros and stay zero. -   `CALIB_FIX_FOCAL_LENGTH` The focal length is not changed during the global optimization if `CALIB_USE_INTRINSIC_GUESS` is set. -   `CALIB_FIX_K1`,..., `CALIB_FIX_K6` The corresponding radial distortion coefficient is not changed during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0. -   `CALIB_RATIONAL_MODEL` Coefficients k4, k5, and k6 are enabled. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the rational model and return 8 coefficients or more. -   `CALIB_THIN_PRISM_MODEL` Coefficients s1, s2, s3 and s4 are enabled. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the thin prism model and return 12 coefficients or more. -   `CALIB_FIX_S1_S2_S3_S4` The thin prism distortion coefficients are not changed during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0. -   `CALIB_TILTED_MODEL` Coefficients tauX and tauY are enabled. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the tilted sensor model and return 14 coefficients. -   `CALIB_FIX_TAUX_TAUY` The coefficients of the tilted sensor model are not changed during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
* `criteria`: Termination criteria for the iterative optimization algorithm.

**Returns**: the overall RMS re-projection error. The function estimates the intrinsic camera parameters and extrinsic parameters for each of the views. By default, the optimization follows a sparse bundle adjustment formulation with Schur complement; see [Triggs2000_bundle_adjustment] and [Lourakis2009_sba] for background. Use `CALIB_DISABLE_SCHUR_COMPLEMENT` to switch to the Bouguet calibration engine. The coordinates of 3D object points and their corresponding 2D projections in each view must be specified. That may be achieved by using an object with known geometry and easily detectable feature points. Such an object is called a calibration rig or calibration pattern, and OpenCV has built-in support for a chessboard as a calibration rig (see `findChessboardCorners`). Currently, initialization of intrinsic parameters (when `CALIB_USE_INTRINSIC_GUESS` is not set) is only implemented for planar calibration patterns (where Z-coordinates of the object points must be all zeros). 3D calibration rigs can also be used as long as initial cameraMatrix is provided. The algorithm performs the following steps: -   Compute the initial intrinsic parameters (the option only available for planar calibration patterns) or read them from the input parameters. The distortion coefficients are all set to zeros initially unless some of CALIB_FIX_K? are specified. -   Estimate the initial camera pose as if the intrinsic parameters have been already known. This is done using `SolvePnP` . -   Run the global Levenberg-Marquardt optimization algorithm to minimize the reprojection error, that is, the total sum of squared distances between the observed feature points imagePoints and the projected (using the current estimates for camera parameters and the poses) object points objectPoints. See `projectPoints` for details. -   In practice, robust acquisition is essential for stable results: use multiple board poses with significant tilt, avoid collecting all views at a single working distance, span the expected working-distance range (a larger board with larger squares can help for longer distances).

---
### `Cv2.CalibrateCamera`
**Signature**: `double CalibrateCamera(IntPtr objectPoints, IntPtr imagePoints, Size imageSize, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, int flags, TermCriteria criteria)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `imageSize`: The imageSize parameter.
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `rvecs`: The rvecs parameter.
* `tvecs`: The tvecs parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.CalibrateCameraRO`
**Signature**: `double CalibrateCameraRO(IntPtr objectPoints, IntPtr imagePoints, Size imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, Mat newObjPoints, Mat stdDeviationsIntrinsics, Mat stdDeviationsExtrinsics, Mat stdDeviationsObjPoints, Mat perViewErrors, int flags, TermCriteria criteria)`

Finds the camera intrinsic and extrinsic parameters from several views of a calibration pattern.

**Detailed Remarks**:
This function is an extension of `CalibrateCamera` with the method of releasing object which was
proposed in **Citation**:  strobl2011iccv. In many common cases with inaccurate, unmeasured, roughly planar
targets (calibration plates), this method can dramatically improve the precision of the estimated
camera parameters. Both the object-releasing method and standard method are supported by this
function. Use the parameter **iFixedPoint** for method selection. In the internal implementation,
`CalibrateCamera` is a wrapper for this function.
**See also**: 
CalibrateCamera, findChessboardCorners, SolvePnP, initCameraMatrix2D, stereoCalibrate, undistort

**Parameters**:
* `objectPoints`: Vector of vectors of calibration pattern points in the calibration pattern coordinate space. See `CalibrateCamera` for details. If the method of releasing object to be used, the identical calibration board must be used in each view and it must be fully visible, and all objectPoints[i] must be the same and all points should be roughly close to a plane. **The calibration target has to be rigid, or at least static if the camera (rather than the calibration target) is shifted for grabbing images.**
* `imagePoints`: Vector of vectors of the projections of calibration pattern points. See `CalibrateCamera` for details.
* `imageSize`: Size of the image used only to initialize the intrinsic camera matrix.
* `iFixedPoint`: The index of the 3D object point in objectPoints[0] to be fixed. It also acts as a switch for calibration method selection. If object-releasing method to be used, pass in the parameter in the range of [1, objectPoints[0].size()-2], otherwise a value out of this range will make standard calibration method selected. Usually the top-right corner point of the calibration board grid is recommended to be fixed when object-releasing method being utilized. According to [Strobl2011iccv], two other points are also fixed. In this implementation, objectPoints[0].front and objectPoints[0].back.z are used. With object-releasing method, accurate rvecs, tvecs and newObjPoints are only possible if coordinates of these three fixed points are accurate enough.
* `cameraMatrix`: Output 3x3 floating-point camera matrix. See `CalibrateCamera` for details.
* `distCoeffs`: Output vector of distortion coefficients. See `CalibrateCamera` for details.
* `rvecs`: Output vector of rotation vectors estimated for each pattern view. See `CalibrateCamera` for details.
* `tvecs`: Output vector of translation vectors estimated for each pattern view.
* `newObjPoints`: The updated output vector of calibration pattern points. The coordinates might be scaled based on three fixed points. The returned coordinates are accurate only if the above mentioned three fixed points are accurate. If not needed, null can be passed in. This parameter is ignored with standard calibration method.
* `stdDeviationsIntrinsics`: Output vector of standard deviations estimated for intrinsic parameters. See `CalibrateCamera` for details.
* `stdDeviationsExtrinsics`: Output vector of standard deviations estimated for extrinsic parameters. See `CalibrateCamera` for details.
* `stdDeviationsObjPoints`: Output vector of standard deviations estimated for refined coordinates of calibration pattern points. It has the same size and order as objectPoints[0] vector. This parameter is ignored with standard calibration method.
* `perViewErrors`: Output vector of the RMS re-projection error estimated for each pattern view.
* `flags`: Different flags that may be zero or a combination of some predefined values. See `CalibrateCamera` for details. If the method of releasing object is used, the calibration time may be much longer. CALIB_USE_QR or CALIB_USE_LU could be used for faster calibration with potentially less precise and less stable in some rare cases.
* `criteria`: Termination criteria for the iterative optimization algorithm.

**Returns**: the overall RMS re-projection error. The function estimates the intrinsic camera parameters and extrinsic parameters for each of the views. The object-releasing extension follows [strobl2011iccv] and uses the same optimization core as `CalibrateCamera`. See `CalibrateCamera` for other detailed explanations.

---
### `Cv2.CalibrateCameraRO`
**Signature**: `double CalibrateCameraRO(IntPtr objectPoints, IntPtr imagePoints, Size imageSize, int iFixedPoint, Mat cameraMatrix, Mat distCoeffs, IntPtr rvecs, IntPtr tvecs, Mat newObjPoints, int flags, TermCriteria criteria)`

This is an overloaded member function, provided for convenience.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `imageSize`: The imageSize parameter.
* `iFixedPoint`: The iFixedPoint parameter.
* `cameraMatrix`: The cameraMatrix parameter.
* `distCoeffs`: The distCoeffs parameter.
* `rvecs`: The rvecs parameter.
* `tvecs`: The tvecs parameter.
* `newObjPoints`: The newObjPoints parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.StereoCalibrate`
**Signature**: `double StereoCalibrate(IntPtr objectPoints, IntPtr imagePoints1, IntPtr imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Size imageSize, Mat R, Mat T, Mat E, Mat F, IntPtr rvecs, IntPtr tvecs, Mat perViewErrors, int flags, TermCriteria criteria)`

Calibrates a stereo camera set up. This function finds the intrinsic parameters for each of the two cameras and the extrinsic parameters between the two cameras.

**Parameters**:
* `objectPoints`: Vector of vectors of the calibration pattern points. The same structure as in `CalibrateCamera`. For each pattern view, both cameras need to see the same object points. Therefore, objectPoints.size(), imagePoints1.size(), and imagePoints2.size() need to be equal as well as objectPoints[i].size(), imagePoints1[i].size(), and imagePoints2[i].size() need to be equal for each i.
* `imagePoints1`: Vector of vectors of the projections of the calibration pattern points, observed by the first camera. The same structure as in `CalibrateCamera`.
* `imagePoints2`: Vector of vectors of the projections of the calibration pattern points, observed by the second camera. The same structure as in `CalibrateCamera`.
* `cameraMatrix1`: Input/output camera intrinsic matrix for the first camera, the same as in `CalibrateCamera`. Furthermore, for the stereo case, additional flags may be used, see below.
* `distCoeffs1`: Input/output vector of distortion coefficients, the same as in `CalibrateCamera`.
* `cameraMatrix2`: Input/output second camera intrinsic matrix for the second camera. See description for cameraMatrix1.
* `distCoeffs2`: Input/output lens distortion coefficients for the second camera. See description for distCoeffs1.
* `imageSize`: Size of the image used only to initialize the camera intrinsic matrices.
* `R`: Output rotation matrix. Together with the translation vector T, this matrix brings points given in the first camera's coordinate system to points in the second camera's coordinate system. In more technical terms, the tuple of R and T performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Due to its duality, this tuple is equivalent to the position of the first camera with respect to the second camera coordinate system.
* `T`: Output translation vector, see description above.
* `E`: Output essential matrix.
* `F`: Output fundamental matrix.
* `rvecs`: Output vector of rotation vectors ( `Rodrigues` ) estimated for each pattern view in the coordinate system of the first camera of the stereo pair (e.g. Mat[]). More in detail, each i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter description) brings the calibration pattern from the object coordinate space (in which object points are specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms, the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space to camera coordinate space of the first camera of the stereo pair.
* `tvecs`: Output vector of translation vectors estimated for each pattern view, see parameter description of previous output parameter ( rvecs ).
* `perViewErrors`: Output vector of the RMS re-projection error estimated for each pattern view.
* `flags`: Different flags that may be zero or a combination of the following values: -   `CALIB_FIX_INTRINSIC` Fix cameraMatrix? and distCoeffs? so that only R, T, E, and F matrices are estimated. -   `CALIB_USE_INTRINSIC_GUESS` Optimize some or all of the intrinsic parameters according to the specified flags. Initial values are provided by the user. -   `CALIB_USE_EXTRINSIC_GUESS` R and T contain valid initial values that are optimized further. Otherwise R and T are initialized to the median value of the pattern views (each dimension separately). -   `CALIB_FIX_PRINCIPAL_POINT` Fix the principal points during the optimization. -   `CALIB_FIX_FOCAL_LENGTH` Fix formula and formula . -   `CALIB_FIX_ASPECT_RATIO` Optimize formula . Fix the ratio formula . -   `CALIB_SAME_FOCAL_LENGTH` Enforce formula and formula . -   `CALIB_ZERO_TANGENT_DIST` Set tangential distortion coefficients for each camera to zeros and fix there. -   `CALIB_FIX_K1`,..., `CALIB_FIX_K6` Do not change the corresponding radial distortion coefficient during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0. -   `CALIB_RATIONAL_MODEL` Enable coefficients k4, k5, and k6. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the rational model and return 8 coefficients. If the flag is not set, the function computes and returns only 5 distortion coefficients. -   `CALIB_THIN_PRISM_MODEL` Coefficients s1, s2, s3 and s4 are enabled. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the thin prism model and return 12 coefficients. If the flag is not set, the function computes and returns only 5 distortion coefficients. -   `CALIB_FIX_S1_S2_S3_S4` The thin prism distortion coefficients are not changed during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0. -   `CALIB_TILTED_MODEL` Coefficients tauX and tauY are enabled. To provide the backward compatibility, this extra flag should be explicitly specified to make the calibration function use the tilted sensor model and return 14 coefficients. If the flag is not set, the function computes and returns only 5 distortion coefficients. -   `CALIB_FIX_TAUX_TAUY` The coefficients of the tilted sensor model are not changed during the optimization. If `CALIB_USE_INTRINSIC_GUESS` is set, the coefficient from the supplied distCoeffs matrix is used. Otherwise, it is set to 0.
* `criteria`: Termination criteria for the iterative optimization algorithm. The function estimates the transformation between two cameras making a stereo pair. If one computes the poses of an object relative to the first camera and to the second camera, ( formula,formula ) and (formula,formula), respectively, for a stereo camera where the relative position and orientation between the two cameras are fixed, then those poses definitely relate to each other. This means, if the relative position and orientation (formula,formula) of the two cameras is known, it is possible to compute (formula,formula) when (formula,formula) is given. This is what the described function does. It computes (formula,formula) such that: [see mathematical formula in OpenCV docs] [see mathematical formula in OpenCV docs] Therefore, one can compute the coordinate representation of a 3D point for the second camera's coordinate system when given the point's coordinate representation in the first camera's coordinate system: [see mathematical formula in OpenCV documentation] Optionally, it computes the essential matrix E: [see mathematical formula in OpenCV docs] where formula are components of the translation vector formula : formula . And the function can also compute the fundamental matrix F: [see mathematical formula in OpenCV docs] Besides the stereo-related information, the function can also perform a full calibration of each of the two cameras. However, due to the high dimensionality of the parameter space and noise in the input data, the function can diverge from the correct solution. If the intrinsic parameters can be estimated with high accuracy for each of the cameras individually (for example, using `CalibrateCamera` ), you are recommended to do so and then pass `CALIB_FIX_INTRINSIC` flag to the function along with the computed intrinsic parameters. Otherwise, if all the parameters are estimated at once, it makes sense to restrict some parameters, for example, pass `CALIB_SAME_FOCAL_LENGTH` and `CALIB_ZERO_TANGENT_DIST` flags, which is usually a reasonable assumption. Similarly to `CalibrateCamera`, the function minimizes the total re-projection error for all the points in all the available views from both cameras. The function returns the final value of the re-projection error.

**Returns**: The returned value.

---
### `Cv2.StereoCalibrate`
**Signature**: `double StereoCalibrate(IntPtr objectPoints, IntPtr imagePoints1, IntPtr imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Size imageSize, Mat R, Mat T, Mat E, Mat F, int flags, TermCriteria criteria)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints1`: The imagePoints1 parameter.
* `imagePoints2`: The imagePoints2 parameter.
* `cameraMatrix1`: The cameraMatrix1 parameter.
* `distCoeffs1`: The distCoeffs1 parameter.
* `cameraMatrix2`: The cameraMatrix2 parameter.
* `distCoeffs2`: The distCoeffs2 parameter.
* `imageSize`: The imageSize parameter.
* `R`: The R parameter.
* `T`: The T parameter.
* `E`: The E parameter.
* `F`: The F parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.StereoCalibrate`
**Signature**: `double StereoCalibrate(IntPtr objectPoints, IntPtr imagePoints1, IntPtr imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, Mat cameraMatrix2, Mat distCoeffs2, Size imageSize, Mat R, Mat T, Mat E, Mat F, Mat perViewErrors, int flags, TermCriteria criteria)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints1`: The imagePoints1 parameter.
* `imagePoints2`: The imagePoints2 parameter.
* `cameraMatrix1`: The cameraMatrix1 parameter.
* `distCoeffs1`: The distCoeffs1 parameter.
* `cameraMatrix2`: The cameraMatrix2 parameter.
* `distCoeffs2`: The distCoeffs2 parameter.
* `imageSize`: The imageSize parameter.
* `R`: The R parameter.
* `T`: The T parameter.
* `E`: The E parameter.
* `F`: The F parameter.
* `perViewErrors`: The perViewErrors parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.RegisterCameras`
**Signature**: `double RegisterCameras(IntPtr objectPoints1, IntPtr objectPoints2, IntPtr imagePoints1, IntPtr imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, CameraModel cameraModel1, Mat cameraMatrix2, Mat distCoeffs2, CameraModel cameraModel2, Mat R, Mat T, Mat E, Mat F, IntPtr rvecs, IntPtr tvecs, Mat perViewErrors, int flags, TermCriteria criteria)`

Calibrates a camera pair set up. This function finds the extrinsic parameters between the two cameras.

**Detailed Remarks**:
**See also**: CalibrateCamera, stereoCalibrate

**Parameters**:
* `objectPoints1`: Vector of vectors of the calibration pattern points for camera 1. A similar structure as objectPoints in `CalibrateCamera` and for each pattern view, both cameras do not need to see the same object points. objectPoints1.size(), imagePoints1.size() nees to be equal,as well as objectPoints1[i].size(), imagePoints1[i].size() need to be equal for each i.
* `objectPoints2`: Vector of vectors of the calibration pattern points for camera 2. A similar structure as objectPoints1. objectPoints2.size(), and imagePoints2.size() nees to be equal, as well as objectPoints2[i].size(), imagePoints2[i].size() need to be equal for each i. However, objectPoints1[i].size() and objectPoints2[i].size() are not required to be equal.
* `imagePoints1`: Vector of vectors of the projections of the calibration pattern points, observed by the first camera. The same structure as in `CalibrateCamera`.
* `imagePoints2`: Vector of vectors of the projections of the calibration pattern points, observed by the second camera. The same structure as in `CalibrateCamera`.
* `cameraMatrix1`: Input/output camera intrinsic matrix for the first camera, the same as in `CalibrateCamera`. Furthermore, for the stereo case, additional flags may be used, see below.
* `distCoeffs1`: Input/output vector of distortion coefficients, the same as in `CalibrateCamera`.
* `cameraModel1`: Flag reflecting the type of model for camera 1 (pinhole / fisheye): - `CALIB_MODEL_PINHOLE` pinhole camera model - `CALIB_MODEL_FISHEYE` fisheye camera model
* `cameraMatrix2`: Input/output second camera intrinsic matrix for the second camera. See description for cameraMatrix1.
* `distCoeffs2`: Input/output lens distortion coefficients for the second camera. See description for distCoeffs1.
* `cameraModel2`: Flag reflecting the type of model for camera 2 (pinhole / fisheye). See description for cameraModel1.
* `R`: Output rotation matrix. Together with the translation vector T, this matrix brings points given in the first camera's coordinate system to points in the second camera's coordinate system. In more technical terms, the tuple of R and T performs a change of basis from the first camera's coordinate system to the second camera's coordinate system. Due to its duality, this tuple is equivalent to the position of the first camera with respect to the second camera coordinate system.
* `T`: Output translation vector, see description above.
* `E`: Output essential matrix.
* `F`: Output fundamental matrix.
* `rvecs`: Output vector of rotation vectors ( `Rodrigues` ) estimated for each pattern view in the coordinate system of the first camera of the stereo pair (e.g. Mat[]). More in detail, each i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter description) brings the calibration pattern from the object coordinate space (in which object points are specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms, the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space to the camera coordinate space of the first camera of the stereo pair.
* `tvecs`: Output vector of translation vectors estimated for each pattern view, see parameter description of previous output parameter ( rvecs ).
* `perViewErrors`: Output vector of the RMS re-projection error estimated for each pattern view.
* `flags`: Different flags that may be zero or a combination of the following values: -   `CALIB_USE_EXTRINSIC_GUESS` R and T contain valid initial values that are optimized further.
* `criteria`: Termination criteria for the iterative optimization algorithm. The function estimates the transformation between two cameras similar to stereo pair calibration. The principle follows closely to `stereoCalibrate`. To understand the problem of estimating the relative pose between a camera pair, please refer to the description there. The difference for this function is that, camera intrinsics are not optimized and two cameras are not required to have overlapping fields of view as long as they are observing the same calibration target and the absolute positions of each object point are known.  The above illustration shows an example where such a case may become relevant. Additionally, it supports a camera pair with the mixed model (pinhole / fisheye). Similarly to `CalibrateCamera`, the function minimizes the total re-projection error for all the points in all the available views from both cameras.

**Returns**: the final value of the re-projection error.

---
### `Cv2.RegisterCameras`
**Signature**: `double RegisterCameras(IntPtr objectPoints1, IntPtr objectPoints2, IntPtr imagePoints1, IntPtr imagePoints2, Mat cameraMatrix1, Mat distCoeffs1, CameraModel cameraModel1, Mat cameraMatrix2, Mat distCoeffs2, CameraModel cameraModel2, Mat R, Mat T, Mat E, Mat F, Mat perViewErrors, int flags, TermCriteria criteria)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objectPoints1`: The objectPoints1 parameter.
* `objectPoints2`: The objectPoints2 parameter.
* `imagePoints1`: The imagePoints1 parameter.
* `imagePoints2`: The imagePoints2 parameter.
* `cameraMatrix1`: The cameraMatrix1 parameter.
* `distCoeffs1`: The distCoeffs1 parameter.
* `cameraModel1`: The cameraModel1 parameter.
* `cameraMatrix2`: The cameraMatrix2 parameter.
* `distCoeffs2`: The distCoeffs2 parameter.
* `cameraModel2`: The cameraModel2 parameter.
* `R`: The R parameter.
* `T`: The T parameter.
* `E`: The E parameter.
* `F`: The F parameter.
* `perViewErrors`: The perViewErrors parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.CalibrateMultiview`
**Signature**: `double CalibrateMultiview(IntPtr objPoints, IntPtr imagePoints, IntPtr imageSize, Mat detectionMask, Mat models, IntPtr Ks, IntPtr distortions, IntPtr Rs, IntPtr Ts, Mat initializationPairs, IntPtr rvecs0, IntPtr tvecs0, Mat perFrameErrors, Mat? flagsForIntrinsics, int flags, TermCriteria criteria)`

Estimates intrinsics and extrinsics (camera pose) for multi-camera system a.k.a multiview calibration.

**Detailed Remarks**:
**See also**: findChessboardCorners, findCirclesGrid, CalibrateCamera, fisheye.calibrate, registerCameras

**Parameters**:
* `objPoints`: The objPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `imageSize`: The imageSize parameter.
* `detectionMask`: The detectionMask parameter.
* `models`: The models parameter.
* `Ks`: The Ks parameter.
* `distortions`: The distortions parameter.
* `Rs`: The Rs parameter.
* `Ts`: The Ts parameter.
* `initializationPairs`: The initializationPairs parameter.
* `rvecs0`: The rvecs0 parameter.
* `tvecs0`: The tvecs0 parameter.
* `perFrameErrors`: The perFrameErrors parameter.
* `flagsForIntrinsics`: The flagsForIntrinsics parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: Overall RMS re-projection error over detectionMask.

---
### `Cv2.CalibrateMultiview`
**Signature**: `double CalibrateMultiview(IntPtr objPoints, IntPtr imagePoints, IntPtr imageSize, Mat detectionMask, Mat models, IntPtr Ks, IntPtr distortions, IntPtr Rs, IntPtr Ts, Mat? flagsForIntrinsics, int flags, TermCriteria criteria)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objPoints`: The objPoints parameter.
* `imagePoints`: The imagePoints parameter.
* `imageSize`: The imageSize parameter.
* `detectionMask`: The detectionMask parameter.
* `models`: The models parameter.
* `Ks`: The Ks parameter.
* `distortions`: The distortions parameter.
* `Rs`: The Rs parameter.
* `Ts`: The Ts parameter.
* `flagsForIntrinsics`: The flagsForIntrinsics parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
### `Cv2.FisheyeCalibrate`
**Signature**: `double FisheyeCalibrate(IntPtr objectPoints, IntPtr imagePoints, Size image_size, Mat K, Mat D, IntPtr rvecs, IntPtr tvecs, int flags, TermCriteria criteria)`

Performs camera calibration

**Parameters**:
* `objectPoints`: arrays of matrices of calibration pattern points in the calibration pattern coordinate space.
* `imagePoints`: arrays of matrices of the projections of calibration pattern points. imagePoints.size() and objectPoints.size() and imagePoints[i].size() must be equal to objectPoints[i].size() for each i.
* `image_size`: Size of the image used only to initialize the camera intrinsic matrix.
* `K`: Output 3x3 floating-point camera intrinsic matrix formula . If `CALIB_USE_INTRINSIC_GUESS` is specified, some or all of fx, fy, cx, cy must be initialized before calling the function.
* `D`: Output vector of distortion coefficients formula.
* `rvecs`: Output vector of rotation vectors (see Rodrigues ) estimated for each pattern view. That is, each k-th rotation vector together with the corresponding k-th translation vector (see the next output parameter description) brings the calibration pattern from the model coordinate space (in which object points are specified) to the world coordinate space, that is, a real position of the calibration pattern in the k-th pattern view (k=0.. *M* -1).
* `tvecs`: Output vector of translation vectors estimated for each pattern view.
* `flags`: Different flags that may be zero or a combination of the following values: -   `CALIB_USE_INTRINSIC_GUESS`  cameraMatrix contains valid initial values of fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image center ( imageSize is used), and focal distances are computed in a least-squares fashion. -   `CALIB_RECOMPUTE_EXTRINSIC`  Extrinsic will be recomputed after each iteration of intrinsic optimization. -   `CALIB_CHECK_COND`  The functions will check validity of condition number. -   `CALIB_FIX_SKEW`  Skew coefficient (alpha) is set to zero and stay zero. -   `CALIB_FIX_K1`,..., `CALIB_FIX_K4` Selected distortion coefficients are set to zeros and stay zero. -   `CALIB_FIX_PRINCIPAL_POINT`  The principal point is not changed during the global optimization. It stays at the center or at a different location specified when `CALIB_USE_INTRINSIC_GUESS` is set too. -   `CALIB_FIX_FOCAL_LENGTH` The focal length is not changed during the global optimization. It is the formula or the provided formula, formula when `CALIB_USE_INTRINSIC_GUESS` is set too.
* `criteria`: Termination criteria for the iterative optimization algorithm.

**Returns**: The returned value.

---
### `Cv2.FisheyeStereoCalibrate`
**Signature**: `double FisheyeStereoCalibrate(IntPtr objectPoints, IntPtr imagePoints1, IntPtr imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, Size imageSize, Mat R, Mat T, IntPtr rvecs, IntPtr tvecs, int flags, TermCriteria criteria)`

Performs stereo calibration

**Parameters**:
* `objectPoints`: Vector of vectors of the calibration pattern points.
* `imagePoints1`: Vector of vectors of the projections of the calibration pattern points, observed by the first camera.
* `imagePoints2`: Vector of vectors of the projections of the calibration pattern points, observed by the second camera.
* `K1`: Input/output first camera intrinsic matrix: formula , formula . If any of `CALIB_USE_INTRINSIC_GUESS` , `CALIB_FIX_INTRINSIC` are specified, some or all of the matrix components must be initialized.
* `D1`: Input/output vector of distortion coefficients formula of 4 elements.
* `K2`: Input/output second camera intrinsic matrix. The parameter is similar to K1 .
* `D2`: Input/output lens distortion coefficients for the second camera. The parameter is similar to D1 .
* `imageSize`: Size of the image used only to initialize camera intrinsic matrix.
* `R`: Output rotation matrix between the 1st and the 2nd camera coordinate systems.
* `T`: Output translation vector between the coordinate systems of the cameras.
* `rvecs`: Output vector of rotation vectors ( `Rodrigues` ) estimated for each pattern view in the coordinate system of the first camera of the stereo pair (e.g. Mat[]). More in detail, each i-th rotation vector together with the corresponding i-th translation vector (see the next output parameter description) brings the calibration pattern from the object coordinate space (in which object points are specified) to the camera coordinate space of the first camera of the stereo pair. In more technical terms, the tuple of the i-th rotation and translation vector performs a change of basis from object coordinate space to camera coordinate space of the first camera of the stereo pair.
* `tvecs`: Output vector of translation vectors estimated for each pattern view, see parameter description of previous output parameter ( rvecs ).
* `flags`: Different flags that may be zero or a combination of the following values: -   `CALIB_FIX_INTRINSIC`  Fix K1, K2? and D1, D2? so that only R, T matrices are estimated. -   `CALIB_USE_INTRINSIC_GUESS`  K1, K2 contains valid initial values of fx, fy, cx, cy that are optimized further. Otherwise, (cx, cy) is initially set to the image center (imageSize is used), and focal distances are computed in a least-squares fashion. -   `CALIB_RECOMPUTE_EXTRINSIC`  Extrinsic will be recomputed after each iteration of intrinsic optimization. -   `CALIB_CHECK_COND`  The functions will check validity of condition number. -   `CALIB_FIX_SKEW`  Skew coefficient (alpha) is set to zero and stay zero. -   `CALIB_FIX_K1`,..., `CALIB_FIX_K4` Selected distortion coefficients are set to zeros and stay zero.
* `criteria`: Termination criteria for the iterative optimization algorithm.

**Returns**: The returned value.

---
### `Cv2.FisheyeStereoCalibrate`
**Signature**: `double FisheyeStereoCalibrate(IntPtr objectPoints, IntPtr imagePoints1, IntPtr imagePoints2, Mat K1, Mat D1, Mat K2, Mat D2, Size imageSize, Mat R, Mat T, int flags, TermCriteria criteria)`

Wrapper for OpenCV's native functionality.

**Parameters**:
* `objectPoints`: The objectPoints parameter.
* `imagePoints1`: The imagePoints1 parameter.
* `imagePoints2`: The imagePoints2 parameter.
* `K1`: The K1 parameter.
* `D1`: The D1 parameter.
* `K2`: The K2 parameter.
* `D2`: The D2 parameter.
* `imageSize`: The imageSize parameter.
* `R`: The R parameter.
* `T`: The T parameter.
* `flags`: Operation flags.
* `criteria`: The criteria parameter.

**Returns**: The returned value.

---
## 🔢 Enumerations

### `CameraModel`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Pinhole`** | `0` | Pinhole |
| **`Fisheye`** | `1` | Fisheye |

---
### `HandEyeCalibrationMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Tsai`** | `0` | Tsai |
| **`Park`** | `1` | Park |
| **`Horaud`** | `2` | Horaud |
| **`Andreff`** | `3` | Andreff |
| **`Daniilidis`** | `4` | Daniilidis |

---
### `RobotWorldHandEyeCalibrationMethod`
Wrapper for OpenCV's native functionality.

| Member | Value | Description |
| :--- | :--- | :--- |
| **`Shah`** | `0` | Shah |
| **`Li`** | `1` | Li |

---

</div>