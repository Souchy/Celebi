using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.impl.shared;
using System.Security.Cryptography.Xml;

namespace souchy.celebi.spark.services
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string Federation { get; set; } = null!;
        public string ModelsDB { get; set; } = null!;
        public string FightsDB { get; set; } = null!;
        public string MetaDB { get; set; } = null!;
        public string I18NDB { get; set; } = null!;
    }
    public class MongoClientService
    {
        private readonly IMongoClient _client;

        public MongoClientService(IOptions<MongoSettings> settings)
            => _client = new MongoClient(settings.Value.ConnectionString);
        public IMongoDatabase GetDatabase(string databaseName) => _client.GetDatabase(databaseName);
    }
    public abstract class DbService
    {
        protected readonly IMongoDatabase db;
        protected DbService(IMongoDatabase db) => this.db = db;
        public IMongoCollection<T> GetMongoCollection<T>(string? collectionName = null)
            => db.GetCollection<T>(collectionName ?? typeof(T).Name);
        public CollectionService<T> GetMongoService<T>(string? collectionName = null) where T : IEntity
            => new CollectionService<T>(db.GetCollection<T>(collectionName ?? typeof(T).Name));
    }
    public class MongoModelsDbService : DbService
    {
        public MongoModelsDbService(MongoClientService mongoService, IOptions<MongoSettings> settings) 
            : base(mongoService.GetDatabase(settings.Value.ModelsDB))
        { }
    }
    public class MongoFightsDbService : DbService
    {
        public MongoFightsDbService(MongoClientService mongoService, IOptions<MongoSettings> settings)
            : base(mongoService.GetDatabase(settings.Value.FightsDB))
        { }
    }
    public class MongoMetaDbService : DbService
    {
        public MongoMetaDbService(MongoClientService mongoService, IOptions<MongoSettings> settings)
            : base(mongoService.GetDatabase(settings.Value.FightsDB))
        { }
    }
    public class MongoI18NDbService
    {
        private readonly IMongoDatabase db;

        public MongoI18NDbService(MongoClientService mongoService, IOptions<MongoSettings> settings)
            => db = mongoService.GetDatabase(settings.Value.I18NDB);
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
            => db.GetCollection<T>(collectionName);
    }
}
