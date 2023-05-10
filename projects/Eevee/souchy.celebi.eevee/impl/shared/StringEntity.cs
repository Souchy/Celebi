using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace souchy.celebi.eevee.impl.shared
{
    public class StringEntity : IStringEntity
    {
        /// <summary>
        /// mongo id
        /// </summary>
        [BsonId]
        //[BsonElement("_id")]
        public ObjectId entityUid { get; set; }
        /// <summary>
        /// string model id / key
        /// </summary>
        public IID modelUid { get; set; }
        /// <summary>
        /// string value
        /// </summary>
        public string value { get => _value;
            set
            {
                _value = value;
                this.GetEntityBus()?.publish(this); // IEventBus.save, this);
            }
        }
        private string _value;


        private StringEntity() { }
        public static StringEntity Create(string val = "") => new StringEntity()
        {
            entityUid = Eevee.RegisterIIDTemporary(),
            value = val
        };


        public override string ToString() => value;

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }
}
