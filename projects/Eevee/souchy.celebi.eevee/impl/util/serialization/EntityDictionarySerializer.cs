using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System.ComponentModel;
using System.Globalization;

namespace souchy.celebi.eevee.impl.util.serialization
{
    public class EntityDictionarySerializer<TKey, TValue> : IBsonSerializer<EntityDictionary<TKey, TValue>> 
    {
        public Type ValueType => typeof(EntityDictionary<TKey, TValue>);

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, EntityDictionary<TKey, TValue> value)
        {
            if (value == null) return;
            value.serialize(context);
        }

        public EntityDictionary<TKey, TValue> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var dic = BsonSerializer.Deserialize<Dictionary<TKey, TValue>>(context.Reader);
            var edic = EntityDictionary<TKey, TValue>.Create(dic);
            return (EntityDictionary<TKey, TValue>) edic;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            this.Serialize(context, (EntityDictionary<TKey, TValue>) value);
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return Deserialize(context, args);
        }
    }

    //public class EntityDictionaryJsonConverter<TKey, TValue> : JsonConverter<EntityDictionary<TKey, TValue>> //where T : IID
    //{
    //    //public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
    //    //{
    //    //    string s = (string) reader.Value;
    //    //    return (T) Activator.CreateInstance(typeof(T), s);
    //    //}
    //    //public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
    //    //{
    //    //    writer.WriteValue(value.value);
    //    //}
    //    public override EntityDictionary<TKey, TValue> ReadJson(JsonReader reader, Type objectType, EntityDictionary<TKey, TValue> existingValue, bool hasExistingValue, JsonSerializer serializer)
    //    {
    //        object o = reader.Value;

    //        Dictionary<TKey, TValue> dic = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(o.ToString());
    //        throw new NotImplementedException();
    //    }

    //    public override void WriteJson(JsonWriter writer, EntityDictionary<TKey, TValue> value, JsonSerializer serializer)
    //    {
    //        writer.WriteValue(value.Pairs);
    //    }
    //}

    //public class EntityDictionaryTypeConverter<TKey, TValue> : TypeConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
    //        sourceType == typeof(string);
    //    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
    //        destinationType == typeof(string);
    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {

    //        return value switch
    //        {
    //            string s => Activator.CreateInstance(typeof(T), s),
    //            null => null,
    //            _ => throw new ArgumentException($"Cannot convert from {value} to IID", nameof(value))
    //        };
    //    }
    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType == typeof(string))
    //        {
    //            if (typeof(T).IsAssignableFrom(value.GetType()))
    //            {
    //                return value.ToString();
    //            }
    //        }
    //        throw new ArgumentException($"Cannot convert {value ?? "(null)"} to {destinationType}", nameof(destinationType));
    //    }
    //}

}
