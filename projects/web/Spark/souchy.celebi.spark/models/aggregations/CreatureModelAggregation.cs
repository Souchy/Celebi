using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.spark.models.aggregations
{
    public record CreatureModelTextAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IStringEntity nameId { get; set; }
        public IStringEntity descriptionId { get; set; }
    }

    public record CreatureModelAggregation : CreatureModelTextAggregation
    {
        public CreatureStats statsId { get; set; }
        public IEntitySet<ISpellModel> spellIds { get; init; } = new EntitySet<ISpellModel>();
        public IEntitySet<IStatusModel> statusPassiveIds { get; init; } = new EntitySet<IStatusModel>();
        public IEntitySet<ICreatureSkin> skinIds { get; init; } = new EntitySet<ICreatureSkin>();
    }
}
