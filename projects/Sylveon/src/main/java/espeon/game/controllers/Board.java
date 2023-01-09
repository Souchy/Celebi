package espeon.game.controllers;

import java.util.ArrayList;
import java.util.List;

import espeon.game.red.Aoe;
import espeon.game.red.Cell;
import espeon.game.red.Position;
import espeon.util.Table;

public class Board {
    
    // public final int fightid;
    private Table<Cell> cells = new Table<>(24, 24, null);

    public Board(Fight f) {
        // this.fightid = f.id;
        for(int i = 0; i < cells.size(); i++) {
            cells.set(i, new Cell(f.id, f.newEntityId()));
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

    public Cell findCreatureCell(int sourceid) {
        return cells.findOne(c -> c.creatures.containsValue(sourceid));
    }

    public Position getPos(int cellid) {
        Position pos = new Position();
        pos.x = cells.getCol(cellid);
        pos.y = cells.getRow(cellid);
        pos.z = 0; // cell.height;
        return pos;
    }

    public int getWidth() {
        return cells.getWidth();
    }
    public int getHeight() {
        return cells.getHeight();
    }

}
