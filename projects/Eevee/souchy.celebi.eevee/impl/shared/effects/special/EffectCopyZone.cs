using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.compiledeffects;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;

namespace souchy.celebi.eevee.face.shared.effects.special
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

        public override ICompiledEffect compile(IFight fight, IID source, IID targetCell)
        {
            throw new NotImplementedException();
        }
    }
}
