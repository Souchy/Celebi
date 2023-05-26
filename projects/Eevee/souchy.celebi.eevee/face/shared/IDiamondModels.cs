using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.face.shared
{
    public interface IDiamondModels : IDisposable //: IEntity
    {
        public IEntityDictionary<ObjectId, IMap> maps { get; init; }

        public IEntityDictionary<ObjectId, ICreatureModel> creatureModels { get; init; }
        public IEntityDictionary<ObjectId, ISpellModel> spellModels { get; init; }
        public IEntityDictionary<ObjectId, IStatusModel> statusModels { get; init; }
        public IEntityDictionary<ObjectId, IEffectModel> effectModels { get; init; }
        /// <summary>
        /// 
        /// /////////"Model/Static" effects for spells and static statuses (like passives, as opposed to variable things like fourberie)
        /// </summary>
        public IEntityDictionary<ObjectId, IEffect> effects { get; init; }
        public IEntityDictionary<ObjectId, IStats> stats { get; init; }


        public IEntityDictionary<ObjectId, ICreatureSkin> creatureSkins { get; init; }
        public IEntityDictionary<ObjectId, ISpellSkin> spellSkins { get; init; }
        public IEntityDictionary<ObjectId, IEffectSkin> effectSkins { get; init; }
        /// <summary>
        /// load 1 language at a time
        /// </summary>
        public IEntityDictionary<ObjectId, IStringEntity> i18n { get; init; }


    }
}
