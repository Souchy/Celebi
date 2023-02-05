using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.statuses;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.statuses;

namespace souchy.celebi.eevee.impl.util
{
    public class EntityDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEntityDictionary<TKey, TValue> where TValue : IEntity
    {
        public IID entityUid { get; set; } // = Eevee.RegisterIID();

        IEnumerable<TValue> IEntityDictionary<TKey, TValue>.Values => Values;
        IEnumerable<TKey> IEntityDictionary<TKey, TValue>.Keys => Keys;
        public IEnumerable<KeyValuePair<TKey, TValue>> Pairs => this;


        protected EntityDictionary() { }
        protected EntityDictionary(IID id) => entityUid = id;
        public static IEntityDictionary<TKey, TValue> Create() => new EntityDictionary<TKey, TValue>(Eevee.RegisterIID<IEntityDictionary<TKey, TValue>>());


        public TValue Get(TKey key)
        {
            if (ContainsKey(key))
                return this[key];
            else 
                return default;
        }

        public bool Has(TKey key)
        {
            return this.ContainsKey(key);
        }

        public void Set(TKey key, TValue value)
        {
            this[key] = value;
            this.GetEntityBus().publish(nameof(Set), this, key, value);
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            this.GetEntityBus().publish(nameof(Add), this, key, value); 
        }
        public void AddAll(IEntityDictionary<TKey, TValue> dictionary)
        {
            foreach(var pair in dictionary.Pairs)
                Add(pair.Key, pair.Value);
        }

        public new bool Remove(TKey key)
        {
            TValue? val;
            base.TryGetValue(key, out val);
            try
            {
                var result = base.Remove(key);
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
            var toRemove = this.Where(p => predicate(p.Value)).ToList();
            foreach(var rem in toRemove)
                this.Remove(rem.Key);
        }

        public new void Clear()
        {
            foreach (var k in Keys.ToList())
                Remove(k);
            //base.Clear();
        }

        public void ForEach(Action<TValue> action)
        {
            foreach (var v in Values)
                action(v);
        }

        public void ForEach(Action<TKey, TValue> action)
        {
            foreach(var pair in this)
                action(pair.Key, pair.Value);
        }

        public void Dispose()
        {
            this.Clear();
            Eevee.DisposeIID<IEntityDictionary<TKey, TValue>>(entityUid);
        }

    }
}
