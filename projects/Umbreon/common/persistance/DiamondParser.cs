using Godot;
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
using Umbreon.common.persistance;
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

        internal static readonly Dictionary<Type, object> typeToDictionaryConversion = new Dictionary<Type, object>() {
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
        internal static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            Converters = new List<JsonConverter> { new IIDJsonConverter() }
        };
        internal readonly IDiamondPersistance persistance = new DiamondPersistanceJson(); //new DiamondParserMongo();


        public static I18NType i18nType { get; set; } = I18NType.fr;
        public CreatureModelData[] creatureModelsData;
        public CreatureSkinData[] creatureSkinsData;
        public MapModelData[] mapModelsData;

        public override void _Ready()
        {
            parseData();
            // FIXME Autosave for Vaporeon only
            Eevee.models.creatureModels.GetEntityBus().subscribe(persistance);
            Eevee.models.spellModels.GetEntityBus().subscribe(persistance);
            Eevee.models.statusModels.GetEntityBus().subscribe(persistance);
            Eevee.models.effectModels.GetEntityBus().subscribe(persistance);
            Eevee.models.effects.GetEntityBus().subscribe(persistance);
            Eevee.models.stats.GetEntityBus().subscribe(persistance);
            Eevee.models.i18n.GetEntityBus().subscribe(persistance); 

            Eevee.models.creatureSkins.GetEntityBus().subscribe(persistance);
            Eevee.models.spellSkins.GetEntityBus().subscribe(persistance);
            Eevee.models.effectSkins.GetEntityBus().subscribe(persistance);

            Eevee.models.maps.GetEntityBus().subscribe(persistance);
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
            persistance.load(Eevee.models.i18n); //, getFileName<IStringEntity>());
            persistance.load(Eevee.models.creatureSkins);
            persistance.load(Eevee.models.spellSkins);
            persistance.load(Eevee.models.effectSkins);
            persistance.load(Eevee.models.stats);
            
            persistance.load(Eevee.models.creatureModels);
            persistance.load(Eevee.models.statusModels);
            persistance.load(Eevee.models.spellModels);
            persistance.load(Eevee.models.effectModels);
            persistance.load(Eevee.models.effects);
            persistance.load(Eevee.models.maps);

            // Register individual stats
            foreach (var v in Eevee.models.stats.Values)
                foreach (var s in v.Values)
                    Eevee.RegisterIID<IStat>(s.entityUid);
        }

        internal static object getDictionaryForEntity(IEntity e)
        {
            foreach (var pair in typeToDictionaryConversion)
                if (pair.Key.IsAssignableFrom(e.GetType()))
                    return pair.Value;
            return null;
        }
        internal static Type getTypeForEntity(IEntity e)
        {
            foreach (var pair in typeToDictionaryConversion)
                if (pair.Key.IsAssignableFrom(e.GetType()))
                    return pair.Key;
            return null;
        }
    }


    public interface IDiamondPersistance
    {
        #region Diamond Models Saver on event handler
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Add), nameof(IEntityDictionary<IID, IID>.Set))]
        public void onAddSet(object dic, IID id, IEntity obj);
        [Subscribe(nameof(IEntityDictionary<IID, IID>.Remove))]
        public void onRemove(object dic, IID id, IEntity obj);
        [Subscribe("", IEventBus.save)]
        public void onSave(IEntity e);
        #endregion

        #region Save/Load
        public string getFileName(Type objType);
        public string getFileName(IEntity obj);
        public string getFileName<T>();
        public void save(object dic, string fileName);
        //public void _save(object dic, string fileName);
        public IEntityDictionary<IID, V> load<V>(IEntityDictionary<IID, V> dic, string filename = "") where V : IEntity;
        #endregion
    }


}
