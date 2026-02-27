# Actualizacion de Dependencias de Tests Unitarios

## Proyecto
`qckdev.Text.Json` (topologia de tests multi-proyecto)

## Proyectos de tests involucrados

- `qckdev.Text.Json.Test` (runner principal)
- `qckdev.Text.Json.Test.Common` (codigo compartido)
- `qckdev.Text.Json.Test.Net35` (runner legacy)
- `qckdev.Text.Json.Test.Net40` (runner legacy)

## Estado actual (fuente de verdad)

### `qckdev.Text.Json.Test`

- Target frameworks: `net10.0;net9.0;net8.0;net7.0;net6.0;net5.0;netcoreapp3.1;$(net461);$(net451);$(net45)`
- Stack testing moderno (netcoreapp3.1+):
  - `Microsoft.NET.Test.Sdk` `17.11.1`
  - `MSTest.*` `3.2.2`
  - `coverlet.*` `6.0.0`
- Stack testing legacy (net461/net451/net45):
  - `Microsoft.NET.Test.Sdk` `17.11.0`
  - `MSTest.*` `2.2.10`
  - `coverlet.msbuild` `3.1.2`
  - `coverlet.collector` `1.2.0`

### `qckdev.Text.Json.Test.Common`

- Debe cubrir todos los TFM del proyecto `Test`.
- Actualmente incluye ademas `$(net40);$(net35)` para soporte legacy.

## Reglas para mantenimiento

1. No cambiar `TargetFrameworks` sin solicitud explicita.
2. Mantener dos bloques de testing en `qckdev.Text.Json.Test.csproj`:
   - moderno (netcoreapp3.1+)
   - legacy (net461/net451/net45)
3. Si se agrega un TFM en `Test`, replicarlo en `Test.Common`.
4. No eliminar `Test.Net35`/`Test.Net40` sin validar pipeline legacy.
5. Mantener referencias legacy (`Microsoft.CSharp`) solo en el bloque que corresponde.

## Verificacion

```powershell
dotnet restore qckdev.Text.Json.Test.Common\qckdev.Text.Json.Test.Common.csproj
dotnet build qckdev.Text.Json.Test.Common\qckdev.Text.Json.Test.Common.csproj

dotnet restore qckdev.Text.Json.Test\qckdev.Text.Json.Test.csproj
dotnet test qckdev.Text.Json.Test\qckdev.Text.Json.Test.csproj
dotnet test qckdev.Text.Json.Test\qckdev.Text.Json.Test.csproj --list-tests
```
