# Build de Proyectos con `AssemblyName` Diferente en `net35`

## Contexto

`qckdev.Text.Json` usa un nombre de ensamblado diferente cuando el target es `net35`:

- `net35` -> `qckdev.Text.Json.2.dll`
- resto de frameworks -> `qckdev.Text.Json.dll`

Esto se define en `qckdev.Text.Json/qckdev.Text.Json.csproj`.

## Problema conocido

En soluciones multi-target con frameworks legacy, Visual Studio puede no resolver bien el estado inicial si no se hace restore/build desde CLI.

Referencia:
- https://github.com/dotnet/sdk/issues/22469#issuecomment-1732733899

## Flujo recomendado de compilacion

```powershell
cd qckdev.Text.Json
dotnet restore
dotnet build
```

Para release/pack:

```powershell
cd qckdev.Text.Json
dotnet build --configuration Release
dotnet pack --configuration Release
```

## Verificacion rapida

Comprobar que existan ambos outputs:

- `bin/<Configuration>/net35/qckdev.Text.Json.2.dll`
- `bin/<Configuration>/<otro-framework>/qckdev.Text.Json.dll`

