package espeon.game.jade;

import espeon.game.jade.Target.TargetTypeFilter;
import espeon.game.red.Aoe;

public class EffectModel {

    public int id;
    public Aoe aoe = new Aoe();
    public TargetTypeFilter filter;

    public EffectType type() {
        return EffectType.valueOf(id);
    }

}
