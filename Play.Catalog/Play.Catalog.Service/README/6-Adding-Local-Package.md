## Adding Local Package
1. Adding Package from the Directory
```c#
dotnet nuget add source D:\Dev\1-Core\MicroServiceBackEnd\packages -n PlayEconomy
// Directory where command Executed
// /d/Dev/1-Core/MicroServiceBackEnd/Play.Catalog/play.catalog.service
 
dotnet nuget add source ../../packages/ -n PlayEconomy // Working in the Same Directory
// NOTE:
// After that you have to Paste the Project into 
// C:\Users\ali_a\AppData\Roaming\packages // cli will look for the project here
// Also Disable the Options for Looking the Project in the internet

dotnet nuget remove source PlayEconomy // Removing Local Projects
dotnet nuget list source
dotnet add package
```