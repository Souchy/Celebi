﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        public ObjectId entityUid { get; set; }
        public CharacteristicId statId { get; init; }

        private bool _value;
        public bool value { get => _value; 
            set
            {
                _value = value;
                this.GetEntityBus()?.publish(statId, this);
                this.GetEntityBus()?.publish(this);
            }
        }

        private StatBool() { }
        public static StatBool Create(CharacteristicId st, bool value = false)
            => new StatBool()
            {
                statId = st,
                value = value,
                entityUid = Eevee.RegisterIIDTemporary()
            };

        public IStat copy() => Create(statId, value); 

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

        public void Add(IStat s)
        {
            if (s is StatBool b)
                this.value |= b.value;
        }
    }
}
