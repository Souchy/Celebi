using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.util.math
{
    public interface Vector3
    {
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
        public Vector3 set(int x, int z, int y = 0);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public Vector3 add(Vector3 p);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public Vector3 sub(Vector3 p);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public Vector3 mult(Vector3 p);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public Vector3 div(Vector3 p);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        /// <param name="index">0, 1, 2 for x, z, y</param>
        /// <param name="value">value to set</param>
        /// <returns></returns>
        public Vector3 setAt(int index, int value);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public Vector3 copy();

    }
}
