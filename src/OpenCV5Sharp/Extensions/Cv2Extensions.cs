// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace OpenCV5Sharp
{
    public static partial class Cv2
    {
        /// <summary>
        /// Decodes an image from a managed byte array.
        /// </summary>
        /// <param name="bytes">The byte array containing the encoded image data (e.g., PNG, JPEG).</param>
        /// <param name="flags">Read flags specifying the color type. Use <see cref="ImreadModes"/> values cast to <see cref="int"/>.</param>
        /// <returns>A new <see cref="Mat"/> containing the decoded image.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="bytes"/> is <c>null</c>.</exception>
        public static Mat Imdecode(byte[] bytes, int flags)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            using var mat = new Mat(1, bytes.Length, 0); // CV_8UC1 = 0
            Marshal.Copy(bytes, 0, mat.Data, bytes.Length);
            return Imdecode(mat, flags)!;
        }

        /// <summary>
        /// Encodes an image into a managed byte array using a temporary file.
        /// </summary>
        /// <param name="ext">The file extension specifying the encoding format (e.g., ".png", ".jpg").</param>
        /// <param name="img">The <see cref="Mat"/> image to encode.</param>
        /// <returns>A byte array containing the encoded image data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="ext"/> or <paramref name="img"/> is <c>null</c>.</exception>
        /// <exception cref="OpenCVException">Thrown when the native writing operation fails.</exception>
        /// <exception cref="System.IO.IOException">Thrown when file read/write issues occur.</exception>
        public static byte[] Imencode(string ext, Mat img)
        {
            if (ext == null) throw new ArgumentNullException(nameof(ext));
            if (img == null) throw new ArgumentNullException(nameof(img));
            string tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ext);
            try
            {
                Imwrite(tempFile, img, IntPtr.Zero);
                return System.IO.File.ReadAllBytes(tempFile);
            }
            finally
            {
                if (System.IO.File.Exists(tempFile))
                {
                    System.IO.File.Delete(tempFile);
                }
            }
        }
    }
}
