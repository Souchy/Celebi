package espeon.game.red;

import java.util.HashMap;
import java.util.Map;

import espeon.game.types.Mod;

public class Stats {
    
    private Map<Mod, Integer> map;
    
    public Stats() {
        map = new HashMap<>();
        for(var m : Mod.values()) {
            set(m, 0);
        }
    }

    public int get(Mod mod) {
        return map.get(mod);
    }

    public void set(Mod mod, int val) {
        map.put(mod, val);
    }

    public void add(Mod mod, int val) {
        int old = get(mod);
        map.put(mod, old + val);
    }
    
    public void sub(Mod mod, int val) {
        int old = get(mod);
        map.put(mod, old - val);
    }

    public void add(Stats stats) {
        for(var m : Mod.values()) {
            add(m, stats.get(m));
        }
    }

    public Stats copy() {
        Stats s = new Stats();
        s.map = new HashMap<>(map);
        return s;
    }

}
