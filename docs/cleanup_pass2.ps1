$refDir = 'G:\Git-Repos\OpenCV5\docs\guide\reference'
$files = Get-ChildItem "$refDir\*.md"

foreach ($file in $files) {
    $c = Get-Content $file.FullName -Raw -Encoding UTF8
    
    # Replace remaining bare std::vector references (without type params)
    $c = $c -replace '\bstd::vector\b', 'array'
    
    # Replace remaining std::string references
    $c = $c -replace '\bstd::string\b', 'string'
    
    [System.IO.File]::WriteAllText($file.FullName, $c, [System.Text.Encoding]::UTF8)
    Write-Host "Pass2: $($file.Name)"
}

Write-Host "Pass 2 done"
