using Microsoft.CodeAnalysis;
using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.impl.util.math
{
    public class Position : Vector3, IPosition
    {
        public Position() { }
        public Position(int i) : base(i, i, 0) { }
        public Position(int x, int z, int y = 0) : base(x, z, y) { }

        public List<IPosition> pathTo(IPosition target)
        {
            throw new NotImplementedException();
        }
    }
}
