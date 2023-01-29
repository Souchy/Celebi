
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
        public IEntityDictionary<IID, IMap> maps { get; init; } = new EntityDictionary<IID, IMap>();
        public IEntityDictionary<IID, ICreatureModel> creatureModels { get; init; } = new EntityDictionary<IID, ICreatureModel>();
        public IEntityDictionary<IID, IStats> stats { get; init; } = new EntityDictionary<IID, IStats>();
        public IEntityDictionary<IID, ISpellModel> spellModels { get; init; } = new EntityDictionary<IID, ISpellModel>();
        public IEntityDictionary<IID, IStatusModel> statusModels { get; init; } = new EntityDictionary<IID, IStatusModel>();
        public IEntityDictionary<IID, IEffectModel> effectModels { get; init; } = new EntityDictionary<IID, IEffectModel>();
        public IEntityDictionary<IID, IEffect> effects { get; init; } = new EntityDictionary<IID, IEffect>();
        public IEntityDictionary<IID, ICreatureSkin> creatureSkins { get; init; } = new EntityDictionary<IID, ICreatureSkin>();
        public IEntityDictionary<IID, ISpellSkin> spellSkins { get; init; } = new EntityDictionary<IID, ISpellSkin>();
        public IEntityDictionary<IID, IEffectSkin> effectSkins { get; init; } = new EntityDictionary<IID, IEffectSkin>();
        public IEntityDictionary<IID, string> i18n { get; init; } = new EntityDictionary<IID, string>();

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
