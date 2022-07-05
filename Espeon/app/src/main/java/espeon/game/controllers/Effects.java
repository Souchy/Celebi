package espeon.game.controllers;

import espeon.game.controllers.ActionPipeline.EffectAction;
import espeon.game.jade.Mod;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.red.Board;
import espeon.game.red.Creature;
import espeon.game.red.Stats;
import espeon.game.red.Board.Cell;
import espeon.game.red.compiledEffects.CompiledDamage;
import espeon.game.red.compiledEffects.CompiledEffect;

public class Effects {

    public static CompiledEffect compile(ActionPipeline p, EffectAction action) {
        CompiledEffect e = switch(action.effect.type()) {
            case damage -> applyDamage(action, (DamageEffect) action.effect);
            case flee -> null;
            case heal -> null;
            case move -> null;
            case status -> null;
            case summon -> null;
        };
        return e;
        // throw new IllegalArgumentException("Illegal effect type: " + action.effect.type());
    }

    public static CompiledEffect applyDamage(EffectAction p, DamageEffect e) {
        // Board.
        Fight fight = Diamonds.getFightByClient(p.sourceid);
        Board board = fight.board;
        Cell cell = board.get(p.cellid);

        int creatureid = cell.getBottom();
        if(creatureid == Creature.noid) {
            System.err.printf("Effects.applyDamage cell [%s] {%s, %s} has no creature.\n", p.cellid, cell.getX(), cell.getY());
            return null;
        }
        // Creature creatureTarget = Diamonds.getCreatureInstance(creatureid);
        // if(creatureTarget == null) return null;

        // Stats stats = p.stats.get(creatureid);
        // int hp = stats.get(Mod.hp);
        int hp = p.getStat(creatureid, Mod.hp);
        int defense = p.getStat(creatureid, Mod.defense);

        int dmg = e.power - defense;
        p.setStat(creatureid, Mod.hp, hp - dmg);

        var compiled = new CompiledDamage();
        compiled.creatureid = creatureid;
        compiled.damage = dmg;
        compiled.sourceid = p.sourceid;
        return compiled;
    }

}
