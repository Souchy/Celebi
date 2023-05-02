package sylveon.emerald;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.FightMock;
import espeon.emerald.amber.Amber;

public class AmberTest {
    
    private static FightMock mock;

    @BeforeAll
    private static void setup() {
        mock = new FightMock();
    }

    @Test void deleteSpellModel() {
        // delete data
        Amber.spells.delete(mock.sm.id);
        var val = Amber.spells.getActionId(mock.sm.id);
        assertNull(val);
    }
    
    @Test void addSpellModel() {
        Amber.spells.setActionId(mock.sm.id, mock.sm.actionid);
        String val = Amber.spells.getActionId(mock.sm.id);
        assertEquals(val, mock.sm.actionid);
    }
    

    @Test void testGetAction() {

    }

}
