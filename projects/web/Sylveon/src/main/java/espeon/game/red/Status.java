package espeon.game.red;

import java.util.ArrayList;
import java.util.List;
import espeon.game.jade.Trigger;

public class Status {
    
    public int id;
    public int source;
    public int spellModelSource;

    public MergeStrategy mergeStrategy;
    public List<Status> stacks = new ArrayList<>();
    // public Map<StatusMod, Integer> mods = new HashMap<>();
	public int maxStacks = 0;

    public int stacks() {
        return stacks.size();
    }

	
    public class StatusStack {
		/**
		 * each stack can have a different value. 
		 * ex: fourberie donne 80 ou 100 int en normal/crit
		 */
        public Stats stats; 
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
