package espeon.game.types;

public enum GameActionType {

    move,
    spell,
    pass,
    forfeit;

    // quit(-4),
    // forfeit(-3),
    // pass(-2),
    // move(-1),
    // spell(1);

    // private int val;
    // private GameActionType(int id) {
    //     this.val = id;
    // }

    // public static GameActionType valueOf(int actionid) {
    //     if(actionid >= spell.val) {
    //         return spell;
    //     } else {
    //         for(var e : GameActionType.values()) {
    //             if(e.val == actionid)
    //                 return e;
    //         }
    //         throw new IllegalArgumentException("GameAction id outside of enum's range: [" + actionid + "]");
    //     }
    // }

}
