using MongoDB.Driver;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.spark.models;

namespace souchy.celebi.spark.services.meta
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accountCollection;

        public AccountService(MongoMetaDbService service) => _accountCollection = service.GetMongoCollection<Account>(nameof(Account));

        //public async Task<Account?> FindAuthorizedAccount(Account account)
        //{
        //    if (account.Id != null)
        //    {
        //        var acc = await _accountCollection.Find(x => x.Id == account.Id).SingleOrDefaultAsync();
        //        if (acc != null) return acc;
        //    }
        //    if (account.Email != "")
        //    {
        //        var acc = await _accountCollection.Find(x => x.Email == account.Email && x.PasswordHash == account.PasswordHash).SingleOrDefaultAsync();
        //        if (acc != null) return acc;
        //    }
        //    //if (account.Username != "")
        //    //{
        //    //    var acc = await _accountCollection.Find(x => x.Username == account.Username && x.Password == account.Password).SingleOrDefaultAsync();
        //    //    if (acc != null) return acc;
        //    //}
        //    return null;
        //}

        //public async Task<bool> Create(Account account)
        //{
        //    var similar = await Find(account);
        //    if (similar != null) return false;
        //    await _accountCollection.InsertOneAsync(account);
        //    return true;
        //}

        //public async Task Update(Account account)
        //{
        //    //return null;
        //}

        //public async Task<DeleteResult> Delete(Account account)
        //{
        //    return await _accountCollection.DeleteOneAsync(x => x.Id == account.Id); // TokenID
        //}

        //public async Task<Account?> Find(Account account)
        //{
        //    if (account.Id != null)
        //    {
        //        var acc = await _accountCollection.Find(x => x.Id == account.Id).SingleOrDefaultAsync();
        //        if (acc != null) return acc;
        //    }
        //    if (account.Email != null)
        //    {
        //        var acc = await _accountCollection.Find(x => x.Email == account.Email).SingleOrDefaultAsync();
        //        if (acc != null) return acc;
        //    }
        //    //if (account.Username != "")
        //    //{
        //    //    var acc = await _accountCollection.Find(x => x.Username == account.Username).SingleOrDefaultAsync();
        //    //    if (acc != null) return acc;
        //    //}
        //    if (account.Info.DisplayName != null)
        //    {
        //        var acc = await _accountCollection.Find(x => x.Info.DisplayName == account.Info.DisplayName).SingleOrDefaultAsync();
        //        if (acc != null) return acc;
        //    }
        //    return null;
        //}

        public async Task<Account> FindByDisplayName(string displayName)
        {
            return await _accountCollection.Find(x => x.Info.DisplayName == displayName).SingleOrDefaultAsync();   
        }
    }
}
