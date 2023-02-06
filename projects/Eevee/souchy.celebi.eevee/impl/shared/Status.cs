using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.values;
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

        public IValue<int> delay { get; set; } = new Value<int>();
        public IValue<int> duration { get; set; } = new Value<int>();
        public IEntityList<IID> effectIds { get; set; } = new EntityList<IID>(); //EntityList<IID>.Create();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
