using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.spark.models.aggregations
{
    public record StatusModelAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public AssetIID icon { get; set; } = new();
        public IEntitySet<ObjectId> skinIds { get; init; } = new EntitySet<ObjectId>();

        public StatusModelStats stats { get; set; }
        public IValue<StatusPriorityType> priority { get; set; } = new Value<StatusPriorityType>();
        public IEntityList<EffectPermanentAggregation> effects { get; set; } = new EntityList<EffectPermanentAggregation>();
    }
}
