using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.spark.models.aggregations
{
    public record ICreatureModelView : IEntity
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public CreatureStats stats { get; set; }
        public IEnumerable<ISpellModelView> spells { get; init; } = new List<ISpellModelView>();
        public IEnumerable<IStatusModel> statusPassives { get; init; } = new List<IStatusModel>();
        public IEnumerable<ICreatureSkin> skins { get; init; } = new List<ICreatureSkin>();

        public void Dispose()
        {
        }
    }
}
