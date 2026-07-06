// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Base class for all OpenCV wrapper objects that own unmanaged resources.
    /// Provides thread-safe disposal via <see cref="Interlocked.Exchange(ref IntPtr, IntPtr)"/>
    /// to prevent double-free race conditions in multi-threaded scenarios.
    /// </summary>
    public abstract class DisposableOpenCVObject : IDisposable
    {
        static DisposableOpenCVObject()
        {
            PlatformGuard.EnsureSupported();
        }

        private readonly OpenCVHandle _handle;

        /// <summary>Gets the type-safe SafeHandle to the underlying OpenCV object.</summary>
        /// <value>The unmanaged object's SafeHandle.</value>
        public OpenCVHandle Handle => _handle;

        /// <summary>Gets a value indicating whether this object has been disposed.</summary>
        /// <value><c>true</c> if the object is disposed or the native handle is invalid; otherwise, <c>false</c>.</value>
        public bool IsDisposed => _handle.IsClosed || _handle.IsInvalid;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableOpenCVObject"/> class
        /// with the specified unmanaged SafeHandle.
        /// </summary>
        /// <param name="handle">The unmanaged SafeHandle to the native OpenCV object.</param>
        /// <exception cref="OpenCVException">Thrown when the handle is null or invalid.</exception>
        protected DisposableOpenCVObject(OpenCVHandle handle)
        {
            if (handle == null || handle.IsInvalid)
            {
                ErrorHelper.CheckError();
                throw new OpenCVException("Failed to allocate or retrieve native OpenCV object (received null or invalid handle).");
            }
            _handle = handle;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/> if this object has been disposed.
        /// Call this at the beginning of every public method and property accessor.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        public void ThrowIfDisposed()
        {
            if (_handle.IsClosed || _handle.IsInvalid)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>Releases all resources used by this object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">true if called from Dispose; false if called from finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _handle.Dispose();
            }
        }
    }

    /// <summary>
    /// Helper class to perform argument validation and fetch unmanaged handles.
    /// </summary>
    internal static class ValidationHelper
    {
        /// <summary>
        /// Gets the native pointer handle from a disposable OpenCV object, validating that it is not null (if not optional) and not disposed.
        /// </summary>
        /// <param name="obj">The disposable OpenCV object.</param>
        /// <param name="paramName">The parameter name to report in exceptions.</param>
        /// <param name="isOptional">True if the parameter can be null; false if it is required.</param>
        /// <returns>The unmanaged pointer handle of the object, or <see cref="IntPtr.Zero"/> if null and optional.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is null and <paramref name="isOptional"/> is false.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="obj"/> has been disposed.</exception>
        public static IntPtr GetHandle(DisposableOpenCVObject? obj, string paramName, bool isOptional)
        {
            if (obj is null)
            {
                if (isOptional) return IntPtr.Zero;
                throw new ArgumentNullException(paramName);
            }
            var safeHandle = obj.Handle;
            if (safeHandle == null || safeHandle.IsInvalid)
            {
                throw new ObjectDisposedException(obj.GetType().Name);
            }
            return safeHandle.DangerousGetHandle();
        }

        /// <summary>
        /// Gets the strongly-typed OpenCVHandle from a disposable OpenCV object, validating that it is not null (if not optional) and not disposed.
        /// </summary>
        /// <typeparam name="T">The specific OpenCVHandle subclass.</typeparam>
        /// <param name="obj">The disposable OpenCV object.</param>
        /// <param name="paramName">The parameter name to report in exceptions.</param>
        /// <param name="isOptional">True if the parameter can be null; false if it is required.</param>
        /// <returns>The strongly-typed unmanaged handle of the object, or null if null and optional.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is null and <paramref name="isOptional"/> is false.</exception>
        /// <exception cref="ObjectDisposedException">Thrown when <paramref name="obj"/> has been disposed.</exception>
        public static T GetHandle<T>(DisposableOpenCVObject? obj, string paramName, bool isOptional) where T : OpenCVHandle
        {
            if (obj is null)
            {
                if (isOptional) return null!;
                throw new ArgumentNullException(paramName);
            }
            var safeHandle = obj.Handle;
            if (safeHandle == null || safeHandle.IsInvalid)
            {
                throw new ObjectDisposedException(obj.GetType().Name);
            }
            return (T)safeHandle;
        }

        /// <summary>Reinterprets a ulong as an unmanaged struct value (e.g. Size2F, Point2F, Size, Point).</summary>
        /// <typeparam name="T">The unmanaged struct type to reinterpret to.</typeparam>
        /// <param name="val">The raw ulong value to reinterpret.</param>
        /// <returns>The reinterpreted struct of type <typeparamref name="T"/>.</returns>
        public static unsafe T Reinterpret<T>(ulong val) where T : unmanaged
        {
            if (sizeof(T) > sizeof(ulong))
            {
                throw new ArgumentException($"Cannot reinterpret ulong as type {typeof(T).Name} because its size ({sizeof(T)} bytes) exceeds the size of a ulong ({sizeof(ulong)} bytes).");
            }
            return *(T*)&val;
        }
    }

    /// <summary>
    /// Helper class to perform dimension validation for Mat construction.
    /// </summary>
    internal static class MatValidation
    {
        /// <summary>
        /// Validates matrix dimensions and invokes a factory to create the native handle.
        /// Throws if dimensions are invalid; otherwise returns the result of <paramref name="factory"/>.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        /// <param name="factory">The factory function generating the handle.</param>
        /// <returns>The unmanaged pointer handle of the newly created Mat.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="rows"/> or <paramref name="cols"/> is negative.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix size exceeds limits.</exception>
        public static IntPtr CheckDimensions(int rows, int cols, Func<IntPtr> factory)
        {
            if (rows < 0) throw new ArgumentOutOfRangeException(nameof(rows));
            if (cols < 0) throw new ArgumentOutOfRangeException(nameof(cols));
            if ((long)rows * cols > 1_073_741_824) throw new ArgumentException("Mat dimensions exceed maximum safe size");
            return factory();
        }

        /// <summary>
        /// Validates matrix size and invokes a factory to create the native handle.
        /// </summary>
        /// <param name="size">The size of the matrix.</param>
        /// <param name="factory">The factory function generating the handle.</param>
        /// <returns>The unmanaged pointer handle of the newly created Mat.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when size elements are negative.</exception>
        /// <exception cref="ArgumentException">Thrown when matrix size exceeds limits.</exception>
        public static IntPtr CheckSize(Size size, Func<IntPtr> factory)
        {
            return CheckDimensions(size.Height, size.Width, factory);
        }
    }
}
