using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.creature
{
    public class EffectDig : Effect, IEffectDig
    {
        public IValue<int> depth { get; set; }
    }
}
