﻿using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.objects.stats
{
    internal class StatIIDDictionary : IStatIIDDictionary
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        public CharacteristicId statId { get; init; }
        public Dictionary<IID, IStat> value { get; set; }

        public void set(IID key, IStat val)
        {
            value[key] = val;
            this.GetEntityBus()?.publish(statId, this);
            this.GetEntityBus()?.publish(this);
        }

        private StatIIDDictionary() { }
        public static StatIIDDictionary Create(CharacteristicId st, Dictionary<IID, IStat> value = null)
            => new StatIIDDictionary()
            {
                statId = st,
                value = value != null ? value : new Dictionary<IID, IStat>(),
                entityUid = Eevee.RegisterIIDTemporary()
            };

        public void Add(IStat s)
        {
            if (s is StatIIDDictionary b)
            {
                foreach(var p in value)
                {
                    if(b.value.ContainsKey(p.Key))
                        p.Value.Add(b.value[p.Key]);
                }
            }
        }

        public IStat copy()
        {
            var stat = Create(statId);
            stat.value = new Dictionary<IID, IStat>();
            foreach (var pair in value)
                stat.value[pair.Key] = pair.Value.copy();
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
