using souchy.celebi.eevee.face.controllers;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.stats;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee
{
    public interface ICreature : IBoardEntity
    {
        /// <summary>
        /// Current owner of the creature
        /// </summary>
        public IID playerUid { get; set; }
        public IStats stats { get; set; }
        public List<IID> spells { get; set; }
    }
}