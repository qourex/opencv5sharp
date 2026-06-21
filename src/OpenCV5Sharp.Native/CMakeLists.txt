# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# See LICENSE file in the project root for full license information.

cmake_minimum_required(VERSION 3.12)
project(opencv5sharp_native VERSION 1.0.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Configure OpenCV path - auto-detect from environment or relative path
if(DEFINED ENV{OPENCV_DIR})
    set(OpenCV_DIR $ENV{OPENCV_DIR})
elseif(EXISTS "${CMAKE_CURRENT_SOURCE_DIR}/../../opencv_prebuilt/opencv/build")
    set(OpenCV_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../../opencv_prebuilt/opencv/build")
else()
    message(FATAL_ERROR
        "OpenCV build directory not found. Set one of:\n"
        "  - OPENCV_DIR environment variable\n"
        "  - Place OpenCV at <workspace>/opencv_prebuilt/opencv/build/")
endif()

find_package(OpenCV REQUIRED)

message(STATUS "OpenCV library status:")
message(STATUS "    version: ${OpenCV_VERSION}")
message(STATUS "    libraries: ${OpenCV_LIBS}")
message(STATUS "    include path: ${OpenCV_INCLUDE_DIRS}")

# Define the shared library
add_library(opencv5sharp_native SHARED opencv5sharp_native.cpp opencv5sharp_native.h)

# Include directories
target_include_directories(opencv5sharp_native PRIVATE ${OpenCV_INCLUDE_DIRS})

# Link libraries
target_link_libraries(opencv5sharp_native PRIVATE ${OpenCV_LIBS})

# Define exports
target_compile_definitions(opencv5sharp_native PRIVATE "CVAPI_EXPORTS")

# Security hardening flags (MSVC)
if(MSVC)
    target_compile_options(opencv5sharp_native PRIVATE /GS /sdl /W4)
    target_link_options(opencv5sharp_native PRIVATE /DYNAMICBASE /NXCOMPAT /guard:cf)
endif()
