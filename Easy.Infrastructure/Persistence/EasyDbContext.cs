using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Easy.Infrastructure.Persistence
{
    public class EasyDbContext
    {
        private readonly IMongoDatabase _database;

        public EasyDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}


