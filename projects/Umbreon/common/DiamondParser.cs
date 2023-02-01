using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.shared.models;
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
            Eevee.models.effects.GetEntityBus().subscribe(this); // , nameof(onAddModel), nameof(onSetModel), nameof(onRemoveModel));
            Eevee.models.i18n.GetEntityBus().subscribe(this, nameof(onAddI18n), nameof(onSetI18n), nameof(onRemoveI18n));
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
            var i18n = load(Eevee.models.i18n, getNameI18n(i18nType));  //loadI18n();
            var creatureModels = load(Eevee.models.creatureModels);
            var spellModels = load(Eevee.models.spellModels);
            var effects = load(Eevee.models.effects);
            var statusModels = load(Eevee.models.statusModels);
        }

        #region Diamond Models Saver on event handler
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        #endregion

        #region I18n Saver on event handler
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddI18n(object dic, IID id, IStringEntity obj) => save(dic, getNameI18n(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetI18n(object dic, IID id, IStringEntity obj) => save(dic, getNameI18n(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveI18n(object dic, IID id, IStringEntity obj) => save(dic, getNameI18n(i18nType));
        #endregion


        #region Save/Load
        private string getNameI18n(I18NType type) => "i18n_" + Enum.GetName(i18nType);
        private string getNameDic(object obj) => obj.GetType().Name + "s";
        public void save(object dic, string fileName)
        {
            GD.Print($"Parser.save: {fileName} = {dic}");
            var str = JsonConvert.SerializeObject(dic, jsonSettings);
            using FileAccess file = FileAccess.Open($"res://data/test/{fileName}.json", FileAccess.ModeFlags.Write);
            file.StoreString(str);
            file.Flush();
        }
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "")
        {
            if (filename == "")
                filename = typeof(V).Name.Substring(1) + "s";
            string filepath = $"res://data/test/{filename}.json";
            if (!FileAccess.FileExists(filepath))
                return null;

            using FileAccess file = FileAccess.Open(filepath, FileAccess.ModeFlags.Read);
            if(file == null)
                GD.Print($"Parser.load null: {filepath}");
            var json = file.GetAsText();
            var data = JsonConvert.DeserializeObject<IEntityDictionary<IID, V>>(json, jsonSettings);
            GD.Print($"Parser.load: {filepath} = {data}");

            // restore entity ids from keys
            if (typeof(IEntity).IsAssignableFrom(typeof(V)))
                data.ForEach((k, v) => ((IEntity) v).entityUid = k);
            // register IDs to buses
            data.ForEach((k, v) => Eevee.RegisterIID<V>(k));
            // add all data to Eevee.models dic
            dic.AddAll(data);

            return data;
        }
        #endregion

    }
}
