using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }

        public IID baseStats { get; set; }
        public HashSet<IID> baseSpells { get; init; }
        public HashSet<IID> baseStatusPassives { get; init; }

        public HashSet<IID> skins { get; init; }


        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
        public IStats GetBaseStats() => Eevee.models.stats.Get(baseStats);

    }
}
