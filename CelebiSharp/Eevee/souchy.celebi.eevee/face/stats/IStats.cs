namespace souchy.celebi.eevee.face.stats
{
    public interface IStats
    {

        protected Dictionary<int, IStat> stats { get; set; }


        public T get<T>(int statId) where T : IStat;

        public void set(int statId, IStat value);

    }
}
