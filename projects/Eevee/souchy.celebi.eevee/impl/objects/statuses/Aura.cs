using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.objects.statuses
{
    public class Aura : StatusContainer, IAura
    {
        //public IEntitySet<IID> cellIds { get; set; } = new EntitySet<IID>();

        private Aura(IID id, IID fightId) : base(id, fightId) { }
        public static new IAura Create(IID fightId) => new Aura(Eevee.RegisterIID<IAura>(), fightId);

        public new void Dispose()
        {
            base.Dispose();
        }
    }
}
