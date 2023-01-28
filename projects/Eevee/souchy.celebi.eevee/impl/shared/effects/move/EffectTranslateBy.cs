using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.shared.effects;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public class EffectTranslateBy : Effect, IEffectTranslateBy
    {
        public IValue<IPosition> delta { get; set; }
    }


    public class EffectPullBy : Effect, IEffectPullBy
    {
        public IValue<IPosition> delta { get; set; }
    }
    public class EffectPushBy : Effect, IEffectPushBy
    {
        public IValue<IPosition> delta { get; set; }
    }
    public class EffectDashBy : Effect, IEffectDashBy
    {
        public IValue<IPosition> delta { get; set; }
    }
    public class EffectDashAwayBy : Effect, IEffectDashAwayBy
    {
        public IValue<IPosition> delta { get; set; }
    }


}
