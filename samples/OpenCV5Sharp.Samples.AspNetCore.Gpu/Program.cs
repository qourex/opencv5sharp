// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// Using OpenCV5Sharp is already imported above

namespace OpenCV5Sharp.Samples.AspNetCore.Gpu
{
    public class Program
    {
        private const string YoloUrl = "https://huggingface.co/Kalray/yolov8/resolve/main/yolov8n.onnx";
        private const string YoloFile = "yolov8n.onnx";

        static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Register singleton thread-serialized inference service
            builder.Services.AddSingleton<GpuInferenceService>();

            var app = builder.Build();

            app.MapGet("/", () => "OpenCV5Sharp GPU REST API. Use POST /api/detect with form key 'file' to run YOLOv8 object detection on GPU.");

            app.MapPost("/api/detect", async (HttpContext context, GpuInferenceService detector) =>
            {
                if (!context.Request.HasFormContentType)
                {
                    return Results.BadRequest("Invalid form content type.");
                }

                var form = await context.Request.ReadFormAsync();
                var file = form.Files.GetFile("file");
                if (file == null || file.Length == 0)
                {
                    return Results.BadRequest("No file uploaded. Please upload a file via key 'file'.");
                }

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();

                try
                {
                    var results = await detector.DetectObjectsAsync(fileBytes);
                    return Results.Json(results);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"GPU Inference failed: {ex.Message}");
                }
            });

