using souchy.celebi.eevee.face.shared.conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.impl.util
{
    public static class StaticEnumUtils
    {

        public static IEnumerable<T> findValues<T>()
        {
            return typeof(T).GetFields().Where(f => f.FieldType == typeof(T)).Select(f => (T) f.GetValue(null));
        }

    }
}
