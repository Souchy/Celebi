using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.stats;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public CharacteristicId statId { get; init; }

        private int _value;
        public int value { 
            get => _value; 
            set
            {
                _value = value;
                this.GetEntityBus()?.publish(statId, this); //Enum.GetName(statId), this);
                this.GetEntityBus()?.publish(this);
            }
        }

        private StatSimple() { }
        public StatSimple(CharacteristicId st, int value = 0)
        {
            this.statId = st;
            this.value = value;
        }
        public static StatSimple Create(CharacteristicId st, int value = 0)
            => new StatSimple(st, value)
            {
                entityUid = Eevee.RegisterIIDTemporary(),
            };

        public void Add(IStat s)
        {
            if (s is StatSimple b)
                this.value += b.value;
        }

        public IStat copy(bool anonymous = false)
        {
            if (anonymous) return new StatSimple(this.statId, value);
            else return Create(statId, value);
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }
}
