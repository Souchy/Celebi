using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatDetailed : IStatDetailed
    {
        public int baseFlat { get; init; }
        public int increasedPercent { get; init; }
        public int increasedFlat { get; init; }
        public int morePercent { get; init; }

        public int Value { 
            get {
                double step1 = 1 + baseFlat * increasedPercent / 100d;
                double step2 = step1 + increasedFlat;
                double step3 = 1 + step2 * morePercent / 100d;
                return (int) step3;
            }
            init { }
        }

        public StatDetailed() { }
        public StatDetailed(int baseFlat, int increasedPercent, int increasedFlat, int morePercent)
        {
            this.baseFlat = baseFlat;
            this.increasedPercent = increasedPercent;
            this.increasedFlat = increasedFlat;
            this.morePercent = morePercent;
        }

        public IStat copy() => new StatDetailed(baseFlat, increasedPercent, increasedFlat, morePercent);
    }
}
