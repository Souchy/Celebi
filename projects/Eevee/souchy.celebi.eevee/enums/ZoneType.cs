using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects.zones;
using System.Reflection;
using static souchy.celebi.eevee.impl.objects.zones.AreaGenerator;

namespace souchy.celebi.eevee.enums
{



    public enum ZoneType
    {
        [ZoneSize<ZoneSizePoint>] point,
        //multi, // multi zones in one

        [ZoneSize<ZoneSizeLength>] line,
        [ZoneSize<ZoneSizeLength>] diagonal, // diag = line rotated 45°. base is aligned with character, can use 90° rotation to make it perpendicular diagonal

        [ZoneSize<ZoneSizeRadius2>] cross, // '+' made of 2 lines orthogonal
        [ZoneSize<ZoneSizeRadius2>] xcross, // 'x' made of 2 diagonals
        [ZoneSize<ZoneSizeRadius2>] star, // both crosses (=8 directions)

        [ZoneSize<ZoneSizeRadius>] circle,      // like a diamond
        [ZoneSize<ZoneSizeRadius>] square,      // orthogonal square
        [ZoneSize<ZoneSizeRadius>] circleHalf,  // cone, like a triangle
        [ZoneSize<ZoneSizeRadius2>] rectangle,
        [ZoneSize<ZoneSizeRadius2>] ellipse,     // ellipse : radius, radius
        [ZoneSize<ZoneSizeRadius2>] ellipseHalf,

        // adds a parameter for ring width
        [ZoneSize<ZoneSizeRadiusRing>] circleRing,      // 
        [ZoneSize<ZoneSizeRadiusRing>] squareRing,      // like 1 perpendicular line and 2 sides
        [ZoneSize<ZoneSizeRadiusRing>] circleHalfRing,  // like a V
        [ZoneSize<ZoneSizeRadius2Ring>] rectangleRing,  // 
        [ZoneSize<ZoneSizeRadius2Ring>] rectangleHalfRing,  // 
        [ZoneSize<ZoneSizeRadius2Ring>] ellipseRing,    // take 2 radius from ellipse and 3rd parameter is ring width
        [ZoneSize<ZoneSizeRadius2Ring>] ellipseHalfRing,// aka boomerang
    }

    public abstract class IZoneSizeAttribute : Attribute
    {
        public abstract Type type();
        public IZoneSize create(IZone zone)
        {
            var t = (IZoneSize) Activator.CreateInstance(type());
            t.sizeParams = zone.size.value;
            return t;
        }
    }
    public class ZoneSizeAttribute<T> : IZoneSizeAttribute where T : IZoneSize
    {
        public override Type type() => typeof(T); 
        //public Func<IZone, Points> action { get; set; }
        //public ZoneSizeAttribute(Func<IZone, Points> action) {
        //    //this.action = action;
        //}
        public T createT(IZone zone) {
            var t = (T) Activator.CreateInstance(typeof(T));
            t.sizeParams = zone.size.value;
            return t;
        }
    }
    public static class ZoneTypeExtentions
    {
        public static T GetSize<T>(this IZone zone) where T : IZoneSize
        {
            var attr = (ZoneSizeAttribute<T>) typeof(ZoneType)
                    .GetField(Enum.GetName(zone.zoneType.value))
                    .GetCustomAttribute(typeof(ZoneSizeAttribute<T>), true);
            return attr.createT(zone);
        }
        public static IZoneSize GetSize(this IZone zone)
        {
            var attr = (IZoneSizeAttribute) typeof(ZoneType)
                    .GetField(Enum.GetName(zone.zoneType.value))
                    .GetCustomAttribute(typeof(IZoneSizeAttribute), true);
            return attr.create(zone);
        }
        public static Points GeneratePoints(this IZone zone)
        {
            var points = (Points) typeof(AreaGenerator)
                .GetMethod(Enum.GetName(zone.zoneType.value))
                .Invoke(null, new object[] { zone });
            return points;
        }
    }

    public abstract class IZoneSize
    {
        public IVector3 sizeParams; // { get; set; }
    }
    public class ZoneSizePoint : IZoneSize
    {
        //public IVector3 sizeParams { get; set; }
    }
    // line, diagonal
    public class ZoneSizeLength : IZoneSize
    {
        //public IVector3 sizeParams { get; set; }
        public int length { get => sizeParams.x; set => sizeParams.x = value; }
    }
    // circle O, square [], halfcircle /_\, v  /\
    public class ZoneSizeRadius : IZoneSize
    {
        //public IVector3 sizeParams { get; set; }
        public int radius { get => sizeParams.x; set => sizeParams.x = value; }
    }
    // cross, xcross, star, rectangle, ellipse, ellipseHalf
    public class ZoneSizeRadius2 : IZoneSize
    {
        //public IVector3 sizeParams { get; set; }
        public int radiusForward { get => sizeParams.x; set => sizeParams.x = value; }
        public int radiusSide { get => sizeParams.z; set => sizeParams.z = value; }
    }
    // circleRing /\, squareRing :_:, circleHalfRing
    public class ZoneSizeRadiusRing : IZoneSize
    {
        //public IVector3 sizeParams { get; set; } 
        public int radius { get => sizeParams.x; set => sizeParams.x = value; }
        public int ringWidth { get => sizeParams.z; set => sizeParams.z = value; }
    }
    // rectangleRing :_:, ellipseRing, ellipseHalfRing .-.
    public class ZoneSizeRadius2Ring : IZoneSize
    {
        //public IVector3 sizeParams { get; set; }
        public int radiusForward { get => sizeParams.x; set => sizeParams.x = value; }
        public int radiusSide { get => sizeParams.z; set => sizeParams.z = value; }
        public int ringWidth { get => sizeParams.y; set => sizeParams.y = value; }
    }



}