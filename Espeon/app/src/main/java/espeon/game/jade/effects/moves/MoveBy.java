package espeon.game.jade.effects.moves;

/**
 * Moves by x amount in the direction of the vector from the source to the target
 */
public class MoveBy extends MoveEffect {

    public int amount;

    public MoveBy(MoveType type) {
        super(type);
    }

}
