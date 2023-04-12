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
        public StatusModelProperty(int localId) : base(CharacteristicCategory.State, localId, SimpleFactory)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly StatusModelProperty Stacks       = new(0);
        public static readonly StatusModelProperty Delay        = new(1);
        public static readonly StatusModelProperty Duration     = new(2);
        public static readonly StatusModelProperty MaxStacks    = new(3);
        public static readonly StatusModelProperty MaxDelay     = new(4);
        public static readonly StatusModelProperty MaxDuration  = new(5);


        public static readonly Dictionary<CharacteristicId, StatusModelProperty> values = new();
        static StatusModelProperty()
        {
            var fields = typeof(StatusModelProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (StatusModelProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
