using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.spark.models.aggregations
{
    //public record SpellModelTextAggregation
    //{
    //    [BsonId]
    //    public ObjectId entityUid { get; set; }
    //    public IID modelUid { get; set; }
    //    public IStringEntity nameId { get; set; }
    //    public IStringEntity descriptionId { get; set; }
    //}

    public record SpellModelAggregation : IEntity //: SpellModelTextAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public AssetIID icon { get; set; } = new();
        public IEnumerable<ISpellSkin> skins { get; init; } = new List<ISpellSkin>();

        public SpellModelStats stats { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public IEnumerable<EffectPermanentAggregation> effects { get; set; } = new List<EffectPermanentAggregation>();
        public IZone rangeZoneMin { get; set; }
        public IZone rangeZoneMax { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
