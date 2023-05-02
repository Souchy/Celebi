using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class ConsumableProductService
    {
        private readonly IMongoCollection<ConsumableProduct> _products;

        public ConsumableProductService(MongoMetaDbService service) => _products = service.GetMongoCollection<ConsumableProduct>(nameof(ConsumableProduct));

        public async Task<List<ConsumableProduct>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<ConsumableProduct?> GetAsync(ObjectId id) =>
            await _products.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ConsumableProduct product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(ObjectId id, ConsumableProduct product) =>
            await _products.ReplaceOneAsync(x => x._id == id, product);

        public async Task RemoveAsync(ObjectId id) =>
            await _products.DeleteOneAsync(x => x._id == id);

    }
}
