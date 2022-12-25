using souchy.celebi.eevee.values;

namespace souchy.celebi.eevee.face.filter
{
    public interface IStatusFilter
    {
        /// <summary>
        /// If this is empty, then every status is allowed expect the disallowed ones
        /// </summary>
        public IValue<int[]> allowStatusModelIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IValue<int[]> disallowStatusModelIds { get; set; }
    }
}
