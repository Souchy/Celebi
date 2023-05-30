using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.impl.shared.conditions.value;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Affinity : CharacteristicType
    {
        public ElementType Element { get; init; }
        public Affinity(int localId, string name, ElementType ele = ElementType.None, params ICondition[] conditions) : base(CharacteristicCategory.Affinity, localId, name, conditions)
        {
            this.Element = ele;
            this.StatValueType = StatValueType.Simple;
        }

        public static readonly Affinity True                = new(1, nameof(True),  ElementType.True);
        public static readonly Affinity Fire                = new(2, nameof(Fire),  ElementType.Fire);
        public static readonly Affinity Water               = new(3, nameof(Water), ElementType.Water);
        public static readonly Affinity Earth               = new(4, nameof(Earth), ElementType.Earth);
        public static readonly Affinity Air                 = new(5, nameof(Air),   ElementType.Air);
        public static readonly Affinity Dark                = new(6, nameof(Dark),  ElementType.Dark);
        public static readonly Affinity Light               = new(7, nameof(Light), ElementType.Light);

        /// <summary>
        /// All damage
        /// </summary>
        public static readonly Affinity Damage              = new(20,  nameof(Damage));
        /// <summary>
        /// Poison damage
        /// </summary>
        public static readonly Affinity IndirectDamage      = new(21,  nameof(IndirectDamage));
        public static readonly Affinity Heal                = new(22,  nameof(Heal));
        public static readonly Affinity Melee               = new(23,  nameof(Melee), conditions: new ICondition[]{ 
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.LE,
            }
        });
        public static readonly Affinity Distance            = new(24,  nameof(Distance), conditions: new ICondition[]{
            new DistanceCondition() {
                distance = Constants.MELEE_RANGE,
                comparator = ConditionComparatorType.GT,
            }
        });
        public static readonly Affinity Trap                = new(25, nameof(Trap));
        public static readonly Affinity Glyph               = new(26, nameof(Glyph));
        /// <summary>
        /// penetrates resistances (just another multiplier)
        /// applies to defenses rather than offenses
        /// </summary>
        public static readonly Affinity PenetrationPercent  = new(27, nameof(PenetrationPercent));


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
