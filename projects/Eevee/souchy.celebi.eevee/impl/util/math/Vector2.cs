using souchy.celebi.eevee.face.util.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.util.math
{
    public class Vector2 : IVector2
    {
        public int x { get; set; }
        public int z { get; set; }

        public Vector2() { }
        public Vector2(int i) : this(i, i) { }
        public Vector2(int x, int z)
        {
            this.x = x;
            this.z = z;
        }

        public IVector2 set(int x, int z)
        {
            this.x = x;
            this.z = z;
            return this;
        }

        public IVector2 add(IVector2 p)
        {
            this.x += p.x;
            this.z += p.z;
            return this;
        }

        public IVector2 div(IVector2 p)
        {
            x /= p.x;
            z /= p.z;
            return this;
        }

        public IVector2 mult(IVector2 p)
        {
            x *= p.x;
            z *= p.z;
            return this;
        }
        public IVector2 sub(IVector2 p)
        {
            x -= p.x;
            z -= p.z;
            return this;
        }

        public int distanceManhattan(IPosition p)
        {
            return Math.Abs(p.x - x) + Math.Abs(p.z - z);
        }


        public IVector2 copy()
        {
            return new Vector2(x, z);
        }

        public IVector2 abs()
        {
            x = Math.Abs(x);
            z = Math.Abs(z);
            return this;
        }

        public IVector2 setAt(int index, int value)
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    z = value;
                    break;
                default:
                    throw new Exception("Position.setAt() index not supported: " + index);
            }
            return this;
        }

        public int getAt(int index)
        {
            return index switch
            {
                0 => x,
                1 => z,
                _ => throw new NotImplementedException(),
            };
        }

        public IVector2 add(int x, int z)
        {
            this.x += x;
            this.z += z;
            return this;
        }

        public IVector2 scale(int x, int z)
        {
            this.x *= x;
            this.z *= z;
            return this;
        }

        public int length()
        {
            return (int) Math.Pow(x * x + z * z, 0.5);
        }
    }
}
