using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.util;
using Umbreon.data;

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
            Eevee.models.creatureModels.GetEventBus().subscribe(this);
            Eevee.models.spellModels.GetEventBus().subscribe(this);
            Eevee.models.statusModels.GetEventBus().subscribe(this);
            Eevee.models.effects.GetEventBus().subscribe(this); // , nameof(onAddModel), nameof(onSetModel), nameof(onRemoveModel));
            Eevee.models.i18n.GetEventBus().subscribe(this, nameof(onAddI18n), nameof(onSetI18n), nameof(onRemoveI18n));
            parseData();
        }

        public void parseData()
        {
            var creatureModelsText = Godot.FileAccess.Open("res://data/creatures.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            creatureModelsData = JsonConvert.DeserializeObject<CreatureModelData[]>(creatureModelsText);
            var creatureSkinsText = Godot.FileAccess.Open("res://data/skins.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            creatureSkinsData = JsonConvert.DeserializeObject<CreatureSkinData[]>(creatureSkinsText);
            var mapModelsDataText = Godot.FileAccess.Open("res://data/maps.json", Godot.FileAccess.ModeFlags.Read).GetAsText();
            mapModelsData = JsonConvert.DeserializeObject<MapModelData[]>(mapModelsDataText);
        }

        #region Diamond Models saver
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddModel(object dic, IID id, object obj) => save(dic, obj.GetType().Name + "s");
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetModel(object dic, IID id, object obj) => save(dic, obj.GetType().Name + "s");
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveModel(object dic, IID id, object obj) => save(dic, obj.GetType().Name + "s");
        #endregion

        #region I18n saver
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add))]
        public void onAddI18n(object dic, IID id, string obj) => save(dic, "i18n_" + Enum.GetName(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Set))]
        public void onSetI18n(object dic, IID id, string obj) => save(dic, "i18n_" + Enum.GetName(i18nType));
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemoveI18n(object dic, IID id, string obj) => save(dic, "i18n_" + Enum.GetName(i18nType));
        #endregion

        #region Save
        public void save(object dic, string fileName)
        {
            //var type = obj.GetType();
            var str = JsonConvert.SerializeObject(dic, Formatting.Indented);
            var file = Godot.FileAccess.Open($"res://data/test/{fileName}.json", Godot.FileAccess.ModeFlags.Write);
            file.StoreString(str);
            file.Flush();
        }
        #endregion

    }
}
