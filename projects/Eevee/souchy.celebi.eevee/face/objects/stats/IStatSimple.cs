using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStatSimple : IStat, IValue<int>
    {
        //public static readonly IStatSimple zero = StatSimple.c // maybe for DamageUtil.damage calculation

        //public static IStatSimple operator +(IStatSimple a, IValue<int> b) => StatSimple.Create(a.statId, a.value + b.value); 
        //public static IStatSimple operator -(IStatSimple a, IValue<int> b) => StatSimple.Create(a.statId, a.value - b.value);
        //public static IStatSimple operator *(IStatSimple a, IValue<int> b) => StatSimple.Create(a.statId, a.value * b.value);
        //public static IStatSimple operator /(IStatSimple a, IValue<int> b) => StatSimple.Create(a.statId, a.value / b.value);

        //public static IStatSimple operator +(IValue<int> a, IStatSimple b) => StatSimple.Create(a.statId, a.value + b.value);
        //public static IStatSimple operator -(IValue<int> a, IStatSimple b) => StatSimple.Create(a.statId, a.value - b.value);
        //public static IStatSimple operator *(IValue<int> a, IStatSimple b) => StatSimple.Create(a.statId, a.value * b.value);
        //public static IStatSimple operator /(IValue<int> a, IStatSimple b) => StatSimple.Create(a.statId, a.value / b.value);


        //public static IStatSimple operator +(IStatSimple a, int b) => StatSimple.Create(a.statId, a.value + b);
        //public static IStatSimple operator -(IStatSimple a, int b) => StatSimple.Create(a.statId, a.value - b);
        //public static IStatSimple operator *(IStatSimple a, int b) => StatSimple.Create(a.statId, a.value * b);
        //public static IStatSimple operator /(IStatSimple a, int b) => StatSimple.Create(a.statId, a.value / b);

    }
}
