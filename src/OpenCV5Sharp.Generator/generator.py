# Copyright (c) 2026 Qourex. Licensed under Apache-2.0.
# See LICENSE file in the project root for full license information.

import os
import sys
import glob
import re
import argparse

# hdr_parser is imported dynamically after resolving the OpenCV directory path.
# See the main() function at the bottom of this file.
hdr_parser = None  # Populated at runtime

CLASS_TO_MODULE = {}
ALL_CLASSES_AND_ENUMS = set()
MODULE_NAMESPACES = {
    "dnn": "cv::dnn",
    "ml": "cv::ml",
    "flann": "cv::flann",
    "cuda": "cv::cuda",
    "aruco": "cv::aruco"
}

READONLY_PROPERTIES = {
    ("cv::Mat", "rows"), ("cv::Mat", "cols"), ("cv::Mat", "data"),
    ("cv::Mat", "dims"), ("cv::Mat", "step"),
    ("cv.Mat", "rows"), ("cv.Mat", "cols"), ("cv.Mat", "data"),
    ("cv.Mat", "dims"), ("cv.Mat", "step"),
    ("Mat", "rows"), ("Mat", "cols"), ("Mat", "data"),
    ("Mat", "dims"), ("Mat", "step"),
}

def normalize_module_name(mod: str) -> str:
    """Normalize OpenCV module name to C# PascalCase module name."""
    if not mod:
        return "Core"
    mod = mod.lower()
    if mod == "features2d":
        return "Features2D"
    if mod == "calib3d":
        return "Calib3D"
    if mod == "dnn":
        return "Dnn"
    if mod == "ml":
        return "Ml"
    if mod == "flann":
        return "Flann"
    if mod == "photo":
        return "Photo"
    if mod == "video":
        return "Video"
    if mod == "videoio":
        return "VideoIO"
    if mod == "objdetect":
        return "Objdetect"
    if mod == "stitching":
        return "Stitching"
    return mod.capitalize()

def format_xml_doc(doc_str: str, indent_spaces: int, params_list=None, returns_desc=None, has_disposable=False, is_enum=False) -> str:
    """Format C++ docstring into C# XML documentation comments."""
    if not doc_str:
        doc_str = ""
    
    doc_str = doc_str.replace('\r', '')
    summary_lines = []
    remarks_lines = []
    
    current_param = None
    param_descs = {}
    return_lines = []
    
    in_brief = False
    in_param = False
    in_return = False
    in_remarks = False
    
    lines = doc_str.split('\n')
    for line in lines:
        line_strip = line.strip()
        if not line_strip:
            if summary_lines and not remarks_lines and not param_descs and not return_lines:
                in_brief = False
                in_remarks = True
            continue

        # Strip Doxygen artifacts that leak into XML docs
        if line_strip.startswith('@snippet') or line_strip.startswith('@copydoc'):
            continue  # Skip @snippet and @copydoc lines entirely
        if line_strip.startswith('@overload'):
            summary_lines.append('This is an overloaded member function, provided for convenience.')
            continue
        # Convert LaTeX \f[...\f] to plain text
        import re as _re
        line_strip = _re.sub(r'\\f\[.*?\\f\]', '[see mathematical formula in OpenCV docs]', line_strip)
        line_strip = _re.sub(r'\\f\$.*?\\f\$', '[formula]', line_strip)

        if line_strip.startswith('@brief'):
            in_brief = True
            in_param = False
            in_return = False
            in_remarks = False
            content = line_strip[6:].strip()
            if content:
                summary_lines.append(content)
        elif line_strip.startswith('@param'):
            in_brief = False
            in_param = True
            in_return = False
            in_remarks = False
            parts = line_strip[6:].strip().split(None, 1)
            if parts:
                current_param = parts[0].strip()
                content = parts[1].strip() if len(parts) > 1 else ""
                param_descs[current_param] = [content] if content else []
        elif line_strip.startswith('@return') or line_strip.startswith('@returns'):
            in_brief = False
            in_param = False
            in_return = True
            in_remarks = False
            content = line_strip[7:].strip() if line_strip.startswith('@return') else line_strip[8:].strip()
            if content:
                return_lines.append(content)
        elif line_strip.startswith('@note') or line_strip.startswith('@warning') or line_strip.startswith('@sa') or line_strip.startswith('@see'):
            in_brief = False
            in_param = False
            in_return = False
            in_remarks = True
            remarks_lines.append(line_strip)
        else:
            if in_brief:
                summary_lines.append(line_strip)
            elif in_param and current_param:
                param_descs[current_param].append(line_strip)
            elif in_return:
                return_lines.append(line_strip)
            elif in_remarks:
                remarks_lines.append(line_strip)
            else:
                if not summary_lines:
                    summary_lines.append(line_strip)
                else:
                    remarks_lines.append(line_strip)

    if not summary_lines or (len(summary_lines) == 1 and not summary_lines[0].strip()):
        summary_lines = ["OpenCV type (see OpenCV documentation for details)."]
        
    indent = " " * indent_spaces
    xml_lines = []
    xml_lines.append(f"{indent}/// <summary>")
    for sl in summary_lines:
        sl_esc = sl.replace('&', '&amp;').replace('<', '&lt;').replace('>', '&gt;')
        xml_lines.append(f"{indent}/// {sl_esc}")
    xml_lines.append(f"{indent}/// </summary>")
    
    if params_list:
        for p in params_list:
            p_name = p[1]
            p_name_san = sanitize_csharp_argument_name(p_name)
            desc_lines = param_descs.get(p_name, [])
            desc = " ".join(desc_lines).strip()
            if not desc:
                # Provide meaningful defaults for common parameter names
                _common_param_descs = {
                    'src': 'Source matrix or image.',
                    'dst': 'Destination matrix or image (output).',
                    'mask': 'Optional operation mask.',
                    'borderType': 'Pixel extrapolation method (see BorderTypes).',
                    'ksize': 'Kernel size.',
                    'image': 'Input image.',
                    'img': 'Input image.',
                    'filename': 'Path to the file.',
                    'flags': 'Operation flags.',
                    'color': 'Color value (BGR or BGRA).',
                    'thickness': 'Line thickness.',
                    'lineType': 'Type of the line (see LineTypes).',
                    'shift': 'Number of fractional bits in coordinates.',
                    'sigmaX': 'Gaussian kernel standard deviation in X direction.',
                    'sigmaY': 'Gaussian kernel standard deviation in Y direction.',
                    'threshold1': 'First threshold for the hysteresis procedure.',
                    'threshold2': 'Second threshold for the hysteresis procedure.',
                    'apertureSize': 'Aperture size for the Sobel operator.',
                }
                desc = _common_param_descs.get(p_name, f"The {p_name_san} parameter.")
            desc_esc = desc.replace('&', '&amp;').replace('<', '&lt;').replace('>', '&gt;')
            xml_lines.append(f"{indent}/// <param name=\"{p_name_san.lstrip('@')}\">{desc_esc}</param>")
            
    if returns_desc and returns_desc != "void":
        ret_desc = " ".join(return_lines).strip()
        if not ret_desc:
            ret_desc = "The returned value."
        ret_desc_esc = ret_desc.replace('&', '&amp;').replace('<', '&lt;').replace('>', '&gt;')
        xml_lines.append(f"{indent}/// <returns>{ret_desc_esc}</returns>")

    # Only emit exception tags for non-enum types (enums cannot throw exceptions)
    if not is_enum:
        if has_disposable:
            xml_lines.append(f"{indent}/// <exception cref=\"ArgumentNullException\">Thrown when a required parameter is null.</exception>")
            xml_lines.append(f"{indent}/// <exception cref=\"ObjectDisposedException\">Thrown when a parameter has been disposed.</exception>")
        xml_lines.append(f"{indent}/// <exception cref=\"OpenCVException\">Thrown when the underlying OpenCV native call fails.</exception>")
    
    if remarks_lines:
        xml_lines.append(f"{indent}/// <remarks>")
        for rl in remarks_lines:
            rl_esc = rl.replace('&', '&amp;').replace('<', '&lt;').replace('>', '&gt;')
            xml_lines.append(f"{indent}/// {rl_esc}")
        xml_lines.append(f"{indent}/// </remarks>")
        
    return "\n".join(xml_lines)

def clean_type(tp):
    tp = tp.strip()
    tp = tp.replace("const", "").replace("&", "").strip()
    return tp

def to_pascal_case(s):
    s = s.strip('_')
    if '_' not in s:
        if s.isupper():
            return s.capitalize()
        return s[0].upper() + s[1:] if s else ""
    parts = s.split('_')
    if all(p.isupper() for p in parts if any(c.isalpha() for c in p)):
        return "".join(p.capitalize() for p in parts if p)
    return "".join(p[0].upper() + p[1:] if p else "" for p in parts if p)

def sanitize_identifier(name):
    # Replace C++ operator symbols with text
    ops = {
        "operator[]": "operator_get",
        "operator()": "operator_call",
        "operator==": "operator_equals",
        "operator!=": "operator_notequals",
        "operator+": "operator_add",
        "operator-": "operator_sub",
        "operator*": "operator_mul",
        "operator/": "operator_div",
        "operator<": "operator_lt",
        "operator>": "operator_gt",
        "operator<=": "operator_le",
        "operator>=": "operator_ge",
        "operator=": "operator_assign",
    }
    for op, repl in ops.items():
        name = name.replace(op, repl)
    # Remove any other characters that are not alphanumeric or underscore
    name = re.sub(r'[^a-zA-Z0-9_]', '_', name)
    return name

def sanitize_csharp_argument_name(name):
    keywords = {
        "params", "object", "string", "ref", "out", "event", "delegate",
        "base", "this", "class", "struct", "interface", "enum", "default",
        "operator", "checked", "unchecked", "fixed", "volatile", "implicit",
        "explicit", "lock", "virtual", "override", "new", "namespace",
        "public", "private", "protected", "internal", "static", "readonly",
        "const", "extern", "sealed", "abstract", "unsafe", "in", "as", "is"
    }
    if name in keywords:
        return "@" + name
    return name

