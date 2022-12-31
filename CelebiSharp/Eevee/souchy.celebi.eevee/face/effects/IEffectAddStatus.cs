using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.effects
{
    public interface IEffectAddStatus : IEffect
    {

        public IValue<IID> spellModelId { get; set; }

    }
}
