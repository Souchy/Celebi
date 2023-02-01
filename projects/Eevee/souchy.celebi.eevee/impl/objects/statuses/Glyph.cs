using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : Status, IGlyph
    {
        public List<IID> cellIds { get; set; }


        private Glyph() { }
        public static new IGlyph Create() => new Glyph()
        {
            entityUid = Eevee.RegisterIID<IGlyph>()
        };

        public new void Dispose()
        {
            base.Dispose();
        }

    }
}
