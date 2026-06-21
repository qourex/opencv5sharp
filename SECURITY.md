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

- **Acknowledgment**: Within 48 hours
- **Initial assessment**: Within 5 business days
- **Fix timeline**: Depends on severity
  - Critical: 7 days
  - High: 14 days
  - Medium: 30 days
  - Low: Next release

### Scope

Security issues in scope include:

- Memory safety vulnerabilities in the managed wrapper
- DLL hijacking or loading vulnerabilities
- Native code buffer overflows or crashes
- Input validation bypasses
- Denial of service through resource exhaustion

### Out of Scope

- Vulnerabilities in upstream OpenCV (report to [OpenCV](https://github.com/opencv/opencv/security))
- Vulnerabilities in upstream FFmpeg (report to [FFmpeg](https://ffmpeg.org/security.html))

### Recognition

We acknowledge security researchers who responsibly disclose vulnerabilities in our release notes.

## Security Best Practices

When using OpenCV5Sharp in your application:

1. **Always dispose** `Mat` and other `IDisposable` objects to prevent memory leaks
2. **Validate inputs** before passing to OpenCV methods (image dimensions, file paths)
3. **Keep updated** to the latest version for security patches
4. The library uses `DefaultDllImportSearchPaths` to prevent DLL hijacking (CWE-426)
5. Native binaries include ASLR (`/DYNAMICBASE`), DEP (`/NXCOMPAT`), and CFG (`/guard:cf`) protections
