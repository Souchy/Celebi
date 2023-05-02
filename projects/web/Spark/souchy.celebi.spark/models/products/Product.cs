using MongoDB.Bson;

namespace souchy.celebi.spark.models
{
    public abstract class Product
    {
        /// <summary>
        /// Mongo's ID contains a date (=release date) so we can sort products by most recent
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// Price in game currency (or amount of currency to obtain in the case of ShopCurrency)
        /// </summary>
        public int Currency { get; set; }
        /// <summary>
        /// If the product is available in the shop or not. 
        /// We need to keep track of older products for transactions even if they are not available anymore
        /// </summary>
        public bool IsAvailableInShop { get; set; }

        /// <summary>
        /// Can the product drop randomly from fights won
        /// </summary>
        public bool IsAvailableInDrop { get; set; }
        /// <summary>
        /// The weight determines the chance to drop the product after winning a fight
        /// </summary>
        public int DropWeight { get; set; }

        /// <summary>
        /// For Consumables: limit of transactions (bought)
        /// For Permanents: limit of owned (owned)
        /// </summary>
        public LimitType LimitType { get; set; }
        /// <summary>
        /// For Consumables: limit of transactions
        /// For Permanents: limit of owned
        /// </summary>
        public int LimitPerAccount { get; set; }
    }

    public enum LimitType
    {
        Owned,
        Bought
    }
}
