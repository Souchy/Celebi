using MongoDB.Bson;
using Newtonsoft.Json;
using souchy.celebi.eevee.impl.util;
using System.ComponentModel;
using System.Globalization;

namespace souchy.celebi.eevee.face.util
{
    [Serializable]
    [JsonConverter(typeof(IIDJsonConverter))]
    [TypeConverter(typeof(IIDTypeConverter))]
    public sealed record IID(string value)
    {
        public override string ToString() => value;

        public static implicit operator string(IID iid) => iid.ToString();
        public static explicit operator IID(string str) => new IID(str);

        // int conversion is only for UidGenerator. MongoIDs wouldn't go through this
        public static implicit operator int(IID i) => int.Parse(i.value);
        public static explicit operator IID(int i) => new IID(i.ToString());
    }

    public class IIDTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) =>
            sourceType == typeof(string);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) =>
            destinationType == typeof(string);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value switch
            {
                string s => new IID(s),
                null => null,
                _ => throw new ArgumentException($"Cannot convert from {value} to IID", nameof(value))
            };
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return value switch
                {
                    IID id => id.value,
                    null => null,
                    _ => throw new ArgumentException($"Cannot convert {value} to string", nameof(value))
                };
            }
            throw new ArgumentException($"Cannot convert {value ?? "(null)"} to {destinationType}", nameof(destinationType));
        }

    }
}
