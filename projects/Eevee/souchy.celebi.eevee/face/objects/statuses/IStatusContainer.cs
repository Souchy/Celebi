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
        public IID sourceSpellModel { get; set; }
        /// <summary>
        /// Creature who applied the status
        /// </summary>
        public ObjectId sourceCreature { get; set; }
        /// <summary>
        /// Entity on which the status is 
        /// </summary>
        public ObjectId holderEntity { get; set; }
        /// <summary>
        /// Container stats like Stacks, MaxStacks, 
        /// </summary>
        public ObjectId stats { get; set; }

        public IStats GetStats() => this.GetFight().stats.Get(stats);

        public List<IStatusInstance> instances { get; set; }
    }
}
