# Framework Compatibility

This document describes the framework compatibility and security considerations for `qckdev.Text.Json`.

## Supported Frameworks

This package supports an extensive range of target frameworks for maximum compatibility:

| Framework | Status | Support Level | JSON Library Used |
|-----------|--------|---------------|-------------------|
| .NET 10.0 | ✅ Supported | Current | System.Text.Json 10.0.0 |
| .NET 9.0 | ✅ Supported | Preview | System.Text.Json 10.0.0 |
| .NET 8.0 | ✅ Supported | LTS | System.Text.Json 8.0.5 |
| .NET 7.0 | ✅ Supported | EOL (May 2024) | System.Text.Json 8.0.5 |
| .NET 6.0 | ✅ Supported | LTS | System.Text.Json 6.0.11 |
| .NET Standard 2.0 | ✅ Supported | Cross-platform | System.Text.Json 6.0.11 |
| .NET Standard 1.2 | ✅ Supported | Legacy | Newtonsoft.Json 13.0.3 |
| .NET Framework 4.6.1 | ✅ Supported | Legacy | Newtonsoft.Json 13.0.3 |
| .NET Framework 4.0 | ✅ Supported | Legacy | Newtonsoft.Json 13.0.3 |
| .NET Framework 3.5 | ✅ Supported | Legacy | Newtonsoft.Json 13.0.3 |

## Package Strategy

This library provides a **unified JSON serialization API** that transparently uses:
- **System.Text.Json** for modern frameworks (.NET 6.0+, .NET Standard 2.0)
- **Newtonsoft.Json** for legacy frameworks (.NET Framework, .NET Standard 1.2)

This allows you to write code once and target multiple frameworks without changing your JSON handling code.

## Package Versions

| Framework(s) | JSON Package | Version | Notes |
|--------------|--------------|---------|-------|
| net10.0, net9.0 | System.Text.Json | 10.0.0 | Latest features |
| net8.0, net7.0 | System.Text.Json | 8.0.5 | LTS version |
| net6.0, netstandard2.0 | System.Text.Json | 6.0.11 | LTS version |
| netstandard1.2, net461, net40, net35 | Newtonsoft.Json | 13.0.3 | Legacy support |

## Security Considerations

### Vulnerability Mitigations

This package includes explicit security mitigations for legacy frameworks:

#### For .NET Standard 1.2 Only

##### CVE-2019-0820 (GHSA-7jgj-8wvc-jh57)
- **Severity**: High (CVSS 7.5)
- **Issue**: Denial of Service vulnerability in System.Net.Http
- **Affected Versions**: System.Net.Http < 4.3.4
- **Mitigation**: 
  - Explicitly upgraded to `System.Net.Http` version **4.3.4**
  - Only affects netstandard1.2 target
- **Advisory**: https://github.com/advisories/GHSA-7jgj-8wvc-jh57

##### CVE-2019-0820 (GHSA-cmhx-cq75-c4mj)
- **Severity**: High (CVSS 7.5)
- **Issue**: Regular Expression Denial of Service (ReDoS) in System.Text.RegularExpressions
- **Affected Versions**: System.Text.RegularExpressions < 4.3.1
- **Mitigation**: 
  - Explicitly upgraded to `System.Text.RegularExpressions` version **4.3.1**
  - Only affects netstandard1.2 target
- **Advisory**: https://github.com/advisories/GHSA-cmhx-cq75-c4mj

### Version Selection Strategy

Package versions were selected using the **Minimum Viable Product (MVP)** approach:
- ✅ Uses **System.Text.Json** for modern frameworks (better performance, lower memory usage)
- ✅ Uses **Newtonsoft.Json 13.0.3+** for legacy frameworks (proven stability, no known vulnerabilities)
- ✅ Explicit security patches for netstandard1.2 transitive dependencies
- ✅ Version ranges ensure automatic security updates for Newtonsoft.Json: `[13.0.3,14.0)`
- ✅ Avoids unnecessary updates that might introduce breaking changes

## Framework-Specific Notes

