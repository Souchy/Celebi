using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// An item you can buy in the shop.
    /// It links to a model like a Spell, Creature, Skin...
    /// The modelId is then added the the owned models on the account.
    /// It costs currency.
    /// </summary>
    public class ShopProduct
    {
        /// <summary>
        /// Mongo's ID contains a date so we can sort products by most recent
        /// </summary>
        public ObjectId MongoID { get; set; }

        /// <summary>
        /// CreatureModel, SpellModel, CreatureSkin... maybe BoardSkins?
        /// </summary>
        public IID ModelID { get; set; }

        /// <summary>
        /// Price in game currency
        /// </summary>
        public int CurrencyPrice { get; set; }
        /// <summary>
        /// If the product is available in the shop or not. 
        /// We need to keep track of older products for transactions even if they are not available anymore
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
