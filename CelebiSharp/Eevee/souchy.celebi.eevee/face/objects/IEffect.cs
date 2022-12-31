using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.triggers;
using souchy.celebi.eevee.face.zones;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.objects
{
    public interface IEffect : IEntityModeled
    {
        /// <summary>
        /// If the source condition doesnt match, the entire effect is not applied
        /// </summary>
        public ICondition sourceCondition { get; set; }
        /// <summary>
        /// Condition for each target
        /// </summary>
        public ICondition targetFilter { get; set; }
        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }


    }
}