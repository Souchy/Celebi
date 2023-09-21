using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.CodeAnalysis;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.impl.util.math;
using souchy.celebi.eevee.impl.util.serialization;
using System.ComponentModel;

namespace souchy.celebi.eevee.impl.stats
{
    public class Stats : IStats 
    {
        [BsonId]
        public ObjectId entityUid { get; set; }

        [BsonSerializer(typeof(EntityDictionarySerializer<CharacteristicId, IStat>))]
        public EntityDictionary<CharacteristicId, IStat> @base { get; set; } = (EntityDictionary<CharacteristicId, IStat>) EntityDictionary<CharacteristicId, IStat>.Create();

        [BsonSerializer(typeof(EntityDictionarySerializer<CharacteristicId, MathEquation>))]
        public EntityDictionary<CharacteristicId, MathEquation> growth { get; set; } = (EntityDictionary<CharacteristicId, MathEquation>) EntityDictionary<CharacteristicId, MathEquation>.Create();

        [JsonIgnore]
        public IEnumerable<CharacteristicId> Keys => @base.Keys;
        [JsonIgnore]
        public IEnumerable<IStat> Values => @base.Values;
        [JsonIgnore]
        public IEnumerable<KeyValuePair<CharacteristicId, IStat>> Pairs => @base.Pairs;
        
        protected Stats() { }
        public static IStats Create() => new Stats()
        {
            entityUid = Eevee.RegisterIIDTemporary()
        };

        public void Add(IStat value) => Add(value.statId, value);
        public void Set(IStat value) => Set(value.statId, value);
        public T? Get<T>(CharacteristicType charac, object defaultValue = default) where T : IStat => charac != null ? Get<T>(charac.ID, defaultValue) : default; //charac != null ? Get<T>(charac.ID) : createDefaultStat<T>(CharacteristicId.Default);
        public T Get<T>(CharacteristicId characId, object defaultValue = default) where T : IStat => (T) Get(characId) ?? createDefaultStat<T>(characId, defaultValue);
        public T Get<T>(CharacteristicId characId) where T : IStat => (T) Get(characId);

        public V GetValue<T, V>(CharacteristicType charac, V defaultValue = default) where T : IValue<V>
        {
            if(charac == null)
            {
                return defaultValue;
            }
            var stat = Get(charac.ID);
            if(stat == null) 
                return defaultValue;
            return (V) stat.genericValue;
        }

        private T createDefaultStat<T>(CharacteristicId statId, object defaultValue = default) where T : IStat
        {
            if(typeof(T) == typeof(IStatSimple))
            {
                T stat = (T) (IStat) StatSimple.Create(statId, (int) (defaultValue ?? 0));
                this.Add(stat);
                return stat;
            }
            if (typeof(T) == typeof(IStatBool))
            {
                T stat = (T) (IStat) StatBool.Create(statId, (bool) (defaultValue ?? false));
                this.Add(stat);
                return stat;
            }
            throw new NotImplementedException();
        }

        public void applyGrowth(int currentTurn)
        {
            foreach (var key in growth.Keys)
            {
                var grow = growth.Get(key);
                if(key.GetCharactType().StatValueType == StatValueType.Simple)
                {
                    var charac = Get<IStatSimple>(key);
                    charac.value += grow.getAsInt(currentTurn);
                }
                if (key.GetCharactType().StatValueType == StatValueType.Bool)
                {
                    var charac = Get<IStatBool>(key);
                    charac.value = grow.getAsBool(currentTurn, charac.value);
                }
            }
        }

        public IStats copy(bool anonymous = false)
        {
            if (anonymous)
                return copyTo(new Stats(), anonymous);
            else
                return copyTo(Stats.Create(), anonymous);
        }

        public IStats copyToFight(ObjectId fightUid, IStats target = null)
        {
            var copy = target != null ? copyTo(target) : this.copy(false);
            // add stats to fight
            Eevee.fights.Get(fightUid).stats.Add(copy.entityUid, copy);
            return copy;
        }
        public IStats copyTo(IStats stats, bool anonymous = false)
        {
            foreach (var p in Pairs.Where(p => p.Value != null))
                stats.@base.Set(p.Key, p.Value.copy(anonymous));
            foreach (var p in growth.Pairs)
                stats.growth.Set(p.Key, p.Value.copy());
            return stats;
        }

        public bool Has(CharacteristicId key) => @base.Has(key);
        public IStat Get(CharacteristicId key) => @base.Get(key);
        public IEntityDictionary<CharacteristicId, IStat> Add(CharacteristicId key, IStat value) => @base.Add(key, value);
        public IEntityDictionary<CharacteristicId, IStat> AddAll(IEntityDictionary<CharacteristicId, IStat> dictionary) => @base.AddAll(dictionary);
        public IEntityDictionary<CharacteristicId, IStat> AddAll(Dictionary<CharacteristicId, IStat> dictionary)
        {
            foreach (var pair in dictionary)
                Add(pair.Key, pair.Value);
            return this;
        }
        public IEntityDictionary<CharacteristicId, IStat> Set(CharacteristicId key, IStat value) => @base.Set(key, value);
        public bool Remove(CharacteristicId key) => @base.Remove(key);
        public void Remove(Predicate<IStat> predicate) => @base.Remove(predicate);  
        public IEntityDictionary<CharacteristicId, IStat> ForEach(Action<IStat> action) => @base.ForEach(action);
        public IEntityDictionary<CharacteristicId, IStat> ForEach(Action<CharacteristicId, IStat> action) => @base.ForEach(action);
        public void Clear() => @base.Clear();

        public void Dispose()
        {
            Eevee.DisposeEventBus(this);
            @base.Dispose();
            growth.Dispose();
        }

        public void serialize(BsonSerializationContext context)
        {
            // c'est voulu, on veut pas serializer stats en un dictionaire
            // on veut plutot insérer les 2 dictionnaires dedans (base, growth) 
            throw new NotImplementedException(); 
        }

    }
}