using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson;
using souchy.celebi.eevee.enums.characteristics.creature;

namespace souchy.celebi.eevee.impl.shared
{
    public class CreatureModel : ICreatureModel
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public IEntitySet<ObjectId> skinIds { get; init; } = new EntitySet<ObjectId>();

        public ObjectId statsId { get; set; }
        public IEntitySet<ObjectId> spellIds { get; init; } = new EntitySet<ObjectId>();
        public IEntitySet<ObjectId> statusPassiveIds { get; init; } = new EntitySet<ObjectId>();
        public List<ResourceEnum> resourceBars { get; init; } = new(3);

        private CreatureModel() { }
        public static ICreatureModel CreatePermanent() => new CreatureModel() 
        {
            entityUid = Eevee.RegisterIIDTemporary(),
            //modelUid = Eevee.RegisterIIDPermanent<ICreatureModel>(),
        };


        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }
}
