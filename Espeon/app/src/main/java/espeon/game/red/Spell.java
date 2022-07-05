package espeon.game.red;

import java.util.HashMap;
import java.util.Map;

public class Spell {
    
    public int id;
    public int modelid;
    public SpellMemory memory;

    private class SpellMemory {
        public Map<Integer, Integer> castsPerTarget = new HashMap<>();
        public int castsThisTurn;
        public int lastTurnCast;
    };
}
