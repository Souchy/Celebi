using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public class EffectHeal : Effect, IEffectHeal
    {
        public IValue<int> Value { get; set; }
    }
}
