using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using souchy.celebi.eevee.impl.util.serialization;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStats : IEntity, IEntityDictionary<CharacteristicId, IStat>
    {
        //[JsonConverter(typeof(IIDJsonConverter<StringIID>))]
        public EntityDictionary<CharacteristicId, IStat> @base { get; set; }

        //[JsonConverter(typeof(IIDJsonConverter<StringIID>))]
        public EntityDictionary<CharacteristicId, MathEquation> growth { get; set; }

        // todo?
        //public void applyGrowth();
        //public void applyRegen();

        public void Add(IStat value);
        public void Set(IStat value);
        public T Get<T>(CharacteristicType stat) where T : IStat;

        /// <summary>
        /// If anonymous: create an instance with new() without ObjectId and bus
        /// Otherwise: use Create() and register a new ObjectId
        /// </summary>
        public IStats copy(bool anonymous = false);
        public IStats copyToFight(ObjectId fightUid, IStats target = null);
        public IStats copyTo(IStats stats, bool anonymous = false);
    }
}
