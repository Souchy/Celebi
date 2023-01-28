using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public class EffectFly : Effect, IEffectFly
    {
        public IValue<int> height { get; set; }

    }
}
