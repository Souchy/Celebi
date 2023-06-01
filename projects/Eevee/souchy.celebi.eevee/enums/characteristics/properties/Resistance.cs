using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.conditions.value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Resistance : CharacteristicType
    {
        public ElementType Element { get; init; }
        public Resistance(int localId, string name, ElementType ele = ElementType.None, params ICondition[] conditions) 
            : base(CharacteristicCategory.Resistance, localId, name, conditions: conditions)
        {
            this.Element = ele;
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly Resistance True      = new(1,  nameof(True),  ElementType.True);
        public static readonly Resistance Fire      = new(2,  nameof(Fire),  ElementType.Fire);
        public static readonly Resistance Water     = new(3,  nameof(Water), ElementType.Water);
        public static readonly Resistance Earth     = new(4,  nameof(Earth), ElementType.Earth);
        public static readonly Resistance Air       = new(5,  nameof(Air),   ElementType.Air);
        public static readonly Resistance Dark      = new(6,  nameof(Dark),  ElementType.Dark);
        public static readonly Resistance Light     = new(7,  nameof(Light), ElementType.Light);


        public static readonly Resistance Damage    = new(20,  nameof(Damage));
        public static readonly Resistance IndirectDamage = new(21, nameof(IndirectDamage));
        public static readonly Resistance Heal      = new(22,  nameof(Heal));
        public static readonly Resistance Melee     = new(23,  nameof(Melee), conditions: new ICondition[]{
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.LE,
            }
        });
        public static readonly Resistance Distance  = new(24,  nameof(Distance), conditions: new ICondition[]{
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.GT,
            }
        });
        public static readonly Resistance Trap      = new(25, nameof(Trap));
        public static readonly Resistance Glyph     = new(26, nameof(Glyph));


        public static readonly Dictionary<CharacteristicId, Resistance> values = new();
        static Resistance()
        {
            var fields = typeof(Resistance).GetFields()
                .Where(f => f.FieldType == typeof(Resistance));
            foreach (var field in fields)
            {
                var value = (Resistance) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
