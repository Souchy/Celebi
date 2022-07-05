package espeon.game;

import espeon.game.jade.Condition;
import espeon.game.jade.Condition.StatCondition;
import espeon.game.jade.Condition.TimelineCondition;
import espeon.game.red.ActionPipeline;

public class Conditions {
    
    public static boolean verify(Condition c, ActionPipeline p) {
        switch(c.type) {
            case stat:
                return verifyStatsCondition((StatCondition) c, p);
            case timeline:
                return verifyTimelineCondition((TimelineCondition) c, p);
        }
        throw new IllegalArgumentException("Condition type is unmanaged: " + c.type);
    }

    private static boolean verifyStatsCondition(StatCondition c, ActionPipeline p) {
        return true;
    }
    
    private static boolean verifyTimelineCondition(TimelineCondition c, ActionPipeline p) {
        return true;
    }

}
