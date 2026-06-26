# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# OpenCV5Sharp Build Automation Script
# Combines native C++ CMake compilation and .NET NuGet packaging.
# Supports CPU-only and GPU/CUDA-enabled package builds.
#
# Usage:
#   .\build.ps1              # Build both CPU and GPU packages (default, skips GPU if CUDA is missing)
#   .\build.ps1 -CpuOnly     # Build only the CPU package
#   .\build.ps1 -GpuOnly     # Build only the GPU package (fails if CUDA is missing)

param(
    [switch]$CpuOnly,
    [switch]$GpuOnly
)

$ErrorActionPreference = "Stop"

# Determine what to build
$buildCpu = $true
$buildGpu = $true

if ($CpuOnly -and $GpuOnly) {
    throw "Cannot specify both -CpuOnly and -GpuOnly"
} elseif ($CpuOnly) {
    $buildGpu = $false
} elseif ($GpuOnly) {
    $buildCpu = $false
}

Write-Host "==================================================" -ForegroundColor Cyan
Write-Host "         Building OpenCV5Sharp                     " -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan

# 1. Paths Setup
$RootDir = Get-Location
$NativeDir = Join-Path $RootDir "src\OpenCV5Sharp.Native"
$BuildCpuDir = Join-Path $NativeDir "build_cpu_win"
$BuildGpuDir = Join-Path $NativeDir "build_gpu_win"
$CSharpCpuDir = Join-Path $RootDir "src\OpenCV5Sharp"
$CSharpGpuDir = Join-Path $RootDir "src\OpenCV5Sharp.Gpu"

# Ensure CMake from VS is in the PATH so it is always available
$VsCMakeDir = "C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin"
if (-not (Test-Path $VsCMakeDir)) {
    $VsCMakeDir = "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin"
}
if (-not (Test-Path $VsCMakeDir)) {
    $VsCMakeDir = "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin"
}
if (-not (Test-Path $VsCMakeDir)) {
    $VsCMakeDir = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\CMake\CMake\bin"
}
if (Test-Path $VsCMakeDir) {
    $env:PATH = "$VsCMakeDir;$env:PATH"
}

# Resolve VS Developer Command Prompt for compilation tools
$VsDevCmd = "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\Tools\VsDevCmd.bat"
if (-not (Test-Path $VsDevCmd)) {
    $VsDevCmd = "C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\Tools\VsDevCmd.bat"
}
if (-not (Test-Path $VsDevCmd)) {
    $VsDevCmd = "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
}
if (-not (Test-Path $VsDevCmd)) {
    $VsDevCmd = "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\Tools\VsDevCmd.bat"
}
if (-not (Test-Path $VsDevCmd)) {
    $VsDevCmd = "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\Tools\VsDevCmd.bat"
}

# Resolve Ninja executable for fast parallel CUDA compilation
$NinjaExe = "ninja.exe"
$NinjaPaths = @(
    "C:\Program Files\Microsoft Visual Studio\18\Enterprise\Common7\IDE\CommonExtensions\Microsoft\CMake\Ninja\ninja.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\CommonExtensions\Microsoft\CMake\Ninja\ninja.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\CommonExtensions\Microsoft\CMake\Ninja\ninja.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\CommonExtensions\Microsoft\CMake\Ninja\ninja.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\Common7\IDE\CommonExtensions\Microsoft\CMake\Ninja\ninja.exe"
)
$NinjaFound = $false
foreach ($path in $NinjaPaths) {
    if (Test-Path $path) {
        $NinjaExe = $path
        $NinjaDir = Split-Path $path
        $env:PATH = "$NinjaDir;$env:PATH"
        $NinjaFound = $true
        break
    }
}

# CUDA detection helper
function Check-Cuda-Available {
    if ($null -ne $env:CUDA_PATH) { return $true }
    if (Get-Command nvcc -ErrorAction SilentlyContinue) { return $true }
    if (Test-Path "C:\Program Files\NVIDIA GPU Computing Toolkit\CUDA") { return $true }
    return $false
}

