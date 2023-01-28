using souchy.celebi.eevee.face.shared.conditions;
using souchy.celebi.eevee.face.shared.effects;
using souchy.celebi.eevee.face.shared.triggers;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.util;

namespace souchy.celebi.eevee.impl.shared.effects
{
    public class Effect : IEffect
    {
        public IID entityUid { get; init; } = Eevee.RegisterIID();
        public IID modelUid { get; set; }
        public IID fightUid { get; init; }
        public ICondition sourceCondition { get; set; }
        public ICondition targetFilter { get; set; }
        public IZone zone { get; set; }
        public List<ITrigger> triggers { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
