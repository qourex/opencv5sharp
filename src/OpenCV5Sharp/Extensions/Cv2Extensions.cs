// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace OpenCV5Sharp
{
    public static partial class Cv2
    {
        private static readonly Regex ExtensionRegex = new Regex(@"^\.[a-zA-Z0-9]+$", RegexOptions.Compiled);

        static Cv2()
        {
            PlatformGuard.EnsureSupported();
        }

        /// <summary>
        /// Decodes an image from a managed byte array.
        /// </summary>
        /// <param name="bytes">The byte array containing the encoded image data (e.g., PNG, JPEG).</param>
        /// <param name="flags">Read flags specifying the color type. Use <see cref="ImreadModes"/> values cast to <see cref="int"/>.</param>
        /// <returns>A new <see cref="Mat"/> containing the decoded image.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when allocation of unmanaged memory for the temporary Mat fails.</exception>
        /// <exception cref="OpenCVException">Thrown when decoding fails or the byte array is invalid.</exception>
        public static Mat Imdecode(byte[] bytes, int flags)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0) return new Mat();
            
            using var mat = new Mat(1, bytes.Length, 0); // CV_8UC1 = 0
            IntPtr dataPtr = mat.Data;
            if (dataPtr == IntPtr.Zero)
            {
                throw new InvalidOperationException("Failed to allocate unmanaged memory for Mat.");
            }
            Marshal.Copy(bytes, 0, dataPtr, bytes.Length);
            
            var result = Imdecode(mat, flags);
            if (result == null || result.IsDisposed || result.Handle.IsInvalid || result.Data == IntPtr.Zero)
            {
                result?.Dispose();
                throw new OpenCVException("Failed to decode image from the provided byte array.");
            }
            return result;
        }

        /// <summary>
        /// Encodes an image into a managed byte array using a temporary file.
        /// </summary>
        /// <param name="ext">The file extension specifying the encoding format (e.g., ".png", ".jpg").</param>
        /// <param name="img">The <see cref="Mat"/> image to encode.</param>
        /// <returns>A byte array containing the encoded image data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ext"/> or <paramref name="img"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when the file extension is invalid.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when the <paramref name="img"/> matrix has been disposed.</exception>
        /// <exception cref="OpenCVException">Thrown when the native writing operation fails.</exception>
        /// <exception cref="System.IO.IOException">Thrown when file read/write issues occur.</exception>
        public static byte[] Imencode(string ext, Mat img)
        {
            if (ext == null) throw new ArgumentNullException(nameof(ext));
            if (img == null) throw new ArgumentNullException(nameof(img));
            
            // Validate extension to prevent path traversal (VULN-10)
            if (!ExtensionRegex.IsMatch(ext))
            {
                throw new ArgumentException("Invalid file extension format. Expected format like '.png' or '.jpg'.", nameof(ext));
            }
            
            img.ThrowIfDisposed();
            
            string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString("N") + ext);
            try
            {
                if (!Imwrite(tempFile, img, IntPtr.Zero))
                {
                    throw new OpenCVException("Failed to write image to temporary file for encoding.");
                }
                for (int retry = 0; retry < 5; retry++)
                {
                    try
                    {
                        return System.IO.File.ReadAllBytes(tempFile);
                    }
                    catch (System.IO.IOException) when (retry < 4)
                    {
                        System.Threading.Thread.Sleep(10 * (retry + 1));
                    }
                }
                return System.IO.File.ReadAllBytes(tempFile);
            }
            finally
            {
                if (System.IO.File.Exists(tempFile))
                {
                    try
                    {
                        System.IO.File.Delete(tempFile);
                    }
                    catch
                    {
                        // Suppress cleanup failures to prevent leaking exceptions from finally block
                    }
                }
            }
        }
    }
}
