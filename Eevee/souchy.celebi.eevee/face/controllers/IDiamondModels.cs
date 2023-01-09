using souchy.celebi.eevee.face.objects;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.models;
using souchy.celebi.eevee.face.skins;

namespace souchy.celebi.eevee.face.io
{
    public interface IDiamondModels
    {
        public Dictionary<IID, IMap> maps { get; init; }


        public Dictionary<IID, ICreatureModel> creatureModels { get; init; }
        public Dictionary<IID, ISpellModel> spellModels { get; init; }
        public Dictionary<IID, IEffectModel> effectModels { get; init; }
        public Dictionary<IID, IEffect> effects { get; init; }


        public Dictionary<IID, ICreatureSkin> creatureSkins { get; init; }
        public Dictionary<IID, ISpellSkin> spellSkins { get; init; }
        public Dictionary<IID, IEffectSkin> effectSkins { get; init; }



        public void parseData();
        //public void parseCreature();
        //public void parseSpell();
        //public void parseEffect();
        //public void parseMap();
        //public void parseCell();
        //{
        //    var obj = JsonConvert.DeserializeObject("");
        //    serviceProvider.GetService(typeof(ICell));
        //}

    }
}
