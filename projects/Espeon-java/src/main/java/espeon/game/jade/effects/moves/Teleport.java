package espeon.game.jade.effects.moves;

import espeon.game.jade.effects.moves.MoveEffect.MoveType;

public class Teleport {

    public static MoveBy by(int amount) {
        var move = new MoveBy(MoveType.teleportation);
        move.amount = amount;
        return move;
    }

    public static MoveTo to() {
        var move = new MoveTo(MoveType.teleportation);
        return move;
    }

    public static MoveToPrevious toPrevious() {
        var move = new MoveToPrevious();
        return move;
    }

    public static MoveSymmetrically symmetrically() {
        var move = new MoveSymmetrically();
        return move;
    }

    public static MoveTo zoneTo() {
        var move = new MoveTo(MoveType.teleportation);

        return move;
    }

}


