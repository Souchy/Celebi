using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class EntityDictionary<TKey, TValue> : IEntityDictionary<TKey, TValue> // where TValue : IEntity
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        [JsonIgnore]
        public IEnumerable<TValue> Values => dic.Values;
        [JsonIgnore]
        public IEnumerable<TKey> Keys => dic.Keys;
        [JsonIgnore]
        public IEnumerable<KeyValuePair<TKey, TValue>> Pairs => dic;

        [JsonProperty(TypeNameHandling = TypeNameHandling.None)]
        [BsonDictionaryOptions(Representation = DictionaryRepresentation.Document)]
        protected Dictionary<TKey, TValue> dic { get; init; } = new();

        private EntityDictionary() { }
        public static IEntityDictionary<TKey, TValue> Create() => new EntityDictionary<TKey, TValue>() { 
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public TValue Get(TKey key)
        {
            if (dic.ContainsKey(key))
                return dic[key];
            else 
                return default;
        }
        public T Get<T>(TKey key) where T : TValue
        {
            return (T) Get(key);
        }

        public bool Has(TKey key)
        {
            return dic.ContainsKey(key);
        }

        public IEntityDictionary<TKey, TValue> Set(TKey key, TValue value)
        {
            dic[key] = value;
            this.GetEntityBus().publish(nameof(Set), this, key, value);
            return this;
        }

        public IEntityDictionary<TKey, TValue> Add(TKey key, TValue value)
        {
            dic.Add(key, value);
            this.GetEntityBus().publish(nameof(Add), this, key, value);
            return this;
        }
        public IEntityDictionary<TKey, TValue> AddAll(IEntityDictionary<TKey, TValue> dictionary)
        {
            dictionary.ForEach((key, value) => this.Add(key, value));
            return this;
        }

        public bool Remove(TKey key)
        {
            TValue? val;
            dic.TryGetValue(key, out val);
            try
            {
                var result = dic.Remove(key);
                return result;
            } 
            finally
            {
                this.GetEntityBus().publish(nameof(Remove), this, key, val);
                if (val is IDisposable dis)
                    dis.Dispose();
            }
        }

        public void Remove(Predicate<TValue> predicate)
        {
            var toRemove = dic.Where(p => predicate(p.Value)).ToList();
            foreach(var rem in toRemove)
                this.Remove(rem.Key);
        }

        public void Clear()
        {
            foreach (var k in this.dic.Keys.ToList())
                Remove(k);
        }

        public IEntityDictionary<TKey, TValue> ForEach(Action<TValue> action)
        {
            foreach (var v in this.dic.Values)
                action(v);
            return this;
        }

        public IEntityDictionary<TKey, TValue> ForEach(Action<TKey, TValue> action)
        {
            foreach(var pair in dic)
                action(pair.Key, pair.Value);
            return this;
        }

        public IEntityDictionary<TKey, TValue> copy(bool anonymous = true)
        {
            var copy = anonymous ? new EntityDictionary<TKey, TValue>() : EntityDictionary<TKey, TValue>.Create();
            foreach (var p in Pairs)
                copy.Add(p.Key, p.Value);
            return copy;
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            this.Clear();
        }

    }
}
