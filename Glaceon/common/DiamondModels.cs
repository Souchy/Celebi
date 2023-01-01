using Godot;
using souchy.celebi.eevee;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;
using System.Collections.Generic;
//using CreatureData = souchy.celebi.eevee.impl.objects.Creature;

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

        public Dictionary<IID, ICreature> creatures { get; init; } = new Dictionary<IID, ICreature>();
        public Dictionary<IID, ISpell> spells { get; init; } = new Dictionary<IID, ISpell>();
        public Dictionary<IID, IEffect> effects { get; init; } = new Dictionary<IID, IEffect>();
        public Dictionary<IID, IMap> maps { get; init; } = new Dictionary<IID, IMap>();

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
            //var c = new Creature(_uIdGenerator);
            //creatures.Add((IID) 0, c);
            
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
