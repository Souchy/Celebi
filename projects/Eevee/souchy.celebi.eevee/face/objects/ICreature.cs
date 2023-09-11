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
        public ObjectId? summoner { get; set; }
        /// <summary>
        /// Original player owner of the creature (set once)
        /// </summary>
        public ObjectId originalOwnerUid { get; set; }
        /// <summary>
        /// Current player owner of the creature
        /// </summary>
        public ObjectId currentOwnerUid { get; set; }
        /// <summary>
        /// Natural stats: Base + Growth from model
        /// </summary>
        public ObjectId statsId { get; set; }
        public IEntitySet<ObjectId> spellIds { get; set; }


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