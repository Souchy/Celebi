package espeon.game.jade.effects.moves;

import espeon.game.jade.effects.moves.MoveEffect.MoveType;

public class Translate {
    
    public static MoveBy by(int amount) {
        var move = new MoveBy(MoveType.translation);
        move.amount = amount;
        return move;
    }
    
    public static MoveTo to() {
        var move = new MoveTo(MoveType.translation);
        return move;
    }

}
