using Godot;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System.Collections.Generic;
using CreatureData = souchy.celebi.eevee.impl.objects.CreatureInstance;

namespace Celebi.common
{

    public static class DiamondExtension
    {
        public static DiamondModels GetDiamonds(this Node node)
        {
            var nodePath = "/root/DiamondModels";
            var diamonds = node.GetNode<DiamondModels>(nodePath);
            return diamonds;
        }
    }

    public partial class DiamondModels : Node, IDiamondModels
    {

        public Dictionary<int, ICreatureInstance> creatures { get; init; } = new Dictionary<int, ICreatureInstance>();
        public Dictionary<int, ISpell> spells { get; init; } = new Dictionary<int, ISpell>();
        public Dictionary<int, IEffect> effects { get; init; } = new Dictionary<int, IEffect>();
        public Dictionary<int, IMap> maps { get; init; } = new Dictionary<int, IMap>();

        private readonly IUIdGenerator _uIdGenerator = new UIdGenerator();

        public DiamondModels()
        {
            var fileCreatures = Godot.FileAccess.Open("res://data/creatures.json", FileAccess.ModeFlags.Read);
            var jsonCreatures = Godot.JSON.ParseString(fileCreatures.GetAsText());
            // TODO: 
            //foreach(var creature in jsonCreatures.AsGodotArray()) 
                //parseCreature(creature);

        }

        public void parseCreature() //TODO: Variant jsonCreature)
        {
            var c = new CreatureData(_uIdGenerator);
            creatures.Add(0, c);
            
        }

        public void parseSpell()
        {
            
        }

        public void parseEffect()
        {
            
        }

        public void parseMap()
        {
            
        }

        public void parseCell()
        {
            
        }

    }
}
