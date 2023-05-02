using MongoDB.Bson;

namespace souchy.celebi.spark.models
{
    /// <summary>
    /// A bundle/package of currency that you can buy for real money.
    /// There might be a few bundles like : [5 currency, 10, 30, 50] for the same price in cad or something idk
    /// Sometimes there could be a sale bundle so you add for example 5 currency for 3$ instead of 5 for 5 
    /// </summary>
    public class CurrencyProduct : Product 
    {
        /// <summary>
        /// Real Money price
        /// </summary>
        public float Price { get; set; }    

        /// <summary>
        /// Amount of game currency to buy
        /// </summary>
        //public int Amount { get; set; }

    }
}
