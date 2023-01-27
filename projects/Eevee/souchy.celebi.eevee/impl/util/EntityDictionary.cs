using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.util
{
    public class EntityDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IEntityDictionary<TKey, TValue>
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();

        IEnumerable<TValue> IEntityDictionary<TKey, TValue>.Values => Values;

        public TValue Get(TKey key)
        {
            return this[key];
        }

        public void Set(TKey key, TValue value)
        {
            this[key] = value;
            this.GetEventBus().publish(nameof(Set), this, key, value); // this.GetType().Name + 
        }

        public void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            this.GetEventBus().publish(nameof(Add), this, key, value); // this.GetType().Name + 
        }

        public bool Remove(TKey key)
        {
            TValue? val;
            base.TryGetValue(key, out val);
            try
            {
                var result = base.Remove(key);
                return result;
            } finally
            {
                this.GetEventBus().publish(nameof(Remove), this, key, val); // this.GetType().Name + 
            }
        }

        public void Remove(Predicate<KeyValuePair<TKey, TValue>> predicate)
        {
            var toRemove = this.Where(p => predicate(p)).ToList();
            foreach(var rem in toRemove)
            {
                this.Remove(rem.Key);
            }
        }

        public void Dispose()
        {
            this.Clear();
            Eevee.DisposeIID(this);
        }

    }
}
