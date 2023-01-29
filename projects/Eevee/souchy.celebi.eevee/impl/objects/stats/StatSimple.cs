using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatSimple : IStatSimple
    {
        //public StatValueType valueType => StatValueType.Simple;

        public int value { get; init; }

        public StatSimple() { }
        public StatSimple(int value) { this.value = value; }

        public IStat copy() => new StatSimple(value);

    }
}
