using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
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
        public T Get<T>(CharacteristicType stat) where T : IStat => Get<T>(stat.ID);
        public T Get<T>(CharacteristicId statId) where T : IStat => (T) Get(statId);

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

        //IEntityDictionary<CharacteristicId, IStat>
        public IStats copy(bool anonymous = false)
        {
            var c = new Stats();
            //growth.copy();
            foreach (var s in Pairs)
                c.@base.Set(s.Key, s.Value.copy(anonymous));
            foreach (var s in growth.Pairs)
                c.growth.Set(s.Key, s.Value.copy());
            return c;
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