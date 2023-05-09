using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.spark.services
{
    public class CollectionService<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public CollectionService(IMongoCollection<T> collection) => _collection = collection;

        public async Task<List<T>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();
        public async Task<List<T>> GetAsync(FilterDefinition<T> filter) =>
            await _collection.Find(filter).ToListAsync();
        public async Task<T?> GetAsync(ObjectId id) =>
            await _collection.Find(x => x.entityUid == id).FirstOrDefaultAsync();

        public async Task CreateAsync(T newStats) =>
            await _collection.InsertOneAsync(newStats);

        public async Task<ReplaceOneResult> UpdateAsync(ObjectId id, T updatedStats) =>
            await _collection.ReplaceOneAsync(x => x.entityUid == id, updatedStats);

        public async Task<DeleteResult> RemoveAsync(ObjectId id) =>
            await _collection.DeleteOneAsync(x => x.entityUid == id);
        public async Task<DeleteResult> RemoveAsync(FilterDefinition<T> filter) =>
            await _collection.DeleteOneAsync(filter);
    }


}
