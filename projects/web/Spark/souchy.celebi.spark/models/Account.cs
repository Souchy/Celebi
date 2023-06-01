using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// Pretty much everything is optional <br></br>
    /// We can authenticate with either a token ID or email+pass <br></br>
    /// There is no username, we just use emails
    /// </summary>
    public class Account : MongoUser
    {
        public AccountInfo Info { get; set; }
    }

    public class AccountInfo
    {
        #region User information
        /// <summary>
        /// Unique
        /// Pseudo
        /// </summary>
        public string DisplayName { get; set; }
        //public string Name { get; set; }
        //public string FamilyName { get; set; }
        //public string Picture { get; set; }
        /// <summary>
        /// Only 1 access object per ip. Update the LastAccess when using the same ip.
        /// Ask for verification when using a new IP (ex: poe asks email confirmation).
        /// </summary>
        public List<IpAccess> AccessByIp = new List<IpAccess>();
        #endregion

        #region Owned 
        /// <summary>
        /// Amount of game currency owned. Can be purchased through ShopCurrency items.
        /// </summary>
        public int Currency { get; set; }
        /// <summary>
        /// List of models owned. Can be purcharges through ShopProduct items. <br></br>
        /// Purchasable models include CreatureModel, SpellModel, CreatureSkin...
        /// </summary>
        public List<IID> OwnedModels { get; set; } = new();
        #endregion

        // i think we dont list fights here?, rather go fights.where(f => f.date >= lastWeek && f.players.contains(this)) ? 

        public void CheckAccess(string ipAddress)
        {
            var access = AccessByIp.Find(a => a.IpAddress == ipAddress);
            if (access != null)
            {
                if (access.IsExpired())
                {
                    // expired
                }
                if (!access.Verified)
                {
                    // unverified
                }
                // good
            }
            // add new access & verify
        }
        public void AddAccess()
        {
            
        }
    }

    public enum AccountType
    {
        User = 0,
        VerifiedUser = 1,
        Admin = 999
    }

    public class AccountRole : MongoRole
    {
        public AccountType type { get; set; } = AccountType.User;
        public AccountRole(AccountType type) : base(Enum.GetName(type))
        {
            this.type = type;
        }
    }

    public class IpAccess
    {
        public static readonly TimeSpan expirationTime = TimeSpan.FromDays(30);
        public string IpAddress { get; set; }
        public DateTime LastAccess { get; set; } 
        public bool Verified { get; set; }
        public string RefreshToken { get; set; }
        public bool IsExpired() => LastAccess.Add(IpAccess.expirationTime) < DateTime.UtcNow;
    }

}
