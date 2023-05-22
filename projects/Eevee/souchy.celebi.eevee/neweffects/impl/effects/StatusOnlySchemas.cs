using souchy.celebi.eevee.enums.characteristics;
using souchy.celebi.eevee.enums.characteristics.creature;
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
    public record AddStatSchema() : IEffectSchema {
        public IStat stat { get; set; } = Affinity.Fire.Create(0);
    }
    public record LearnSpellSchema() : IEffectSchema
    {
        public SpellIID modelId { get; set; } 
    }
    public record ForgetSpellSchema() : IEffectSchema {
        public SpellIID modelId { get; set; }
    }
    public record ChangeAppearance() : IEffectSchema
    {
        public AssetIID modelId { get; set; } // any asset file (scene, 3d model, texture, music...)
    }
    public record ChangeAnimationSet() : IEffectSchema {
        public AnimationSetIID modelId { get; set; } 
    }
    public record ReduceDamageReceived() : IEffectSchema
    {
        public int reduction { get; set; } = 0;
    }
    #endregion

    #region Spell
    public record AddSpellRange() : IEffectSchema
    {
        public int range { get; set; } = 0;
    }
    public record SetSpellLineOfSight() : IEffectSchema {
        public bool value { get; set; } = false;
    }
    public record AddSpellEffectBaseDamage() : IEffectSchema {
        public int dmg { get; set; } = 0;
    }
    public record ChangeSpellRangeZone() : IEffectSchema
    {
        public IZone zone { get; set; } = new Zone();
    }
    public record ChangeSpellEffectZone() : IEffectSchema
    {
        public IZone zone { get; set; } = new Zone();
    }
    #endregion

    #region Board
    public record BuildObstacle() : IEffectSchema {
        //obstacle3dModelUid
        public AssetIID modelId { get; set; }
    }
    public record DestroyObstacle() : IEffectSchema { }
    public record DigHole() : IEffectSchema { }
    public record FillHole() : IEffectSchema { }
    #endregion

}
