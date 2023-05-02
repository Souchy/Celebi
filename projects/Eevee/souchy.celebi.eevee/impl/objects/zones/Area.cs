using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.impl.objects.zones
{
    public class Area : IArea
    {
        public IZone zone { get; set; }
        public List<ICell> Cells { get; set; } = new();
    }


}
