using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;
using System.Linq.Expressions;

namespace souchy.celebi.spark.services.models
{
    public class IDCounterService
    {
        private IMongoCollection<IDCounter> _counters;
        public IDCounterService(MongoModelsDbService service) => _counters = service.GetMongoCollection<IDCounter>();

        public async Task<IID> GetID<T>() where T : IEntityModel
        {
            var name = typeof(T).Name;
            var docFilter = Builders<IDCounter>.Filter.Eq(nameof(IDCounter.Name), name);
            var counter = await _counters.Find(docFilter).FirstOrDefaultAsync();

            if(counter == null)
            {
                counter = new IDCounter()
                {
                    Name = name,
                };
                await _counters.InsertOneAsync(counter);
            }

            if(counter.Unused.Count > 0)
            {
                var unused = counter.Unused.Pop();
                var update = Builders<IDCounter>.Update.Set(nameof(IDCounter.Unused), counter.Unused);
                await _counters.UpdateOneAsync(docFilter, update);
                return (IID) unused;
            } else
            {
                counter.Counter += 1;
                var update = Builders<IDCounter>.Update.Set(nameof(IDCounter.Counter), counter.Counter);
                await _counters.UpdateOneAsync(docFilter, update);
                return (IID) counter.Counter;
            }
        }
        public async Task<UpdateResult> UnuseID<T>(IID id) where T : IEntityModel
        {
            var name = typeof(T).Name;
            var docFilter = Builders<IDCounter>.Filter.Eq(nameof(IDCounter.Name), name);
            var counter = await _counters.Find(docFilter).FirstOrDefaultAsync();

            counter.Unused.Push(id);
            var update = Builders<IDCounter>.Update.Set(nameof(IDCounter.Unused), counter.Unused);
            return await _counters.UpdateOneAsync(docFilter, update);
        }
    }

    public class IDCounter
    {
        [BsonId]
        public string Name { get; set; }
        public int Counter { get; set; } = 1;
        public Stack<int> Unused { get; set; } = new();
    }

}
