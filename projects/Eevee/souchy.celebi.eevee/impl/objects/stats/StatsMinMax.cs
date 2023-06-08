using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.stats
{
    /// <summary>
    /// NEVERMIND. Condition always has a comparator (>, <, ==...)
    /// So use that instead of StatsMinMax. aka if you need a min and max, juste create 2 conditions. 
    /// Or if you need the exact value or just min or just max, then you can create just 1 condition.
    /// 
    /// <para></para>
    /// 
    /// Used for StatsCondition filters.
    /// </summary>
    public class StatsMinMax
    {
        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        [BsonDictionaryOptions(Representation = DictionaryRepresentation.Document)]
        public IEntityDictionary<CharacteristicId, IStat> min { get; set; } = EntityDictionary<CharacteristicId, IStat>.Create();

        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        [BsonDictionaryOptions(Representation = DictionaryRepresentation.Document)]
        public IEntityDictionary<CharacteristicId, IStat> max { get; set; } = EntityDictionary<CharacteristicId, IStat>.Create();

        public StatsMinMax() { }

        public StatsMinMax copy()
        {
            var c = new StatsMinMax();
            //max.copy();
            foreach (var s in min.Pairs)
                c.min.Set(s.Key, s.Value.copy(true));
            foreach (var s in max.Pairs)
                c.max.Set(s.Key, s.Value.copy(true));
            return c;
        }
        public void Clear() => min.Clear();

        public void Dispose()
        {
            min.Dispose();
            max.Dispose();
        }
    }
}
