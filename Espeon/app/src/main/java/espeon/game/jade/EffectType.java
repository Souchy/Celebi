package espeon.game.jade;

public enum EffectType {
    
    move,
    damage,
    heal,
    summon,
    status,
    flee;

    public static EffectType valueOf(int effectid) {
        return move;
    }

}
