package sylveon.emerald;

import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.FightMock;
import espeon.emerald.Emerald;

public class EmeraldTest {

    static FightMock mock;
    
    @BeforeAll
    static void setup() {
        Emerald.init();
        mock = new FightMock();
    }

    @Test
    void setSpellModel() {
        // var result = Emerald.spells().insertOne(mock.sm);
        // assertTrue(result.wasAcknowledged());
        // assertNotNull(result.getInsertedId());
        // System.out.println("EmeraldTest set spell");
    }
    
    @Test
    void setAction() {
        // Emerald.actions().insertOne(mock.action);
    }

    @Test
    void testGetSpellModel() {

    }

    @Test
    void testGetAction() {

    }


}
