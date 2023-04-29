using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;
using Spark.souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.models
{
    public class SpellModelService
    {
        private readonly IMongoCollection<ISpellModel> _SpellModelsCollection;

        public SpellModelService(MongoModelsDbService service)
        {
            _SpellModelsCollection = service.GetMongoCollection<ISpellModel>(nameof(ISpellModel));
        }

        public async Task<List<ISpellModel>> GetAsync() =>
            await _SpellModelsCollection.Find(_ => true).ToListAsync();

        public async Task<ISpellModel?> GetAsync(string id) =>
            await _SpellModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ISpellModel newSpellModel) =>
            await _SpellModelsCollection.InsertOneAsync(newSpellModel);

        public async Task UpdateAsync(string id, ISpellModel updatedSpellModel) =>
            await _SpellModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedSpellModel);

        public async Task RemoveAsync(string id) =>
            await _SpellModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
