using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.face.util
{
    public interface IEntityDictionary<TKey, TValue> : IEntity
    {
        public IEnumerable<TValue> Values { get; }
        public TValue Get(TKey key);
        public void Add(TKey key, TValue value);
        public void Set(TKey key, TValue value);
        public bool Remove(TKey key);
        public void Remove(Predicate<KeyValuePair<TKey, TValue>> predicate);
        public void Clear();
    }
}
