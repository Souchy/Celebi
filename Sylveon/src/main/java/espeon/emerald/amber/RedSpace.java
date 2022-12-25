package espeon.emerald.amber;

import java.util.Set;

import espeon.emerald.Red;

public abstract class RedSpace {
    final String url;
    protected RedSpace(String url){
        this.url = url;
    }
    /**
     * Set of IDs of instances in this space
     */
    public Set<String> keys() {
        return Red.jedis.smembers(url + "keys");
    }
    /**
     * Add the id to the instance ID Set
     */
    public void create(String id) {
        Red.jedis.set(url + id, "");
    }
    /**
     * Keys for the instance given
     */
    public Set<String> keys(String id) {
        return Red.jedis.keys(url + id + ":*");
    }
    /**
     * Delete all keys for the instance and remove it from the instance Set
     */
    public void delete(String id) {
        Red.jedis.srem(url + "keys", id);
        for(var k : keys(id))
            Red.jedis.del(k);
    }
}
