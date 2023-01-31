using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.move
{
    public class EffectTranslateBy : Effect, IEffectTranslateBy
    {
        public IValue<IPosition> delta { get; set; }

        private EffectTranslateBy() { }
        private EffectTranslateBy(IID id) : base(id) { }
        public static IEffectTranslateBy Create() => new EffectTranslateBy(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }

    public class EffectPullBy : Effect, IEffectPullBy
    {
        public IValue<IPosition> delta { get; set; }

        private EffectPullBy() { }
        private EffectPullBy(IID id) : base(id) { }
        public static IEffectPullBy Create() => new EffectPullBy(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectPushBy : Effect, IEffectPushBy
    {
        public IValue<IPosition> delta { get; set; }

        private EffectPushBy() { }
        private EffectPushBy(IID id) : base(id) { }
        public static IEffectPushBy Create() => new EffectPushBy(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectDashBy : Effect, IEffectDashBy
    {
        public IValue<IPosition> delta { get; set; }

        private EffectDashBy() { }
        private EffectDashBy(IID id) : base(id) { }
        public static IEffectDashBy Create() => new EffectDashBy(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectDashAwayBy : Effect, IEffectDashAwayBy
    {
        public IValue<IPosition> delta { get; set; }

        private EffectDashAwayBy() { }
        private EffectDashAwayBy(IID id) : base(id) { }
        public static IEffectDashAwayBy Create() => new EffectDashAwayBy(Eevee.RegisterIID<IEffect>());
        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }


}
