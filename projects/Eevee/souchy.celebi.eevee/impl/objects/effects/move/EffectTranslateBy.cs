using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.face.util.math;
using souchy.celebi.eevee.face.values;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.values;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.move
{
    /// <summary>
    /// Translate by X in the direction of the target
    /// </summary>
    public class EffectTranslateBy : Effect, IEffectTranslateBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        private EffectTranslateBy() { }
        private EffectTranslateBy(ObjectId id) : base(id) { }
        public static IEffectTranslateBy Create() => new EffectTranslateBy(Eevee.RegisterIIDTemporary());
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Pull by X in the direction of the source
    /// </summary>
    public class EffectPullBy : Effect, IEffectPullBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        private EffectPullBy() { }
        private EffectPullBy(ObjectId id) : base(id) { }
        public static IEffectPullBy Create() => new EffectPullBy(Eevee.RegisterIIDTemporary());
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectPushBy : Effect, IEffectPushBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        private EffectPushBy() { }
        private EffectPushBy(ObjectId id) : base(id) { }
        public static IEffectPushBy Create() => new EffectPushBy(Eevee.RegisterIIDTemporary());
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectDashBy : Effect, IEffectDashBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        private EffectDashBy() { }
        private EffectDashBy(ObjectId id) : base(id) { }
        public static IEffectDashBy Create() => new EffectDashBy(Eevee.RegisterIIDTemporary());
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
    public class EffectDashAwayBy : Effect, IEffectDashAwayBy
    {
        public IValue<IPosition> Delta { get; set; } = new Value<IPosition>();

        private EffectDashAwayBy() { }
        private EffectDashAwayBy(ObjectId id) : base(id) { }
        public static IEffectDashAwayBy Create() => new EffectDashAwayBy(Eevee.RegisterIIDTemporary());
        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }


}
