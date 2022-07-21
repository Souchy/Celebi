package espeon.game.red;

public class Position {
    public int x, y, z;

    public Position() { }
    public Position(int x, int y) {
        this(x, y, 0);
    }
    public Position(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Position copy() {
        var pos = new Position();
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }
}
