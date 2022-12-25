using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.util.math
{
    public interface Vector3
    {

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Vector3 copy();
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

    }
}
