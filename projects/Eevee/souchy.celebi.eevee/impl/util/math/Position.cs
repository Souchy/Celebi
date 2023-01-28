using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.impl.util.math
{
    public class Position : IPosition
    {
        public int x { get; set; }
        public int z { get; set; }
        public int y { get; set; }

        public Vector3 set(int x, int z, int y = 0)
        {
            this.x = x;
            this.z = z;
            this.y = y;
            return this;
        }

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
            return Math.Abs(p.x - x) + Math.Abs(p.y - y) + Math.Abs(p.z - z); 
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

        public Vector3 abs() {
            x = Math.Abs(x);
            y = Math.Abs(y);
            z = Math.Abs(z);
            return this;
        }

        public List<IPosition> pathTo(IPosition target)
        {
            throw new NotImplementedException();
        }

        public Vector3 setAt(int index, int value)
        {
            switch(index)
            {
                case 0:
                    x = value; 
                    break;
                case 1: z = value;
                    break;
                case 2: y = value;
                    break;
                default:
                    throw new Exception("Position.setAt() index not supported: " + index);
            }
            return this;
        }
    }
}
