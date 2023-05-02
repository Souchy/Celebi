using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accountCollection;

        public AccountService(MongoMetaDbService service) => _accountCollection = service.GetMongoCollection<Account>(nameof(Account));

        public async Task<Account> FindByDisplayName(string displayName)
        {
            return await _accountCollection.Find(x => x.Info.DisplayName == displayName).SingleOrDefaultAsync();   
        }

        public async Task<List<Account>> GetAsync() =>
            await _accountCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAsync(ObjectId id) =>
            await _accountCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Account product) =>
            await _accountCollection.InsertOneAsync(product);

        public async Task<ReplaceOneResult> UpdateAsync(ObjectId id, Account product) =>
            await _accountCollection.ReplaceOneAsync(x => x.Id == id, product);

        public async Task<DeleteResult> RemoveAsync(ObjectId id) =>
            await _accountCollection.DeleteOneAsync(x => x.Id == id);

    }
}
