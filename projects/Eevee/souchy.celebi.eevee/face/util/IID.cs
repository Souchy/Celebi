using MongoDB.Bson;
using Newtonsoft.Json;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.serialization;
using System.ComponentModel;
using System.Globalization;

namespace souchy.celebi.eevee.face.util
{
    [Serializable]
    [JsonConverter(typeof(IIDJsonConverter))]
    [TypeConverter(typeof(IIDTypeConverter<IID>))]
    public record IID(string value)
    {
        public static readonly IID Zero = new IID("0");

        public override string ToString() => value;

        public static implicit operator string(IID iid) => iid.ToString();
        public static explicit operator IID(string str) => new IID(str);

        // int conversion is only for UidGenerator. MongoIDs wouldn't go through this
        public static implicit operator int(IID i) => int.Parse(i.value);
        public static explicit operator IID(int i) => new IID(i.ToString());
    }

    [TypeConverter(typeof(IIDTypeConverter<StringIID>))]
    public sealed record StringIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<SpellIID>))]
    public sealed record SpellIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<StatusIID>))]
    public sealed record StatusIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<CreatureIID>))]
    public sealed record CreatureIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<AnimationSetIID>))]
    public sealed record AnimationSetIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<AnimationIID>))]
    public sealed record AnimationIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<SceneIID>))]
    public sealed record SceneIID(string value) : IID(value) { }

    [TypeConverter(typeof(IIDTypeConverter<AssetIID>))]
    public sealed record AssetIID(string value) : IID(value) { }

    public class IIDTypeConverter<T> : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(string);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                string s => Activator.CreateInstance(typeof(T), s),
                null => null,
                _ => throw new ArgumentException($"Cannot convert from {value} to IID", nameof(value))
            };
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if(typeof(T).IsAssignableFrom(value.GetType()))
                {
                    return value.ToString();
                }
            }
            throw new ArgumentException($"Cannot convert {value ?? "(null)"} to {destinationType}", nameof(destinationType));
        }

    }
}
