using souchy.celebi.eevee.enums.characteristics.creature;
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
        public OtherProperty(int localId, string name) : base(CharacteristicCategory.Other, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
            this.Factory = SimpleFactory;
        }

        public static readonly OtherProperty Range      = new(0, nameof(Range));
        public static readonly OtherProperty Speed      = new(1, nameof(Speed)); // initiative
        public static readonly OtherProperty Erosion    = new(2, nameof(Erosion));
        public static readonly OtherProperty Echo       = new(3, nameof(Echo)); // number of times it echoes


        public static readonly Dictionary<CharacteristicId, OtherProperty> values = new();
        static OtherProperty()
        {
            var fields = typeof(OtherProperty).GetFields()
                .Where(f => f.FieldType == typeof(OtherProperty));
            foreach (var field in fields)
            {
                var value = (OtherProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}

