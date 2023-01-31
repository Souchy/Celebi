using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.shared
{
    public class StringEntity : IStringEntity
    {
        public IID entityUid { get; set; }

        private string _value;
        public string value { get => _value;
            set
            {
                _value = value;
                this.GetEntityBus().publish("", value);
            }
        }

        private StringEntity() { }
        public static StringEntity Create() => new StringEntity()
        {
            entityUid = Eevee.RegisterIID<IStringEntity>()
        };

        public void Dispose()
        {
            Eevee.DisposeIID<IStringEntity>(entityUid);
        }

        public override string ToString() => value;

    }
}
