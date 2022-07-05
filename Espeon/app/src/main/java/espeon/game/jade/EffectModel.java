package espeon.game.jade;

import espeon.game.red.Aoe;
import espeon.game.types.EffectType;

public abstract class EffectModel {

    // public int id;
    public Aoe aoe = new Aoe();
    // public TargetTypeFilter filter;

    public abstract EffectType type(); // {
        // return EffectType.valueOf(id);
    // }

}
