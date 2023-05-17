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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.face.shared;

namespace souchy.celebi.eevee.impl.shared
{
    public class StatusModel : IStatusModel
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }

        public ObjectId nameId { get; set; }
        public ObjectId descriptionId { get; set; }
        public IValue<int> delay { get; set; } = new Value<int>();
        public IValue<int> duration { get; set; } = new Value<int>();
        public IValue<bool> canBeUnbewitched { get; set; } = new Value<bool>();
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
