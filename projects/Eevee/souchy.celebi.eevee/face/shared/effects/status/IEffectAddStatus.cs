using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public interface IEffectAddStatus : IEffect
    {

        public IValue<IID> spellModelId { get; set; }

    }
}
