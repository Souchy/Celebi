using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums
{
    //public enum CellType
    //{
    //    hole,
    //    floor,
    //    block
    //}

    public sealed record CellType
    {
        public int id { get; }
        public bool isWalkable { get; }
        public bool blocksLos { get; }
        public CellType(int id, bool isWalkable, bool blocksLos)
        {
            this.id = id;
            this.isWalkable = isWalkable;
            this.blocksLos = blocksLos;
            _values.Add(this);
        }

        public static readonly CellType hole = new CellType(0, false, false);
        public static readonly CellType floor = new CellType(1, true, false);
        public static readonly CellType block = new CellType(2, false, true);


        private static List<CellType> _values = new List<CellType>();
        public static CellType get(int id) => _values.Find(v => v.id == id);
        public static IEnumerable<CellType> values() => _values.ToArray();
    }


}
