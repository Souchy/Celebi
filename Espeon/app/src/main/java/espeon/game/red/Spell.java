package espeon.game.red;

import java.util.HashMap;
import java.util.Map;

public class Spell {
    
    private static int counter = 1;

    public final int id = counter++;
    public int modelid;
    public Memory memory;

    public class Memory {
        public Map<Integer, Integer> castsPerTarget = new HashMap<>();
        public int castsThisTurn;
        public int lastTurnCast;
    };
}
