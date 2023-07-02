### Repo

1. Adding Item Entity
2. Installing MongoDB.Driver
```c#
// /d/Dev/1-Core/MicroServiceBackEnd/Play.Catalog/src/Play.Catalog.Service
dotnet add package MongoDB.Driver
```
3. Create Entity Item
4. Create Repo ItemRepository
5. Add Extension for Mapping
6. Setting up Controller Options for SuppressAsyncSuffixInActionNames 
```c#
// Program.cs Settings for MongoDB
builder.Services.AddControllers(options => {
    options.SuppressAsyncSuffixInActionNames = false;
});

BsonSerializer.RegisterSerializer(
    new GuidSerializer(BsonType.String)
);
BsonSerializer.RegisterSerializer(
    new DateTimeOffsetSerializer(BsonType.String)
);
```
7. Update ItemsController

