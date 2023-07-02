### Docker with MicroService
1. Running Docker MongoDB Container
```c# 
docker run -d --rm --name mongo -p 27017:27017 mongodbdata:/data/db mongo
```
2. Install Mongo DB Extension then Connect that with the following local port
- mongodb://127.0.0.1:27017
3. Setting up Strongly Type Configuration for MongoDB
4. Using Dependency Injection for Repository
5. Program.cs Changes for DI and MongoDB Config
```c#
ConfigurationManager? configuration = builder.Configuration;
IServiceCollection? services = builder.Services;

// Add services to the container.

{
  BsonSerializer.RegisterSerializer(
      new GuidSerializer(BsonType.String)
  );
  BsonSerializer.RegisterSerializer(
      new DateTimeOffsetSerializer(BsonType.String)
  );
  ServiceSettings? serviceSettings = builder.Configuration.GetSection(
      nameof(ServiceSettings)).Get<ServiceSettings>();

  builder.Services.AddSingleton(sp =>
  {
    MongoDbSettings? mongoSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

    MongoClient? mongoClient = new MongoClient(mongoSettings.ConnectionString);

    return mongoClient.GetDatabase(serviceSettings.ServiceName);
  });
  services.AddSingleton<IItemsRepository, ItemsRepository>(); 
}

services.AddControllers(options =>
{
  options.SuppressAsyncSuffixInActionNames = false;
});

```
6. appsettings.json
```c#
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ServiceSettings": {
    "ServiceName": "Catalog"
  },
  "MongoDbSettings": {
    "Host": "localhost",
    "Port": "27017"
  },
  "AllowedHosts": "*"
}
```
7. Use full Extension
```c#
C#, C# DevKit, IntelliCode for C# Dev Kit, MongoDB for VS Code, C# Extensions, .Net Install Tool for Extensions
```