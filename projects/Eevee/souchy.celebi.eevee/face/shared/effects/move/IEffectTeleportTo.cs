using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public interface IEffectTeleportTo : IEffect
    {

        public IValue<IPosition> position { get; set; }

    }
}
