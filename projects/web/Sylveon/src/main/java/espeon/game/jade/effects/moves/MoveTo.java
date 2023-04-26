package espeon.game.jade.effects.moves;

import espeon.game.jade.aoes.Zone;
import espeon.game.red.Aoe;

public class MoveTo extends MoveEffect {

    /**
     * Only if translating :
     * if true: move to the farthest available cell in the path
     * if false : cancel the move if the target cell is occupied
     */
    public boolean cellByCell = false;

    // Destination aoe (can move whole areas of people)
    // private Aoe to = new Aoe();
    private Zone to = new Zone();

    public MoveTo(MoveType type) {
        super(type);
    }

    /**
     * Take creatures from those cells to teleport them elsewhere
     */
    public Zone from() {
        return zone;
    }

    /**
     * Teleport creatures to those destination cells
     */
    public Zone to() {
        return to;
    }
    
}
