using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.interfaces;
using souchy.celebi.eevee.util.math;

namespace souchy.celebi.eevee.impl.util.math
{
    public class Position : IPosition
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public Vector3 add(Vector3 p)
        {
            this.x += p.x;
            this.y += p.y;
            this.z += p.z;
            return this;
        }

        public Vector3 div(Vector3 p)
        {
            x /= p.x;
            y /= p.y;
            z /= p.z;
            return this;
        }

        public Vector3 mult(Vector3 p)
        {
            x *= p.x;
            y *= p.y;
            z *= p.z;
            return this;
        }
        public Vector3 sub(Vector3 p)
        {
            x -= p.x;
            y -= p.y;
            z -= p.z;
            return this;
        }

        public int distanceManhattan(IPosition p)
        {
            return Math.Abs(p.x - x) + Math.Abs(p.y - y);
        }


        public Vector3 copy()
        {
            return new Position()
            {
                x = this.x,
                y = this.y,
                z = this.z
            };
        }

        public List<IPosition> pathTo(IPosition target)
        {
            throw new NotImplementedException();
        }

    }
}
