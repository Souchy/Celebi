using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStat : IEntity
    {

        public StatType StatType { get; init; }
        //public StatValueType valueType { get; }
        //public int get();
        //public int set(int value);

        public IStat copy();

    }

}
