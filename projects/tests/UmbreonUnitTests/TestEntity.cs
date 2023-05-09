using MongoDB.Bson;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.umbreonUnitTests
{
    public class TestEntity : IEntity
    {
        public ObjectId entityUid { get; set; } = Eevee.RegisterIIDTemporary();

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }
    }
}
