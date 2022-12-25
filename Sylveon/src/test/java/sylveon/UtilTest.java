package sylveon;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.util.ArrayList;
import java.util.List;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.util.Util;

class UtilTest {
    
    public static List<String> values;
    public static int iterations = 100 * 1000;
    public static int sum = 0;

    @BeforeAll
    public static void setup() {
        values = new ArrayList<>();
        for(int i = 0; i < 100; i++) {
            sum += i;
            values.add(String.valueOf(i));
        }
        
    }

    @Test
    public void testIntegerParseInt() {
        long start = System.currentTimeMillis();
        int longSum = 0;
        for(int i = 0; i < iterations; i++) {
            for(String str : values) {
                longSum += Integer.parseInt(str);
            }
        }
        assertEquals(UtilTest.sum * iterations, longSum);
        long diff = System.currentTimeMillis() - start;
        System.out.println("testIntegerParseInt completed in " + diff + "ms");
    }
    
    @Test
    public void testUtilParseInt() {
        long start = System.currentTimeMillis();
        int longSum = 0;
        for(int i = 0; i < iterations; i++) {
            for(String str : values) {
                longSum += Util.parseInt(str);
            }
        }
        assertEquals(UtilTest.sum * iterations, longSum);
        long diff = System.currentTimeMillis() - start;
        System.out.println("testUtilParseInt completed in " + diff + "ms");
    }

}
