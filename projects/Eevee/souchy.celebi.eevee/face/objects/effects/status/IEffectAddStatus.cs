using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.effects.status
{
    public interface IEffectAddStatus : IEffect
    {

        public IValue<IID> SpellModelId { get; set; }

    }
}
