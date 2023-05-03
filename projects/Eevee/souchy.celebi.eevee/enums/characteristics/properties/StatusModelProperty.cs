using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record StatusModelProperty : CharacteristicType
    {
        public StatusModelProperty(int localId, string name) : base(CharacteristicCategory.State, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
            this.Factory = SimpleFactory;
        }

        public static readonly StatusModelProperty Stacks       = new(0, nameof(Stacks     ));
        public static readonly StatusModelProperty Delay        = new(1, nameof(Delay      ));
        public static readonly StatusModelProperty Duration     = new(2, nameof(Duration   ));
        public static readonly StatusModelProperty MaxStacks    = new(3, nameof(MaxStacks  ));
        public static readonly StatusModelProperty MaxDelay     = new(4, nameof(MaxDelay   ));
        public static readonly StatusModelProperty MaxDuration  = new(5, nameof(MaxDuration));


        public static readonly Dictionary<CharacteristicId, StatusModelProperty> values = new();
        static StatusModelProperty()
        {
            var fields = typeof(StatusModelProperty).GetFields().Where(f => f.FieldType == typeof(StatusModelProperty));
            foreach (var field in fields)
            {
                var value = (StatusModelProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