### Modern Frameworks (.NET 6.0+)
- Uses System.Text.Json natively
- Best performance and lowest memory allocation
- Fully async/await support
- Source generators compatible (future enhancement)

### .NET Standard 2.0
- Uses System.Text.Json via NuGet package
- Great balance between compatibility and performance
- Recommended target for libraries

### .NET Standard 1.2
- Uses Newtonsoft.Json for compatibility
- Additional security patches applied
- Limited API surface (no dynamic support)
- ⚠️ Note: .NET Standard 1.2 is no longer recommended by Microsoft

### Legacy .NET Framework
- Uses Newtonsoft.Json (industry standard for .NET Framework)
- Excellent compatibility
- Battle-tested in production environments
- .NET 3.5: No dynamic type support

## API Compatibility

The library provides a consistent API across all frameworks:

```csharp
// Serialization - works on all frameworks
string json = JsonSerializer.Serialize(myObject);

// Deserialization - works on all frameworks
var myObject = JsonSerializer.Deserialize<MyType>(json);
```

The implementation automatically uses the appropriate JSON library for your target framework.

## Verification

To verify there are no known vulnerabilities in this package:

```powershell
dotnet list package --vulnerable --include-transitive
```

Expected output:
```
The given project has no vulnerable packages given the current sources.
```

## Migration Guide

### From Newtonsoft.Json

If you're migrating from direct Newtonsoft.Json usage:

1. Install qckdev.Text.Json:
   ```xml
   <PackageReference Include="qckdev.Text.Json" Version="1.3.0" />
   ```

2. Update your usings:
   ```csharp
   // Before
   using Newtonsoft.Json;
   
   // After
   using qckdev.Text.Json;
   ```

3. The API is intentionally similar - most code will work with minimal changes

### From System.Text.Json

If you're using System.Text.Json directly:

1. The API is largely compatible
2. You gain automatic compatibility with legacy frameworks
3. Your library can now target a wider range of consumers

## Performance Considerations

### System.Text.Json (.NET 6.0+)
- ✅ Faster serialization/deserialization
- ✅ Lower memory allocation
- ✅ Better throughput
- ✅ Native async support

### Newtonsoft.Json (Legacy Frameworks)
- ✅ Mature and stable
- ✅ Rich feature set
- ✅ Wide ecosystem support
- ⚠️ Slightly higher memory usage

## Known Limitations

### .NET Standard 1.2
- No dynamic type support
- Limited reflection capabilities
- Async operations not available

### .NET Framework 3.5
- No dynamic type support
- No async/await
- Limited LINQ support

### System.Text.Json (All Versions)
- Some advanced Newtonsoft.Json features not available
- Different attribute names for configuration

## Dependencies

### Modern Frameworks (.NET 6.0+)
- System.Text.Json (version specific to framework)

### Legacy Frameworks
- Newtonsoft.Json (>= 13.0.3, < 14.0)

### .NET Standard 1.2 Only
- System.Net.Http (>= 4.3.4) - security patch
- System.Text.RegularExpressions (>= 4.3.1) - security patch

All transitive dependencies are monitored for vulnerabilities and updated as needed.

## Additional Resources

- [System.Text.Json Documentation](https://docs.microsoft.com/dotnet/standard/serialization/system-text-json-overview)
- [Newtonsoft.Json Documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm)
- [.NET Standard Guidance](https://aka.ms/dotnet/dotnet-standard-guidance)
- [JSON Specification (RFC 8259)](https://tools.ietf.org/html/rfc8259)

## Recommendations

### For New Projects
- Target .NET 6.0+ or .NET 8.0 (LTS) when possible
- Avoid .NET Standard 1.2 unless absolutely necessary

### For Library Authors
- Target .NET Standard 2.0 for broad compatibility
- This package handles the JSON library selection for you

### For Legacy Applications
- This package allows gradual migration to modern frameworks
- Your JSON code remains compatible during the transition

## Last Updated

Document last updated: February 25, 2026

For the latest information, please check the [GitHub repository](https://github.com/hfrances/qckdev.Text.Json).
