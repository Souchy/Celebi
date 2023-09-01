using souchy.celebi.eevee.face.shared.models;

namespace souchy.celebi.spark.models.aggregations
{
    public record NameDescriptionAggregation
    {
        public IStringEntity nameId { get; set; }
        public IStringEntity descriptionId { get; set; }
    }
}
