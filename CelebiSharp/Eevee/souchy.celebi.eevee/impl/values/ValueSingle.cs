using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.values
{
    public class ValueSingle<T> : IValueSingle<T>
    {
        public T Value { get; set; }
    }
}