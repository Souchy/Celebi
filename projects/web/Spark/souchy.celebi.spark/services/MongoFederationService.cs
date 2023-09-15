using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
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

        public async Task<IEnumerable<ICreatureModelView>> FindCreatures()
        {
            return await creatures.Aggregate<ICreatureModelView>(creatureAggregation).ToListAsync();
        }

        public async Task<IEnumerable<TextEntityAggregation>> GetCreaturesTextAggregation(List<ObjectId> ids)
        {
            return await creatures.Aggregate<TextEntityAggregation>(filterTextAggregation(ids)).ToListAsync();
        }
        public async Task<IEnumerable<TextEntityAggregation>> GetSpellsTextAggregation(List<ObjectId> ids)
        {
            return await spells.Aggregate<TextEntityAggregation>(filterTextAggregation(ids)).ToListAsync();
        }
        public async Task<IEnumerable<TextEntityAggregation>> GetStatusesTextAggregation(List<ObjectId> ids)
        {
            return await statuses.Aggregate<TextEntityAggregation>(filterTextAggregation(ids)).ToListAsync();
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

        private static BsonDocument?[] filterTextAggregation(List<ObjectId> ids)
        {
            var filter = new[]
            {
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", nameof(IStringEntity) },
                        { "localField", "nameId" },
                        { "foreignField", "_id" },
                        { "as", nameof(TextEntityAggregation.name) }
                    }),
                new BsonDocument("$unwind",
                new BsonDocument("path", "$name")),
                new BsonDocument("$lookup",
                new BsonDocument
                    {
                        { "from", nameof(IStringEntity) },
                        { "localField", "descriptionId" },
                        { "foreignField", "_id" },
                        { "as", nameof(TextEntityAggregation.description) }
                    }),
                new BsonDocument("$unwind",
                new BsonDocument("path", "$description")),
                new BsonDocument("$project",
                new BsonDocument
                    {
                        { "_id", 1 },
                        { nameof(IEntityModeled.modelUid), 1 },
                        { nameof(TextEntityAggregation.name), 1 },
                        { nameof(TextEntityAggregation.description), 1 }
                    })
            }.ToList();
            if(ids?.Count >= 1)
            {
                filter.Insert(0,
                    new BsonDocument("$match",
                        new BsonDocument("_id",
                            new BsonDocument("$in",
                                new BsonArray(ids)
                            )
                        )
                    ));
            }
            return filter.ToArray();
        }

        private static readonly BsonDocument?[] creatureAggregation = new []
        {
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", nameof(ISpellModel) },
                    { "localField", nameof(ICreatureModel.spellIds) + "._v" },
                    { "foreignField", "_id" },
                    { "as", nameof(ICreatureModelView.spells) }
                }),
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", nameof(IStatusModel) },
                    { "localField", nameof(ICreatureModel.statusPassiveIds) + "._v" },
                    { "foreignField", "_id" },
                    { "as", nameof(ICreatureModelView.statusPassives) }
                }),
            new BsonDocument("$lookup",
            new BsonDocument
                {
                    { "from", nameof(IStats) },
                    { "localField", nameof(ICreatureModel.statsId) },
                    { "foreignField", "_id" },
                    { "as", nameof(ICreatureModelView.stats) }
                }),
            new BsonDocument("$unwind",
            new BsonDocument
                {
                    { "path", "$" + nameof(ICreatureModelView.stats) },
                    { "preserveNullAndEmptyArrays", true }
                }),
            new BsonDocument("$project",
            new BsonDocument
                {
                    { "_t", 1 },
                    { nameof(ICreatureModelView.modelUid), 1 },
                    { nameof(ICreatureModelView.nameId), 1 },
                    { nameof(ICreatureModelView.descriptionId), 1 },
                    { nameof(ICreatureModelView.stats), 1 },
                    { nameof(ICreatureModelView.spells), 1 },
                    { nameof(ICreatureModelView.statusPassives), 1 },
                    { "skinIds", 1 } 
                })
        };
    }
}
