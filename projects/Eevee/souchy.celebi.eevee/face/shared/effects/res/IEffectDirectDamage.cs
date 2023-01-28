using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public interface IEffectDirectDamage : IEffect
    {

        public IValue<ElementType> element { get; set; }
        public IValue<int> Value { get; set; }

    }
}
