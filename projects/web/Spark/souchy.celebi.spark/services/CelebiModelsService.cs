using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spark.souchy.celebi.spark.models;

namespace souchy.celebi.spark.services
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
    public class MongoModelsDbService
    {
        private readonly IMongoDatabase db;

        public MongoModelsDbService(MongoService mongoService) => db = mongoService.GetDatabase("Celebi#Models");
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName) => db.GetCollection<T>(collectionName);
    }
    public class MongoFightsDbService
    {
        private readonly IMongoDatabase db;
        public MongoFightsDbService(MongoService mongoService) => db = mongoService.GetDatabase("Celebi#Fights");
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName) => db.GetCollection<T>(collectionName);
    }
    public class MongoExtraDbService
    {
        private readonly IMongoDatabase db;
        public MongoExtraDbService(MongoService mongoService) => db = mongoService.GetDatabase("Celebi#Extra");
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName) => db.GetCollection<T>(collectionName);
    }
    public class MongoMetaDbService
    {
        private readonly IMongoDatabase db;
        public MongoMetaDbService(MongoService mongoService) => db = mongoService.GetDatabase("Celebi#Meta");
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName) => db.GetCollection<T>(collectionName);
    }
}
