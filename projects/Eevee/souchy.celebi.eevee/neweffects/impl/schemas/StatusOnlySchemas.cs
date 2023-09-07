﻿using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl.schemas
{
    #region Board
    public record BuildObstacle() : IEffectSchema {
        //obstacle3dModelUid
        public AssetIID modelId { get; set; } = new();
    }
    public record DestroyObstacle() : IEffectSchema { }
    public record DigHole() : IEffectSchema { }
    public record FillHole() : IEffectSchema { }
    #endregion

    #region Creature
    public record AddStats() : IEffectSchema
    {
        public IStats stats { get; set; } = Stats.Create();
    }
    public record AddStatsPercent() : IEffectSchema
    {
        public IStats statsPercent { get; set; } = Stats.Create();
    }
    /// <summary>
    /// Take x% of "from" stats and add them as y% of "to" stats.
    /// Ex: for each 2 of (% missing life) -> +1 (resistance) <br></br>
    ///     gain 100% of damage taken as shield
    ///     
    /// </summary>
    public record AddStatsPerStat() : IEffectSchema
    {
        public IStats statsFrom { get; set; } = Stats.Create();
        public IStats statsTo { get; set; } = Stats.Create();
    }
    
    // maybe?
    //public record SetStats() : IEffectSchema
    //{
    //    public IStats stats { get; set; } = Stats.Create();
    //}
    // maybe?
    /// <summary>
    /// Ex: 
    ///     input: life = 100%               (prend 100% de la vie)
    ///     output: life = 50%, mana = 50%   (converti 50% de la valeur prise en life et 50% en mana)
    ///     
    /// i guess ce serait égal à faire ça:
    ///     input: life = 50%    (prend 50% de la vie)
    ///     output: mana = 100%  (converti 100% de la valeur prise en mana)
    ///     
    /// Pour annuler une stat:
    ///     input: life = 100%
    ///     output: life = 0 ou juste pas besoin de rien? 
    ///     
    /// This could be useful to convert spell costs 
    /// or a creature that switches its affinities to different elements? (solvable with swapping the creature model? (Transformation effect)) 
    /// 
    /// </summary>
    //public record ConvertStats() : IEffectSchema
    //{
    //    public IStats inputPercent { get; set; } = Stats.Create();
    //    public IStats outputPercent { get; set; } = Stats.Create();
    //}

    /// <summary>
    /// Heal targets in AcquisitionZone when the status holder receives damage. <br></br>
    /// Heals a percent of the damage taken <br></br>
    /// If you want to heal a percent of creature's life, then just do HealPercent with a trigger for damage received <br></br>
    /// (ex: proie, feu de mine, supplice, diffusion, perfusion, arbre de vie, mot sacrificiel)
    /// </summary>
    public record HealPercentDamageReceivedByEffect() : IEffectSchema
    {
        public ElementType element { get; set; } = ElementType.None;
        public int percentHeal { get; set; } = 0;
    }

    /// <summary>
    /// POE's "take 10% phys as fir damage"
    /// </summary>
    public record TakeDamageAsElement() : IEffectSchema
    {
        public ElementType input { get; set; } = ElementType.All;
        public ElementType output { get; set; } = ElementType.None;
        public int percentConversion { get; set; } = 100;
    }

    public record LearnSpell() : IEffectSchema
    {
        public SpellIID modelId { get; set; } = new();
    }
    public record ForgetSpell() : IEffectSchema {
        public SpellIID modelId { get; set; } = new();
    }
    public record ChangeAppearance() : IEffectSchema
    {
        public AssetIID modelId { get; set; } = new(); // any asset file (scene, 3d model, texture, music...)
    }
    public record ChangeAnimationSet() : IEffectSchema {
        public AnimationSetIID modelId { get; set; } = new();
    }
    public record ReduceDamageReceived() : IEffectSchema
    {
        public int reduction { get; set; } = 0;
    }
    #endregion

}