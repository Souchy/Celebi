using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatDetailed : IStatDetailed
    {
        public StatType StatType { get; init; }
        public IID entityUid { get; set; }


        public int baseFlat { get; init; }
        public int increasedPercent { get; init; }
        public int increasedFlat { get; init; }
        public int morePercent { get; init; }

        public int value { 
            get {
                double step1 = 1 + baseFlat * increasedPercent / 100d;
                double step2 = step1 + increasedFlat;
                double step3 = 1 + step2 * morePercent / 100d;
                return (int) step3;
            }
            set { }
        }


        public StatDetailed(StatType st) => this.StatType = st;
        public StatDetailed(StatType st, int baseFlat, int increasedPercent, int increasedFlat, int morePercent) : this(st)
        {
            this.baseFlat = baseFlat;
            this.increasedPercent = increasedPercent;
            this.increasedFlat = increasedFlat;
            this.morePercent = morePercent;
        }

        public IStat copy() => new StatDetailed(StatType, baseFlat, increasedPercent, increasedFlat, morePercent);

        public void Dispose()
        {
            Eevee.DisposeIID(this);
        }
    }
}
