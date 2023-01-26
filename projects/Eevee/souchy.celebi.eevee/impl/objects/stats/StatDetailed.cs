using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatDetailed : IStatDetailed
    {
        public int baseFlat { get; set; }
        public int increasedPercent { get; set; }
        public int increasedFlat { get; set; }
        public int morePercent { get; set; }

        public StatValueType type => StatValueType.Detailed;

        public int Value { 
            get {
                double step1 = 1 + baseFlat * increasedPercent / 100d;
                double step2 = step1 + increasedFlat;
                double step3 = 1 + step2 * morePercent / 100d;
                return (int) step3;
            } 
            set => throw new NotImplementedException(); 
        }
    }
}
