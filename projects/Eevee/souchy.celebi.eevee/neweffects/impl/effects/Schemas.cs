using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions.value;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.move;

namespace souchy.celebi.eevee.neweffects.impl.effects
{

    #region Creature
    public record Kill() : IEffectSchema { }
    public record Revive() : IEffectSchema { }
    public record EndTurn() : IEffectSchema { }
    public record SpawnSummon() : IEffectSchema {
        public CreatureIID modelId { get; set; } = new();
    }
    public record UnspawnSummon() : IEffectSchema { }
    public record SpawnSummonDouble() : IEffectSchema { }
    public record SpawnSummonDoubleIllusion() : IEffectSchema { }
    public record RevealCreatureSpells() : IEffectSchema { }
    #endregion

    #region Move Translation
    public record Walk() : IMoveSchema { }
    public record PushBy() : IMoveSchema
    {
        public int distance { get; set; } = 1;
    }
    public record PullBy() : IMoveSchema
    {
        public int distance { get; set; } = 1;
    }
    public record DashBy() : IMoveSchema
    {
        public int distance { get; set; } = 1;
    }
    public record DashAwayBy() : IMoveSchema
    {
        public int distance { get; set; } = 1;
    }
    public record PushTo() : IMoveSchema { }
    public record PullTo() : IMoveSchema { }
    public record DashTo() : IMoveSchema { }
    #endregion

    #region Move Teleportation
    public record SwapSelfWith() : IEffectSchema { }
    public record SwapTargetWith() : IMoveSchema { }
    /// <summary>
    /// BoardTargeetType = cell
    /// </summary>
    public record TeleportSelfTo() : IEffectSchema { }
    /// <summary>
    /// BoardTargetType = creature
    /// </summary>
    public record TeleportTargetTo() : IMoveSchema { }
    public record TeleportSelfBy() : IEffectSchema
    {
        public int distance { get; set; } = 1;
    }
    public record TeleportTargetBy() : IMoveSchema
    {
        public int distance { get; set; } = 1;
    }
    public record TeleportSymmetricallySelfOverTarget() : IEffectSchema { }
    public record TeleportSymmetricallyTargetOverSelf() : IEffectSchema { }
    public record TeleportSymmetricallyAoeOverTarget() : IMoveSchema { }
    public record TeleportToPreviousPosition() : IEffectSchema { }
    public record TeleportToStartOfTurnPosition() : IEffectSchema { }
    public record TeleportToStartOfFightPosition() : IEffectSchema { }
    #endregion

    #region Meta
    public record ChangeActor() : IEffectSchema { }
    public record CastSubSpell() : IEffectSchema
    {
        public SpellIID modelId { get; set; } = new();
    }
    public record RandomChild() : IEffectSchema { }
    // this one might be thougher with Effect.GetPossibleBoardTargets then Mind.applyEffectContainer which foreaches(targets)
    public record RandomPointsInZone() : IEffectSchema
    {
        public int maximumPointsCount { get; set; } = int.MaxValue;
        public int percentChancePerPoint { get; set; } = 50;
    }
    public record EmptyText() : IEffectSchema {
        public StringIID modelId { get; set; } = new();
    }
    #endregion

    #region Status Add
    public record AddStatusCreature() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
    }
    public record AddTrap() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
    }
    public record AddGlyph() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
    }
    public record AddGlyphAura() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
    }
    #endregion

    #region Status Create
    public record CreateStatusCreature() : IEffectSchema
    {
        public CreatureStats statusStats { get; set; } = CreatureStats.Create();
    }
    public record CreateTrap() : IEffectSchema
    {
        public StatusStats statusStats { get; set; } = StatusStats.Create();
    }
    public record CreateGlyph() : IEffectSchema
    {
        public StatusStats statusStats { get; set; } = StatusStats.Create();
    }
    public record CreateGlyphAura() : IEffectSchema
    {
        public StatusStats statusStats { get; set; } = StatusStats.Create();
    }
    #endregion

    #region Status Remove
    public record RemoveStatusCreature() : IEffectSchema
    {
        public IStatusCondition statusFilter { get; set; }
        public int durationToRemove { get; set; }
    }
    public record RemoveTrap() : IEffectSchema
    {
        public IStatusCondition statusFilter { get; set; }
    }
    public record RemoveGlyph() : IEffectSchema
    {
        public IStatusCondition statusFilter { get; set; }
    }
    #endregion

    #region Resource
    #region Damage 
            public abstract record ADamageSchema() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int baseDamage { get; set; } = 0;
                public int percentPenetration { get; set; } = 0;
            }
            public record DirectDamage() : ADamageSchema() { }
            // Indirect Damage ignore defensive stats so they can't have penetration either
            public record IndirectDamage() : ADamageSchema() { } // no pen

            // PercentLife damage don't benefit from offensive stats (affinities), but they calculate defensive stats (resistance, penetration)
            public record DirectDamagePercentLifeMax() : ADamageSchema() { }
            public record IndirectDamagePercentLifeMax() : ADamageSchema() { } // no pen

            public record RedirectDamage() : IEffectSchema {
                public int percentRedirect { get; set; } = 0;
            }

            public record DamagePerDynamicResourceUsedForSpell() : IEffectSchema {
                public CharacteristicId charId { get; set; } = Resource.Life.ID;
                public int baseDamagePerCharacUsed { get; set; } = 0;
            }
            public record DamagePerManaReducedInTurn() : IEffectSchema {
                    public int baseDamagePerCharacReduced { get; set; } = 0;
            }
            public record DamagePerMovementReducedInTurn() : IEffectSchema {
                public int baseDamagePerCharacReduced { get; set; } = 0;
            }
        #endregion
        #region Heal
            public record Heal() : IEffectSchema {
                public ElementType element { get; set; } = ElementType.None;
                public int baseHeal { get; set; } = 0;
            }
            public record HealPercentLifeMax() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int percentHeal { get; set; } = 0;
                public ActorType percentOfWhoseLife { get; set; } = ActorType.Target;
            }
            // child of Status 
            public record HealPercentLifeDamageReceived() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int percentHeal { get; set; } = 0;
            }
            // child of Status or DamageEffect
            // (DmgEff selects targets, then the heal starts from their positions and heals according to its new TargetAcquisitionZone)
            // That means you need to calculate Area += foreach(zone.area(target))
            public record HealPercentLifeDamageDone() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int percentHeal { get; set; } = 0;
            }
        #endregion
        #region Both
            public record DirectDamageStealLife() : ADamageSchema() { }

            public record TransferLife() : IEffectSchema { 
                public int value { get; set; } = 0;
            }
            public record TransferPercentLifeMax() : IEffectSchema { 
                public int percentValue { get; set; } = 0;
            }
        #endregion
    #endregion

    #region Fight
    public record SwapOut() : IEffectSchema { }
    public record SwapIn() : IEffectSchema { }
    #endregion

}
