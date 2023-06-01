using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects.zones;
using souchy.celebi.eevee.impl.stats;
using souchy.celebi.eevee.neweffects.face;

namespace souchy.celebi.eevee.neweffects.impl.effects.creature
{
    #region Creature
    //public record AddStat() : IEffectSchema {
    //    public IStat stat { get; set; } = Affinity.Fire.Create(0);
    //    //public IStats stats { get; set; }
    //}
    public record AddStats() : IEffectSchema
    {
        public IStats stats { get; set; } = Stats.Create();
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

    #region Spell

    /// <summary>
    /// This is instanced SpellStats <br></br>
    /// As opposed to SpellMeta's  SpellModelStats <br></br>
    /// Can be used to refresh the current cooldown, add charges..
    /// </summary>
    public record SpellAddtats() : IEffectSchema
    {
        public SpellStats stats { get; set; } = SpellStats.Create();
    }

    public interface SpellMetaSchema : IEffectSchema {}

    /// <summary>
    /// this should replace all the other Spell effects above, or maybe it will contain them
    /// This effect is a Meta effect, doesn't do anything on its own, just applies its children in a certain way
    /// Its children modify a spell:
    ///     - modify costs, range, filters, 
    ///     - modifly cooldown remaining
    ///     - add effect...
    ///     - modify effect damages, zones, 
    /// </summary>
    public record MetaSpell() : SpellMetaSchema
    {
        public SpellIID spell { get; set; } = new();
        public List<SpellMetaSchema> spellMods { get; set; } = new();

        // SpellStats have a range stat
        //public record AddSpellRange() : IEffectSchema
        //{
        //    public int range { get; set; } = 0;
        //}
        // LoS is definitely handled by SpellStats
        //public record SetSpellLineOfSight() : IEffectSchema {
        //    public bool value { get; set; } = false;
        //}
        public record SpellMetaEffectAddBaseDamage() : SpellMetaSchema
        {
            public ObjectId effectId { get; set; } = ObjectId.Empty;
            public int dmg { get; set; } = 0;
        }
        public record SpellMetaEffectAddBaseHeal() : SpellMetaSchema
        {
            public ObjectId effectId { get; set; } = ObjectId.Empty;
            public int heal { get; set; } = 0;
        }
        public record SpellMetaEffectChangeElement() : SpellMetaSchema
        {
            public ObjectId effectId { get; set; } = ObjectId.Empty;
            public ElementType element { get; set; } = ElementType.None;
        }
        public record SpellMetaEffectChangeZone() : SpellMetaSchema
        {
            public ObjectId effectId { get; set; } = ObjectId.Empty;
            public IZone zone { get; set; } = new Zone();
        }
        public record SpellMetaChangeMinRangeZone() : SpellMetaSchema
        {
            public IZone minRange { get; set; } = new Zone();
        }
        public record SpellMetaChangeMaxRangeZone() : SpellMetaSchema
        {
            public IZone maxRange { get; set; } = new Zone();
        }
        public record SpellMetaAddtats() : SpellMetaSchema
        {
            public SpellModelStats stats { get; set; } = SpellModelStats.Create();
        }
        /// <summary>
        /// will add the children of this effect to the spell
        /// </summary>
        public record SpellMetaAddChildEffects() : SpellMetaSchema
        {
            /// <summary>
            /// If the parent is the spell, then leave it at 0
            /// </summary>
            public ObjectId effectParent = ObjectId.Empty;
            /// <summary>
            /// where to insert the effects in the spell's list <br></br>
            /// 0 to insert at the start.
            /// int.max to insert at the end
            /// </summary>
            public int order = int.MaxValue;
            // actually just addd the children to the spell effects
            //public ObjectId newEffect { get; set; } = ObjectId.Empty;
        }

        //public record MetaAddEffectValue() : IEffectSchema
        //{
        //    //public SpellIID spell { get; set; } = new();
        //    public ObjectId effectId { get; set; } = ObjectId.Empty;
        //    /// <summary>
        //    /// ex: "schema.dmg", "schema.element", "zone.size.x", "zone.maxSampleCount"
        //    /// </summary>
        //    public string propertyPath { get; set; } = "";
        //    public object value { get; set; } = null;
        //}
    }
    #endregion

    #region Board
    public record BuildObstacle() : IEffectSchema {
        //obstacle3dModelUid
        public AssetIID modelId { get; set; } = new();
    }
    public record DestroyObstacle() : IEffectSchema { }
    public record DigHole() : IEffectSchema { }
    public record FillHole() : IEffectSchema { }
    #endregion

}
