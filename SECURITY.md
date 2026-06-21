# Security Policy

## Supported Versions

| Version | Supported          |
|:--------|:------------------:|
| 1.0.x   | :white_check_mark: |
| < 1.0   | :x:                |

## Reporting a Vulnerability

If you discover a security vulnerability in OpenCV5Sharp, please report it responsibly.

**DO NOT** open a public GitHub issue for security vulnerabilities.

### How to Report

1. **GitHub Security Advisories** (preferred): [Report a vulnerability](https://github.com/qourex/opencv5sharp/security/advisories/new)
2. **Email**: info@qourex.com

### What to Include

- Description of the vulnerability
- Steps to reproduce
- Impact assessment
- Suggested fix (if any)

### Response Timeline

We are a small open-source team. We aim to acknowledge vulnerability reports within 48 hours and will work as quickly as we can to release patches for confirmed issues.

### Scope

We actively track security concerns related to:
- Memory safety in the C# wrapper and unmanaged boundaries
- DLL loading hazards (DLL hijacking risks)
- Native integration buffer overflows or resource leaks

Issues in upstream OpenCV or FFmpeg should be reported directly to their respective security channels.

## Security Best Practices

To keep your applications secure when using OpenCV5Sharp:
1. **Manage resource lifecycles**: Always call `Dispose()` or use C# `using` blocks on `Mat` and other disposable types to prevent memory leaks and resource exhaustion.
2. **Validate input data**: Ensure image sizes, file paths, and stream inputs are validated before passing them to the API.
3. **Keep package updated**: Apply updates regularly to receive the latest security fixes.
4. **DLL Hijacking Mitigation**: The library restricts P/Invoke searches to prevent loading arbitrary DLLs from untrusted paths (mitigating CWE-426).

