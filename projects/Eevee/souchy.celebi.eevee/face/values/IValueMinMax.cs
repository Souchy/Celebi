using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.values
{
    public interface IValueMinMax<T> : IValue<(T min, T max)>
    {

        public T min { get; set; }
        public T max { get; set; }

    }
}
