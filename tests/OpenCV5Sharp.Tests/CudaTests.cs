// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using OpenCV5Sharp;
using Xunit;

namespace OpenCV5Sharp.Tests
{
    public class CudaTests
    {
        [Fact]
        [Trait("Category", "CUDA")]
        public void TestGetCudaDeviceCount()
        {
            // This call should run cleanly on all configurations (returning 0 or >= 0)
            int count = Cv2.CudaGetCudaEnabledDeviceCount();
            Assert.True(count >= 0);
        }

        [SkippableFact]
        [Trait("Category", "CUDA")]
        public void TestCudaDeviceInfo()
        {
            int count = Cv2.CudaGetCudaEnabledDeviceCount();
            Skip.If(count == 0, "No CUDA enabled devices found — skipping test");

            using (var info = new CudaDeviceInfo(0))
            {
                Assert.NotNull(info);
                Assert.NotEqual(IntPtr.Zero, info!.Handle);
                
                int id = info.DeviceID();
                Assert.Equal(0, id);

                long mem = info.TotalGlobalMem();
                Assert.True(mem > 0);
            }
        }

        [SkippableFact]
        [Trait("Category", "CUDA")]
        public void TestCudaGpuMatUploadDownload()
        {
            int count = Cv2.CudaGetCudaEnabledDeviceCount();
            Skip.If(count == 0, "No CUDA enabled devices found — skipping test");

            const int CV_8UC3 = 64;
            using (var cpuSrc = new Mat(100, 100, CV_8UC3))
            using (var gpuMat = new CudaGpuMat(100, 100, CV_8UC3, CudaGpuMat.DefaultAllocator()))
            using (var cpuDst = new Mat())
            {
                // Write pattern to cpuSrc
                int totalBytes = (int)(cpuSrc.Rows * cpuSrc.Step);
                byte[] fillBuffer = new byte[totalBytes];
                for (int i = 0; i < fillBuffer.Length; i++)
                {
                    fillBuffer[i] = (byte)(i % 256);
                }
                Marshal.Copy(fillBuffer, 0, cpuSrc.Data, fillBuffer.Length);

                // Upload
                gpuMat.Upload(cpuSrc);

                // Download
                gpuMat.Download(cpuDst);

                // Verify
                Assert.Equal(cpuSrc.Rows, cpuDst.Rows);
                Assert.Equal(cpuSrc.Cols, cpuDst.Cols);
                Assert.Equal(cpuSrc.Channels(), cpuDst.Channels());

                byte[] readBuffer = new byte[totalBytes];
                Marshal.Copy(cpuDst.Data, readBuffer, 0, readBuffer.Length);

                for (int i = 0; i < totalBytes; i++)
                {
                    Assert.Equal(fillBuffer[i], readBuffer[i]);
                }
            }
        }

        [SkippableFact]
        [Trait("Category", "CUDA")]
        public void TestCudaStreamAsyncUploadDownload()
        {
            int count = Cv2.CudaGetCudaEnabledDeviceCount();
            Skip.If(count == 0, "No CUDA enabled devices found — skipping test");

            const int CV_8UC3 = 64;
            using (var cpuSrc = new Mat(100, 100, CV_8UC3))
            using (var gpuMat = new CudaGpuMat(100, 100, CV_8UC3, CudaGpuMat.DefaultAllocator()))
            using (var cpuDst = new Mat())
            using (var stream = new CudaStream())
            {
                int totalBytes = (int)(cpuSrc.Rows * cpuSrc.Step);
                byte[] fillBuffer = new byte[totalBytes];
                for (int i = 0; i < fillBuffer.Length; i++)
                {
                    fillBuffer[i] = (byte)(i % 256);
                }
                Marshal.Copy(fillBuffer, 0, cpuSrc.Data, fillBuffer.Length);

                // Upload async
                gpuMat.Upload(cpuSrc, stream);

                // Download async
                gpuMat.Download(cpuDst, stream);

                // Wait
                stream.WaitForCompletion();

                // Verify
                Assert.Equal(cpuSrc.Rows, cpuDst.Rows);
                Assert.Equal(cpuSrc.Cols, cpuDst.Cols);

                byte[] readBuffer = new byte[totalBytes];
                Marshal.Copy(cpuDst.Data, readBuffer, 0, readBuffer.Length);

                for (int i = 0; i < totalBytes; i++)
                {
                    Assert.Equal(fillBuffer[i], readBuffer[i]);
                }
            }
        }
    }
}
