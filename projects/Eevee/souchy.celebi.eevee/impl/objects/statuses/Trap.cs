using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.statuses;

namespace souchy.celebi.eevee.interfaces.statuses
{
    public class Trap : StatusContainer, ITrap
    {
        public IEntitySet<ObjectId> cellIds { get; set; } = new EntitySet<ObjectId>();

        private Trap(ObjectId id, ObjectId fightId) : base(id, fightId) { }
        public static new ITrap Create(ObjectId fightId) => new Trap(Eevee.RegisterIIDTemporary(), fightId);

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
