using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util.math;
using static souchy.celebi.eevee.impl.objects.zones.AreaGenerator;

namespace souchy.celebi.eevee.impl.objects.zones
{
    public static partial class AreaGenerator
    {
        public class Points : List<IVector3>
        {
            public IZone zone { get; set; }
            public Points(IZone zone) => this.zone = zone;
            /// <summary>
            /// rotate around the anchor
            /// </summary>
            /// <returns></returns>
            public Points rotate()
            {
                //var x2 = sizeX() / 2;
                //var z2 = sizeZ() / 2;
                //Math.Sin(45);
                var unit = this.zone.rotation.GetUnitVector().z;
                //foreach(var p in this)
                //{
                //    var add = p.copy().scale((int) unit.x, (int) unit.z);
                //    p.add(add);
                //}
                var angle = unit * Math.PI / 180;
                foreach (var p in this)
                {
                    var xb = Math.Round(p.x * Math.Cos(angle) - p.z * Math.Sin(angle));
                    var zb = Math.Round(p.z * Math.Cos(angle) + p.x * Math.Sin(angle));
                    p.set((int) xb, (int) zb);
                }
                /*
                foreach(var p in this)
                    switch(this.zone.rotation)
                    {
                        case Rotation4Type.top:
                            break;
                        case Rotation4Type.bottom:
                            p.scale(1, -1);
                            break;
                        case Rotation4Type.right:
                            var tempx = p.x;
                            p.x = p.z;
                            p.z = tempx;
                            break;
                        case Rotation4Type.left:
                            var tempx2 = p.x;
                            p.x = p.z;
                            p.z = tempx2;
                            break;
                    }
                */
                return this;
            }
            public Points anchor()
            {
                switch (zone.zoneType.value)
                {
                    case ZoneType.point:
                        return this;
                    case ZoneType.line:
                    case ZoneType.diagonal:
                        return Anchoring.anchorLine(this);
                    case ZoneType.crossHalf:
                    case ZoneType.circleHalf:
                    case ZoneType.squareHalf:
                    case ZoneType.ellipseHalf:
                        return Anchoring.anchorFormHalf(this);
                    case ZoneType.cross:
                    case ZoneType.xcross:
                    case ZoneType.star:
                    case ZoneType.circle:
                    case ZoneType.square:
                    case ZoneType.rectangle:
                    case ZoneType.ellipse:
                        return Anchoring.anchorForm(this);
                        //default:
                        //    throw new Exception("Invalid zonetype: " + zone.zoneType.value);
                }
                return this;
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
            public IArea projectToBoard()
            {
                throw new Exception();
            }
            public override string ToString()
            {
                return "[" + string.Join(", ", this) + "]";
            }
        }


        public static class Anchoring
        {
            public static Points anchorLine(Points points)
            {
                var origin = (Direction3TypeLine) (int) points.zone.localOrigin;
                var unit = origin.GetUnitVector();
                int offx = (int) Math.Floor((points.sizeX() - 1) * unit.x);
                int offz = (int) Math.Floor((points.sizeZ() - 1) * unit.z);
                return points.offset(offx, offz);
            }
            public static Points anchorForm(Points points)
            {
                var unit = points.zone.localOrigin.GetUnitVector();
                int offx = (int) (Math.Floor(points.sizeX() / 2f) * unit.x);
                int offz = (int) (Math.Floor(points.sizeZ() / 2f) * unit.z);
                return points.offset(offx, offz);
            }
            public static Points anchorFormHalf(Points points)
            {
                var unit = points.zone.localOrigin.GetUnitVector();
                //var origin2 = (Direction3TypeLine) points.zone.localOrigin;
                //var unit2 = points.zone.localOrigin.GetUnitVector();

                int offx = (int) (Math.Floor(points.sizeX() / 2f) * unit.x);
                int offz = (int) Math.Floor((points.sizeZ() - 1) * unit.z);
                return points.offset(offx, offz);
            }
        }

    }
}
