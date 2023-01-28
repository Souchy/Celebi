using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public interface IEffectHeal : IEffect
    {
        public IValue<int> Value { get; set; }
    }
}
