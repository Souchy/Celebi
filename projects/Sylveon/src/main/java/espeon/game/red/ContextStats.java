package espeon.game.red;

import java.util.HashMap;
import java.util.Map;

import espeon.game.types.ContextMod;

public class ContextStats {
	
    private Map<ContextMod, Integer> map;
    
    public ContextStats() {
        map = new HashMap<>();
        for(var m : ContextMod.values()) {
            set(m, 0);
        }
    }

    public int get(ContextMod mod) {
        return map.get(mod);
    }

    public void set(ContextMod mod, int val) {
        map.put(mod, val);
    }

    public void add(ContextMod mod, int val) {
        int old = get(mod);
        map.put(mod, old + val);
    }
    
    public void sub(ContextMod mod, int val) {
        int old = get(mod);
        map.put(mod, old - val);
    }

    public void add(ContextStats stats) {
        for(var m : ContextMod.values()) {
            add(m, stats.get(m));
        }
    }

    public ContextStats copy() {
        ContextStats s = new ContextStats();
        s.map = new HashMap<>(map);
        return s;
    }


}
