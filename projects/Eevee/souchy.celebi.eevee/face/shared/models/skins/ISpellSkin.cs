using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.shared.models.skins
{
    /// <summary>
    /// A spell skin can be re-used on almost any spell, because it's basically just a set of animations and vfx to play on spell cast
    /// </summary>
    public interface ISpellSkin : IEntity
    {
        //public SpellIID spellModelUid { get; set; }
        public AssetIID icon { get; set; }
        public AssetIID sourceAnimation { get; set; }
        public AssetIID targetAnimation { get; set; }
        /// <summary>
        /// Projectile? going from caster to targets or the opposite? 
        /// Aoe spawn at target? 
        /// 
        /// Spawn at Target or Spawn at Source? 
        /// Moves to target or move to source?
        /// 
        /// If we were talking about a status (or glyph, trap), would it stay for a duration?
        /// 
        /// Also at what moment do the damage numbers show up? when the projectile/thing hits the target... when is that? 
        /// 
        /// I think the 'behaviourScript' is supposed to be the Godot scene.
        ///     - it would include the vfx and code
        ///     - the godot code can tell the vfx what to do (from/to positions)
        ///         - and time everything right with the numbers popping up too
        ///     - we did have some generic ProjectileVFX scenes a while ago to implement/re-use
        ///     - if the client wants, they can deactivate the animations entirely i guess, all of this is just visual
        /// 
        /// </summary>
        public AssetIID behaviourScript { get; set; }
    }
}
