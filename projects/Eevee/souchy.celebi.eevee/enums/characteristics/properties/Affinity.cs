using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.conditions.value;
using souchy.celebi.eevee.impl.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Affinity : CharacteristicType
    {
        public ElementType Element { get; init; }
        public Affinity(int localId, string name, ElementType ele = ElementType.None, params ICondition[] conditions) : base(CharacteristicCategory.Affinity, localId, name, conditions)
        {
            this.Element = ele;
            this.StatValueType = StatValueType.Simple;
            this.Factory = SimpleFactory;
        }

        public static readonly Affinity Fire        = new(1, nameof(Fire),  ElementType.Fire);
        public static readonly Affinity Water       = new(2, nameof(Water), ElementType.Water);
        public static readonly Affinity Earth       = new(3, nameof(Earth), ElementType.Earth);
        public static readonly Affinity Air         = new(4, nameof(Air),   ElementType.Air);
        public static readonly Affinity True        = new(5, nameof(True),  ElementType.True);

        public static readonly Affinity Damage      = new(6,  nameof(Damage));
        public static readonly Affinity Heal        = new(7,  nameof(Heal));
        public static readonly Affinity Melee       = new(8,  nameof(Melee), conditions: new ICondition[]{ 
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.LE,
            }
        });
        public static readonly Affinity Distance    = new(9,  nameof(Distance), conditions: new ICondition[]{
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.GT,
            }
        });
        public static readonly Affinity Trap        = new(10, nameof(Trap));
        public static readonly Affinity Glyph       = new(11, nameof(Glyph));
        /// <summary>
        /// penetrates resistances (just another multiplier)
        /// </summary>
        public static readonly Affinity PenetrationPercent = new(12, nameof(PenetrationPercent));


        public static readonly Dictionary<CharacteristicId, Affinity> values = new();
        static Affinity()
        {
            var fields = typeof(Affinity).GetFields().Where(f => f.FieldType == typeof(Affinity));
            foreach (var field in fields)
            {
                var value = (Affinity) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
