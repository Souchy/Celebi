using souchy.celebi.eevee.face.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record SpellProperty(int localId) : CharacteristicType<IStatSimple>(CharacteristicCategory.Spell, localId)
    {
        public static readonly SpellProperty RemainingCharges = new(0);
        public static readonly SpellProperty RemainingCooldown = new(1);


        public static readonly Dictionary<int, SpellProperty> values = new();
        static SpellProperty()
        {
            var fields = typeof(SpellProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (SpellProperty) field.GetValue(null);
                values[value.id] = value;
            }
        }
    }
}
