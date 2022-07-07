package espeon.game.controllers;

import java.util.HashMap;
import java.util.Map;
import java.util.function.BiFunction;
import java.util.function.Function;

import espeon.game.controllers.actionpipeline.NewPipeline;
import espeon.game.controllers.actionpipeline.Node.NodeEffect;
import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.jade.effects.moves.MoveBy;
import espeon.game.jade.effects.moves.MoveEffect;
import espeon.game.jade.effects.moves.MoveSymmetrically;
import espeon.game.jade.effects.moves.MoveTo;
import espeon.game.jade.effects.moves.MoveToPrevious;
import espeon.game.red.Cell;
import espeon.game.red.Creature;
import espeon.game.red.Stats;
import espeon.game.red.Status;
import espeon.game.red.compiledeffects.CompiledDamage;
import espeon.game.red.compiledeffects.CompiledEffect;
import espeon.game.red.compiledeffects.CompiledMove;

public class Effects {


    public static CompiledEffect compile(NewPipeline p, NodeEffect action) {
        CompiledEffect e = switch(action.effect.model.type()) {
            case damage -> compileDamage(action, (DamageEffect) action.effect.model);
            case flee -> null;
            case heal -> null;
            case move -> compileMove(action, (MoveEffect) action.effect.model);
            case status -> null;
            case summon -> null;
        };
        return e;
        // throw new IllegalArgumentException("Illegal effect type: " + action.effect.type());
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
        int creatureid = action.effect.entityid;
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
        // Fight fight = Diamonds.getFightByClient(action.sourceid);
        // Board board = fight.board;
        // Cell cell = board.get(action.cellid);



        return null;
    }
    public static CompiledEffect compileTranslateTo(NodeEffect action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.effect.entityid); // action.cellid);

        var aoe = e.aoe;
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
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.effect.entityid);

        var from = e.aoe;
        return null;
    }
    public static CompiledEffect compileTeleportTo(NodeEffect action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.effect.entityid);
        var from = e.from();
        var to = e.to();
        return null;
    }
    public static CompiledEffect compileTeleportSymmetrically(NodeEffect action, MoveSymmetrically e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.effect.entityid);
        var aoe = e.aoe;
        var center = e.aoe.origin;
        return null;
    }
    public static CompiledEffect compileTeleportToPrevious(NodeEffect action, MoveToPrevious e) {
        CompiledMove m = new CompiledMove();
        // Fight fight = Diamonds.getFightByClient(action.sourceid);
        // Board board = fight.board;
        // Cell cell = board.get(action.effect.entityid);
        Diamonds.getCreatureInstance(action.effect.entityid);
        var aoe = e.aoe;
        return null;
    }

}
