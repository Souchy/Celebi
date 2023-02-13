using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.impl.objects.zones
{
    public static partial class AreaGenerator
    {
        public class Points : List<IVector3>
        {
            public IZone zone { get; set; }
            public Points(IZone zone) => this.zone = zone;
            public Points rotate()
            {
                return this;
            }
            public Points anchor()
            {
                var unit = this.zone.localOrigin.GetUnitVector();
                int offx = sizeX() / 2 * unit.x;
                int offz = sizeZ() / 2 * unit.z;
                return offset(offx, offz);
            }
            public Points offset()  => offset(zone.worldOffset.x, zone.worldOffset.z);
            public Points offset(int x, int z)
            {
                foreach (var p in this)
                    p.add(x, z);
                return this;
            }
            public Points Add(Points points2)
            {
                foreach (var p in points2)
                    Add(p);
                return this;
            }
            public new List<IVector3> AddRange(IEnumerable<IVector3> points2)
            {
                foreach (var p in points2)
                    Add(p);
                return this;
            }
            public new void Add(IVector3 p)
            {
                if (!this.Any(v => v.equals(p)))
                    base.Add(p);
            }
            public int minX() => this.Min(v => v.x);
            public int minZ() => this.Min(v => v.z);
            public int maxX() => this.Max(v => v.x);
            public int maxZ() => this.Max(v => v.z);
            public int sizeX() => maxX() - minX() + 1;
            public int sizeZ() => maxZ() - minZ() + 1;
            public IBoardArea projectToBoard()
            {
                throw new Exception();
            }
        }

    }
}
