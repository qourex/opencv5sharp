// Copyright (c) 2026 qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

#error This is a shadow header file, which is not intended for processing by any compiler. \
       Only bindings parser should handle this file.

namespace cv
{

class CV_EXPORTS_W Mat
{
public:
    CV_WRAP Mat();
    CV_WRAP Mat(int rows, int cols, int type);
    CV_WRAP Mat(int rows, int cols, int type, void* data, size_t step = 0);
    CV_WRAP Mat(const Mat& m, const Range& rowRange, const Range& colRange);
    CV_WRAP Mat(const Mat& m, const Rect& roi);
    
    CV_WRAP void create(int rows, int cols, int type);
    CV_WRAP void release();
    CV_WRAP Mat clone() const;
    CV_WRAP void copyTo(CV_OUT Mat& dst) const;
    CV_WRAP void convertTo(CV_OUT Mat& dst, int rtype, double alpha = 1, double beta = 0) const;
    
    CV_WRAP int type() const;
    CV_WRAP int depth() const;
    CV_WRAP int channels() const;
    CV_WRAP size_t elemSize() const;
    CV_WRAP bool empty() const;
    CV_WRAP size_t total() const;
    CV_WRAP bool isContinuous() const;
    CV_WRAP bool isSubmatrix() const;
    
    CV_PROP int rows;
    CV_PROP int cols;
    CV_PROP uchar* data;
    CV_PROP size_t step;
};

} // namespace cv
