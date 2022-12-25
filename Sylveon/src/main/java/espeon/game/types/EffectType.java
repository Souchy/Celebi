package espeon.game.types;

public enum EffectType {
    
	resource,
    damage,
    heal,
    move,
    summon,
    status,
    flee // aka send the target out of the board to swap it out with another creature
	;

    public static EffectType valueOf(int effectid) {
        return move;
    }

}
