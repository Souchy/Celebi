using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.controllers;

namespace souchy.celebi.spark.services.fights
{
    public class FightService
    {
        private readonly IMongoCollection<IFight> _Fights;

        public FightService(MongoFightsDbService service) => _Fights = service.GetMongoCollection<IFight>(nameof(IFight));

        public async Task<List<IFight>> GetAsync() =>
            await _Fights.Find(_ => true).ToListAsync();

        public async Task<IFight?> GetAsync(string id) =>
            await _Fights.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IFight newFight) =>
            await _Fights.InsertOneAsync(newFight);

        public async Task UpdateAsync(string id, IFight updatedFight) =>
            await _Fights.ReplaceOneAsync(x => x.entityUid == id, updatedFight);

        public async Task RemoveAsync(string id) =>
            await _Fights.DeleteOneAsync(x => x.entityUid == id);

    }
}
