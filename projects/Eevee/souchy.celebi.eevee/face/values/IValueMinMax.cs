using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.values
{
    public interface IValueMinMax<T> : IValue<(T min, T max)>
    {

        public T min { get; init; }
        public T max { get; init; }

    }
}
