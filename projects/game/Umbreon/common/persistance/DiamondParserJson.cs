using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.impl.util;
using souchy.celebi.eevee.face.util;
using FileAccess = Godot.FileAccess;
using static souchy.celebi.umbreon.common.persistance.DiamondParser;
using souchy.celebi.eevee.enums.characteristics;
using MongoDB.Bson;
using MongoDB.Driver;
using souchy.celebi.eevee;

namespace souchy.celebi.umbreon.common.persistance
{
    internal class DiamondPersistanceJson : IDiamondPersistance
    {

        #region Diamond Models Saver on event handler
        [Subscribe(nameof(IEntityDictionary<ObjectId, ObjectId>.Add), nameof(IEntityDictionary<ObjectId, ObjectId>.Set))]
        public void onAddSet(object dic, ObjectId id, IEntity obj)
        {
            //GD.Print($"DiamondParser.onAddSet: {obj} ");
            obj.GetEntityBus().subscribe(this, nameof(onSave));
            save(dic, getFileName(obj));
        }
        [Subscribe(nameof(IEntityDictionary<ObjectId, ObjectId>.Remove))]
        public void onRemove(object dic, ObjectId id, IEntity obj)
        {
            //GD.Print($"DiamondParser.onRemove: {obj} ");
            obj.GetEntityBus().unsubscribe(this, nameof(onSave));
            save(dic, getFileName(obj));
        }
        [Subscribe("", IEventBus.save)]
        public void onSave(IEntity e) => save(getDictionaryForEntity(e), getFileName(e));
        #endregion


        #region Save/Load
        public string getFileName(Type objType)
        {
            if (typeof(IStringEntity).IsAssignableFrom(objType))
                return "i18n_" + Enum.GetName(i18nType);
            foreach (var t in typeToDictionaryConversion.Keys)
                if (t.IsAssignableFrom(objType))
                    return t.Name; 
            return null;
        }
        public string getFileName(IEntity obj) => getFileName(obj.GetType());
        public string getFileName<T>() => getFileName(typeof(T));
        public void save(object dic, string fileName)
        {
            Debouncer.debounce($"{nameof(DiamondParser)}.{nameof(save)}:{fileName}", () => _save(dic, fileName));
        }
        public async void _save(object dic, string fileName)
        {
            GD.Print($"Parser.save: {fileName}");
            var str = JsonConvert.SerializeObject(dic, jsonSettings);
            using FileAccess file = FileAccess.Open($"res://data/test/{fileName}.json", FileAccess.ModeFlags.Write);
            file.StoreString(str);
            file.Flush();
        }
        public IEntityDictionary<ObjectId, V> load<V>(IEntityDictionary<ObjectId, V> dic, string filename = "") where V : IEntity
        {
            if (filename == "") filename = getFileName<V>();
            string filepath = $"res://data/test/{filename}.json";
            if (!FileAccess.FileExists(filepath))
                return null;

            using FileAccess file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            if (file == null)
                GD.Print($"Parser.load null: {filepath}");
            var json = file.GetAsText();
            IEntityDictionary<ObjectId, V> data = JsonConvert.DeserializeObject<EntityDictionary<ObjectId, V>>(json, jsonSettings);
            if (data == null)
                data = EntityDictionary<ObjectId, V>.Create();
            GD.Print($"Parser.load: {filepath} = {data.Keys.Count()} count");

            // restore entity ids from keys
            if (typeof(IEntity).IsAssignableFrom(typeof(V)))
                data.ForEach((k, v) => ((IEntity) v).entityUid = k);
            // register IDs to buses + subscribe to save events
            data.ForEach((k, v) =>
            {
                //Eevee.RegisterIID<V>(k);
                Eevee.RegisterEventBus(v);
                v.GetEntityBus().subscribe(this, nameof(onSave));
            });
            // add all data to Eevee.models dic
            dic.AddAll(data);

            return data;
        }
        public void loadCharacteristics(string filename = "")
        {
            if (filename == "") filename = getFileName<CharacteristicType>();
            string filepath = $"res://data/test/{filename}.json";
            if (!FileAccess.FileExists(filepath))
                return;
            using FileAccess file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            if (file == null)
                GD.Print($"Parser.load null: {filepath}");
            var json = file.GetAsText();
            List<CharacteristicType> data = JsonConvert.DeserializeObject<List<CharacteristicType>>(json, jsonSettings);
            foreach(var ch in CharacteristicType.Characteristics)
            {
                var ct = data.Find(c => c.ID == ch.ID);
                if(ct == null)
                {
                    GD.Print($"Parser Characteristic missing data id {ch.ID}");
                    continue;
                }
                //ch.NameID = ct.NameID;
                //Eevee.RegisterIID<IStringEntity>(ch.NameID);
                ch.GetName().GetEntityBus().subscribe(this, nameof(onSave));
            }
        }
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "") where V : IEntityModel
        {
            if (filename == "") filename = getFileName<V>();
            string filepath = $"res://data/test/{filename}.json";
            if (!FileAccess.FileExists(filepath))
                return null;

            using FileAccess file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            if (file == null)
                GD.Print($"Parser.load null: {filepath}");
            var json = file.GetAsText();
            IEntityDictionary<IID, V> data = JsonConvert.DeserializeObject<EntityDictionary<IID, V>>(json, jsonSettings);
            if (data == null)
                data = EntityDictionary<IID, V>.Create();
            GD.Print($"Parser.load: {filepath} = {data.Keys.Count()} count");

            // restore entity ids from keys
            if (typeof(IEntity).IsAssignableFrom(typeof(V)))
                data.ForEach((k, v) => v.modelUid = k);
            // register IDs to buses + subscribe to save events
            data.ForEach((k, v) =>
            {
                //Eevee.RegisterIID<V>(k);
                Eevee.RegisterEventBus(v);
                v.GetEntityBus().subscribe(this, nameof(onSave));
            });
            // add all data to Eevee.models dic
            dic.AddAll(data);

            return data;
        }
        #endregion

    }
}
