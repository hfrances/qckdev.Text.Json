# Documentation

Welcome to the documentation for `qckdev.Text.Json`.

## Table of Contents

- [Framework Compatibility](COMPATIBILITY.md) - Supported frameworks, package versions, and security considerations
- [Legacy AssemblyName Build](LEGACY_ASSEMBLYNAME_BUILD.md) - Why `net35` generates a different assembly name and required CLI restore/build steps

## Quick Links

- [Main README](../README.md) - Project overview and usage examples
- [NuGet Package](https://www.nuget.org/packages/qckdev.Text.Json)
- [GitHub Repository](https://github.com/hfrances/qckdev.Text.Json)

## Documentation Contents

### [Framework Compatibility](COMPATIBILITY.md)
Detailed information about:
- Supported .NET frameworks (10 different targets!)
- JSON library selection strategy (System.Text.Json vs Newtonsoft.Json)
- Security vulnerability mitigations for legacy frameworks
- Performance considerations
- Migration guides
- Framework-specific limitations

## About This Package

`qckdev.Text.Json` provides a unified JSON serialization API that works seamlessly across all .NET versions, from .NET Framework 3.5 to .NET 10.0. The package automatically selects the best JSON library for your target framework:

- **Modern frameworks** (.NET 6.0+): Uses high-performance System.Text.Json
- **Legacy frameworks** (.NET Framework, .NET Standard 1.2): Uses proven Newtonsoft.Json

This allows you to write your code once and target any .NET framework without worrying about which JSON library to use.

## Key Features

✨ **Universal Compatibility**: Supports 10 different framework targets  
⚡ **Optimal Performance**: Automatically uses the best JSON library for each framework  
🔒 **Security First**: All known vulnerabilities mitigated  
🎯 **Consistent API**: Same code works across all frameworks  
📦 **Zero Configuration**: Works out of the box

## Contributing

If you find any issues or have suggestions for improving the documentation, please open an issue or submit a pull request on [GitHub](https://github.com/hfrances/qckdev.Text.Json/issues).

## License

This project is licensed under the terms specified in the [LICENSE](../LICENSE) file.

---

Last updated: February 25, 2026
