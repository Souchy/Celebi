using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record StatusModelProperty(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Status, localId)
    {
        public static readonly StatusModelProperty Stacks = new(0);
        public static readonly StatusModelProperty Delay = new(1);
        public static readonly StatusModelProperty Duration = new(2);
        public static readonly StatusModelProperty MaxStacks = new(3);
        public static readonly StatusModelProperty MaxDelay = new(4);
        public static readonly StatusModelProperty MaxDuration = new(5);


        public static readonly Dictionary<int, StatusModelProperty> values = new();
        static StatusModelProperty()
        {
            var fields = typeof(StatusModelProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (StatusModelProperty) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
