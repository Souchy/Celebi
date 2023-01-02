using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.res
{
    public interface IEffectStealStat : IEffect
    {
        public StatType type { get; set; }
        public IValue<int> Value { get; set; }
    }
}
