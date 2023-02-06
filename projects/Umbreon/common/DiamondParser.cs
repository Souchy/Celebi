﻿using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using Umbreon.common.util;
using Umbreon.data;
using FileAccess = Godot.FileAccess;


namespace Umbreon.common
{

    public static class DiamondParserExtension
    {
        public static DiamondParser GetDiamondsParser(this Node node)
        {
            var nodePath = "/root/DiamondParser";
            var diamonds = node.GetNode<DiamondParser>(nodePath);
            return diamonds;
        }
    }

    public partial class DiamondParser : Node
    {

        private static readonly Dictionary<Type, object> typeToDictionaryConversion = new Dictionary<Type, object>() {
            { typeof(ICreatureSkin), Eevee.models.creatureSkins },
            { typeof(ISpellSkin), Eevee.models.spellSkins },
            { typeof(IEffectSkin), Eevee.models.effectSkins },
            { typeof(ICreatureModel), Eevee.models.creatureModels },
            { typeof(ISpellModel), Eevee.models.spellModels },
            { typeof(IStatusModel), Eevee.models.statusModels },
            { typeof(IEffectModel), Eevee.models.effectModels },
            { typeof(IEffect), Eevee.models.effects },
            { typeof(IStats), Eevee.models.stats },
            { typeof(IStat), Eevee.models.stats },
            { typeof(IMap), Eevee.models.maps },
            { typeof(IStringEntity), Eevee.models.i18n },
        };
        private static object getDictionaryForEntity(IEntity e)
        {
            foreach(var pair in typeToDictionaryConversion)
                if(pair.Key.IsAssignableFrom(e.GetType()))
                    return pair.Value;
            return null;
        }

        public CreatureModelData[] creatureModelsData;
        public CreatureSkinData[] creatureSkinsData;
        public MapModelData[] mapModelsData;

        public I18NType i18nType { get; set; } = I18NType.fr;

        
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            Converters = new List<JsonConverter> { new IIDJsonConverter() }
        };

        public override void _Ready()
        {
            parseData();
            // FIXME Autosave for Vaporeon only
            Eevee.models.creatureModels.GetEntityBus().subscribe(this);
            Eevee.models.spellModels.GetEntityBus().subscribe(this);
            Eevee.models.statusModels.GetEntityBus().subscribe(this);
            Eevee.models.effectModels.GetEntityBus().subscribe(this);
            Eevee.models.effects.GetEntityBus().subscribe(this); // , nameof(onAddModel), nameof(onSetModel), nameof(onRemoveModel));
            Eevee.models.stats.GetEntityBus().subscribe(this);
            Eevee.models.i18n.GetEntityBus().subscribe(this); //, nameof(onAddI18n), nameof(onRemoveI18n)); //  nameof(onSetI18n),

            Eevee.models.creatureSkins.GetEntityBus().subscribe(this);
            Eevee.models.spellSkins.GetEntityBus().subscribe(this);
            Eevee.models.effectSkins.GetEntityBus().subscribe(this);

            Eevee.models.maps.GetEntityBus().subscribe(this);
        }

        public void parseData()
        {
            var creatureModelsText = FileAccess.Open("res://data/creatures.json", FileAccess.ModeFlags.Read).GetAsText();
            creatureModelsData = JsonConvert.DeserializeObject<CreatureModelData[]>(creatureModelsText);
            var creatureSkinsText = FileAccess.Open("res://data/skins.json", FileAccess.ModeFlags.Read).GetAsText();
            creatureSkinsData = JsonConvert.DeserializeObject<CreatureSkinData[]>(creatureSkinsText);
            var mapModelsDataText = FileAccess.Open("res://data/maps.json", FileAccess.ModeFlags.Read).GetAsText();
            mapModelsData = JsonConvert.DeserializeObject<MapModelData[]>(mapModelsDataText);

            // Load and register entities
            load(Eevee.models.i18n); //, getFileName<IStringEntity>());
            load(Eevee.models.creatureSkins);
            load(Eevee.models.spellSkins);
            load(Eevee.models.effectSkins);
            load(Eevee.models.stats);

            load(Eevee.models.creatureModels);
            load(Eevee.models.statusModels);
            load(Eevee.models.spellModels);
            load(Eevee.models.effectModels);
            load(Eevee.models.effects);
            load(Eevee.models.maps);

            // Register individual stats
            foreach (var v in Eevee.models.stats.Values)
                foreach (var s in v.Values)
                    Eevee.RegisterIID<IStat>(s.entityUid);
        }


