using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStat : IEntity
    {

        public StatType statId { get; init; }

        public IStat copy();

    }

}
