using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spark.Models;

namespace Spark.Services
{
    public class MongoService
    {
        private readonly IMongoClient _client;

        public MongoService(IOptions<CelebiModelsDatabaseSettings> settings)
            => _client = new MongoClient(settings.Value.ConnectionString);
        public IMongoDatabase GetDatabase(string databaseName) => _client.GetDatabase(databaseName);
        public IMongoCollection<T> GetCollection<T>(IOptions<CollectionSettings> settings)
            => _client.GetDatabase(settings.Value.DatabaseName).GetCollection<T>(settings.Value.CollectionName);
    }
    public class MongoModelsService
    {
        private readonly IMongoDatabase db;

        public MongoModelsService(MongoService mongoService) => db = mongoService.GetDatabase("Models");
        public IMongoCollection<T> GetMongoCollection<T>(IOptions<CollectionSettings> settings)
            => db.GetCollection<T>(settings.Value.CollectionName);
    }
    public class MongoFightsService
    {
        private readonly IMongoDatabase db;
        public MongoFightsService(MongoService mongoService) => db = mongoService.GetDatabase("Fights");
        public IMongoCollection<T> GetMongoCollection<T>(IOptions<CollectionSettings> settings)
            => db.GetCollection<T>(settings.Value.CollectionName);
    }
    public class MongoExtraService
    {
        private readonly IMongoDatabase db;
        public MongoExtraService(MongoService mongoService) => db = mongoService.GetDatabase("Extra");
        public IMongoCollection<T> GetMongoCollection<T>(IOptions<CollectionSettings> settings)
            => db.GetCollection<T>(settings.Value.CollectionName);
    }
}
