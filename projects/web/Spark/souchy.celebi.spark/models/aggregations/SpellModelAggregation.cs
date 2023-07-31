using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.spark.models.aggregations
{
    public record SpellModelTextAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IStringEntity nameId { get; set; }
        public IStringEntity descriptionId { get; set; }
    }
    public record SpellModelAggregation : SpellModelTextAggregation
    {
        public AssetIID icon { get; set; } = new();
        public IEntitySet<ISpellSkin> skinIds { get; init; } = new EntitySet<ISpellSkin>();

        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }

        public SpellModelStats statsId { get; set; }
        public IEntityList<IEffect> EffectIds { get; set; } = new EntityList<IEffect>();

        public IZone RangeZoneMin { get; set; }
        public IZone RangeZoneMax { get; set; }
    }
}
