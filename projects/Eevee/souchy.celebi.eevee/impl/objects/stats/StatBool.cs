using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        //public StatValueType valueType => StatValueType.Bool;
        public IID entityUid { get; set; }
        public StatType statId { get; init; }

        public bool value { get; set; }

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
