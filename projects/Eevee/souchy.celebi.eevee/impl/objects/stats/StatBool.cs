using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects.stats;

namespace souchy.celebi.eevee.impl.stats
{
    public class StatBool : IStatBool
    {
        //public StatValueType valueType => StatValueType.Bool;

        public bool Value { get; init; }

        public StatBool() { }
        public StatBool(bool value) { Value = value; }

        public IStat copy() => new StatBool(Value);
    }
}
