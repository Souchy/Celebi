using souchy.celebi.eevee;
using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace Espeon.souchy.celebi.eeevee.impl
{
    public class DiamondModels : IDiamondModels
    {
        public Dictionary<IID, ICreature> creatures { get; init; } = new Dictionary<IID, ICreature>();
        public Dictionary<IID, ISpellModel> spells { get; init; } = new Dictionary<IID, ISpellModel>();
        public Dictionary<IID, IEffect> effects { get; init; } = new Dictionary<IID, IEffect>();
        public Dictionary<IID, IMap> maps { get; init; } = new Dictionary<IID, IMap>();

        private readonly IUIdGenerator _uIdGenerator = new UIdGenerator();

        public DiamondModels()
        {
            //var fileCreatures = Godot.FileAccess.Open("res://data/creatures.json", FileAccess.ModeFlags.Read);
            //var jsonCreatures = Godot.JSON.ParseString(fileCreatures.GetAsText());
            // TODO: 
            //foreach(var creature in jsonCreatures.AsGodotArray()) 
            //parseCreature(creature);
        }

        public void parseCreature() //TODO: Variant jsonCreature)
        {
            //var c = new CreatureData(_uIdGenerator);
            //creatures.Add((IID)0, c);

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
