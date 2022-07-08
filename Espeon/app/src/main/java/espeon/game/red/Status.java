package espeon.game.red;

import java.util.List;
import espeon.game.jade.Trigger;

public class Status {
    
    public int id;
    public int source;
    public int spellModelSource;

    public List<Status> instances;
    public MergeStrategy mergeStrategy;
    // public Map<StatusMod, Integer> mods = new HashMap<>();
    public int stacks() {
        return instances.size();
    }
    public int maxStacks = 0;

    public class StatusInstace {
        /**
         * These apply once per stack. Stats with 10 hp and 5 stacks mean 50 hp
         */
        public Stats stats; 
        // public List<Stats> stats; // this is interesting because each stack can have a different value. ex: fourberie donne 80 ou 100 int en normal/crit
        public int duration;
        public int maxDuration;
        // triggers for the list of effects
        public List<Trigger> triggers;
    }

 

    
    /*
     * Basic actions for status on turnEnd:
     *      - removeStack,
     *      - removeDuration,
     */

    public static enum StatusMod {
        stacks,
        maxStacks,
        duration,
        maxDuration;
    }

    public static enum MergeStrategyType {
        addStacks,
        addDuration,
        addStats,
        setStacks,
        setDuration,
        setStats,
        discard;
        public final int val = (int) Math.pow(2, ordinal());
        public static int defaul() {
            return addStacks.val | addDuration.val | setStats.val;
        }
        public static int addAll() {
            return addStacks.val | addDuration.val | addStats.val;
        }
        public static int setAll() {
            return setStacks.val | setDuration.val | setStats.val;
        }
    }
    public static class MergeStrategy {
        public int value = MergeStrategyType.defaul();
    }



}
