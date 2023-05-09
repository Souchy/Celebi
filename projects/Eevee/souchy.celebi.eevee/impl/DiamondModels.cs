
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.shared.models;
using souchy.celebi.eevee.face.shared.models.skins;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.util;

namespace souchy.celebi.eevee.impl
{
    public class DiamondModels : IDiamondModels
    {
        public IEntityDictionary<IID, IMap> maps { get; init; } = EntityDictionary<IID, IMap>.Create();
        public IEntityDictionary<IID, ICreatureModel> creatureModels { get; init; } = EntityDictionary<IID, ICreatureModel>.Create();
        public IEntityDictionary<IID, ISpellModel> spellModels { get; init; } = EntityDictionary<IID, ISpellModel>.Create();
        public IEntityDictionary<IID, IStatusModel> statusModels { get; init; } = EntityDictionary<IID, IStatusModel>.Create();
        public IEntityDictionary<IID, IEffectModel> effectModels { get; init; } = EntityDictionary<IID, IEffectModel>.Create();

        public IEntityDictionary<ObjectId, ICreatureSkin> creatureSkins { get; init; } = EntityDictionary<ObjectId, ICreatureSkin>.Create();
        public IEntityDictionary<ObjectId, ISpellSkin> spellSkins { get; init; } = EntityDictionary<ObjectId, ISpellSkin>.Create();
        public IEntityDictionary<ObjectId, IEffectSkin> effectSkins { get; init; } = EntityDictionary<ObjectId, IEffectSkin>.Create();

        public IEntityDictionary<ObjectId, IStringEntity> i18n { get; init; } = EntityDictionary<ObjectId, IStringEntity>.Create();
        public IEntityDictionary<ObjectId, IEffect> effects { get; init; } = EntityDictionary<ObjectId, IEffect>.Create();
        public IEntityDictionary<ObjectId, IStats> stats { get; init; } = EntityDictionary<ObjectId, IStats>.Create();

        public void Dispose()
        {
            maps.Dispose();
            creatureModels.Dispose();
            stats.Dispose();
            spellModels.Dispose();
            statusModels.Dispose();
            effectModels.Dispose();
            effects.Dispose();
            creatureSkins.Dispose();
            spellSkins.Dispose();
            effectSkins.Dispose();
            i18n.Dispose();
        }
    }
}
