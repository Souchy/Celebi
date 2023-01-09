package espeon.game.jade.filters;

import espeon.game.red.Cell;
import espeon.game.red.Entity;

public class CellFilter implements Filter {
	public boolean allowEmpty = true;
	public boolean allowOccupied = true;
	
	@Override
	public boolean filter(Entity src, Entity target) {
		Cell c = (Cell) target;
		if(c.creatures.isEmpty() && allowEmpty)
			return true;
		if(!c.creatures.isEmpty() && allowOccupied)
			return true;
		return false;
	}
}
