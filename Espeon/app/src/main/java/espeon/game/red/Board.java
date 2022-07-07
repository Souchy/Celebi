package espeon.game.red;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import espeon.emerald.Constants;
import espeon.util.Table;

public class Board {
    
    private int counter = 0;
    public Table<Cell> cells = new Table<>(24, 24, null);

    public Board() {
        for(int i = 0; i < cells.size(); i++) {
            cells.set(i, new Cell());
        }
    }
    
    public Cell get(int cellid) {
        return cells.get(cellid);
    }
    public Cell get(int x, int y) {
        return cells.get(y, x);
    }

    public List<Cell> getCellsInAoe(int cellid, Aoe aoe) {
        List<Cell> list = new ArrayList<>();
        for(int i = 0; i < aoe.size(); i++) {
            // TargetTypeFilter f = aoe.get(i);
            int u = aoe.getCol(i);
            int v = aoe.getRow(i);
            int x = cells.getCol(cellid) + u;
            int y = cells.getRow(cellid) + v;
            Cell cell = get(x, y);
            list.add(cell);
            // int creatureid = cell.getBottom();
        }
        return list;
    }

    public class Cell extends Entity {
        public final int id = counter++;
        public List<Status> status = new ArrayList<>();
        // public LinkedList<Integer> creatures = new LinkedList<>();
        public Map<Integer, Integer> creatures = new HashMap<>();
        public int getGround() {
            if(creatures.size() == 0) return Creature.noid;
            return creatures.get(0);
        }
        public int getBottomMost() {
            if(creatures.size() == 0) return Creature.noid;
            for(int i = Constants.maxDepth(); i <= Constants.maxHeight(); i++) {
                if(creatures.containsKey(i)) {
                    return creatures.get(i);
                }
            }
            return Creature.noid;
        }
        public int getTopMost() {
            if(creatures.size() == 0) return Creature.noid;
            for(int i = Constants.maxHeight(); i >= Constants.maxDepth(); i--) {
                if(creatures.containsKey(i)) {
                    return creatures.get(i);
                }
            }
            return Creature.noid;
        }
        public Collection<Integer> getCreatures() {
            return creatures.values();
        }
        public int getX() {
            return cells.getCol(id);
        }
        public int getY() {
            return cells.getRow(id);
        }
        @Override
        public EntityType type() {
            return EntityType.cell;
        }
    }

    public Cell findCreatureCell(int sourceid) {
        return cells.findOne(c -> c.getBottomMost() == sourceid);
    }

}
