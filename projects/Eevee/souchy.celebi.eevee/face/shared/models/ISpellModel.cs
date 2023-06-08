using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.enums.characteristics.stats;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.face.shared.models
{
    public interface ISpellModel : IEntityModel, IEffectsContainer
    {
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public AssetIID icon { get; set; }
        /// <summary>
        /// Even if skins can be used on almost any spell, 
        /// we have a list here because usually skins are made specifically for 1 spell
        /// (ex: fireball vfx for fireball, void sphere vfx for void sphere....)
        /// and we need a way to edit them starting from the spell
        /// </summary>
        public IEntitySet<ObjectId> skinIds { get; init; }

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public ObjectId statsId { get; set; }

        public IZone RangeZoneMin { get; set; }
        public IZone RangeZoneMax { get; set; }

        public IStringEntity GetName() => Eevee.models.i18n.Get(nameId);
        public IStringEntity GetDescription() => Eevee.models.i18n.Get(descriptionId);
        /// <summary>
        /// Costs are included in the stats for easier modification by statuses
        /// </summary>
        public SpellModelStats GetStats => (SpellModelStats) Eevee.models.stats.Get(statsId);
    }


}