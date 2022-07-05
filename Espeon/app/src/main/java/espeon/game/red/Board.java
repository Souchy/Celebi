package espeon.game.red;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.List;
import java.util.Stack;

import espeon.game.jade.StatusModel;
import espeon.game.jade.Target.TargetTypeFilter;
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

    public class Cell {
        public final int id = counter++;
        public List<StatusModel> status = new ArrayList<>();
        public LinkedList<Integer> creatures = new LinkedList<>();
        public int getBottom() {
            if(creatures.size() == 0) return Creature.noid;
            return creatures.peekLast();
        }
        public int getTop() {
            if(creatures.size() == 0) return Creature.noid;
            return creatures.peekFirst();
        }
        public List<Integer> getCreatures() {
            return creatures;
        }
        public int getX() {
            return cells.getCol(id);
        }
        public int getY() {
            return cells.getRow(id);
        }
    }

    public Cell findCreatureCell(int sourceid) {
        return cells.findOne(c -> c.getBottom() == sourceid);
    }

}
