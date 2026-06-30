$refDir = 'G:\Git-Repos\OpenCV5\docs\guide\reference'
$files = Get-ChildItem "$refDir\*.md"

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw -Encoding UTF8
    
    # === Fix HTML entities first (used in many files for angle brackets) ===
    # Only in descriptive text contexts, not inside actual HTML tags
    # We'll handle these carefully
    
    # === Doxygen markers ===
    # @ref cv::ClassName -> `ClassName`
    $content = $content -replace '@ref\s+cv::(\w+)', '`$1`'
    # @ref #ClassName -> `ClassName`
    $content = $content -replace '@ref\s+#(\w+)', '`$1`'
    # @ref tutorial_xxx "text" -> the text
    $content = $content -replace '@ref\s+\w+\s+"([^"]+)"', '$1'
    # @ref ClassName -> `ClassName`
    $content = $content -replace '@ref\s+(\w+)', '`$1`'
    
    # #calibrateCamera / #methodName -> `calibrateCamera` / `methodName`
    $content = $content -replace '(?<=\s)#(\w+)', '`$1`'

    # @param[in] name / @param[out] name / @param name -> **name**
    $content = $content -replace '@param\[in\]\s+(\w+)', '**$1**'
    $content = $content -replace '@param\[out\]\s+(\w+)', '**$1**'
    $content = $content -replace '@param\s+(\w+)', '**$1**'
    
    # @cite Reference -> [Reference]
    $content = $content -replace '@cite\s+(\w+)', '[$1]'
    
    # @todo -> TODO:
    $content = $content -replace '@todo\b', 'TODO:'
    
    # @snippet filepath id -> *(see OpenCV documentation for examples)*
    $content = $content -replace '@snippet\s+[^\r\n]+', '*(see OpenCV documentation for examples)*'
    
    # @note -> **Note:**
    $content = $content -replace '@note\s', '**Note:** '
    
    # @sa -> **See also:**
    $content = $content -replace '@sa\s', '**See also:** '
    
    # @brief -> (remove, brief is just the first paragraph)
    $content = $content -replace '@brief\s+', ''
    
    # @overload -> *(Overload)*
    $content = $content -replace '@overload', '*(Overload)*'

    # @see -> **See also:**
    $content = $content -replace '@see\s', '**See also:** '
    
    # @code{.cpp} ... @endcode -> ``` ... ```
    $content = $content -replace '@code\{?\.?\w*\}?', '```'
    $content = $content -replace '@endcode', '```'
    
    # {.cpp} at start of code blocks (leftover from Doxygen) -> remove
    $content = $content -replace '(?<=```csharp\r?\n)\{\.cpp\}\r?\n', ''

    # === C++ type references (HTML-encoded versions from the generator) ===
    # Handle &lt; &gt; encoded versions
    $content = $content -replace 'std::vector&lt;std::vector&lt;cv::Point&gt;\s*&gt;', 'Point[][]'
    $content = $content -replace 'std::vector&lt;std::vector&lt;cv::Point2f&gt;\s*&gt;', 'Point2F[][]'
    $content = $content -replace 'std::vector&lt;std::vector&lt;cv::Vec3f&gt;&gt;', 'Vec3F[][]'
    $content = $content -replace 'std::vector&lt;std::vector&lt;Point2f&gt;\s*&gt;', 'Point2F[][]'
    $content = $content -replace 'std::vector&lt;std::vector&lt;cv::Vec2f&gt;&gt;', 'Vec2F[][]'
    $content = $content -replace 'std::vector&lt;cv::Point2f&gt;', 'Point2F[]'
    $content = $content -replace 'std::vector&lt;cv::Point3f&gt;', 'Point3F[]'
    $content = $content -replace 'std::vector&lt;cv::Point&gt;', 'Point[]'
    $content = $content -replace 'std::vector&lt;cv::Vec3f&gt;', 'Vec3F[]'
    $content = $content -replace 'std::vector&lt;cv::Vec4i&gt;', 'Vec4I[]'
    $content = $content -replace 'std::vector&lt;cv::Vec2f&gt;', 'Vec2F[]'
    $content = $content -replace 'std::vector&lt;cv::Mat&gt;', 'Mat[]'
    $content = $content -replace 'std::vector&lt;cv::Rect&gt;', 'Rect[]'
    $content = $content -replace 'std::vector&lt;cv::DMatch&gt;', 'DMatch[]'
    $content = $content -replace 'std::vector&lt;cv::KeyPoint&gt;', 'KeyPoint[]'
    $content = $content -replace 'std::vector&lt;Dictionary&gt;', 'Dictionary[]'
    $content = $content -replace 'std::vector&lt;Point2f&gt;', 'Point2F[]'
    $content = $content -replace 'std::vector&lt;Point&gt;', 'Point[]'
    $content = $content -replace 'std::vector&lt;int&gt;', 'int[]'
    $content = $content -replace 'std::vector&lt;float&gt;', 'float[]'
    $content = $content -replace 'std::vector&lt;double&gt;', 'double[]'
    $content = $content -replace 'std::vector&lt;uchar&gt;', 'byte[]'
    $content = $content -replace 'std::vector&lt;std::string&gt;', 'string[]'
    $content = $content -replace 'std::vector&lt;Mat&gt;', 'Mat[]'
    $content = $content -replace 'std::vector&lt;&gt;', '[]'
    $content = $content -replace 'std::vector&lt;([^&]+)&gt;', '$1[]'
    
    # Handle backslash-escaped angle bracket versions: std::vector\<...\>
    $content = $content -replace 'std::vector\\&lt;\\&gt;', '[]'
    $content = $content -replace 'std::vector\\&lt;int\\&gt;', 'int[]'
    $content = $content -replace 'std::vector\\&lt;Point\\&gt;', 'Point[]'
    
    # Handle plain < > versions (in code blocks etc.)
    $content = $content -replace 'std::vector<std::vector<cv::Point>>', 'Point[][]'
    $content = $content -replace 'std::vector<std::vector<cv::Vec3f>>', 'Vec3F[][]'
    $content = $content -replace 'std::vector<std::vector<cv::Point2f>>', 'Point2F[][]'
    $content = $content -replace 'std::vector<cv::Point2f>', 'Point2F[]'
    $content = $content -replace 'std::vector<cv::Point3f>', 'Point3F[]'
    $content = $content -replace 'std::vector<cv::Point>', 'Point[]'
    $content = $content -replace 'std::vector<cv::Vec3f>', 'Vec3F[]'
    $content = $content -replace 'std::vector<cv::Vec4i>', 'Vec4I[]'
    $content = $content -replace 'std::vector<cv::Mat>', 'Mat[]'
    $content = $content -replace 'std::vector<cv::Rect>', 'Rect[]'
    $content = $content -replace 'std::vector<cv::DMatch>', 'DMatch[]'
    $content = $content -replace 'std::vector<cv::KeyPoint>', 'KeyPoint[]'
    $content = $content -replace 'std::vector<int>', 'int[]'
    $content = $content -replace 'std::vector<float>', 'float[]'
    $content = $content -replace 'std::vector<double>', 'double[]'
    $content = $content -replace 'std::vector<uchar>', 'byte[]'
    $content = $content -replace 'std::vector<std::string>', 'string[]'
    $content = $content -replace 'std::vector<([^>]+)>', '$1[]'
    
    # std::string -> string
    $content = $content -replace 'std::string', 'string'
    
    # std::numeric_limits
    $content = $content -replace 'std::numeric_limits<float>::quiet_NaN\(\)', 'float.NaN'
    $content = $content -replace 'std::numeric_limits<double>::infinity\(\)', 'double.PositiveInfinity'
    $content = $content -replace 'std::numeric_limits&lt;float&gt;::quiet_NaN\(\)', 'float.NaN'
    $content = $content -replace 'std::numeric_limits&lt;double&gt;::infinity\(\)', 'double.PositiveInfinity'
    
    # cv:: prefix removal - specific types first
    $content = $content -replace '\bcv::InputArray\b', 'Mat'
    $content = $content -replace '\bcv::OutputArray\b', 'Mat'
    $content = $content -replace '\bcv::InputOutputArray\b', 'Mat'
    $content = $content -replace '\bcv::Exception\b', 'OpenCVException'
    # Generic cv:: prefix for remaining references
    $content = $content -replace '\bcv::cuda::(\w+)', '$1'
    $content = $content -replace '\bcv::(\w+)', '$1'
    
    # [formula] -> *(formula)* - make it clear these are mathematical formulas
    $content = $content -replace '\[formula\]', '*(formula)*'
    
    # Clean up &amp; -> &
    $content = $content -replace '&amp;', '&'
    # Clean up &lt; &gt; in descriptive text (but be careful)
    $content = $content -replace '&lt;', '<'
    $content = $content -replace '&gt;', '>'
    
    # Write back
    [System.IO.File]::WriteAllText($file.FullName, $content, [System.Text.Encoding]::UTF8)
    Write-Host "Processed: $($file.Name)"
}

Write-Host "`nDone! All reference docs cleaned."
