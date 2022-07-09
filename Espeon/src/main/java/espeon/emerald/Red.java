package espeon.emerald;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import espeon.game.jade.Mod;
import espeon.game.red.Stats;
import espeon.game.red.Entity.EntityType;
import redis.clients.jedis.JedisPooled;

/**
 * Represents a fight scope
 */
public class Red {
    
    /** fights by id */
    public static final Map<Integer, Red> redFights = new HashMap<>();
    public static final JedisPooled jedis = new JedisPooled("localhost", 7379);


    public final int fightid;
    public final String redPath;
    public Red(int fightid) {
        this.fightid = fightid;
        this.redPath = "celebi:fight:" + fightid;
    }

    // public int getEntityStatId(int entityid) {
    //     return 0;
    // }
    // public int getStatusStatId(int statusid) {
    //     return 0;
    // }
    // public int getStatValue(int statsId, Mod m) {
    //     return 0;
    // }

    private final RedCreature creatureAccessor = new RedCreature();
    public RedCreature getCreature() {
        return creatureAccessor;
    }

    private final RedEntity entityAccessor = new RedEntity();
    public RedEntity getEntity() {
        return entityAccessor;
    }


    public sealed class RedEntity {
        private RedEntity() {}
        public void getFightid(int id) {
            jedis.get(redPath + ":entity:" + id + ":ownerid");
        }
        public void getStatusList(int id) {
            jedis.get(redPath + ":entity:" + id + ":ownerid");
        }
        public EntityType getType(int id) {
            String value = jedis.get(redPath + ":entity:" + id + ":type");
            return EntityType.valueOf(value);
        }
    }

    public final class RedCreature extends RedEntity {
        private RedCreature() {}
        private String creaturePath(int id) {
            return redPath + ":creature:" + id;
        }
        public String getOwnerid(int id) {
            return jedis.get(creaturePath(id) + ":ownerid");
        }
        public String getModelid(int id) {
            return jedis.get(creaturePath(id) + ":modelid");
        }
        public Map<String, String> getStats(int id) {
            return jedis.hgetAll(creaturePath(id) + ":stats");
        }
        public String getStatValue(int id, Mod m) {
            return jedis.hget(creaturePath(id) + ":stats", m.name());
        }
        public List<String> getSpells(int id) {
            return jedis.lrange(creaturePath(id) + ":spells", 0, -1);
        }
        public void newCreature(int id, int modelid, Stats stats, List<Integer> spells) {

        }
        public void delete(int id) {

        }
    }

    public final class RedSpell {
        public String getTurnLastCast(int id) {
            return "";
        }
        public String getCastsThisTurn(int id) {
            return "";
        }
        public int getCastsPerTarget(int id, int target) {
            return 0;
        }
    }


    // public final class RedStats {
    //     public int get(int id, Mod m) {
    //         jedis.get(redPath + ":stats:" + id + ":" + m.toString());
    //     }
    // }

}
