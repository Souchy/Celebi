using souchy.celebi.eevee.enums;

namespace souchy.celebi.eevee.face.stats
{
    public interface IStats
    {

        protected Dictionary<StatType, IStat> stats { get; set; }


        public T get<T>(StatType statId) where T : IStat;

        public void set(StatType statId, IStat value);

    }
}
