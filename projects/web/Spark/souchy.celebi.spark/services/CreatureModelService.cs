using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using Spark.souchy.celebi.spark.models;

namespace souchy.celebi.spark.services
{
    public class CreatureModelService
    {
        private readonly IMongoCollection<ICreatureModel> _creatureModelsCollection;

        //public class CreatureModelDatabaseSettings
        //{
        //    public string ConnectionString { get; set; } = null!;

        //    public string DatabaseName { get; set; } = null!;

        //    public string CreatureModelsCollectionName { get; set; } = null!;
        //}

        public CreatureModelService(IOptions<CelebiModelsDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _creatureModelsCollection = mongoDatabase.GetCollection<ICreatureModel>(nameof(ICreatureModel));
        }

        public async Task<List<ICreatureModel>> GetAsync() =>
            await _creatureModelsCollection.Find(_ => true).ToListAsync();

        public async Task<ICreatureModel?> GetAsync(string id) =>
            await _creatureModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ICreatureModel newCreatureModel) =>
            await _creatureModelsCollection.InsertOneAsync(newCreatureModel);

        public async Task UpdateAsync(string id, ICreatureModel updatedCreatureModel) =>
            await _creatureModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedCreatureModel);

        public async Task RemoveAsync(string id) =>
            await _creatureModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
