using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.statuses
{
    public class StatusInstance : IStatusInstance
    {
        [BsonId]
        public ObjectId entityUid { get; set; }
        public IID modelUid { get; set; }
        public ObjectId fightUid { get; set; }

        //public IValue<int> delay { get; set; } = new Value<int>();
        //public IValue<int> duration { get; set; } = new Value<int>();
        public ObjectId statsId { get; set; }

        public IEntityList<ObjectId> EffectIds { get; set; } = new EntityList<ObjectId>();

        protected StatusInstance() { }
        protected StatusInstance(ObjectId id, ObjectId fightUid)
        {
            this.entityUid = id;
            this.fightUid = fightUid;
            //this.statsId = Eevee.RegisterIIDTemporary();
        }
        public static IStatusInstance Create(ObjectId fightUid) => new StatusInstance(Eevee.RegisterIIDTemporary(), fightUid);


        public IEnumerable<IEffect> GetEffects() => EffectIds.Values.Select(i => this.GetFight().effects.Get(i));

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            ((IStatusInstance) this).GetStats().Dispose();
            throw new NotImplementedException();
        }
    }
}