def map_parser_type_to_cpp(tp, cls_name=None):
    tp = tp.strip()
    if tp == "LayerId" or tp == "cv::LayerId" or tp == "Net::LayerId" or tp == "Net.LayerId":
        return "cv::dnn::DictValue"
    if tp == "Target" or tp == "cv::Target":
        return "cv::dnn::Target"
    if "flann_distance_t" in tp:
        return "cvflann::flann_distance_t"
    if "flann_algorithm_t" in tp:
        return "cvflann::flann_algorithm_t"
    # Strip type specifier keywords
    tp = re.sub(r'^(class|struct|enum class|enum)\s+', '', tp)
    # Normalize dots to C++ double colons
    tp = tp.replace(".", "::")
    
    # If cls_name is provided, check if tp is a nested enum/class
    if cls_name:
        cls_clean = cls_name.replace("cv::", "").replace("cv.", "").replace("::", ".").strip()
        nested_name = f"{cls_clean}.{tp}"
        if nested_name in ALL_CLASSES_AND_ENUMS:
            tp = f"{cls_clean}::{tp}"
    
    # Strip cv:: and cv. namespaces for standard primitives
    base_tp = tp.replace("const", "").replace("&", "").replace("*", "").strip()
    base_tp_clean = base_tp.replace("cv::", "").replace("std::", "").strip()
    
    primitives = ["int", "double", "float", "bool", "char", "uchar", "void", "size_t", "int64", "uint64", "long", "short", "unsigned", "signed", "c_string",
                  "int64_t", "uint64_t", "int32_t", "uint32_t", "int16_t", "uint16_t", "int8_t", "uint8_t"]
    is_primitive = False
    for p in primitives:
        if base_tp_clean == p or base_tp_clean.startswith(p + " ") or base_tp_clean.endswith(" " + p) or (" " + p + " ") in base_tp_clean:
            is_primitive = True
            break
            
    # Resolve namespace/nested classes if we can
    if not is_primitive and "::" not in base_tp_clean and not base_tp_clean.startswith("std::"):
        ns_hint = None
        if cls_name:
            cls_clean = cls_name.replace("cv::", "").replace("cv.", "").strip()
            parts = cls_clean.split('.')
            if len(parts) > 1:
                ns_hint = parts[0]
        
        # Replace underscores with dots to check for flat-style class/struct names (e.g. GpuMat_Allocator -> GpuMat.Allocator)
        base_tp_dots = base_tp_clean.replace("_", ".")
        
        matches = []
        for item in ALL_CLASSES_AND_ENUMS:
            if item == base_tp_clean or item == base_tp_dots or item.endswith("." + base_tp_clean) or item.endswith("." + base_tp_dots):
                matches.append(item)
                
        if matches:
            resolved = None
            if len(matches) > 1 and ns_hint:
                for m in matches:
                    if m.startswith(ns_hint + "."):
                        resolved = m
                        if resolved.startswith("cvflann."):
                            resolved = resolved.replace(".", "::")
                        else:
                            resolved = "cv::" + resolved.replace(".", "::")
                        break
            if not resolved:
                resolved = matches[0]
                if resolved.startswith("cvflann."):
                    resolved = resolved.replace(".", "::")
                else:
                    resolved = "cv::" + resolved.replace(".", "::")
            
            tp = tp.replace(base_tp, resolved)
            base_tp = tp.replace("const", "").replace("&", "").replace("*", "").strip()
            base_tp_clean = base_tp.replace("cv::", "").replace("std::", "").strip()

    if is_primitive:
        res = tp.replace("cv::", "").replace("cv.", "")
        if "c_string" in res:
            res = res.replace("c_string", "const char*")
        return res
        
    # 1. Handle Ptr_...
    if tp.startswith("Ptr_"):
        inner = tp[4:]
        inner_cpp = map_parser_type_to_cpp(inner, cls_name)
        return f"cv::Ptr<{inner_cpp}>"
        
    # 2. Handle vector_vector_...
    if tp.startswith("vector_vector_"):
        inner = tp[14:]
        inner_cpp = map_parser_type_to_cpp(inner, cls_name)
        return f"std::vector<std::vector<{inner_cpp}>>"
        
    # 3. Handle vector_...
    if tp.startswith("vector_"):
        inner = tp[7:]
        inner_cpp = map_parser_type_to_cpp(inner, cls_name)
        return f"std::vector<{inner_cpp}>"
        
    # 4. Handle basic types
    if tp in ["int", "double", "float", "bool", "char", "uchar", "void", "size_t", "int64", "uint64"]:
        return tp
        
    if tp in ["String", "std::string", "string"]:
        return "cv::String"
        
    # Standard OpenCV types
    standard_types = ["Mat", "UMat", "VideoCapture", "VideoWriter", "Algorithm", "Feature2D", "CascadeClassifier", 
                      "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", 
                      "Size2f", "Point2f", "Rect2f", "Point2d", "Size2d", "Rect2d",
                      "DMatch", "KeyPoint", "FileNode", "FileStorage"]
                      
    if tp in standard_types:
        return "cv::" + tp
        
    for std_tp in standard_types:
        if tp.lower() == std_tp.lower():
            return "cv::" + std_tp
            
    # Check if this type maps to a custom module namespace
    module = CLASS_TO_MODULE.get(base_tp_clean)
    if module in MODULE_NAMESPACES:
        ns = MODULE_NAMESPACES[module]
        tp_clean = tp
        if tp_clean.startswith("cv::"):
            tp_clean = tp_clean[4:]
        if not tp.startswith(ns) and not tp.startswith("cv::" + ns):
            return f"{ns}::{tp_clean}"
    if tp.startswith("cv::") or tp.startswith("std::") or tp.startswith("cvflann::"):
        return tp
        
    if "cv::" in tp or "std::" in tp or "cvflann::" in tp:
        return tp

    # Check if it has namespaces/nested classes
    if "_" in tp:
        parts = tp.split("_")
        mapped_parts = [map_parser_type_to_cpp(p, cls_name) for p in parts]
        clean_parts = []
        for i, p in enumerate(mapped_parts):
            if i > 0 and p.startswith("cv::"):
                clean_parts.append(p[4:])
            else:
                clean_parts.append(p)
        return "::".join(clean_parts)
        
    return "cv::" + tp

