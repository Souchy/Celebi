using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class ShopCurrencyService
    {
        private readonly IMongoCollection<ShopCurrency> _products;

        public ShopCurrencyService(MongoMetaDbService service) => _products = service.GetMongoCollection<ShopCurrency>(nameof(ShopCurrency));

        public async Task<List<ShopCurrency>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<ShopCurrency?> GetAsync(ObjectId id) =>
            await _products.Find(x => x.MongoID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShopCurrency product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(ObjectId id, ShopCurrency product) =>
            await _products.ReplaceOneAsync(x => x.MongoID == id, product);

        public async Task RemoveAsync(ObjectId id) =>
            await _products.DeleteOneAsync(x => x.MongoID == id);
    }
}
