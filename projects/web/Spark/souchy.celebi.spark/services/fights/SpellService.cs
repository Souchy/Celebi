using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.spark.services.fights
{
    public class SpellService
    {
        private readonly IMongoCollection<ISpell> _Spells;

        public SpellService(MongoFightsDbService service) => _Spells = service.GetMongoCollection<ISpell>(nameof(ISpell));

        public async Task<List<ISpell>> GetAsync() =>
            await _Spells.Find(_ => true).ToListAsync();

        public async Task<ISpell?> GetAsync(string id) =>
            await _Spells.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ISpell newSpell) =>
            await _Spells.InsertOneAsync(newSpell);

        public async Task UpdateAsync(string id, ISpell updatedSpell) =>
            await _Spells.ReplaceOneAsync(x => x.entityUid == id, updatedSpell);

        public async Task RemoveAsync(string id) =>
            await _Spells.DeleteOneAsync(x => x.entityUid == id);

    }
}
