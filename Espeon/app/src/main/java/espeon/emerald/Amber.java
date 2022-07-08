package espeon.emerald;

import java.util.Map;

import espeon.game.jade.EffectModel;
import espeon.game.jade.Mod;
import espeon.game.jade.Position;
import espeon.game.red.Aoe;
import espeon.game.types.EffectType;

public class Amber {
    
    public static final String redPath = "celebi:amber:";

    public static final RedSpellModel spellModel = new RedSpellModel();
    
    public static final class RedSpellModel {
        private static final String url = redPath + "spell:";
        /**
         * Keys are range patterns (circle, square, line, ring, square ring..) <br>
         * Values are the size/range of each pattern
         */
        // public Map<String, String> getRange(int id) {
        //     return Red.jedis.hgetAll(url + id + ":range");
        // }
        public String getRange(int id) {
            return Red.jedis.get(url + id + ":range");
        }
        public void setRange(int id, String range) {
            Red.jedis.set(url + id + ":range", range);
        }

        public Map<String, String> getCosts(int id) {
            return Red.jedis.hgetAll(url + id + ":costs");
        }
        public void setCosts(int id, Map<Mod, String> costs) {
            Red.jedis.del(url + id + ":costs");
            for(var entry : costs.entrySet())
                setCost(id, entry.getKey(), entry.getValue());
        }
        public String getCost(int id, Mod mod) {
            return Red.jedis.hget(url + id + ":costs", mod.name());
        }
        public void setCost(int id, Mod m, String value) {
            Red.jedis.hset(url + id + ":costs", m.name(), value);
        }

        public String getActionId(int id) {
            return Red.jedis.get(url + id + ":action");
        }
        public void setActionId(int id, String actionid) {
            Red.jedis.set(url + id + ":action", actionid);
        }
        // public void setSpellModel(int range, Map<String, String> costs, int actionid) {
        //     Red.jedis.set(key, value)
        // }
        public void delete(int id) {
            var keys = Red.jedis.keys(url + id + ":*");
            for(var k : keys)
                Red.jedis.del(k);
            // Red.jedis.del(keys.toArray(new String[keys.size()]));
        }
    }
    public static final class RedSpellConditions {
        private static final String url = RedSpellModel.url;
        public Aoe getCellConditions(int id) {
            String origin = Red.jedis.get(url + id + ":cellConditions:origin");
            Position pos = new Position();
            // Red.jedis.rget
            // Aoe aoe = new Aoe();

            return null;
        }
        public void setCellConditions(int id, Aoe aoe) {
            Red.jedis.set(url + id + ":cellConditions:origin", aoe.origin.toString());
            Red.jedis.rpush(url + id + ":cellConditions:aoe", aoe.toStringArray());
        }
        public int getMaxCastsPerTarget(int id) {
            return Integer.parseInt(Red.jedis.get(url + id + ":maxCastsPerTarget"));
        }
        public int getMaxCastsPerTurn(int id) {
            return Integer.parseInt(Red.jedis.get(url + id + ":maxCastsPerTurn"));
        }
        public int getCooldown(int id) {
            return Integer.parseInt(Red.jedis.get(url + id + ":cooldown"));
        }
        public void setMaxCastsPerTarget(int id, int val) {
            Red.jedis.set(url + id + ":maxCastsPerTarget", String.valueOf(val));
        }
        public void setMaxCastsPerTurn(int id, int val) {
            Red.jedis.set(url + id + ":maxCastsPerTurn", String.valueOf(val));
        }
        public void setCooldown(int id, int val) {
            Red.jedis.set(url + id + ":cooldown", String.valueOf(val));
        }
    }
    public static final class RedEffectModel {
        private static final String url = redPath + "effect:";
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

    public static final class RedAction {
        private static final String url = redPath + "action:";
        public void getStatements(int id) {
            // return 0;
        }
    }
    public static final class RedStatement {

    }

    private static final class RedAoe {
        public Aoe getAoe() {
            return null;
        }
    }
    
}
