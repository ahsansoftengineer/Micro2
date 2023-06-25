using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Settings;

namespace Play.Catalog.Service.Repo
{
  public static class Extensions
  {
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
      BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
      BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

      services.AddSingleton(sp =>
      {
        IConfiguration? configuration = sp.GetService<IConfiguration>();
        ServiceSettings? serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
        MongoDbSettings? mongoSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();


        MongoClient? mongoClient = new(mongoSettings.ConnectionString);
        return mongoClient.GetDatabase(serviceSettings.ServiceName);
      });
      return services;
    }
    public static IServiceCollection AddMongoRepo<T>(this IServiceCollection services, string collectionName)
    where T : IEntity
    {
      return services.AddSingleton<IRepo<T>>(sp =>
      {
        IMongoDatabase? database = sp.GetService<IMongoDatabase>();
        return new RepoMongo<T>(database, collectionName);
      });
    }
  }
}