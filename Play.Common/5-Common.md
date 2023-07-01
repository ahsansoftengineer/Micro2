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
7. Build and Make Package for Upcomming Usage
```c#
// Clear Nuget Cache
// dotnet nuget locals all --clear // It will clear all packages don't use it
// within the dir where csproj
dotnet pack -o ../packages // Bash Command
// Telling dotnet source new source of Packages within the Directory PowerShell Command
dotnet nuget add source D:\Dev\1-Core\MicroServiceBackEnd\packages -n PlayEconomy2 // PS Command
dotnet nuget list source

// In the main Project
dotnet add package Play.Common
```

### RabbitMQ
1. Install the Packages
```c#
dotnet add package MassTransit.AspNetCore
dotnet add package MassTransit.RabbitMQ
```
2. Extension Configuration : MassTransit > AddMassTransitWithRabbitMQ
3. Run the Command to Make a Package
```c#
dotnet pack -o ../packages
```