using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record Resistance(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Resistance, localId)
    {
        public static readonly Resistance BaseFire = new(1);
        public static readonly Resistance BaseWater = new(2);
        public static readonly Resistance BaseEarth = new(3);
        public static readonly Resistance BaseAir = new(4);
        public static readonly Resistance BaseDamage = new(5);
        public static readonly Resistance BaseHeal = new(6);
        public static readonly Resistance BaseMelee = new(7);
        public static readonly Resistance BaseDistance = new(8);
        public static readonly Resistance BaseTrap = new(9);
        public static readonly Resistance BaseGlyph = new(10);

        public static readonly Resistance Fire = new(11);
        public static readonly Resistance Water = new(12);
        public static readonly Resistance Earth = new(13);
        public static readonly Resistance Air = new(14);
        public static readonly Resistance Damage = new(15);
        public static readonly Resistance Heal = new(16);
        public static readonly Resistance Melee = new(17);
        public static readonly Resistance Distance = new(18);
        public static readonly Resistance Trap = new(19);
        public static readonly Resistance Glyph = new(20);


        public static readonly Dictionary<int, Resistance> values = new();
        static Resistance()
        {
            var fields = typeof(Resistance).GetFields();
            foreach (var field in fields)
            {
                var value = (Resistance) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
