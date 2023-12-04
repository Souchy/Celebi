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
    /// This is instant SpellStats <br></br>
    /// As opposed to SpellMeta's  SpellModelStats <br></br>
    /// Can be used to refresh the current cooldown, add charges..
    /// </summary>
    public record AddSpellStats() : SpellMetaSchema
    {
        public SpellStats stats { get; set; } = SpellStats.Create();
        public IEffectSchema copy() => new AddSpellStats()
        {
            stats = (SpellStats) stats.copy()
        };
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
