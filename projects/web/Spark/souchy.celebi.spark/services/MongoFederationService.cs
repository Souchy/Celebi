using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models.aggregations;
using System.Text.RegularExpressions;

namespace souchy.celebi.spark.services
{
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
            this.db = _client.GetDatabase("CelebiAggregation");
            this.strings = db.GetCollection<IStringEntity>(nameof(IStringEntity));
            this.creatures = db.GetCollection<ICreatureModel>(nameof(ICreatureModel));
            this.spells = db.GetCollection<ISpellModel>(nameof(ISpellModel));
            this.statuses = db.GetCollection<IStatusModel>(nameof(IStatusModel));
        }
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

        public async Task<IEnumerable<CreatureModelAggregation>> FindCreatures()
        {
            return await creatures.Aggregate<CreatureModelAggregation>(creatureAggregation).ToListAsync();
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
        
        private static readonly BsonDocument?[] creatureAggregation = new []
        {
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", "ISpellModel" },
                    { "localField", "spellIds._v" },
                    { "foreignField", "_id" },
                    { "as", "spells" }
                }),
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", "IStatusModel" },
                    { "localField", "statusPassiveIds._v" },
                    { "foreignField", "_id" },
                    { "as", "statusPassives" }
                }),
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", "IStats" },
                    { "localField", "statsId" },
                    { "foreignField", "_id" },
                    { "as", "stats" }
                }),
            new BsonDocument("$unwind",
            new BsonDocument
                {
                    { "path", "$stats" },
                    { "preserveNullAndEmptyArrays", true }
                }),
            new BsonDocument("$project",
            new BsonDocument
                {
                    { "_t", 1 },
                    { "modelUid", 1 },
                    { "nameId", 1 },
                    { "descriptionId", 1 },
                    { "stats", 1 },
                    { "spells", 1 },
                    { "statusPassives", 1 },
                    { "skinIds", 1 }
                })
        };
    }
}