            app.Run();
        }
    }

    public class DetectionResult
    {
        public string Label { get; set; } = "";
        public float Confidence { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class GpuInferenceService : IDisposable
    {
        private readonly DnnNet _net;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static readonly string[] YoloLabels = new string[]
        {
            "person", "bicycle", "car", "motorcycle", "airplane", "bus", "train", "truck", "boat", "traffic light",
            "fire hydrant", "stop sign", "parking meter", "bench", "bird", "cat", "dog", "horse", "sheep", "cow",
            "elephant", "bear", "zebra", "giraffe", "backpack", "umbrella", "handbag", "tie", "suitcase", "frisbee",
            "skis", "snowboard", "sports ball", "kite", "baseball bat", "baseball glove", "skateboard", "surfboard",
            "tennis racket", "bottle", "wine glass", "cup", "fork", "knife", "spoon", "bowl", "banana", "apple",
            "sandwich", "orange", "broccoli", "carrot", "hot dog", "pizza", "donut", "cake", "chair", "couch",
            "potted plant", "bed", "dining table", "toilet", "tv", "laptop", "mouse", "remote", "keyboard", "cell phone",
            "microwave", "oven", "toaster", "sink", "refrigerator", "book", "clock", "vase", "scissors", "teddy bear",
            "hair drier", "toothbrush"
        };

        public GpuInferenceService()
        {
            DownloadModelWeightsAsync("yolov8n.onnx", "https://huggingface.co/Kalray/yolov8/resolve/main/yolov8n.onnx").GetAwaiter().GetResult();

            Cv2.CudaSetDevice(0);
            _net = Cv2.DnnReadNetFromONNX("yolov8n.onnx", (int)DnnEngineType.Classic)!;
            _net.SetPreferableBackend((int)DnnBackend.Cuda);
            _net.SetPreferableTarget((int)DnnTarget.Cuda);
        }

        public async Task<List<DetectionResult>> DetectObjectsAsync(byte[] imageBytes)
        {
            // Thread serialization to prevent parallel native GPU clashes
            await _semaphore.WaitAsync();
            try
            {
                return await Task.Run(() =>
                {
                    using var src = Cv2.Imdecode(imageBytes, (int)ImreadModes.Color);
                    if (src == null || src.Empty()) throw new Exception("Could not decode image.");

                    // 1. Forward pass
                    using var blob = Cv2.DnnBlobFromImage(src, 1.0 / 255.0, new Size(640, 640), new Scalar(0, 0, 0), true, false, 5)!;
                    _net.SetInput(blob, "", 1.0, new Scalar(0, 0, 0));
                    
                    using var output = _net.Forward("")!;

                    // 2. Parse results via unmanaged float pointer (zero-copy)
                    var boxes = new List<Rect>();
                    var confidences = new List<float>();
                    var classIds = new List<int>();

                    float x_ratio = (float)src.Cols / 640.0f;
                    float y_ratio = (float)src.Rows / 640.0f;

                    unsafe
                    {
                        float* dataPtr = (float*)output.Data.ToPointer();

                        for (int col = 0; col < 8400; col++)
                        {
                            float bestConf = 0.0f;
                            int bestClassId = 0;

                            for (int c = 0; c < 80; c++)
                            {
                                float conf = dataPtr[(4 + c) * 8400 + col];
                                if (conf > bestConf)
                                {
                                    bestConf = conf;
                                    bestClassId = c;
                                }
                            }

                            if (bestConf > 0.45f)
                            {
                                float cx = dataPtr[0 * 8400 + col] * x_ratio;
                                float cy = dataPtr[1 * 8400 + col] * y_ratio;
                                float w = dataPtr[2 * 8400 + col] * x_ratio;
                                float h = dataPtr[3 * 8400 + col] * y_ratio;

                                int rx = (int)(cx - w / 2.0f);
                                int ry = (int)(cy - h / 2.0f);

                                boxes.Add(new Rect(rx, ry, (int)w, (int)h));
                                confidences.Add(bestConf);
                                classIds.Add(bestClassId);
                            }
                        }
                    }

                    var indices = PerformNMS(boxes, confidences, 0.45f);
                    var results = new List<DetectionResult>();

                    foreach (int idx in indices)
                    {
                        var box = boxes[idx];
                        results.Add(new DetectionResult
                        {
                            Label = YoloLabels[classIds[idx]],
                            Confidence = confidences[idx],
                            X = box.X,
                            Y = box.Y,
                            Width = box.Width,
                            Height = box.Height
                        });
                    }

                    return results;
                });
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private List<int> PerformNMS(List<Rect> boxes, List<float> confidences, float nmsThreshold)
        {
            var indices = new List<int>();
            var sortedIndices = new List<int>();
            for (int i = 0; i < boxes.Count; i++) sortedIndices.Add(i);

            sortedIndices.Sort((a, b) => confidences[b].CompareTo(confidences[a]));

            while (sortedIndices.Count > 0)
            {
                int current = sortedIndices[0];
                indices.Add(current);
                sortedIndices.RemoveAt(0);

                for (int i = sortedIndices.Count - 1; i >= 0; i--)
                {
                    int candidate = sortedIndices[i];
                    if (IntersectionOverUnion(boxes[current], boxes[candidate]) > nmsThreshold)
                    {
                        sortedIndices.RemoveAt(i);
                    }
                }
            }

            return indices;
        }

        private float IntersectionOverUnion(Rect boxA, Rect boxB)
        {
            int xA = Math.Max(boxA.X, boxB.X);
            int yA = Math.Max(boxA.Y, boxB.Y);
            int xB = Math.Min(boxA.X + boxA.Width, boxB.X + boxB.Width);
            int yB = Math.Min(boxA.Y + boxA.Height, boxB.Y + boxB.Height);

            int interArea = Math.Max(0, xB - xA) * Math.Max(0, yB - yA);
            if (interArea == 0) return 0.0f;

            int boxAArea = boxA.Width * boxA.Height;
            int boxBArea = boxB.Width * boxB.Height;

            return (float)interArea / (boxAArea + boxBArea - interArea);
        }

        private async Task DownloadModelWeightsAsync(string localPath, string url)
        {
            if (File.Exists(localPath)) return;
            using var client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(localPath, bytes);
        }

        public void Dispose()
        {
            _net.Dispose();
        }
    }
}
