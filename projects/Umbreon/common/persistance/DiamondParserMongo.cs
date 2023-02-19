using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using static Umbreon.common.DiamondParser;
using MongoDB.Driver;

namespace Umbreon.common.persistance
{
    /// <summary>
    /// This save/load system is moreso based on individual entities instead of saving a whole dictionary for every small change
    /// </summary>
    internal class DiamondPersistanceMongo : IDiamondPersistance
    {
        // Connection URI
        private const string connectionUri = "mongodb://localhost:27017";
        // Create a new client and connect to the server
        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        public DiamondPersistanceMongo()
        {
            try
            {
                client = new MongoClient(connectionUri);
                db = client.GetDatabase("Celebi");
                foreach(var pair in typeToDictionaryConversion.DistinctBy(p => p.Value))
                {
                    db.CreateCollection(pair.Key.Name);
                }
            } 
            catch (Exception e)
            {
                GD.PrintErr(e);
            }
        }

        public IMongoCollection<T> GetCollection<T>() where T : IEntity
        {
            return db.GetCollection<T>(typeof(T).Name);
        }
        public IMongoCollection<IEntity> GetCollectionByType(Type type)
        {
            return (IMongoCollection<IEntity>) typeof(DiamondPersistanceMongo)
                .GetMethod(nameof(GetCollection))
                .MakeGenericMethod(type)
                .Invoke(this, null);
        }

        #region Diamond Models Saver on event handler
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add), nameof(IEntityDictionary<IID, IID>.Set))]
        public void onAddSet(object dic, IID id, IEntity obj)
        {
            obj.GetEntityBus().subscribe(this, nameof(onSave));
            save(obj);
        }
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemove(object dic, IID id, IEntity obj)
        {
            obj.GetEntityBus().unsubscribe(this, nameof(onSave));
            save(obj);
        }
        [Subscribe("", IEventBus.save)]
        public void onSave(IEntity e) => save(e);
        #endregion

        #region Save/Load
        public string getFileName(Type objType) => "";
        public string getFileName(IEntity obj) => "";
        public string getFileName<T>() => "";
        public void save(object dic, string fileName = "")
        {
            Debouncer.debounce($"{nameof(DiamondParser)}.{nameof(save)}:{fileName}", () => _save(dic, fileName));
        }
        private async void _save(object obj, string fileName)
        {
            var entity = (IEntity) obj;
            var type = getTypeForEntity(entity);
            var collection = GetCollectionByType(type);
            var filter = Builders<IEntity>.Filter.Eq(e => e.entityUid, entity.entityUid);
            var options = new ReplaceOptions()
            {
                IsUpsert = true,
            };
            collection.ReplaceOne(filter, entity, options);
            GD.Print($"Parser.save: {fileName}");
        }
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "") where V : IEntity
        {
            try
            {
                var collection = GetCollection<V>();
                var asd = collection.Find(Builders<V>.Filter.Empty)
                    .ToList();
                foreach (var v in asd)
                {
                    dic.Add(v.entityUid, v);
                    Eevee.RegisterIID<V>(v.entityUid);
                    v.GetEntityBus().subscribe(this, nameof(onSave));
                }
            }
            catch (Exception e)
            {
                GD.PrintErr(e);
            }
            return dic;
        }
        #endregion

    }
}
