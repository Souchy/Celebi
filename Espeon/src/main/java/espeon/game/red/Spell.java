package espeon.game.red;

import java.util.HashMap;
import java.util.Map;

public class Spell {
    
    private static int counter = 1;
    public final int id = counter++;


    public int modelid;


    public int turnLastCast = 0;
    public int castsThisTurn = 0; // TODO reset after turn
    public Map<Integer, Integer> castsPerTargetThisTurn = new HashMap<>(); // TODO reset after turn
}
