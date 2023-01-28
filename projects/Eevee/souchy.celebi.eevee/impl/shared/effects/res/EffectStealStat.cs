using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectStealStat : Effect, IEffectStealStat
    {
        public StatType type { get; set; }
        public IValue<int> Value { get; set; }
    }
}
