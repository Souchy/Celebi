package espeon;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.game.controllers.Diamonds;
import espeon.game.red.Cell;

class ActionTest {

    private FightMock mock;

    @BeforeAll
    void setupFight() {
        mock = new FightMock();
    }

    @Test
    public void testDiamondsSetup() {
        int fightid = 1;
        // assertEquals(mock.f,  Diamonds.getFightByClient(caster.id));
        assertEquals(mock.caster, Diamonds.getCreatureInstance(fightid, mock.caster.id));
        assertEquals(mock.t1, Diamonds.getCreatureInstance(fightid, mock.t1.id));
        assertEquals(mock.t2, Diamonds.getCreatureInstance(fightid, mock.t2.id));
        assertEquals(mock.sm, Diamonds.getSpellModel(mock.sm.id));
        assertEquals(mock.spell, Diamonds.getSpell(fightid, mock.spell.id));
        assertEquals(mock.f.board.get(0, 0).id, 0, "Cell id should start at 0.");
    }

    @Test
    public void testCell() {
        for(int x = 0; x < mock.f.board.getWidth(); x++) {
            for(int y = 0; y < mock.f.board.getHeight(); y++) {
                Cell c = mock.f.board.get(x, y);
                var pos = mock.f.board.getPos(c.id);
                System.out.printf("Cell at [%s, %s] = [%s] {%s, %s} \n", x, y, c.id, pos.x, pos.y); //c.getX(), c.getY());
                assertEquals(pos.x, x); // c.getX(), x);
                assertEquals(pos.y, y); // c.getY(), y);
            }
        }
    }

    /*/
    @Test
    public void testSpellCast() {
        Cell cell = f.board.get(5, 5);
        ActionPipeline p = new ActionPipeline(caster.id, spell.id, cell.id);
        var handler = new GameActionHandler();
        handler.castSpell(null, p);  

        System.out.printf("Action Effects [%s]: {\n", p.stack.size());
        for(var ae : p.stack) {
            System.out.println("\t" + ae);
        }
        System.out.println("}");

        System.out.printf("Compiled Effects [%s]: {\n", p.compiled.size());
        for(var ce : p.compiled) {
            System.out.println("\t" + ce);
        }
        System.out.println("}");

        assertTrue(true);
    }
    */
    
}
