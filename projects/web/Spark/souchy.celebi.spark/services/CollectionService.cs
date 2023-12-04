using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.services
{
    public class CollectionService<T> where T : IEntity
    {
        protected readonly IMongoCollection<T> _collection;
        public IMongoCollection<T> Collection => _collection;

        public CollectionService(IMongoCollection<T> collection) => _collection = collection;

        public FilterDefinition<T> filterId(ObjectId id) => Builders<T>.Filter.Eq("_id", id); // cant use entityUid on db :( 
        public FilterDefinition<T> filterIds(IEnumerable<ObjectId> ids) => Builders<T>.Filter.In("_id", ids);
        public FilterDefinition<T> filterModelId(IID id) => Builders<T>.Filter.Eq(nameof(IEntityModeled.modelUid), id.value);


        #region Create
        public async Task CreateAsync(T newModel) =>
            await _collection.InsertOneAsync(newModel);
        #endregion

        #region Get List
        //public IFindFluent<T, T> GetFluentAsync() =>
        //    _collection.Find(_ => true);
        public async Task<List<T>> GetAsync() =>
            await _collection.Find(_ => true).ToListAsync();
        public async Task<Dictionary<ObjectId, T>> GetDictionaryAsync() =>
            (await _collection.Find(_ => true).ToListAsync()).ToDictionary(t => t.entityUid, t => t);
        public async Task<List<T>> GetAsync(FilterDefinition<T> filter) =>
            await _collection.Find(filter).ToListAsync();
        public async Task<List<T>> GetInIdsAsync(IEnumerable<ObjectId> ids) =>
            await _collection.Find(filterIds(ids)).ToListAsync();
        #endregion

        #region Get One
        public async Task<T?> GetOneAsync(ObjectId id) =>
            await _collection.Find(filterId(id)).FirstOrDefaultAsync();
        public async Task<T> GetOneAsync(IID id) =>
            await _collection.Find(filterModelId(id)).FirstOrDefaultAsync();
        public async Task<T> GetOneAsync(FilterDefinition<T> filter) =>
            await _collection.Find(filter).FirstOrDefaultAsync();
        #endregion

        #region Update
        public async Task<ReplaceOneResult> UpdateAsync(ObjectId id, T updatedModel) =>
            await _collection.ReplaceOneAsync(filterId(id), updatedModel);
        public async Task<ReplaceOneResult> UpdateAsync(IID id, T updatedModel) =>
            await _collection.ReplaceOneAsync(filterModelId(id), updatedModel);
        public async Task<UpdateResult> UpdateAsync(ObjectId id, UpdateDefinition<T> update) =>
            await _collection.UpdateManyAsync(filterId(id), update);
        public async Task<UpdateResult> UpdateAsync(IID id, UpdateDefinition<T> update) =>
            await _collection.UpdateManyAsync(filterModelId(id), update);
        public async Task<ReplaceOneResult> UpdateAsync(FilterDefinition<T> filter, T updatedModel) =>
            await _collection.ReplaceOneAsync(filter, updatedModel);
        public async Task<UpdateResult> UpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update) =>
            await _collection.UpdateManyAsync(filter, update);
        #endregion

        #region Remove
        public async Task<DeleteResult> RemoveAsync(ObjectId id) =>
            await _collection.DeleteOneAsync(filterId(id));
        public async Task<DeleteResult> RemoveAsync(IID id) =>
            await _collection.DeleteOneAsync(filterModelId(id));
        public async Task<DeleteResult> RemoveAsync(FilterDefinition<T> filter) =>
            await _collection.DeleteOneAsync(filter);
        #endregion

    }


}
