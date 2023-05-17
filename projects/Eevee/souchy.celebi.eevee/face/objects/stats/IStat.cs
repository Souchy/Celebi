using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStat : IEntity
    {

        public CharacteristicId statId { get; init; }

        /// <summary>
        /// Anonymous meaning the entity will have no ObjectId
        /// </summary>
        public IStat copy(bool anonymous = false);

        public void Add(IStat s);

    }

}
