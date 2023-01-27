using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : IStats
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IEntityDictionary<StatType, IStat> stats { get; init; } = new EntityDictionary<StatType, IStat>();

        public T get<T>(StatType statId) where T : IStat
        {
            return (T) stats.Get(statId);
        }

        public void set(StatType statId, IStat value)
        {
            stats.Set(statId, value);
            // nameof(Stats) + nameof(set) + 
            this.GetEventBus().publish(Enum.GetName(statId), this, value);
        }

        public void Dispose()
        {
            stats.Clear();
            Eevee.DisposeIID(this);
        }

    }
}