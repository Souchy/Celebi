using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.statuses;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : Status, ITrap
    {
        public List<IID> cellIds { get; set; }

        private Trap() { }
        private Trap(IID id) : base(id) { }
        public static new ITrap Create() => new Trap(Eevee.RegisterIID<ITrap>());

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
