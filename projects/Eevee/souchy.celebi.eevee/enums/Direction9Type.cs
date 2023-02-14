using souchy.celebi.eevee.impl.util.math;
using System.Reflection;

namespace souchy.celebi.eevee.enums
{

    public class UnitVector2Attribute : Attribute
    {
        public double x, z;
        public UnitVector2Attribute(double x, double z)
        {
            this.x = x;
            this.z = z;
        }
        //public Vector2 toVector() => new Vector2(x, z);
    }

    public enum Direction9Type
    {
        [UnitVector2(0, 0)] center,
        [UnitVector2(0, -1)] top,
        [UnitVector2(-1, -1)] topright,
        [UnitVector2(-1, 0)] right,
        [UnitVector2(-1, 1)] bottomright,
        [UnitVector2(0, 1)] bottom, 
        [UnitVector2(1, 1)] bottomleft,
        [UnitVector2(1, 0)] left, 
        [UnitVector2(1, -1)] topleft
    }

    public enum Direction3TypeLine
    {
        [UnitVector2(-0.5d, -0.5d)] center,
        [UnitVector2(-1, -1)] top,
        [UnitVector2(-1, -1)] topright,
        [UnitVector2(-0.5d, -0.5d)] right,
        [UnitVector2(0, 0)] bottomright,
        [UnitVector2(0, 0)] bottom,
        [UnitVector2(0, 0)] bottomleft,
        [UnitVector2(-0.5d, -0.5d)] left,
        [UnitVector2(-1, -1)] topleft
    }

    public enum Rotation4Type
    {
        [UnitVector2(0, 0)] top,
        [UnitVector2(0, -90)] right,
        [UnitVector2(0, 180)] bottom,
        [UnitVector2(0, 90)] left,
    }

    public static class Direction9Extension
    {
        public static UnitVector2Attribute GetUnitVector(this Direction9Type dir)
        {
            var attr = dir.GetType()
                    .GetField(Enum.GetName(dir))
                    .GetCustomAttribute(typeof(UnitVector2Attribute), true);
            return (UnitVector2Attribute) attr;
        }
        public static UnitVector2Attribute GetUnitVector(this Direction3TypeLine dir)
        {
            var attr = dir.GetType()
                    .GetField(Enum.GetName(dir))
                    .GetCustomAttribute(typeof(UnitVector2Attribute), true);
            return (UnitVector2Attribute) attr;
        }
        public static UnitVector2Attribute GetUnitVector(this Rotation4Type dir)
        {
            var attr = dir.GetType()
                    .GetField(Enum.GetName(dir))
                    .GetCustomAttribute(typeof(UnitVector2Attribute), true);
            return (UnitVector2Attribute) attr;
        }
    }


}