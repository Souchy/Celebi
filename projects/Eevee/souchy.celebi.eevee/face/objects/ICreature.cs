using souchy.celebi.eevee.face.entity;
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
        public ObjectId originalOwnerUid { get; set; }
        /// <summary>
        /// Current owner of the creature
        /// </summary>
        public ObjectId currentOwnerUid { get; set; }
        /// <summary>
        /// Natural stats: Base + Growth from model
        /// </summary>
        public ObjectId stats { get; set; }
        public IEntitySet<ObjectId> spells { get; set; }


        public IPlayer GetOriginalOwner();
        public IPlayer GetCurrentOwner();

        /// <summary>
        /// Base + Growth stats
        /// </summary>
        public IStats GetNaturalStats();
        /// <summary>
        /// Base + Growth + Status stats
        /// </summary>
        public IStats GetTotalStats(IAction action); //, TriggerEvent trigger);

        public IEnumerable<ISpell> GetSpells();

    }
}