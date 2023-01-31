using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity
    {
        public IEntityDictionary<StatType, IStat> stats { get; init; }


        public IStat get(StatType statId);
        public T get<T>(StatType statId) where T : IStat;
        public void Add(StatType statId, IStat value);
        public bool has(StatType statId);

    }
}
