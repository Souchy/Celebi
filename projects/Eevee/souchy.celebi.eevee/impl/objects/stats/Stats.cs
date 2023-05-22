using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : EntityDictionary<CharacteristicId, IStat>, IStats
    {
        public static readonly string dicName = nameof(dic);

        public Dictionary<CharacteristicId, MathEquation> growth { get; set; } = new();

        protected Stats() { }
        public static new IStats Create() => new Stats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public T Get<T>(CharacteristicType ct) where T : IStat => Get<T>(ct.ID);
        public T Get<T>(CharacteristicId statId) where T : IStat => (T) Get(statId);

        public void applyGrowth(int currentTurn)
        {
            foreach (var key in growth.Keys)
            {
                var grow = growth[key];
                if(key.GetCharactType().StatValueType == StatValueType.Simple)
                {
                    var charac = Get<IStatSimple>(key);
                    charac.value += grow.getAsInt(currentTurn);
                }
                if (key.GetCharactType().StatValueType == StatValueType.Bool)
                {
                    var charac = Get<IStatBool>(key);
                    charac.value = grow.getAsBool(currentTurn, charac.value);
                }
            }
        }

        public IStats copy(bool anonymous = false)
        {
            var c = new Stats();
            foreach (var s in Pairs)
                c.Set(s.Key, s.Value.copy(anonymous));
            return c;
        }

    }
}