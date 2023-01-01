using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.models
{
    public interface ICreatureModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IStats baseStats { get; init; }
        public List<IID> baseSpells { get; init; }
        public List<IID> baseStatusPassives { get; init; }

        public List<IID> skins { get; init; }
    }
}
