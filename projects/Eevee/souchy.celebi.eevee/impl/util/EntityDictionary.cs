using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using Swashbuckle.AspNetCore.Annotations;

namespace souchy.celebi.eevee.impl.util
{
    public class EntityDictionary<TKey, TValue> : IEntityDictionary<TKey, TValue> where TValue : IEntity
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

        protected EntityDictionary() { }
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

        public bool Has(TKey key)
        {
            return dic.ContainsKey(key);
        }

        public void Set(TKey key, TValue value)
        {
            dic[key] = value;
            this.GetEntityBus().publish(nameof(Set), this, key, value);
        }

        public void Add(TKey key, TValue value)
        {
            dic.Add(key, value);
            this.GetEntityBus().publish(nameof(Add), this, key, value); 
        }
        public void AddAll(IEntityDictionary<TKey, TValue> dictionary)
        {
            foreach(var pair in dictionary.Pairs)
                Add(pair.Key, pair.Value);
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
            foreach (var k in Keys.ToList())
                Remove(k);
        }

        public void ForEach(Action<TValue> action)
        {
            foreach (var v in Values)
                action(v);
        }

        public void ForEach(Action<TKey, TValue> action)
        {
            foreach(var pair in dic)
                action(pair.Key, pair.Value);
        }

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            this.Clear();
        }

    }
}
