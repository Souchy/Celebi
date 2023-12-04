using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace souchy.celebi.eevee.face.objects.statuses
{
    /// <summary>
    /// Linked to either the StatusModelID or the SpellModelID
    /// </summary>
    public interface IStatusContainer : IEntityModeled, IFightEntity
    {
        /// <summary>
        /// That or some kind of ID that allows merging of stacks/instances
        /// </summary>
        public ObjectId sourceSpellModel { get; set; }
        /// <summary>
        /// Statuses come from effects that apply them.
        /// </summary>
        public ObjectId sourceEffectPermanent { get; set; }
        /// <summary>
        /// Creature who applied the status
        /// </summary>
        public ObjectId sourceCreature { get; set; }
        /// <summary>
        /// Entity on which the status is 
        /// </summary>
        public ObjectId holderEntity { get; set; }
        /// <summary>
        /// Container stats like MergeStrategy, MaxStacks, MaxDuration, MaxDelay, 
        /// </summary>
        public ObjectId statsId { get; set; }

        public StatusContainerStats GetStats() => (StatusContainerStats) this.GetFight().stats.Get(statsId);

        public List<IStatusInstance> instances { get; set; }
    }
}
