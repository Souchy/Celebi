using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : EntityDictionary<CharacteristicId, IStat>, IStats
    {
        public static readonly string dicName = nameof(dic);
        //[JsonIgnore]
        //public IEnumerable<IStat> Values2 => base.Values;
        //[JsonIgnore]
        //public IEnumerable<CharacteristicId> Keys2 => base.Keys;
        //[JsonIgnore]
        //public IEnumerable<KeyValuePair<CharacteristicId, IStat>> Pairs2 => base.dic;
        //[JsonProperty]
        //[BsonDictionaryOptions(Representation = DictionaryRepresentation.Document)]
        //protected Dictionary<CharacteristicId, IStat> dic2 => base.dic;

        private Stats() { }
        public static new IStats Create() => new Stats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public T Get<T>(CharacteristicId statId) where T : IStat
        {
            return (T) Get(statId);
        }

        public IStats anonymousCopy()
        {
            var c = new Stats();
            foreach(var s in Pairs) 
                c.Set(s.Key, s.Value);
            return c;
        }

    }
}