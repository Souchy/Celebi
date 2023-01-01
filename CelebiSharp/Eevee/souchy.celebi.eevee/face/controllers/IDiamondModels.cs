using souchy.celebi.eevee.face.objects;
using Newtonsoft.Json;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.io
{
    public interface IDiamondModels
    {
        //private IServiceProvider serviceProvider;
        //public IDiamondModels()
        //{
        //}

        public Dictionary<IID, ICreature> creatures { get; init; }
        public Dictionary<IID, ISpell> spells { get; init; }
        public Dictionary<IID, IEffect> effects { get; init; }
        public Dictionary<IID, IMap> maps { get; init; }


        public void parseCreature();
        public void parseSpell();
        public void parseEffect();
        public void parseMap();
        public void parseCell();
        //{
        //    var obj = JsonConvert.DeserializeObject("");
        //    serviceProvider.GetService(typeof(ICell));
        //}


    }
}
