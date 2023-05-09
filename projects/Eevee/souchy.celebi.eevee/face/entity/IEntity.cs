using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.entity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity : IDisposable
    {
        //public ObjectId _id { get; set; }
        [BsonId]
        [BsonSerializer(typeof(IIDSerializer))]
        public ObjectId entityUid { get; set; }

        //public IEventBus GetEventBus() => Eevee.eventBuses[entityUid];

    }
}
