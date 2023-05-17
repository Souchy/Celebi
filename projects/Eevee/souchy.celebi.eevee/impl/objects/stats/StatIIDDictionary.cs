using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.objects.stats
{
    /// <summary>
    /// Maps a entity's ObjectId to a Stat (ex: number of spells cast per entity)
    /// </summary>
    public class EntityStatDictionary : IEntityStatDictionary
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        public CharacteristicId statId { get; init; }
        public Dictionary<ObjectId, IStat> value { get; set; }

        public void set(ObjectId key, IStat val)
        {
            value[key] = val;
            this.GetEntityBus()?.publish(statId, this);
            this.GetEntityBus()?.publish(this);
        }

        private EntityStatDictionary() { }
        public EntityStatDictionary(CharacteristicId st, Dictionary<ObjectId, IStat> value = null)
        {
            this.statId = st;
            this.value = value != null ? value : new Dictionary<ObjectId, IStat>();
        }
        public static EntityStatDictionary Create(CharacteristicId st, Dictionary<ObjectId, IStat> value = null)
            => new EntityStatDictionary(st, value)
            {
                entityUid = Eevee.RegisterIIDTemporary()
            };

        public void Add(IStat s)
        {
            if (s is EntityStatDictionary b)
            {
                foreach (var p in value)
                {
                    if (b.value.ContainsKey(p.Key))
                        p.Value.Add(b.value[p.Key]);
                }
            }
        }

        public IStat copy(bool anonymous = false)
        {
            var stat = anonymous ? new EntityStatDictionary(statId) : Create(statId);
            stat.value = new Dictionary<ObjectId, IStat>();
            foreach (var pair in value)
                stat.value[pair.Key] = pair.Value.copy(anonymous);
            return stat;
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            foreach (var pair in value)
                pair.Value.Dispose();
        }
    }
}
