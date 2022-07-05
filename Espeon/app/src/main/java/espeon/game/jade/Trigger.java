package espeon.game.jade;

import com.souchy.randd.commons.tealwaters.commons.Lambda;

public class Trigger {
    Lambda lambda;

    public Trigger(Lambda lambda) {
        this.lambda = lambda;
    }

    public static class OnEffect extends Trigger {
        int effectTypeId;
        boolean ifSource; 
        boolean ifTarget;
        public OnEffect(Lambda lambda, int effectTypeId) {
            super(lambda);
            this.effectTypeId = effectTypeId;
        }
    }
    
    public static class OnTimeline extends Trigger {
        boolean onTurn = false;
        boolean onRound = false;
        boolean onStart = false;
        boolean onEnd = false;
        public OnTimeline(Lambda lambda) {
            super(lambda);
        }
    }

}