# Native build and DLL staging helper
function Build-Native-And-Stage($buildDir, $cudaEnabled, $csharpProjectDir) {
    # Resolve OpenCV path
    $opencvPath = $env:OpenCV_DIR
    if (-not $opencvPath) {
        if ($cudaEnabled) {
            $opencvPath = Join-Path $RootDir "build_opencv_cuda_win\install"
            if (-not (Test-Path $opencvPath)) {
                # OpenCV CUDA install does not exist, let's clone and compile it from source!
                $contribDir = Join-Path $RootDir "opencv_contrib"
                if (-not (Test-Path $contribDir)) {
                    Write-Host "Cloning opencv_contrib (5.x branch)..." -ForegroundColor Yellow
                    & git clone --branch 5.x --depth 1 https://github.com/opencv/opencv_contrib.git $contribDir
                    if ($LASTEXITCODE -ne 0) { throw "Failed to clone opencv_contrib" }
                    
                    # Apply Windows MSVC LLP64 longlong/ulonglong vec_traits fix to modules/cudev
                    $vecTraitsPath = Join-Path $contribDir "modules\cudev\include\opencv2\cudev\util\vec_traits.hpp"
                    if (Test-Path $vecTraitsPath) {
                        Write-Host "Applying Windows LLP64 vec_traits compilation fix..." -ForegroundColor Green
                        $content = Get-Content $vecTraitsPath -Raw
                        
                        # Add typedefs
                        if ($content -notmatch "typedef long long longlong;") {
                            $target = '#include "opencv2/core/cuda/cuda_compat.hpp"'
                            $replacement = "$target`r`n`r`n#if defined(_WIN32)`r`ntypedef unsigned long ulong;`r`ntypedef long long longlong;`r`ntypedef unsigned long long ulonglong;`r`n#endif"
                            $content = $content.Replace($target, $replacement)
                        }
                        
                        # Add MAKE_VEC_INST
                        if ($content -notmatch "CV_CUDEV_MAKE_VEC_INST\(longlong\)") {
                            $target = "CV_CUDEV_MAKE_VEC_INST(ulong)"
                            $replacement = "$target`r`n#if defined(_WIN32)`r`nCV_CUDEV_MAKE_VEC_INST(longlong)`r`nCV_CUDEV_MAKE_VEC_INST(ulonglong)`r`n#endif"
                            $content = $content.Replace($target, $replacement)
                        }
                        
                        # Add VEC_TRAITS_INST
                        if ($content -notmatch "CV_CUDEV_VEC_TRAITS_INST\(longlong\)") {
                            $target = "CV_CUDEV_VEC_TRAITS_INST(ulong)"
                            $replacement = "$target`r`n#if defined(_WIN32)`r`nCV_CUDEV_VEC_TRAITS_INST(longlong)`r`nCV_CUDEV_VEC_TRAITS_INST(ulonglong)`r`n#endif"
                            $content = $content.Replace($target, $replacement)
                        }
                        
                        Set-Content $vecTraitsPath -Value $content -NoNewline
                    }
                }
                
                $opencvBuildDir = Join-Path $RootDir "build_opencv_cuda_win"
                Write-Host "`n[CUDA Build] Configuring and compiling OpenCV 5 with CUDA from source (all kernels)..." -ForegroundColor Yellow
                if (Test-Path $opencvBuildDir) {
                    $buildNinja = Join-Path $opencvBuildDir "build.ninja"
                    $cacheFile = Join-Path $opencvBuildDir "CMakeCache.txt"
                    $needsClean = $false
                    
                    if ($NinjaFound -and -not (Test-Path $buildNinja)) {
                        Write-Host "Non-Ninja generator detected." -ForegroundColor Yellow
                        $needsClean = $true
                    }
                    
                    if (Test-Path $cacheFile) {
                        $cacheContent = Get-Content $cacheFile -Raw
                        $normalizedRoot = $RootDir.ToString().Replace('\', '/')
                        if ($cacheContent -notmatch [regex]::Escape($normalizedRoot)) {
                            Write-Host "Detected path mismatch in CMake cache (moved folder). Wiping build directory for a clean configure..." -ForegroundColor Yellow
                            $needsClean = $true
                        }
                    }
                    
                    if ($needsClean) {
                        Write-Host "Cleaning existing build directory: $opencvBuildDir"
                        Remove-Item -Path $opencvBuildDir -Recurse -Force -ErrorAction SilentlyContinue
                        New-Item -ItemType Directory -Path $opencvBuildDir | Out-Null
                    }
                } else {
                    New-Item -ItemType Directory -Path $opencvBuildDir | Out-Null
                }
                
                Push-Location $opencvBuildDir
                try {
                    $generatorFlags = "-G `"Visual Studio 17 2022`" -A x64"
                    if ($NinjaFound) {
                        Write-Host "Using Ninja generator for parallel file compilation..." -ForegroundColor Green
                        $generatorFlags = "-G `"Ninja`" -DCMAKE_MAKE_PROGRAM=`"$NinjaExe`""
                    }
                    $cmakeCmd = "cmake `"$RootDir\opencv`" $generatorFlags -DCMAKE_BUILD_TYPE=Release -DCMAKE_INSTALL_PREFIX=`"install`" -DBUILD_SHARED_LIBS=ON -DBUILD_opencv_world=ON -DWITH_CUDA=ON -DWITH_CUDNN=ON -DCUDA_FAST_MATH=ON -DOPENCV_DNN_CUDA=ON -DCUDA_ARCH_BIN=`"5.0 5.2 6.0 6.1 7.0 7.5 8.0 8.6 8.9 9.0 10.0 10.1`" -DCUDA_ARCH_PTX=`"10.0 10.1`" -DCMAKE_CUDA_FLAGS=`"--threads 0`" -DOPENCV_EXTRA_MODULES_PATH=`"$contribDir\modules`" -DBUILD_TESTS=OFF -DBUILD_PERF_TESTS=OFF -DBUILD_EXAMPLES=OFF -DBUILD_DOCS=OFF"
                    cmd.exe /c "call `"$VsDevCmd`" -arch=x64 && $cmakeCmd"
                    if ($LASTEXITCODE -ne 0) { throw "OpenCV CMake configuration failed" }

                    cmd.exe /c "call `"$VsDevCmd`" -arch=x64 && cmake --build . --config Release --target install --parallel"
                    if ($LASTEXITCODE -ne 0) { throw "OpenCV CUDA compilation failed" }
                } finally {
                    Pop-Location
                }
                $opencvPath = Join-Path $opencvBuildDir "install"
            }
        } else {
            $opencvPath = Join-Path $RootDir "opencv_prebuilt\opencv\build"
        }
    }
    $opencvPath = Resolve-Path $opencvPath

    if ($cudaEnabled) {
        Write-Host "`nConfiguring and building native C++ wrapper (CUDA) linking to: $opencvPath" -ForegroundColor Green
    } else {
        Write-Host "`nConfiguring and building native C++ wrapper (CPU) linking to: $opencvPath" -ForegroundColor Green
    }

    # Clean existing CMake cache and build files recursively to prevent path/platform mismatches
    if (Test-Path $buildDir) {
        Write-Host "Cleaning existing CMake cache files recursively from: $buildDir"
        Get-ChildItem -Path $buildDir -Filter "CMakeCache.txt" -Recurse | Remove-Item -Force -ErrorAction SilentlyContinue
        Get-ChildItem -Path $buildDir -Filter "CMakeFiles" -Recurse | Remove-Item -Recurse -Force -ErrorAction SilentlyContinue
        Get-ChildItem -Path $buildDir -Filter "Makefile" -Recurse | Remove-Item -Force -ErrorAction SilentlyContinue
        Get-ChildItem -Path $buildDir -Filter "cmake_install.cmake" -Recurse | Remove-Item -Force -ErrorAction SilentlyContinue
    }

    if (-not (Test-Path $buildDir)) {
        New-Item -ItemType Directory -Path $buildDir | Out-Null
    }

    Push-Location $buildDir
    try {
        $cudaFlags = ""
        if ($cudaEnabled) {
            $cudaFlags = "-DWITH_CUDA=ON -DWITH_CUDNN=ON -DCMAKE_CUDA_ARCHITECTURES=`"50;52;60;61;70;75;80;86;89;90;100;101;101+PTX`""
        } else {
            $cudaFlags = "-DWITH_CUDA=OFF -DWITH_CUDNN=OFF"
        }

        # Configure and build CMake using NMake or Ninja and Visual Studio tools
        Write-Host "Using VS Dev Cmd: $VsDevCmd"
        Write-Host "Running CMake configuration and building DLL in: $buildDir"
        $wrapperGenerator = "-G `"NMake Makefiles`""
        if ($NinjaFound) {
            Write-Host "Using Ninja generator for wrapper build..." -ForegroundColor Green
            $wrapperGenerator = "-G `"Ninja`" -DCMAKE_MAKE_PROGRAM=`"$NinjaExe`""
        }
        $buildCmd = "call `"$VsDevCmd`" -arch=x64 && cmake `"$NativeDir`" $wrapperGenerator -DCMAKE_BUILD_TYPE=Release -DOpenCV_DIR=`"$opencvPath`" $cudaFlags && cmake --build . --parallel"
        cmd.exe /c $buildCmd
        
        if ($LASTEXITCODE -ne 0) {
            throw "CMake build failed with exit code $LASTEXITCODE"
        }
    }
    finally {
        Pop-Location
    }

    # Stage native DLLs
    Write-Host "Staging native DLLs into C# project structure..." -ForegroundColor Green
    $runtimeNativeDir = Join-Path $csharpProjectDir "runtimes\win-x64\native"
    if (-not (Test-Path $runtimeNativeDir)) {
        New-Item -ItemType Directory -Path $runtimeNativeDir | Out-Null
    }

    # Copy wrapper DLL
    $compiledDll = Join-Path $buildDir "opencv5sharp_native.dll"
    if (-not (Test-Path $compiledDll)) {
        throw "Compiled native wrapper DLL not found at: $compiledDll"
    }
    Copy-Item -Path $compiledDll -Destination $runtimeNativeDir -Force
    Write-Host "  Copied opencv5sharp_native.dll"

    # Copy OpenCV World and FFMPEG DLLs from the OpenCV build
    $opencvBin = Join-Path $opencvPath "x64\vc16\bin"
    if (-not (Test-Path $opencvBin)) {
        $opencvBin = Join-Path $opencvPath "x64\vc17\bin"
    }
    if (-not (Test-Path $opencvBin)) {
        $opencvBin = Join-Path $opencvPath "bin"
    }
    
    $worldDll = Join-Path $opencvBin "opencv_world500.dll"
    if (Test-Path $worldDll) {
        Copy-Item -Path $worldDll -Destination $runtimeNativeDir -Force
        Write-Host "  Copied opencv_world500.dll"
    }
    $ffmpegDll = Join-Path $opencvBin "opencv_videoio_ffmpeg500_64.dll"
    if (Test-Path $ffmpegDll) {
        Copy-Item -Path $ffmpegDll -Destination $runtimeNativeDir -Force
        Write-Host "  Copied opencv_videoio_ffmpeg500_64.dll"
    }

    Write-Host "  All native DLLs staged at: $runtimeNativeDir"
}

# 2. Execute Native Builds

# CPU Build
if ($buildCpu) {
    Build-Native-And-Stage -buildDir $BuildCpuDir -cudaEnabled $false -csharpProjectDir $CSharpCpuDir
}

# GPU Build
if ($buildGpu) {
    $cudaAvailable = Check-Cuda-Available
    if (-not $cudaAvailable) {
        if ($GpuOnly) {
            throw "CUDA Toolkit not detected on this machine. Cannot build GPU version."
        } else {
            Write-Host "`nCUDA Toolkit not detected. Skipping GPU package build." -ForegroundColor Yellow
            $buildGpu = $false
        }
    } else {
        Build-Native-And-Stage -buildDir $BuildGpuDir -cudaEnabled $true -csharpProjectDir $CSharpGpuDir
    }
}

# 3. Build Solution and Pack NuGet Packages
Write-Host "`nBuilding solution and packing NuGet packages..." -ForegroundColor Green

dotnet build (Join-Path $RootDir "OpenCV5Sharp.slnx") --configuration Release

if ($buildCpu) {
    dotnet pack $CSharpCpuDir --configuration Release --output (Join-Path $RootDir "artifacts")
}

if ($buildGpu) {
    dotnet pack $CSharpGpuDir --configuration Release --output (Join-Path $RootDir "artifacts")
}

Write-Host "`n==================================================" -ForegroundColor Cyan
Write-Host " Build Completed! NuGet packages are in: ./artifacts" -ForegroundColor Cyan
Write-Host "==================================================" -ForegroundColor Cyan
