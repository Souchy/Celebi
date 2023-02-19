using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureModel : IEntity
    {
        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public IEntitySet<IID> skins { get; init; }

        public IID baseStats { get; set; }
        public IEntitySet<IID> baseSpells { get; init; }
        public IEntitySet<IID> baseStatusPassives { get; init; }



        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
        public IStats GetBaseStats() => Eevee.models.stats.Get(baseStats);
        public IEnumerable<ICreatureSkin> GetSkins() => skins.Values.Select(i => Eevee.models.creatureSkins.Get(i));
        public IEnumerable<IStatusModel> GetStatusPassives() => baseStatusPassives.Values.Select(i => Eevee.models.statusModels.Get(i));
        public IEnumerable<ISpellModel> GetSpells() => baseSpells.Values.Select(i => Eevee.models.spellModels.Get(i));
        public ICreatureSkin GetBaseSkin() => Eevee.models.creatureSkins.Get(skins.Values.First());

    }
}
