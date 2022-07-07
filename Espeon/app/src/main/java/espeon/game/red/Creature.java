package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

public class Creature extends Entity {

    public int modelid;

    public Stats stats;
    public List<Integer> spells;
    public List<Status> status;
    

    public Creature copy() {
        Creature c = new Creature();
        // c.id = id * 10;
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
