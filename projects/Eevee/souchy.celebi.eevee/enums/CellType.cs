using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.impl.util;
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
        private CellType() { }  
        public CellType(int id, bool isWalkable, bool blocksLos)
        {
            this.id = id;
            this.isWalkable = isWalkable;
            this.blocksLos = blocksLos;
        }

        public static readonly CellType hole = new CellType(0, false, false);
        public static readonly CellType floor = new CellType(1, true, false);
        public static readonly CellType block = new CellType(2, false, true);
        /// <summary>
        /// Something you can walk through, but still blocks LoS, like a heavy mist, a fabric, a postsign...
        /// It might be confusing to players but at least I have the option if I need it.
        /// </summary>
        public static readonly CellType @object = new CellType(3, true, true);


        private static List<CellType> _values = new List<CellType>();
        static CellType() => _values.AddRange(StaticEnumUtils.findValues<CellType>());
        public static CellType get(int id) => _values.Find(v => v.id == id);
        public static IEnumerable<CellType> values() => _values.ToArray();
    }


}
