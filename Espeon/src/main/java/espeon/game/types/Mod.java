package espeon.game.types;

public enum Mod {
    
    BOUND_START,
    ap,
    ap_max,
    mp,
    mp_max,
    hp,
    hp_max,

    attack,
    defense,
    sp_attack,
    sp_defense,
    speed,

    BOUND_END;


    public static final String[] names;
    static {
        int size = Mod.values().length;
        names = new String[size];
        for(int i = 0; i < size; i++) {
            names[i] = Mod.values()[i].name();
        }
    }
}
