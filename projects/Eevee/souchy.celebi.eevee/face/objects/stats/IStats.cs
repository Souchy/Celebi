using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util.math;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity, IEntityDictionary<CharacteristicId, IStat>
    {
        public IEntityDictionary<CharacteristicId, IStat> @base { get; set; }
        public IEntityDictionary<CharacteristicId, MathEquation> growth { get; set; }

        public void Add(IStat value);
        public void Set(IStat value);
        public T Get<T>(CharacteristicType stat) where T : IStat;
        //public T Get<T>(CharacteristicId statId) where T : IStat;

        /// <summary>
        /// If anonymous: create an instance with new() without ObjectId and bus
        /// Otherwise: use Create() and register a new ObjectId
        /// </summary>
        public IStats copy(bool anonymous = false);
    }
}
