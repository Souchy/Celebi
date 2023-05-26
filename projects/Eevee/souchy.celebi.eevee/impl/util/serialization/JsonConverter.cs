using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

//namespace souchy.celebi.umbreon.common.util
namespace souchy.celebi.eevee.impl.util.serialization
{
    public class ObjectIdConverter : JsonConverter<ObjectId>
    {
        public override ObjectId ReadJson(JsonReader reader, Type objectType, ObjectId existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string) reader.Value;
            return new ObjectId(s);
        }
        public override void WriteJson(JsonWriter writer, ObjectId value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
    public class IIDJsonConverter : JsonConverter<IID>
    {
        public override IID ReadJson(JsonReader reader, Type objectType, IID existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string) reader.Value;
            return (IID) s;
        }
        public override void WriteJson(JsonWriter writer, IID value, JsonSerializer serializer)
        {
            writer.WriteValue(value.value);
        }
    }
    public class IIDJsonConverter<T> : JsonConverter<T> where T : IID
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string) reader.Value;
            return (T) Activator.CreateInstance(typeof(T), s);
        }
        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteValue(value.value);
        }
    }


    public class IStringEntitysonConverter : JsonConverter<IStringEntity>
    {
        public override IStringEntity ReadJson(JsonReader reader, Type objectType, IStringEntity existingValue, bool hasExistingValue, JsonSerializer serializer)
            => StringEntity.Create(reader.ReadAsString());
        public override void WriteJson(JsonWriter writer, IStringEntity value, JsonSerializer serializer)
            => writer.WriteValue(value.ToString());
    }
    public class CharacTypeJsonConverter : JsonConverter<CharacteristicType>
    {
        public override CharacteristicType ReadJson(JsonReader reader, Type objectType, CharacteristicType existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new CharacteristicId(reader.ReadAsInt32().Value).GetCharactType();
        public override void WriteJson(JsonWriter writer, CharacteristicType value, JsonSerializer serializer)
            => writer.WriteValue(value.ID);
    }
    public class CharacIdJsonConverter : JsonConverter<CharacteristicId>
    {
        public override CharacteristicId ReadJson(JsonReader reader, Type objectType, CharacteristicId existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //var val2 = reader.ReadAsInt32();
            var val = reader.Value;
            //var val = reader.ReadAsString();
            //var val2 = reader.ReadAsString();
            //var val3 = reader.ReadAsString();
            var num = 0;
            var resul = int.TryParse(val.ToString(), out num);
            return new CharacteristicId(num);
        }//.ReadAsInt32().Value);
        public override void WriteJson(JsonWriter writer, CharacteristicId value, JsonSerializer serializer)
            => writer.WriteValue(value.ID.ToString());
    }
    public class IValueZoneTypeJsonConverter : JsonConverter<IValue<ZoneType>>
    {
        public override IValue<ZoneType> ReadJson(JsonReader reader, Type objectType, IValue<ZoneType> existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Value<ZoneType>(Enum.Parse<ZoneType>(reader.ReadAsString()));
        public override void WriteJson(JsonWriter writer, IValue<ZoneType> value, JsonSerializer serializer)
            => writer.WriteValue(Enum.GetName<ZoneType>(value.value));
    }
    public class IValueElementJsonConverter : JsonConverter<IValue<ElementType>>
    {
        public override IValue<ElementType> ReadJson(JsonReader reader, Type objectType, IValue<ElementType> existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Value<ElementType>(Enum.Parse<ElementType>(reader.ReadAsString()));
        public override void WriteJson(JsonWriter writer, IValue<ElementType> value, JsonSerializer serializer)
            => writer.WriteValue(Enum.GetName<ElementType>(value.value));
    }
    public class IValueIntJsonConverter : JsonConverter<IValue<int>>
    {
        public override IValue<int> ReadJson(JsonReader reader, Type objectType, IValue<int> existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Value<int>(reader.ReadAsInt32() ?? default);
        public override void WriteJson(JsonWriter writer, IValue<int> value, JsonSerializer serializer)
            => writer.WriteValue(value.value);
    }
    public class IValueBoolJsonConverter : JsonConverter<IValue<bool>>
    {
        public override IValue<bool> ReadJson(JsonReader reader, Type objectType, IValue<bool> existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Value<bool>(reader.ReadAsBoolean() ?? default);
        public override void WriteJson(JsonWriter writer, IValue<bool> value, JsonSerializer serializer)
            => writer.WriteValue(value.value);
    }
    public class IValueDoubleJsonConverter : JsonConverter<IValue<double>>
    {
        public override IValue<double> ReadJson(JsonReader reader, Type objectType, IValue<double> existingValue, bool hasExistingValue, JsonSerializer serializer)
            => new Value<double>(reader.ReadAsDouble() ?? default);
        public override void WriteJson(JsonWriter writer, IValue<double> value, JsonSerializer serializer)
            => writer.WriteValue(value.value);
    }
    public class IEntitySetJsonConverter : JsonConverter<IEntitySet<IID>>
    {
        public override IEntitySet<IID> ReadJson(JsonReader reader, Type objectType, IEntitySet<IID> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = Activator.CreateInstance<EntitySet<IID>>();
            string[] arr = (string[]) reader.Value;
            foreach (var a in arr)
                value.Add((IID) a);
            return value;
        }
        public override void WriteJson(JsonWriter writer, IEntitySet<IID> value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach(var s in value.Values.Select(i => i.ToString()))
                writer.WriteValue(s);
            //writer.WriteValue(value.Values.Select(i => i.ToString()).ToArray());
            writer.WriteEndArray();
        }
    }
    public class IEntityListJsonConverter : JsonConverter<IEntityList<IID>>
    {
        public override IEntityList<IID> ReadJson(JsonReader reader, Type objectType, IEntityList<IID> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = Activator.CreateInstance<EntityList<IID>>();
            string[] arr = (string[]) reader.Value;
            foreach (var a in arr)
                value.Add((IID) a);
            return value;
        }
        public override void WriteJson(JsonWriter writer, IEntityList<IID> value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            foreach (var s in value.Values.Select(i => i.ToString()))
                writer.WriteValue(s);
            writer.WriteEndArray();
        }
    }
    //public class IStatsJsonConverter : JsonConverter<IStats>
    //{
    //    public override IStats ReadJson(JsonReader reader, Type objectType, IStats existingValue, bool hasExistingValue, JsonSerializer serializer)
    //    {
    //        var value = Activator.CreateInstance<Stats>();
    //        var json = reader.Value.ToString();
    //        Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    //        //string[] arr = (string[]) reader.Value;
    //        //foreach (var a in arr)
    //        //    value.Add((IID) a);
    //        //return value;
    //    }
    //    public override void WriteJson(JsonWriter writer, IStats value, JsonSerializer serializer)
    //    {
    //        writer.WriteStartArray();
    //        foreach (var s in value.Values.Select(i => i.ToString()))
    //            writer.WriteValue(s);
    //        //writer.WriteValue(value.Values.Select(i => i.ToString()).ToArray());
    //        writer.WriteEndArray();
    //    }
    //}

    //public class CreatureConverter : CustomCreationConverter<ICreatureModel>
    //{
    //    public override ICreatureModel Create(Type objectType) => Activator.CreateInstance<CreatureModel>();
    //}
    public class CreationConverter : JsonConverter
    {
        public Type interfaceType;
        public Type implType;
        public CreationConverter(Type interfaceType)
        {
            this.interfaceType = interfaceType;
            var list = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => !t.IsInterface && CanConvert(t))
                .ToList();
            if(list.Count == 1)
                this.implType = list.FirstOrDefault();
        }

        public override bool CanConvert(Type objectType)
        {
            return interfaceType.IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }
            var value = Activator.CreateInstance(implType); //Create(objectType);
            if (value == null)
            {
                throw new JsonSerializationException("No object created.");
            }
            serializer.Populate(reader, value);
            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
        }
    }




}
