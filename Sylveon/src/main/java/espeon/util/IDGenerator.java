package espeon.util;

import java.util.concurrent.atomic.AtomicInteger;

public class IDGenerator {
    
    private final AtomicInteger counter;

    public IDGenerator() { 
        this(0); 
    }
    public IDGenerator(int start) {
        counter = new AtomicInteger(start);
    }

    public int get() {
        return counter.getAndIncrement();
    }

}
