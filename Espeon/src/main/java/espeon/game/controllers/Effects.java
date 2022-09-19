package espeon.game.controllers;

import java.util.HashMap;
import java.util.Map;
import java.util.function.BiFunction;
import java.util.function.Function;

import espeon.game.controllers.actionpipeline.NewPipeline;
import espeon.game.controllers.actionpipeline.Node.NodeEffect;
import espeon.game.jade.EffectModel;
import espeon.game.jade.effects.AddStatusEffect;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.jade.effects.moves.MoveBy;
import espeon.game.jade.effects.moves.MoveEffect;
import espeon.game.jade.effects.moves.MoveSymmetrically;
import espeon.game.jade.effects.moves.MoveTo;
import espeon.game.jade.effects.moves.MoveToPrevious;
import espeon.game.red.Cell;
import espeon.game.red.Creature;
import espeon.game.red.Entity;
import espeon.game.red.Stats;
import espeon.game.red.Status;
import espeon.game.red.Entity.EntityType;
import espeon.game.red.compiledeffects.CompiledDamage;
import espeon.game.red.compiledeffects.CompiledEffect;
import espeon.game.red.compiledeffects.CompiledMove;
import espeon.game.types.Mod;

public class Effects {


    public static CompiledEffect compile(NewPipeline p, NodeEffect action) {
        CompiledEffect e = switch(action.effectModel.type()) {
			case damage -> compileDamage(action, (DamageEffect) action.effectModel);
            case move -> compileMove(action, (MoveEffect) action.effectModel);
			case resource -> throw new UnsupportedOperationException("Unimplemented case: " + action.effectModel.type());
            case heal -> throw new UnsupportedOperationException("Unimplemented case: " + action.effectModel.type());
            case status -> throw new UnsupportedOperationException("Unimplemented case: " + action.effectModel.type());
            case summon -> throw new UnsupportedOperationException("Unimplemented case: " + action.effectModel.type());
			case flee -> throw new UnsupportedOperationException("Unimplemented case: " + action.effectModel.type());
        };
        return e;
        // throw new IllegalArgumentException("Illegal effect type: " + action.effect.type());
    }

    public static void addStatus(NodeEffect action, AddStatusEffect ef) {
        Entity en = Diamonds.getEntity(action.getFightId(), action.targetEntityId);
        for(var status : en.status) {
            if(status.spellModelSource == ef.spellModelSource) {
                // merge
                return;
            }
        }
        Status status = new Status();
        en.status.add(status);
    }

    public static void triggerStatus(Creature c) {
        int i = c.status.size() - 1;
        while(i >= 0) {
            Status s = c.status.get(i);
            i--;
        }
    }

    public static CompiledEffect compileDamage(NodeEffect action, DamageEffect e) {
        // Fight fight = Diamonds.getFightByClient(action.sourceid);
        // Board board = fight.board;
        // Cell cell = board.get(action.cellid);
        // var pos = board.getPos(cell.id);

        // int creatureid = cell.getBottomMost();
        // if(creatureid == Creature.noid) {
            // System.err.printf("Effects.compileDamage cell [%s] {%s, %s} has no creature.\n", action.cellid, pos.x, pos.y);
            // return null;
        // } 
        int creatureid = action.targetEntityId;
        var compiled = new CompiledDamage();
        // var creatureTarget = Diamonds.getCreatureInstance(creatureid);
        // var creatureSource = Diamonds.getCreatureInstance(action.sourceid);
        {
            Stats stats = action.context.stats.get(creatureid);
            int hp = stats.get(Mod.hp); //action.getStat(creatureid, Mod.hp);
            int defense = stats.get(Mod.defense); //action.getStat(creatureid, Mod.defense);

            int dmg = e.power - defense;
            stats.set(Mod.hp, hp - dmg); // action.setStat(creatureid, Mod.hp, hp - dmg);

            compiled.creatureid = creatureid;
            compiled.damage = dmg;
            compiled.sourceid = action.sourceid;

            // System.err.printf("Effects.compileDamage cell [%s] {%s, %s}, creature [%s] hp: %s - %s = %s.\n", action.cellid, pos.x, pos.y, creatureid, hp, dmg, (hp-dmg));
            System.err.printf("Effects.compileDamage creature [%s] hp: %s - %s = %s.\n", /* action.cellid, pos.x, pos.y, */ creatureid, hp, dmg, (hp-dmg));

        }
        return compiled;
    }

    public static CompiledEffect compileMove(NodeEffect action, MoveEffect e) {
        CompiledMove m = new CompiledMove();
        switch(e.moveType) {
            case teleportation:
                break;
            case translation:
                break;
            default:
                break;

        }
        return null;
    }

    public static CompiledEffect compileTranslateBy(NodeEffect action, MoveBy e) {
        CompiledMove m = new CompiledMove();
        // Entity entity = Diamonds.getEntity(action.effect.entityid);
        // Fight fight = Diamonds.getFight(entity.fightid);
        // if(entity.type() != EntityType.creature) throw new RuntimeException("Entity does not correspond to a creature.");
        // Creature creature = (Creature) entity;
        // int action.context.positions.get(action.effect.entityid);
        Fight fight = Diamonds.getFight(action.getFightId());

        return null;
    }
    public static CompiledEffect compileTranslateTo(NodeEffect action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Entity entity = Diamonds.getEntity(action.getFightId(), action.targetEntityId);
        if(entity.type() != EntityType.cell) {
            throw new RuntimeException("Entity does not correspond to a cell.");
        }
        Fight fight = Diamonds.getFight(entity.fightid);
        // Board board = fight.board;
        Cell cell = (Cell) entity; // Cell cell = board.get(action.effect.entityid); // action.cellid);

        // var aoe = e.aoe;
        if(cell.creatures.size() > 0) {
            if(e.cellByCell) {
                
            } else {
    
            }
        } else {

        }
        return null;
    }
    
    public static CompiledEffect compileTeleportBy(NodeEffect action, MoveBy e) {
        CompiledMove m = new CompiledMove();
        Entity entity = Diamonds.getEntity(action.getFightId(), action.targetEntityId);
        Fight fight = Diamonds.getFight(entity.fightid);
        Board board = fight.board;
        // Cell cell = board.get(action.effect.entityid);

        // var from = e.aoe;
        return null;
    }
    public static CompiledEffect compileTeleportTo(NodeEffect action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Entity entity = Diamonds.getEntity(action.getFightId(), action.targetEntityId);
        Fight fight = Diamonds.getFight(entity.fightid);
        Board board = fight.board;
        // Cell cell = board.get(action.effect.entityid);
        var from = e.from();
        var to = e.to();
        return null;
    }
    public static CompiledEffect compileTeleportSymmetrically(NodeEffect action, MoveSymmetrically e) {
        CompiledMove m = new CompiledMove();
        Entity entity = Diamonds.getEntity(action.getFightId(), action.targetEntityId);
        Fight fight = Diamonds.getFight(entity.fightid);
        Board board = fight.board;
        // Cell cell = board.get(action.effect.entityid);
        // var aoe = e.aoe;
        // var center = e.aoe.origin;
        return null;
    }
    public static CompiledEffect compileTeleportToPrevious(NodeEffect action, MoveToPrevious e) {
        CompiledMove m = new CompiledMove();
        // Fight fight = Diamonds.getFightByClient(action.sourceid);
        // Board board = fight.board;
        // Cell cell = board.get(action.effect.entityid);
        Diamonds.getCreatureInstance(action.getFightId(), action.targetEntityId);
        // var aoe = e.aoe;
        return null;
    }

}
