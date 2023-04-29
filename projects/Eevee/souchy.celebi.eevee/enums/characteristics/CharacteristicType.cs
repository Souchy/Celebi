using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;
using System;
using System.Linq;
using System.Security.AccessControl;

namespace souchy.celebi.eevee.enums.characteristics
{

    public delegate IStat StatFactory(CharacteristicId id, object value = null);

    public record CharacteristicType(CharacteristicCategory Category, int LocalId, string BaseName, StatFactory Factory, params ICondition[] conditions)
    {
        public StatValueType StatValueType { get; init; }
        public CharacteristicId ID { get; init; } = new CharacteristicId(((int) Category) * 1000 + LocalId);
        public IID NameID { get; set; } = (IID) (nameof(CharacteristicType) + "." + BaseName);

        public IStringEntity GetName() => Eevee.models.i18n.Get(NameID);


        public static IEnumerable<CharacteristicType> Characteristics = Enum.GetValues<CharacteristicCategory>().SelectMany(c => c.GetCharacs());
        public static StatFactory SimpleFactory = (id, value) => StatSimple.Create(id, (int) value);
        public static StatFactory BoolFactory = (id, value) => StatBool.Create(id, (bool) value);
        public static IEnumerable<T> iterate<T>() where T : CharacteristicType
        {
            var t = typeof(T);
            return t.GetFields()
                .Where(f => f.FieldType == t)
                .Select(f => (T) f.GetValue(null));
        }
    }

    public readonly record struct CharacteristicId(int ID)
    {
        public static implicit operator int(CharacteristicId v) => v.ID;
        public CharacteristicCategory GetCategory()
        {
            int cat = (int) Math.Floor(this.ID / 1000d);
            return (CharacteristicCategory) cat;
        }
    }

    public static class CharacteristicIdExtentions
    {
        //public  static Type GetClassType(this CharacteristicId stat)
        //{
        //    var cat = stat.GetCategory();
        //    return cat switch
        //    {
        //        CharacteristicCategory.State => typeof(State),
        //        _ => throw new Exception()
        //    };
        //}
        //public static StatTypePropertiesAttribute GetProperties(this CharacteristicId statType)
        //{
        //    var attr = statType.GetType()
        //            .GetField(Enum.GetName(statType))
        //            .GetCustomAttribute(typeof(StatTypePropertiesAttribute), true);
        //    return (StatTypePropertiesAttribute) attr;
        //}
        //public static StatTypePropertiesAttribute GetProperties(this ResourceType statType) =>
        //    statType.GetStatType().GetProperties();
        public static CharacteristicId GetAffinity(this ElementType stat) =>
            Affinity.values.Values.First(v => v.Element == stat).ID;
        public static CharacteristicId GetResistance(this ElementType stat) =>
            Resistance.values.Values.First(v => v.Element == stat).ID;
        //public static CharacteristicId GetStatType(this CharacteristicId stat) =>
        //    Resistance.values.Values.First(v => v.ID == stat).ID;
            //(CharacteristicId) Enum.Parse(typeof(CharacteristicId), Enum.GetName(stat));
        public static IEnumerable<CharacteristicType> GetCharacs(this CharacteristicCategory cat) => cat switch
        {
            CharacteristicCategory.Resource     => Resource.values.Values,
            CharacteristicCategory.Affinity     => Affinity.values.Values,
            CharacteristicCategory.Resistance   => Resistance.values.Values,

            CharacteristicCategory.Contextual   => Contextual.values.Values,
            CharacteristicCategory.Other        => OtherProperty.values.Values,
            CharacteristicCategory.State        => SpellProperty.values.Values,

            CharacteristicCategory.SpellModel   => Enumerable.Concat<CharacteristicType>(SpellModelProperty.values.Values, SpellModelState.values.Values),
            CharacteristicCategory.Spell        => SpellProperty.values.Values,
            CharacteristicCategory.Status       => StatusModelProperty.values.Values,
            _ => throw new NotImplementedException(),
        };
        public static CharacteristicType GetCharactType(this CharacteristicId id) =>
            id.GetCategory().GetCharacs().FirstOrDefault(v => v.ID == id);
        public static IStat Create(this CharacteristicType type, object value = null) => type.Factory(type.ID, value);
        //public static IStat Create(this CharacteristicId type) =>
        //    type.GetProperties().valueType.Create(type);
        //public static void Create(this CharacteristicId id, StatValueType valueType) =>
        //    valueType switch
        //    {
        //        StatValueType.Simple => StatSimple.Create(st), //new StatSimple(st),
        //        StatValueType.Detailed => StatDetailed.Create(st), //new StatDetailed(st),
        //        StatValueType.Bool => StatBool.Create(st), //new StatBool(st),
        //        StatValueType.Resource => StatResource.Create(st), //new StatResource(st),
        //        _ => throw new Exception()
        //    };
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
