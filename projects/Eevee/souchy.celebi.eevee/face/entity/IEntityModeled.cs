using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util.serialization;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntityModeled : IEntity
    {
        [BsonSerializer(typeof(IIDBsonSerializer<IID>))]
        public IID modelUid { get; set; }
    }
}
