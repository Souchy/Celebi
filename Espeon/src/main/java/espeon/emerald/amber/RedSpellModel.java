package espeon.emerald.amber;

import java.util.Map;

import espeon.emerald.Red;
import espeon.game.jade.Mod;
import espeon.util.Util;

public final class RedSpellModel extends RedSpace {
    RedSpellModel() {
        super(Amber.redPath + "spell:");
    }
    public final RedSpellConditions conditions = new RedSpellConditions();
    
    // public void setSpellModel(int range, Map<String, String> costs, int actionid) {
    //     Red.jedis.set(key, value)
    // }
    public String getActionId(String id) {
        return Red.jedis.get(url + id + ":action");
    }
    public void setActionId(String id, String actionid) {
        Red.jedis.set(url + id + ":action", actionid);
    }

    /**
     * Keys are range patterns (circle, square, line, ring, square ring..) <br>
     * Values are the size/range of each pattern
     */
    // public Map<String, String> getRange(int id) {
    //     return Red.jedis.hgetAll(url + id + ":range");
    // }
    public Integer getRange(String id) {
        var str = Red.jedis.get(url + id + ":range");
        if(str == null) return null;
        return Util.parseInt(str);
    }
    public void setRange(String id, int range) {
        Red.jedis.set(url + id + ":range", String.valueOf(range));
    }

    public Map<String, String> getCosts(String id) {
        return Red.jedis.hgetAll(url + id + ":costs");
    }
    public void setCosts(String id, Map<Mod, String> costs) {
        Red.jedis.del(url + id + ":costs");
        for(var entry : costs.entrySet())
            setCost(id, entry.getKey(), entry.getValue());
    }
    public String getCost(String id, Mod mod) {
        return Red.jedis.hget(url + id + ":costs", mod.name());
    }
    public void setCost(String id, Mod m, String value) {
        Red.jedis.hset(url + id + ":costs", m.name(), value);
    }
    public void deleteCost(String id, Mod mod) {
        Red.jedis.hdel(url + id + ":costs", mod.name());
    }

}
