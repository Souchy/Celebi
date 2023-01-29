using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl;
using static souchy.celebi.eevee.face.entity.IEntity;

namespace souchy.celebi.eevee.statuses
{
    public class Glyph : Status, IGlyph
    {
        public List<IID> cellIds { get; set; }


        public Glyph() { }
        private Glyph(IID id) => entityUid = id;
        public static IGlyph Create() => new Glyph(Eevee.RegisterIID());

        public new void Dispose()
        {
            base.Dispose();
            throw new NotImplementedException();
        }

    }
}
