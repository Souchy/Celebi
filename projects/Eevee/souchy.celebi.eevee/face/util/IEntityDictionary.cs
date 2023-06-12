using MongoDB.Bson.Serialization;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
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
        public T Get<T>(TKey key) where T : TValue;
        public IEntityDictionary<TKey, TValue> Add(TKey key, TValue value);
        public IEntityDictionary<TKey, TValue> AddAll(IEntityDictionary<TKey, TValue> dictionary);
        public IEntityDictionary<TKey, TValue> AddAll(Dictionary<TKey, TValue> dictionary);
        public IEntityDictionary<TKey, TValue> Set(TKey key, TValue value);
        /// <summary>
        /// Remove pair and Dispose value if possible
        /// </summary>
        public bool Remove(TKey key);
        /// <summary>
        /// Remove pair and Dispose value if possible
        /// </summary>
        public void Remove(Predicate<TValue> predicate);
        public IEntityDictionary<TKey, TValue> ForEach(Action<TValue> action);
        public IEntityDictionary<TKey, TValue> ForEach(Action<TKey, TValue> action);
        public void Clear();

        public void serialize(BsonSerializationContext context);
    }
}
