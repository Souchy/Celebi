using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ICreatureModel : IEntityModel
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public IEntitySet<ObjectId> skins { get; init; }

        public ObjectId baseStats { get; set; }
        public ObjectId growthStats { get; set; }
        public IEntitySet<IID> baseSpells { get; init; }
        public IEntitySet<IID> baseStatusPassives { get; init; }



        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId); //GetBaseSkin().GetName(); // 
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId); //GetBaseSkin().GetName(); //
        public IStats GetBaseStats() => Eevee.models.stats.Get(baseStats);
        public IStats GetGrowthStats() => Eevee.models.stats.Get(growthStats);
        public IEnumerable<ICreatureSkin> GetSkins() => skins.Values.Select(i => Eevee.models.creatureSkins.Get(i));
        public IEnumerable<IStatusModel> GetStatusPassives() => baseStatusPassives.Values.Select(i => Eevee.models.statusModels.Get(i));
        public IEnumerable<ISpellModel> GetSpells() => baseSpells.Values.Select(i => Eevee.models.spellModels.Get(i));
        public ICreatureSkin GetBaseSkin() => Eevee.models.creatureSkins.Get(skins.Values.First());

    }
}
