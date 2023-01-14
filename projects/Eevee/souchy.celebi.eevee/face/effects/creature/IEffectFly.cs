using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.creature
{
    public interface IEffectFly : IEffect
    {
        public IValue<int> height { get; set; }

    }
}
