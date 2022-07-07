package espeon.game.jade;

public class Position {
    public int x, y, z;

    public Position copy() {
        var pos = new Position();
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }
}
