using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.entity
{
    public interface IEntity : IDisposable
    {
        [BsonId]
        [BsonSerializer(typeof(IIDSerializer))]
        public IID entityUid { get; set; }

        //public IEventBus GetEventBus() => Eevee.eventBuses[entityUid];

    }
}