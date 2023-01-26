using souchy.celebi.eevee.face.objects;

namespace souchy.celebi.eevee.face.effects.special
{
    /// <summary>
    /// Every child of this effect will use the same zone as this one's.
    /// In other words, all children take the zone of this.
    /// So you dont have to define the same zone multiple times.
    /// </summary>
    public interface IEffectCopyZone : IEffect
    {
        // wait actually i dont need to do this do i? 
        // wouldnt children already have the same targets or something? 

        // actually i think the main goal of having children is to divide contexts
        //      so if you effect P already has a bunch of targets, they will be listed in the context
        //      and you can reuse them in your children

    }
}
