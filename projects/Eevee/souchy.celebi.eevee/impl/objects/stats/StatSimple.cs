using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        public StatValueType type => StatValueType.Simple;

        public int Value { get; set; }
    }

}
