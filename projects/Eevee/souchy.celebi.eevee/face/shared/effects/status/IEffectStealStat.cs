using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public interface IEffectStealStat : IEffect
    {
        public StatType type { get; set; }
        public IValue<int> Value { get; set; }
    }
}
