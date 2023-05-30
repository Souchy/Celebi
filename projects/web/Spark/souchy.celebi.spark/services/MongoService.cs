using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;

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
    public class MongoFederationService
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<IStringEntity> strings;
        private readonly IMongoCollection<ICreatureModel> creatures;
        private readonly IMongoCollection<ISpellModel> spells;
        private readonly IMongoCollection<IStatusModel> statuses; // TODO

        private readonly ProjectionDefinition<BsonDocument> projection = Builders<BsonDocument>.Projection.Include("_id");
        private FilterDefinition<BsonDocument> filter(string str) => Builders<BsonDocument>.Filter.Or(
                    Builders<BsonDocument>.Filter.Regex("name.value", str),
                    Builders<BsonDocument>.Filter.Regex("desc.value", str)
            );
        public MongoFederationService(IOptions<MongoSettings> settings)
        {
            _client = new MongoClient(settings.Value.Federation);
            this.db = _client.GetDatabase("VirtualDatabase0");
            this.strings = db.GetCollection<IStringEntity>(nameof(IStringEntity));
            this.creatures = db.GetCollection<ICreatureModel>(nameof(ICreatureModel));
            this.spells = db.GetCollection<ISpellModel>(nameof(ISpellModel));
            this.statuses = db.GetCollection<IStatusModel>(nameof(IStatusModel));
        }
        private BsonDocument?[] pipelineStrings(string str) => new[]
            {
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", "strings" },
                        { "localField", nameof(ICreatureModel.nameId) },
                        { "foreignField", "_id" },
                        { "as", "name" }
                    }),
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", "strings" },
                        { "localField", nameof(ICreatureModel.descriptionId) },
                        { "foreignField", "_id" },
                        { "as", "desc" }
                    }),
                new BsonDocument("$unwind",
                new BsonDocument("path", "$name")),
                new BsonDocument("$unwind",
                new BsonDocument("path", "$desc")),
                new BsonDocument("$match",
                new BsonDocument("$or",
                new BsonArray
                        {
                            new BsonDocument("name.value", new Regex($"(?i){str}")),
                            new BsonDocument("desc.value", new Regex($"(?i){str}"))
                        })),
                new BsonDocument("$project", new BsonDocument
                    {
                        { "name", 0 },
                        { "desc", 0 }
                    }
                )
            };
        public async Task<List<ICreatureModel>> FindCreaturesByString(string str)
        {
            return await creatures.Aggregate<ICreatureModel>(pipelineStrings(str)).ToListAsync();
        }
        public async Task<List<ISpellModel>> FindSpellsByString(string str)
        {
            return await spells.Aggregate<ISpellModel>(pipelineStrings(str)).ToListAsync();
        }
        public async Task<List<IStatusModel>> FindStatusesByString(string str)
        {
            return await spells.Aggregate<IStatusModel>(pipelineStrings(str)).ToListAsync();
        }
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
        public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
            => db.GetCollection<T>(collectionName);
        public IMongoCollection<T> GetMongoCollection<T>()
            => db.GetCollection<T>(typeof(T).Name);
        public CollectionService<T> GetMongoService<T>() where T : IEntity
            => new CollectionService<T>(db.GetCollection<T>(typeof(T).Name));
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
