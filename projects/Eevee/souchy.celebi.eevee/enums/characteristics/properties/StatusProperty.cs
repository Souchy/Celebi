using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record StatusProperty : CharacteristicType
    {
        public StatusProperty(int localId, string name) : base(CharacteristicCategory.State, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
            this.Factory = SimpleFactory;
        }

        public static readonly StatusProperty Stacks       = new(0, nameof(Stacks     ));
        public static readonly StatusProperty Delay        = new(1, nameof(Delay      ));
        public static readonly StatusProperty Duration     = new(2, nameof(Duration   ));
        public static readonly StatusProperty MaxStacks    = new(3, nameof(MaxStacks  ));
        public static readonly StatusProperty MaxDelay     = new(4, nameof(MaxDelay   ));
        public static readonly StatusProperty MaxDuration  = new(5, nameof(MaxDuration));


        public static readonly Dictionary<CharacteristicId, StatusProperty> values = new();
        static StatusProperty()
        {
            var fields = typeof(StatusProperty).GetFields().Where(f => f.FieldType == typeof(StatusProperty));
            foreach (var field in fields)
            {
                var value = (StatusProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
