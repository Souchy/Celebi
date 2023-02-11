using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.effects.res
{
    public interface IEffectOvertimeDamage : IEffect
    {
        public IValue<ElementType> Element { get; set; } 
        public IValue<int> Value { get; set; }
    }
}
