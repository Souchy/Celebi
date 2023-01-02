using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.stats
{
    public interface IStatResource : IStat, IValue<(int current, int currentMax, int initialMax)>
    {
        /// <summary>
        /// Current value
        /// </summary>
        public int current { get; set; }
        /// <summary>
        /// Current Max in the state of the fight, could be modified (+buffs, -erosion...)
        /// </summary>
        public int currentMax { get; set; }
        /// <summary>
        /// Initial Max at the start of the fight
        /// </summary>
        public int initialMax { get; set; }
        /// <summary>
        /// Computed value from (currentMax - current)
        /// </summary>
        public int missing { get => currentMax - current; }
    }



}
