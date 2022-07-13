package espeon.game.jade.aoes;

import espeon.game.jade.Position;
import espeon.game.jade.Condition.Actor;
import espeon.game.jade.Target.TargetType;
import espeon.game.types.Anchor;
import espeon.game.types.Direction8;

public class Zone {
    
    // type
    public ZoneType type = ZoneType.point;
    // filter
    public int targetTypeFilter = TargetType.enemy.value;

    // World origin : source or target
    public Actor worldOrigin = Actor.target;
    // Offset from the world origin to the table's local origin 
    public Position worldOffset = new Position();
    // Anchor for the table to define its local origin. Usually in the middle
    public Anchor localOrigin = Anchor.center; // public Direction tableAnchor;


    // Can rotate the aoe. 
    // TOP means in the direction of the vector from source -> target
    // for lines : parallel/perpendicular
    public Direction8 rotation = Direction8.top;   // public int orientation;

    // length for lines or radius for circles/squares/cross/star/cone
    public int length;
}


/*
x _ x _ x
_ x x x _
x x x x x
_ x x x _
x _ x _ x
*/


