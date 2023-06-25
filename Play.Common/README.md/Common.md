## Common 
1. To Create a Library
```c#
cd /d/Dev/1-Core/MicroServiceBackEnd/Play.Common/src
dotnet new classlib -n Play.Common // --framework net5.0
```
2. Ctrl + Shift + P (Command Palate)
```c#
.Net: Generate Assets for Build & Debug
// will Generate Task.json for 
```
3. Add below Section in Task.json
```json
"group": {
  "kind": "build",
  "isDefault": true
}
```
4. Add Packages
```c#
dotnet add package MongoDB.Driver
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.DependencyInjection
```
5. Copy paste the files concern with Common Package
6. Fix Imports
7. Build and Packages
```c#
dotnet pack ..\..\..\packages\
```