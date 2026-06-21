# Contributing to OpenCV5Sharp

We are excited that you want to contribute to OpenCV5Sharp! Whether you're fixing a bug, adding new module configurations, improving documentation, or writing tests, your contributions are welcome.

This guide will help you get your environment set up and outline our development workflow.

## Development Environment Setup

### Prerequisites
- **Windows 10/11 x64**
- **.NET SDK 8.0+** (or 9.0+)
- **Python 3.8+** (for the binding generator)
- **Visual Studio 2022** with the "Desktop development with C++" workload
  (for compiling the native DLL)
- **CMake 3.12+** (optional, for CMake-based builds)

### Getting Started

```bash
# Clone the repository
git clone https://github.com/qourex/opencv5sharp.git
cd opencv5sharp

# Restore .NET dependencies
dotnet restore

# Build the managed library
dotnet build src/OpenCV5Sharp/OpenCV5Sharp.csproj -c Debug

# Run the test suite
dotnet test
```

## How to Modify the Generator

The generator (`src/OpenCV5Sharp.Generator/generator.py`) is the single source
of truth for 97% of the codebase. **Do NOT manually edit generated files** — your
changes will be overwritten on the next generation run.

### Generated files (DO NOT EDIT):
- `src/OpenCV5Sharp/Generated/*.cs` (Module-split C# files)
- `src/OpenCV5Sharp.Native/opencv5sharp_native.cpp`
- `src/OpenCV5Sharp.Native/opencv5sharp_native.h`

### Hand-written files (safe to edit):
- `src/OpenCV5Sharp/OpenCVException.cs`
- `src/OpenCV5Sharp/AssemblyInfo.cs`
- `src/OpenCV5Sharp/PlatformGuard.cs`
- `src/OpenCV5Sharp.Generator/generator.py`
- All test and sample files

### Regenerating bindings
```bash
py src/OpenCV5Sharp.Generator/generator.py
```

## Branch Naming Convention

- `feature/short-description` — New features
- `fix/short-description` — Bug fixes
- `docs/short-description` — Documentation changes
- `chore/short-description` — Build, CI, or dependency changes

## Commit Message Format

Follow [Conventional Commits](https://www.conventionalcommits.org/):

```
feat: add new feature
fix: resolve issue with ...
docs: update README
chore: update dependencies
test: add unit tests for ...
```

## Developer Certificate of Origin

All contributions must include a `Signed-off-by` line in the commit message,
certifying the [Developer Certificate of Origin v1.1](https://developercertificate.org/):

```bash
git commit -s -m "feat: add new feature"
```

## Code Style

- Follow the `.editorconfig` rules in the repository root.
- Use PascalCase for all public APIs.
- All public methods must have XML documentation comments.
- All source files must include the Apache 2.0 copyright header.

## Testing Requirements

Before submitting a PR:
1. Ensure `dotnet build` succeeds with zero warnings.
2. Ensure all existing tests pass.
3. Add tests for any new functionality.
4. If modifying the generator, regenerate and verify the output compiles.

## Pull Request Process

1. Fork the repository and create a feature branch.
2. Make your changes following the code style guidelines.
3. Add or update tests as appropriate.
4. Update documentation if your change affects public APIs.
5. Update `CHANGELOG.md` under the `[Unreleased]` section.
6. Submit a pull request with a clear description of the changes.

## Reporting Issues

When reporting bugs, please include:
- .NET SDK version (`dotnet --version`)
- Operating system and architecture
- Steps to reproduce the issue
- Expected vs. actual behavior
- Any error messages or stack traces

## License

By contributing, you agree that your contributions will be licensed under the
Apache License 2.0.
