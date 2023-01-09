package espeon.game.jade;

import java.util.List;

import com.souchy.randd.commons.tealwaters.commons.Lambda;

import espeon.game.jade.effects.moves.MoveEffect.MoveType;
import espeon.game.types.EffectType;

public class Trigger {
    
    // public Lambda lambda;
    
    // public List<Statement> statements;
    public int actionid;


    // public Trigger(Lambda lambda) {
    //     this.lambda = lambda;
    // }

    public static enum TriggerTarget {
        holder,
        trigerrer;
    }

    public static class OnEffect extends Trigger {
        // int effectTypeId;
        EffectType type;
        boolean ifSource; 
        boolean ifTarget;
        public OnEffect() { //int effectTypeId) {
            // this.effectTypeId = effectTypeId;
        }
    }
    
    public static class OnTimeline extends Trigger {
        boolean onTurn = false;
        boolean onRound = false;
        boolean onStart = false;
        boolean onEnd = false;
        public OnTimeline() {
        }
    }

    public static class OnMove extends Trigger {
        public MoveType type;
        public OnMove() {
        }
    }

}
