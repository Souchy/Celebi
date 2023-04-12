using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity, IEntityDictionary<CharacteristicId, IStat>
    {
        public void Add(IStat value);
        public T Get<T>(CharacteristicId statId) where T : IStat;
        public T Get<T>(CharacteristicType stat) where T : IStat => Get<T>(stat.ID);
        public void Set(IStat value) => Set(value.statId, value);
    }
}
