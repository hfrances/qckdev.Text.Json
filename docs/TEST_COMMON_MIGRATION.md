# Migracion de `*.Test.Common` (qckdev.Text.Json)

## Objetivo

Mantener `qckdev.Text.Json.Test.Common` alineado con los frameworks de `qckdev.Text.Json.Test` para evitar fallos de restore/build por TFM faltante.

## Regla principal

`qckdev.Text.Json.Test.Common.csproj` debe incluir como minimo todos los frameworks usados por `qckdev.Text.Json.Test.csproj`.

Actualmente:

- `qckdev.Text.Json.Test`: `net10.0;net9.0;net8.0;net7.0;net6.0;net5.0;netcoreapp3.1;$(net461);$(net451);$(net45)`
- `qckdev.Text.Json.Test.Common`: incluye los anteriores y ademas `$(net40);$(net35)`

## Patron recomendado

1. Usar placeholders legacy (`$(net461)`, `$(net451)`, `$(net45)`, `$(net40)`, `$(net35)`) para mantener consistencia.
2. Mantener los `DefineConstants` por legacy:
   - `NO_DYNAMIC` para `net35`
   - `NEWTONSOFT` para frameworks legacy
3. Referencias condicionales solo donde apliquen:
   - `Microsoft.CSharp` para `net461/net451/net45/net40`

## Checklist de migracion

1. Si agregas un framework en `qckdev.Text.Json.Test`, agrega el mismo en `qckdev.Text.Json.Test.Common`.
2. Compila primero `Test.Common`, luego `Test`.
3. Verifica que no haya `TargetFramework` huérfanos entre ambos proyectos.

## Comandos de verificacion

```powershell
dotnet restore qckdev.Text.Json.Test.Common\qckdev.Text.Json.Test.Common.csproj
dotnet build qckdev.Text.Json.Test.Common\qckdev.Text.Json.Test.Common.csproj

dotnet restore qckdev.Text.Json.Test\qckdev.Text.Json.Test.csproj
dotnet test qckdev.Text.Json.Test\qckdev.Text.Json.Test.csproj --list-tests
```

