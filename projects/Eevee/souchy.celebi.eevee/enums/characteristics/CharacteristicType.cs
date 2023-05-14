using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.serialization;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;

namespace souchy.celebi.eevee.enums.characteristics
{

    //[JsonIgnore]
    public delegate IStat StatFactory(CharacteristicId id, object value = null);

    public record CharacteristicType(CharacteristicCategory Category, int LocalId, string BaseName, params ICondition[] conditions)
    {
        public StatValueType StatValueType { get; init; }
        public CharacteristicId ID { get; init; } = new CharacteristicId(((int) Category) * 1000 + LocalId);
        public IID nameModelUid { get; set; } = (IID) (nameof(CharacteristicType) + "." + BaseName);
        [JsonIgnore]
        public StatFactory Factory { get; init; }

        public IStringEntity GetName() => Eevee.models.i18n.Values.FirstOrDefault(s => s.modelUid == nameModelUid); //i18n.Get(NameID);


        public static IEnumerable<CharacteristicType> Characteristics = Enum.GetValues<CharacteristicCategory>().SelectMany(c => c.GetCharacs());
        public static StatFactory SimpleFactory = (id, value) => StatSimple.Create(id, value == null ? 0 : (int) value);
        public static StatFactory BoolFactory = (id, value) => StatBool.Create(id, value == null ? false : (bool) value);
        public static IEnumerable<T> iterate<T>() where T : CharacteristicType
        {
            var t = typeof(T);
            return t.GetFields()
                .Where(f => f.FieldType == t)
                .Select(f => (T) f.GetValue(null));
        }
    }

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

    public class CharacIdTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(int) || sourceType == typeof(string);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(int) || destinationType == typeof(string);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                string s => new CharacteristicId(int.Parse(s)),
                int s => new CharacteristicId(s),
                null => null,
                _ => throw new ArgumentException($"Cannot convert from {value} to CharacId", nameof(value))
            };
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return value switch
            {
                CharacteristicId id => destinationType == typeof(int) ? id.ID : id.ID.ToString(),
                null => null,
                _ => throw new ArgumentException($"Cannot convert {value} to int/string", nameof(value))
            };
        }
    }


    public static class CharacteristicIdExtentions
    {
        public static CharacteristicId GetAffinity(this ElementType stat) =>
            Affinity.values.Values.First(v => v.Element == stat).ID;
        public static CharacteristicId GetResistance(this ElementType stat) =>
            Resistance.values.Values.First(v => v.Element == stat).ID;
        public static IEnumerable<CharacteristicType> GetCharacs(this CharacteristicCategory cat) => cat switch
        {
            CharacteristicCategory.Resource     => Enumerable.OfType<CharacteristicType>(Resource.values.Values),
            CharacteristicCategory.Affinity     => Enumerable.OfType<CharacteristicType>(Affinity.values.Values),
            CharacteristicCategory.Resistance   => Enumerable.OfType<CharacteristicType>(Resistance.values.Values),

            CharacteristicCategory.Contextual   => Enumerable.OfType<CharacteristicType>(Contextual.values.Values),
            CharacteristicCategory.Other        => Enumerable.OfType<CharacteristicType>(OtherProperty.values.Values),
            CharacteristicCategory.State        => Enumerable.OfType<CharacteristicType>(SpellProperty.values.Values),

            CharacteristicCategory.SpellModel   => Enumerable.Concat<CharacteristicType>(SpellModelProperty.values.Values, SpellModelState.values.Values),
            CharacteristicCategory.Spell        => Enumerable.OfType<CharacteristicType>(SpellProperty.values.Values),
            CharacteristicCategory.Status       => Enumerable.OfType<CharacteristicType>(StatusModelProperty.values.Values),
            _ => throw new NotImplementedException(),
        };
        public static CharacteristicType GetCharactType(this CharacteristicId id) =>
            id.GetCategory().GetCharacs().FirstOrDefault(v => v.ID == id);
        public static IStat Create(this CharacteristicType type, object value = null) => type.Factory(type.ID, value);
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


}
