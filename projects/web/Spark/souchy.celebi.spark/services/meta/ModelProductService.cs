using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class ModelProductService
    {
        private readonly IMongoCollection<ModelProduct> _products;

        public ModelProductService(MongoMetaDbService service) => _products = service.GetMongoCollection<ModelProduct>(nameof(ModelProduct));

        public async Task<List<ModelProduct>> GetAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<ModelProduct?> GetAsync(ObjectId id) =>
            await _products.Find(x => x._id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ModelProduct product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(ObjectId id, ModelProduct product) =>
            await _products.ReplaceOneAsync(x => x._id == id, product);

        public async Task RemoveAsync(ObjectId id) =>
            await _products.DeleteOneAsync(x => x._id == id);

    }
}
