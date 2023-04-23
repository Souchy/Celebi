﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.effectResults;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.statuses
{
    public class StatusInstance : IStatusInstance
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }

        //public IValue<int> delay { get; set; } = new Value<int>();
        //public IValue<int> duration { get; set; } = new Value<int>();
        public IID stats { get; set; }

        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>();

        protected StatusInstance() { }
        protected StatusInstance(IID id, IID fightUid)
        {
            this.entityUid = id;
            this.fightUid = fightUid;
            this.stats = Eevee.RegisterIID<IStats>();
        }
        public static IStatusInstance Create(IID fightUid) => new StatusInstance(Eevee.RegisterIID<IStatusInstance>(), fightUid);


        public IEnumerable<IEffect> GetEffects() => effectIds.Values.Select(i => this.GetFight().effects.Get(i));

        public void Dispose()
        {
            ((IStatusInstance) this).GetStats().Dispose();
            Eevee.DisposeIID<IStatusInstance>(entityUid);
            throw new NotImplementedException();
        }
    }
}