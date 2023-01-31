using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl;
using static souchy.celebi.eevee.face.entity.IEntity;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : Status, IGlyph
    {
        public List<IID> cellIds { get; set; }


        //public Glyph() { }
        //private Glyph(IID id) => entityUid = id;
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
