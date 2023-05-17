using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl.effects.creature
{
    #region Creature
    public record AddStatSchema(IStat stat) : IEffectSchema { }
    public record LearnSpellSchema(ObjectId spellModel) : IEffectSchema { }
    public record ForgetSpellSchema(ObjectId spellModel) : IEffectSchema { }
    public record ChangeAppearance(IID appearanceId) : IEffectSchema { }
    public record ChangeAnimationSet(IID animationSetId) : IEffectSchema { }
    public record ReduceDamageReceived(IValue<int> reduction) : IEffectSchema { }
    #endregion

    #region Spell
    public record AddSpellRange(IValue<int> range) : IEffectSchema { }
    public record SetSpellLineOfSight(IValue<bool> value) : IEffectSchema { }
    public record AddSpellEffectBaseDamage(IValue<int> dmg) : IEffectSchema { }
    public record ChangeSpellRangeZone(IValue<IZone> zone) : IEffectSchema { }
    public record ChangeSpellEffectZone(IValue<IZone> zone) : IEffectSchema { }
    #endregion

    #region Board
    public record BuildObstacle(ObjectId obstacle3dModelUid) : IEffectSchema { }
    public record DestroyObstacle() : IEffectSchema { }
    public record DigHole() : IEffectSchema { }
    public record FillHole() : IEffectSchema { }
    #endregion

}
