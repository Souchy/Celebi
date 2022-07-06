package espeon.game.red;

import java.util.List;

import espeon.game.jade.Statement;
import espeon.util.IDGenerator;

public abstract class Action {
    
    private static IDGenerator counter = new IDGenerator();

    public final int id = counter.get();

    public List<Statement> statements;

}
