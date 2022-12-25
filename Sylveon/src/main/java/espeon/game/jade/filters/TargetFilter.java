package espeon.game.jade.filters;

import espeon.game.red.Entity;

public class TargetFilter implements Filter {
	public TeamFilter team = null;
	public CreatureFilter creature = null;
	public CellFilter cell = null;

	public boolean allowAll = false;
	public boolean allowNone = false;

	public boolean filter(Entity src, Entity target) {
		if(allowAll) return true;
		if(allowNone) return false;

		boolean response = true;
		if(team != null) response &= team.filter(src, target);
		if(creature != null) response &= creature.filter(src, target);
		if(cell != null) response &= cell.filter(src, target);

		return response;
	}
}
