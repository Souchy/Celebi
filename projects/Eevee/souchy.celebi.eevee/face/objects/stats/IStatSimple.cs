using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStatSimple : IStat, IValue<int>
    {


        public static IStatSimple operator +(IStatSimple a, IValue<int> b) => new StatSimple(a.value + b.value);
        public static IStatSimple operator -(IStatSimple a, IValue<int> b) => new StatSimple(a.value - b.value);
        public static IStatSimple operator *(IStatSimple a, IValue<int> b) => new StatSimple(a.value * b.value);
        public static IStatSimple operator /(IStatSimple a, IValue<int> b) => new StatSimple(a.value / b.value);

        public static IStatSimple operator +(IValue<int> a, IStatSimple b) => new StatSimple(a.value + b.value);
        public static IStatSimple operator -(IValue<int> a, IStatSimple b) => new StatSimple(a.value - b.value);
        public static IStatSimple operator *(IValue<int> a, IStatSimple b) => new StatSimple(a.value * b.value);
        public static IStatSimple operator /(IValue<int> a, IStatSimple b) => new StatSimple(a.value / b.value);


        //public static IStatSimple operator +(IStatSimple a, int b) => new StatSimple(a.Value + b);
        //public static IStatSimple operator -(IStatSimple a, int b) => new StatSimple(a.Value - b);
        //public static IStatSimple operator *(IStatSimple a, int b) => new StatSimple(a.Value * b);
        //public static IStatSimple operator /(IStatSimple a, int b) => new StatSimple(a.Value / b);

    }
}
