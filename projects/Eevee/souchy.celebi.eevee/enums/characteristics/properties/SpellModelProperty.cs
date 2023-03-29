using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record SpellModelProperty<T>(int localId) : CharacteristicType<T>(CharacteristicCategory.SpellModel, localId) where T : IStat
    {
        public static readonly SpellModelProperty<IStatBool> LineOfSightRequired = new(0);
        public static readonly SpellModelProperty<IStatSimple> MaxCharges = new(1);
        public static readonly SpellModelProperty<IStatSimple> MaxCastPerTarget = new(2);
        public static readonly SpellModelProperty<IStatSimple> MaxCastPerTurn = new(3);
        public static readonly SpellModelProperty<IStatSimple> Cooldown = new(4);
        public static readonly SpellModelProperty<IStatSimple> CooldownInitial = new(5);
        public static readonly SpellModelProperty<IStatSimple> CooldownGlobal = new(6);


        public static readonly Dictionary<int, SpellModelProperty> values = new();
        static SpellModelProperty()
        {
            var fields = typeof(SpellModelProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (SpellModelProperty) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
