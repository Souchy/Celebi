using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.properties;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.stats;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using System;
using System.Linq;
using System.Security.AccessControl;

namespace souchy.celebi.eevee.enums.characteristics
{

    public delegate IStat StatFactory(CharacteristicId id, object value = null);

    public record CharacteristicType(CharacteristicCategory Category, int LocalId, string BaseName, params ICondition[] conditions)
    {
        public StatValueType StatValueType { get; init; }
        public CharacteristicId ID { get; init; } = new CharacteristicId(((int) Category) * 1000 + LocalId);
        public IID nameModelUid { get; set; } = (IID) string.Join(".", nameof(CharacteristicType), Enum.GetName(Category), BaseName);
        [JsonIgnore]
        public StatFactory Factory { get; init; }

        public IStringEntity GetName() => Eevee.models.i18n.Values.FirstOrDefault(s => s.modelUid == nameModelUid); //i18n.Get(NameID);


        public static IEnumerable<CharacteristicType> Characteristics = Enum.GetValues<CharacteristicCategory>().SelectMany(c => c.GetCharacs());
        public static StatFactory SimpleFactory = (id, value) => StatSimple.Create(id, value == null ? 0 : (int) value);
        public static StatFactory BoolFactory = (id, value) => StatBool.Create(id, value == null ? false : (bool) value);
        public static StatFactory EntityStatDictionaryFactory = (id, value) 
            => EntityStatDictionary.Create(id, value == null ? new() : (Dictionary<ObjectId, IStat>) value);

        public static IEnumerable<T> iterate<T>() where T : CharacteristicType
        {
            var t = typeof(T);
            return t.GetFields()
                .Where(f => f.FieldType == t)
                .Select(f => (T) f.GetValue(null));
        }
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
    public enum StatValueType
    {
        Simple,
        Bool,
        EntityStatDictionary,
        List
    }
    public enum ElementType
    {
        None,
        Water,
        Fire,
        Earth,
        Air,
        /// <summary>
        /// True damage. Not sure if it's an element or just a EffectTrueDamage
        /// </summary>
        True
    }


}
