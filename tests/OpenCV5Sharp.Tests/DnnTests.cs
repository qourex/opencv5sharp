// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Net.Http;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class DnnTests
    {
        private string? FindModelFile(string filename)
        {
            string[] candidates = {
                filename,
                Path.Combine("..", filename),
                Path.Combine("..", "..", filename),
                Path.Combine("..", "..", "..", filename),
                Path.Combine("..", "..", "..", "..", filename),
                Path.Combine("..", "..", "..", "..", "..", filename)
            };
            foreach (var path in candidates)
            {
                if (File.Exists(path)) return Path.GetFullPath(path);
            }
            return null;
        }

        private string GetOrDownloadModel(string filename, string url)
        {
            string? path = FindModelFile(filename);
            if (path != null) return path;

            try
            {
                using (var client = new HttpClient())
                {
                    // Set a timeout of 60 seconds
                    client.Timeout = TimeSpan.FromSeconds(60);
                    var data = client.GetByteArrayAsync(url).Result;
                    File.WriteAllBytes(filename, data);
                    return Path.GetFullPath(filename);
                }
            }
            catch (Exception ex)
            {
                // Fallback: if network fails, try to return filename directly in case it exists but was not found
                Console.WriteLine($"Error downloading {filename}: {ex.Message}");
                return Path.GetFullPath(filename);
            }
        }

        [Fact]
        public void SqueezeNet_Inference_Success()
        {
            string url = "https://github.com/onnx/models/raw/main/validated/vision/classification/squeezenet/model/squeezenet1.1-7.onnx";
            string modelPath = GetOrDownloadModel("squeezenet.onnx", url);

            using (DnnNet? net = Cv2.DnnReadNetFromONNX(modelPath, 1))
            {
                Assert.NotNull(net);
                Assert.NotEqual(IntPtr.Zero, net!.Handle);

                // Create dummy BGR image (224x224, 3 channels)
                const int CV_8UC3 = 64;
                using (Mat img = new Mat(224, 224, CV_8UC3))
                {
                    // Convert to blob
                    using (Mat? blob = Cv2.DnnBlobFromImage(
                        img,
                        1.0 / 255.0,
                        new Size(224, 224),
                        new Scalar(123.675, 116.28, 103.53, 0),
                        true,
                        false,
                        5))
                    {
                        Assert.NotNull(blob);
                        Assert.NotEqual(IntPtr.Zero, blob!.Handle);

                        // Set input and forward
                        net.SetInput(blob, "", 1.0, new Scalar(0, 0, 0, 0));
                        using (Mat? prob = net.Forward(""))
                        {
                            Assert.NotNull(prob);
                            Assert.NotEqual(IntPtr.Zero, prob!.Handle);

                            // The output shape should be 1 x 1000
                            int rows = prob.Rows > prob.Cols ? prob.Rows : prob.Cols;
                            Assert.True(rows > 0);
                        }
                    }
                }
            }
        }

        [Fact]
        public void YuNet_FaceDetector_Success()
        {
            string url = "https://github.com/opencv/opencv_zoo/raw/main/models/face_detection_yunet/face_detection_yunet_2023mar.onnx";
            string modelPath = GetOrDownloadModel("face_detection_yunet.onnx", url);

            using (var detector = FaceDetectorYN.Create(modelPath, "", new Size(320, 240), 0.6f, 0.3f, 5000, 0, 0))
            {
                Assert.NotNull(detector);
                Assert.NotEqual(IntPtr.Zero, detector!.Handle);

                // Check that setters/getters work
                detector.SetInputSize(new Size(640, 480));
                detector.SetScoreThreshold(0.7f);
                Assert.Equal(0.7f, detector.GetScoreThreshold());
            }
        }
    }
}
