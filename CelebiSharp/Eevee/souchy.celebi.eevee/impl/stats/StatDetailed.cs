using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;

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
                return (baseFlat * increasedPercent + increasedFlat) * morePercent;
            } 
            set => throw new NotImplementedException(); 
        }
    }
}
