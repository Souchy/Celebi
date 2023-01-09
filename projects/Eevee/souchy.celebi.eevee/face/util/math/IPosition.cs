using souchy.celebi.eevee.util.math;

namespace souchy.celebi.eevee.face.util.math
{
    public interface IPosition : Vector3
    {
        /// <summary>
        /// 2D distance
        /// </summary>
        public int distanceManhattan(IPosition p);
        /// <summary>
        /// List of positions for A* path
        /// </summary>
        public List<IPosition> pathTo(IPosition target);
    }
}