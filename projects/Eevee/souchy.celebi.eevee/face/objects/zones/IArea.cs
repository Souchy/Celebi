using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.shared.zones
{
    public interface IArea
    {
        public IZone zone { get; set; }
        public List<ICell> Cells { get; set; }

    }
}
