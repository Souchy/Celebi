using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.move
{
    public interface IEffectTeleportBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }
}
