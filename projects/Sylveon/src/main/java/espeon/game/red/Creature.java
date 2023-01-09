package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

/**
 * Dont forget : id, fightid and statuses are in Entity
 */
public class Creature extends Entity {

    public String ownerid;
    public int modelid;
    
    public Stats stats;
    // public int statsid;
    public List<Integer> spells;
    
    public Creature(int fightid, int id) {
        super(fightid, id);
    }


    public Creature copy(int newId) {
        Creature c = new Creature(fightid, newId);
        c.modelid = modelid;
        c.stats = stats.copy();
        c.spells = new ArrayList<>(spells);
        return c;
    }


    @Override
    public EntityType type() {
        return EntityType.creature;
    }

}
