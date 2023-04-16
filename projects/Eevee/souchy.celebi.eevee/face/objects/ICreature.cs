﻿using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.stats;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;

namespace souchy.celebi.eevee.face.objects
{
    public interface ICreature : IBoardEntity
    {
        /// <summary>
        /// Original owner of the creature (set once)
        /// </summary>
        public IID originalOwnerUid { get; set; }
        /// <summary>
        /// Current owner of the creature
        /// </summary>
        public IID currentOwnerUid { get; set; }
        public IID stats { get; set; }
        public IEntitySet<IID> spells { get; set; }


        public IPlayer GetOriginalOwner();
        public IPlayer GetCurrentOwner();
        public IStats GetBaseStats();
        public IStats GetStats(IAction action, TriggerEvent trigger);
        public IEnumerable<ISpell> GetSpells();

    }
}