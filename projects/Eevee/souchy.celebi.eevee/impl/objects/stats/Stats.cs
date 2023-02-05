using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : EntityDictionary<StatType, IStat>, IStats
    {
        private Stats() { }
        private Stats(IID id) => entityUid = id;
        public static new IStats Create() => new Stats(Eevee.RegisterIID<IStats>());

        public T Get<T>(StatType statId) where T : IStat
        {
            return (T) Get(statId);
        }
        public void Add(IStat value) 
        {
            Add(value.statId, value);
        }

    }
}