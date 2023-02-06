using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        public IID entityUid { get; set; }

        public IValue<int> delay { get; set; } = new Value<int>();
        public IValue<int> duration { get; set; } = new Value<int>();

        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>();


        private StatusModel() { }
        private StatusModel(IID id) => entityUid = id;
        public static IStatusModel Create() => new StatusModel(Eevee.RegisterIID<IStatusModel>());


        public IEnumerable<IEffect> GetEffects() => effectIds.Values.Select(i => Eevee.models.effects.Get(i));
        public void Dispose()
        {
            Eevee.DisposeIID<IStatusModel>(entityUid);
        }

    }
}
