package espeon.game.red;

import java.util.ArrayList;
import java.util.List;

import espeon.game.jade.StatusModel;
import espeon.util.Table;

public class Board {
    
    public Table<Cell> cells = new Table<>(24, 24, new Cell());
    
    public Cell get(int cellid) {
        return cells.get(cellid);
    }
    public Cell get(int x, int y) {
        return cells.get(y, x);
    }

    public List<Cell> filterCells(int cellid, Aoe aoe) {
        List<Cell> list = new ArrayList<>();

        return list;
    }


    public static class Cell {
        public int id;
        public List<StatusModel> status = new ArrayList<>();
        public int getCreature() {
            return 0;
        }
        public List<Integer> getCreatures() {
            return null;
        }
    }

}
