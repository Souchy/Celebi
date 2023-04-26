package espeon.emerald;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

public class RedTest {

    // red instance for the fight
    private static Red red;
    // id for the creature
    private static int id;
    
    @BeforeAll
    static void setup() {
        red = new Red(1);
    }

    @Test
    void testGetCreature() {
        String modelid = red.getCreature().getModelid(id);
    }

}
