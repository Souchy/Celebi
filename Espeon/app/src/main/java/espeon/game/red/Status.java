package espeon.game.red;

import java.util.List;

import espeon.game.jade.Trigger;

public class Status {
    
    public int id;
    public int source;
    public int spellModelSource;

    public Stats stats;
    
    public int stacks;
    public int duration;
    public boolean canStack;
    public boolean canRefresh;

    
    // triggers for the list of effects
    public List<Trigger> triggers;
 
}
