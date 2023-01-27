using Godot;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.shared;
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

        public override void _Ready()
        {
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

    }
}
