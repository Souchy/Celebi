package espeon.game.jade;

import java.util.List;

import espeon.game.red.Aoe;
import espeon.game.types.Mod;

public class SpellModel {
    
    public String id;
    public SpellConditions conditions;
    public List<Cost> costs;
    
    public String actionid;

    
    public class SpellConditions {
        public Aoe cellConditions = new Aoe(1,1);
        public int castPerTarget = 0;
        public int castPerTurn = 0;
        public int cooldown = 0;
        public int range = 1;
    };

    public class Cost {
        public Mod resource;
        public int amount;
    };

}
