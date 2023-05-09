using Godot;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.face.util;
using MongoDB.Driver;
using static souchy.celebi.umbreon.common.persistance.DiamondParser;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.shared.models;
using MongoDB.Bson;
using souchy.celebi.eevee;

namespace souchy.celebi.umbreon.common.persistance
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

        private IMongoCollection<T> GetCollection<T>() //where T : IEntity
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
        [Subscribe(nameof(IEntityDictionary<ObjectId, ObjectId>.Add), nameof(IEntityDictionary<ObjectId, ObjectId>.Set))]
        public void onAddSet(object dic, ObjectId id, IEntity obj)
        {
            obj.GetEntityBus().subscribe(this, nameof(onSave));
            save(obj);
        }
        [Subscribe(nameof(IEntityDictionary<ObjectId, ObjectId>.Remove))]
        public void onRemove(object dic, ObjectId id, IEntity obj)
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
        public IEntityDictionary<ObjectId, V> load<V>(IEntityDictionary<ObjectId, V> dic, string filename = "") where V : IEntity
        {
            try
            {
                var collection = GetCollection<V>();
                var list = collection.Find(Builders<V>.Filter.Empty)
                    .ToList();
                foreach (var v in list)
                {
                    dic.Add(v.entityUid, v);
                    //Eevee.RegisterIID<V>(v.entityUid);
                    Eevee.RegisterEventBus(v);
                    v.GetEntityBus().subscribe(this, nameof(onSave));
                }
            }
            catch (Exception e)
            {
                GD.PrintErr(e);
            }
            return dic;
        }

        public void loadCharacteristics(string filename = "")
        {
            try
            {
                var collection = GetCollection<CharacteristicType>();
                var list = collection.Find(Builders<CharacteristicType>.Filter.Empty)
                    .ToList();
                foreach (var ch in CharacteristicType.Characteristics)
                {
                    var ct = list.Find(c => c.ID == ch.ID);
                    if (ct == null)
                    {
                        GD.Print($"Parser Characteristic missing data id {ch.ID}");
                        continue;
                    }
                    //ch.NameID = ct.NameID;
                    //Eevee.RegisterIID<IStringEntity>(ch.NameID);
                    ch.GetName().GetEntityBus().subscribe(this, nameof(onSave));
                }
            }
            catch (Exception e)
            {
                GD.PrintErr(e);
            }
        }

        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "") where V : IEntityModel
        {
            try
            {
                var collection = GetCollection<V>();
                var list = collection.Find(Builders<V>.Filter.Empty)
                    .ToList();
                foreach (var v in list)
                {
                    dic.Add(v.modelUid, v);
                    //Eevee.RegisterIID<V>(v.entityUid);
                    Eevee.RegisterEventBus(v);
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
