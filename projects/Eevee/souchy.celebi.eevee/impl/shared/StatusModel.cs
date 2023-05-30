using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public AssetIID icon { get; set; } = new();
        public IEntitySet<ObjectId> skinIds { get; init; } = new EntitySet<ObjectId>();

        public ObjectId statsId { get; set; }
        public IValue<StatusPriorityType> priority { get; set; } = new Value<StatusPriorityType>();

        public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();


        private StatusModel() { }
        private StatusModel(ObjectId id) => entityUid = id;
        public static IStatusModel CreatePermanent() => new StatusModel(Eevee.RegisterIIDTemporary());


        public IEnumerable<IEffect> GetEffects() => EffectIds.Values.Select(i => Eevee.models.effects.Get(i));

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
        }

    }
}
