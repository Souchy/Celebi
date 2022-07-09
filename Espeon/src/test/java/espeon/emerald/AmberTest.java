package espeon.emerald;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.FightMock;

public class AmberTest {
    
    private static FightMock mock;

    @BeforeAll
    private static void setup() {
        mock = new FightMock();
    }

    @Test void deleteSpellModel() {
        // delete data
        Amber.spellModel.delete(mock.sm.id);
        var val = Amber.spellModel.getActionId(mock.sm.id);
        assertNull(val);
    }
    
    @Test void addSpellModel() {
        Amber.spellModel.setActionId(mock.sm.id, String.valueOf(mock.sm.actionid));
        String val = Amber.spellModel.getActionId(mock.sm.id);
        assertEquals(val, String.valueOf(mock.sm.actionid));
    }
    

    @Test void testGetAction() {

    }

}
