package espeon.game.jade;

import java.util.List;

public abstract class Condition {

    public Actor actor;
    public List<Condition> children;
    public ConditionLink childLink;

    public Condition() {
    }
    
    public ConditionType type() {
        if(this instanceof StatCondition) return ConditionType.stat;
        else
        if(this instanceof TimelineCondition) return ConditionType.timeline;
        throw new RuntimeException("Illegal Condition Type: " + this.getClass().getCanonicalName());
    }

    public boolean compare(ComparisonOperator e, int a, int b) {
        switch (e) {
            case eq:
            return a == b;
            case gt:
            return a > b;
            case ge:
            return a >= b;
            case lt:
            return a < b;
            case le:
            return a <= b;
        }
        throw new RuntimeException("Switch case unsupported for " + e);
    }


    public static enum ConditionType {
        stat,
        timeline;
    }

    public static enum ComparisonOperator {
        eq,
        gt,
        ge,
        lt,
        le;
    }

    public static enum Actor {
        source,
        target,
        accumulator;
    }
    
    public static enum ConditionLink {
        and,
        or
    };

    public static class StatCondition extends Condition {
        public Mod mod;
        public int val;
        public ComparisonOperator op;
        // @Override
        // boolean verify() {
        //     return false;
        // }
    }

    public static class TimelineCondition extends Condition {
        
    }

}
