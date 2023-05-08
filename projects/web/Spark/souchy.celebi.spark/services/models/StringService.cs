using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.shared;

namespace souchy.celebi.spark.services.models
{
    /// <summary>
    /// TODO: support i18n multilangue: 1 string needs the same ID in all languages.
    /// So maybe copy the actions here to every other languages automatically.
    /// </summary>
    public class StringService
    {
        private readonly IEnumerable<IMongoCollection<IStringEntity>> _collections;

        public StringService(MongoModelsDbService service)
            => _collections = Enum.GetNames<I18NType>().Select(lang => service.GetMongoCollection<IStringEntity>(lang));

        public async Task<List<IStringEntity>> GetAsync(I18NType lang) =>
            await getCollection(lang).Find(_ => true).ToListAsync();
        public async Task<List<IStringEntity>> GetAsync(I18NType lang, FilterDefinition<IStringEntity> filter) =>
            await getCollection(lang).Find(filter).ToListAsync();
        public async Task<IStringEntity?> GetAsync(I18NType lang, string id) =>
            await getCollection(lang).Find(x => x.entityUid == id).FirstOrDefaultAsync();
        public async Task<ReplaceOneResult> UpdateAsync(I18NType lang, string id, IStringEntity updatedStringEntity) =>
            await getCollection(lang).ReplaceOneAsync(x => x.entityUid == id, updatedStringEntity);

        /// <summary>
        /// Create a new string entity in all I18n collections
        /// </summary>
        public async Task CreateAsync(IStringEntity newStringEntity)
        {
            //await _stringsCollection.InsertOneAsync(newStringEntity);
            foreach (var col in _collections)
                await col.InsertOneAsync(newStringEntity);
        }
        /// <summary>
        /// Remove a string entity from all I18n collections
        /// </summary>
        public async Task<DeleteResult> RemoveAsync(string id)
        {
            //await _stringsCollection.DeleteOneAsync(x => x.entityUid == id);
            DeleteResult result = null;
            foreach (var col in _collections)
                result = await col.DeleteOneAsync(x => x.entityUid == id);
            return result;
        }

        private IMongoCollection<IStringEntity> getCollection(I18NType lang)
            => _collections.First(c => c.CollectionNamespace.CollectionName == lang.ToString());

    }
}
