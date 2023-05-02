using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class StatusModelService
    {
        private readonly IMongoCollection<IStatusModel> _StatusModelsCollection;

        public StatusModelService(MongoModelsDbService service)
        {
            _StatusModelsCollection = service.GetMongoCollection<IStatusModel>(nameof(IStatusModel));
        }

        public async Task<List<IStatusModel>> GetAsync() =>
            await _StatusModelsCollection.Find(_ => true).ToListAsync();

        public async Task<IStatusModel?> GetAsync(string id) =>
            await _StatusModelsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IStatusModel newStatusModel) =>
            await _StatusModelsCollection.InsertOneAsync(newStatusModel);

        public async Task<ReplaceOneResult> UpdateAsync(string id, IStatusModel updatedStatusModel) =>
            await _StatusModelsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedStatusModel);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _StatusModelsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
