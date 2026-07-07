// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using Microsoft.Win32.SafeHandles;

namespace OpenCV5Sharp
{
    /// <summary>
    /// Base class for all type-safe unmanaged resource handles in OpenCV5Sharp.
    /// Inherits from <see cref="SafeHandleZeroOrMinusOneIsInvalid"/> to leverage standard
    /// .NET P/Invoke reference counting and prevent use-after-free race conditions.
    /// </summary>
    public abstract class OpenCVHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenCVHandle"/> class.
        /// </summary>
        /// <param name="ownsHandle">True to reliably release the handle during the finalization phase; otherwise, false.</param>
        protected OpenCVHandle(bool ownsHandle) : base(ownsHandle) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenCVHandle"/> class wrapping the raw pointer.
        /// </summary>
        /// <param name="ptr">The raw unmanaged handle address.</param>
        /// <param name="ownsHandle">True to reliably release the handle during the finalization phase; otherwise, false.</param>
        protected OpenCVHandle(IntPtr ptr, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(ptr);
        }

        /// <summary>
        /// Gets the raw unmanaged handle address.
        /// </summary>
        public IntPtr Address => handle;

        /// <summary>
        /// Implicitly converts an <see cref="OpenCVHandle"/> to its raw unmanaged pointer.
        /// <para><b>WARNING:</b> This operator uses <see cref="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle"/>
        /// without incrementing the reference count. The returned <see cref="IntPtr"/> may become
        /// invalid if the owning object is garbage collected or disposed. Prefer passing the
        /// <see cref="OpenCVHandle"/> directly to P/Invoke methods, which handle ref-counting
        /// automatically. Only use this operator when you are certain the handle will be kept
        /// alive by a <c>GC.KeepAlive</c> call or <c>using</c> scope.</para>
        /// </summary>
        /// <param name="h">The handle to convert.</param>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static implicit operator IntPtr(OpenCVHandle? h)
        {
            return h?.DangerousGetHandle() ?? IntPtr.Zero;
        }
    }
}
