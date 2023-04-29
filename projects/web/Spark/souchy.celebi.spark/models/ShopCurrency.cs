using MongoDB.Bson;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// A bundle/package of currency that you can buy for real money.
    /// There might be a few bundles like : [5 currency, 10, 30, 50] for the same price in cad or something idk
    /// Sometimes there could be a sale bundle so you add for example 5 currency for 3$ instead of 5 for 5 
    /// </summary>
    public class ShopCurrency
    {
        /// <summary>
        /// Mongo's ID contains a date so we can sort products by most recent
        /// </summary>
        public ObjectId MongoID { get; set; }

        /// <summary>
        /// Amount of game currency to buy
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Real money price
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// If the product is available in the shop or not. 
        /// We need to keep track of older products for transactions even if they are not available anymore
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
