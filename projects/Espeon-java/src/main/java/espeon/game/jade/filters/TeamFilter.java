package espeon.game.jade.filters;

import espeon.game.red.Entity;

public class TeamFilter implements Filter {
	public boolean allowAlly = true;
	public boolean allowEnemy = true;
	
	public boolean filter(Entity src, Entity target) {
		// if(allowAlly && src.team == target.team) return true;
		// if(allowEnemy && src.team != target.team) return true;
		return false;
	}
}
