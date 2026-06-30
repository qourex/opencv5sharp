# FLANN Module API Reference

Complete documentation for the **FLANN** classes, methods, and enums in OpenCV5Sharp. Equivalent to the [Official OpenCV Flann Documentation](https://docs.opencv.org/5.x/main_modules/flann.html).

---
<div v-pre>

## 📦 Classes and Structs

### `FlannIndex`
**Inherits from**: `DisposableOpenCVObject`

FLANN (Fast Library for Approximate Nearest Neighbors) index for performing fast nearest-neighbor searches. Wraps the native `cv::flann::Index` class and manages its lifecycle through `DisposableOpenCVObject`. The underlying native handle is released via `flann_Index_Delete` on disposal.

#### Constructors
* `new FlannIndex()`
  * *Summary*: Creates a new, empty FLANN index. The index must be populated later using `Build()` or `Load()` before performing searches.
  * *Throws*: `OpenCVException` if the underlying native call fails.
* `new FlannIndex(Mat features, FlannIndexParams @params, IntPtr distType)`
  * *Summary*: Creates a new FLANN index and immediately builds it from the given feature descriptors, index parameters, and distance type.
  * *Parameter* `features`: Matrix of feature descriptors (one row per feature) used to build the index.
  * *Parameter* `params`: Index parameters that control the algorithm and its configuration (e.g., KD-tree, KMeans, etc.).
  * *Parameter* `distType`: Native pointer specifying the distance metric to use when comparing features.
  * *Throws*: `ArgumentNullException` if `features` or `params` is `null`. `ObjectDisposedException` if `features` or `params` has been disposed. `OpenCVException` if the underlying native call fails.

#### Methods
* `void Build(Mat features, FlannIndexParams @params, IntPtr distType)`
  * *Summary*: Builds (or rebuilds) the FLANN index from the given feature descriptors, replacing any previously built index.
  * *Parameter* `features`: Matrix of feature descriptors (one row per feature) used to build the index.
  * *Parameter* `params`: Index parameters that control the algorithm and its configuration.
  * *Parameter* `distType`: Native pointer specifying the distance metric to use when comparing features.
  * *Throws*: `ArgumentNullException` if `features` or `params` is `null`. `ObjectDisposedException` if the index, `features`, or `params` has been disposed. `OpenCVException` if the underlying native call fails.
* `void KnnSearch(Mat query, Mat indices, Mat dists, int knn, FlannSearchParams? @params)`
  * *Summary*: Performs a K-nearest-neighbors search on the index. For each row in the query matrix, finds the `knn` closest feature vectors and writes their indices and distances into the output matrices.
  * *Parameter* `query`: Matrix of query feature vectors (one row per query point).
  * *Parameter* `indices`: Output matrix that will be filled with the indices of the nearest neighbors found.
  * *Parameter* `dists`: Output matrix that will be filled with the distances to the nearest neighbors found.
  * *Parameter* `knn`: Number of nearest neighbors to find for each query point.
  * *Parameter* `params`: Optional search parameters controlling the search behavior (e.g., number of checks). Pass `null` for defaults.
  * *Throws*: `ArgumentNullException` if `query`, `indices`, or `dists` is `null`. `ObjectDisposedException` if the index or any parameter has been disposed. `OpenCVException` if the underlying native call fails.
* `int RadiusSearch(Mat query, Mat indices, Mat dists, double radius, int maxResults, FlannSearchParams? @params)`
  * *Summary*: Performs a radius-based search on the index. For each row in the query matrix, finds all feature vectors within the specified distance radius, up to a maximum number of results.
  * *Parameter* `query`: Matrix of query feature vectors (one row per query point).
  * *Parameter* `indices`: Output matrix that will be filled with the indices of the neighbors found within the radius.
  * *Parameter* `dists`: Output matrix that will be filled with the distances to the neighbors found within the radius.
  * *Parameter* `radius`: The search radius; only neighbors within this distance are returned.
  * *Parameter* `maxResults`: Maximum number of neighbors to return per query point.
  * *Parameter* `params`: Optional search parameters controlling the search behavior. Pass `null` for defaults.
  * *Returns*: The number of neighbors found within the specified radius (capped by `maxResults`).
  * *Throws*: `ArgumentNullException` if `query`, `indices`, or `dists` is `null`. `ObjectDisposedException` if the index or any parameter has been disposed. `OpenCVException` if the underlying native call fails.
* `void Save(string filename)`
  * *Summary*: Saves the built FLANN index to a file so it can be reloaded later without rebuilding.
  * *Parameter* `filename`: Path to the file where the index will be saved.
  * *Throws*: `ArgumentNullException` if `filename` is `null`. `OpenCVException` if the underlying native call fails.
* `bool Load(Mat features, string filename)`
  * *Summary*: Loads a previously saved FLANN index from a file. The original feature descriptors must be provided since only the index structure is stored, not the feature data itself.
  * *Parameter* `features`: Matrix of feature descriptors that were used when the index was originally built.
  * *Parameter* `filename`: Path to the file from which to load the index.
  * *Returns*: `true` if the index was loaded successfully; `false` otherwise.
  * *Throws*: `ArgumentNullException` if `features` is `null` or `filename` is `null`. `ObjectDisposedException` if the index or `features` has been disposed. `OpenCVException` if the underlying native call fails.
* `void Release()`
  * *Summary*: Releases the internally held native FLANN index resources. After calling this method, the index object should not be used for searches.
  * *Throws*: `OpenCVException` if the underlying native call fails.
* `IntPtr GetDistance()`
  * *Summary*: Returns the distance metric type used by the built FLANN index.
  * *Returns*: A native pointer representing the distance type identifier used by this index.
  * *Throws*: `OpenCVException` if the underlying native call fails.
* `IntPtr GetAlgorithm()`
  * *Summary*: Returns the algorithm type used by the built FLANN index.
  * *Returns*: A native pointer representing the algorithm type identifier (e.g., KD-tree, KMeans) used by this index.
  * *Throws*: `OpenCVException` if the underlying native call fails.

---
### `FlannIndexParams`
**Inherits from**: `DisposableOpenCVObject`

Represents index parameter settings for a FLANN index. Wraps the native `cv::flann::IndexParams` class. Used to configure the indexing algorithm (e.g., KD-tree, KMeans, composite, autotuned) when building a `FlannIndex`. The underlying native handle is released via `flann_IndexParams_Delete` on disposal.

#### Constructors
* `new FlannIndexParams()`
  * *Summary*: Creates a new `FlannIndexParams` instance with default parameter values.
  * *Throws*: `OpenCVException` if the underlying native call fails.

---
### `FlannSearchParams`
**Inherits from**: `DisposableOpenCVObject`

Represents search parameter settings used when performing queries on a FLANN index. Wraps the native `cv::flann::SearchParams` class. Controls search behavior such as the number of recursive checks during a search. The underlying native handle is released via `flann_SearchParams_Delete` on disposal.

#### Constructors
* `new FlannSearchParams()`
  * *Summary*: Creates a new `FlannSearchParams` instance with default search parameter values.
  * *Throws*: `OpenCVException` if the underlying native call fails.

---
## 🔢 Enumerations

### `FlannFlannIndexType`
Specifies the data type constants used by FLANN index parameters. These values correspond to OpenCV's internal type identifiers for FLANN parameter storage and mirror standard OpenCV depth types with additional FLANN-specific types.

**Backing type**: `int`

| Member | Value | Description |
| :--- | :--- | :--- |
| **`FlannIndexType8u`** | `0` | 8-bit unsigned integer type (corresponds to `CV_8U`). |
| **`FlannIndexType8s`** | `1` | 8-bit signed integer type (corresponds to `CV_8S`). |
| **`FlannIndexType16u`** | `2` | 16-bit unsigned integer type (corresponds to `CV_16U`). |
| **`FlannIndexType16s`** | `3` | 16-bit signed integer type (corresponds to `CV_16S`). |
| **`FlannIndexType32s`** | `4` | 32-bit signed integer type (corresponds to `CV_32S`). |
| **`FlannIndexType32f`** | `5` | 32-bit floating-point type (corresponds to `CV_32F`). |
| **`FlannIndexType64f`** | `6` | 64-bit floating-point type (corresponds to `CV_64F`). |
| **`FlannIndexTypeString`** | `7` | String parameter type for FLANN index configuration. |
| **`FlannIndexTypeBool`** | `8` | Boolean parameter type for FLANN index configuration. |
| **`FlannIndexTypeAlgorithm`** | `9` | Algorithm identifier parameter type for FLANN index configuration. |
| **`LastValueFlannIndexType`** | `9` | Sentinel value equal to the last defined type (`FlannIndexTypeAlgorithm`). Used for range validation. |

---

</div>