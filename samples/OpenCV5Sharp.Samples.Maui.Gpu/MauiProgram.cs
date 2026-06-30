// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Hosting;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples.Maui.Gpu
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }

    public class App : Application
    {
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }

    public class MainPage : ContentPage
    {
        private const string YoloUrl = "https://huggingface.co/Kalray/yolov8/resolve/main/yolov8n.onnx";
        private const string YoloFile = "yolov8n.onnx";
        private const string YunetUrl = "https://github.com/opencv/opencv_zoo/raw/main/models/face_detection_yunet/face_detection_yunet_2023mar.onnx";
        private const string YunetFile = "face_detection_yunet.onnx";

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

        private Image _imageControl;
        private Label _fpsLabel;
        private Label _timeLabel;
        private Label _statusLabel;
        private Picker _sourcePicker;
        private Picker _modelPicker;
        private Button _btnStart;
        private Button _btnLoadFile;
        private ActivityIndicator _loadingIndicator;

        private VideoCapture? _videoCapture;
        private CancellationTokenSource? _cts;
        private bool _isRunning = false;
        private string? _selectedFilePath;
        private string? _currentStaticPath;

        // DNN Networks
        private DnnNet? _yoloNet;
        private FaceDetectorYN? _yunetNet;

        private int _brightness = 0;
        private double _contrast = 1.0;
        private int _rotation = 0;

        public MainPage()
        {
            BackgroundColor = Color.FromArgb("#121212");
            Title = "OpenCV5Sharp GPU MAUI Dashboard";

            // Title Header
            var header = new Grid
            {
                BackgroundColor = Color.FromArgb("#1a1a1a"),
                Padding = new Thickness(20, 15),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto }
                }
            };
            header.Add(new Label
            {
                Text = "OpenCV5Sharp GPU Web/App Dashboard",
                FontAttributes = FontAttributes.Bold,
                FontSize = 20,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center
            }, 0, 0);
            header.Add(new Label
            {
                Text = "GPU CUDA Backend",
                TextColor = Color.FromArgb("#3b82f6"),
                FontAttributes = FontAttributes.Bold,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center
            }, 1, 0);

            // Left Control Panel
            var panelLayout = new VerticalStackLayout
            {
                Spacing = 15,
                Padding = 20,
                BackgroundColor = Color.FromArgb("#1e1e1e"),
                WidthRequest = 300
            };

            panelLayout.Children.Add(new Label { Text = "CONTROL PANEL", FontAttributes = FontAttributes.Bold, TextColor = Colors.White, FontSize = 14 });

            // Source Selector
            panelLayout.Children.Add(new Label { Text = "Input Source:", TextColor = Colors.Gray, FontSize = 12 });
            _sourcePicker = new Picker { Title = "Select Source", TextColor = Colors.White, TitleColor = Colors.Gray };
            _sourcePicker.Items.Add("Live Webcam");
            _sourcePicker.Items.Add("Video File");
            _sourcePicker.Items.Add("Static Image File");
            _sourcePicker.SelectedIndex = 0;
            _sourcePicker.SelectedIndexChanged += SourcePicker_SelectedIndexChanged;
            panelLayout.Children.Add(_sourcePicker);

            // Load File Button
            _btnLoadFile = new Button
            {
                Text = "Browse File",
                BackgroundColor = Color.FromArgb("#4b5563"),
                TextColor = Colors.White,
                IsVisible = false
            };
            _btnLoadFile.Clicked += BtnLoadFile_Clicked;
            panelLayout.Children.Add(_btnLoadFile);

            // Model Selector
            panelLayout.Children.Add(new Label { Text = "GPU DNN Model:", TextColor = Colors.Gray, FontSize = 12 });
            _modelPicker = new Picker { Title = "Select Model", TextColor = Colors.White, TitleColor = Colors.Gray };
            _modelPicker.Items.Add("Original (No AI)");
            _modelPicker.Items.Add("Grayscale");
            _modelPicker.Items.Add("Canny Edge Detection");
            _modelPicker.Items.Add("Gaussian Blur");
            _modelPicker.Items.Add("YOLOv8 Object Detector (GPU)");
            _modelPicker.Items.Add("YuNet Face Tracker (GPU)");
            _modelPicker.Items.Add("Hand & Finger Tracker");
            _modelPicker.SelectedIndex = 0;
            _modelPicker.SelectedIndexChanged += ModelPicker_SelectedIndexChanged;
            panelLayout.Children.Add(_modelPicker);

            // Loading Indicator
            _loadingIndicator = new ActivityIndicator
            {
                IsRunning = false,
                Color = Color.FromArgb("#3b82f6"),
                HeightRequest = 30
            };
            panelLayout.Children.Add(_loadingIndicator);

            // Brightness Slider
            panelLayout.Children.Add(new Label { Text = "Brightness:", TextColor = Colors.Gray, FontSize = 12 });
            var brightnessValLabel = new Label { Text = "Value: 0", TextColor = Colors.White, FontSize = 12 };
            var brightnessSlider = new Slider { Minimum = -100, Maximum = 100, Value = 0 };
            brightnessSlider.ValueChanged += (s, e) =>
            {
                _brightness = (int)e.NewValue;
                brightnessValLabel.Text = $"Value: {_brightness}";
                OnParameterChanged();
            };
            panelLayout.Children.Add(brightnessValLabel);
            panelLayout.Children.Add(brightnessSlider);

            // Contrast Slider
            panelLayout.Children.Add(new Label { Text = "Contrast:", TextColor = Colors.Gray, FontSize = 12 });
            var contrastValLabel = new Label { Text = "Value: 1.0x", TextColor = Colors.White, FontSize = 12 };
            var contrastSlider = new Slider { Minimum = 0.5, Maximum = 3.0, Value = 1.0 };
            contrastSlider.ValueChanged += (s, e) =>
            {
                _contrast = e.NewValue;
                contrastValLabel.Text = $"Value: {_contrast:F1}x";
                OnParameterChanged();
            };
            panelLayout.Children.Add(contrastValLabel);
            panelLayout.Children.Add(contrastSlider);

            // Rotation Slider
            panelLayout.Children.Add(new Label { Text = "Rotation / Skew:", TextColor = Colors.Gray, FontSize = 12 });
            var rotationValLabel = new Label { Text = "Value: 0°", TextColor = Colors.White, FontSize = 12 };
            var rotationSlider = new Slider { Minimum = -45, Maximum = 45, Value = 0 };
            rotationSlider.ValueChanged += (s, e) =>
            {
                _rotation = (int)e.NewValue;
                rotationValLabel.Text = $"Value: {_rotation}°";
                OnParameterChanged();
            };
            panelLayout.Children.Add(rotationValLabel);
            panelLayout.Children.Add(rotationSlider);

            // Action Button
            _btnStart = new Button
            {
                Text = "Start Feed",
                BackgroundColor = Color.FromArgb("#2563eb"),
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold
            };
            _btnStart.Clicked += BtnStart_Clicked;
            panelLayout.Children.Add(_btnStart);

            // Metrics Header
            panelLayout.Children.Add(new BoxView { Color = Color.FromArgb("#374151"), HeightRequest = 1 });
            panelLayout.Children.Add(new Label { Text = "PERFORMANCE METRICS", FontAttributes = FontAttributes.Bold, TextColor = Colors.White, FontSize = 14 });

            _fpsLabel = new Label { Text = "FPS: N/A", TextColor = Color.FromArgb("#3b82f6"), FontSize = 13 };
            _timeLabel = new Label { Text = "Inference: N/A", TextColor = Color.FromArgb("#10b981"), FontSize = 13 };
            _statusLabel = new Label { Text = "Backend: CUDA Active", TextColor = Colors.Gray, FontSize = 12 };
            panelLayout.Children.Add(_fpsLabel);
            panelLayout.Children.Add(_timeLabel);
            panelLayout.Children.Add(_statusLabel);

            // Main Viewport
            _imageControl = new Image
            {
                Aspect = Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Colors.Black
            };

            var imageContainer = new Border
            {
                Stroke = Color.FromArgb("#374151"),
                StrokeThickness = 1,
                Content = _imageControl,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = new Thickness(15),
                BackgroundColor = Colors.Black
            };

            // Main Page Layout
            var contentGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star }
                }
            };
            contentGrid.Add(panelLayout, 0, 0);
            contentGrid.Add(imageContainer, 1, 0);

            Content = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star }
                },
                Children =
                {
                    header,
                    contentGrid
                }
            };

            Grid.SetRow(header, 0);
            Grid.SetRow(contentGrid, 1);
        }

        private void SourcePicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            StopCapture();
            _selectedFilePath = null;
            _currentStaticPath = null;
            _btnLoadFile.IsVisible = (_sourcePicker.SelectedIndex == 1 || _sourcePicker.SelectedIndex == 2);
            _btnStart.IsVisible = (_sourcePicker.SelectedIndex != 2);
        }

        private async void BtnLoadFile_Clicked(object? sender, EventArgs e)
        {
            try
            {
                var options = new PickOptions
                {
                    PickerTitle = _sourcePicker.SelectedIndex == 1 ? "Select Video File" : "Select Image File"
                };
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    _selectedFilePath = result.FullPath;
                    if (_sourcePicker.SelectedIndex == 2) // Static Image
                    {
                        await ProcessStaticImageAsync(_selectedFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error Picking File", ex.Message, "OK");
            }
        }

        private async void ModelPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                StopCapture();
                await InitializeSelectedModelAsync();
                BtnStart_Clicked(this, EventArgs.Empty);
            }
            else
            {
                await InitializeSelectedModelAsync();
                OnParameterChanged();
            }
        }

        private void OnParameterChanged()
        {
            if (_sourcePicker.SelectedIndex == 2 && _currentStaticPath != null)
            {
                _ = ProcessStaticImageAsync(_currentStaticPath);
            }
        }

        private async Task InitializeSelectedModelAsync()
        {
            _loadingIndicator.IsRunning = true;
            _statusLabel.Text = "Loading Model to GPU...";

            try
            {
                string localDir = FileSystem.AppDataDirectory;
                string yoloPath = Path.Combine(localDir, YoloFile);
                string yunetPath = Path.Combine(localDir, YunetFile);

                if (_modelPicker.SelectedIndex == 4) // YOLOv8
                {
                    await DownloadModelIfMissingAsync(yoloPath, YoloUrl);
                    if (_yoloNet == null)
                    {
                        _yoloNet = Cv2.DnnReadNetFromONNX(yoloPath, (int)DnnEngineType.Classic)!;
                        if (Cv2.CudaGetCudaEnabledDeviceCount() > 0)
                        {
                            _yoloNet.SetPreferableBackend((int)DnnBackend.Cuda);
                            _yoloNet.SetPreferableTarget((int)DnnTarget.Cuda);
                        }
                    }
                }
                else if (_modelPicker.SelectedIndex == 5) // YuNet
                {
                    await DownloadModelIfMissingAsync(yunetPath, YunetUrl);
                    if (_yunetNet == null)
                    {
                        int backend = Cv2.CudaGetCudaEnabledDeviceCount() > 0 ? (int)DnnBackend.Cuda : 0;
                        int target = Cv2.CudaGetCudaEnabledDeviceCount() > 0 ? (int)DnnTarget.Cuda : 0;
                        _yunetNet = FaceDetectorYN.Create(yunetPath, "", new Size(320, 320), 0.6f, 0.3f, 5000, backend, target)!;
                    }
                }
                _statusLabel.Text = "GPU Model Ready";
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Model Error", $"Failed loading CUDA weights: {ex.Message}", "OK");
                _statusLabel.Text = "Model Error";
            }
            finally
            {
                _loadingIndicator.IsRunning = false;
            }
        }

        private async Task DownloadModelIfMissingAsync(string fullPath, string url)
        {
            if (File.Exists(fullPath)) return;

            _statusLabel.Text = $"Downloading weights...";
            using var client = new HttpClient();
            byte[] data = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(fullPath, data);
        }

        private async void BtnStart_Clicked(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                StopCapture();
            }
            else
            {
                if (_sourcePicker.SelectedIndex == 1) // Video File
                {
                    if (string.IsNullOrEmpty(_selectedFilePath))
                    {
                        await DisplayAlertAsync("No Video Selected", "Please browse for a video file first.", "OK");
                        return;
                    }
                    _ = StartCaptureLoopAsync(_selectedFilePath);
                }
                else // Live Webcam
                {
                    _ = StartCaptureLoopAsync(null);
                }
            }
        }

        private async Task StartCaptureLoopAsync(string? sourcePath)
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            _isRunning = true;
            _btnStart.Text = "Stop Feed";
            _btnStart.BackgroundColor = Color.FromArgb("#ef4444");

            try
            {
                _videoCapture = sourcePath != null ? new VideoCapture(sourcePath, 0) : new VideoCapture(0, 0);

                if (!_videoCapture.IsOpened())
                {
                    await DisplayAlertAsync("Source Error", "Could not open webcam or video source file.", "OK");
                    StopCapture();
                    return;
                }

                var sw = new Stopwatch();
                using var frame = new Mat();

                while (!token.IsCancellationRequested)
                {
                    sw.Restart();
                    bool hasFrame = _videoCapture.Read(frame);
                    if (!hasFrame || frame.Empty())
                    {
                        if (sourcePath != null)
                        {
                            _videoCapture.Set(1, 0); // Loop Pos Frame property
                            continue;
                        }
                        break;
                    }

                    using var processed = ApplyAIPipeline(frame);
                    byte[] jpegBytes = Cv2.Imencode(".jpg", processed);

                    sw.Stop();
                    long elapsed = sw.ElapsedMilliseconds;
                    double fps = elapsed > 0 ? 1000.0 / elapsed : 0.0;

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _imageControl.Source = ImageSource.FromStream(() => new MemoryStream(jpegBytes));
                        _fpsLabel.Text = $"FPS: {fps:F1}";
                        _timeLabel.Text = $"Inference: {elapsed} ms";
                    });

                    int sleepTime = Math.Max(1, 33 - (int)elapsed);
                    await Task.Delay(sleepTime, token);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in loop: {ex.Message}");
            }
            finally
            {
                StopCapture();
            }
        }

        private void StopCapture()
        {
            _cts?.Cancel();
            _cts = null;
            _videoCapture?.Dispose();
            _videoCapture = null;
            _isRunning = false;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _btnStart.Text = "Start Feed";
                _btnStart.BackgroundColor = Color.FromArgb("#2563eb");
            });
        }

        private async Task ProcessStaticImageAsync(string path)
        {
            try
            {
                _currentStaticPath = path;
                using var src = Cv2.Imread(path, 1);
                if (src == null || src.Empty()) return;

                using var processed = ApplyAIPipeline(src);
                byte[] jpegBytes = Cv2.Imencode(".jpg", processed);

                _imageControl.Source = ImageSource.FromStream(() => new MemoryStream(jpegBytes));
                _fpsLabel.Text = "FPS: N/A";
                _timeLabel.Text = "Inference: Static";
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Image Error", ex.Message, "OK");
            }
        }

        private Mat ApplyAIPipeline(Mat src)
        {
            var adjusted = new Mat();
            src.ConvertTo(adjusted, -1, _contrast, _brightness);

            var rotated = new Mat();
            if (_rotation != 0)
            {
                var center = new Point2F(adjusted.Cols / 2f, adjusted.Rows / 2f);
                using var rotationMatrix = Cv2.GetRotationMatrix2D(center, _rotation, 1.0);
                Cv2.WarpAffine(adjusted, rotated, rotationMatrix!, new Size(adjusted.Cols, adjusted.Rows), (int)InterpolationFlags.InterLinear, (int)BorderTypes.Constant, new Scalar(0, 0, 0), AlgorithmHint.Default);
            }
            else
            {
                adjusted.CopyTo(rotated);
            }
            adjusted.Dispose();

            int selectedModel = _modelPicker.SelectedIndex;

            if (selectedModel == 1) // Grayscale
            {
                var dst = new Mat();
                Cv2.CvtColor(rotated, dst, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 2) // Canny Edge
            {
                var dst = new Mat();
                using var gray = new Mat();
                Cv2.CvtColor(rotated, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                Cv2.Canny(gray, dst, 50, 150, 3, false);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 3) // Gaussian Blur
            {
                var dst = new Mat();
                Cv2.GaussianBlur(rotated, dst, new Size(15, 15), 0, 0, 4, AlgorithmHint.Default);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 4 && _yoloNet != null) // YOLOv8
            {
                var res = RunYoloInference(rotated);
                rotated.Dispose();
                return res;
            }
            else if (selectedModel == 5 && _yunetNet != null) // YuNet
            {
                var res = RunYuNetInference(rotated);
                rotated.Dispose();
                return res;
            }
            else if (selectedModel == 6) // Hand Tracker
            {
                var res = TrackHandAndFingers(rotated);
                rotated.Dispose();
                return res;
            }
            else
            {
                return rotated;
            }
        }

        private unsafe Mat RunYoloInference(Mat src)
        {
            var dst = new Mat();
            src.CopyTo(dst);

            using var blob = Cv2.DnnBlobFromImage(src, 1.0 / 255.0, new Size(640, 640), new Scalar(0, 0, 0), true, false, 5)!;
            _yoloNet!.SetInput(blob, "", 1.0, new Scalar(0, 0, 0));
            using var output = _yoloNet.Forward("")!;

            float* dataPtr = (float*)output.Data.ToPointer();

            var boxes = new List<Rect>();
            var confidences = new List<float>();
            var classIds = new List<int>();

            float x_ratio = (float)src.Cols / 640.0f;
            float y_ratio = (float)src.Rows / 640.0f;

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

            var indices = PerformNMS(boxes, confidences, 0.45f);

            foreach (int idx in indices)
            {
                var box = boxes[idx];
                string label = $"{YoloLabels[classIds[idx]]} {confidences[idx]:P0}";

                Cv2.Rectangle(dst, box, new Scalar(0, 255, 0), 2, 8, 0);
                Cv2.PutText(dst, label, new Point(box.X, box.Y - 5), (int)HersheyFonts.HersheySimplex, 0.5, new Scalar(255, 255, 255), 1, 8, false);
            }

            return dst;
        }

        private unsafe Mat RunYuNetInference(Mat src)
        {
            var dst = new Mat();
            src.CopyTo(dst);

            if (_yunetNet == null) return dst;

            _yunetNet.SetInputSize(new Size(src.Cols, src.Rows));
            using var faces = new Mat();
            _yunetNet.Detect(src, faces);

            int numFaces = faces.Rows;
            if (numFaces <= 0) return dst;

            int cols = faces.Cols; // 15 columns
            int totalFloats = numFaces * cols;
            float[] data = new float[totalFloats];
            Marshal.Copy(faces.Data, data, 0, totalFloats);

            for (int i = 0; i < numFaces; i++)
            {
                int offset = i * cols;
                float x = data[offset + 0];
                float y = data[offset + 1];
                float w = data[offset + 2];
                float h = data[offset + 3];
                float score = data[offset + 14];

                if (score > 0.5f)
                {
                    var box = new Rect((int)x, (int)y, (int)w, (int)h);
                    Cv2.Rectangle(dst, box, new Scalar(255, 0, 0), 2, 8, 0);

                    // Draw 5-Point Landmarks
                    for (int l = 0; l < 5; l++)
                    {
                        float lx = data[offset + 4 + l * 2];
                        float ly = data[offset + 4 + l * 2 + 1];
                        Cv2.Circle(dst, new Point((int)lx, (int)ly), 3, new Scalar(0, 255, 255), -1, 8, 0);
                    }

                    Cv2.PutText(dst, $"Face {score:P0}", new Point((int)x, (int)Math.Max(y - 5, 15)), (int)HersheyFonts.HersheySimplex, 0.5, new Scalar(255, 255, 255), 1, 8, false);
                }
            }

            return dst;
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

        private unsafe Mat TrackHandAndFingers(Mat src)
        {
            var dst = new Mat();
            src.CopyTo(dst);

            // Define ROI box in the center-right of the screen
            int roiWidth = 200;
            int roiHeight = 240;
            int roiX = Math.Max(src.Cols - roiWidth - 30, 0);
            int roiY = Math.Max((src.Rows - roiHeight) / 2, 0);

            // Draw ROI Box on the output frame
            var roiRect = new Rect(roiX, roiY, roiWidth, roiHeight);
            Cv2.Rectangle(dst, roiRect, new Scalar(255, 255, 255), 2, 8, 0);
            Cv2.PutText(dst, "Place Hand Inside Box", new Point(roiX, Math.Max(roiY - 10, 15)), (int)HersheyFonts.HersheySimplex, 0.5, new Scalar(255, 255, 255), 1, 8, false);

            using (var roiMat = new Mat(src, roiRect))
            using (var ycrcb = new Mat())
            {
                Cv2.CvtColor(roiMat, ycrcb, (int)ColorConversionCodes.BGR2YCrCb, 0, AlgorithmHint.Default);

                int rows = ycrcb.Rows;
                int cols = ycrcb.Cols;
                const int CV_8UC3 = 64;

                using (var lowerb = new Mat(rows, cols, CV_8UC3))
                using (var upperb = new Mat(rows, cols, CV_8UC3))
                using (var skinMask = new Mat())
                {
                    int pixelCount = rows * cols;
                    int size = pixelCount * 3;
                    byte[] lowData = new byte[size];
                    byte[] highData = new byte[size];
                    for (int j = 0; j < size; j += 3)
                    {
                        lowData[j] = 0;       // Y min
                        lowData[j + 1] = 133; // Cr min
                        lowData[j + 2] = 77;  // Cb min

                        highData[j] = 255;    // Y max
                        highData[j + 1] = 173; // Cr max
                        highData[j + 2] = 127; // Cb max
                    }
                    Marshal.Copy(lowData, 0, lowerb.Data, size);
                    Marshal.Copy(highData, 0, upperb.Data, size);

                    Cv2.InRange(ycrcb, lowerb, upperb, skinMask);

                    using (var element = Cv2.GetStructuringElement((int)MorphShapes.Ellipse, new Size(3, 3), new Point(-1, -1)))
                    using (var cleanMask = new Mat())
                    {
                        if (element != null)
                        {
                            Cv2.Erode(skinMask, cleanMask, element, new Point(-1, -1), 1, (int)BorderTypes.Constant, new Scalar(0));
                            Cv2.Dilate(cleanMask, skinMask, element, new Point(-1, -1), 2, (int)BorderTypes.Constant, new Scalar(0));
                        }

                        using (var m = Cv2.Moments(skinMask, true))
                        {
                            if (m == null || m.M00 < 800)
                            {
                                Cv2.PutText(dst, "No Hand Detected", new Point(roiX, roiY + roiHeight + 20), (int)HersheyFonts.HersheySimplex, 0.6, new Scalar(0, 0, 255), 2, 8, false);
                                return dst;
                            }

                            double cx = m.M10 / m.M00;
                            double cy = m.M01 / m.M00;

                            int width = skinMask.Cols;
                            int height = skinMask.Rows;
                            byte[] maskPixels = new byte[width * height];
                            Marshal.Copy(skinMask.Data, maskPixels, 0, maskPixels.Length);

                            int minX = width, maxX = 0, minY = height, maxY = 0;
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    if (maskPixels[y * width + x] == 255)
                                    {
                                        if (x < minX) minX = x;
                                        if (x > maxX) maxX = x;
                                        if (y < minY) minY = y;
                                        if (y > maxY) maxY = y;
                                    }
                                }
                            }

                            int sliceY = (int)(cy - (cy - minY) * 0.55);
                            int fingerCount = 0;
                            bool inFinger = false;
                            int fingerWidth = 0;

                            if (sliceY >= 0 && sliceY < height)
                            {
                                for (int x = minX; x <= maxX; x++)
                                {
                                    if (maskPixels[sliceY * width + x] == 255)
                                    {
                                        if (!inFinger)
                                        {
                                            inFinger = true;
                                            fingerWidth = 1;
                                        }
                                        else
                                        {
                                            fingerWidth++;
                                        }
                                    }
                                    else
                                    {
                                        if (inFinger)
                                        {
                                            inFinger = false;
                                            if (fingerWidth > 2 && fingerWidth < (maxX - minX) * 0.4)
                                            {
                                                fingerCount++;
                                            }
                                        }
                                    }
                                }
                                if (inFinger && fingerWidth > 2 && fingerWidth < (maxX - minX) * 0.4)
                                {
                                    fingerCount++;
                                }
                            }

                            // Offset coords by ROI origin
                            int absMinX = minX + roiX;
                            int absMaxX = maxX + roiX;
                            int absMinY = minY + roiY;
                            int absMaxY = maxY + roiY;
                            int absCx = (int)cx + roiX;
                            int absCy = (int)cy + roiY;
                            int absSliceY = sliceY + roiY;

                            Cv2.Rectangle(dst, new Rect(absMinX, absMinY, absMaxX - absMinX, absMaxY - absMinY), new Scalar(0, 255, 0), 2, 8, 0);
                            Cv2.Circle(dst, new Point(absCx, absCy), 8, new Scalar(0, 0, 255), -1, 8, 0);
                            if (sliceY >= 0 && sliceY < height)
                            {
                                Cv2.Line(dst, new Point(absMinX, absSliceY), new Point(absMaxX, absSliceY), new Scalar(255, 255, 0), 1, 8, 0);
                            }
                            Cv2.PutText(dst, $"Fingers: {fingerCount}", new Point(absMinX, Math.Max(absMinY - 10, 20)), (int)HersheyFonts.HersheySimplex, 0.7, new Scalar(0, 255, 255), 2, 8, false);
                        }
                    }
                }
            }

            return dst;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopCapture();
            _yoloNet?.Dispose();
            _yunetNet?.Dispose();
        }
    }
}
