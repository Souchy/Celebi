package espeon.util;

public class IDGenerator {
    
    private int counter = 1;

    public int get() {
        return counter++;
    }

}
