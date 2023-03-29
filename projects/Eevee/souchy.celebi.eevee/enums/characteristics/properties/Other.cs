using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.properties
{
    public sealed record OtherProperty(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Other, localId)
    {
        public static readonly OtherProperty Range = new(0);
        public static readonly OtherProperty Speed = new(1);
        public static readonly OtherProperty Erosion = new(2);
        public static readonly OtherProperty Echo = new(3);


        public static readonly Dictionary<int, OtherProperty> values = new();
        static OtherProperty()
        {
            var fields = typeof(OtherProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (OtherProperty) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}