        #region Diamond Models Saver on event handler
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add), nameof(IEntityDictionary<IID, IID>.Set))]
        public void onAddSet(object dic, IID id, IEntity obj)
        {
            //GD.Print($"DiamondParser.onAddSet: {obj} ");
            obj.GetEntityBus().subscribe(this, nameof(onSave)); 
            save(dic, getFileName(obj));
        }
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemove(object dic, IID id, IEntity obj)
        {
            //GD.Print($"DiamondParser.onRemove: {obj} ");
            obj.GetEntityBus().unsubscribe(this, nameof(onSave));
            save(dic, getFileName(obj));
        }
        [Subscribe("", IEventBus.save)]
        public void onSave(IEntity e) => save(getDictionaryForEntity(e), getFileName(e));
        //if(e is IStringEntity) save(Eevee.models.i18n, getFileName<IStringEntity>());
        //[Subscribe]
        //public void onStatChange(StatType st, IStat s) => ;
        #endregion


        #region Save/Load
        private string getFileName(Type objType)
        {
            if (typeof(IStringEntity).IsAssignableFrom(objType))
                return "i18n_" + Enum.GetName(i18nType);
            foreach (var t in typeToDictionaryConversion.Keys)
                if (t.IsAssignableFrom(objType))
                    return t.Name; // t.Name.Substring(1) + "s";
            //objType.Name + "s";
            //typeof(V).Name.Substring(1) + "s";
            return null;
        }
        private string getFileName(IEntity obj) => getFileName(obj.GetType());
        private string getFileName<T>() => getFileName(typeof(T));
        public void save(object dic, string fileName)
        {
            Debouncer.debounce($"{nameof(DiamondParser)}.{nameof(save)}:{fileName}", () => _save(dic, fileName));
        }
        private void _save(object dic, string fileName)
        {
            GD.Print($"Parser.save: {fileName}");
            var str = JsonConvert.SerializeObject(dic, jsonSettings);
            using FileAccess file = FileAccess.Open($"res://data/test/{fileName}.json", FileAccess.ModeFlags.Write);
            file.StoreString(str);
            file.Flush();
        }
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "") where V : IEntity
        {
            if (filename == "") filename = getFileName<V>();
            string filepath = $"res://data/test/{filename}.json";
            if (!FileAccess.FileExists(filepath))
                return null;

            using FileAccess file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            if(file == null)
                GD.Print($"Parser.load null: {filepath}");
            var json = file.GetAsText();
            IEntityDictionary<IID, V> data = JsonConvert.DeserializeObject<EntityDictionary<IID, V>>(json, jsonSettings);
            if (data == null)
                data = EntityDictionary<IID, V>.Create();
            GD.Print($"Parser.load: {filepath} = {data.Keys.Count()} count");

            // restore entity ids from keys
            if (typeof(IEntity).IsAssignableFrom(typeof(V)))
                data.ForEach((k, v) => ((IEntity) v).entityUid = k);
            // register IDs to buses + subscribe to save events
            data.ForEach((k, v) =>
            {
                Eevee.RegisterIID<V>(k);
                v.GetEntityBus().subscribe(this, nameof(onSave));
            });
            // add all data to Eevee.models dic
            dic.AddAll(data);

            return data;
        }
        #endregion

    }
}
