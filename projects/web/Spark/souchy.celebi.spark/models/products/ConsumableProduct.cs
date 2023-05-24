using MongoDB.Bson;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// A consumable you can buy in the shop.
    /// It costs currency.
    /// When consumed, it gives one or all of the item models contained 
    ///     - battlepass
    ///     - lootbox
    ///     - 
    /// </summary>
    public class ConsumableProduct : Product
    {
        /// <summary>
        /// Wether the consumable gives All items or just one Random
        /// </summary>
        public ConsumableType Type { get; set; }
        /// <summary>
        /// List of items that can drop from the consumable box
        /// </summary>
        public List<ConsumableDrop> Drops { get; set; } = new();

    }
    /// <summary>
    /// Determines wether the consumable gives all of its contained items or just one random
    /// </summary>
    public enum ConsumableType
    {
        All,
        Random
    }
    public class ConsumableDrop
    {
        /// <summary>
        /// Product ID that will be "purchased" for free (create a transaction?)
        /// </summary>
        public ObjectId ProductId { get; set; }

        /// <summary>
        /// CreatureModel, SpellModel, CreatureSkin... maybe BoardSkins?
        /// </summary>
        //public IID ModelID { get; set; }
        public int Weight { get; set; }
    }
}
