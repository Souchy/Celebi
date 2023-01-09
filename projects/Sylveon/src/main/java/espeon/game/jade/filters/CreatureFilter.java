package espeon.game.jade.filters;

import espeon.game.red.Creature;
import espeon.game.red.Entity;

public class CreatureFilter implements Filter {
	public int[] creatureModelIds;
	public int[] creatureSummonerModelIds;
	/** Primary creature as in, a player creature that is in the team initially, not a summon */
	public boolean allowPrimary = true;
	public boolean allowCorpse = true;
	public boolean allowSummon = true;
	public boolean allowSummonStatic = true;
	
	@Override
	public boolean filter(Entity src, Entity target) {
		Creature t = (Creature) target;
		boolean response = false;
		if(creatureModelIds.length == 0) {
			response = true;
		} else {
			for(int id : creatureModelIds) {
				if(id == t.modelid) {
					response = true;
					break;
				}
			}
		}
		if(creatureSummonerModelIds.length == 0) {
			response = true;
		} else {
			// for(int id : creatureSummonerModelIds) {
			// 	if(id == t.summoner.modelid) {
			// 		response = true;
			// 		break;
			// 	}
			// }
		}
		// if(!allowSummon && t.isPrimary) return false;
		// if(!allowCorpse && t.isDead) return false;
		// if(!allowSummon && t.isSummon) return false;
		// if(!allowSummon && t.isSummonStatic) return false;
		return false;
	}
}
