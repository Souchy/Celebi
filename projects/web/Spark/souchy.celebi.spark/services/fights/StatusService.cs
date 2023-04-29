using MongoDB.Driver;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;

namespace souchy.celebi.spark.services.fights
{
    public class StatusService
    {
        private readonly IMongoCollection<IStatusContainer> _StatusContainers;

        public StatusService(MongoFightsDbService service) => _StatusContainers = service.GetMongoCollection<IStatusContainer>(nameof(IStatusContainer));

        public async Task<List<IStatusContainer>> GetAsync() =>
            await _StatusContainers.Find(_ => true).ToListAsync();

        public async Task<IStatusContainer?> GetAsync(string id) =>
            await _StatusContainers.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IStatusContainer newStatusContainer) =>
            await _StatusContainers.InsertOneAsync(newStatusContainer);

        public async Task UpdateAsync(string id, IStatusContainer updatedStatusContainer) =>
            await _StatusContainers.ReplaceOneAsync(x => x.entityUid == id, updatedStatusContainer);

        public async Task RemoveAsync(string id) =>
            await _StatusContainers.DeleteOneAsync(x => x.entityUid == id);

    }
}
