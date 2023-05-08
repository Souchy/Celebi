using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }

        public IID nameId { get; set; }
        public IID descriptionId { get; set; }
        public IValue<int> delay { get; set; } = new Value<int>();
        public IValue<int> duration { get; set; } = new Value<int>();
        public IValue<bool> canBeUnbewitched { get; set; } = new Value<bool>();
        public IValue<StatusPriorityType> priority { get; set; } = new Value<StatusPriorityType>();

        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>();


        private StatusModel() { }
        private StatusModel(IID id) => entityUid = id;
        public static IStatusModel CreatePermanent() => new StatusModel(Eevee.RegisterIID<IStatusModel>());


        public IEnumerable<IEffect> GetEffects() => effectIds.Values.Select(i => Eevee.models.effects.Get(i));

        public void Dispose()
        {
            Eevee.DisposeIID<IStatusModel>(entityUid);
        }

    }
}
