using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.util.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace souchy.celebi.eevee.impl.util.map
{
    public class MapTools
    {
        public const int MAP_WIDTH = 20;
        public const int MAP_HEIGHT = 20;
        public const float cellHalf = 0.5f;
        public const int mapCountCell = MapTools.MAP_WIDTH * MapTools.MAP_HEIGHT * 2;

        public int getCellId(IVector3 vec) => getCellId(vec.x, vec.z);
        public int getCellId(int x, int z)
        {
            return 0;
        }
        public Position getCellPosition(int id)
        {
            return null;
        }

        public static int getCellIdByCoord(int x, int z)
        {
            //if (!MapTools.isValidCoord(x, y)) {
            //    return -1;
            //}
            return (int) Math.Floor((x - z) * MapTools.MAP_WIDTH + z + (x - z) / 2f);
        }

        public static Position getCellCoordById(int cellId) {
            if (!MapTools.isValidCellId(cellId))
            {
                return null;
            }
            int height = (int) Math.Floor((float) cellId / MapTools.MAP_WIDTH);
            int _loc3_ = (int) Math.Floor((height + 1f) / 2f);
            int _loc4_ = height - _loc3_;
            int offsetX = cellId - height * MapTools.MAP_WIDTH;
            return new Position
            {
                x = _loc3_ + offsetX,
                z = offsetX - _loc4_
            };
        }

        public static bool  isValidCellId(int cellId)
        {
            if (cellId >= 0)
            {
                return cellId < MapTools.mapCountCell;
            }
            return false;
        }

        //public static bool isValidCoord(int cell1, int cell2) {
        //    if (cell2 >= -cell1 && cell2 <= cell1 && cell2 <= MapTools.MAP_WIDTH + MapTools.MAX_Y_COORD - cell1) {
        //        return cell2 >= cell1 - (MapTools.MAP_HEIGHT - MapTools.MIN_Y_COORD);
        //    }
        //    return false;
        //}


    }
}
