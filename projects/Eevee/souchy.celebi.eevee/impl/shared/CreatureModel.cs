using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson;

namespace souchy.celebi.eevee.impl.shared
{
    public class CreatureModel : ICreatureModel
    {
        [BsonId]
        //[BsonElement("_id")]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public IEntitySet<ObjectId> skins { get; init; } = new EntitySet<ObjectId>();

        public ObjectId baseStats { get; set; }
        public ObjectId growthStats { get; set; }
        public IEntitySet<ObjectId> baseSpells { get; init; } = new EntitySet<ObjectId>();
        public IEntitySet<ObjectId> baseStatusPassives { get; init; } = new EntitySet<ObjectId>();

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
