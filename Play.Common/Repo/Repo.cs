
using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common.Entities;

namespace Play.Common.Repo
{
	public class RepoMongo<T> : IRepo<T>
	where T : IEntity
	{
		private readonly IMongoCollection<T> dbCollection;
		private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

		public RepoMongo(IMongoDatabase database, string collectionName)
		{
			// MongoClient? mongoClient = new("mongodb://localhost:27017");
			// var database = mongoClient.GetDatabase("Catalog");
			dbCollection = database.GetCollection<T>(collectionName);
		}

		public async Task<IReadOnlyCollection<T>> GetAllAsync()
		{
			return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
		}
		public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
		{
			return await dbCollection.Find(filter).ToListAsync();
		}

		public async Task<T> GetAsync(Guid id)
		{
			FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);

			return await dbCollection.Find(filter).FirstOrDefaultAsync();
		}
		public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
		{
			return await dbCollection.Find(filter).FirstOrDefaultAsync();
		}
		public async Task CreateAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			await dbCollection.InsertOneAsync(entity);
		}
		public async Task UpdateAsync(T entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}
			FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, entity.Id);
			await dbCollection.ReplaceOneAsync(filter, entity);

		}
		public async Task RemoveAsync(Guid id)
		{
			FilterDefinition<T> filter = filterBuilder.Eq(entity => entity.Id, id);
			await dbCollection.DeleteOneAsync(filter);
		}
	}
}