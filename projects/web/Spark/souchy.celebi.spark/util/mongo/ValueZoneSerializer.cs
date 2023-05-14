using MongoDB.Bson.Serialization;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.spark.util.mongo
{
    /// <summary>
    /// This maps CharacIds to a String in the Mongo database
    /// </summary>
    public class ValueZoneSerializer : IBsonSerializer<IValue<ZoneType>>
    {
        public Type ValueType => typeof(IValue<ZoneType>);

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IValue<ZoneType> value)
        {
            BsonSerializer.Serialize(context.Writer, value.value);
        }
        public IValue<ZoneType> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var id = BsonSerializer.Deserialize<ZoneType>(context.Reader);
            return new Value<ZoneType>(id);
        }
        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, (IValue<ZoneType>) value);
        }
        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return this.Deserialize(context, args);
        }
    }
}
