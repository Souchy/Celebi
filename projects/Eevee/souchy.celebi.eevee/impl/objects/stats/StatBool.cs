using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        public IID entityUid { get; set; }
        public StatType statId { get; init; }

        private bool _value;
        public bool value { get => _value; 
            set
            {
                _value = value;
                this.GetEntityBus()?.publish(Enum.GetName(statId), this);
                this.GetEntityBus()?.publish(this);
            }
        }

        private StatBool() { }
        public static StatBool Create(StatType st, bool value = false)
            => new StatBool() //st, value)
            {
                statId = st,
                value = value,
                entityUid = Eevee.RegisterIID<IStatBool>()
            };

        public IStat copy() => Create(statId, value); //new StatBool(statId, value);

        public void Dispose()
        {
            Eevee.DisposeIID<IStatBool>(entityUid);
        }
    }
}
