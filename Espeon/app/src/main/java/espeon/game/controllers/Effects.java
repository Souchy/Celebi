package espeon.game.controllers;

import espeon.game.controllers.ActionPipeline.EffectAction;
import espeon.game.jade.Mod;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.jade.effects.moves.MoveBy;
import espeon.game.jade.effects.moves.MoveEffect;
import espeon.game.jade.effects.moves.MoveSymmetrically;
import espeon.game.jade.effects.moves.MoveTo;
import espeon.game.jade.effects.moves.MoveToPrevious;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Board.Cell;
import espeon.game.red.compiledeffects.CompiledDamage;
import espeon.game.red.compiledeffects.CompiledEffect;
import espeon.game.red.compiledeffects.CompiledMove;

public class Effects {

    public static CompiledEffect compile(ActionPipeline p, EffectAction action) {
        CompiledEffect e = switch(action.effect.type()) {
            case damage -> compileDamage(action, (DamageEffect) action.effect);
            case flee -> null;
            case heal -> null;
            case move -> compileMove(action, (MoveEffect) action.effect);
            case status -> null;
            case summon -> null;
        };
        return e;
        // throw new IllegalArgumentException("Illegal effect type: " + action.effect.type());
    }

    public static CompiledEffect compileDamage(EffectAction action, DamageEffect e) {
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);

        int creatureid = cell.getBottom();
        if(creatureid == Creature.noid) {
            System.err.printf("Effects.compileDamage cell [%s] {%s, %s} has no creature.\n", action.cellid, cell.getX(), cell.getY());
            return null;
        } 

        int hp = action.getStat(creatureid, Mod.hp);
        int defense = action.getStat(creatureid, Mod.defense);

        int dmg = e.power - defense;
        action.setStat(creatureid, Mod.hp, hp - dmg);

        var compiled = new CompiledDamage();
        compiled.creatureid = creatureid;
        compiled.damage = dmg;
        compiled.sourceid = action.sourceid;
        
        System.err.printf("Effects.compileDamage cell [%s] {%s, %s}, creature [%s] hp: %s - %s = %s.\n", action.cellid, cell.getX(), cell.getY(), creatureid, hp, dmg, (hp-dmg));
        return compiled;
    }

    public static CompiledEffect compileMove(EffectAction action, MoveEffect e) {
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

    public static CompiledEffect compileTranslateBy(EffectAction action, MoveBy e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);
        var aoe = e.aoe;
        return null;
    }
    public static CompiledEffect compileTranslateTo(EffectAction action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);
        var aoe = e.aoe;
        if(cell.creatures.size() > 0) {
            if(e.translateToFarthestAvailable) {
                
            } else {
    
            }
        } else {

        }
        return null;
    }
    
    public static CompiledEffect compileTeleportBy(EffectAction action, MoveBy e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);

        var from = e.aoe;
        return null;
    }
    public static CompiledEffect compileTeleportTo(EffectAction action, MoveTo e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);
        var from = e.from();
        var to = e.to();
        return null;
    }
    public static CompiledEffect compileTeleportSymmetrically(EffectAction action, MoveSymmetrically e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);
        var aoe = e.aoe;
        var center = e.aoe.origin;
        return null;
    }
    public static CompiledEffect compileTeleportToPrevious(EffectAction action, MoveToPrevious e) {
        CompiledMove m = new CompiledMove();
        Fight fight = Diamonds.getFightByClient(action.sourceid);
        Board board = fight.board;
        Cell cell = board.get(action.cellid);
        var aoe = e.aoe;
        return null;
    }

}
