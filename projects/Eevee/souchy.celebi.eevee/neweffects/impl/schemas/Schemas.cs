using Newtonsoft.Json.Linq;
using souchy.celebi.eevee.enums;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.neweffects.impl.effects.move;
using System;
using System.Xml.Linq;

namespace souchy.celebi.eevee.neweffects.impl.schemas
{

    #region Creature
    public record Kill() : IEffectSchema
    {
        public IEffectSchema copy() => new Kill();
    }
    public record Revive() : IEffectSchema
    {
        public IEffectSchema copy() => new Revive();
    }
    public record EndTurn() : IEffectSchema
    {
        public IEffectSchema copy() => new EndTurn();
    }
    public record SpawnSummon() : IEffectSchema {
        public CreatureIID modelId { get; set; } = new();
        public IEffectSchema copy() => new SpawnSummon()
        {
            modelId = modelId
        };
    }
    public record UnspawnSummon() : IEffectSchema
    {
        public IEffectSchema copy() => new UnspawnSummon();
    }
    public record SpawnSummonDouble() : IEffectSchema
    {
        public IEffectSchema copy() => new SpawnSummonDouble();
    }
    public record SpawnSummonDoubleIllusion() : IEffectSchema
    {
        public IEffectSchema copy() => new SpawnSummonDoubleIllusion();
    }
    public record RevealCreatureSpells() : IEffectSchema
    {
        public IEffectSchema copy() => new RevealCreatureSpells();
    }
    /// <summary>
    /// This is instanced SpellStats <br></br>
    /// As opposed to SpellMeta's  SpellModelStats <br></br>
    /// Can be used to refresh the current cooldown, add charges..
    /// </summary>
    public record SpellAddStats() : SpellMetaSchema
    {
        public SpellStats stats { get; set; } = SpellStats.Create();
        public IEffectSchema copy() => new SpellAddStats()
        {
            stats = (SpellStats) stats.copy()
        };
    }
    #endregion

    #region Move Translation
    public record Walk() : MoveSchema
    {
        public override IEffectSchema copy() => new Walk()
        {
            MoveTargetZone = MoveTargetZone.copy(),
        };
    }
    public record PushBy() : MoveSchema
    {
        public int distance { get; set; } = 1;
        public override IEffectSchema copy() => new PushBy()
        {
            MoveTargetZone = MoveTargetZone.copy(),
            distance = distance
        };
    }
    public record PullBy() : MoveSchema
    {
        public int distance { get; set; } = 1;
        public override IEffectSchema copy() => new PullBy()
        {
            MoveTargetZone = MoveTargetZone.copy(),
            distance = distance
        };
    }
    public record DashBy() : MoveSchema
    {
        public int distance { get; set; } = 1;
        public override IEffectSchema copy() => new DashBy()
        {
            MoveTargetZone = MoveTargetZone.copy(),
            distance = distance
        };
    }
    public record DashAwayBy() : MoveSchema
    {
        public int distance { get; set; } = 1;
        public override IEffectSchema copy() => new DashAwayBy()
        {
            MoveTargetZone = MoveTargetZone.copy(),
            distance = distance
        };
    }
    public record PushTo() : MoveSchema
    {
        public override IEffectSchema copy() => new PushTo()
        {
            MoveTargetZone = MoveTargetZone.copy()
        };
    }
    public record PullTo() : MoveSchema
    {
        public override IEffectSchema copy() => new PullTo()
        {
            MoveTargetZone = MoveTargetZone.copy()
        };
    }
    public record DashTo() : MoveSchema
    {
        public override IEffectSchema copy() => new DashTo()
        {
            MoveTargetZone = MoveTargetZone.copy()
        };
    }
    #endregion

