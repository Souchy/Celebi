using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;

namespace souchy.celebi.spark.services.models
{
    public class CreatureModelService
    {
        private readonly IMongoCollection<ICreatureModel> _creatureModelsCollection;

        public CreatureModelService(MongoModelsDbService service)
        {
            _creatureModelsCollection = service.GetMongoCollection<ICreatureModel>(nameof(ICreatureModel));
        }

        public async Task<List<ICreatureModel>> GetAsync() =>
            await _creatureModelsCollection.Find(_ => true).ToListAsync();

        public async Task<List<ICreatureModel>> GetFilteredAsync(FilterDefinition<ICreatureModel> filter) =>
            await _creatureModelsCollection.Find(filter).ToListAsync();

        public async Task<ICreatureModel?> GetAsync(string id) =>
            await _creatureModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ICreatureModel newCreatureModel) =>
            await _creatureModelsCollection.InsertOneAsync(newCreatureModel);

        public async Task<ReplaceOneResult> UpdateAsync(string id, ICreatureModel updatedCreatureModel) =>
            await _creatureModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedCreatureModel);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _creatureModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
