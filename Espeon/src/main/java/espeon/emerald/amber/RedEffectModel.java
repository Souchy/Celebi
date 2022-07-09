package espeon.emerald.amber;

import java.util.Map;

import espeon.game.jade.EffectModel;
import espeon.game.red.Aoe;
import espeon.game.types.EffectType;

public final class RedEffectModel {
    private static final String url = Amber.redPath + "effect:";
    private RedEffectModel() {}

    public Class<? extends EffectModel> clazz(int id) {
        return null;
    }
    public EffectType type(int id) {
        return null;
    }
    public Aoe getAoe(int id) {
        return null;
    }
    public Map<String, String> properties(int id) {
        return null;
    }
    public String getProperty(int id, String property) {
        return "";
    }
}
