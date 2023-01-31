
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
        public IEntityDictionary<IID, IStats> stats { get; init; } = EntityDictionary<IID, IStats>.Create();
        public IEntityDictionary<IID, ISpellModel> spellModels { get; init; } = EntityDictionary<IID, ISpellModel>.Create();
        public IEntityDictionary<IID, IStatusModel> statusModels { get; init; } = EntityDictionary<IID, IStatusModel>.Create();
        public IEntityDictionary<IID, IEffectModel> effectModels { get; init; } = EntityDictionary<IID, IEffectModel>.Create();
        public IEntityDictionary<IID, IEffect> effects { get; init; } = EntityDictionary<IID, IEffect>.Create();
        public IEntityDictionary<IID, ICreatureSkin> creatureSkins { get; init; } = EntityDictionary<IID, ICreatureSkin>.Create();
        public IEntityDictionary<IID, ISpellSkin> spellSkins { get; init; } = EntityDictionary<IID, ISpellSkin>.Create();
        public IEntityDictionary<IID, IEffectSkin> effectSkins { get; init; } = EntityDictionary<IID, IEffectSkin>.Create();
        public IEntityDictionary<IID, IStringEntity> i18n { get; init; } = EntityDictionary<IID, IStringEntity>.Create();

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
