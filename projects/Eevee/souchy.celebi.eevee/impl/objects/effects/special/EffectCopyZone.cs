using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.special;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.special
{
    /// <summary>
    /// Every child of this effect will use the same zone as this one's.
    /// In other words, all children take the zone of this.
    /// So you dont have to define the same zone multiple times.
    /// </summary>
    public class EffectCopyZone : Effect, IEffectCopyZone
    {
        // wait actually i dont need to do this do i? 
        // wouldnt children already have the same targets or something? 

        // actually i think the main goal of having children is to divide contexts
        //      so if you effect P already has a bunch of targets, they will be listed in the context
        //      and you can reuse them in your children

        private EffectCopyZone() { }
        private EffectCopyZone(IID id) : base(id) { }
        public static IEffectCopyZone Create() => new EffectCopyZone(Eevee.RegisterIID<IEffect>());

        public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets) {
            throw new NotImplementedException();
        }

        public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
        {
            throw new NotImplementedException();
        }
    }
}