    #region Move Teleportation
    public record SwapSelfWith() : IEffectSchema
    {
        public IEffectSchema copy() => new SwapSelfWith();
    }
    public record SwapTargetWith() : MoveSchema
    {
        public override IEffectSchema copy() => new SwapTargetWith()
        {
            MoveTargetZone = MoveTargetZone.copy()
        };
    }
    /// <summary>
    /// BoardTargeetType = cell
    /// </summary>
    public record TeleportSelfTo() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportSelfTo();
    }
    /// <summary>
    /// BoardTargetType = creature
    /// </summary>
    public record TeleportTargetTo() : MoveSchema
    {
        public override IEffectSchema copy() => new TeleportTargetTo()
        {
            MoveTargetZone = MoveTargetZone.copy()
        };
    }
    public record TeleportSelfBy() : IEffectSchema
    {
        public int distance { get; set; } = 1;
        public IEffectSchema copy() => new TeleportSelfBy()
        {
            distance = distance
        };
    }
    public record TeleportTargetBy() : MoveSchema
    {
        public int distance { get; set; } = 1;
        public override IEffectSchema copy() => new TeleportTargetBy()
        {
            MoveTargetZone = MoveTargetZone.copy(),
            distance = distance
        };
    }
    public record TeleportSymmetricallySelfOverTarget() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportSymmetricallySelfOverTarget();
    }
    public record TeleportSymmetricallyTargetOverSelf() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportSymmetricallyTargetOverSelf();
    }
    public record TeleportSymmetricallyAoeOverTarget() : MoveSchema
    {
        public override IEffectSchema copy() => new TeleportSymmetricallyAoeOverTarget()
        {
            MoveTargetZone = MoveTargetZone.copy(),
        };
    }
    public record TeleportToPreviousPosition() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportToPreviousPosition();
    }
    public record TeleportToStartOfTurnPosition() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportToStartOfTurnPosition();
    }
    public record TeleportToStartOfFightPosition() : IEffectSchema
    {
        public IEffectSchema copy() => new TeleportToStartOfFightPosition();
    }
    #endregion

    #region Meta
    public record ChangeActor() : IEffectSchema
    {
        public IEffectSchema copy() => new ChangeActor();
    }
    public record CastSubSpell() : IEffectSchema
    {
        public SpellIID modelId { get; set; } = new();
        public IEffectSchema copy() => new CastSubSpell()
        {
            modelId = modelId
        };
    }
    public record RandomChild() : IEffectSchema 
    {
        public int samplingCount { get; set; } = 1;
        public List<int> weights { get; set; } = new();
        public IEffectSchema copy() => new RandomChild()
        {
            samplingCount = samplingCount,
            weights = new(weights)
        };
    }
    // this one might be thougher with Effect.GetPossibleBoardTargets then Mind.applyEffectContainer which foreaches(targets)
    //public record RandomPointsInZone() : IEffectSchema
    //{
    //    public int maximumPointsCount { get; set; } = int.MaxValue;
    //    public int percentChancePerPoint { get; set; } = 50;
    //}
    public record EmptyText() : IEffectSchema {
        public StringIID modelId { get; set; } = new();
        public IEffectSchema copy() => new EmptyText()
        {
            modelId = modelId,
        };
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
        public IEffectSchema copy() => new SpellChain()
        {
            maxChains = maxChains,
            chainZone = chainZone.copy(),
            TargetFilter = TargetFilter.copy()
        };
    }
    #endregion

    #region Status Add
    public record AddStatusCreature() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
        public IEffectSchema copy() => new AddStatusCreature()
        {
            modelId = modelId,
        };
    }
    public record AddTrap() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
        public IEffectSchema copy() => new AddTrap()
        {
            modelId = modelId,
        };
    }
    public record AddGlyph() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
        public IEffectSchema copy() => new AddGlyph()
        {
            modelId = modelId,
        };
    }
    public record AddGlyphAura() : IEffectSchema
    {
        public StatusIID modelId { get; set; } = new();
        public IEffectSchema copy() => new AddGlyphAura()
        {
            modelId = modelId,
        };
    }
    #endregion

    #region Status Create
    public record CreateStatusCreature() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
        public IEffectSchema copy() => new CreateStatusCreature()
        {
            statusStats = (StatusModelStats) statusStats.copy()
        };
    }
    public record CreateTrap() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
        public IEffectSchema copy() => new CreateTrap()
        {
            statusStats = (StatusModelStats) statusStats.copy()
        };
    }
    public record CreateGlyph() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
        public IEffectSchema copy() => new CreateGlyph()
        {
            statusStats = (StatusModelStats) statusStats.copy()
        };
    }
    public record CreateGlyphAura() : IEffectSchema
    {
        public StatusModelStats statusStats { get; set; } = StatusModelStats.Create();
        public IEffectSchema copy() => new CreateGlyphAura()
        {
            statusStats = (StatusModelStats) statusStats.copy()
        };
    }
    #endregion

    #region Status Remove
    public record RemoveStatusCreature() : IEffectSchema
    {
        public ICondition statusFilter { get; set; }
        public int durationToRemove { get; set; }
        public IEffectSchema copy() => new RemoveStatusCreature()
        {
            statusFilter = statusFilter.copy(),
            durationToRemove = durationToRemove
        };
    }
    public record RemoveTrap() : IEffectSchema
    {
        public ICondition statusFilter { get; set; }
        public int durationToRemove { get; set; }
        public IEffectSchema copy() => new RemoveTrap()
        {
            statusFilter = statusFilter.copy(),
            durationToRemove = durationToRemove
        };
    }
    public record RemoveGlyph() : IEffectSchema
    {
        public ICondition statusFilter { get; set; } // IStatusCondition
        public int durationToRemove { get; set; }
        public IEffectSchema copy() => new RemoveGlyph()
        {
            statusFilter = statusFilter.copy(),
            durationToRemove = durationToRemove
        };
    }
    #endregion

    #region Resource
    #region Damage 
    public abstract record AbstractDamageSchema() : IEffectSchema
    {
        public ElementType element { get; set; } = ElementType.None;
        public int baseDamage { get; set; } = 0;
        public int percentPenetration { get; set; } = 0;
        public int percentVariance { get; set; } = 0;

        public abstract IEffectSchema copy();
    }
    public record DirectDamage() : AbstractDamageSchema()
    {
        public override IEffectSchema copy() => new DirectDamage()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance
        };
    }
    // Indirect Damage ignore defensive stats so they can't have penetration either
    public record IndirectDamage() : AbstractDamageSchema()
    {
        public override IEffectSchema copy() => new IndirectDamage()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance
        };
    } // no pen

    // PercentLife damage don't benefit from offensive stats (affinities), but they calculate defensive stats (resistance, penetration)
    public record DirectDamagePercentLifeMax() : AbstractDamageSchema()
    {
        public override IEffectSchema copy() => new DirectDamagePercentLifeMax()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance
        };
    }
    public record IndirectDamagePercentLifeMax() : AbstractDamageSchema()
    {
        public override IEffectSchema copy() => new IndirectDamagePercentLifeMax()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance
        };
    } // no pen

    public record RedirectDamage() : IEffectSchema
    {
        public int percentRedirect { get; set; } = 0;
        public IEffectSchema copy() => new RedirectDamage()
        {
            percentRedirect = percentRedirect,
        };
    }

    public record DamagePerDynamicResourceUsedForSpell() : IEffectSchema
    {
        public CharacteristicId charId { get; set; } = Resource.Life.ID;
        public int baseDamagePerCharacUsed { get; set; } = 0;
        public IEffectSchema copy() => new DamagePerDynamicResourceUsedForSpell()
        {
            charId = charId,
            baseDamagePerCharacUsed = baseDamagePerCharacUsed
        };
    }

    public record DamagePerContextualStat() : AbstractDamageSchema
    {
        public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
        /// <summary>
        /// multiplies the damageTaken or whatever ContextualStat
        /// </summary>
        //public int multiplier => this.baseDamage;
        /// <summary>
        /// Ex: take 1000 damage, deal 100% of that back
        /// </summary>
        public bool isMultiplierPercentage = false;
        public override IEffectSchema copy() => new DamagePerContextualStat()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance,
            statId = statId,
            isMultiplierPercentage = isMultiplierPercentage,
        };
    }
    #endregion
    #region Heal
    public record Heal() : IEffectSchema
    {
        public ElementType element { get; set; } = ElementType.None;
        public int baseHeal { get; set; } = 0;
        public int percentVariance { get; set; } = 0;

        public virtual IEffectSchema copy() => new Heal()
        {
            element = element,
            baseHeal = baseHeal,
            percentVariance = percentVariance,
        };
    }
    public record HealPerContextualStat() : Heal
    {
        public CharacteristicId statId { get; set; } = Contextual.DamageTaken.ID;
        /// <summary>
        /// multiplies the damageTaken or whatever ContextualStat
        /// </summary>
        //public int multiplier => this.baseHeal;
        /// <summary>
        /// Ex: take 1000 damage, heal 100% of that
        /// </summary>
        public bool isMultiplierPercentage = false;

        public override IEffectSchema copy() => new HealPerContextualStat()
        {
            element = element,
            baseHeal = baseHeal,
            percentVariance = percentVariance,
            statId = statId,
            isMultiplierPercentage = isMultiplierPercentage,
            //multiplier = multiplier
        };
    }
    public record HealPercentLifeMax() : IEffectSchema
    {
        public ElementType element { get; set; } = ElementType.None;
        public int percentHeal { get; set; } = 0;
        public ActorType percentOfWhoseLife { get; set; } = ActorType.Target;
        public IEffectSchema copy() => new HealPercentLifeMax()
        {
            element = element,
            percentHeal = percentHeal,
            percentOfWhoseLife = percentOfWhoseLife
        };
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
        public IEffectSchema copy() => new HealPercentDamageDoneByEffect()
        {
            element = element,
            percentHeal = percentHeal,
        };
    }
    #endregion
    #region Both
    public record DirectDamageStealLife() : AbstractDamageSchema()
    {
        public override IEffectSchema copy() => new DirectDamageStealLife()
        {
            element = element,
            baseDamage = baseDamage,
            percentPenetration = percentPenetration,
            percentVariance = percentVariance,
        };
    }

    public record TransferLife() : IEffectSchema
    {
        public IZone transferFrom = new Zone();
        public int value { get; set; } = 0;
        public int percentVariance { get; set; } = 0;
        public IEffectSchema copy() => new TransferLife()
        {
            transferFrom = transferFrom.copy(),
            value = value,
            percentVariance = percentVariance
        };
    }
    public record TransferPercentLifeMax() : IEffectSchema
    {
        public IZone transferFrom = new Zone();
        public int percentValue { get; set; } = 0;
        public IEffectSchema copy() => new TransferPercentLifeMax()
        {
            transferFrom = transferFrom.copy(),
            percentValue = percentValue,
        };
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
        public IEffectSchema copy() => new SwapOut()
        {
            ignoreOutCooldown = ignoreOutCooldown,
            overrideInCooldown = overrideInCooldown,
            setInCooldown = setInCooldown
        };
    }
    public record SwapIn() : IEffectSchema
    {
        public bool ignoreInCooldown { get; set; } = false;
        public bool overrideOutCooldown { get; set; } = false;
        public int setOutCooldown { get; set; } = 0;
        public IEffectSchema copy() => new SwapIn()
        {
            ignoreInCooldown = ignoreInCooldown,
            overrideOutCooldown = overrideOutCooldown,
            setOutCooldown = setOutCooldown
        };
    }
    #endregion

}