class OpenCVWrapperGenerator:
    def __init__(self, opencv_dir, workspace_dir, verbose=False):
        self.opencv_dir = opencv_dir
        self.workspace_dir = workspace_dir
        self.verbose = verbose
        self.skipped_items = []
        self.parser = hdr_parser.CppHeaderParser(preprocessor_definitions={
            "CV_VERSION_MAJOR": 5,
            "CV_VERSION_MINOR": 0,
            "OPENCV_ABI_COMPATIBILITY": 500,
            "CV_NEON": 0,
            "CV_VSX": 0,
            "CV__EXCEPTION_PTR": 0,
            "OPENCV_HAVE_FILESYSTEM_SUPPORT": 0,
            "OPENCV_SUPPORTS_FP_DENORMALS_HINT": 0,
            "CV_LOG_STRIP_LEVEL": 0,
            "CV_LOG_LEVEL_INFO": 1,
            "CV_LOG_LEVEL_DEBUG": 2,
            "CV_LOG_LEVEL_VERBOSE": 3,
            "TBB_INTERFACE_VERSION": 0,
            "EIGEN_WORLD_VERSION": 0,
            "EIGEN_MAJOR_VERSION": 0
        })
        self.classes = {}
        self.enums = {}
        self.functions = []
        self.funcs = []
        
    def find_headers(self):
        all_headers = []
        modules_dir = os.path.join(self.opencv_dir, "modules")
        for module in os.listdir(modules_dir):
            module_path = os.path.join(modules_dir, module)
            if not os.path.isdir(module_path):
                continue
            include_path = os.path.join(module_path, "include")
            if not os.path.exists(include_path):
                continue
            for root, dirs, files in os.walk(include_path):
                for f in files:
                    if f.endswith(".hpp") or f.endswith(".h"):
                        all_headers.append(os.path.join(root, f))
                        
        # Filter headers
        filtered_headers = []
        for h in all_headers:
            h_match = h.replace("\\", "/")
            if "/private" in h_match or "/legacy/" in h_match or "/cuda/" in h_match or "/opencl/" in h_match or "/hal/" in h_match:
                continue
            if h_match.endswith(".inl.hpp") or h_match.endswith("_inl.hpp") or ".details" in h_match or "fast_math.hpp" in h_match or "trace.hpp" in h_match:
                continue
            if h_match.endswith(".h") and not h_match.endswith(".hpp"):
                continue
            if "bindings_utils.hpp" in h_match:
                continue
            filtered_headers.append(h)
            
        # Add shadow_mat.hpp
        shadow_mat = os.path.join(self.workspace_dir, "src", "OpenCV5Sharp.Generator", "shadow_mat.hpp")
        if os.path.exists(shadow_mat):
            filtered_headers.append(shadow_mat)
            
        return filtered_headers

    def parse_all(self):
        headers = self.find_headers()
        print(f"Found {len(headers)} headers to parse.")
        
        all_decls = []
        for h in headers:
            match = re.search(r'[/\\]modules[/\\]([^/\\]+)', h)
            module = match.group(1) if match else "core"
            try:
                decls = self.parser.parse(h)
                for decl in decls:
                    d_list = list(decl)
                    while len(d_list) < 6:
                        d_list.append("")
                    d_list.append(module)
                    all_decls.append(d_list)
                    
                    name = d_list[0]
                    if name.startswith("class ") or name.startswith("struct ") or name.startswith("enum "):
                        if name.startswith("enum class "):
                            prefix = "enum class "
                        elif name.startswith("enum "):
                            prefix = "enum "
                        elif name.startswith("class "):
                            prefix = "class "
                        else:
                            prefix = "struct "
                        t_name = name[len(prefix):].strip()
                        ALL_CLASSES_AND_ENUMS.add(t_name.replace("cv.", "").strip())
                        
                        cls_name_clean = t_name.replace("cv.", "").replace(".", "_")
                        CLASS_TO_MODULE[cls_name_clean] = module
                        if "_" in cls_name_clean:
                            suffix = cls_name_clean.split("_")[-1]
                            CLASS_TO_MODULE[suffix] = module
            except Exception as e:
                self.skipped_items.append((h, str(e)))
                if self.verbose:
                    print(f"  [SKIP] Failed to parse {h}: {e}", file=sys.stderr)
                
        print(f"Total declarations parsed: {len(all_decls)}")
        
        # Categorize Pass 1: Parse all class and struct declarations
        for decl in all_decls:
            name = decl[0]
            if name.startswith("class ") or name.startswith("struct "):
                is_class = name.startswith("class ")
                prefix = "class " if is_class else "struct "
                cls_name = name[len(prefix):].strip()
                base_class = None
                if decl[1] and isinstance(decl[1], str) and decl[1].strip().startswith(":"):
                    raw_inherits = decl[1].strip().lstrip(":").strip()
                    first_base = raw_inherits.split(",")[0].strip()
                    if first_base:
                        base_class = first_base
                self.classes[cls_name] = {
                    "name": cls_name,
                    "methods": [],
                    "props": decl[3],
                    "base": base_class,
                    "is_struct": not is_class,
                    "doc": decl[5],
                    "module": decl[6]
                }
            elif name.startswith("enum "):
                enum_name = name[len("enum "):].strip()
                if enum_name.startswith("class "):
                    enum_name = enum_name[len("class "):].strip()
                elif enum_name.startswith("struct "):
                    enum_name = enum_name[len("struct "):].strip()
                self.enums[enum_name] = {
                    "members": decl[3],
                    "doc": decl[5],
                    "module": decl[6]
                }

        # Categorize Pass 2: Parse methods and functions
        for decl in all_decls:
            name = decl[0]
            if name.startswith("class ") or name.startswith("struct ") or name.startswith("enum "):
                continue
            
            parts = name.split('.')
            if len(parts) == 2:
                # Global function (cv.foo)
                self.funcs.append(decl)
            elif len(parts) > 2:
                # Class method (cv.Class.method) or Namespace function (cv.Namespace.func)
                cls_name = ".".join(parts[:-1])
                method_name = parts[-1]
                if cls_name in self.classes:
                    self.classes[cls_name]["methods"].append(decl)
                else:
                    # Treat as global/namespace function
                    self.funcs.append(decl)
                    
        # Add stub classes for un-exported but required classes
        stub_classes = [
            ("cv.dnn.LayerParams", "dnn"),
            ("cv.dnn.DictValue", "dnn"),
            ("cv.dnn.Dict", "dnn"),
            ("cv.flann.IndexParams", "flann"),
            ("cv.flann.SearchParams", "flann"),
            ("cv.ocl.Device", "core")
        ]
        for stub_name, module in stub_classes:
            if stub_name not in self.classes:
                self.classes[stub_name] = {
                    "name": stub_name,
                    "methods": [
                        [f"{stub_name}.{stub_name.split('.')[-1]}", stub_name, ["/Co"], [], None, "", module]
                    ],
                    "props": [],
                    "base": None,
                    "is_struct": False,
                    "doc": f"Stub class representing {stub_name}.",
                    "module": module
                }
                ALL_CLASSES_AND_ENUMS.add(stub_name.replace("cv.", "").strip())
                clean_key = stub_name.replace("cv.", "").replace(".", "_")
                CLASS_TO_MODULE[clean_key] = module

        ALL_CLASSES_AND_ENUMS.add("cvflann.flann_distance_t")
        ALL_CLASSES_AND_ENUMS.add("cvflann.flann_algorithm_t")

        print(f"Categorized: {len(self.classes)} classes, {len(self.enums)} enums, {len(self.funcs)} global functions.")

    def is_enum_type(self, tp):
        ctp = clean_type(tp).replace("const", "").replace("&", "").strip()
        ctp_clean = ctp.replace("cv::", "").replace("cv.", "").replace("::", ".").strip()
        for key in self.enums:
            key_clean = key.replace("cv.", "").strip()
            if key_clean == ctp_clean or key_clean.endswith("." + ctp_clean):
                return True
        return False

    def get_c_flat_type(self, tp):
        ctp = clean_type(tp)
        if self.is_enum_type(tp):
            return "int"
        if ctp in ["int", "double", "float", "bool", "char", "uchar", "void", "size_t", "int64", "uint64",
                    "int64_t", "uint64_t", "int32_t", "uint32_t", "int16_t", "uint16_t", "int8_t", "uint8_t"]:
            return ctp
        if ctp in ["String", "std::string", "string", "c_string"]:
            return "const char*"
        if ctp in ["Size", "Point", "Rect", "Scalar", "Range", "TermCriteria"]:
            return "cv::" + ctp
        if ctp == "Size2f" or ctp == "Size2F":
            return "cv::Size2f"
        if ctp == "Point2f" or ctp == "Point2F":
            return "cv::Point2f"
        if ctp == "Rect2f" or ctp == "Rect2F":
            return "cv::Rect2f"
        return "void*"

    def get_csharp_type(self, tp, is_parameter=False, is_return=False):
        ctp = clean_type(tp)
        if self.is_enum_type(tp):
            return "int"
        if ctp == "bool":
            return "[MarshalAs(UnmanagedType.U1)] bool" if is_parameter else "bool"
        if ctp in ["int", "double", "float", "void"]:
            return ctp
        if ctp in ["char", "uchar", "uint8_t"]:
            return "byte"
        if ctp == "int8_t":
            return "sbyte"
        if ctp in ["size_t", "int64", "uint64", "int64_t", "uint64_t"]:
            return "long"
        if ctp in ["int32_t", "uint32_t"]:
            return "int"
        if ctp in ["int16_t", "uint16_t"]:
            return "short"
        if ctp in ["String", "std::string", "string", "c_string"]:
            return "IntPtr" if is_return else ("[MarshalAs(UnmanagedType.LPUTF8Str)] string" if is_parameter else "string")
        if ctp in ["Size", "Point", "Rect", "Scalar", "Range", "TermCriteria"]:
            return ctp
        if ctp == "Size2f" or ctp == "Size2F":
            return "Size2F"
        if ctp == "Point2f" or ctp == "Point2F":
            return "Point2F"
        if ctp == "Rect2f" or ctp == "Rect2F":
            return "Rect2F"
        return "IntPtr"

    def get_user_facing_csharp_type(self, tp):
        ctp = clean_type(tp)
        if self.is_enum_type(tp):
            ctp_clean = ctp.replace("cv::", "").replace("cv.", "").replace("::", ".").strip()
            for key in self.enums:
                key_clean = key.replace("cv.", "").strip()
                if key_clean == ctp_clean or key_clean.endswith("." + ctp_clean):
                    clean = key.replace("cv.", "").replace(".", "_")
                    parts = clean.split('_')
                    return "".join(to_pascal_case(p) for p in parts if p)
            return "int"
        if ctp in ["int", "double", "float", "bool", "void"]:
            return ctp
        if ctp in ["char", "uchar", "uint8_t"]:
            return "byte"
        if ctp == "int8_t":
            return "sbyte"
        if ctp in ["size_t", "int64", "uint64", "int64_t", "uint64_t"]:
            return "long"
        if ctp in ["int32_t", "uint32_t"]:
            return "int"
        if ctp in ["int16_t", "uint16_t"]:
            return "short"
        if ctp in ["String", "std::string", "string", "c_string"]:
            return "string"
        if ctp in ["Size", "Point", "Rect", "Scalar", "Range", "TermCriteria"]:
            return ctp
        if ctp == "Size2f" or ctp == "Size2F":
            return "Size2F"
        if ctp == "Point2f" or ctp == "Point2F":
            return "Point2F"
        if ctp == "Rect2f" or ctp == "Rect2F":
            return "Rect2F"
            
        if ctp.startswith("Ptr_"):
            ctp = ctp[4:]
        if ctp.startswith("vector_"):
            return "IntPtr"
            
        # Resolve via ALL_CLASSES_AND_ENUMS
        for item in ALL_CLASSES_AND_ENUMS:
            if item == ctp or item.endswith("." + ctp):
                clean = item.replace("cv.", "").replace(".", "_")
                parts = clean.split('_')
                return "".join(to_pascal_case(p) for p in parts if p)
                
        return "IntPtr"

    def get_cpp_type(self, tp, cls_name=None):
        return map_parser_type_to_cpp(tp, cls_name)

    def get_user_facing_csharp_type_nullable(self, tp, is_optional=False):
        t = self.get_user_facing_csharp_type(tp)
        if t == "string":
            return "string?" if is_optional else "string"
        if t in self.generated_class_names:
            return t + "?" if is_optional else t
        return t

    def get_user_facing_csharp_return_type_nullable(self, tp):
        t = self.get_user_facing_csharp_type(tp)
        if t == "string":
            return "string?"
        if t in self.generated_class_names:
            return t + "?"
        return t

    def has_ancestor_method(self, cls_name, method_name, arg_types):
        curr_cls = cls_name
        while True:
            cls_info = self.classes.get(curr_cls)
            if not cls_info:
                break
            base_cls = cls_info.get("base")
            if not base_cls:
                break
            base_key = base_cls.replace("cv::", "cv.").replace("::", ".")
            base_info = self.classes.get(base_key)
            if not base_info:
                clean_base = base_cls.replace("cv::", "").replace("cv.", "").replace("::", "_").replace(".", "_")
                for k in self.classes:
                    if k.replace("cv.", "").replace(".", "_") == clean_base:
                        base_info = self.classes[k]
                        base_key = k
                        break
            if not base_info:
                break
            for m_decl in base_info["methods"]:
                m_name = m_decl[0].split('.')[-1]
                if m_name == method_name:
                    base_args = m_decl[3]
                    if len(base_args) == len(arg_types):
                        match = True
                        for i in range(len(arg_types)):
                            t1 = arg_types[i].replace("const", "").replace("&", "").replace("*", "").strip()
                            t2 = base_args[i][0].replace("const", "").replace("&", "").replace("*", "").strip()
                            if t1 != t2:
                                match = False
                                break
                        if match:
                            return True
            curr_cls = base_key
        return False

    def generate(self):
        # Gather all generated class names
        self.generated_class_names = set()
        for cls_name in self.classes:
            clean_cls_name = cls_name.replace("cv.", "").replace(".", "_")
            if not clean_cls_name or "IStreamReader" in clean_cls_name:
                continue
            pascal_cls_name = "".join(to_pascal_case(p) for p in clean_cls_name.split("_") if p)
            self.generated_class_names.add(pascal_cls_name)

        # Collect all classes returned via cv::Ptr
        ptr_classes = set()
        for decl in self.funcs:
            ret_type = decl[1]
            if ret_type.startswith("Ptr_"):
                ptr_classes.add(ret_type[4:])
        for c_name, info in self.classes.items():
            for decl in info["methods"]:
                ret_type = decl[1]
                if ret_type.startswith("Ptr_"):
                    ptr_classes.add(ret_type[4:])

        core_macros = {
            "CV_8U": "0",
            "CV_8S": "1",
            "CV_16U": "2",
            "CV_16S": "3",
            "CV_32S": "4",
            "CV_32F": "5",
            "CV_64F": "6",
            "CV_16F": "7",
            "CV_16BF": "8",
            "CV_Bool": "9",
            "CV_64U": "10",
            "CV_64S": "11",
            "CV_32U": "12",
            "INT_MAX": "2147483647",
            "CV_DEPTH_CURR_MAX": "13",
        }
        # Let's generate native C++ files first
        cpp_h_path = os.path.join(self.workspace_dir, "src", "OpenCV5Sharp.Native", "opencv5sharp_native.h")
        cpp_cpp_path = os.path.join(self.workspace_dir, "src", "OpenCV5Sharp.Native", "opencv5sharp_native.cpp")
        
        # Track name occurrences to append overloads
        c_names = {}
        def get_unique_c_name(base_name):
            if base_name not in c_names:
                c_names[base_name] = 0
                return f"{base_name}_0"
            c_names[base_name] += 1
            return f"{base_name}_{c_names[base_name]}"

        cpp_decls = []
        cpp_impls = []
        
        # Dictionary containers for C# files by module
        classes_by_module = {}
        enums_by_module = {}
        cv2_methods_by_module = {}
        native_methods_by_module = {}
        
        # Add global error handling support with thread-local error buffer
        cpp_decls.append('extern "C" __declspec(dllexport) const char* opencv5sharp_getLastError();')
        cpp_decls.append('extern "C" __declspec(dllexport) int opencv5sharp_getLastErrorCode();')
        cpp_decls.append('extern "C" __declspec(dllexport) void opencv5sharp_clearLastError();')
        cpp_impls.append('static thread_local char _lastError[4096] = {0};')
        cpp_impls.append('static thread_local int _lastErrorCode = 0;')
        cpp_impls.append('static void _setError(int code, const char* msg) {')
        cpp_impls.append('    _lastErrorCode = code;')
        cpp_impls.append('    strncpy(_lastError, msg, sizeof(_lastError) - 1);')
        cpp_impls.append('    _lastError[sizeof(_lastError) - 1] = 0;')
        cpp_impls.append('}')
        cpp_impls.append('static void _clearError() { _lastError[0] = 0; _lastErrorCode = 0; }')
        cpp_impls.append('extern "C" __declspec(dllexport) const char* opencv5sharp_getLastError() { return _lastError; }')
        cpp_impls.append('extern "C" __declspec(dllexport) int opencv5sharp_getLastErrorCode() { return _lastErrorCode; }')
        cpp_impls.append('extern "C" __declspec(dllexport) void opencv5sharp_clearLastError() { _clearError(); }')

        # String free function
        cpp_decls.append('extern "C" __declspec(dllexport) void cv_FreeString(char* ptr);')
        cpp_impls.append('extern "C" __declspec(dllexport) void cv_FreeString(char* ptr) { free(ptr); }')

        # VectorInt helper functions
        cpp_decls.append('extern "C" __declspec(dllexport) void cv_VectorInt_Delete(void* ptr);')
        cpp_decls.append('extern "C" __declspec(dllexport) int cv_VectorInt_Size(void* ptr);')
        cpp_decls.append('extern "C" __declspec(dllexport) void cv_VectorInt_GetData(void* ptr, int* outBuf);')
        cpp_decls.append('extern "C" __declspec(dllexport) void* cv_VectorInt_New(int* data, int size);')
        
        cpp_impls.append('extern "C" __declspec(dllexport) void cv_VectorInt_Delete(void* ptr) { delete (std::vector<int>*)ptr; }')
        cpp_impls.append('extern "C" __declspec(dllexport) int cv_VectorInt_Size(void* ptr) { return ptr ? (int)((std::vector<int>*)ptr)->size() : 0; }')
        cpp_impls.append('extern "C" __declspec(dllexport) void cv_VectorInt_GetData(void* ptr, int* outBuf) { if (ptr && outBuf) { auto& vec = *(std::vector<int>*)ptr; std::copy(vec.begin(), vec.end(), outBuf); } }')
        cpp_impls.append('extern "C" __declspec(dllexport) void* cv_VectorInt_New(int* data, int size) { return new std::vector<int>(data, data + size); }')

        # VectorMat helper functions
        cpp_decls.append('extern "C" __declspec(dllexport) void cv_VectorMat_Delete(void* ptr);')
        cpp_decls.append('extern "C" __declspec(dllexport) int cv_VectorMat_Size(void* ptr);')
        cpp_decls.append('extern "C" __declspec(dllexport) void* cv_VectorMat_GetElement(void* ptr, int index);')
        cpp_decls.append('extern "C" __declspec(dllexport) void* cv_VectorMat_New(void** mats, int size);')
        
        cpp_impls.append('extern "C" __declspec(dllexport) void cv_VectorMat_Delete(void* ptr) { delete (std::vector<cv::Mat>*)ptr; }')
        cpp_impls.append('extern "C" __declspec(dllexport) int cv_VectorMat_Size(void* ptr) { return ptr ? (int)((std::vector<cv::Mat>*)ptr)->size() : 0; }')
        cpp_impls.append('extern "C" __declspec(dllexport) void* cv_VectorMat_GetElement(void* ptr, int index) { if (!ptr) return nullptr; auto& vec = *(std::vector<cv::Mat>*)ptr; return new cv::Mat(vec[index]); }')
        cpp_impls.append('extern "C" __declspec(dllexport) void* cv_VectorMat_New(void** mats, int size) { auto* vec = new std::vector<cv::Mat>(); vec->reserve(size); for (int i = 0; i < size; i++) { if (mats && mats[i]) { vec->push_back(*(cv::Mat*)mats[i]); } else { vec->push_back(cv::Mat()); } } return vec; }')

        core_native = [
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern IntPtr opencv5sharp_getLastError();',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern int opencv5sharp_getLastErrorCode();',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern void opencv5sharp_clearLastError();',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern void cv_FreeString(IntPtr ptr);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern void cv_VectorInt_Delete(IntPtr ptr);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern int cv_VectorInt_Size(IntPtr ptr);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern void cv_VectorInt_GetData(IntPtr ptr, [Out] int[] outBuf);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern IntPtr cv_VectorInt_New(int[] data, int size);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern void cv_VectorMat_Delete(IntPtr ptr);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern int cv_VectorMat_Size(IntPtr ptr);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern IntPtr cv_VectorMat_GetElement(IntPtr ptr, int index);',
            '[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]',
            'public static extern IntPtr cv_VectorMat_New(IntPtr[] mats, int size);'
        ]
        native_methods_by_module["Core"] = core_native

        # 1. Generate Enums
        unnamed_counter = 0
        for enum_name, enum_info in sorted(self.enums.items()):
            members = enum_info["members"]
            doc = enum_info["doc"]
            mod = enum_info.get("module", "core")
            mod_norm = normalize_module_name(mod)
            
            if mod_norm not in enums_by_module:
                enums_by_module[mod_norm] = []
                
            clean_enum_name = enum_name.replace("cv.", "").replace(".", "_")
            if not clean_enum_name or "<unnamed>" in clean_enum_name:
                suffix = clean_enum_name.replace("<unnamed>", "").strip("_")
                clean_enum_name = f"UnnamedEnum_{unnamed_counter}" + (f"_{suffix}" if suffix else "")
                unnamed_counter += 1
            
            pascal_enum_name = "".join(to_pascal_case(p) for p in clean_enum_name.split("_") if p)
            
            member_names = [m[0].split('.')[-1].replace("const cv.", "").replace("const ", "") for m in members]
            common_prefix = ""
            if len(member_names) > 1:
                s1, s2 = min(member_names), max(member_names)
                prefix_len = 0
                for c1, c2 in zip(s1, s2):
                    if c1 != c2:
                        break
                    prefix_len += 1
                common_prefix = s1[:prefix_len]
                if '_' in common_prefix:
                    common_prefix = common_prefix[:common_prefix.rfind('_') + 1]
                else:
                    common_prefix = ""
            
            local_map = {}
            for m in members:
                raw_m_name = m[0].split('.')[-1].replace("const cv.", "").replace("const ", "")
                clean_m_name = raw_m_name
                if common_prefix and clean_m_name.startswith(common_prefix):
                    clean_m_name = clean_m_name[len(common_prefix):]
                m_name = to_pascal_case(clean_m_name)
                if m_name and m_name[0].isdigit():
                    m_name = "_" + m_name
                local_map[raw_m_name] = m_name

            enums_by_module[mod_norm].append(format_xml_doc(doc, 0, is_enum=True))
            enums_by_module[mod_norm].append(f"public enum {pascal_enum_name} : int\n{{")
            
            generated_enum_members = set()
            for m in members:
                raw_m_name = m[0].split('.')[-1].replace("const cv.", "").replace("const ", "")
                m_name = local_map[raw_m_name]
                if m_name in generated_enum_members:
                    continue
                generated_enum_members.add(m_name)
                
                val = m[1]
                val = val.replace("(", "").replace(")", "").strip()
                for raw, clean in core_macros.items():
                    val = re.sub(r'\b' + re.escape(raw) + r'\b', clean, val)
                for raw, clean in local_map.items():
                    val = re.sub(r'\b' + re.escape(raw) + r'\b', clean, val)
                    
                if not re.match(r'^-?\d+$', val):
                    val = re.sub(r'(\w+)\s*([\+\-\*/])\s*(\w+)', r'\1 \2 \3', val)
                    val = f"unchecked((int)({val}))"
                    
                m_doc = m[5] if len(m) > 5 else ""
                if m_doc.strip():
                    enums_by_module[mod_norm].append(format_xml_doc(m_doc, 4))
                else:
                    enums_by_module[mod_norm].append(f"    /// <summary>{m_name}</summary>")
                enums_by_module[mod_norm].append(f"    {m_name} = {val},")
            enums_by_module[mod_norm].append("}\n")

        # 2. Generate Classes and Methods
        for cls_name, info in sorted(self.classes.items()):
            clean_cls_name = cls_name.replace("cv.", "").replace(".", "_")
            if not clean_cls_name or "IStreamReader" in clean_cls_name:
                continue
                
            pascal_cls_name = "".join(to_pascal_case(p) for p in clean_cls_name.split("_") if p)
            mod = info.get("module", "core")
            mod_norm = normalize_module_name(mod)
            
            if mod_norm not in classes_by_module:
                classes_by_module[mod_norm] = []
            if mod_norm not in native_methods_by_module:
                native_methods_by_module[mod_norm] = []
                
            cs_class_methods = []
            generated_ctors = {("IntPtr",)}
            generated_methods = set()
            
            delete_func_name = f"{clean_cls_name}_Delete"
            cpp_decls.append(f'extern "C" __declspec(dllexport) void {delete_func_name}(void* self);')
            cpp_impls.append(f'extern "C" __declspec(dllexport) void {delete_func_name}(void* self) {{')
            cpp_impls.append(f'    if (!self) return;')
            cls_cpp_name = self.get_cpp_type(cls_name)
            cpp_impls.append('    try {')
            if cls_name.split('.')[-1] in ptr_classes:
                cpp_impls.append(f'        delete (cv::Ptr<{cls_cpp_name}>*)self;')
            else:
                cpp_impls.append(f'        delete ({cls_cpp_name}*)self;')
            cpp_impls.append('    } catch (const cv::Exception& e) {')
            cpp_impls.append('        _setError(e.code, e.what());')
            cpp_impls.append('    } catch (const std::exception& e) {')
            cpp_impls.append('        _setError(-2, e.what());')
            cpp_impls.append('    } catch (...) {')
            cpp_impls.append('        _setError(-3, "Unknown native exception");')
            cpp_impls.append('    }')
            cpp_impls.append('}')
            
            native_methods_by_module[mod_norm].append(f'[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]')
            native_methods_by_module[mod_norm].append(f'public static extern void {delete_func_name}(IntPtr self);')
            
            # Generate Methods
            for decl in info["methods"]:
                name = decl[0]
                ret_type = decl[1]
                args = decl[3]
                doc = decl[5]
                
                if "NativeByteArray" in ret_type or any("NativeByteArray" in arg[0] for arg in args):
                    continue
                if "IStreamReader" in ret_type or any("IStreamReader" in arg[0] for arg in args):
                    continue
                
                parts = name.split('.')
                subname = parts[-1]
                is_constructor = (subname == parts[-2])
                sanitized_subname = sanitize_identifier(subname)
                is_static = '/S' in decl[2]
                
                if is_constructor:
                    ctor_sig = tuple(self.get_user_facing_csharp_type(arg[0]) for arg in args)
                    if ctor_sig in generated_ctors:
                        continue
                    generated_ctors.add(ctor_sig)
                else:
                    method_sig = (to_pascal_case(sanitized_subname), tuple(self.get_user_facing_csharp_type(arg[0]) for arg in args))
                    if method_sig in generated_methods:
                        continue
                    generated_methods.add(method_sig)
                
                base_c_name = f"{clean_cls_name}_New" if is_constructor else f"{clean_cls_name}_{subname}"
                flat_name = get_unique_c_name(sanitize_identifier(base_c_name))
                
                cpp_args_decl = []
                cpp_args_call = []
                cpp_setup_statements = []
                
                if not is_constructor and not is_static:
                    cpp_args_decl.append("void* self")
                    
                for i, arg in enumerate(args):
                    arg_tp, arg_name, arg_def, arg_mods = arg[0], arg[1], arg[2], arg[3]
                    cpp_args_decl.append(f"{self.get_c_flat_type(arg_tp)} {arg_name}")
                    
                    clean_tp = clean_type(arg_tp)
                    cpp_tp = self.get_cpp_type(arg_tp, cls_name)
                    if self.get_c_flat_type(arg_tp) == "void*" and clean_tp != "void":
                        if arg_tp.strip().endswith("*"):
                            cpp_args_call.append(f"({cpp_tp}){arg_name}")
                        else:
                            cpp_type_no_ref = cpp_tp.replace("&", "").strip()
                            if "const" in arg_tp or "vector" in arg_tp or arg_def:
                                default_var = f"default_{arg_name}"
                                ptr_var = f"ptr_{arg_name}"
                                cpp_setup_statements.append(f"    {cpp_type_no_ref} {default_var};")
                                cpp_setup_statements.append(f"    {cpp_type_no_ref}* {ptr_var} = {arg_name} ? ({cpp_type_no_ref}*){arg_name} : &{default_var};")
                                cpp_args_call.append(f"*{ptr_var}")
                            else:
                                cpp_args_call.append(f"*({cpp_type_no_ref}*){arg_name}")
                    elif clean_tp in ["String", "std::string", "string"]:
                        cpp_args_call.append(f"cv::String({arg_name})")
                    elif self.is_enum_type(arg_tp):
                        cpp_args_call.append(f"({cpp_tp}){arg_name}")
                    else:
                        cpp_args_call.append(arg_name)
                        
                c_ret = self.get_c_flat_type(ret_type) if not is_constructor else "void*"
                cpp_decls.append(f'extern "C" __declspec(dllexport) {c_ret} {flat_name}({", ".join(cpp_args_decl)});')
                
                cpp_impls.append(f'extern "C" __declspec(dllexport) {c_ret} {flat_name}({", ".join(cpp_args_decl)}) {{')
                cpp_impls.append('    _clearError();')
                
                ret_expr = "" if c_ret == "void" else " {}"
                if not is_constructor and not is_static:
                    cpp_impls.append(f'    if (!self) {{ _setError(-1, "Null pointer: self"); return{ret_expr}; }}')
                for arg in args:
                    arg_tp, arg_name, arg_def = arg[0], arg[1], arg[2]
                    clean_tp = clean_type(arg_tp)
                    if self.get_c_flat_type(arg_tp) == "void*" and clean_tp != "void" and not arg_def:
                        cpp_impls.append(f'    if (!{arg_name}) {{ _setError(-1, "Null pointer: {arg_name}"); return{ret_expr}; }}')
                for stmt in cpp_setup_statements:
                    cpp_impls.append(stmt)
                
                cpp_impls.append('    try {')
                if is_constructor:
                    cpp_impls.append(f'        return new {cls_cpp_name}({", ".join(cpp_args_call)});')
                else:
                    if is_static:
                        fcall = f'{cls_cpp_name}::{subname}({", ".join(cpp_args_call)})'
                    else:
                        if cls_name.split('.')[-1] in ptr_classes:
                            self_cast = f"(*((cv::Ptr<{cls_cpp_name}>*)self))"
                        else:
                            self_cast = f"(({cls_cpp_name}*)self)"
                        fcall = f'{self_cast}->{subname}({", ".join(cpp_args_call)})'
                    
                    cl_ret = clean_type(ret_type)
                    if c_ret == "void":
                        cpp_impls.append(f'        {fcall};')
                    elif cl_ret in ["String", "std::string", "string"]:
                        cpp_impls.append(f'        return _strdup({fcall}.c_str());')
                    elif c_ret == "void*":
                        ret_cpp = self.get_cpp_type(ret_type, cls_name)
                        if ret_type.strip().endswith("*"):
                            cpp_impls.append(f'        return {fcall};')
                        else:
                            cpp_impls.append(f'        return new {ret_cpp}({fcall});')
                    else:
                        cpp_impls.append(f'        return {fcall};')
                cpp_impls.append('    } catch (const cv::Exception& e) {')
                cpp_impls.append('        _setError(e.code, e.what());')
                cpp_impls.append('    } catch (const std::exception& e) {')
                cpp_impls.append('        _setError(-2, e.what());')
                cpp_impls.append('    } catch (...) {')
                cpp_impls.append('        _setError(-3, "Unknown native exception");')
                cpp_impls.append('    }')
                if c_ret == 'void':
                    pass
                else:
                    cpp_impls.append('    return {};')
                cpp_impls.append('}')
                
                cs_args_decl = []
                if not is_constructor and not is_static:
                    cs_args_decl.append("IntPtr self")
                cs_args_decl.extend([f"{self.get_csharp_type(arg[0], is_parameter=True)} {sanitize_csharp_argument_name(arg[1])}" for arg in args])
                
                native_methods_by_module[mod_norm].append(f'[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]')
                if clean_type(ret_type) == "bool" and not is_constructor:
                    native_methods_by_module[mod_norm].append('[return: MarshalAs(UnmanagedType.U1)]')
                native_methods_by_module[mod_norm].append(f'public static extern {self.get_csharp_type(ret_type, is_return=True) if not is_constructor else "IntPtr"} {flat_name}({", ".join(cs_args_decl)});')
                
                # C# Class Method Implementation
                sanitized_subname = sanitize_identifier(subname)
                has_disposable = any(self.get_user_facing_csharp_type(a[0]) in self.generated_class_names for a in args)
                if is_constructor:
                    cs_class_methods.append(format_xml_doc(doc, 4, args, has_disposable=has_disposable))
                    cs_class_methods.append(f'    public {pascal_cls_name}({", ".join([f"{self.get_user_facing_csharp_type_nullable(arg[0], arg[2])} {sanitize_csharp_argument_name(arg[1])}" for arg in args])})')
                    call_args = []
                    for arg in args:
                        san_name = sanitize_csharp_argument_name(arg[1])
                        user_type = self.get_user_facing_csharp_type(arg[0])
                        if self.is_enum_type(arg[0]):
                            call_args.append(f"(int){san_name}")
                        elif user_type in self.generated_class_names:
                            is_opt = "true" if arg[2] else "false"
                            call_args.append(f"ValidationHelper.GetHandle({san_name}, nameof({san_name}), {is_opt})")
                        elif user_type not in ["void", "int", "double", "float", "bool", "byte", "long", "string", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                            call_args.append(f"{san_name} == null ? IntPtr.Zero : {san_name}.Handle")
                        else:
                            call_args.append(san_name)
                    
                    validation_expr = ""
                    if pascal_cls_name == "Mat":
                        has_rows_cols = any(a[1] == "rows" for a in args) and any(a[1] == "cols" for a in args)
                        has_size = any(a[1] == "size" for a in args)
                        if has_rows_cols:
                            validation_expr = "CheckDimensions(rows, cols"
                        elif has_size:
                            validation_expr = "CheckSize(size"

                    native_call = f"NativeMethods.{flat_name}({', '.join(call_args)})"
                    if validation_expr:
                        base_call = f"MatValidation.{validation_expr}, () => {native_call})"
                    else:
                        base_call = native_call
                    cs_class_methods.append(f'        : base({base_call})')
                    cs_class_methods.append('    {')
                    cs_class_methods.append('        ErrorHelper.CheckError();')
                    cs_class_methods.append('    }')
                else:
                    static_keyword = "static " if is_static else ""
                    pascal_name = to_pascal_case(sanitized_subname)
                    is_hiding = pascal_name in ("GetType", "ToString", "GetHashCode", "Equals") or self.has_ancestor_method(cls_name, subname, [arg[0] for arg in args])
                    new_keyword = "new " if is_hiding else ""
                    ret_user = self.get_user_facing_csharp_type(ret_type)
                    ret_user_nullable = self.get_user_facing_csharp_return_type_nullable(ret_type)
                    
                    cs_class_methods.append(format_xml_doc(doc, 4, args, ret_type, has_disposable=has_disposable))
                    cs_class_methods.append(f'    public {static_keyword}{new_keyword}{ret_user_nullable} {pascal_name}({", ".join([f"{self.get_user_facing_csharp_type_nullable(arg[0], arg[2])} {sanitize_csharp_argument_name(arg[1])}" for arg in args])})')
                    cs_class_methods.append("    {")
                    if not is_static:
                        cs_class_methods.append("        ThrowIfDisposed();")
                    call_args = []
                    if not is_static:
                        call_args.append("Handle")
                    for arg in args:
                        san_name = sanitize_csharp_argument_name(arg[1])
                        user_type = self.get_user_facing_csharp_type(arg[0])
                        if self.is_enum_type(arg[0]):
                            call_args.append(f"(int){san_name}")
                        elif user_type in self.generated_class_names:
                            is_opt = "true" if arg[2] else "false"
                            call_args.append(f"ValidationHelper.GetHandle({san_name}, nameof({san_name}), {is_opt})")
                        elif user_type not in ["void", "int", "double", "float", "bool", "byte", "long", "string", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                            call_args.append(f"{san_name} == null ? IntPtr.Zero : {san_name}.Handle")
                        else:
                            call_args.append(san_name)
                    fcall = f"NativeMethods.{flat_name}({', '.join(call_args)})"
                    
                    if ret_user == "void":
                        cs_class_methods.append(f"        {fcall};")
                        cs_class_methods.append(f"        ErrorHelper.CheckError();")
                    elif ret_user == "string":
                        cs_class_methods.append(f"        IntPtr res = {fcall};")
                        cs_class_methods.append(f"        ErrorHelper.CheckError();")
                        cs_class_methods.append(f"        if (res == IntPtr.Zero) return null;")
                        cs_class_methods.append(f"        string strRes = Marshal.PtrToStringUTF8(res);")
                        cs_class_methods.append(f"        NativeMethods.cv_FreeString(res);")
                        cs_class_methods.append(f"        return strRes;")
                    elif self.is_enum_type(ret_type):
                        cs_class_methods.append(f"        var res = {fcall};")
                        cs_class_methods.append(f"        ErrorHelper.CheckError();")
                        cs_class_methods.append(f"        return ({ret_user})res;")
                    elif ret_user not in ["int", "double", "float", "bool", "byte", "long", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                        cs_class_methods.append(f"        IntPtr res = {fcall};")
                        cs_class_methods.append(f"        ErrorHelper.CheckError();")
                        cs_class_methods.append(f"        return res == IntPtr.Zero ? null : new {ret_user}(res);")
                    else:
                        cs_class_methods.append(f"        var res = {fcall};")
                        cs_class_methods.append(f"        ErrorHelper.CheckError();")
                        cs_class_methods.append(f"        return res;")
                    cs_class_methods.append("    }")

            # Generate Properties
            for prop in info["props"]:
                prop_type, prop_name = prop[0], prop[1]
                if "NativeByteArray" in prop_type or "IStreamReader" in prop_type:
                    continue
                    
                is_readonly = (cls_name, prop_name) in READONLY_PROPERTIES or (pascal_cls_name, to_pascal_case(prop_name)) in READONLY_PROPERTIES or prop_name == "step"
                is_vector_int = (prop_type == "vector_int" or prop_type == "std::vector<int>")
                is_vector_mat = (prop_type == "vector_Mat" or prop_type == "std::vector<cv::Mat>")

                getter_name = f"{clean_cls_name}_{prop_name}_get"
                c_ret = self.get_c_flat_type(prop_type)
                cpp_decls.append(f'extern "C" __declspec(dllexport) {c_ret} {getter_name}(void* self);')
                cpp_impls.append(f'extern "C" __declspec(dllexport) {c_ret} {getter_name}(void* self) {{')
                cpp_impls.append('    _clearError();')
                cpp_impls.append('    if (!self) { _setError(-1, "Null pointer: self"); return {}; }')
                cls_cpp_name = self.get_cpp_type(cls_name)
                if cls_name.split('.')[-1] in ptr_classes:
                    self_cast = f"(*((cv::Ptr<{cls_cpp_name}>*)self))"
                else:
                    self_cast = f"(({cls_cpp_name}*)self)"
                fval = f"{self_cast}->{prop_name}"
                
                cl_ret = clean_type(prop_type)
                cpp_impls.append('    try {')
                if cl_ret in ["String", "std::string", "string"]:
                    cpp_impls.append(f'        return _strdup({fval}.c_str());')
                elif c_ret == "void*":
                    ret_cpp = self.get_cpp_type(prop_type, cls_name)
                    if prop_type.strip().endswith("*"):
                        cpp_impls.append(f'        return {fval};')
                    else:
                        cpp_impls.append(f'        return new {ret_cpp}({fval});')
                else:
                    cpp_impls.append(f'        return {fval};')
                cpp_impls.append('    } catch (const cv::Exception& e) {')
                cpp_impls.append('        _setError(e.code, e.what());')
                cpp_impls.append('    } catch (const std::exception& e) {')
                cpp_impls.append('        _setError(-2, e.what());')
                cpp_impls.append('    } catch (...) {')
                cpp_impls.append('        _setError(-3, "Unknown native exception");')
                cpp_impls.append('    }')
                cpp_impls.append('    return {};')
                cpp_impls.append('}')
                
                native_methods_by_module[mod_norm].append(f'[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]')
                if clean_type(prop_type) == "bool":
                    native_methods_by_module[mod_norm].append('[return: MarshalAs(UnmanagedType.U1)]')
                native_methods_by_module[mod_norm].append(f'public static extern {self.get_csharp_type(prop_type, is_return=True)} {getter_name}(IntPtr self);')
                
                if not is_readonly:
                    setter_name = f"{clean_cls_name}_{prop_name}_set"
                    cpp_decls.append(f'extern "C" __declspec(dllexport) void {setter_name}(void* self, {self.get_c_flat_type(prop_type)} val);')
                    cpp_impls.append(f'extern "C" __declspec(dllexport) void {setter_name}(void* self, {self.get_c_flat_type(prop_type)} val) {{')
                    cpp_impls.append('    _clearError();')
                    cpp_impls.append('    if (!self) { _setError(-1, "Null pointer: self"); return; }')
                    if self.get_c_flat_type(prop_type) == "void*" and clean_tp != "void":
                        cpp_impls.append('    if (!val) { _setError(-1, "Null pointer: val"); return; }')
                    if cls_name.split('.')[-1] in ptr_classes:
                        self_cast = f"(*((cv::Ptr<{cls_cpp_name}>*)self))"
                    else:
                        self_cast = f"(({cls_cpp_name}*)self)"
                    
                    clean_tp = clean_type(prop_type)
                    cpp_tp = self.get_cpp_type(prop_type, cls_name)
                    if self.get_c_flat_type(prop_type) == "void*" and clean_tp != "void":
                        if prop_type.strip().endswith("*"):
                            cpp_val = f"({cpp_tp})val"
                        else:
                            cpp_type_no_ref = cpp_tp.replace("&", "").strip()
                            cpp_val = f"*({cpp_type_no_ref}*)val"
                    elif clean_tp in ["String", "std::string", "string"]:
                        cpp_val = f"cv::String(val)"
                    elif self.is_enum_type(prop_type):
                        cpp_val = f"({cpp_tp})val"
                    else:
                        cpp_val = "val"
                        
                    cpp_impls.append('    try {')
                    cpp_impls.append(f'        {self_cast}->{prop_name} = {cpp_val};')
                    cpp_impls.append('    } catch (const cv::Exception& e) {')
                    cpp_impls.append('        _setError(e.code, e.what());')
                    cpp_impls.append('    } catch (const std::exception& e) {')
                    cpp_impls.append('        _setError(-2, e.what());')
                    cpp_impls.append('    } catch (...) {')
                    cpp_impls.append('        _setError(-3, "Unknown native exception");')
                    cpp_impls.append('    }')
                    cpp_impls.append('}')
                    
                    native_methods_by_module[mod_norm].append(f'[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]')
                    native_methods_by_module[mod_norm].append(f'public static extern void {setter_name}(IntPtr self, {self.get_csharp_type(prop_type, is_parameter=True)} val);')
                
                # C# property definition
                prop_desc = f"Gets the {prop_name} property." if is_readonly else f"Gets or sets the {prop_name} property."
                cs_class_methods.append(f"    /// <summary>{prop_desc}</summary>")
                cs_class_methods.append(f"    /// <exception cref=\"OpenCVException\">Thrown when the underlying OpenCV native call fails.</exception>")
                
                if is_vector_int:
                    cs_class_methods.append(f"    public int[] {to_pascal_case(prop_name)}")
                    cs_class_methods.append("    {")
                    cs_class_methods.append("        get {")
                    cs_class_methods.append("            ThrowIfDisposed();")
                    cs_class_methods.append(f"            IntPtr res = NativeMethods.{getter_name}(Handle);")
                    cs_class_methods.append("            ErrorHelper.CheckError();")
                    cs_class_methods.append("            if (res == IntPtr.Zero) return Array.Empty<int>();")
                    cs_class_methods.append("            int size = NativeMethods.cv_VectorInt_Size(res);")
                    cs_class_methods.append("            int[] data = new int[size];")
                    cs_class_methods.append("            NativeMethods.cv_VectorInt_GetData(res, data);")
                    cs_class_methods.append("            NativeMethods.cv_VectorInt_Delete(res);")
                    cs_class_methods.append("            return data;")
                    cs_class_methods.append("        }")
                    if not is_readonly:
                        cs_class_methods.append("        set {")
                        cs_class_methods.append("            ThrowIfDisposed();")
                        cs_class_methods.append("            if (value == null) return;")
                        cs_class_methods.append("            IntPtr vecPtr = NativeMethods.cv_VectorInt_New(value, value.Length);")
                        cs_class_methods.append(f"            NativeMethods.{setter_name}(Handle, vecPtr);")
                        cs_class_methods.append("            ErrorHelper.CheckError();")
                        cs_class_methods.append("            NativeMethods.cv_VectorInt_Delete(vecPtr);")
                        cs_class_methods.append("        }")
                    cs_class_methods.append("    }")
                elif is_vector_mat:
                    cs_class_methods.append(f"    public Mat[] {to_pascal_case(prop_name)}")
                    cs_class_methods.append("    {")
                    cs_class_methods.append("        get {")
                    cs_class_methods.append("            ThrowIfDisposed();")
                    cs_class_methods.append(f"            IntPtr res = NativeMethods.{getter_name}(Handle);")
                    cs_class_methods.append("            ErrorHelper.CheckError();")
                    cs_class_methods.append("            if (res == IntPtr.Zero) return Array.Empty<Mat>();")
                    cs_class_methods.append("            int size = NativeMethods.cv_VectorMat_Size(res);")
                    cs_class_methods.append("            Mat[] data = new Mat[size];")
                    cs_class_methods.append("            for (int i = 0; i < size; i++) {")
                    cs_class_methods.append("                IntPtr matPtr = NativeMethods.cv_VectorMat_GetElement(res, i);")
                    cs_class_methods.append("                data[i] = matPtr == IntPtr.Zero ? null : new Mat(matPtr);")
                    cs_class_methods.append("            }")
                    cs_class_methods.append("            NativeMethods.cv_VectorMat_Delete(res);")
                    cs_class_methods.append("            return data;")
                    cs_class_methods.append("        }")
                    if not is_readonly:
                        cs_class_methods.append("        set {")
                        cs_class_methods.append("            ThrowIfDisposed();")
                        cs_class_methods.append("            if (value == null) return;")
                        cs_class_methods.append("            IntPtr[] handles = new IntPtr[value.Length];")
                        cs_class_methods.append("            for (int i = 0; i < value.Length; i++) {")
                        cs_class_methods.append("                handles[i] = value[i] == null ? IntPtr.Zero : value[i].Handle;")
                        cs_class_methods.append("            }")
                        cs_class_methods.append("            IntPtr vecPtr = NativeMethods.cv_VectorMat_New(handles, handles.Length);")
                        cs_class_methods.append(f"            NativeMethods.{setter_name}(Handle, vecPtr);")
                        cs_class_methods.append("            ErrorHelper.CheckError();")
                        cs_class_methods.append("            NativeMethods.cv_VectorMat_Delete(vecPtr);")
                        cs_class_methods.append("        }")
                    cs_class_methods.append("    }")
                else:
                    ret_user = self.get_user_facing_csharp_type(prop_type)
                    ret_user_nullable = self.get_user_facing_csharp_return_type_nullable(prop_type)
                    cs_class_methods.append(f"    public {ret_user_nullable} {to_pascal_case(prop_name)}")
                    cs_class_methods.append("    {")
                    # Getter
                    if ret_user == "string":
                        cs_class_methods.append(f"        get {{")
                        cs_class_methods.append(f"            ThrowIfDisposed();")
                        cs_class_methods.append(f"            IntPtr res = NativeMethods.{getter_name}(Handle);")
                        cs_class_methods.append(f"            ErrorHelper.CheckError();")
                        cs_class_methods.append(f"            if (res == IntPtr.Zero) return null;")
                        cs_class_methods.append(f"            string strRes = Marshal.PtrToStringUTF8(res);")
                        cs_class_methods.append(f"            NativeMethods.cv_FreeString(res);")
                        cs_class_methods.append(f"            return strRes;")
                        cs_class_methods.append(f"        }}")
                    elif self.is_enum_type(prop_type):
                        cs_class_methods.append(f"        get {{ ThrowIfDisposed(); var res = NativeMethods.{getter_name}(Handle); ErrorHelper.CheckError(); return ({ret_user})res; }}")
                    elif ret_user not in ["int", "double", "float", "bool", "byte", "long", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                        cs_class_methods.append(f"        get {{")
                        cs_class_methods.append(f"            ThrowIfDisposed();")
                        cs_class_methods.append(f"            IntPtr res = NativeMethods.{getter_name}(Handle);")
                        cs_class_methods.append(f"            ErrorHelper.CheckError();")
                        cs_class_methods.append(f"            return res == IntPtr.Zero ? null : new {ret_user}(res);")
                        cs_class_methods.append(f"        }}")
                    else:
                        cs_class_methods.append(f"        get {{ ThrowIfDisposed(); var res = NativeMethods.{getter_name}(Handle); ErrorHelper.CheckError(); return res; }}")
                    
                    # Setter
                    if not is_readonly:
                        if self.is_enum_type(prop_type):
                            cs_class_methods.append(f"        set {{ ThrowIfDisposed(); NativeMethods.{setter_name}(Handle, (int)value); ErrorHelper.CheckError(); }}")
                        elif ret_user not in ["int", "double", "float", "bool", "byte", "long", "string", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                            cs_class_methods.append(f"        set {{ ThrowIfDisposed(); NativeMethods.{setter_name}(Handle, value == null ? IntPtr.Zero : value.Handle); ErrorHelper.CheckError(); }}")
                        else:
                            cs_class_methods.append(f"        set {{ ThrowIfDisposed(); NativeMethods.{setter_name}(Handle, value); ErrorHelper.CheckError(); }}")
                    cs_class_methods.append("    }")

            # Determine base class
            base_class_name = "DisposableOpenCVObject"
            if info.get("base"):
                raw_base = info["base"].replace("cv::", "").replace("cv.", "").replace("::", "_").replace(":", "").replace(".", "_").strip()
                for c_name in self.classes:
                    clean_c_name = c_name.replace("cv.", "").replace(".", "_")
                    if clean_c_name == raw_base:
                        base_class_name = "".join(to_pascal_case(p) for p in clean_c_name.split("_") if p)
                        break

            cls_doc = info.get("doc", "")
            class_lines = []
            class_lines.append(format_xml_doc(cls_doc, 0))
            class_lines.append(f"public class {pascal_cls_name} : {base_class_name}\n{{")
            class_lines.append(f"    internal {pascal_cls_name}(IntPtr handle) : base(handle) {{}}")
            class_lines.append(f"    protected override void DisposeUnmanaged(IntPtr handle)\n    {{")
            class_lines.append(f"        NativeMethods.{delete_func_name}(handle);")
            class_lines.append("    }")
            class_lines.extend(cs_class_methods)
            class_lines.append("}\n")
            
            classes_by_module[mod_norm].append("\n".join(class_lines))

        # 3. Generate Global Functions
        generated_globals = set()
        for decl in self.funcs:
            name = decl[0]
            ret_type = decl[1]
            args = decl[3]
            doc = decl[5]
            mod = decl[6] if len(decl) > 6 else "core"
            mod_norm = normalize_module_name(mod)
            
            if mod_norm not in cv2_methods_by_module:
                cv2_methods_by_module[mod_norm] = []
            if mod_norm not in native_methods_by_module:
                native_methods_by_module[mod_norm] = []
            
            if "NativeByteArray" in ret_type or any("NativeByteArray" in arg[0] for arg in args):
                continue
            if "IStreamReader" in ret_type or any("IStreamReader" in arg[0] for arg in args):
                continue
            
            parts = name.split('.')
            if parts[0] == "cv":
                parts = parts[1:]
            
            csharp_method_name = "".join(to_pascal_case(sanitize_identifier(p)) for p in parts)
            global_sig = (csharp_method_name, tuple(self.get_user_facing_csharp_type(arg[0]) for arg in args))
            if global_sig in generated_globals:
                continue
            generated_globals.add(global_sig)
            
            flat_base_name = "cv_" + "_".join(parts)
            flat_name = get_unique_c_name(sanitize_identifier(flat_base_name))
            
            cpp_args_decl = []
            cpp_args_call = []
            cpp_setup_statements = []
            for i, arg in enumerate(args):
                arg_tp, arg_name, arg_def, arg_mods = arg[0], arg[1], arg[2], arg[3]
                cpp_args_decl.append(f"{self.get_c_flat_type(arg_tp)} {arg_name}")
                
                clean_tp = clean_type(arg_tp)
                cpp_tp = self.get_cpp_type(arg_tp)
                if self.get_c_flat_type(arg_tp) == "void*" and clean_tp != "void":
                    if arg_tp.strip().endswith("*"):
                        cpp_args_call.append(f"({cpp_tp}){arg_name}")
                    else:
                        cpp_type_no_ref = cpp_tp.replace("&", "").strip()
                        if "const" in arg_tp or "vector" in arg_tp or arg_def:
                            default_var = f"default_{arg_name}"
                            ptr_var = f"ptr_{arg_name}"
                            cpp_setup_statements.append(f"    {cpp_type_no_ref} {default_var};")
                            cpp_setup_statements.append(f"    {cpp_type_no_ref}* {ptr_var} = {arg_name} ? ({cpp_type_no_ref}*){arg_name} : &{default_var};")
                            cpp_args_call.append(f"*{ptr_var}")
                        else:
                            cpp_args_call.append(f"*({cpp_type_no_ref}*){arg_name}")
                elif clean_tp in ["String", "std::string", "string"]:
                    cpp_args_call.append(f"cv::String({arg_name})")
                elif self.is_enum_type(arg_tp):
                    cpp_args_call.append(f"({cpp_tp}){arg_name}")
                else:
                    cpp_args_call.append(arg_name)
                    
            c_ret = self.get_c_flat_type(ret_type)
            cpp_decls.append(f'extern "C" __declspec(dllexport) {c_ret} {flat_name}({", ".join(cpp_args_decl)});')
            
            cpp_impls.append(f'extern "C" __declspec(dllexport) {c_ret} {flat_name}({", ".join(cpp_args_decl)}) {{')
            cpp_impls.append('    _clearError();')
            
            ret_expr = "" if c_ret == "void" else " {}"
            for arg in args:
                arg_tp, arg_name, arg_def = arg[0], arg[1], arg[2]
                clean_tp = clean_type(arg_tp)
                if self.get_c_flat_type(arg_tp) == "void*" and clean_tp != "void" and not arg_def:
                    cpp_impls.append(f'    if (!{arg_name}) {{ _setError(-1, "Null pointer: {arg_name}"); return{ret_expr}; }}')
            for stmt in cpp_setup_statements:
                cpp_impls.append(stmt)
            cpp_call_name = "cv::" + "::".join(parts)
            fcall = f'{cpp_call_name}({", ".join(cpp_args_call)})'
            
            cpp_impls.append('    try {')
            cl_ret = clean_type(ret_type)
            if c_ret == "void":
                cpp_impls.append(f'        {fcall};')
            elif cl_ret in ["String", "std::string", "string"]:
                cpp_impls.append(f'        return _strdup({fcall}.c_str());')
            elif c_ret == "void*":
                ret_cpp = self.get_cpp_type(ret_type)
                if ret_type.strip().endswith("*"):
                    cpp_impls.append(f'        return {fcall};')
                else:
                    cpp_impls.append(f'        return new {ret_cpp}({fcall});')
            else:
                cpp_impls.append(f'        return {fcall};')
            cpp_impls.append('    } catch (const cv::Exception& e) {')
            cpp_impls.append('        _setError(e.code, e.what());')
            cpp_impls.append('    } catch (const std::exception& e) {')
            cpp_impls.append('        _setError(-2, e.what());')
            cpp_impls.append('    } catch (...) {')
            cpp_impls.append('        _setError(-3, "Unknown native exception");')
            cpp_impls.append('    }')
            if c_ret == 'void':
                pass
            else:
                cpp_impls.append('    return {};')
            cpp_impls.append('}')
            
            # C# Native signature
            cs_args_decl = [f"{self.get_csharp_type(arg[0], is_parameter=True)} {sanitize_csharp_argument_name(arg[1])}" for arg in args]
            native_methods_by_module[mod_norm].append(f'[DllImport("opencv5sharp_native", CallingConvention = CallingConvention.Cdecl)]')
            if clean_type(ret_type) == "bool":
                native_methods_by_module[mod_norm].append('[return: MarshalAs(UnmanagedType.U1)]')
            native_methods_by_module[mod_norm].append(f'public static extern {self.get_csharp_type(ret_type, is_return=True)} {flat_name}({", ".join(cs_args_decl)});')
            
            # C# Wrapper Method in Cv2 static class
            has_disposable = any(self.get_user_facing_csharp_type(a[0]) in self.generated_class_names for a in args)
            ret_user = self.get_user_facing_csharp_type(ret_type)
            ret_user_nullable = self.get_user_facing_csharp_return_type_nullable(ret_type)
            
            cv2_lines = []
            cv2_lines.append(format_xml_doc(doc, 4, args, ret_type, has_disposable=has_disposable))
            cv2_lines.append(f'    public static {ret_user_nullable} {csharp_method_name}({", ".join([f"{self.get_user_facing_csharp_type_nullable(arg[0], arg[2])} {sanitize_csharp_argument_name(arg[1])}" for arg in args])})')
            cv2_lines.append("    {")
            call_args = []
            for arg in args:
                san_name = sanitize_csharp_argument_name(arg[1])
                user_type = self.get_user_facing_csharp_type(arg[0])
                if self.is_enum_type(arg[0]):
                    call_args.append(f"(int){san_name}")
                elif user_type in self.generated_class_names:
                    is_opt = "true" if arg[2] else "false"
                    call_args.append(f"ValidationHelper.GetHandle({san_name}, nameof({san_name}), {is_opt})")
                elif user_type not in ["void", "int", "double", "float", "bool", "byte", "long", "string", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                    call_args.append(f"{san_name} == null ? IntPtr.Zero : {san_name}.Handle")
                else:
                    call_args.append(san_name)
                    
            fcall = f"NativeMethods.{flat_name}({', '.join(call_args)})"
            if ret_user == "void":
                cv2_lines.append(f"        {fcall};")
                cv2_lines.append(f"        ErrorHelper.CheckError();")
            elif ret_user == "string":
                cv2_lines.append(f"        IntPtr res = {fcall};")
                cv2_lines.append(f"        ErrorHelper.CheckError();")
                cv2_lines.append(f"        if (res == IntPtr.Zero) return null;")
                cv2_lines.append(f"        string strRes = Marshal.PtrToStringUTF8(res);")
                cv2_lines.append(f"        NativeMethods.cv_FreeString(res);")
                cv2_lines.append(f"        return strRes;")
            elif self.is_enum_type(ret_type):
                cv2_lines.append(f"        var res = {fcall};")
                cv2_lines.append(f"        ErrorHelper.CheckError();")
                cv2_lines.append(f"        return ({ret_user})res;")
            elif ret_user not in ["int", "double", "float", "bool", "byte", "long", "IntPtr", "Size", "Point", "Rect", "Scalar", "Range", "TermCriteria", "Size2F", "Point2F", "Rect2F"]:
                cv2_lines.append(f"        IntPtr res = {fcall};")
                cv2_lines.append(f"        ErrorHelper.CheckError();")
                cv2_lines.append(f"        return res == IntPtr.Zero ? null : new {ret_user}(res);")
            else:
                cv2_lines.append(f"        var res = {fcall};")
                cv2_lines.append(f"        ErrorHelper.CheckError();")
                cv2_lines.append(f"        return res;")
            cv2_lines.append("    }")
            
            cv2_methods_by_module[mod_norm].append("\n".join(cv2_lines))

        # Copyright header for generated files
        cs_copyright = "// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.\n// See LICENSE file in the project root for full license information.\n// AUTO-GENERATED FILE — DO NOT EDIT MANUALLY. Generated by generator.py.\n\n#pragma warning disable CS8600, CS8601, CS8602, CS8603, CS8604, CS8625\n\n"
        cpp_copyright = "// Copyright (c) 2026 Qourex. Licensed under Apache-2.0.\n// See LICENSE file in the project root for full license information.\n// AUTO-GENERATED FILE — DO NOT EDIT MANUALLY. Generated by generator.py.\n\n"

        # Write C++ header
        with open(cpp_h_path, "w", encoding="utf-8") as f:
            f.write(cpp_copyright)
            f.write("#pragma once\n")
            f.write("#include <opencv2/opencv.hpp>\n")
            f.write("#include <cstring>\n")
            
            modules = [
                "calib.hpp", "calib3d.hpp", "core.hpp", "dnn.hpp", "features.hpp",
                "features2d.hpp", "flann.hpp", "geometry.hpp", "highgui.hpp",
                "imgcodecs.hpp", "imgproc.hpp", "objdetect.hpp", "photo.hpp",
                "ptcloud.hpp", "stereo.hpp", "stitching.hpp", "video.hpp", "videoio.hpp",
                "world.hpp",
                "geometry/mst.hpp", "geometry/segment.hpp",
                "ptcloud/depth.hpp", "ptcloud/odometry.hpp", "ptcloud/odometry_frame.hpp",
                "ptcloud/odometry_settings.hpp", "ptcloud/volume.hpp", "ptcloud/volume_settings.hpp",
                "stitching/detail/autocalib.hpp", "stitching/detail/blenders.hpp", "stitching/detail/camera.hpp",
                "stitching/detail/exposure_compensate.hpp", "stitching/detail/matchers.hpp",
                "stitching/detail/motion_estimators.hpp", "stitching/detail/seam_finders.hpp",
                "stitching/detail/timelapsers.hpp", "stitching/detail/warpers.hpp",
                "core/ocl.hpp",
                "ptcloud/detail/pose_graph.hpp", "ptcloud/detail/submap.hpp",
                "core/parallel/parallel_backend.hpp", "videoio/registry.hpp",
                "imgproc/bindings.hpp", "photo/cuda.hpp", "core/cuda.hpp"
            ]
            for m in modules:
                f.write(f"#include <opencv2/{m}>\n")
            f.write("\n")
            f.write("\n// Explicit instantiations to prevent MSVC C2526 template bugs with C linkage\n")
            f.write("template class cv::Rect_<int>;\n")
            f.write("template class cv::Rect_<float>;\n")
            f.write("template class cv::Rect_<double>;\n")
            f.write("template class cv::Point_<int>;\n")
            f.write("template class cv::Point_<float>;\n")
            f.write("template class cv::Point_<double>;\n")
            f.write("template class cv::Size_<int>;\n")
            f.write("template class cv::Size_<float>;\n")
            f.write("template class cv::Size_<double>;\n\n")
            f.write("\n".join(cpp_decls))
            f.write("\n")
            
        # Write C++ source
        with open(cpp_cpp_path, "w", encoding="utf-8") as f:
            f.write(cpp_copyright)
            f.write('#include "opencv5sharp_native.h"\n\n')
            f.write("\n".join(cpp_impls))
            f.write("\n")

        structs = [
            "    /// <summary>Represents a 2D integer point (x, y), corresponding to cv::Point.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Point\n    {\n        /// <summary>X coordinate.</summary>\n        public int X;\n        /// <summary>Y coordinate.</summary>\n        public int Y;\n        /// <summary>Creates a new Point with the specified coordinates.</summary>\n        /// <param name=\"x\">X coordinate.</param>\n        /// <param name=\"y\">Y coordinate.</param>\n        public Point(int x, int y) { X = x; Y = y; }\n    }",
            "    /// <summary>Represents a 2D floating-point point (x, y), corresponding to cv::Point2f.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Point2F\n    {\n        /// <summary>X coordinate.</summary>\n        public float X;\n        /// <summary>Y coordinate.</summary>\n        public float Y;\n        /// <summary>Creates a new Point2F with the specified coordinates.</summary>\n        /// <param name=\"x\">X coordinate.</param>\n        /// <param name=\"y\">Y coordinate.</param>\n        public Point2F(float x, float y) { X = x; Y = y; }\n    }",
            "    /// <summary>Represents a 2D integer size (width, height), corresponding to cv::Size.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Size\n    {\n        /// <summary>Width of the size.</summary>\n        public int Width;\n        /// <summary>Height of the size.</summary>\n        public int Height;\n        /// <summary>Creates a new Size with the specified dimensions.</summary>\n        /// <param name=\"width\">Width value.</param>\n        /// <param name=\"height\">Height value.</param>\n        public Size(int width, int height) { Width = width; Height = height; }\n    }",
            "    /// <summary>Represents a 2D floating-point size (width, height), corresponding to cv::Size2f.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Size2F\n    {\n        /// <summary>Width of the size.</summary>\n        public float Width;\n        /// <summary>Height of the size.</summary>\n        public float Height;\n        /// <summary>Creates a new Size2F with the specified dimensions.</summary>\n        /// <param name=\"width\">Width value.</param>\n        /// <param name=\"height\">Height value.</param>\n        public Size2F(float width, float height) { Width = width; Height = height; }\n    }",
            "    /// <summary>Represents a 2D integer rectangle (x, y, width, height), corresponding to cv::Rect.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Rect\n    {\n        /// <summary>X coordinate of the top-left corner.</summary>\n        public int X;\n        /// <summary>Y coordinate of the top-left corner.</summary>\n        public int Y;\n        /// <summary>Width of the rectangle.</summary>\n        public int Width;\n        /// <summary>Height of the rectangle.</summary>\n        public int Height;\n        /// <summary>Creates a new Rect with the specified position and dimensions.</summary>\n        /// <param name=\"x\">X coordinate.</param>\n        /// <param name=\"y\">Y coordinate.</param>\n        /// <param name=\"w\">Width.</param>\n        /// <param name=\"h\">Height.</param>\n        public Rect(int x, int y, int w, int h) { X = x; Y = y; Width = w; Height = h; }\n    }",
            "    /// <summary>Represents a 2D floating-point rectangle, corresponding to cv::Rect2f.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Rect2F\n    {\n        /// <summary>X coordinate of the top-left corner.</summary>\n        public float X;\n        /// <summary>Y coordinate of the top-left corner.</summary>\n        public float Y;\n        /// <summary>Width of the rectangle.</summary>\n        public float Width;\n        /// <summary>Height of the rectangle.</summary>\n        public float Height;\n        /// <summary>Creates a new Rect2F with the specified position and dimensions.</summary>\n        /// <param name=\"x\">X coordinate.</param>\n        /// <param name=\"y\">Y coordinate.</param>\n        /// <param name=\"w\">Width.</param>\n        /// <param name=\"h\">Height.</param>\n        public Rect2F(float x, float y, float w, float h) { X = x; Y = y; Width = w; Height = h; }\n    }",
            "    /// <summary>Represents a range of integer values [start, end), corresponding to cv::Range.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Range\n    {\n        /// <summary>Start of the range (inclusive).</summary>\n        public int Start;\n        /// <summary>End of the range (exclusive).</summary>\n        public int End;\n        /// <summary>Creates a new Range with the specified bounds.</summary>\n        /// <param name=\"start\">Start of range (inclusive).</param>\n        /// <param name=\"end\">End of range (exclusive).</param>\n        public Range(int start, int end) { Start = start; End = end; }\n    }",
            "    /// <summary>Represents a 4-element double vector, corresponding to cv::Scalar. Commonly used for pixel color values.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct Scalar\n    {\n        /// <summary>First channel value (e.g., Blue in BGR).</summary>\n        public double V0;\n        /// <summary>Second channel value (e.g., Green in BGR).</summary>\n        public double V1;\n        /// <summary>Third channel value (e.g., Red in BGR).</summary>\n        public double V2;\n        /// <summary>Fourth channel value (e.g., Alpha).</summary>\n        public double V3;\n        /// <summary>Creates a new Scalar with the specified channel values.</summary>\n        /// <param name=\"v0\">First channel value.</param>\n        /// <param name=\"v1\">Second channel value (default 0).</param>\n        /// <param name=\"v2\">Third channel value (default 0).</param>\n        /// <param name=\"v3\">Fourth channel value (default 0).</param>\n        public Scalar(double v0, double v1 = 0, double v2 = 0, double v3 = 0) { V0 = v0; V1 = v1; V2 = v2; V3 = v3; }\n    }",
            "    /// <summary>Represents termination criteria for iterative algorithms, corresponding to cv::TermCriteria.</summary>\n    [StructLayout(LayoutKind.Sequential)]\n    public struct TermCriteria\n    {\n        /// <summary>Termination criteria type (max iterations, epsilon, or both).</summary>\n        public int Type;\n        /// <summary>Maximum number of iterations.</summary>\n        public int MaxCount;\n        /// <summary>Desired accuracy or change in parameters.</summary>\n        public double Epsilon;\n        /// <summary>Creates a new TermCriteria with the specified parameters.</summary>\n        /// <param name=\"type\">Criteria type flags.</param>\n        /// <param name=\"maxCount\">Maximum iteration count.</param>\n        /// <param name=\"epsilon\">Desired accuracy threshold.</param>\n        public TermCriteria(int type, int maxCount, double epsilon) { Type = type; MaxCount = maxCount; Epsilon = epsilon; }\n    }"
        ]

        # Clean old unified C# files
        old_files = ["Classes.cs", "NativeMethods.cs", "Cv2.cs", "Enums.cs"]
        for of in old_files:
            of_path = os.path.join(self.workspace_dir, "src", "OpenCV5Sharp", of)
            if os.path.exists(of_path):
                try:
                    os.remove(of_path)
                    print(f"Removed old unified file: {of_path}")
                except Exception as ex:
                    print(f"Error removing {of_path}: {ex}")

        # Ensure Generated directory exists
        gen_dir = os.path.join(self.workspace_dir, "src", "OpenCV5Sharp", "Generated")
        os.makedirs(gen_dir, exist_ok=True)

        # Remove existing files in Generated to avoid duplicates
        for f in glob.glob(os.path.join(gen_dir, "*.cs")):
            try:
                os.remove(f)
            except Exception as ex:
                print(f"Error cleaning generated file {f}: {ex}")

        all_modules = set(classes_by_module.keys()) | set(enums_by_module.keys()) | set(cv2_methods_by_module.keys()) | set(native_methods_by_module.keys())
        if "Core" not in all_modules:
            all_modules.add("Core")

        # Write C# files by module
        for mod in sorted(all_modules):
            # 1. Classes
            mod_classes_lines = classes_by_module.get(mod, [])
            mod_classes_path = os.path.join(gen_dir, f"Classes.{mod}.cs")
            if mod_classes_lines or mod == "Core":
                with open(mod_classes_path, "w", encoding="utf-8") as f:
                    f.write(cs_copyright)
                    f.write("using System;\n")
                    f.write("using System.Runtime.InteropServices;\n")
                    f.write("using System.Threading;\n\n")
                    f.write("namespace OpenCV5Sharp\n{\n")
                    
                    if mod == "Core":
                        for s in structs:
                            f.write(s + "\n\n")
                            
                    for line in mod_classes_lines:
                        indented = "\n".join("    " + l if l else "" for l in line.split("\n"))
                        f.write(indented + "\n")
                    f.write("}\n")
                    
            # 2. NativeMethods
            mod_native_lines = native_methods_by_module.get(mod, [])
            if mod_native_lines:
                mod_native_path = os.path.join(gen_dir, f"NativeMethods.{mod}.cs")
                with open(mod_native_path, "w", encoding="utf-8") as f:
                    f.write(cs_copyright)
                    f.write("using System;\n")
                    f.write("using System.Runtime.InteropServices;\n\n")
                    f.write("namespace OpenCV5Sharp\n{\n")
                    f.write("    internal static partial class NativeMethods\n    {\n")
                    for line in mod_native_lines:
                        f.write(f"        {line}\n")
                    f.write("    }\n}\n")
                    
            # 3. Cv2
            mod_cv2_lines = cv2_methods_by_module.get(mod, [])
            if mod_cv2_lines:
                mod_cv2_path = os.path.join(gen_dir, f"Cv2.{mod}.cs")
                with open(mod_cv2_path, "w", encoding="utf-8") as f:
                    f.write(cs_copyright)
                    f.write("using System;\n")
                    f.write("using System.Runtime.InteropServices;\n\n")
                    f.write("namespace OpenCV5Sharp\n{\n")
                    f.write("    public static partial class Cv2\n    {\n")
                    for line in mod_cv2_lines:
                        indented = "\n".join("        " + l if l else "" for l in line.split("\n"))
                        f.write(indented + "\n")
                    f.write("    }\n}\n")
                    
            # 4. Enums
            mod_enums_lines = enums_by_module.get(mod, [])
            if mod_enums_lines:
                mod_enums_path = os.path.join(gen_dir, f"Enums.{mod}.cs")
                with open(mod_enums_path, "w", encoding="utf-8") as f:
                    f.write(cs_copyright)
                    f.write("namespace OpenCV5Sharp\n{\n")
                    for line in mod_enums_lines:
                        indented = "\n".join("    " + l if l else "" for l in line.split("\n"))
                        f.write(indented + "\n")
                    f.write("}\n")

        print("Binding generation complete!")
        print(f"\nGeneration Summary:")
        print(f"  Classes generated: {len(self.classes)}")
        print(f"  Functions generated: {len(self.funcs)}")
        print(f"  Enums generated: {len(self.enums)}")
        print(f"  Items skipped: {len(self.skipped_items)}")
        if self.verbose and self.skipped_items:
            for item, reason in self.skipped_items:
                print(f"    SKIPPED: {item} - {reason}")


def main():
    """CLI entry point for the OpenCV5Sharp binding generator."""
    global hdr_parser

    script_dir = os.path.dirname(os.path.abspath(__file__))
    default_opencv = os.path.normpath(os.path.join(script_dir, "..", "..", "opencv"))
    default_workspace = os.path.normpath(os.path.join(script_dir, "..", ".."))

    parser = argparse.ArgumentParser(
        description="OpenCV5Sharp binding generator - generates C#/C++ wrappers from OpenCV headers"
    )
    parser.add_argument(
        "--opencv-dir", default=default_opencv,
        help=f"Path to OpenCV source directory (default: {default_opencv})"
    )
    parser.add_argument(
        "--workspace-dir", default=default_workspace,
        help=f"Path to workspace root (default: {default_workspace})"
    )
    parser.add_argument(
        "--verbose", action="store_true",
        help="Log skipped functions and parse errors"
    )
    args = parser.parse_args()

    # Dynamically import hdr_parser from the OpenCV source tree
    hdr_parser_path = os.path.join(args.opencv_dir, "modules", "python", "src2")
    if not os.path.isdir(hdr_parser_path):
        print(f"ERROR: hdr_parser not found at {hdr_parser_path}", file=sys.stderr)
        print(f"Ensure --opencv-dir points to the OpenCV source directory.", file=sys.stderr)
        sys.exit(1)
    sys.path.insert(0, hdr_parser_path)
    import hdr_parser as _hdr_parser
    hdr_parser = _hdr_parser

    gen = OpenCVWrapperGenerator(args.opencv_dir, args.workspace_dir, verbose=args.verbose)
    gen.parse_all()
    gen.generate()


if __name__ == "__main__":
    main()
