using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.effects.move
{
    public interface IEffectTranslateTo : IEffect
    {
        public IValue<IPosition> Position { get; set; }

    }
}
