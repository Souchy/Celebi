package espeon;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.ArrayList;
import java.util.List;

import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import espeon.game.controllers.Board;
import espeon.game.controllers.Diamonds;
import espeon.game.controllers.Fight;
import espeon.game.jade.Condition;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.SpellModel;
import espeon.game.jade.Statement;
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
import espeon.game.jade.effects.moves.MoveBy;
import espeon.game.jade.effects.moves.Translate;
import espeon.game.jade.effects.moves.MoveEffect.MoveType;
import espeon.game.net.handlers.GameActionHandler;
import espeon.game.red.Action;
import espeon.game.red.Aoe;
import espeon.game.red.Creature;
import espeon.game.red.Entity;
import espeon.game.red.Spell;
import espeon.game.red.Stats;

class FightMock {
    
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
        // f.timeline = new ArrayList<>();
        // f.timeline.add(1);
        // f.timeline.add(2);
        // f.timeline.add(3);
        // Diamonds.setFightClient(1, f);
        setupAction();
        setupSpellModel();
        setupSpell();
        setupCreatures();
        
        // spawn with no summoner adds the creature to the front of the list so we do it in reverse
        f.spawn(Entity.noid, t2.id);
        f.spawn(Entity.noid, t1.id);
        f.spawn(Entity.noid, caster.id);

        f.board.get(5, 1).setGround(1); // .creatures.push(1);
        f.board.get(4, 5).setGround(2); // .creatures.push(2);
        f.board.get(6, 5).setGround(3); // .creatures.push(3);
    }
    
    public static void setupCreatures() {
        {
            caster = new Creature();
            caster.modelid = 1;
            caster.spells = new ArrayList<>();
            caster.spells.add(spell.id);
            caster.stats = new Stats();
            caster.stats.add(Mod.hp_max, 100);
            caster.stats.add(Mod.hp, 100);
            caster.stats.add(Mod.ap_max, 11);
            caster.stats.add(Mod.ap, 11);
            caster.stats.add(Mod.sp_attack, 5);
            caster.stats.add(Mod.attack, 5);
            caster.stats.add(Mod.sp_defense, 5);
            caster.stats.add(Mod.defense, 5);
            // f.creatures.add(caster);
            // f.timeline.add(caster.id);
            Diamonds.setFightClient(caster.id, f);
            Diamonds.setCreature(caster.id, caster);
        }
        {
            t1 = new Creature();
            t1.modelid = 1;
            t1.spells = new ArrayList<>();
            t1.stats = new Stats();
            t1.stats.add(Mod.hp_max, 100);
            t1.stats.add(Mod.hp, 100);
            t1.stats.add(Mod.ap_max, 11);
            t1.stats.add(Mod.ap, 11);
            t1.stats.add(Mod.sp_attack, 5);
            t1.stats.add(Mod.attack, 5);
            t1.stats.add(Mod.sp_defense, 5);
            t1.stats.add(Mod.defense, 5);
            // f.creatures.add(target);
            // f.timeline.add(t1.id);
            Diamonds.setFightClient(t1.id, f);
            Diamonds.setCreature(t1.id, t1);
        }
        {
            t2 = t1.copy();
            // f.timeline.add(t2.id);
            Diamonds.setFightClient(t2.id, f);
            Diamonds.setCreature(t2.id, t2);
        }        
    }
    
    public static void setupSpell() {
        spell = new Spell();
        spell.modelid = sm.id;
        // spell.memory = spell.new Memory();
        Diamonds.setSpell(spell.id, spell);
    }

    public static void setupAction() {
        Action a = new Action();
        a.id = 1;
        var filter = new TargetTypeFilter(); // affects all by default
        // Statement push target if high defense, pull target if low defense
        {
            StatementGroup group = new StatementGroup();
            a.statements.add(group);
            {
                StatCondition con = new StatCondition();
                group.condition = con;
                con.actor = Actor.target;
                con.mod = Mod.defense;
                con.val = 3;
                con.op = ComparisonOperator.ge;
                con.children = new ArrayList<>();
                con.childLink = null;
            }
            {
                StatementEffect se = new StatementEffect();
                group.children.add(se);
                {
                    MoveBy em = Translate.by(2);
                    em.aoe = Aoe.newLinePerpendicular(3, filter);
                    se.effect = em;
                }
            }
            {
                StatementEffect se = new StatementEffect();
                group.childrenOtherwise.add(se);
                {
                    MoveBy em = Translate.by(-2);
                    em.aoe = Aoe.newLinePerpendicular(3, filter);
                    se.effect = em;
                }
            }
        }
        // Statement Damage
        {
            StatementEffect se = new StatementEffect();
            a.statements.add(se);
            {
                DamageEffect em = new DamageEffect();
                em.power = 10;
                em.aoe = Aoe.newLinePerpendicular(3, filter);
                se.effect = em;
            }
        }
    }

    public static void setupSpellModel() {
        sm = new SpellModel();
        sm.id = 1;
        sm.actionid = 1;
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
        Diamonds.setSpellModel(sm.id, sm);
    }

}
