package espeon.game.red;

import java.util.Collection;
import java.util.HashMap;
import java.util.Map;

import espeon.emerald.Constants;

public class Cell extends Entity {
    
    public Cell(int fightid, int id) {
        super(fightid, id);
    }

    public Map<Integer, Integer> creatures = new HashMap<>();

    public void setGround(int creatureid) {
        this.creatures.put(0, creatureid);
    }
    

    public int getGround() {
        if(creatures.size() == 0) return Entity.noid;
        return creatures.get(0);
    }


    public int getBottomMost() {
        if(creatures.size() == 0) return Entity.noid;
        for(int i = Constants.maxDepth(); i <= Constants.maxHeight(); i++) {
            if(creatures.containsKey(i)) {
                return creatures.get(i);
            }
        }
        return Entity.noid;
    }

    public int getTopMost() {
        if(creatures.size() == 0) return Entity.noid;
        for(int i = Constants.maxHeight(); i >= Constants.maxDepth(); i--) {
            if(creatures.containsKey(i)) {
                return creatures.get(i);
            }
        }
        return Entity.noid;
    }

    public Collection<Integer> getCreatures() {
        return creatures.values();
    }

    @Override
    public EntityType type() {
        return EntityType.cell;
    }
}
