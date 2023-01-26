using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.stats
{
    public interface IStats : IEntity
    {
        public Dictionary<StatType, IStat> stats { get; set; }


        public T get<T>(StatType statId) where T : IStat;

        public void set(StatType statId, IStat value);

    }
}
