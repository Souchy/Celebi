package espeon.game.jade.effects;

import espeon.game.jade.EffectModel;
import espeon.game.types.EffectType;

public class SummonEffect extends EffectModel {
    
    public int creatureId;

    @Override
    public EffectType type() {
        return EffectType.summon;
    }
    

}
