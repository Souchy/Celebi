using souchy.celebi.eevee.impl.util.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.umbreon.util
{
    public static class VectorUtils
    {
        public static Godot.Vector3 toGodot(this Vector3 fromBoard)
        {
            return new Godot.Vector3(fromBoard.x - (Sapphire.mapLengthX - 1) / 2, fromBoard.y, fromBoard.z - (Sapphire.mapLengthZ - 1) / 2);
        }
        public static Vector3 fromGodot(this Godot.Vector3 fromGodot)
        {
            return new Vector3((int) fromGodot.X + (Sapphire.mapLengthX - 1) / 2, (int) fromGodot.Y, (int) fromGodot.Z + (Sapphire.mapLengthZ - 1) / 2);
        }
    }
}
