# NuGet Package Signing Guide

This guide details the procedure for signing official releases of the **OpenCV5Sharp** NuGet packages. Package signing ensures integrity and authenticity, helping consumers trust the binaries.

---

## Prerequisites

1. **Code Signing Certificate**: A valid code signing certificate from an approved Certificate Authority (CA) in a PFX format (or access to a hardware security module / Azure Key Vault).
2. **dotnet CLI** or **NuGet.exe** installed.

---

## Step 1: Sign the Assembly (Optional, Strong Naming)

We currently build with strong naming disabled. If strong naming is required in the future, it is configured in the `.csproj` file:
```xml
<PropertyGroup>
  <SignAssembly>true</SignAssembly>
  <AssemblyOriginatorKeyFile>key.snk</AssemblyOriginatorKeyFile>
</PropertyGroup>
```

---

## Step 2: Sign the NuGet Package (.nupkg)

Once the NuGet package is generated using `dotnet pack`, it can be signed using `dotnet nuget sign`.

### Signing command:
```powershell
dotnet nuget sign "artifacts\OpenCV5Sharp.<VERSION>.nupkg" `
  --certificate-path "path\to\your\certificate.pfx" `
  --certificate-password "your-certificate-password" `
  --timestamper "http://timestamp.digicert.com"
```

### Parameters:
- `--certificate-path`: Path to the certificate file (`.pfx`) containing the private key.
- `--certificate-password`: Password for the PFX file.
- `--timestamper`: URL of a RFC 3161 compliant timestamp server (e.g., DigiCert, Sectigo). Timestamping is **required** to ensure the package remains valid even after the signing certificate expires.

---

## Step 3: Verify the Package Signature

After signing the package, verify that the signature is valid using `dotnet nuget verify`.

### Verification command:
```powershell
dotnet nuget verify "artifacts\OpenCV5Sharp.<VERSION>.nupkg"
```

### Expected Output:
```text
Successfully verified package 'OpenCV5Sharp.<VERSION>.nupkg'.
```

---

## Step 4: Publish to NuGet.org

After verification, push the signed package to NuGet.org:
```powershell
dotnet nuget push "artifacts\OpenCV5Sharp.<VERSION>.nupkg" --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

NuGet.org will automatically validate the signature against your registered certificate.
