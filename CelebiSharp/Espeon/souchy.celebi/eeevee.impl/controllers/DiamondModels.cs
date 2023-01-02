using souchy.celebi.eevee;
using souchy.celebi.eevee.face.io;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.skins;
using souchy.celebi.eevee.face.util;

namespace Espeon.souchy.celebi.eeevee.impl
{
    public class DiamondModels : IDiamondModels
    {
        public Dictionary<IID, ICreature> creatures { get; init; } = new Dictionary<IID, ICreature>();
        public Dictionary<IID, ISpellModel> spells { get; init; } = new Dictionary<IID, ISpellModel>();
        public Dictionary<IID, IEffect> effects { get; init; } = new Dictionary<IID, IEffect>();
        public Dictionary<IID, IMap> maps { get; init; } = new Dictionary<IID, IMap>();
        public Dictionary<IID, ICreatureModel> creatureModels { get; init; } = new Dictionary<IID, ICreatureModel>();
        public Dictionary<IID, ISpellModel> spellModels { get; init; } = new Dictionary<IID, ISpellModel>();
        public Dictionary<IID, IEffectModel> effectModels { get; init; } = new Dictionary<IID, IEffectModel>();
        public Dictionary<IID, ICreatureSkin> creatureSkins { get; init; } = new Dictionary<IID, ICreatureSkin>();
        public Dictionary<IID, ISpellSkin> spellSkins { get; init; } = new Dictionary<IID, ISpellSkin>();
        public Dictionary<IID, IEffectSkin> effectSkins { get; init; } = new Dictionary<IID, IEffectSkin>();

        public DiamondModels()
        {
            //var fileCreatures = Godot.FileAccess.Open("res://data/creatures.json", FileAccess.ModeFlags.Read);
            //var jsonCreatures = Godot.JSON.ParseString(fileCreatures.GetAsText());
            // TODO:
            //foreach(var creature in jsonCreatures.AsGodotArray())
            //parseCreature(creature);
        }

        public void parseData()
        {
            throw new NotImplementedException();
        }
    }
}