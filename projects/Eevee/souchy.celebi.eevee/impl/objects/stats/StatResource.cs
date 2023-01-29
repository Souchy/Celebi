using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.values;
using System.Reflection.Metadata.Ecma335;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatResource : IStatResource
    {
        //public StatValueType valueType => StatValueType.Resource;

        public int current { get; init; }
        public int currentMax { get; init; }
        public int initialMax { get; init; }

        public (int current, int currentMax, int initialMax) value { 
            get => (current, currentMax, initialMax);
            init
            {
                this.current = value.current;
                this.currentMax = value.currentMax;
                this.initialMax = value.initialMax;
            }
        }

        public StatResource() { }
        public StatResource(int current, int currentMax, int initialMax)
        {
            this.current = current;
            this.currentMax = currentMax;
            this.initialMax = initialMax;
        }

        public IStat copy() => new StatResource(current, currentMax, initialMax);

    }

}
