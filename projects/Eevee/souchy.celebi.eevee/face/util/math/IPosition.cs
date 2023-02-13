
namespace souchy.celebi.eevee.face.util.math
{
    public interface IPosition : IVector3
    {
        /// <summary>
        /// List of positions for A* path
        /// </summary>
        public List<IPosition> pathTo(IPosition target);
    }
}