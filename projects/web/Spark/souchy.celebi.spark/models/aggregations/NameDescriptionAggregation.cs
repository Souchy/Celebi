using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.spark.models.aggregations
{
    /// <summary>
    /// Useful for lists of entity models previews. Like a list of creatures, spells, statuses.
    /// </summary>
    public record TextEntityAggregation
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public IStringEntity nameId { get; set; }
        public IStringEntity descriptionId { get; set; }
    }
}
