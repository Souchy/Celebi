package espeon.game.jade;

import java.beans.Statement;
import java.util.List;

import espeon.game.red.Stats;

public class StatusModel {
    
    public int id;
    public int source;
    // ---
    public Stats stats;
    public List<Statement> statements;
    public List<Trigger> triggers;
    // triggers for the list of effects
 
    public int stacks;
    public int duration;
    public boolean canStack;
    public boolean canRefresh;
}
