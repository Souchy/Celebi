using souchy.celebi.eevee.values;
using System.Collections.Generic;

namespace souchy.celebi.eevee.face.stats
{
    public interface IStat
    {
        public int get();
        public int set(int value);
    }

    public interface IStatSimple : IStat, IValue<int>
    {
    }

    public interface IStatDetailed<T> : IStat, IValue<(T current, T currentMax, T initialMax)>
    {
        /// <summary>
        /// Current value
        /// </summary>
        public int current { get; set; }
        /// <summary>
        /// Max in the current state of the fight, could've been modified (buffs, erosion...)
        /// </summary>
        public T currentMax { get; set; }
        /// <summary>
        /// Max at the start of the fight
        /// </summary>
        public T initialMax { get; init; }
        /// <summary>
        /// Computed value from (currentMax - current)
        /// </summary>
        public T getMissing();
    }



}
