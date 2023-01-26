using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.util;

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
        public List<IID> spells { get; set; }
    }
}