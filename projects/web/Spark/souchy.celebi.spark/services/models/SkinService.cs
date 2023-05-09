using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;

namespace souchy.celebi.spark.services.models
{
    public class SkinService
    {
        private readonly IMongoCollection<ICreatureSkin> _CreatureSkinsCollection;

        public SkinService(MongoModelsDbService service) => _CreatureSkinsCollection = service.GetMongoCollection<ICreatureSkin>(nameof(ICreatureSkin));

        public async Task<List<ICreatureSkin>> GetAsync() =>
            await _CreatureSkinsCollection.Find(_ => true).ToListAsync();

        public async Task<ICreatureSkin?> GetAsync(ObjectId id) =>
            await _CreatureSkinsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ICreatureSkin newCreatureSkin) =>
            await _CreatureSkinsCollection.InsertOneAsync(newCreatureSkin);

        public async Task UpdateAsync(ObjectId id, ICreatureSkin updatedCreatureSkin) =>
            await _CreatureSkinsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedCreatureSkin);

        public async Task RemoveAsync(ObjectId id) =>
            await _CreatureSkinsCollection.DeleteOneAsync(x => x.entityUid == id);
    }
}
