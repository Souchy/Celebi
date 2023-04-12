using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record SpellModelProperty: CharacteristicType
    {
        public SpellModelProperty(int localId, string name) : base(CharacteristicCategory.SpellModel, localId, name, SimpleFactory)
        {
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly SpellModelProperty MaxCharges        = new(1, nameof(MaxCharges));
        public static readonly SpellModelProperty MaxCastPerTarget  = new(2, nameof(MaxCastPerTarget));
        public static readonly SpellModelProperty MaxCastPerTurn    = new(3, nameof(MaxCastPerTurn));
        public static readonly SpellModelProperty Cooldown          = new(4, nameof(Cooldown));
        public static readonly SpellModelProperty CooldownInitial   = new(5, nameof(CooldownInitial));
        public static readonly SpellModelProperty CooldownGlobal    = new(6, nameof(CooldownGlobal));


        public static readonly Dictionary<CharacteristicId, SpellModelProperty> values = new();
        static SpellModelProperty()
        {
            var fields = typeof(SpellModelProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (SpellModelProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
    public sealed record SpellModelState : CharacteristicType
    {
        public SpellModelState(int localId) : base(CharacteristicCategory.SpellModel, localId, CharacteristicType.BoolFactory)
        {
            this.StatValueType = StatValueType.Bool;
        }

        public static readonly SpellModelState LineOfSightRequired = new(0);


        public static readonly Dictionary<CharacteristicId, SpellModelState> values = new();
        static SpellModelState()
        {
            var fields = typeof(SpellModelState).GetFields();
            foreach (var field in fields)
            {
                var value = (SpellModelState) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
