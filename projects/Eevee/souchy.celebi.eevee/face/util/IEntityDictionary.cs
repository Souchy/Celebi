using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;

namespace souchy.celebi.eevee.face.util
{
    public interface IEntityDictionary<TKey, TValue> : IEntity
    {
        public IEnumerable<TKey> Keys { get; }
        public IEnumerable<TValue> Values { get; }
        public IEnumerable<KeyValuePair<TKey, TValue>> Pairs { get; }

        public bool Has(TKey key);
        public TValue Get(TKey key);
        public void Add(TKey key, TValue value);
        public void AddAll(IEntityDictionary<TKey, TValue> dictionary);
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
        public void ForEach(Action<TKey, TValue> action);
        public void Clear();
    }
}
