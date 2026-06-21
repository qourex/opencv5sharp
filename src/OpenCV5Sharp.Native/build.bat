@REM Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
@REM See LICENSE file in the project root for full license information.

@echo off
echo ========================================
echo   Building Native OpenCV5Sharp Wrapper
echo ========================================

REM Auto-detect Visual Studio using vswhere
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if not exist "%VSWHERE%" (
    echo ERROR: vswhere.exe not found. Install Visual Studio with C++ workload.
    exit /b 1
)

for /f "usebackq tokens=*" %%i in (`"%VSWHERE%" -latest -products * -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath`) do (
    set "VS_PATH=%%i"
)

if not defined VS_PATH (
    echo ERROR: Visual Studio with C++ tools not found.
    exit /b 1
)

echo Visual Studio: %VS_PATH%

:: Setup MSVC environment
call "%VS_PATH%\VC\Auxiliary\Build\vcvars64.bat"

:: Clean and create build directory
if exist build rmdir /s /q build
mkdir build
cd build

:: Configure CMake with NMake Makefiles
echo.
echo [1/2] Configuring CMake...
cmake -G "NMake Makefiles" -DCMAKE_BUILD_TYPE=Release ..
if %errorlevel% neq 0 (
    echo CMake configuration failed.
    exit /b %errorlevel%
)

:: Build the project using nmake
echo.
echo [2/2] Building project using nmake...
nmake
if %errorlevel% neq 0 (
    echo Build failed.
    exit /b %errorlevel%
)

echo.
echo Build completed successfully!
