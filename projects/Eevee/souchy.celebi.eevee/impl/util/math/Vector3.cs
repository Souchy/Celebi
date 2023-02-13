using souchy.celebi.eevee.face.util.math;

namespace souchy.celebi.eevee.impl.util.math
{
    public class Vector3 : IVector3
    {
        public int x { get; set; }
        public int z { get; set; }
        public int y { get; set; }

        public Vector3() { }
        public Vector3(int i) : this(i, i, 0) { }
        public Vector3(int x, int z, int y = 0)
        {
            this.x = x;
            this.z = z;
            this.y = y;
        }

        public IVector3 set(int x, int z, int y = 0)
        {
            this.x = x;
            this.z = z;
            this.y = y;
            return this;
        }

        public IVector3 add(IVector3 p)
        {
            this.x += p.x;
            this.y += p.y;
            this.z += p.z;
            return this;
        }

        public IVector3 div(IVector3 p)
        {
            x /= p.x;
            y /= p.y;
            z /= p.z;
            return this;
        }

        public IVector3 mult(IVector3 p)
        {
            x *= p.x;
            y *= p.y;
            z *= p.z;
            return this;
        }
        public IVector3 sub(IVector3 p)
        {
            x -= p.x;
            y -= p.y;
            z -= p.z;
            return this;
        }

        public int distanceManhattan(IVector3 p)
        {
            return Math.Abs(p.x - x) + Math.Abs(p.y - y) + Math.Abs(p.z - z);
        }


        public IVector3 copy()
        {
            return new Vector3(x, z, y);
        }

        public IVector3 abs()
        {
            x = Math.Abs(x);
            y = Math.Abs(y);
            z = Math.Abs(z);
            return this;
        }

        public IVector3 setAt(int index, int value)
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    z = value;
                    break;
                case 2:
                    y = value;
                    break;
                default:
                    throw new Exception("Vector3.setAt() index not supported: " + index);
            }
            return this;
        }

        public int getAt(int index)
        {
            return index switch
            {
                0 => x,
                1 => z,
                2 => y,
                _ => throw new NotImplementedException()
            };
        }

        public IVector3 add(int x, int z)
        {
            this.x += x;
            this.z += z;
            return this;
        }

        public IVector3 scale(int x, int y)
        {
            this.x *= x;
            this.y *= y;
            return this;
        }

        public int length()
        {
            return (int) Math.Pow(x * x + z * z + y * y, 0.5);
        }

        public bool equals(IVector3 v)
        {
            return v.x == x && v.z == z && v.y == y;
        }
    }
}
