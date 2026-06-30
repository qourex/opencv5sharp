$refDir = 'G:\Git-Repos\OpenCV5\docs\guide\reference'
$files = Get-ChildItem "$refDir\*.md"

foreach ($file in $files) {
    $c = Get-Content $file.FullName -Raw -Encoding UTF8
    
    # @returns -> **Returns**:
    $c = $c -replace '@returns\s', '**Returns**: '
    
    # @p varname -> `varname` (inline code parameter reference)
    $c = $c -replace '@p\s+(\w+)', '`$1`'
    
    # @details -> (remove, just text)
    $c = $c -replace '@details\s+', ''
    
    # @deprecated -> *(Deprecated)*
    $c = $c -replace '@deprecated', '*(Deprecated)*'
    
    # \c word -> `word` (doxygen monospace)
    $c = $c -replace '\\c\s+(\w+)', '`$1`'
    
    [System.IO.File]::WriteAllText($file.FullName, $c, [System.Text.Encoding]::UTF8)
    Write-Host "Pass3: $($file.Name)"
}

Write-Host "Pass 3 done"
