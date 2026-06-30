#!/usr/bin/env bash
# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# OpenCV5Sharp Linux & macOS Build Automation Script
# Combines native C++ CMake compilation and .NET NuGet packaging.
# Supports CPU-only and GPU/CUDA-enabled package builds.
#
# Usage:
#   ./build.sh              # Build both CPU and GPU packages (default, skips GPU if CUDA is missing)
#   ./build.sh --cpu-only   # Build only the CPU package
#   ./build.sh --gpu-only   # Build only the GPU package (fails if CUDA is missing)

set -e

# Parse arguments
cpu_only=false
gpu_only=false

for arg in "$@"; do
  case $arg in
    --cpu-only)
      cpu_only=true
      shift
      ;;
    --gpu-only)
      gpu_only=true
      shift
      ;;
    *)
      echo "Unknown argument: $arg"
      echo "Usage: ./build.sh [--cpu-only | --gpu-only]"
      exit 1
      ;;
  esac
done

if [ "$cpu_only" = true ] && [ "$gpu_only" = true ]; then
  echo "Error: Cannot specify both --cpu-only and --gpu-only"
  exit 1
fi

build_cpu=true
build_gpu=true

if [ "$cpu_only" = true ]; then
  build_gpu=false
elif [ "$gpu_only" = true ]; then
  build_cpu=false
fi

echo -e "\e[36m==================================================\e[0m"
echo -e "\e[36m               Building OpenCV5Sharp              \e[0m"
echo -e "\e[36m==================================================\e[0m"

# 1. Paths Setup
ROOT_DIR="$(pwd)"
NATIVE_DIR="$ROOT_DIR/src/OpenCV5Sharp.Native"
BUILD_CPU_DIR="$NATIVE_DIR/build_cpu_linux"
BUILD_GPU_DIR="$NATIVE_DIR/build_gpu_linux"
CSHARP_CPU_DIR="$ROOT_DIR/src/OpenCV5Sharp"
CSHARP_GPU_DIR="$ROOT_DIR/src/OpenCV5Sharp.Gpu.Linux"

# CUDA detection helper
check_cuda_available() {
  if [ -n "$CUDA_PATH" ]; then return 0; fi
  if command -v nvcc &> /dev/null; then return 0; fi
  if [ -d "/usr/local/cuda" ]; then return 0; fi
  return 1
}

# Determine OS Platform
OS_TYPE="linux"
if [[ "$OSTYPE" == "darwin"* ]]; then
  OS_TYPE="osx"
fi

