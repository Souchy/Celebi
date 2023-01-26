using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;

namespace souchy.celebi.eevee.face.shared
{
    public interface IDiamondModels : IEntity
    {
        public Dictionary<IID, IMap> maps { get; init; }


        public Dictionary<IID, ICreatureModel> creatureModels { get; init; }
        public Dictionary<IID, ISpellModel> spellModels { get; init; }
        public Dictionary<IID, IStatusModel> statusModels { get; init; }
        public Dictionary<IID, IEffectModel> effectModels { get; init; }
        /// <summary>
        /// 
        /// /////////"Model/Static" effects for spells and static statuses (like passives, as opposed to variable things like fourberie)
        /// </summary>
        public Dictionary<IID, IEffect> effects { get; init; }


        public Dictionary<IID, ICreatureSkin> creatureSkins { get; init; }
        public Dictionary<IID, ISpellSkin> spellSkins { get; init; }
        public Dictionary<IID, IEffectSkin> effectSkins { get; init; }
        /// <summary>
        /// load 1 language at a time
        /// </summary>
        public Dictionary<IID, string> i18n { get; set; }

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
