package espeon.game.red;

import espeon.util.IDGenerator;

public abstract class Entity {

    public static enum EntityType {
        cell,
        creature;
    }

    private static IDGenerator counter = new IDGenerator();
    public static final int noid = -1;
    
    public final int id = counter.get();

    public abstract EntityType type();
    
}
