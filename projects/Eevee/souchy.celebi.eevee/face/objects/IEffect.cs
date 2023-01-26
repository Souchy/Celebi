using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.objects
{
    public interface IEffect : IEntityModeled
    {
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }


        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }
    }
}