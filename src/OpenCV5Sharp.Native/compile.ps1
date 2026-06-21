# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# See LICENSE file in the project root for full license information.

<#
.SYNOPSIS
    Compiles opencv5sharp_native.dll using MSVC.
.DESCRIPTION
    Auto-detects Visual Studio installation using vswhere and compiles the
    native wrapper DLL. No hardcoded paths required.
.PARAMETER OpenCVDir
    Path to the OpenCV build directory containing include/ and lib/.
    Defaults to ../../opencv_prebuilt/opencv/build relative to this script.
.PARAMETER Configuration
    Build configuration: Release or Debug. Defaults to Release.
#>

param(
    [string]$OpenCVDir = "",
    [string]$Configuration = "Release"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# Resolve OpenCV directory
if (-not $OpenCVDir) {
    $OpenCVDir = Join-Path $PSScriptRoot "..\..\opencv_prebuilt\opencv\build"
}
$OpenCVDir = Resolve-Path $OpenCVDir -ErrorAction Stop
Write-Host "OpenCV directory: $OpenCVDir"

# Auto-detect Visual Studio using vswhere
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
if (-not (Test-Path $vswhere)) {
    throw "vswhere.exe not found. Install Visual Studio with C++ workload."
}

$vsPaths = & $vswhere -products * -property installationPath
$vsPath = ""
foreach ($path in $vsPaths) {
    if ($path -like "*SQL Server Management Studio*") { continue }
    if (Test-Path (Join-Path $path "Common7\Tools\Microsoft.VisualStudio.DevShell.dll")) {
        $vsPath = $path
        break
    }
}

if (-not $vsPath) {
    throw "Visual Studio with C++ tools not found. Install the 'Desktop development with C++' workload."
}
Write-Host "Visual Studio: $vsPath"

# Import VS developer environment
$devShellModule = Join-Path $vsPath "Common7\Tools\Microsoft.VisualStudio.DevShell.dll"
if (Test-Path $devShellModule) {
    Import-Module $devShellModule
    Enter-VsDevShell -VsInstallPath $vsPath -SkipAutomaticLocation -DevCmdArguments "-arch=x64" | Out-Null
} else {
    # Fallback: use vcvarsall.bat or vcvars64.bat
    $vcvarsall = Join-Path $vsPath "VC\Auxiliary\Build\vcvarsall.bat"
    $vcvars64 = Join-Path $vsPath "VC\Auxiliary\Build\vcvars64.bat"
    $vcvars = if (Test-Path $vcvarsall) { $vcvarsall } else { $vcvars64 }
    if (-not (Test-Path $vcvars)) { throw "Cannot find vcvarsall.bat or vcvars64.bat" }
    cmd /c "`"$vcvars`" amd64 && set" | ForEach-Object {
        if ($_ -match "^(.+?)=(.*)$") {
            [System.Environment]::SetEnvironmentVariable($matches[1], $matches[2])
        }
    }
}

# Check if cl.exe is accessible. If not, configure environment variables manually as a fallback.
if (-not (Get-Command cl.exe -ErrorAction SilentlyContinue)) {
    Write-Host "cl.exe not found in PATH. Configuring MSVC environment variables manually..." -ForegroundColor Yellow
    $msvcDir = Get-ChildItem -Path "$vsPath\VC\Tools\MSVC" | Where-Object { Test-Path (Join-Path $_.FullName "include") } | Sort-Object Name -Descending | Select-Object -First 1
    if (-not $msvcDir) { throw "Could not find MSVC tools directory containing include under $vsPath\VC\Tools\MSVC" }
    $msvcPath = $msvcDir.FullName
    
    $sdkDir = "C:\Program Files (x86)\Windows Kits\10"
    if (-not (Test-Path $sdkDir)) { throw "Windows SDK directory not found: $sdkDir" }
    $sdkVersionDir = Get-ChildItem -Path "$sdkDir\Lib" | Sort-Object Name -Descending | Select-Object -First 1
    if (-not $sdkVersionDir) { throw "Could not find any Windows SDK version under $sdkDir\Lib" }
    $sdkVersion = $sdkVersionDir.Name
    
    $env:PATH = "$msvcPath\bin\HostX64\x64;" + $env:PATH
    $env:INCLUDE = "$msvcPath\include;$sdkDir\Include\$sdkVersion\ucrt;$sdkDir\Include\$sdkVersion\shared;$sdkDir\Include\$sdkVersion\um;$sdkDir\Include\$sdkVersion\winrt;" + $env:INCLUDE
    $env:LIB = "$msvcPath\lib\x64;$sdkDir\Lib\$sdkVersion\ucrt\x64;$sdkDir\Lib\$sdkVersion\um\x64;" + $env:LIB
    
    if (-not (Get-Command cl.exe -ErrorAction SilentlyContinue)) {
        throw "Failed to locate cl.exe even after manual path configuration."
    }
}

Write-Host ""
Write-Host "Compiling native DLL ($Configuration)..."

$OptFlags = if ($Configuration -eq "Release") { "/O2 /DNDEBUG /MD" } else { "/Od /Zi /MDd" }
$SecurityFlags = "/GS /sdl /W4 /wd4996"
$LibName = if ($Configuration -eq "Release") { "opencv_world500.lib" } else { "opencv_world500d.lib" }
$LinkFlags = if ($Configuration -eq "Release") { "/DYNAMICBASE /NXCOMPAT /guard:cf" } else { "/DEBUG /DYNAMICBASE /NXCOMPAT /guard:cf" }

# Run cl.exe
Push-Location $PSScriptRoot
try {
    & cl.exe /LD /std:c++17 $OptFlags.Split(' ') $SecurityFlags.Split(' ') `
        opencv5sharp_native.cpp `
        /I "$OpenCVDir\include" `
        "$OpenCVDir\x64\vc16\lib\$LibName" `
        /Fe:opencv5sharp_native.dll `
        /link $LinkFlags.Split(' ')

    if ($LASTEXITCODE -ne 0) {
        throw "Compilation FAILED with exit code $LASTEXITCODE"
    }

    Write-Host ""
    Write-Host "Compilation SUCCESS! Output: opencv5sharp_native.dll" -ForegroundColor Green
} finally {
    Pop-Location
}
