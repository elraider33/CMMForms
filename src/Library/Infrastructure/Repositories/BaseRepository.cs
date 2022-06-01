using Library.Domain.Options;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Library.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly MongoClient _client;
        protected IMongoDatabase _database;

        public BaseRepository(IMongoOptions options, ILogger logger)
        {
            _client = new MongoClient(options.ConnectionString);
            _database = _client.GetDatabase(options.DatabaseName);
        }
    }
}