using Newtonsoft.Json;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.shared
{
    public class StringEntity : IStringEntity
    {
        [JsonIgnore]
        public IID entityUid { get; set; }

        private string _value;
        public string value { get => _value;
            set
            {
                _value = value;
                this.GetEntityBus()?.publish("", this);
            }
        }

        private StringEntity() { }
        public static StringEntity Create(string val = "") => new StringEntity()
        {
            entityUid = Eevee.RegisterIID<IStringEntity>(),
            value = val
        };


        public override string ToString() => value;

        public void Dispose()
        {
            Eevee.DisposeIID<IStringEntity>(entityUid);
        }

    }
}
