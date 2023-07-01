## CLI COMMANDS

```C# 
dotnet new webapi -n Play.Catalog.Service --framework net5.0
dotnet new classlib -n Play.Common 
dotnet build
dotnet dev-certs https --trust
dotnet run
dotnet run --launch-profile https
```
### Custom / Local Packages
- Packages are Prebuild
```c#
dotnet new classlib -n Play.Common 
dotnet pack -o ../packages 
dotnet pack -p:PackageVersion=1.0.1 -o ../packages 

dotnet nuget add source D:\Dev\1-Core\MicroServiceBackEnd\packages -n PlayEconomy

dotnet nuget add source ../../packages/ -n PlayEconomy

dotnet nuget remove source PlayEconomy 
dotnet nuget list source

```
### Local Projects
- Packages are not Prebuild
```c#
dotnet add reference ../../Play.Catalog.Contracts/Play.Catalog.Contracts.csproj

```
### Dotnet Core Packages
```c#
dotnet add package MongoDB.Driver
dotnet add package Microsoft.Extensions.Configuration.Binder
dotnet add package Microsoft.Extensions.DependencyInjection

dotnet add package Microsoft.Extensions.Http.Polly

dotnet add package MassTransit.AspNetCore
dotnet add package MassTransit.RabbitMQ
```
### Docker 

```c#
docker run -d --rm --name mongo -p 27017:27017 mongodbdata:/data/db mongo

docker-compose up -d
```

### Git
```c#
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