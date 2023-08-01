using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.util.serialization;
using System.ComponentModel;

namespace souchy.celebi.eevee.enums.characteristics
{
    [JsonConverter(typeof(CharacIdJsonConverter))]
    [TypeConverter(typeof(CharacIdTypeConverter))]
    public readonly record struct CharacteristicId(int ID)
    {
        public static implicit operator int(CharacteristicId v) => v.ID;
        public CharacteristicCategory GetCategory()
        {
            int cat = (int) Math.Floor(this.ID / 1000d);
            return (CharacteristicCategory) cat;
        }
        public override string ToString()
        {
            return ID.ToString();
        }
    }

    public static class CharacteristicIdExtentions
    {
        public static CharacteristicId GetAffinity(this ElementType stat) =>
            Affinity.values.First(v => v.Element == stat).ID;
        public static CharacteristicId GetResistance(this ElementType stat) =>
            Resistance.values.Values.First(v => v.Element == stat).ID;
        public static IEnumerable<CharacteristicType> GetCharacs(this CharacteristicCategory cat) => cat switch
        {
            CharacteristicCategory.Resource => Enumerable.OfType<CharacteristicType>(Resource.values.Values),
            CharacteristicCategory.Affinity => Enumerable.OfType<CharacteristicType>(Affinity.values),
            CharacteristicCategory.Resistance => Enumerable.OfType<CharacteristicType>(Resistance.values.Values),

            CharacteristicCategory.Contextual => Enumerable.OfType<CharacteristicType>(Contextual.values.Values),
            CharacteristicCategory.Other => Enumerable.OfType<CharacteristicType>(OtherProperty.values.Values),
            CharacteristicCategory.State => Enumerable.OfType<CharacteristicType>(State.values.Values),
            
            CharacteristicCategory.SpellModel => Enumerable.OfType<CharacteristicType>(SpellModelProperty.values.Values), //, SpellModelState.values.Values),
            CharacteristicCategory.Spell => Enumerable.OfType<CharacteristicType>(SpellProperty.values.Values),
            CharacteristicCategory.StatusContainer => Enumerable.OfType<CharacteristicType>(StatusContainerProperty.values.Values),
            CharacteristicCategory.StatusInstance => Enumerable.OfType<CharacteristicType>(StatusInstanceProperty.values.Values),
            _ => throw new NotImplementedException(),
        };
        public static CharacteristicType GetCharactType(this CharacteristicId id) =>
            id.GetCategory().GetCharacs().FirstOrDefault(v => v.ID == id);
    }

}
