using MongoDB.Bson;

namespace souchy.celebi.spark.models
{
    public class Transaction
    {
        /// <summary>
        /// MongoID will contain the date of the transaction
        /// </summary>
        public ObjectId _id { get; set; }
        /// <summary>
        /// Id of the product bought
        /// </summary>
        public ObjectId ProductId { get; set; }
        /// <summary>
        /// Types include: Purchase, Refund
        /// </summary>
        public TransactionType Type { get; set; } = TransactionType.Purchase;
        /// <summary>
        /// Price at the time of the transaction. The product's price could change later.
        /// Important for refunds? 
        /// </summary>
        public float Price { get; set; }  
    }

    public enum TransactionType { 
        Purchase,
        Refund
    }

}
