// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Exception thrown when an underlying OpenCV native operation fails.
    /// This exception propagates error information from the native C++ layer
    /// through the P/Invoke boundary to managed code.
    /// </summary>
    public class OpenCVException : Exception
    {
        /// <summary>
        /// Gets the OpenCV-specific error code.
        /// A value of -1 indicates a generic cv::Exception.
        /// A value of -2 indicates a std::exception.
        /// A value of -3 indicates an unknown native exception.
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenCVException"/> class
        /// with a specified error message and error code.
        /// </summary>
        /// <param name="message">The error message from the native layer.</param>
        /// <param name="errorCode">The OpenCV error code. Defaults to -1.</param>
        public OpenCVException(string message, int errorCode = -1)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenCVException"/> class
        /// with a specified error message, error code, and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="errorCode">The OpenCV error code.</param>
        /// <param name="innerException">The inner exception.</param>
        public OpenCVException(string message, int errorCode, Exception innerException)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>Returns a string representation including the error code.</summary>
        public override string ToString()
        {
            return $"OpenCVException (Code: {ErrorCode}): {Message}{(InnerException != null ? $"\n---> {InnerException}" : "")}{(StackTrace != null ? $"\n{StackTrace}" : "")}";
        }
    }

    /// <summary>
    /// Internal helper class to check native OpenCV error codes and throw managed exceptions.
    /// </summary>
    internal static class ErrorHelper
    {
        /// <summary>
        /// Queries the native thread-local error state and throws an OpenCVException if a native error is registered.
        /// </summary>
        /// <exception cref="OpenCVException">Thrown when a native error state is detected.</exception>
        public static void CheckError()
        {
            int code = NativeMethods.opencv5sharp_getLastErrorCode();
            if (code != 0)
            {
                IntPtr errPtr = NativeMethods.opencv5sharp_getLastError();
                string message = "Unknown native error";
                if (errPtr != IntPtr.Zero)
                {
                    message = System.Runtime.InteropServices.Marshal.PtrToStringUTF8(errPtr) ?? "Unknown native error";
                }
                NativeMethods.opencv5sharp_clearLastError();
                throw new OpenCVException(message, code);
            }
        }
    }
}
