package espeon.game.controllers;

import espeon.game.controllers.actionpipeline.NewPipeline;
import espeon.game.jade.Condition;
import espeon.game.jade.Condition.StatCondition;
import espeon.game.jade.Condition.TimelineCondition;

public class Conditions {
    
    public static boolean verify(Condition c, NewPipeline p) {
        switch(c.type()) {
            case stat:
                return verifyStatsCondition((StatCondition) c, p);
            case timeline:
                return verifyTimelineCondition((TimelineCondition) c, p);
        }
        throw new IllegalArgumentException("Condition type is unmanaged: " + c.type());
    }

    private static boolean verifyStatsCondition(StatCondition c, NewPipeline p) {
        return true;
    }
    
    private static boolean verifyTimelineCondition(TimelineCondition c, NewPipeline p) {
        return true;
    }

}
