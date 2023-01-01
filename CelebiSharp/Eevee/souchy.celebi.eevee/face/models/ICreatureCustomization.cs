using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.models
{
    public interface ICreatureCustomization : IEntityModeled
    {
        public IStats chosenStats { get; init; }
        public List<IID> chosenSpellModels { get; init; }
        public IID chosenSkin { get; init; }

        //public List<IID> baseStatusPassives { get; init; }
    }
}
