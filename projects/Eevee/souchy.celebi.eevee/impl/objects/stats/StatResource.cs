using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatResource : IStatResource
    {
        public IID entityUid { get; set; }
        public StatType statId { get; init; }

        public int current { get; set; }
        public int currentMax { get; set; }
        public int initialMax { get; set; }

        public (int current, int currentMax, int initialMax) value { 
            get => (current, currentMax, initialMax);
            set 
            {
                this.current = value.current;
                this.currentMax = value.currentMax;
                this.initialMax = value.initialMax;
            }
        }

        private StatResource() { }
        public static StatResource Create(StatType st, int current = 0, int currentMax = 0, int initialMax = 0) => new StatResource()
        {
            current = current,
            currentMax = currentMax,
            initialMax = initialMax,
            entityUid = Eevee.RegisterIID<IStatResource>()
        };

        public IStat copy() => Create(statId, current, currentMax, initialMax);

        public void Dispose()
        {
            Eevee.DisposeIID<IStatResource>(entityUid);
        }
    }

}
