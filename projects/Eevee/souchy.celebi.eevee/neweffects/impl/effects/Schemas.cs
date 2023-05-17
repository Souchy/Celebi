using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.move;

namespace souchy.celebi.eevee.neweffects.impl.effects
{

    #region Creature
    public record Kill() : IEffectSchema { }
    public record Revive() : IEffectSchema { }
    public record EndTurn() : IEffectSchema { }
    public record SpawnSummon(IID summonModelUid) : IEffectSchema { }
    public record UnspawnSummon() : IEffectSchema { }
    public record SpawnSummonDouble() : IEffectSchema { }
    public record SpawnSummonDoubleIllusion() : IEffectSchema { }
    public record RevealCreatureSpells() : IEffectSchema { }
    #endregion

    #region Move Translation
    public record Walk() : IMoveSchema { }
    public record PushBy() : IMoveSchema { }
    public record PullBy() : IMoveSchema { }
    public record DashBy() : IMoveSchema { }
    public record DashAwayBy() : IMoveSchema { }
    public record PushTo() : IMoveSchema { }
    public record PullTo() : IMoveSchema { }
    public record DashTo() : IMoveSchema { }
    #endregion

    #region Move Teleportation
    public record SwapSelfWith() : IMoveSchema { }
    public record SwapTargetWith() : IMoveSchema { }
    public record TeleportSelfTo() : IMoveSchema { }
    public record TeleportTargetTo() : IMoveSchema { }
    public record TeleportSelfBy() : IMoveSchema { }
    public record TeleportTargetBy() : IMoveSchema { }
    public record TeleportSymmetricallySelfOverTarget() : IMoveSchema { }
    public record TeleportSymmetricallyTargetOverSelf() : IMoveSchema { }
    public record TeleportSymmetricallyAoeOverTarget() : IMoveSchema { }
    public record TeleportToPreviousPosition() : IMoveSchema { }
    public record TeleportToStartOfTurnPosition() : IMoveSchema { }
    public record TeleportToStartOfFightPosition() : IMoveSchema { }
    #endregion

    #region Meta
    public record ChangeSourceActor() : IEffectSchema { }
    public record CastSubSpell(IID spellModelUid) : IEffectSchema { }
    public record RandomChild() : IEffectSchema { }
    public record RandomPointsInZone(int maximumPointsCount = int.MaxValue, int percentChancePerPoint = 50) : IEffectSchema { }
    public record EmptyText(ObjectId stringUid) : IEffectSchema { }
    #endregion

    #region Status
    public record AddStatusCreature(IID statusModelUid) : IEffectSchema { }
    public record RemoveStatusCreature(IValue<int> durationToRemove) : IEffectSchema { }
    public record AddTrap(IID statusModelUid) : IEffectSchema { }
    public record AddGlyph(IID statusModelUid) : IEffectSchema { }
    public record AddGlyphAura(IID statusModelUid) : IEffectSchema { }
    public record AddAddStatStatus(IStat stat) : IEffectSchema { }
    public record AddStealStatStatus(IStat stat) : IEffectSchema { }
    #endregion

    #region Resource
    public abstract record ADamageSchema(ElementType element, int baseDamage, int percentPenetration = 0) : IEffectSchema { }

    public record DirectDamage(ElementType element, int baseDamage, int percentPenetration) : ADamageSchema(element, baseDamage, percentPenetration) { }
    public record DirectDamageStealLife(ElementType element, int baseDamage, int percentPenetration) : ADamageSchema(element, baseDamage, percentPenetration) { }
    // Indirect Damage ignore defensive stats so they can't have penetration either
    public record IndirectDamage(ElementType element, int baseDamage) : ADamageSchema(element, baseDamage) { }

    // PercentLife damage don't benefit from offensive stats (affinities), but they calculate defensive stats (resistance, penetration)
    public record DirectDamagePercentLifeMax(ElementType element, int baseDamage, int percentPenetration) : ADamageSchema(element, baseDamage, percentPenetration) { }
    public record IndirectDamagePercentLifeMax(ElementType element, int baseDamage) : ADamageSchema(element, baseDamage) { }

    public record RedirectDamage(double percentRedirect) : IEffectSchema { }

    public record Heal(ElementType element, int baseHeal) : IEffectSchema { }
    public record HealPercentLifeMax(ElementType element, int percentHeal) : IEffectSchema { }
    public record HealPercentLifeDamageReceived(ElementType element, int percentHeal) : IEffectSchema { }
    public record HealPercentLifeDamageDone(ElementType element, int percentHeal) : IEffectSchema { }


    public record TransferLife(int value) : IEffectSchema { }
    public record TransferPercentLifeMax(int percentValue) : IEffectSchema { }
    public record DamagePerDynamicResourceUsedForSpell(CharacteristicId charId, int baseDamagePerCharacUsed) : IEffectSchema { }
    public record DamagePerManaReducedInTurn(int baseDamagePerCharacReduced) : IEffectSchema { }
    public record DamagePerMovementReducedInTurn(int baseDamagePerCharacReduced) : IEffectSchema { }
    #endregion

    #region Fight
    public record SwapOut() : IEffectSchema { }
    public record SwapIn() : IEffectSchema { }
    #endregion

}
