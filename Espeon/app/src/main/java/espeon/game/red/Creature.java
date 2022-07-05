package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

public class Creature {

    public static final int noid = 0;

    private static int counter = 1;

    public final int id = counter++;
    public int modelid;

    public Stats stats;
    public List<Integer> spells;
    

    public Creature copy() {
        Creature c = new Creature();
        // c.id = id * 10;
        c.modelid = modelid;
        c.stats = stats.copy();
        c.spells = new ArrayList<>(spells);
        return c;
    }

}
