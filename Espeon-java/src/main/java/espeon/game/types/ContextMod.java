package espeon.game.types;

/**
 * Used,
 * Lost/Gained
 * Reduced/Increased
 */
public enum ContextMod {
	
	numberOfTargetsAffected,
	
	damage_done,
	damage_received,

	healing_done,
	healing_received,

	
	used_ap,
	lost_ap,
	gained_ap,
	reduced_ap,
	increased_ap,

	lost_ap_max,
	gained_ap_max,
	reduced_ap_max,
	increased_ap_max,

	used_mp,
	lost_mp,
	gained_mp,
	reduced_mp,
	increased_mp,

	lost_mp_max,
	gained_mp_max,
	reduced_mp_max,
	increased_mp_max,

	/** ex: using life to cast spells */
	used_life,
	/** this is not damage or healing, it is ex: lifo -1000 life */
	lost_life,
	gained_life,
	reduced_life,
	increased_life,
	
	lost_life_max,
	gained_life_max,
	reduced_life_max,
	increased_life_max,


	// other:
	// attack, defense, sp_attack, sp_defense, 

}
