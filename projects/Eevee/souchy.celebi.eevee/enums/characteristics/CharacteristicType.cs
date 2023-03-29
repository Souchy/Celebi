using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.enums.characteristics
{
    public record CharacteristicType<T>(CharacteristicCategory category, int localId) where T : IStat
    {
        public readonly IID id = (IID) ((int) category * 1000 + localId);
    }

    public enum CharacteristicCategory
    {
        Resource = 1,
        Affinity = 2,
        Resistance = 3,
        State = 4,
        Contextual = 5,
        Other = 6,

        Spell = 7,
        SpellModel = 8,
        Status = 9,
    }
    public enum ResourceEnum
    {
        Life,
        Mana,
        Movement,
        Summon,
        Rage
    }
    public enum ResourceProperty
    {
        Current,
        Max,
        Missing,
        Regen,
        Percent,
        MissingPercent
    }

    // [AttributeUsage(AttributeTargets.Field)]
    // public class CharacteristicTypePropertiesAttribute : Attribute 
    // {
    //     public readonly StatValueType valueType;
    //     //public readonly StatCategory category;
    //     public readonly ElementType element;
    //     public CharacteristicTypePropertiesAttribute(StatValueType valueType, ElementType element = ElementType.Water)
    //     {
    //         this.valueType = valueType;
    //         //this.category = category;
    //         this.element = element;
    //     }
    // }
    // [AttributeUsage(AttributeTargets.Field)]
    // public class ElementTypePropertiesAttribute : Attribute
    // {
    //     public readonly ElementType element;
    //     public ElementTypePropertiesAttribute(ElementType element)
    //     {
    //         this.element = element;
    //     }
    // }


}
