using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : StatusContainer, IGlyph
    {
        public IEntitySet<ObjectId> cellIds { get; set; } = new EntitySet<ObjectId>();

        private Glyph(ObjectId id, ObjectId fightId) : base(id, fightId) { }
        public static new IGlyph Create(ObjectId fightId) => new Glyph(Eevee.RegisterIIDTemporary(), fightId);

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
