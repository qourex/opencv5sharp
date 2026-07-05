// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace OpenCV5Sharp
{
    public partial class Mat
    {
        private GCHandle _pinnedDataHandle;
        private int _pinnedState = 0; // 0 = unpinned/free, 1 = pinned

        /// <summary>
        /// Initializes a new instance of the <see cref="Mat"/> class wrapping a managed array.
        /// Pins the managed array to prevent the Garbage Collector from relocating it while in use by OpenCV.
        /// </summary>
        /// <param name="rows">Number of rows.</param>
        /// <param name="cols">Number of columns.</param>
        /// <param name="type">Matrix type (depth and channels).</param>
        /// <param name="data">The managed data array to pin.</param>
        /// <param name="step">Number of bytes each matrix row occupies.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="rows"/> or <paramref name="cols"/> is negative.</exception>
        /// <exception cref="ArgumentException">Thrown when the matrix dimensions exceed the maximum safe size.</exception>
        /// <exception cref="OpenCVException">Thrown when the native matrix allocation fails or a native error is detected.</exception>
        public Mat(int rows, int cols, int type, Array data, long step)
            : this(AllocateAndPin(rows, cols, type, data, step, out GCHandle handle))
        {
            _pinnedDataHandle = handle;
            _pinnedState = 1;
            ErrorHelper.CheckError();
        }

        private static IntPtr AllocateAndPin(int rows, int cols, int type, Array data, long step, out GCHandle handle)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                IntPtr addr = handle.AddrOfPinnedObject();
                IntPtr matHandle = MatValidation.CheckDimensions(rows, cols, () => NativeMethods.Mat_New_2(rows, cols, type, addr, step));
                if (matHandle == IntPtr.Zero)
                {
                    throw new OpenCVException("Failed to allocate native Mat from managed array.");
                }
                return matHandle;
            }
            catch
            {
                handle.Free();
                throw;
            }
        }

        /// <summary>
        /// Releases resources and unpins the managed memory if pinned.
        /// </summary>
        /// <param name="handle">The native handle to release.</param>
        protected override void DisposeUnmanaged(IntPtr handle)
        {
            try
            {
                NativeMethods.Mat_Delete(handle);
            }
            finally
            {
                if (Interlocked.CompareExchange(ref _pinnedState, 0, 1) == 1)
                {
                    if (_pinnedDataHandle.IsAllocated)
                    {
                        _pinnedDataHandle.Free();
                    }
                }
            }
        }
    }
}
