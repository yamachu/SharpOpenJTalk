# SharpOpenJTalk

SharpOpenJTalk is a C# wrapper for OpenJTalk.

## WebAssembly (wasm) Target and Emscripten Version

When targeting WebAssembly, you can optionally specify the Emscripten version to use by setting the `EmscriptenVersion` MSBuild property. If not specified, a default version will be automatically selected based on the target framework.

### Emscripten Version Table

| TargetFramework | EmscriptenVersion (default) |
|-----------------|----------------------------|
| net6.0          | 2.0.23                     |
| net7.0          | 3.1.12                     |
| net8.0          | 3.1.34                     |
| net9.0 / net10.0| 3.1.56                     |

You can override the version by passing the property at build time:

```sh
dotnet build -p:EmscriptenVersion=3.1.34
```
