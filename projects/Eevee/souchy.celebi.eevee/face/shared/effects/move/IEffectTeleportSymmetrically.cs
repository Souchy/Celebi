using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public interface IEffectTeleportSymmetrically : IEffect
    {

        public IValue<IPosition> center { get; set; }

    }
}
