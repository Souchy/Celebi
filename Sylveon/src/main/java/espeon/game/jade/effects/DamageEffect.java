package espeon.game.jade.effects;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;

public class DamageEffect extends EffectModel {
    
    public int power;

    @Override
    public EffectType type() {
        return EffectType.damage;
    }

}
