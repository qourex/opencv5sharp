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

        private IntPtr _handle;

        /// <summary>Gets the native handle to the underlying OpenCV object.</summary>
        /// <value>A pointer to the unmanaged object, or <see cref="IntPtr.Zero"/> if disposed.</value>
        public IntPtr Handle => _handle;

        /// <summary>Gets a value indicating whether this object has been disposed.</summary>
        public bool IsDisposed => _handle == IntPtr.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableOpenCVObject"/> class
        /// with the specified native handle.
        /// </summary>
        /// <param name="handle">The native handle to the unmanaged OpenCV object.</param>
        protected DisposableOpenCVObject(IntPtr handle)
        {
            _handle = handle;
        }

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/> if this object has been disposed.
        /// Call this at the beginning of every public method and property accessor.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when the object has been disposed.</exception>
        public void ThrowIfDisposed()
        {
            if (_handle == IntPtr.Zero)
                throw new ObjectDisposedException(GetType().Name);
        }

        /// <summary>Releases all resources used by this object.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources. Uses <see cref="Interlocked.Exchange(ref IntPtr, IntPtr)"/>
        /// to atomically swap the handle to <see cref="IntPtr.Zero"/>, preventing double-free.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> if called from <see cref="Dispose()"/>; <c>false</c> if called from the finalizer.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            IntPtr h = Interlocked.Exchange(ref _handle, IntPtr.Zero);
            if (h != IntPtr.Zero)
            {
                DisposeUnmanaged(h);
            }
        }

        /// <summary>
        /// When overridden in a derived class, releases the native resource identified by <paramref name="handle"/>.
        /// The handle is guaranteed to be non-zero and this method is called exactly once.
        /// </summary>
        /// <param name="handle">The native handle to release. Guaranteed non-zero.</param>
        protected abstract void DisposeUnmanaged(IntPtr handle);

        /// <summary>Finalizer. Releases unmanaged resources if Dispose was not called.</summary>
        ~DisposableOpenCVObject() { Dispose(false); }
    }

    /// <summary>
    /// Helper class to perform argument validation and fetch unmanaged handles.
    /// </summary>
    internal static class ValidationHelper
    {
        public static IntPtr GetHandle(DisposableOpenCVObject? obj, string paramName, bool isOptional)
        {
            if (obj is null)
            {
                if (isOptional) return IntPtr.Zero;
                throw new ArgumentNullException(paramName);
            }
            obj.ThrowIfDisposed();
            return obj.Handle;
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
        public static IntPtr CheckSize(Size size, Func<IntPtr> factory)
        {
            return CheckDimensions(size.Height, size.Width, factory);
        }
    }
}
