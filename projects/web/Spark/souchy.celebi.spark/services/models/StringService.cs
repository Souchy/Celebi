using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    public class StringService
    {
        private readonly IMongoCollection<IStringEntity> _stringsCollection;

        public StringService(MongoModelsDbService service)
        {
            _stringsCollection = service.GetMongoCollection<IStringEntity>(nameof(IStringEntity));
        }

        public async Task<List<IStringEntity>> GetAsync() =>
            await _stringsCollection.Find(_ => true).ToListAsync();
        public async Task<List<IStringEntity>> GetAsync(FilterDefinition<IStringEntity> filter) =>
            await _stringsCollection.Find(filter).ToListAsync();

        public async Task<IStringEntity?> GetAsync(string id) =>
            await _stringsCollection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(IStringEntity newStringEntity) =>
            await _stringsCollection.InsertOneAsync(newStringEntity);

        public async Task<ReplaceOneResult> UpdateAsync(string id, IStringEntity updatedStringEntity) =>
            await _stringsCollection.ReplaceOneAsync(x => x.entityUid == id, updatedStringEntity);

        public async Task<DeleteResult> RemoveAsync(string id) =>
            await _stringsCollection.DeleteOneAsync(x => x.entityUid == id);

    }
}
