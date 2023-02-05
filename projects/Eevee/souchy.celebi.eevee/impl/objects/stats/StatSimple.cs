using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        public IID entityUid { get; set; }
        public StatType statId { get; init; }

        private int _value;
        public int value { 
            get => _value; 
            set
            {
                _value = value;
                this.GetEntityBus()?.publish(Enum.GetName(statId), this);
                this.GetEntityBus()?.publish(this);
            }
        }

        private StatSimple() { }
        public static StatSimple Create(StatType st, int value = 0)
            => new StatSimple() //st, value)
            {
                statId = st,
                value = value,
                entityUid = Eevee.RegisterIID<IStatSimple>(),
            };

        public IStat copy() => Create(statId, value); //new StatSimple(StatType, value);

        public void Dispose()
        {
            Eevee.DisposeIID<IStatSimple>(entityUid);
        }
    }
}
