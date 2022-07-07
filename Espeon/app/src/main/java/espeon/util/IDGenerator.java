package espeon.util;

public class IDGenerator {
    
    private int counter = 0;

    public IDGenerator() {}
    public IDGenerator(int start) {
        counter = start;
    }

    public int get() {
        return counter++;
    }

}
