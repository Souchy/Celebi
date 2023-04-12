using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.properties
{
    public sealed record OtherProperty: CharacteristicType
    {
        public OtherProperty(int localId) : base(CharacteristicCategory.Other, localId, CharacteristicType.SimpleFactory)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly OtherProperty Range      = new(0);
        public static readonly OtherProperty Speed      = new(1); // initiative
        public static readonly OtherProperty Erosion    = new(2);
        public static readonly OtherProperty Echo       = new(3); // number of times it echoes


        public static readonly Dictionary<CharacteristicId, OtherProperty> values = new();
        static OtherProperty()
        {
            var fields = typeof(OtherProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (OtherProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}

