using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.status
{
    public class EffectAddStatus : Effect, IEffectAddStatus
    {

        public IValue<IID> spellModelId { get; set; }

    }
}
