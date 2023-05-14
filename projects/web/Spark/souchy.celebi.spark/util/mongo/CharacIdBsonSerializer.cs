using MongoDB.Bson.Serialization;
using souchy.celebi.eevee.enums.characteristics;

namespace souchy.celebi.spark.util.mongo
{
    /// <summary>
    /// This maps CharacIds to a String in the Mongo database
    /// </summary>
    public class CharacIdBsonSerializer : IBsonSerializer<CharacteristicId>
    {
        public Type ValueType => typeof(CharacteristicId);

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CharacteristicId value)
        {
            //if (value == null) return;
            //var val = value.ToString();
            //var oid = new ObjectId(val);
            BsonSerializer.Serialize(context.Writer, value.ID.ToString());
            //if (value == null) value = IID.GenerateOID();
            //BsonSerializer.Serialize(context.Writer, value.ToString());
        }

        public CharacteristicId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var id = BsonSerializer.Deserialize<string>(context.Reader);
            //var id = BsonSerializer.Deserialize<ObjectId>(context.Reader);
            return new CharacteristicId(int.Parse(id));
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, (CharacteristicId) value);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return this.Deserialize(context, args);
        }
    }
}
