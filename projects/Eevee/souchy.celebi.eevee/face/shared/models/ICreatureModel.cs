using souchy.celebi.eevee.enums.characteristics.other;
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
        public IEntitySet<ObjectId> skinIds { get; init; }

        public ObjectId statsId { get; set; }
        public IEntitySet<ObjectId> spellIds { get; init; }
        public IEntitySet<ObjectId> statusPassiveIds { get; init; }


        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId); //GetBaseSkin().GetName(); // 
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId); //GetBaseSkin().GetName(); //
        public CreatureStats GetBaseStats() => (CreatureStats) Eevee.models.stats.Get(statsId);
        public IEnumerable<ICreatureSkin> GetSkins() => skinIds.Values.Select(i => Eevee.models.creatureSkins.Get(i));
        public IEnumerable<IStatusModel> GetStatusPassives() => statusPassiveIds.Values.Select(i => Eevee.models.statusModels.Get(i));
        public IEnumerable<ISpellModel> GetSpells() => spellIds.Values.Select(i => Eevee.models.spellModels.Get(i));
        public ICreatureSkin GetBaseSkin() => Eevee.models.creatureSkins.Get(skinIds.Values.First());

    }
}
