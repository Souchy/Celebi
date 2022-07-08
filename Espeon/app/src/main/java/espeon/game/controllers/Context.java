package espeon.game.controllers;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import espeon.game.red.Stats;
import espeon.game.red.Status;

public class Context {

    // fight id
    public int fightid;
    // stats per entity id
    public Map<Integer, Stats> stats = new HashMap<>();
    // cellid per entity id
    public Map<Integer, Integer> positions = new HashMap<>();
    // statuses per entity id
    public Map<Integer, StatusList> status = new HashMap<>();


    public Context copy() {
        Context copy = new Context();
        copy.stats = new HashMap<>(stats);
        copy.positions = new HashMap<>(positions);
        return null;
    }



    public class StatusList {
        public List<Status> list;
        public Stats getTotalStats() {
            return null;
        }
    }
    
}
