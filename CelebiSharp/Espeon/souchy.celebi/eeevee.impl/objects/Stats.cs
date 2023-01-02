using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.stats;

namespace Espeon.souchy.celebi.eeevee.impl.objects
{
    public class Stats : IStats
    {
        public Dictionary<StatType, IStat> stats { get; set; } = new Dictionary<StatType, IStat>();

        public T get<T>(StatType statId) where T : IStat
        {
            return (T) stats[statId];
        }

        public void set(StatType statId, IStat value)
        {
            stats[statId] = value;
        }

        public void Dispose()
        {
            stats.Clear();
        }
    }
}
