// Copyright (c) 2026 Qourex. Licensed under the Apache 2.0 and LGPL 2.1 Licenses.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples.WinForms.Gpu
{
    public class MainForm : Form
    {
        private const string YoloUrl = "https://huggingface.co/Kalray/yolov8/resolve/main/yolov8n.onnx";
        private const string YoloFile = "yolov8n.onnx";
        private const string YunetUrl = "https://github.com/opencv/opencv_zoo/raw/main/models/face_detection_yunet/face_detection_yunet_2023mar.onnx";
        private const string YunetFile = "face_detection_yunet.onnx";

        private ComboBox _sourceCombo = null!;
        private ComboBox _modelCombo = null!;
        private Button _actionBtn = null!;
        private PictureBox _videoBox = null!;
        private Label _statusLabel = null!;

        private VideoCapture? _capture;
        private CancellationTokenSource? _cts;
        private bool _isRunning;
        private string? _selectedFilePath;

        // DNN Networks
        private DnnNet? _yoloNet;
        private FaceDetectorYN? _yunetNet;

        private string? _currentStaticPath;
        private TrackBar _brightnessBar = null!;
        private TrackBar _contrastBar = null!;
        private TrackBar _rotationBar = null!;
        private Label _brightnessLabel = null!;
        private Label _contrastLabel = null!;
        private Label _rotationLabel = null!;

        // Diagnostics
        private static int _activeMatCount = 0;
        private static int _activeGpuMatCount = 0;

        // YOLOv8 class labels
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

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "OpenCV5Sharp - GPU Windows Forms Sample";
            this.Size = new System.Drawing.Size(1100, 750);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(24, 24, 24);
            this.ForeColor = Color.FromArgb(230, 230, 230);

            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(10)
            };
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F)); // Top Controls
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // View
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F)); // Status Bar

            // 1. TOP BAR: Input Controls
            var topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var sourceLabel = new Label
            {
                Text = "Source:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 10, 5, 0)
            };

            _sourceCombo = new ComboBox
            {
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };
            _sourceCombo.Items.AddRange(new object[] { "Live Webcam", "Video File...", "Image File..." });
            _sourceCombo.SelectedIndex = 0;
            _sourceCombo.SelectedIndexChanged += SourceCombo_SelectedIndexChanged;

            var modelLabel = new Label
            {
                Text = "AI Model:",
                AutoSize = true,
                Anchor = AnchorStyles.Left,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(15, 10, 5, 0)
            };

            _modelCombo = new ComboBox
            {
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White
            };
            _modelCombo.Items.AddRange(new object[] {
                "Original (No AI)",
                "Grayscale",
                "Canny Edge",
                "Gaussian Blur",
                "YOLOv8 Object Detector (GPU)",
                "YuNet Face Tracker (GPU)",
                "Hand & Finger Tracker"
            });
            _modelCombo.SelectedIndex = 0;
            _modelCombo.SelectedIndexChanged += ModelCombo_SelectedIndexChanged;

            _actionBtn = new Button
            {
                Text = "Start Video ▶",
                Width = 130,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(15, 3, 0, 0)
            };
            _actionBtn.Click += ActionBtn_Click;

            topPanel.Controls.Add(sourceLabel);
            topPanel.Controls.Add(_sourceCombo);
            topPanel.Controls.Add(modelLabel);
            topPanel.Controls.Add(_modelCombo);
            topPanel.Controls.Add(_actionBtn);
            mainLayout.Controls.Add(topPanel, 0, 0);

            // 2. VIDEO DISPLAY BOX
            _videoBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };
            mainLayout.Controls.Add(_videoBox, 0, 1);

            // 3. RIGHT PANEL: Diagnostics
            var rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(32, 32, 32)
            };

            var diagLabel = new Label
            {
                Text = "GPU Telemetry & Logs",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };

            var infoBox = new Label
            {
                Text = "Active CUDA SDK: OpenCV 5\nInference Target: GPU\nModel Format: ONNX\nNMS Threshold: 0.45",
                Width = 200,
                Height = 100,
                ForeColor = Color.FromArgb(170, 170, 170),
                Font = new Font("Consolas", 9.5F)
            };

            // Brightness TrackBar
            _brightnessLabel = new Label { Text = "Brightness: 0", ForeColor = Color.White, Margin = new Padding(0, 15, 0, 2), AutoSize = true };
            _brightnessBar = new TrackBar { Minimum = -100, Maximum = 100, Value = 0, Width = 180, TickFrequency = 20, Margin = new Padding(0, 0, 0, 5) };
            _brightnessBar.Scroll += (s, e) => { _brightnessLabel.Text = $"Brightness: {_brightnessBar.Value}"; TriggerStaticRefresh(); };

            // Contrast TrackBar
            _contrastLabel = new Label { Text = "Contrast: 1.0x", ForeColor = Color.White, Margin = new Padding(0, 10, 0, 2), AutoSize = true };
            _contrastBar = new TrackBar { Minimum = 5, Maximum = 30, Value = 10, Width = 180, TickFrequency = 5, Margin = new Padding(0, 0, 0, 5) };
            _contrastBar.Scroll += (s, e) => { _contrastLabel.Text = $"Contrast: {(_contrastBar.Value / 10.0):F1}x"; TriggerStaticRefresh(); };

            // Rotation TrackBar
            _rotationLabel = new Label { Text = "Rotation / Skew: 0°", ForeColor = Color.White, Margin = new Padding(0, 10, 0, 2), AutoSize = true };
            _rotationBar = new TrackBar { Minimum = -45, Maximum = 45, Value = 0, Width = 180, TickFrequency = 15, Margin = new Padding(0, 0, 0, 5) };
            _rotationBar.Scroll += (s, e) => { _rotationLabel.Text = $"Rotation / Skew: {_rotationBar.Value}°"; TriggerStaticRefresh(); };

            rightPanel.Controls.Add(diagLabel);
            rightPanel.Controls.Add(infoBox);
            rightPanel.Controls.Add(_brightnessLabel);
            rightPanel.Controls.Add(_brightnessBar);
            rightPanel.Controls.Add(_contrastLabel);
            rightPanel.Controls.Add(_contrastBar);
            rightPanel.Controls.Add(_rotationLabel);
            rightPanel.Controls.Add(_rotationBar);
            mainLayout.Controls.Add(rightPanel, 1, 1);

            // 4. BOTTOM BAR: Telemetry Status
            _statusLabel = new Label
            {
                Text = "Status: Idle | Native Mats: 0 | Native GpuMats: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Italic)
            };
            mainLayout.Controls.Add(_statusLabel, 0, 2);

            this.Controls.Add(mainLayout);
        }

        private void SourceCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            StopProcessing();
            _actionBtn.Text = _sourceCombo.SelectedIndex == 2 ? "Process Image 🖼" : "Start Video ▶";
        }

        private async void ActionBtn_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                StopProcessing();
                return;
            }

            int selectedSource = _sourceCombo.SelectedIndex;

            _statusLabel.Text = "Checking model files...";
            _actionBtn.Enabled = false;

            try
            {
                await InitializeSelectedModelAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load AI models: {ex.Message}", "Model Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _actionBtn.Enabled = true;
                return;
            }

            _actionBtn.Enabled = true;

            if (selectedSource == 0) // Webcam
            {
                _actionBtn.Text = "Stop Video 🛑";
                _isRunning = true;
                _sourceCombo.Enabled = false;
                _modelCombo.Enabled = false;
                _cts = new CancellationTokenSource();
                await StartCaptureLoopAsync(null, _cts.Token);
            }
            else if (selectedSource == 1) // Video File
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Video Files (*.mp4;*.avi;*.mov)|*.mp4;*.avi;*.mov";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        _selectedFilePath = ofd.FileName;
                        _actionBtn.Text = "Stop Video 🛑";
                        _isRunning = true;
                        _sourceCombo.Enabled = false;
                        _modelCombo.Enabled = false;
                        _cts = new CancellationTokenSource();
                        await StartCaptureLoopAsync(_selectedFilePath, _cts.Token);
                    }
                }
            }
            else if (selectedSource == 2) // Image File
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files (*.jpg;*.png;*.jpeg;*.bmp)|*.jpg;*.png;*.jpeg;*.bmp";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        ProcessStaticImage(ofd.FileName);
                    }
                }
            }
        }

        private async Task StartCaptureLoopAsync(string? videoPath, CancellationToken token)
        {
            try
            {
                _statusLabel.Text = "Opening media capture...";
                _capture = videoPath != null ? new VideoCapture(videoPath, 0) : new VideoCapture(0, 0);

                if (!_capture.IsOpened())
                {
                    MessageBox.Show("Could not initialize video capture device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StopProcessing();
                    return;
                }

                double fps = videoPath != null ? _capture.Get((int)VideoCaptureProperties.Fps) : 30;
                if (fps <= 0) fps = 30;
                int delayMs = (int)(1000 / fps);

                var sw = new Stopwatch();

                while (!token.IsCancellationRequested)
                {
                    sw.Restart();

                    var frame = TrackMat(new Mat());
                    if (!_capture.Read(frame) || frame.Empty())
                    {
                        UntrackMat(frame);
                        if (videoPath != null) break;
                        await Task.Delay(10, token);
                        continue;
                    }

                    // Apply AI pipeline
                    var processed = ApplyAIPipeline(frame);

                    // Render
                    var bmp = MatToBitmap(processed);
                    UntrackMat(processed);

                    this.BeginInvoke(new Action(() =>
                    {
                        var old = _videoBox.Image;
                        _videoBox.Image = bmp;
                        old?.Dispose();
                    }));

                    sw.Stop();
                    long elapsed = sw.ElapsedMilliseconds;
                    double currentFps = 1000.0 / Math.Max(elapsed, 1);

                    _statusLabel.Text = $"Status: Running | Size: {frame.Cols}x{frame.Rows} | Latency: {elapsed} ms ({currentFps:F1} FPS) | Active Mats: {_activeMatCount} | Active GpuMats: {_activeGpuMatCount}";
                    UntrackMat(frame);

                    int sleepTime = Math.Max(1, delayMs - (int)elapsed);
                    await Task.Delay(sleepTime, token);
                }
            }
            catch (Exception ex)
            {
                _statusLabel.Text = $"Error: {ex.Message}";
            }
            finally
            {
                StopProcessing();
            }
        }

        private async Task InitializeSelectedModelAsync()
        {
            if (_modelCombo.SelectedIndex == 4) // YOLOv8
            {
                await DownloadModelIfMissingAsync(YoloFile, YoloUrl);
                if (_yoloNet == null)
                {
                    _yoloNet = Cv2.DnnReadNetFromONNX(YoloFile, (int)DnnEngineType.Classic)!;
                    if (Cv2.CudaGetCudaEnabledDeviceCount() > 0)
                    {
                        _yoloNet.SetPreferableBackend((int)DnnBackend.Cuda);
                        _yoloNet.SetPreferableTarget((int)DnnTarget.Cuda);
                    }
                }
            }
            else if (_modelCombo.SelectedIndex == 5) // YuNet
            {
                // Copy YuNet model from root if exists, otherwise download
                if (!File.Exists(YunetFile) && File.Exists("../../face_detection_yunet.onnx"))
                {
                    File.Copy("../../face_detection_yunet.onnx", YunetFile);
                }
                await DownloadModelIfMissingAsync(YunetFile, YunetUrl);
                if (_yunetNet == null)
                {
                    int backend = Cv2.CudaGetCudaEnabledDeviceCount() > 0 ? (int)DnnBackend.Cuda : 0;
                    int target = Cv2.CudaGetCudaEnabledDeviceCount() > 0 ? (int)DnnTarget.Cuda : 0;
                    _yunetNet = FaceDetectorYN.Create(YunetFile, "", new Size(320, 320), 0.6f, 0.3f, 5000, backend, target)!;
                }
            }
        }

        private async void ModelCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                StopProcessing();
                await InitializeSelectedModelAsync();
                _isRunning = true;
                _sourceCombo.Enabled = false;
                _cts = new CancellationTokenSource();
                if (_sourceCombo.SelectedIndex == 1)
                    await StartCaptureLoopAsync(_selectedFilePath, _cts.Token);
                else
                    await StartCaptureLoopAsync(null, _cts.Token);
            }
            else
            {
                await InitializeSelectedModelAsync();
                TriggerStaticRefresh();
            }
        }

        private void ProcessStaticImage(string path)
        {
            try
            {
                _currentStaticPath = path;
                var src = Cv2.Imread(path, 1);
                if (src == null || src.Empty())
                {
                    if (src != null) src.Dispose();
                    return;
                }
                TrackMat(src);

                var processed = ApplyAIPipeline(src);
                UntrackMat(src);

                var bmp = MatToBitmap(processed);
                UntrackMat(processed);

                var old = _videoBox.Image;
                _videoBox.Image = bmp;
                old?.Dispose();

                _statusLabel.Text = $"Static Processed | Active Mats: {_activeMatCount} | Active GpuMats: {_activeGpuMatCount}";
            }
            catch (Exception ex)
            {
                _statusLabel.Text = $"Error: {ex.Message}";
            }
        }

        private void TriggerStaticRefresh()
        {
            if (_sourceCombo.SelectedIndex == 2 && _currentStaticPath != null)
            {
                ProcessStaticImage(_currentStaticPath);
            }
        }

        private Mat ApplyAIPipeline(Mat src)
        {
            var adjusted = TrackMat(new Mat());
            double contrast = _contrastBar.Value / 10.0;
            double brightness = _brightnessBar.Value;
            src.ConvertTo(adjusted, -1, contrast, brightness);

            var rotated = TrackMat(new Mat());
            double angle = _rotationBar.Value;
            if (angle != 0)
            {
                var center = new Point2F(adjusted.Cols / 2f, adjusted.Rows / 2f);
                using var rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, 1.0);
                Cv2.WarpAffine(adjusted, rotated, rotationMatrix!, new OpenCV5Sharp.Size(adjusted.Cols, adjusted.Rows), (int)InterpolationFlags.InterLinear, (int)BorderTypes.Constant, new Scalar(0, 0, 0), AlgorithmHint.Default);
            }
            else
            {
                adjusted.CopyTo(rotated);
            }
            UntrackMat(adjusted);
            adjusted.Dispose();

            int selectedModel = _modelCombo.SelectedIndex;

            if (selectedModel == 1) // Grayscale
            {
                var dst = TrackMat(new Mat());
                Cv2.CvtColor(rotated, dst, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                UntrackMat(rotated);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 2) // Canny Edge
            {
                var dst = TrackMat(new Mat());
                using var gray = new Mat();
                Cv2.CvtColor(rotated, gray, (int)ColorConversionCodes.Bgr2gray, 0, AlgorithmHint.Default);
                Cv2.Canny(gray, dst, 50, 150, 3, false);
                UntrackMat(rotated);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 3) // Gaussian Blur
            {
                var dst = TrackMat(new Mat());
                Cv2.GaussianBlur(rotated, dst, new OpenCV5Sharp.Size(15, 15), 0, 0, 4, AlgorithmHint.Default);
                UntrackMat(rotated);
                rotated.Dispose();
                return dst;
            }
            else if (selectedModel == 4 && _yoloNet != null) // YOLOv8
            {
                var res = RunYoloInference(rotated);
                UntrackMat(rotated);
                rotated.Dispose();
                return res;
            }
            else if (selectedModel == 5 && _yunetNet != null) // YuNet Face Landmarks
            {
                var res = RunYuNetInference(rotated);
                UntrackMat(rotated);
                rotated.Dispose();
                return res;
            }
            else if (selectedModel == 6) // Hand Tracker
            {
                var res = TrackHandAndFingers(rotated);
                UntrackMat(rotated);
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
            var dst = TrackMat(new Mat());
            src.CopyTo(dst);

            // YOLOv8 requires input blob: 640x640, scale 1.0/255.0, swapRB true
            using var blob = Cv2.DnnBlobFromImage(src, 1.0 / 255.0, new Size(640, 640), new Scalar(0, 0, 0), true, false, 5)!;
            _yoloNet!.SetInput(blob, "", 1.0, new Scalar(0, 0, 0));

            using var output = _yoloNet.Forward("")!;

            // Output has shape [1, 84, 8400]
            // We read the floats directly using pointer logic (zero-copy parsing)
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

                // Find class with highest confidence
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

            // Custom NMS to avoid complex low-level vector pointer interop
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
            var dst = TrackMat(new Mat());
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

        private unsafe Mat TrackHandAndFingers(Mat src)
        {
            var dst = TrackMat(new Mat());
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

        private List<int> PerformNMS(List<Rect> boxes, List<float> confidences, float nmsThreshold)
        {
            var indices = new List<int>();
            var sortedIndices = new List<int>();
            for (int i = 0; i < boxes.Count; i++) sortedIndices.Add(i);

            // Sort by confidence descending
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

        private Bitmap MatToBitmap(Mat mat)
        {
            using var temp = new Mat();
            if (mat.Channels() == 1)
            {
                Cv2.CvtColor(mat, temp, (int)ColorConversionCodes.Gray2bgr, 0, AlgorithmHint.Default);
            }
            else
            {
                mat.CopyTo(temp);
            }

            using var bmp = new Bitmap(temp.Cols, temp.Rows, (int)temp.Step, System.Drawing.Imaging.PixelFormat.Format24bppRgb, temp.Data);
            var deepCopy = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(deepCopy))
            {
                g.DrawImage(bmp, 0, 0);
            }
            return deepCopy;
        }

        private static Mat TrackMat(Mat mat)
        {
            Interlocked.Increment(ref _activeMatCount);
            return mat;
        }

        private static void UntrackMat(Mat mat)
        {
            mat.Dispose();
            Interlocked.Decrement(ref _activeMatCount);
        }

        private static CudaGpuMat TrackGpuMat(CudaGpuMat mat)
        {
            Interlocked.Increment(ref _activeGpuMatCount);
            return mat;
        }

        private static void UntrackGpuMat(CudaGpuMat mat)
        {
            mat.Dispose();
            Interlocked.Decrement(ref _activeGpuMatCount);
        }

        private async Task DownloadModelIfMissingAsync(string localPath, string url)
        {
            if (File.Exists(localPath)) return;
            _statusLabel.Text = $"Downloading model weights ({localPath})...";
            using var client = new HttpClient();
            var bytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(localPath, bytes);
        }

        private void StopProcessing()
        {
            _cts?.Cancel();
            _isRunning = false;
            _sourceCombo.Enabled = true;
            _modelCombo.Enabled = true;
            _actionBtn.Text = _sourceCombo.SelectedIndex == 2 ? "Process Image 🖼" : "Start Video ▶";
            _statusLabel.Text = "Status: Stopped";

            _capture?.Dispose();
            _capture = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopProcessing();
            _yoloNet?.Dispose();
            _yunetNet?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
