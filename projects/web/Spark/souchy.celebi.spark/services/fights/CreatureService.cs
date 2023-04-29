using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;

namespace souchy.celebi.spark.services.fights
{
    public class CreatureService
    {
        private readonly IMongoCollection<ICreature> _creatures;

        public CreatureService(MongoFightsDbService service) => _creatures = service.GetMongoCollection<ICreature>(nameof(ICreature));

        public async Task<List<ICreature>> GetAsync() =>
            await _creatures.Find(_ => true).ToListAsync();

        public async Task<ICreature?> GetAsync(string id) =>
            await _creatures.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ICreature newCreature) =>
            await _creatures.InsertOneAsync(newCreature);

        public async Task UpdateAsync(string id, ICreature updatedCreature) =>
            await _creatures.ReplaceOneAsync(x => x.entityUid == id, updatedCreature);

        public async Task RemoveAsync(string id) =>
            await _creatures.DeleteOneAsync(x => x.entityUid == id);

    }
}
