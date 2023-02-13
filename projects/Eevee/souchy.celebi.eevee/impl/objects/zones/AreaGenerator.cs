﻿using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.impl.objects.zones
{
    public static partial class AreaGenerator
    {
        private static void example()
        {
            IZone zone = new Zone();
            var points = line(zone)
                            .anchor()
                            .rotate()
                            .offset();
            Console.WriteLine("points: " + points);
        }

        public static Points point(IZone zone)
        {
            return new(zone)
            {
                new Position()
            };
        }

        public static Points line(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeLength>();
            for (int i = 0; i < size.length; i++)
                points.Add(new Position(0, i));
            return points;
        }
        public static Points diagonal(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeLength>();
            for (int i = 0; i < size.length; i++)
                points.Add(new Position(i, i));
            return points;
        }

        public static Points cross(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2>();
            for (int i = -size.radiusForward; i <= size.radiusForward; i++)
                points.Add(new Position(0, i));
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                points.Add(new Position(i, 0));
            return points;
        }
        public static Points xcross(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2>();
            for (int i = -size.radiusForward; i <= size.radiusForward; i++)
                points.Add(new Position(i, i));
            for (int j = -size.radiusSide; j <= size.radiusSide; j++)
                points.Add(new Position(j, -j));
            return points;
        }

        public static Points star(IZone zone)
        {
            var points = cross(zone).Add(xcross(zone));
            return points;
        }

        public static Points circle(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = -size.radius; j <= size.radius; j++)
                    if (Math.Abs(i) + Math.Abs(j) <= size.radius)
                        points.Add(new Position(i, j));
            return points;
        }
        public static Points square(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = -size.radius; j <= size.radius; j++)
                    points.Add(new Position(i, j));
            return points;
        }
        public static Points rectangle(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2>();
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                for (int j = -size.radiusForward; j <= size.radiusForward; j++)
                    points.Add(new Position(i, j));
            return points;
        }

        public static Points circleRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadiusRing>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = -size.radius; j <= size.radius; j++)
                {
                    var r = Math.Abs(i) + Math.Abs(j);
                    if (r <= size.radius && r > size.radius - size.ringWidth)
                        points.Add(new Position(i, j));
                }
            return points;
        }

        public static Points squareRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadiusRing>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = -size.radius; j <= size.radius; j++)
                {
                    var r = Math.Max(Math.Abs(i), Math.Abs(j));
                    if (r <= size.radius && r > size.radius - size.ringWidth)
                        points.Add(new Position(i, j));
                }
            return points;
        }
        public static Points rectangleRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2Ring>();
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                for (int j = -size.radiusForward; j <= size.radiusForward; j++)
                {
                    if (i <= size.radiusSide && i > size.radiusSide - size.ringWidth) 
                        if(j <= size.radiusForward && j > size.radiusForward - size.ringWidth)
                            points.Add(new Position(i, j));
                }
            return points;
        }

        public static Points circleHalfRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadiusRing>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = 0; j <= size.radius; j++)
                {
                    var r = Math.Abs(i) + Math.Abs(j);
                    if (r <= size.radius && r > size.radius - size.ringWidth)
                        points.Add(new Position(i, j));
                }
            return points;
        }

        public static Points squareHalfRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadiusRing>();
            for (int i = -size.radius; i <= size.radius; i++)
                for (int j = 0; j <= size.radius; j++)
                {
                    var r = Math.Max(Math.Abs(i), Math.Abs(j));
                    if (r <= size.radius && r > size.radius - size.ringWidth)
                        points.Add(new Position(i, j));
                }
            return points;
        }
        public static Points rectangleHalfRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2Ring>();
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                for (int j = 0; j <= size.radiusForward; j++)
                {
                    var ai = Math.Abs(i);
                    var aj = Math.Abs(j);
                    if (ai <= size.radiusSide && ai > size.radiusSide - size.ringWidth)
                        if (aj <= size.radiusForward && aj > size.radiusForward - size.ringWidth)
                            points.Add(new Position(i, j));
                }
            return points;
        }


        public static Points ellipse(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2>();
            int maxRadius = Math.Max(size.radiusSide, size.radiusForward);
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                for (int j = -size.radiusForward; j <= size.radiusForward; j++)
                {
                    var r = Math.Abs(i) + Math.Abs(j);
                    if (r <= maxRadius)
                        points.Add(new Position(i, j));
                }
            return points;
        }
        public static Points ellipseRing(IZone zone)
        {
            Points points = new(zone);
            var size = zone.GetSize<ZoneSizeRadius2Ring>();
            int maxRadius = Math.Max(size.radiusSide, size.radiusForward);
            for (int i = -size.radiusSide; i <= size.radiusSide; i++)
                for (int j = -size.radiusForward; j <= size.radiusForward; j++)
                {
                    var r = Math.Abs(i) + Math.Abs(j);
                    if (r <= maxRadius && r > maxRadius - size.ringWidth)
                        points.Add(new Position(i, j));
                }
            return points;
        }


    }
}
