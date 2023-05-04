using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntityModeled : IEntity
    {
        [BsonSerializer(typeof(IIDSerializer))]
        public IID modelUid { get; set; }
    }
}
