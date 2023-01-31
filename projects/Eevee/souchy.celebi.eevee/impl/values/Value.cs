using souchy.celebi.eevee.face.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.values
{
    public class Value<T> : IValue<T>
    {
        public T value { get; set; }

        public Value(T v)
        {
            value = v;
        }


    }
}
