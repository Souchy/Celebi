using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// Pretty much everything is optional
    /// We can authenticate either with a token ID, or user+pass or email+pass
    /// </summary>
    public class Account
    {
        #region User authentication
        /// <summary>
        /// Unique
        /// ID from JWT token
        /// </summary>
        public string ID { get; }

        /// <summary>
        /// Unique
        /// Username is optional
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password is hashed and optional
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region User information from Google, Facebook, Twitter, Microsoft or custom
        /// <summary>
        /// Unique
        /// Can also authenticate with email+pass instead of user+pass
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Unique
        /// Pseudo
        /// </summary>
        public string DisplayName { get; set; }
        public AccountType Type { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string Picture { get; set; }
        public DateTime CreationDate { get; set; }
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
        /// <summary>
        /// Track every purchase/refund in the shop: currency bought, models bought/refunded, what was the date and how much they paid
        /// </summary>
        public List<ObjectId> Transactions { get; set; } = new();
        #endregion

        // i think we dont list fights here?, rather go fights.where(f => f.date >= lastWeek && f.players.contains(this)) ? 

    }

    public enum AccountType
    {
        User = 0,
        VerifiedUser = 1,
        Admin = 999
    }


}
