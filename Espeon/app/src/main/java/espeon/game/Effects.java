package espeon.game;

import espeon.game.jade.Mod;
import espeon.game.jade.effects.DamageEffect;
import espeon.game.red.ActionPipeline;
import espeon.game.red.ActionPipeline.EffectAction;

public class Effects {

    

    public static void apply(EffectAction action) {
        // if(action.effect instanceof DamageEffect e) {
        //     applyDamage(action, e);
        // }
        switch(action.effect.type()) {
            case damage:
                applyDamage(action, (DamageEffect) action.effect);
                break;
            case flee:
                break;
            case heal:
                break;
            case move:
                break;
            case status:
                break;
            case summon:
                break;
        }
        throw new IllegalArgumentException("Illegal effect type: " + action.effect.type());
    }

    public static void applyDamage(EffectAction p, DamageEffect e) {
        // Board.
        int entityid = 0; // Board.get1stCreatureOnCell()
        int dmg = e.power;
        int hp = p.stats.get(entityid).stats.get(Mod.hp);
        p.setStat(entityid, Mod.hp, hp - dmg);
    }

}
