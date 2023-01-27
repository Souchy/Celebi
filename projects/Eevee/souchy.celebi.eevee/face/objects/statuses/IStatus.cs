﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.face.objects.statuses
{
    /// <summary>
    /// Each status instance is 1 stack
    /// The client can just visually "merge" them on the UI ?
    /// Use the model id to stack the instances
    /// </summary>
    public interface IStatus : IEntityModeled
    {
        /// <summary>
        /// That or some kind of ID that allows merging of stacks/instances
        /// </summary>
        public IID sourceSpell { get; set; }
        /// <summary>
        /// Creature who applied the status
        /// </summary>
        public IID sourceCreature { get; set; }
        /// <summary>
        /// Entity on which the status is 
        /// </summary>
        public IID holderEntity { get; set; }


        public IStatSimple delay { get; set; }
        public IStatSimple duration { get; set; }


        public List<IID> effectIds { get; set; }

    }

}