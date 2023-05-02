using souchy.celebi.eevee.face.objects;
using souchy.celebi.eevee.face.objects.effectResults;
using souchy.celebi.eevee.face.objects.controllers;
using souchy.celebi.eevee.face.objects.effects.move;
using souchy.celebi.eevee.face.util;
using souchy.celebi.eevee.impl.objects;
using souchy.celebi.eevee.impl.shared.triggers;
using souchy.celebi.eevee.impl.objects.effectReturn;
using souchy.celebi.eevee.face.shared.zones;
using souchy.celebi.eevee.face.entity;

namespace souchy.celebi.eevee.impl.objects.effects.move;

public class EffectWalk : Effect, IEffectWalk
{
    private EffectWalk() { }
    private EffectWalk(IID id) : base(id) { }
    public static IEffectWalk Create() => new EffectWalk(Eevee.RegisterIID<IEffect>());

    public override IEffectPreview preview(IAction action, IEnumerable<IBoardEntity> targets)
    {
        throw new NotImplementedException();
    }

    public override IEffectReturnValue apply(IAction action, IEnumerable<IBoardEntity> targets)
    {
        // calculate the path to walk to targetCell
        // for all the cells in the path {
        //      make a compiledEffect 
        //      give it the MoveType = Walk
        //      then a trigger can react to the compiledEffect with MoveType=walk on the cell
        throw new NotImplementedException();
    }

}
