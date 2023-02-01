using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbreon.common.util
{
    public class IIDJsonConverter : JsonConverter<IID>
    {
        public override IID ReadJson(JsonReader reader, Type objectType, IID existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string) reader.Value;
            return new IID(s);
        }
        public override void WriteJson(JsonWriter writer, IID value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

    public class IStringEntityJsonConverter : JsonConverter<IStringEntity>
    {
        public override IStringEntity ReadJson(JsonReader reader, Type objectType, IStringEntity existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            //reader.Path
            var jo = JObject.Load(reader);
            //jo.Path


            return null;
        }

        public override void WriteJson(JsonWriter writer, IStringEntity value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
