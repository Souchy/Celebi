using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record StatusContainerProperty : CharacteristicType
    {
        public StatusContainerProperty(int localId, string name, int defaultValue = 0) : base(CharacteristicCategory.StatusContainer, localId, name, defaultValue)
        {
            this.StatValueType = StatValueType.Simple;
        }
        public StatusContainerProperty(int localId, string name, object defaultValue) : base(CharacteristicCategory.StatusContainer, localId, name, defaultValue)
        {
            this.StatValueType = StatValueType.Simple;
        }

        //public static readonly StatusContainerProperty Stacks       = new(0, nameof(Stacks     )); 
        public static readonly StatusContainerProperty MergeStrategy = new(0, nameof(MergeStrategy), defaultValue: StatusMergeStrategy.Ignore) // StatusMergeStrategy enum
        {
            enumValueConstraint = typeof(StatusMergeStrategy)
        };
        public static readonly StatusContainerProperty UnbewitchStrategy = new(1, nameof(UnbewitchStrategy), defaultValue: StatusUnbewitchStrategy.Dispellable) // StatusUnbewitchStrategy
        {
            enumValueConstraint = typeof(StatusUnbewitchStrategy)
        };
        public static readonly StatusContainerProperty MaxStacks        = new(2, nameof(MaxStacks  )); 
        public static readonly StatusContainerProperty MaxDelay         = new(3, nameof(MaxDelay   )); 
        public static readonly StatusContainerProperty MaxDuration      = new(4, nameof(MaxDuration)); 


        public static readonly Dictionary<CharacteristicId, StatusContainerProperty> values = new();
        static StatusContainerProperty()
        {
            var fields = typeof(StatusContainerProperty).GetFields().Where(f => f.FieldType == typeof(StatusContainerProperty));
            foreach (var field in fields)
            {
                var value = (StatusContainerProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }

    public sealed record StatusInstanceProperty : CharacteristicType
    {
        public StatusInstanceProperty(int localId, string name) : base(CharacteristicCategory.StatusInstance, localId, name)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly StatusInstanceProperty Delay = new(1, nameof(Delay)); // instance
        public static readonly StatusInstanceProperty Duration = new(2, nameof(Duration)); // instance


        public static readonly Dictionary<CharacteristicId, StatusInstanceProperty> values = new();
        static StatusInstanceProperty()
        {
            var fields = typeof(StatusInstanceProperty).GetFields().Where(f => f.FieldType == typeof(StatusInstanceProperty));
            foreach (var field in fields)
            {
                var value = (StatusInstanceProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
