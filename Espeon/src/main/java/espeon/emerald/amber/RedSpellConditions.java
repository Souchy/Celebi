package espeon.emerald.amber;

import espeon.emerald.Red;
import espeon.game.jade.Position;
import espeon.game.red.Aoe;
import espeon.util.Util;

public final class RedSpellConditions { // extends RedSpace {
    RedSpellConditions() {}
    private String url(int id) {
        return Amber.spells.url + id + ":conditions:";
    }

    // aoe cell conditions
    public Aoe getCellConditions(int id) {
        String origin = Red.jedis.get(url(id) + "origin");
        Position pos = new Position();
        // Red.jedis.rget
        // Aoe aoe = new Aoe();

        return null;
    }
    public void setCellConditions(int id, Aoe aoe) {
        Red.jedis.set(url(id) + "aoewidth", String.valueOf(aoe.getWidth()));
        Red.jedis.rpush(url(id) + "aoe", aoe.toStringArray());
        Red.jedis.set(url(id) + "origin", aoe.origin.toString());
    }

    public int getMaxCastsPerTarget(int id) {
        return Util.parseInt(Red.jedis.get(url(id) + "maxCastsPerTarget"));
    }
    public int getMaxCastsPerTurn(int id) {
        return Util.parseInt(Red.jedis.get(url(id) + "maxCastsPerTurn"));
    }
    public int getCooldown(int id) {
        return Util.parseInt(Red.jedis.get(url(id) + "cooldown"));
    }

    public void setMaxCastsPerTarget(int id, int val) {
        Red.jedis.set(url(id) + "maxCastsPerTarget", String.valueOf(val));
    }
    public void setMaxCastsPerTurn(int id, int val) {
        Red.jedis.set(url(id) + "maxCastsPerTurn", String.valueOf(val));
    }
    public void setCooldown(int id, int val) {
        Red.jedis.set(url(id) + "cooldown", String.valueOf(val));
    }
}
