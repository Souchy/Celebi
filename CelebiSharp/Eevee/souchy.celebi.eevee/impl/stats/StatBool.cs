using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        public StatValueType type => StatValueType.Bool;

        public bool Value { get; set; }
    }
}
