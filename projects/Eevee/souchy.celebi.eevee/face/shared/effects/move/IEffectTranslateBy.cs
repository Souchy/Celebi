using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public interface IEffectTranslateBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }


    public interface IEffectPullBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }
    public interface IEffectPushBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }
    public interface IEffectDashBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }
    public interface IEffectDashAwayBy : IEffect
    {
        public IValue<IPosition> delta { get; set; }
    }


}
