using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : IStats
    {
        public IID entityUid { get; set; } //= Eevee.RegisterIID();
        public IEntityDictionary<StatType, IStat> stats { get; init; } = new EntityDictionary<StatType, IStat>();


        public Stats() { }
        private Stats(IID id) => entityUid = id;
        public static IStats Create() => new Stats(Eevee.RegisterIID());


        public IStat get(StatType statId)
        {
            return stats.Get(statId);
        }
        public T get<T>(StatType statId) where T : IStat
        {
            return (T) stats.Get(statId);
        }
        public void Add(StatType statId, IStat value)
        {
            stats.Add(statId, value);
            this.GetEventBus().publish(Enum.GetName(statId), this, value);
        }
        public bool has(StatType statId)
        {
            return stats.Get(statId) != null;
        }


        public void Dispose()
        {
            stats.Clear();
            Eevee.DisposeIID(this);
        }

    }
}