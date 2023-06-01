﻿using souchy.celebi.eevee.enums.characteristics.other;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.shared;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.stats;

namespace souchy.celebi.eevee.face.objects.statuses
{
    /// <summary>
    /// Instance (1 stack) inside a IStatusContainer
    /// </summary>
    public interface IStatusInstance : IFightEntity, IEntityModeled, IEffectsContainer
    {
        //public IValue<int> delay { get; set; } // IStatSimple
        //public IValue<int> duration { get; set; }

        public ObjectId statsId { get; set; }
        public StatusInstanceStats GetStats() => (StatusInstanceStats) this.GetFight().stats.Get(statsId);
    }

}