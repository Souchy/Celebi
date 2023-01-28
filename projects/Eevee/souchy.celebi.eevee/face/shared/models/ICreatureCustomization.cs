using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureCustomization : IEntityModeled, IFightEntity
    {
        public IStats chosenStats { get; init; }
        public List<IID> chosenSpellModels { get; init; }
        public IID chosenSkin { get; init; }

        //public List<IID> baseStatusPassives { get; init; }
    }
}
