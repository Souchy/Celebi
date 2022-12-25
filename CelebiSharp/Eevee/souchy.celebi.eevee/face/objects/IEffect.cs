using souchy.celebi.eevee.face.conditions;
using souchy.celebi.eevee.face.entity;
using souchy.celebi.eevee.face.triggers;
using souchy.celebi.eevee.face.zones;
using souchy.celebi.eevee.interfaces;

namespace souchy.celebi.eevee.face.objects
{
    public interface IEffect : IEntityModeled
    {
        public ITargetFilter targetFilter { get; set; }
        public ICondition condition { get; set; }
        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }


    }
}