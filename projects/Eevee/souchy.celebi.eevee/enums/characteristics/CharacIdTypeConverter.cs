using System.ComponentModel;
using System.Globalization;

namespace souchy.celebi.eevee.enums.characteristics
{
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


}
