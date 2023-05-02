using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects.statuses;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : StatusContainer, IGlyph
    {
        public IEntitySet<IID> cellIds { get; set; } = new EntitySet<IID>();

        private Glyph(IID id, IID fightId) : base(id, fightId) { }
        public static new IGlyph Create(IID fightId) => new Glyph(Eevee.RegisterIID<IGlyph>(), fightId);

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
