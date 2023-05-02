using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class StatsService
    {
        private readonly IMongoCollection<IStats> _statsCollection;

        public StatsService(MongoModelsDbService service)
        {
            _statsCollection = service.GetMongoCollection<IStats>(nameof(IStats));
        }

        public async Task<List<IStats>> GetAsync() =>
            await _statsCollection.Find(_ => true).ToListAsync();

        public async Task<IStats?> GetAsync(string id) =>
            await _statsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IStats newStats) =>
            await _statsCollection.InsertOneAsync(newStats);

        public async Task<ReplaceOneResult> UpdateAsync(string id, IStats updatedStats) =>
            await _statsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedStats);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _statsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
