using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class ShopProductService
    {
        private readonly IMongoCollection<ShopProduct> _products;

        public ShopProductService(MongoMetaDbService service) => _products = service.GetMongoCollection<ShopProduct>(nameof(ShopProduct));

        public async Task<List<ShopProduct>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<ShopProduct?> GetAsync(ObjectId id) =>
            await _products.Find(x => x.MongoID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShopProduct product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(ObjectId id, ShopProduct product) =>
            await _products.ReplaceOneAsync(x => x.MongoID == id, product);

        public async Task RemoveAsync(ObjectId id) =>
            await _products.DeleteOneAsync(x => x.MongoID == id);

    }
}
