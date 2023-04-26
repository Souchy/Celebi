package espeon.game.types;

/**
 * [][][]
 * [][][]
 * [][][]
 * 
 * Can also be a position(u,v) with 00, 01, 0-1, -1-1, +1+1, etc
 */
public enum Anchor {
    
    center,     //  0, 0
    top,        //  0,+1
    top_right,   // +1,+1
    right,      // +1, 0
    bottom_right,// +1,-1
    bottom,     //  0,-1
    bottom_left, // -1,-1
    left,       // -1, 0
    top_left;    // -1,+1

}
