using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class CurrencyProductService
    {
        private readonly IMongoCollection<CurrencyProduct> _products;

        public CurrencyProductService(MongoMetaDbService service) => _products = service.GetMongoCollection<CurrencyProduct>(nameof(CurrencyProduct));

        public async Task<List<CurrencyProduct>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<CurrencyProduct?> GetAsync(ObjectId id) =>
            await _products.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(CurrencyProduct product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(ObjectId id, CurrencyProduct product) =>
            await _products.ReplaceOneAsync(x => x._id == id, product);

        public async Task RemoveAsync(ObjectId id) =>
            await _products.DeleteOneAsync(x => x._id == id);
    }
}
