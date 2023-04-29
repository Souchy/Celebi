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
        public SpellProperty(int localId, string name, StatFactory factory) : base(CharacteristicCategory.Spell, localId, name)
        {
            this.StatValueType = StatValueType.Variant;
            this.Factory = factory;
        }


        public static readonly SpellProperty RemainingCharges               = new(0, nameof(RemainingCharges              ), SimpleFactory);
        public static readonly SpellProperty RemainingCooldown              = new(1, nameof(RemainingCooldown             ), SimpleFactory);
        public static readonly SpellProperty NumberOfCastsThisTurn          = new(2, nameof(NumberOfCastsThisTurn         ), SimpleFactory);
        public static readonly SpellProperty NumberOfCastsThisTurnPerEntity = new(3, nameof(NumberOfCastsThisTurnPerEntity), (id, value) => StatIIDDictionary.Create(id, (Dictionary<IID, IStat>) value)); // IStatIIDDictionary <iid, IStatSimple>


        public static readonly Dictionary<CharacteristicId, SpellProperty> values = new();
        static SpellProperty()
        {
            var fields = typeof(SpellProperty).GetFields();
            foreach (var field in fields)
            {
                var value = (SpellProperty) field.GetValue(null);
                values[value.ID] = value;
            }
        }
    }
}
