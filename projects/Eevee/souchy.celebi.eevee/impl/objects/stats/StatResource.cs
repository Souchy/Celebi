using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using System.Reflection.Metadata.Ecma335;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatResource : IStatResource
    {
        public IID entityUid { get; set; }
        public StatType StatType { get; init; }

        public int current { get; set; } // init
        public int currentMax { get; set; } // init
        public int initialMax { get; set; } // init

        public (int current, int currentMax, int initialMax) value { 
            get => (current, currentMax, initialMax);
            set //init
            {
                this.current = value.current;
                this.currentMax = value.currentMax;
                this.initialMax = value.initialMax;
            }
        }


        public StatResource(StatType st) => this.StatType = st;
        public StatResource(StatType st, int current, int currentMax, int initialMax) : this(st)
        {
            this.current = current;
            this.currentMax = currentMax;
            this.initialMax = initialMax;
        }

        public IStat copy() => new StatResource(StatType, current, currentMax, initialMax);

        public void Dispose()
        {
            Eevee.DisposeIID(this);
            throw new NotImplementedException();
        }
    }

}
