package espeon.game.red;

import java.util.Map;
import espeon.game.jade.Mod;

public class Stats {
    
    public Map<Mod, Integer> stats;
    
    public Stats() {
        for(int m = 0; m < Mod.values().length; m++) {
            stats.put(Mod.values()[m], 0);
        }
    }

    public int get(Mod mod) {
        return stats.get(mod);
    }

    public void set(Mod mod, int val) {
        stats.put(mod, val);
    }

    public void add(Mod mod, int val) {
        int old = get(mod);
        stats.put(mod, old + val);
    }
    
    public void sub(Mod mod, int val) {
        int old = get(mod);
        stats.put(mod, old - val);
    }

}
