# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# See LICENSE file in the project root for full license information.

<#
.SYNOPSIS
    Unified build script for OpenCV5Sharp.
.DESCRIPTION
    Orchestrates the full build pipeline: prerequisites check, optional
    code generation, C# build, test execution, and NuGet packaging.
.PARAMETER Configuration
    Build configuration: Release or Debug. Defaults to Release.
.PARAMETER SkipTests
    Skip running the test suite.
.PARAMETER SkipPack
    Skip NuGet package creation.
.PARAMETER Regenerate
    Run the Python binding generator before building.
.PARAMETER OpenCVDir
    Path to OpenCV source directory (required if -Regenerate is specified).
#>

param(
    [string]$Configuration = "Release",
    [switch]$SkipTests,
    [switch]$SkipPack,
    [switch]$Regenerate,
    [string]$OpenCVDir = ""
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$startTime = Get-Date

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  OpenCV5Sharp Build Pipeline" -ForegroundColor Cyan
Write-Host "  Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

# Step 1: Prerequisites
Write-Host "`n[1/5] Checking prerequisites..." -ForegroundColor Yellow
$dotnetVersion = dotnet --version
Write-Host "  .NET SDK: $dotnetVersion"

if ($Regenerate) {
    $pythonVersion = py --version 2>&1
    Write-Host "  Python: $pythonVersion"
}

Write-Host "  Verifying DLL integrity checksums..."
& "$PSScriptRoot\scripts\VerifyChecksums.ps1"

# Step 2: Code Generation (optional)
if ($Regenerate) {
    Write-Host "`n[2/5] Running binding generator..." -ForegroundColor Yellow
    if (-not $OpenCVDir) {
        $OpenCVDir = Join-Path $PSScriptRoot "opencv"
    }
    py "$PSScriptRoot\src\OpenCV5Sharp.Generator\generator.py" --opencv-dir $OpenCVDir --workspace-dir $PSScriptRoot --verbose
    if ($LASTEXITCODE -ne 0) { throw "Code generation failed" }
} else {
    Write-Host "`n[2/5] Skipping code generation (use -Regenerate to enable)" -ForegroundColor DarkGray
}

# Step 3: Build
Write-Host "`n[3/5] Building solution..." -ForegroundColor Yellow
dotnet build OpenCV5Sharp.slnx -c $Configuration
if ($LASTEXITCODE -ne 0) { throw "Build failed" }

# Step 4: Test
if (-not $SkipTests) {
    Write-Host "`n[4/5] Running tests..." -ForegroundColor Yellow
    dotnet test OpenCV5Sharp.slnx --no-build -c $Configuration --settings tests/coverlet.runsettings --logger:"console;verbosity=detailed"
    if ($LASTEXITCODE -ne 0) { throw "Tests failed" }
} else {
    Write-Host "`n[4/5] Skipping tests (use without -SkipTests to enable)" -ForegroundColor DarkGray
}

# Step 5: Pack
if (-not $SkipPack -and $Configuration -eq "Release") {
    Write-Host "`n[5/5] Creating NuGet package..." -ForegroundColor Yellow
    dotnet pack src\OpenCV5Sharp\OpenCV5Sharp.csproj -c Release --no-build
    if ($LASTEXITCODE -ne 0) { throw "Pack failed" }
    
    $nupkg = Get-ChildItem -Path "src\OpenCV5Sharp\bin\Release\*.nupkg" | Select-Object -First 1
    Write-Host "  Package: $($nupkg.FullName)" -ForegroundColor Green
    Write-Host "  Size: $([math]::Round($nupkg.Length / 1MB, 2)) MB" -ForegroundColor Green
} else {
    Write-Host "`n[5/5] Skipping NuGet pack" -ForegroundColor DarkGray
}

$elapsed = (Get-Date) - $startTime
Write-Host "`n========================================" -ForegroundColor Green
Write-Host "  Build completed in $($elapsed.TotalSeconds.ToString('F1'))s" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
