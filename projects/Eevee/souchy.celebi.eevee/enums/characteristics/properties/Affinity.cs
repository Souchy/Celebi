using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Affinity(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Affinity, localId)
    {
        //[ElementTypeProperties(ElementType.Fire)]
        // [CharacteristicTypeProperties(StatValueType.Simple, ElementType.Fire)]
        public static readonly Affinity BaseFire = new(1);
        //[ElementTypeProperties(ElementType.Water)]
        public static readonly Affinity BaseWater = new(2);
        //[ElementTypeProperties(ElementType.Earth)]
        public static readonly Affinity BaseEarth = new(3);
        //[ElementTypeProperties(ElementType.Air)]
        public static readonly Affinity BaseAir = new(4);

        public static readonly Affinity BaseDamage = new(5);
        public static readonly Affinity BaseHeal = new(6);
        public static readonly Affinity BaseMelee = new(7);
        public static readonly Affinity BaseDistance = new(8);
        public static readonly Affinity BaseTrap = new(9);
        public static readonly Affinity BaseGlyph = new(10);


        public static readonly Affinity Fire = new(11);
        public static readonly Affinity Water = new(12);
        public static readonly Affinity Earth = new(13);
        public static readonly Affinity Air = new(14);

        public static readonly Affinity Damage = new(15);
        public static readonly Affinity Heal = new(16);
        public static readonly Affinity Melee = new(17);
        public static readonly Affinity Distance = new(18);
        public static readonly Affinity Trap = new(19);
        public static readonly Affinity Glyph = new(20);


        public static readonly Dictionary<int, Affinity> values = new();
        static Affinity()
        {
            var fields = typeof(Affinity).GetFields();
            foreach (var field in fields)
            {
                var value = (Affinity) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
