using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects.status
{
    public interface IEffectAddStatus : IEffect
    {

        public IValue<IID> spellModelId { get; set; }

    }
}
