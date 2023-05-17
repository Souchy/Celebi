using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    public sealed record SpellProperty : CharacteristicType
    {
        public SpellProperty(int localId, string name, StatValueType type, StatFactory factory) : base(CharacteristicCategory.Spell, localId, name)
        {
            this.StatValueType = type;
            this.Factory = factory;
        }


        public static readonly SpellProperty RemainingCharges               = new(0, nameof(RemainingCharges              ), StatValueType.Simple, SimpleFactory);
        public static readonly SpellProperty RemainingCooldown              = new(1, nameof(RemainingCooldown             ), StatValueType.Simple, SimpleFactory);
        public static readonly SpellProperty NumberOfCastsThisTurn          = new(2, nameof(NumberOfCastsThisTurn         ), StatValueType.Simple, SimpleFactory);
        public static readonly SpellProperty NumberOfCastsThisTurnPerEntity = new(3, nameof(NumberOfCastsThisTurnPerEntity), StatValueType.EntityStatDictionary, EntityStatDictionaryFactory); 


        public static readonly Dictionary<CharacteristicId, SpellProperty> values = new();
        static SpellProperty()
        {
            var fields = typeof(SpellProperty).GetFields().Where(f => f.FieldType == typeof(SpellProperty));
            foreach (var field in fields)
            {
                var value = (SpellProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
