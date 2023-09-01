using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.spark.models.aggregations
{
    public record EffectPermanentAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IEffectModel model { get; set; }

        public IEffectSchema schema { get; set; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public IZone targetAcquisitionZone { get; set; }
        public IEntityList<ITriggerModel> triggers { get; set; } = new EntityList<ITriggerModel>();
        public IEntityList<EffectPermanentAggregation> effects { get; set; } = new EntityList<EffectPermanentAggregation>();

    }
}
