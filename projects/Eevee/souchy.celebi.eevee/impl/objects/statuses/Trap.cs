using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.statuses;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : StatusContainer, ITrap
    {
        public IEntitySet<IID> cellIds { get; set; } = new EntitySet<IID>();

        private Trap(IID id, IID fightId) : base(id, fightId) { }
        public static new ITrap Create(IID fightId) => new Trap(Eevee.RegisterIID<ITrap>(), fightId);

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
