using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accountCollection;

        public AccountService(MongoMetaDbService service) => _accountCollection = service.GetMongoCollection<Account>(nameof(Account));

        public async Task<Account?> FindAuthAccount(Account account)
        {
            if (account.ID != "")
            {
                var acc = await _accountCollection.Find(x => x.ID == account.ID).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            if (account.Email != "")
            {
                var acc = await _accountCollection.Find(x => x.Email == account.Email && x.Password == account.Password).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            if (account.Username != "")
            {
                var acc = await _accountCollection.Find(x => x.Username == account.Username && x.Password == account.Password).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            return null;
        }
        //public async Task<Account> GetByID(string ID)
        //{
        //    return await _accountCollection.Find(x => x.ID == ID).SingleOrDefaultAsync();
        //}
        //public async Task<Account> GetByUserPass(string user, string pass)
        //{
        //    return await _accountCollection.Find(x => x.Username == user && x.Password == pass).SingleOrDefaultAsync();
        //}
        //public async Task<Account> GetByEmailPass(string email, string pass)
        //{
        //    return await _accountCollection.Find(x => x.Email == email && x.Password == pass).SingleOrDefaultAsync();
        //}

        public async Task<bool> Create(Account account)
        {
            var similar = await FindSimilar(account);
            if (similar != null) return false;
            await _accountCollection.InsertOneAsync(account);
            return true;
        }

        public async Task Update(Account account)
        {
            //return null;
        }

        public async Task<DeleteResult> Delete(Account account)
        {
            return await _accountCollection.DeleteOneAsync(x => x.ID == account.ID);
        }

        public async Task<Account?> FindSimilar(Account account)
        {
            if (account.ID != "")
            {
                var acc = await _accountCollection.Find(x => x.ID == account.ID).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            if (account.Email != "")
            {
                var acc = await _accountCollection.Find(x => x.Email == account.Email).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            if (account.Username != "")
            {
                var acc = await _accountCollection.Find(x => x.Username == account.Username).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            if (account.DisplayName != "")
            {
                var acc = await _accountCollection.Find(x => x.DisplayName == account.DisplayName).SingleOrDefaultAsync();
                if (acc != null) return acc;
            }
            return null;
        }

    }
}
