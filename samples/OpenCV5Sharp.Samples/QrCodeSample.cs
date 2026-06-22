// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.IO;
using OpenCV5Sharp;

namespace OpenCV5Sharp.Samples
{
    public static class QrCodeSample
    {
        public static void Run()
        {
            Console.WriteLine("--- [5] QR Code Encoding & Decoding ---");

            string qrText = "https://github.com/qourex/opencv5sharp";
            string qrImagePath = "sample_qrcode.png";

            Console.WriteLine($"\n1. Encoding text to QR Code: \"{qrText}\"");

            using (var parameters = new QRCodeEncoderParams())
            using (var encoder = QRCodeEncoder.Create(parameters.Handle))
            {
                if (encoder == null)
                {
                    Console.WriteLine("   [ERROR] Failed to create QRCodeEncoder.");
                    return;
                }

                using (var qrMat = new Mat())
                {
                    encoder.Encode(qrText, qrMat);
                    Console.WriteLine($"   QR Code matrix generated. Size: {qrMat.Cols}x{qrMat.Rows}, Channels: {qrMat.Channels()}");

                    // Scale up the QR code image by 10x using Nearest Neighbor interpolation so it is readable
                    int scale = 10;
                    int newWidth = qrMat.Cols * scale;
                    int newHeight = qrMat.Rows * scale;
                    using (var scaledQr = new Mat())
                    {
                        Cv2.Resize(qrMat, scaledQr, new Size(newWidth, newHeight), 0, 0, (int)InterpolationFlags.InterNearest);
                        Cv2.Imwrite(qrImagePath, scaledQr, IntPtr.Zero);
                    }
                    Console.WriteLine($"   Saved upscaled QR Code image to: {qrImagePath}");
                }
            }

            Console.WriteLine("\n2. Loading and decoding the QR Code from disk...");
            using (var qrImage = Cv2.Imread(qrImagePath, (int)ImreadModes.Color))
            {
                if (qrImage.Handle == IntPtr.Zero)
                {
                    Console.WriteLine("   [ERROR] Failed to load QR Code image.");
                    return;
                }

                using (var detector = new QRCodeDetector())
                using (var points = new Mat())
                using (var straight = new Mat())
                {
                    Console.WriteLine("   Running QRCodeDetector.DetectAndDecode...");
                    string? decoded = detector.DetectAndDecode(qrImage, points, straight);

                    Console.WriteLine($"   Decoded Text: \"{decoded}\"");
                    if (decoded == qrText)
                    {
                        Console.WriteLine("   [SUCCESS] Decoded text matches the original!");
                    }
                    else
                    {
                        Console.WriteLine("   [FAILED] Decoded text does not match!");
                    }

                    if (points.Cols > 0 && points.Rows > 0)
                    {
                        Console.WriteLine($"   Detected corners matrix size: {points.Cols}x{points.Rows}");
                    }
                }
            }

            // Cleanup
            try
            {
                if (File.Exists(qrImagePath))
                {
                    File.Delete(qrImagePath);
                    Console.WriteLine("\nTemporary QR Code image file cleaned up.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[WARNING] Failed to delete temporary file: {ex.Message}");
            }

            Console.WriteLine("\nQR Code sample completed.");
        }
    }
}
