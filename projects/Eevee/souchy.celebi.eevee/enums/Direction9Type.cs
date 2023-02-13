using souchy.celebi.eevee.impl.util.math;
using System.Reflection;

namespace souchy.celebi.eevee.enums
{

    public class UnitVector2Attribute : Attribute
    {
        public int x, z;
        public UnitVector2Attribute(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public Vector2 toVector() => new Vector2(x, z);
    }

    public enum Direction9Type
    {
        [UnitVector2(0, 0)] center,
        [UnitVector2(0, 1)] top,
        [UnitVector2(1, 1)] topright,
        [UnitVector2(1, 0)] right,
        [UnitVector2(1, -1)] bottomright,
        [UnitVector2(0, -1)] bottom, 
        [UnitVector2(-1, -1)] bottomleft,
        [UnitVector2(-1, 0)] left, 
        [UnitVector2(-1, 1)] topleft
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
    }


}