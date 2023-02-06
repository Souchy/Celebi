using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : Status, IGlyph
    {
        public IEntitySet<IID> cellIds { get; set; } = new EntitySet<IID>();


        private Glyph() { }
        private Glyph(IID id) : base(id) { }
        public static new IGlyph Create() => new Glyph(Eevee.RegisterIID<IGlyph>());

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
