package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

import espeon.util.IDGenerator;

public abstract class Entity {

    public static enum EntityType {
        cell,
        creature;
    }

    // private static IDGenerator counter = new IDGenerator();
    public static final int noid = -1;
    
    public final int fightid;
    public final int id; // = counter.get();
    public List<Status> status = new ArrayList<>();

    public Entity(int fightid, int id) {
        this.fightid = fightid;
        this.id = id;
    }

    public abstract EntityType type();
}
