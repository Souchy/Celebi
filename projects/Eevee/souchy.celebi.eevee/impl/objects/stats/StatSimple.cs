using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        public StatValueType type => StatValueType.Simple;

        public int Value { get; set; }
    }

}
