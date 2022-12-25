package espeon.game.jade.effects.moves;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;

public abstract class MoveEffect extends EffectModel {

    /*
     * MoveBy (by x cells amount) : Push, Pull, Dash, TeleportBy
     * MoveTo (to x cell which is the aoe single cell) : PushTo, PullTo, DashTo, TeleportTo
     * 
     * Teleportation: TeleportTo(cellDest), Swap(t1, t2)
     */
    public static enum MoveType {
        translation,
        teleportation;
    }

    public final MoveType moveType;

    public MoveEffect(MoveType type) {
        this.moveType = type;
    }

    @Override
    public EffectType type() {
        return EffectType.move;
    }
    
}
