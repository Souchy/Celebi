package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

import espeon.game.jade.Statement;
import espeon.util.IDGenerator;

/**
 * Used in Spells and status Triggers
 */
public class Action {
    
    // private static IDGenerator counter = new IDGenerator();
    // public final int id = counter.get();

    public String id;

    public List<Statement> statements = new ArrayList<>();

}
