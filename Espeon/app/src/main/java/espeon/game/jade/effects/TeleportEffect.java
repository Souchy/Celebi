package espeon.game.jade.effects;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;

public class TeleportEffect extends EffectModel {

    @Override
    public EffectType type() {
        return EffectType.move;
    }
    
}
