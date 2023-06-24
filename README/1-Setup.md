## Initial Project Setup
1. Startingup MicroService with VS Code
```c#
// With in the Folder
cd Play.Catalog/src
dotnet new webapi -n Play.Catalog.Service --framework net5.0
// Settup .vscode Folder > launch.json, tasks.json
// Properties

```
2. Setting VS Code for Debugging just by clicking on Enable VS Code Debugging
3. Setting up Project Port | Properties>launchSettings.json>
```json
"applicationUrl": "https://localhost:8000;http://localhost:8001",
```
4. Building Project
```c#
PS MicroServiceBackEnd\Play.Catalog\src\Play.Catalog.Service> dotnet build
```
5. This Option Enable
- - Menu>Terminal>Run Build Task (Ctrl + Shift + Build)
```json
.vscode>Tasks.json
"tasks": [
  {
    "group": {
      "kind": "build",
      "isDefault": true
    }
  }
]

```
6. Dotnet Core Project Setup
```c#
// Run Project
PS MicroServiceBackEnd\Play.Catalog\src\Play.Catalog.Service> dotnet run
// VS Code Sidebar Menu > Debug & Run >
Click on the Play Button |>
Ctrl + F5
 
// Dev Certificate
dotnet dev-certs https --trust

// Initializing git
git init
dotnet new gitignore
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/ahsansoftengineer/DotnetCoreMicroService.git
git push -u origin main
git push -u origin 0-InMemory-API
git checkout -b feature_branch_name
```

