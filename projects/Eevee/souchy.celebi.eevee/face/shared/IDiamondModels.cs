using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.face.shared
{
    public interface IDiamondModels : IDisposable //: IEntity
    {
        public IEntityDictionary<IID, IMap> maps { get; init; }

        public IEntityDictionary<IID, ICreatureModel> creatureModels { get; init; }
        public IEntityDictionary<IID, ISpellModel> spellModels { get; init; }
        public IEntityDictionary<IID, IStatusModel> statusModels { get; init; }
        public IEntityDictionary<IID, IEffectModel> effectModels { get; init; }
        /// <summary>
        /// 
        /// /////////"Model/Static" effects for spells and static statuses (like passives, as opposed to variable things like fourberie)
        /// </summary>
        public IEntityDictionary<IID, IEffect> effects { get; init; }


        public IEntityDictionary<IID, ICreatureSkin> creatureSkins { get; init; }
        public IEntityDictionary<IID, ISpellSkin> spellSkins { get; init; }
        public IEntityDictionary<IID, IEffectSkin> effectSkins { get; init; }
        /// <summary>
        /// load 1 language at a time
        /// </summary>
        public IEntityDictionary<IID, string> i18n { get; init; }


    }
}
