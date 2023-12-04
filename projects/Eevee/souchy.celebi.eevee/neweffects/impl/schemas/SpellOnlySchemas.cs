using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.neweffects.face;
using souchy.celebi.eevee.impl.objects;
using System;

namespace souchy.celebi.eevee.neweffects.impl.schemas
{

    public interface SpellMetaSchema : IEffectSchema { }

    /// <summary>
    /// This effect is a Meta effect, doesn't do anything on its own, just applies its children in a certain way
    /// Its children modify a spell:
    ///     - modify costs, range, filters, 
    ///     - modifly cooldown remaining
    ///     - add effect...
    ///     - modify effect damages, zones, 
    /// </summary>
    public record SpellMetaModifySpell() : SpellMetaSchema
    {
        public SpellIID spell { get; set; } = new();
        public IEffectSchema copy() => new SpellMetaModifySpell()
        {
            spell = spell,
        };
    }
    /// <summary>
    /// Modify an effect with the children.
    /// Need to be a child of SpellMetaModifySpell
    /// </summary>
    public record SpellMetaModifyEffect() : SpellMetaSchema
    {
        public ObjectId effectId { get; set; } = ObjectId.Empty;
        public IEffectSchema copy() => new SpellMetaModifyEffect()
        {
            effectId = effectId,
        };
    }




    #region Spell - need to be child of SpellMetaModifySpell
    public record SpellMetaDeactivate() : IEffectSchema
    {
        public IEffectSchema copy() => new SpellMetaDeactivate();
    }
    public record SpellMetaChangeMinRangeZone() : SpellMetaSchema
    {
        public IZone minRange { get; set; } = new Zone();
        public IEffectSchema copy() => new SpellMetaChangeMinRangeZone()
        {
            minRange = minRange,
        };
    }
    public record SpellMetaChangeMaxRangeZone() : SpellMetaSchema
    {
        public IZone maxRange { get; set; } = new Zone();
        public IEffectSchema copy() => new SpellMetaChangeMaxRangeZone()
        {
            maxRange = maxRange,
        };
    }
    public record SpellMetaAddtats() : SpellMetaSchema
    {
        public SpellModelStats stats { get; set; } = SpellModelStats.Create();
        public IEffectSchema copy() => new SpellMetaAddtats()
        {
            stats = (SpellModelStats) stats.copy()
        };
    }
    public record SpellMetaAddCosts() : SpellMetaSchema
    {
        public IStats stats { get; set; } = Stats.Create();
        public IEffectSchema copy() => new SpellMetaAddCosts()
        {
            stats = stats.copy(),
        };
    }
    public record SpellMetaConvertCosts() : SpellMetaSchema
    {
        public IStats input { get; set; } = Stats.Create();
        public IStats output { get; set; } = Stats.Create();
        public IEffectSchema copy() => new SpellMetaConvertCosts()
        {
            input = input.copy(),
            output = output.copy(),
        };
    }
    /// <summary>
    /// Will add the children of this effect to the effectParent or to the spell if it's empty
    /// </summary>
    public record SpellMetaAddChildEffects() : SpellMetaSchema
    {
        /// <summary>
        /// If the parent is the spell, then leave it at 0/empty
        /// </summary>
        public ObjectId effectParent = ObjectId.Empty;
        /// <summary>
        /// where to insert the effects in the parent's effect list <br></br>
        /// 0 to insert at the start.
        /// int.max to insert at the end
        /// </summary>
        public int index = int.MaxValue;
        public IEffectSchema copy() => new SpellMetaAddChildEffects()
        {
            effectParent = effectParent,
            index = index
        };
    }
    #endregion


    #region Effect - need to be child of SpellMetaModifyEffect
    public record SpellMetaEffectAddBaseDamage() : SpellMetaSchema
    {
        public int addDmg { get; set; } = 0;
        public IEffectSchema copy() => new SpellMetaEffectAddBaseDamage()
        {
            addDmg = addDmg,
        };
    }
    public record SpellMetaEffectAddBaseHeal() : SpellMetaSchema
    {
        public int addHeal { get; set; } = 0;
        public IEffectSchema copy() => new SpellMetaEffectAddBaseHeal()
        {
            addHeal = addHeal,
        };
    }
    public record SpellMetaEffectChangeElement() : SpellMetaSchema
    {
        public ElementType output { get; set; } = ElementType.None;
        public int percentConversion { get; set; } = 100;
        public IEffectSchema copy() => new SpellMetaEffectChangeElement()
        {
            output = output,
            percentConversion = percentConversion
        };
    }
    public record SpellMetaEffectChangeZone() : SpellMetaSchema
    {
        public IZone zone { get; set; } = new Zone();
        public IEffectSchema copy() => new SpellMetaEffectChangeZone()
        {
            zone = zone.copy()
        };
    }
    public record SpellMetaEffectChangeVariance() : SpellMetaSchema
    {
        public int percentVariance { get; set; } = 0;
        public IEffectSchema copy() => new SpellMetaEffectChangeVariance()
        {
            percentVariance = percentVariance,
        };
    }
    public record SpellMetaEffectChangePenetration() : SpellMetaSchema
    {
        public int percentPenetration { get; set; } = 0;
        public IEffectSchema copy() => new SpellMetaEffectChangePenetration()
        {
            percentPenetration = percentPenetration,
        };
    }
    #endregion


}
