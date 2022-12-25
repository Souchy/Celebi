using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.stats;

namespace souchy.celebi.eevee.face.statuses
{
    /// <summary>
    /// Each status instance is 1 stack
    /// The client can just visually "merge" them on the UI ?
    /// </summary>
    public interface IStatus : IEntityModeled
    {
        /// <summary>
        /// That or some kind of ID that allows merging of stacks/instances
        /// </summary>
        //public int sourceSpell { get; set; }
        public ICreatureInstance source { get; set; }
        public IBoardEntity holder { get; set; }


        public IStatDetailed<int> delay { get; set; }
        public IStatDetailed<int> duration { get; set; }

        public List<IEffect> effects { get; set; }

    }

}