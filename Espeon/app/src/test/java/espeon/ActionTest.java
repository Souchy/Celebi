package espeon;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.ArrayList;
import java.util.List;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.game.controllers.ActionPipeline;
import espeon.game.controllers.Diamonds;
import espeon.game.controllers.Fight;
import espeon.game.handlers.GameActionHandler;
import espeon.game.jade.Condition;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.SpellModel;
import espeon.game.jade.Statement;
import espeon.game.jade.StatusModel;
import espeon.game.jade.Condition.Actor;
import espeon.game.jade.Condition.ComparisonOperator;
import espeon.game.jade.Condition.StatCondition;
import espeon.game.jade.SpellModel.Cost;
import espeon.game.jade.SpellModel.SpellConditions;
import espeon.game.jade.Statement.StatementEffect;
import espeon.game.jade.Statement.StatementGroup;
import espeon.game.jade.Target.TargetType;
import espeon.game.jade.Target.TargetTypeFilter;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.red.Aoe;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Spell;
import espeon.game.red.Stats;
import espeon.game.red.Board.Cell;

class ActionTest extends FightMock {

    @Test
    public void testDiamondsSetup() {
        assertEquals(ActionTest.caster, Diamonds.getCreatureInstance(caster.id));
        assertEquals(ActionTest.t1, Diamonds.getCreatureInstance(t1.id));
        assertEquals(ActionTest.t2, Diamonds.getCreatureInstance(t2.id));
        assertEquals(ActionTest.sm, Diamonds.getSpellModel(sm.id));
        assertEquals(ActionTest.f,  Diamonds.getFightByClient(caster.id));
        assertEquals(ActionTest.spell, Diamonds.getSpell(spell.id));
    }

    @Test
    public void testCell() {
        for(int x = 0; x < f.board.cells.getWidth(); x++) {
            for(int y = 0; y < f.board.cells.getHeight(); y++) {
                Cell c = f.board.get(x, y);
                // System.out.printf("Cell at [%s, %s] = [%s] {%s, %s} \n", x, y, c.id, c.getX(), c.getY());
                assertEquals(c.getX(), x);
                assertEquals(c.getY(), y);
            }
        }
        for(int i = 0; i < f.board.cells.size(); i++) {

        }
    }

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

}
