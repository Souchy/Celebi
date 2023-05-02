using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class EffectService
    {
        private readonly IMongoCollection<IEffect> _effectCollection;
        // + IEffectModel ?

        public EffectService(MongoModelsDbService service)
        {
            _effectCollection = service.GetMongoCollection<IEffect>(nameof(IEffect));
        }

        public async Task<List<IEffect>> GetAsync() =>
            await _effectCollection.Find(_ => true).ToListAsync();

        public async Task<IEffect?> GetAsync(string id) =>
            await _effectCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IEffect newEffect) =>
            await _effectCollection.InsertOneAsync(newEffect);

        public async Task<ReplaceOneResult> UpdateAsync(string id, IEffect updatedEffect) =>
            await _effectCollection.ReplaceOneAsync(x => x.entityUid == id, updatedEffect);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _effectCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
