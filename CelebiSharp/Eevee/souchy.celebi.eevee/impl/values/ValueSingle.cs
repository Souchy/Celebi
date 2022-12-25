using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.values
{
    public class ValueSingle<T> : IValueSingle<T>
    {
        public T value { get; set; }

        public T get() => value;
        public void set(T val) => value = val;
    }
}