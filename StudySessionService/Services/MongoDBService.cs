using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerformanceTrackerStudySessionService.Services
{
    public class MongoDBService<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDBService(IConfiguration config, string collectionName)
        {
            var client = new MongoClient(config.GetConnectionString("MongoDBAtlas"));
            var database = client.GetDatabase(config["DatabaseName"]);
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return null;
            }

            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task UpdateAsync(string id, T item)
        {
            var itemType = typeof(T);
            var idProperty = itemType.GetProperty("Id");

            if (idProperty != null)
            {
                idProperty.SetValue(item, id);
            }
            else
            {
                throw new InvalidOperationException("The item does not contain an Id property.");
            }

            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await _collection.ReplaceOneAsync(filter, item);

            if (result.MatchedCount == 0)
            {
                Console.WriteLine("No document matched the filter.");
            }
            else if (result.ModifiedCount == 0)
            {
                Console.WriteLine("Document matched but was not modified.");
            }
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            await _collection.DeleteOneAsync(filter);
        }
    }
}
