using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.util
{
    public interface IEntityDictionary<TKey, TValue> : IEntity
    {
        public IEnumerable<TValue> Values { get; }
        public TValue Get(TKey key);
        public void Add(TKey key, TValue value);
        public void Set(TKey key, TValue value);
        /// <summary>
        /// Remove pair and Dispose value if possible
        /// </summary>
        public bool Remove(TKey key);
        /// <summary>
        /// Remove pair and Dispose value if possible
        /// </summary>
        public void Remove(Predicate<TValue> predicate);
        public void ForEach(Action<TValue> action);
        public void Clear();
    }
}
