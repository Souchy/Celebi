using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        public IID entityUid { get; set; }
        public StatType StatType { get; init; }

        public int value { get; set; }

        public StatSimple(StatType st) => this.StatType = st;
        public StatSimple(StatType st, int value) : this(st) => this.value = value; 

        public IStat copy() => new StatSimple(StatType, value);

        public void Dispose()
        {
            Eevee.DisposeIID(this);
            throw new NotImplementedException();
        }
    }
}
