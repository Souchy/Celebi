﻿using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics.creature
{
    /// <summary>
    /// Spell instance properties. <br></br>
    /// All of this is 'contextual' pretty much. 
    /// </summary>
    public sealed record SpellProperty : CharacteristicType
    {
        public SpellProperty(int localId, string name, StatValueType type) : base(CharacteristicCategory.Spell, localId, name)
        {
            this.StatValueType = type;
        }


        public static readonly SpellProperty RemainingCharges               = new(0, nameof(RemainingCharges              ), StatValueType.Simple);
        public static readonly SpellProperty RemainingCooldown              = new(1, nameof(RemainingCooldown             ), StatValueType.Simple);
        public static readonly SpellProperty NumberOfCastsThisTurn          = new(2, nameof(NumberOfCastsThisTurn         ), StatValueType.Simple);
        public static readonly SpellProperty NumberOfCastsThisTurnPerEntity = new(3, nameof(NumberOfCastsThisTurnPerEntity), StatValueType.EntityStatDictionary); 
        
        // Effects
        /// <summary>
        /// Compilation of effects for this spell's current cast. <br></br>
        /// Ex: pillage uses this to heal in aoe
        /// </summary>
        public static readonly SpellProperty DamageDoneThisCast             = new(101, nameof(DamageDoneThisCast), StatValueType.Simple);
        /// <summary>
        /// Ex: something like reconstitution: heal the target and do equivalent damage all around 
        /// </summary>
        public static readonly SpellProperty HealDoneThisCast               = new(102, nameof(HealDoneThisCast), StatValueType.Simple);
        /// <summary>
        /// Ex: do x damage per ap reduced
        /// </summary>
        public static readonly SpellProperty ManaReducedThisCast            = new(103, nameof(ManaReducedThisCast), StatValueType.Simple);
        public static readonly SpellProperty MovementReducedThisCast        = new(104, nameof(MovementReducedThisCast), StatValueType.Simple);


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
