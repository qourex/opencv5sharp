# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# OpenCV5Sharp Linux CUDA compilation Dockerfile
# To build:
#   docker build -t opencv5sharp-cuda-builder .
# To run (mounts your workspace and builds Linux CUDA binaries):
#   docker run --gpus all -it --rm -v $(pwd):/workspace opencv5sharp-cuda-builder

FROM nvidia/cuda:12.8.0-cudnn-devel-ubuntu22.04

# Avoid prompts
ENV DEBIAN_FRONTEND=noninteractive

# 1. Install general compilation dependencies and media libraries for OpenCV
RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential \
    cmake \
    git \
    wget \
    pkg-config \
    ca-certificates \
    python3 \
    python3-dev \
    # OpenCV dependencies
    libjpeg-dev \
    libpng-dev \
    libtiff-dev \
    libavcodec-dev \
    libavformat-dev \
    libswscale-dev \
    libv4l-dev \
    libxvidcore-dev \
    libx264-dev \
    libatlas-base-dev \
    gfortran \
    libtbb-dev \
    # Clean up apt cache
    && rm -rf /var/lib/apt/lists/*

# 2. Install Microsoft .NET SDK (to compile C# project and pack NuGet packages)
RUN wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update && apt-get install -y --no-install-recommends \
    dotnet-sdk-8.0 \
    dotnet-sdk-9.0 \
    dotnet-sdk-10.0 \
    && rm -rf /var/lib/apt/lists/*

# 3. Setup workdir
WORKDIR /workspace

# Default command compiles OpenCV 5 with CUDA and wrapper using the build script
CMD ["bash", "build.sh", "--gpu-only"]
