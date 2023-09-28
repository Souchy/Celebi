using souchy.celebi.eevee.enums.characteristics.creature;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.impl.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects
{
    public interface IActionResult //: IFightEntity
    {
    }
    public class SpellResult : IActionResult
    {

    }
    public class EffectResult : IActionResult
    {

    }
    public class EffectTargetResult : IActionResult
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class EffectTargetResourceResult : EffectTargetResult
    {
        public Dictionary<Resource, int> resources { get; set; } = new();
    }
    /// <summary>
    /// 
    /// </summary>
    public class EffectTargetPositionResult : EffectTargetResult
    {
        public ObjectId cellId { get; set; } // that or Position instance (Vector2d)
    }
    /// <summary>
    /// 
    /// </summary>
    public class EffectTargetStatusResult : EffectTargetResult
    {
        // TODO ???
    }
    /// <summary>
    /// 
    /// </summary>
    public class EffectTargetSwapResult : EffectTargetResult
    {
        public ObjectId creatureSwapIn { get; set; }
        public ObjectId creatureSwapOut { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class EffectTargetAddInstantStatsResult : EffectTargetResult
    {
        /// <summary>
        /// What stats will receive the update (ex: could be a SpellInstance's stats)
        /// </summary>
        public ObjectId statsTarget { get; set; }
        /// <summary>
        /// How much to add
        /// </summary>
        public IStats addStats { get; set; }
    }

}
