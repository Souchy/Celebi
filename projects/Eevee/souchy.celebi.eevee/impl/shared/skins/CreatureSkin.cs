using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace souchy.celebi.eevee.impl.shared.skins
{
    public class CreatureSkin : ICreatureSkin
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }

        public string meshModel { get; set; }
        public string meshName { get; set; }
        public string icon { get; set; }
        public AnimationsData animations { get; set; }
        public Dictionary<ObjectId, ObjectId> spellSkins { get; set; } = new Dictionary<ObjectId, ObjectId>();
        public Dictionary<ObjectId, ObjectId> effectSkins { get; set; } = new Dictionary<ObjectId, ObjectId>(); // FIXME This should go inside ISpellSkin


        private CreatureSkin() { }
        public static ICreatureSkin Create() => new CreatureSkin()
        {
            entityUid = Eevee.RegisterIIDTemporary(),
        };

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }
}
