using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class SpellModelService
    {
        private readonly IMongoCollection<ISpellModel> _spellModelsCollection;

        public SpellModelService(MongoModelsDbService service)
        {
            _spellModelsCollection = service.GetMongoCollection<ISpellModel>(nameof(ISpellModel));
        }

        public async Task<List<ISpellModel>> GetAsync() =>
            await _spellModelsCollection.Find(_ => true).ToListAsync();

        public async Task<ISpellModel?> GetAsync(string id) =>
            await _spellModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ISpellModel newSpellModel) =>
            await _spellModelsCollection.InsertOneAsync(newSpellModel);

        public async Task<ReplaceOneResult> UpdateAsync(string id, ISpellModel updatedSpellModel) =>
            await _spellModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedSpellModel);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _spellModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
