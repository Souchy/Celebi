using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.objects.stats
{
    public interface IStatResource : IStat, IValue<(int current, int currentMax, int initialMax)>
    {
        /// <summary>
        /// Current value
        /// </summary>
        public int current { get; init; }
        /// <summary>
        /// Current Max in the state of the fight, could be modified (+buffs, -erosion...)
        /// </summary>
        public int currentMax { get; init; }
        /// <summary>
        /// Initial Max at the start of the fight
        /// </summary>
        public int initialMax { get; init; }
        /// <summary>
        /// Computed value from (currentMax - current)
        /// </summary>
        public int missing { get => currentMax - current; }
    }



}
