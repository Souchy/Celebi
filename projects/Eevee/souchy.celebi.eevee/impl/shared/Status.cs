using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.shared
{
    public class Status : IStatus
    {
        public IID modelUid { get; set; }
        public IID fightUid { get; set; }
        public IID entityUid { get; set; }

        public IID sourceSpell { get; set; }
        public IID sourceCreature { get; set; }
        public IID holderEntity { get; set; }

        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }
        public List<IID> effectIds { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
