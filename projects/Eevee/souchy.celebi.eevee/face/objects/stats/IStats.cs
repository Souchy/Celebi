using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity, IEntityDictionary<StatType, IStat>
    {
        public T Get<T>(StatType statId) where T : IStat;
        public void Add(IStat value);
    }
}
