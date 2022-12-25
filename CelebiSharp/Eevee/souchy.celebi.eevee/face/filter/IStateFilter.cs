using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.filter
{
    public interface IStateFilter
    {
        /// <summary>
        /// If this is empty, then every state is allowed expect the disallowed ones
        /// </summary>
        public IValue<int[]> allowStateModelIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IValue<int[]> disallowStateModelIds { get; set; }
    }
}
