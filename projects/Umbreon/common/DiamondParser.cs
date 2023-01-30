using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
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

        public override void _Ready()
        {
            parseData();
            // FIXME Autosave for Vaporeon only
            Eevee.models.creatureModels.GetEventBus().subscribe(this);
            Eevee.models.spellModels.GetEventBus().subscribe(this);
            Eevee.models.statusModels.GetEventBus().subscribe(this);
            Eevee.models.effects.GetEventBus().subscribe(this); // , nameof(onAddModel), nameof(onSetModel), nameof(onRemoveModel));
            Eevee.models.i18n.GetEventBus().subscribe(this, nameof(onAddI18n), nameof(onSetI18n), nameof(onRemoveI18n));
        }

        public void parseData()
        {
            var creatureModelsText = FileAccess.Open("res://data/creatures.json", FileAccess.ModeFlags.Read).GetAsText();
            creatureModelsData = JsonConvert.DeserializeObject<CreatureModelData[]>(creatureModelsText);
            var creatureSkinsText = FileAccess.Open("res://data/skins.json", FileAccess.ModeFlags.Read).GetAsText();
            creatureSkinsData = JsonConvert.DeserializeObject<CreatureSkinData[]>(creatureSkinsText);
            var mapModelsDataText = FileAccess.Open("res://data/maps.json", FileAccess.ModeFlags.Read).GetAsText();
            mapModelsData = JsonConvert.DeserializeObject<MapModelData[]>(mapModelsDataText);


            //var tgas = Godot.FileAccess.Open("res://data/test/{fileName}.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            //var asd = JsonConvert.DeserializeObject<IEntityDictionary<IID, ICreatureModel>>(tgas);

            var creatureModels = load(Eevee.models.creatureModels);
            creatureModels.ForEach((k, v) => Eevee.RegisterIID(k));
            Eevee.models.creatureModels.AddAll(creatureModels);

            var spellModels = load(Eevee.models.spellModels);
            spellModels.ForEach((k, v) => Eevee.RegisterIID(k));
            Eevee.models.spellModels.AddAll(spellModels);

            var effects = load(Eevee.models.effects);
            effects.ForEach((k, v) => Eevee.RegisterIID(k));
            Eevee.models.effects.AddAll(effects);

            var statusModels = load(Eevee.models.statusModels);
            statusModels.ForEach((k, v) => Eevee.RegisterIID(k));
            Eevee.models.statusModels.AddAll(statusModels);

            var i18n = loadI18n();
            i18n.ForEach((k, v) => Eevee.RegisterIID(k));
            Eevee.models.i18n.AddAll(i18n);
        }

        #region Diamond Models saver
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveModel(object dic, IID id, object obj) => save(dic, getNameDic(obj));
        #endregion

        #region I18n saver
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddI18n(object dic, IID id, string obj) => save(dic, getNameI18n(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetI18n(object dic, IID id, string obj) => save(dic, getNameI18n(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveI18n(object dic, IID id, string obj) => save(dic, getNameI18n(i18nType));
        #endregion


        #region Save/Load
        private string getNameI18n(I18NType type) => "i18n_" + Enum.GetName(i18nType);
        private string getNameDic(object obj) => obj.GetType().Name + "s";
        public void save(object dic, string fileName)
        {
            var str = JsonConvert.SerializeObject(dic, Formatting.Indented);
            var file = FileAccess.Open($"res://data/test/{fileName}.json", FileAccess.ModeFlags.Write);
            file.StoreString(str);
            file.Flush();
        }
        public IEntityDictionary<IID, string> loadI18n()
        {
            string fileName = $"res://data/test/{getNameI18n(this.i18nType)}.json";
            var file = FileAccess.Open(fileName, FileAccess.ModeFlags.ReadWrite);
            var json = file.GetAsText();
            var data = JsonConvert.DeserializeObject<IEntityDictionary<IID, string>>(json);
            return data;
        }
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic)
        {
            string fileName = $"res://data/test/{typeof(V).Name.Substring(1)}s.json";
            var file = FileAccess.Open(fileName, FileAccess.ModeFlags.ReadWrite);
            var json = file.GetAsText();
            var data = JsonConvert.DeserializeObject<IEntityDictionary<IID, V>>(json);
            GD.Print($"Parser.load: {fileName} = {data}");

            return data;
        }
        #endregion

    }
}
