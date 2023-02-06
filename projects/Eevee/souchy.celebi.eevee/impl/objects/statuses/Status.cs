using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;

namespace souchy.celebi.eevee.statuses
{
    public class Status : IStatus
    {
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }

        public IID sourceSpell { get; set; }
        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }

        public IValue<int> delay { get; set; } = new Value<int>(); // IStatSimple
        public IValue<int> duration { get; set; } = new Value<int>();

        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>();


        protected Status() { }
        protected Status(IID id) => entityUid = id;
        public static IStatus Create() => new Status(Eevee.RegisterIID<IStatus>());


        public void Dispose()
        {
            Eevee.DisposeIID<IStatus>(entityUid);
            throw new NotImplementedException();
        }

    }
}