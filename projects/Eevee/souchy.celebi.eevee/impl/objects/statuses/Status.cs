using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using static souchy.celebi.eevee.face.entity.IEntity;

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

        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }

        public List<IID> effectIds { get; set; }


        public static IStatus Create() => new Status()
        {
            entityUid = Eevee.RegisterIID<IStatus>()
        };


        public void Dispose()
        {
            Eevee.DisposeIID<IStatus>(entityUid);
            throw new NotImplementedException();
        }

    }
}