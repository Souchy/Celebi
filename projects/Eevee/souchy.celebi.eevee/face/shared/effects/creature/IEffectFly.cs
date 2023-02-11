using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public interface IEffectFly : IEffect
    {
        public IValue<int> Height { get; set; }

    }
}
