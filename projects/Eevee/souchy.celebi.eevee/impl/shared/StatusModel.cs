using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        public IID entityUid { get; set; }

        //public IStatSimple delay { get; set; }
        //public IStatSimple duration { get; set; }
        public IValue<int> delay { get; set; } = new Value<int>();
        public IValue<int> duration { get; set; } = new Value<int>();
        public List<IID> effectIds { get; set; } = new();

        private StatusModel() { }
        private StatusModel(IID id) => entityUid = id;
        public static IStatusModel Create() => new StatusModel(Eevee.RegisterIID<IStatusModel>());

        public void Dispose()
        {
            Eevee.DisposeIID<IStatusModel>(entityUid);
        }
    }
}
