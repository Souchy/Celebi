using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.creature
{
    public interface IEffectDig : IEffect
    {
        public IValue<int> depth { get; set; }
    }
}
