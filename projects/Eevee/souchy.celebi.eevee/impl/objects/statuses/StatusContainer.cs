using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.statuses
{
    public class StatusContainer : IStatusContainer
    {
        public IID fightUid { get; set; }
        public IID entityUid { get; set; }
        public IID modelUid { get; set; }

        public IID sourceSpellModel { get; set; }
        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }
        public IID stats { get; set; }

        public List<IStatusInstance> instances { get; set; } = new List<IStatusInstance>();

        protected StatusContainer(IID id, IID fightUid)
        {
            this.entityUid = id;
            this.fightUid = fightUid;
            this.stats = Eevee.RegisterIID<IStats>();
        }
        public static IStatusContainer Create(IID fightUid) => new StatusContainer(Eevee.RegisterIID<IStatusContainer>(), fightUid);


        public void Dispose()
        {
            ((IStatusContainer) this).GetStats().Dispose();
            Eevee.DisposeIID<IStatusContainer>(entityUid);
        }
    }
}
