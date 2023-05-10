using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.umbreonUnitTests
{
    public class TestEntity : IEntity
    {
        [BsonId]
        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary();

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }
}