# Native build and library staging helper
build_native_and_stage() {
  local build_dir="$1"
  local cuda_enabled="$2"
  local csharp_project_dir="$3"

  # Resolve OpenCV path
  local opencvPath="$OPENCV_DIR"
  if [ -z "$opencvPath" ]; then
    if [ "$cuda_enabled" = true ]; then
      opencvPath="$ROOT_DIR/build_opencv_cuda_linux/install"
      if [ ! -d "$opencvPath" ]; then
        # OpenCV CUDA install does not exist, let's clone and compile it from source!
        contribDir="$ROOT_DIR/opencv_contrib"
        if [ ! -d "$contribDir" ]; then
          echo -e "\n\e[32mCloning opencv_contrib (5.x branch)...\e[0m"
          git clone --branch 5.x --depth 1 https://github.com/opencv/opencv_contrib.git "$contribDir"
        fi

        opencvBuildDir="$ROOT_DIR/build_opencv_cuda_linux"
        echo -e "\n\e[32m[CUDA Build] Configuring and compiling OpenCV 5 with CUDA from source (all kernels)...\e[0m"
        
        needs_clean=false
        if [ -d "$opencvBuildDir" ]; then
          cache_file="$opencvBuildDir/CMakeCache.txt"
          if [ -f "$cache_file" ]; then
            normalized_root="${ROOT_DIR//\\//}"
            if ! grep -q "$normalized_root" "$cache_file"; then
              echo "Detected path mismatch in CMake cache (moved folder or docker mount change). Wiping build directory for a clean configure..."
              needs_clean=true
            fi
          fi
        fi

        if [ "$needs_clean" = true ]; then
          echo "Cleaning existing OpenCV build directory: $opencvBuildDir"
          rm -rf "$opencvBuildDir"
        fi
        mkdir -p "$opencvBuildDir"
        pushd "$opencvBuildDir" > /dev/null
        
        cmake "$ROOT_DIR/opencv" \
          -DCMAKE_BUILD_TYPE=Release \
          -DCMAKE_INSTALL_PREFIX=install \
          -DBUILD_SHARED_LIBS=ON \
          -DBUILD_opencv_world=ON \
          -DWITH_CUDA=ON \
          -DWITH_CUDNN=ON \
          -DCUDA_FAST_MATH=ON \
          -DOPENCV_DNN_CUDA=ON \
          -DCUDA_USE_STATIC_CUDA_RUNTIME=OFF \
          -DCUDA_ARCH_BIN="6.1 7.5 8.6 8.9 10.1" \
          -DCUDA_ARCH_PTX="10.1" \
          -DCMAKE_CUDA_FLAGS="--threads 0" \
          -DOPENCV_EXTRA_MODULES_PATH="$contribDir/modules" \
          -DBUILD_TESTS=OFF \
          -DBUILD_PERF_TESTS=OFF \
          -DBUILD_EXAMPLES=OFF \
          -DBUILD_DOCS=OFF
          
        cmake --build . --config Release --target install --parallel $(nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 2)
        popd > /dev/null
        opencvPath="$opencvBuildDir/install"
      fi
    else
      opencvPath="$ROOT_DIR/opencv_prebuilt/opencv/build"
    fi
  fi

  if [ "$cuda_enabled" = true ]; then
    echo -e "\n\e[32mConfiguring and building native C++ wrapper (CUDA) linking to: $opencvPath...\e[0m"
  else
    echo -e "\n\e[32mConfiguring and building native C++ wrapper (CPU) linking to: $opencvPath...\e[0m"
  fi

  # Clean existing CMake cache and build files
  if [ -d "$build_dir" ]; then
    echo "Cleaning existing CMake cache files recursively from: $build_dir"
    find "$build_dir" -name "CMakeCache.txt" -delete
    find "$build_dir" -name "CMakeFiles" -type d -exec rm -rf {} +
    find "$build_dir" -name "Makefile" -delete
    find "$build_dir" -name "cmake_install.cmake" -delete
  fi

  mkdir -p "$build_dir"
  pushd "$build_dir" > /dev/null

  cmake_flags=""
  if [ "$cuda_enabled" = true ]; then
    cmake_flags="-DWITH_CUDA=ON -DWITH_CUDNN=ON -DCMAKE_CUDA_ARCHITECTURES=\"61;75;86;89;101;101+PTX\""
  else
    cmake_flags="-DWITH_CUDA=OFF -DWITH_CUDNN=OFF"
  fi

  # Run CMake configuration and build
  echo "Running CMake configuration in: $build_dir"
  cmake "$NATIVE_DIR" -DCMAKE_BUILD_TYPE=Release -DOpenCV_DIR="$opencvPath" $cmake_flags
  cmake --build . --config Release --parallel $(nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 2)

  popd > /dev/null

  # Determine target architecture
  ARCH_TYPE="x64"
  if [[ "$(uname -m)" == "arm64" || "$(uname -m)" == "aarch64" ]]; then
    ARCH_TYPE="arm64"
  fi

  # Stage native libraries
  local stage_platform="${OS_TYPE}-${ARCH_TYPE}"
  echo -e "\e[32mStaging native libraries into C# project structure for ${stage_platform}...\e[0m"
  local runtime_native_dir="$csharp_project_dir/runtimes/${stage_platform}/native"
  mkdir -p "$runtime_native_dir"

  # Copy compiled wrapper library (.so on Linux, .dylib on macOS)
  if [ "$OS_TYPE" = "osx" ]; then
    local compiled_lib="$build_dir/libopencv5sharp_native.dylib"
    if [ ! -f "$compiled_lib" ]; then
      echo "Error: Compiled native wrapper not found at: $compiled_lib"
      exit 1
    fi
    cp "$compiled_lib" "$runtime_native_dir/"
    echo "  Copied libopencv5sharp_native.dylib"
    
    # Copy OpenCV World library
    local ct_lib="$opencvPath/lib/libopencv_world.dylib"
    if [ -f "$ct_lib" ]; then
      cp "$ct_lib" "$runtime_native_dir/"
      echo "  Copied libopencv_world.dylib"
    fi
  else
    local compiled_lib="$build_dir/libopencv5sharp_native.so"
    if [ ! -f "$compiled_lib" ]; then
      echo "Error: Compiled native wrapper not found at: $compiled_lib"
      exit 1
    fi
    cp "$compiled_lib" "$runtime_native_dir/"
    echo "  Copied libopencv5sharp_native.so"

    # Copy OpenCV World library
    local ct_lib="$opencvPath/lib/libopencv_world.so"
    if [ ! -f "$ct_lib" ]; then
      # Try finding match
      ct_lib=$(find "$opencvPath/lib" -name "libopencv_world.so*" | head -n 1)
    fi
    if [ -f "$ct_lib" ]; then
      local lib_name=$(basename "$ct_lib")
      cp "$ct_lib" "$runtime_native_dir/$lib_name"
      echo "  Copied $lib_name"
    fi
  fi

  echo -e "  All native libraries staged at: $runtime_native_dir"
}

# 2. Execute Native Builds

# CPU Build
if [ "$build_cpu" = true ]; then
  build_native_and_stage "$BUILD_CPU_DIR" false "$CSHARP_CPU_DIR"
fi

# GPU Build
if [ "$build_gpu" = true ]; then
  if check_cuda_available; then
    build_native_and_stage "$BUILD_GPU_DIR" true "$CSHARP_GPU_DIR"
  else
    if [ "$gpu_only" = true ]; then
      echo "Error: CUDA Toolkit / nvcc not detected on this machine. Cannot build GPU version."
      exit 1
    else
      echo -e "\n\e[33mCUDA Toolkit not detected. Skipping GPU package build.\e[0m"
      build_gpu=false
    fi
  fi
fi

# 3. Build Solution and Pack NuGet Packages
if command -v dotnet &> /dev/null; then
  echo -e "\n\e[32mBuilding solution and packing NuGet packages...\e[0m"

  dotnet build "$ROOT_DIR/OpenCV5Sharp.slnx" --configuration Release

  if [ "$build_cpu" = true ]; then
    dotnet pack "$CSHARP_CPU_DIR" --configuration Release --output "$ROOT_DIR/artifacts"
  fi

  if [ "$build_gpu" = true ]; then
    dotnet pack "$CSHARP_GPU_DIR" --configuration Release --output "$ROOT_DIR/artifacts"
  fi

  echo -e "\n\e[36m==================================================\e[0m"
  echo -e "\e[36m Build Completed! NuGet packages are in: ./artifacts\e[0m"
  echo -e "\e[36m==================================================\e[0m"
else
  echo -e "\n\e[33mDotnet SDK not found in PATH. Skipping .NET build and pack steps.\e[0m"
fi
