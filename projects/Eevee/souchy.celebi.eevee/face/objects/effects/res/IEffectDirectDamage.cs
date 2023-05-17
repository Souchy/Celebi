using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.effects.res
{
    public interface IEffectDirectDamage : IEffect
    {

        public IValue<ElementType> Element { get; set; }
        public IValue<int> Value { get; set; }

    }
}
