package espeon.game.jade.effects.moves;

/**
 * Moves by x amount in the direction of the vector from the source to the target
 * Need to stop at traps and obstacles, then trigger status from the End cell
 * 
 * En fait les Traps ont un Trigger OnMove
 * Alors que les Glyphs ont des Trigger OnMoveEnd, OnTurnEnd, OnTurnStart
 */
public class MoveBy extends MoveEffect {

    public int amount;

    public MoveBy(MoveType type) {
        super(type);
    }

}
