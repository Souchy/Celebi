using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : IStats
    {
        public IID entityUid { get; init; }
        public Dictionary<StatType, IStat> stats { get; set; } = new Dictionary<StatType, IStat>();


        public T get<T>(StatType statId) where T : IStat
        {
            return (T)stats[statId];
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