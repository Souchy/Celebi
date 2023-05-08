﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class IIDSerializer : IBsonSerializer<IID>
    {
        public Type ValueType => typeof(IID);

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IID value)
        {
            if (value == null) return;
            var val = value.ToString();
            //var oid = new ObjectId(val);
            BsonSerializer.Serialize(context.Writer, val);
            //if (value == null) value = IID.GenerateOID();
            //BsonSerializer.Serialize(context.Writer, value.ToString());
        }

        public IID Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var id = BsonSerializer.Deserialize<string>(context.Reader);
            //var id = BsonSerializer.Deserialize<ObjectId>(context.Reader);
            return (IID) id;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, (IID) value);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return this.Deserialize(context, args);
        }
    }
}