using MongoDB.Bson;

namespace souchy.celebi.spark.models
{
    public class Transaction
    {
        /// <summary>
        /// MongoID will contain the date of the transaction
        /// </summary>
        public ObjectId Id { get; set; }
        /// <summary>
        /// Id of the product bought
        /// </summary>
        public ObjectId ProductId { get; set; }
        /// <summary>
        /// Types include: Purchase, Refund
        /// </summary>
        public TransactionType Type { get; set; }
    }

    public enum TransactionType { 
        Purchase,
        Refund
    }

}
