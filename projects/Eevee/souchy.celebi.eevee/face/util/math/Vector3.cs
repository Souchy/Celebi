using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.util.math
{
    public interface IVector3
    {
        public const int X_INDEX = 0;
        public const int Z_INDEX = 1;
        /**
        Height
        */
        public const int Y_INDEX = 2;


        /// <summary>
        /// Board width
        /// </summary>
        public int x { get; set; }
        /// <summary>
        /// Board length
        /// </summary>
        public int z { get; set; }
        /// <summary>
        /// Height
        /// </summary>
        public int y { get; set; }


        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 set(int x, int z, int y = 0);
        public IVector3 set(IVector3 vec);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 add(IVector3 vec);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public IVector3 add(int x, int z);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 sub(IVector3 vec);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 mult(IVector3 vec);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public IVector3 scale(int x, int z);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 div(IVector3 vec);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int length();
        /// <summary>
        /// Returns self for chaining <br></br>
        /// 0 = x, 1 = z, 2 = y
        /// </summary>
        /// <param name="index">0, 1, 2 for x, z, y</param>
        /// <param name="value">value to set</param>
        /// <returns></returns>
        public IVector3 setAt(int index, int value);
        /// <summary>
        /// Get value at index: 0 = x, 1 = z, 2 = y
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int getAt(int index);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector3 copy();
        /// <summary>
        /// Returns self 
        /// </summary>
        public IVector3 abs();

        /// <summary>
        /// 2D distance
        /// </summary>
        public int distanceManhattan(IVector3 vec);
        /// <summary>
        /// 3D distance
        /// </summary>
        public int distanceManhattan3(IVector3 vec);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool equals(IVector3 v);
    }
}
