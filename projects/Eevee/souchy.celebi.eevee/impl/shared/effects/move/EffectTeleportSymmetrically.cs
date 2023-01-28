using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public class EffectTeleportSymmetrically : Effect, IEffectTeleportSymmetrically
    {
        public IValue<IPosition> center { get; set; }
    }
}
