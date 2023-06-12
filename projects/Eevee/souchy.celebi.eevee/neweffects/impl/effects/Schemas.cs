using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.conditions.status;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.zones;
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
    /// <summary>
    /// This is instanced SpellStats <br></br>
    /// As opposed to SpellMeta's  SpellModelStats <br></br>
    /// Can be used to refresh the current cooldown, add charges..
    /// </summary>
    public record SpellAddStats() : SpellMetaSchema
    {
        public SpellStats stats { get; set; } = SpellStats.Create();
    }
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
    /// <summary>
    /// This effect applies its children to the target,  <br></br>
    /// chains to the next target in aoe,  <br></br>
    /// applies its children to the new target,  <br></br>
    /// etc. <br></br>
    /// </summary>
    public record SpellChain() : IEffectSchema
    {
        /// <summary>
        /// Maximum number of chains / depth of iteration tree (this is only vertical, the horizontal limit is the chainzone.maxSampleCount + spell.maxFork)
        /// </summary>
        public int maxChains { get; set; } = 1;
        /// <summary>
        /// Chaining zone starting from each target in the Effect zone
        /// </summary>
        public IZone chainZone { get; set; } = new Zone();
        public ICondition TargetFilter { get; set; }
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
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    }
    public record CreateTrap() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    }
    public record CreateGlyph() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
    }
    public record CreateGlyphAura() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
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
                public int percentVariance { get; set; } = 0;
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
            
            public record DamagePerContextualStat() : ADamageSchema
            {
                public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
                /// <summary>
                /// multiplies the damageTaken or whatever ContextualStat
                /// </summary>
                public int multiplier => this.baseDamage;
                /// <summary>
                /// Ex: take 1000 damage, deal 100% of that back
                /// </summary>
                public bool isMultiplierPercentage = false;
            }
            public record HealPerContextualStat() : Heal
            {
                public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
                /// <summary>
                /// multiplies the damageTaken or whatever ContextualStat
                /// </summary>
                public int multiplier => this.baseHeal;
                /// <summary>
                /// Ex: take 1000 damage, heal 100% of that
                /// </summary>
                public bool isMultiplierPercentage = false;
            }
        #endregion
        #region Heal
            public record Heal() : IEffectSchema {
                public ElementType element { get; set; } = ElementType.None;
                public int baseHeal { get; set; } = 0;
                public int percentVariance { get; set; } = 0;
            }
            public record HealPercentLifeMax() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int percentHeal { get; set; } = 0;
                public ActorType percentOfWhoseLife { get; set; } = ActorType.Target;
            }
            /// <summary>
            ///  child of DamageEffect
            ///  (ex: pillage, piège fangeux, mot interdit..)
            ///  (DmgEff selects targets, then the heal starts from their positions and heals according to its new TargetAcquisitionZone)
            ///  That means you need to calculate Area += foreach(zone.area(target))
            /// </summary>
            public record HealPercentDamageDoneByEffect() : IEffectSchema
            {
                public ElementType element { get; set; } = ElementType.None;
                public int percentHeal { get; set; } = 0;
            }
        #endregion
        #region Both
            public record DirectDamageStealLife() : ADamageSchema() { }

            public record TransferLife() : IEffectSchema 
            { 
                public IZone transferFrom = new Zone();
                public int value { get; set; } = 0;
                public int percentVariance { get; set; } = 0;
            }
            public record TransferPercentLifeMax() : IEffectSchema 
            { 
                public IZone transferFrom = new Zone();
                public int percentValue { get; set; } = 0;
            }
        #endregion
    #endregion

    #region Fight
    public record SwapOut() : IEffectSchema
    {
        /// <summary>
        /// if true, will swap out even if creature.stats.contextual.swapOutRemainingCooldown > 0
        /// </summary>
        public bool ignoreOutCooldown { get; set; } = false;
        /// <summary>
        /// if true, will override creature.stats.other.swapInBaseCooldown
        /// </summary>
        public bool overrideInCooldown { get; set; } = false;
        public int setInCooldown { get; set; } = 0;
    }
    public record SwapIn() : IEffectSchema
    {
        public bool ignoreInCooldown { get; set; } = false;
        public bool overrideOutCooldown { get; set; } = false;
        public int setOutCooldown { get; set; } = 0;
    }
    #endregion

}
