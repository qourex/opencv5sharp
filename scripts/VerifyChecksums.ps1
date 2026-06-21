# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# See LICENSE file in the project root for full license information.

$checksumFile = Join-Path $PSScriptRoot "..\CHECKSUMS.sha256"
if (-not (Test-Path $checksumFile)) {
    Write-Error "Checksum file not found: $checksumFile"
    exit 1
}

$lines = Get-Content $checksumFile
$failures = 0

foreach ($line in $lines) {
    if ([string]::IsNullOrWhiteSpace($line) -or $line.StartsWith("#")) {
        continue
    }

    $parts = $line.Split(" ", [System.StringSplitOptions]::RemoveEmptyEntries)
    if ($parts.Length -lt 2) {
        continue
    }

    $expectedHash = $parts[0].Trim().ToLower()
    $relativePath = $parts[1].Trim()
    $absolutePath = Resolve-Path "$PSScriptRoot\..\$relativePath" -ErrorAction SilentlyContinue

    if (-not $absolutePath -or -not (Test-Path $absolutePath)) {
        Write-Warning "File not found: $relativePath"
        $failures++
        continue
    }

    $actualHash = (Get-FileHash $absolutePath -Algorithm SHA256).Hash.ToLower()

    if ($actualHash -eq $expectedHash) {
        Write-Host "  [PASS] $relativePath matches checksum" -ForegroundColor Green
    } else {
        Write-Error "  [FAIL] $relativePath checksum mismatch! Expected: $expectedHash, Got: $actualHash"
        $failures++
    }
}

if ($failures -gt 0) {
    Write-Error "Checksum verification FAILED with $failures error(s)."
    exit 1
} else {
    Write-Host "Checksum verification PASSED." -ForegroundColor Green
}
