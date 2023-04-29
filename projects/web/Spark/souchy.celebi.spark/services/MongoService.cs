using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Spark.souchy.celebi.spark.models;

namespace souchy.celebi.spark.services
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string ModelsDB { get; set; } = null!;
        public string FightsDB { get; set; } = null!;
        public string MetaDB { get; set; } = null!;
    }
    public class MongoService
    {
        private readonly IMongoClient _client;

        public MongoService(IOptions<MongoSettings> settings)
            => _client = new MongoClient(settings.Value.ConnectionString);
        public IMongoDatabase GetDatabase(string databaseName) => _client.GetDatabase(databaseName);
    }
    public class MongoModelsDbService
    {
        private readonly IMongoDatabase db;

        public MongoModelsDbService(MongoService mongoService, IOptions<MongoSettings> settings)
            => db = mongoService.GetDatabase(settings.Value.ModelsDB);
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
            => db.GetCollection<T>(collectionName);
    }
    public class MongoFightsDbService
    {
        private readonly IMongoDatabase db;
        public MongoFightsDbService(MongoService mongoService, IOptions<MongoSettings> settings)
            => db = mongoService.GetDatabase(settings.Value.FightsDB);
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
            => db.GetCollection<T>(collectionName);
    }
    public class MongoMetaDbService
    {
        private readonly IMongoDatabase db;
        public MongoMetaDbService(MongoService mongoService, IOptions<MongoSettings> settings)
            => db = mongoService.GetDatabase(settings.Value.MetaDB);
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
            => db.GetCollection<T>(collectionName);
    }
}
