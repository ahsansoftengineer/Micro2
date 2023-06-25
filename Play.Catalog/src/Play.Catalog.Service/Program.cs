using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Settings;
using Play.Catalog.Service.Repo;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
