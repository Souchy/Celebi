namespace souchy.celebi.eevee.face.util.math
{
    public interface IVector2
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
        /// Returns self for chaining
        /// </summary>
        public IVector2 set(int x, int z);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector2 add(IVector2 p);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public IVector2 add(int x, int z);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector2 sub(IVector2 p);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector2 mult(IVector2 p);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public IVector2 scale(int x, int z);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector2 div(IVector2 p);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int length();
        /// <summary>
        /// Returns self for chaining <br></br>
        /// 0 = x, 1 = z
        /// </summary>
        /// <param name="index">0, 1 for x, z</param>
        /// <param name="value">value to set</param>
        /// <returns></returns>
        public IVector2 setAt(int index, int value);
        /// <summary>
        /// Get value at index: 0 = x, 1 = z
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int getAt(int index);
        /// <summary>
        /// Returns self for chaining
        /// </summary>
        public IVector2 copy();
        /// <summary>
        /// Returns self 
        /// </summary>
        public IVector2 abs();
    }
}
