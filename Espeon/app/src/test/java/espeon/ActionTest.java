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
import espeon.game.jade.effects.PushEffect;
import espeon.game.red.Aoe;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Spell;
import espeon.game.red.Stats;
import espeon.game.red.Board.Cell;

public class ActionTest {

    public static Fight f;
    public static SpellModel sm;
    public static Spell spell;
    public static Creature caster;
    public static Creature t1;
    public static Creature t2;

    @BeforeAll
    public static void setup() {
        f = new Fight();
        f.board = new Board();
        // f.creatures = new ArrayList<>();
        f.timeline = new ArrayList<>();
        // f.timeline.add(1);
        // f.timeline.add(2);
        // f.timeline.add(3);
        // Diamonds.setFightClient(1, f);
        setupSpellModel();
        setupSpell();
        setupCreatures();

        f.board.get(5, 1).creatures.push(1);
        f.board.get(4, 5).creatures.push(2);
        f.board.get(6, 5).creatures.push(3);
    }
    
    public static void setupSpellModel() {
        sm = new SpellModel();
        sm.id = 1;
        {
            SpellConditions sc = sm.new SpellConditions();
            sc.castPerTarget = 1;
            sc.castPerTurn = 2;
            sc.cooldown = 0;
            sm.conditions = sc;
        }
        {
            sm.costs = new ArrayList<>();
            Cost c = sm.new Cost();
            c.resource = Mod.ap;
            c.amount = 3;
            sm.costs.add(c);
        }
        {
            List<Statement> statements = new ArrayList<>();
            {
                // Statement Damage
                {
                    StatementEffect se = new StatementEffect();
                    {
                        var filter = new TargetTypeFilter(); // affects all by default
                        DamageEffect em = new DamageEffect();
                        em.power = 10;
                        em.aoe = Aoe.newLinePerpendicular(3, filter);
                        se.effect = em;
                    }
                    statements.add(se);
                }
                // Statement reduce target's defense if source's sp_attack is >=5
                // {
                //     StatementGroup group = new StatementGroup();
                //     {
                //         StatCondition con = new StatCondition();
                //         con.actor = Actor.source;
                //         con.mod = Mod.sp_attack;
                //         con.val = 5;
                //         con.op = ComparisonOperator.ge;
                //         con.children = new ArrayList<>();
                //         con.childLink = null;
                //         group.condition = con;
                //     }
                //     {
                //         StatementEffect se = new StatementEffect();
                //         {
                //             StatusEffect em = new StatusEffect();
                //             StatusModel sm = new StatusModel();
                //             em.power = 1;
                //             se.effect = em;
                //         }
                //         group.children.add(se);
                //     }
                //     statements.add(group);
                // }
                // Statement push target if high defense, pull target if low defense
                {
                    StatementGroup group = new StatementGroup();
                    group.children = new ArrayList<>();
                    group.childrenOtherwise = new ArrayList<>();
                    {
                        StatCondition con = new StatCondition();
                        con.actor = Actor.target;
                        con.mod = Mod.defense;
                        con.val = 3;
                        con.op = ComparisonOperator.ge;
                        con.children = new ArrayList<>();
                        con.childLink = null;
                        group.condition = con;
                    }
                    {
                        StatementEffect se = new StatementEffect();
                        {
                            PushEffect em = new PushEffect();
                            em.power = 2;
                            se.effect = em;
                        }
                        group.children.add(se);
                    }
                    {
                        StatementEffect se = new StatementEffect();
                        {
                            PushEffect em = new PushEffect();
                            em.power = -2;
                            se.effect = em;
                        }
                        group.childrenOtherwise.add(se);
                    }
                    statements.add(group);
                }
            }
            sm.statements = statements;
        }
        
        Diamonds.setSpellModel(sm.id, sm);
    }

    public static void setupSpell() {
        spell = new Spell();
        spell.modelid = sm.id;
        spell.memory = spell.new Memory();
        Diamonds.setSpell(spell.id, spell);
    }

    public static void setupCreatures() {
        {
            caster = new Creature();
            caster.modelid = 1;
            caster.spells = new ArrayList<>();
            caster.spells.add(spell.id);
            caster.stats = new Stats();
            caster.stats.add(Mod.ap_max, 11);
            caster.stats.add(Mod.ap, 11);
            caster.stats.add(Mod.sp_attack, 5);
            caster.stats.add(Mod.attack, 5);
            caster.stats.add(Mod.sp_defense, 5);
            caster.stats.add(Mod.defense, 5);
            // f.creatures.add(caster);
            f.timeline.add(caster.id);
            Diamonds.setFightClient(caster.id, f);
            Diamonds.setCreature(caster.id, caster);
        }
        {
            t1 = new Creature();
            t1.modelid = 1;
            t1.spells = new ArrayList<>();
            t1.stats = new Stats();
            t1.stats.add(Mod.ap_max, 11);
            t1.stats.add(Mod.ap, 11);
            t1.stats.add(Mod.sp_attack, 5);
            t1.stats.add(Mod.attack, 5);
            t1.stats.add(Mod.sp_defense, 5);
            t1.stats.add(Mod.defense, 5);
            // f.creatures.add(target);
            f.timeline.add(t1.id);
            Diamonds.setFightClient(t1.id, f);
            Diamonds.setCreature(t1.id, t1);
        }
        {
            t2 = t1.copy();
            f.timeline.add(t2.id);
            Diamonds.setFightClient(t2.id, f);
            Diamonds.setCreature(t2.id, t2);
        }        
    }

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
                System.out.printf("Cell at [%s, %s] = [%s] {%s, %s} \n", x, y, c.id, c.getX(), c.getY());
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
