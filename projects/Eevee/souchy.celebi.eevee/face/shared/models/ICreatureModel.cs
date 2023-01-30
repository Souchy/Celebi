using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureModel : IEntity
    {
        public static IUIdGenerator uIdGenerator = new UIdGenerator();

        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IID baseStats { get; init; }
        public HashSet<IID> baseSpells { get; init; }
        public HashSet<IID> baseStatusPassives { get; init; }

        public HashSet<IID> skins { get; init; }


        public IStats GetBaseStats() => Eevee.models.stats.Get(baseStats);

    }
}
