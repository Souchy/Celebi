package espeon.game.controllers;

import java.util.HashMap;
import java.util.Map;

import org.checkerframework.checker.units.qual.s;

import espeon.game.jade.SpellModel;
import espeon.game.red.Creature;
import espeon.game.red.Entity;
import espeon.game.red.Spell;

public class Diamonds {

    public static Entity getEntity(int id) {
        return creatures.get(id);
    }
    public static Creature getCreatureInstance(int id) {
        return creatures.get(id);
    }
    public static SpellModel getSpellModel(int id) {
        return spellModels.get(id);
    }
    public static Spell getSpell(int id) {
        return spells.get(id);
    }
    public static Fight getFightByClient(int clientid) {
        // get the fight by client.fightid
        return fights.get(clientid);
    }
    // public static SpellModel getStatusModel(int id) {
    //     return statusModels.get(id);
    // }


    public static void setCreature(int id, Creature c) {
        creatures.put(id, c);
    }
    public static void setSpell(int id, Spell s) {
        spells.put(id, s);
    }
    public static void setSpellModel(int id, SpellModel s) {
        spellModels.put(id, s);
    }
    public static void setFightClient(int id, Fight f) {
        // set fight id in the client's memory
        fights.put(id, f);
    }


    // Secret database simulation before we set up redis
    private static Map<Integer, Spell> spells = new HashMap<>();
    private static Map<Integer, SpellModel> spellModels = new HashMap<>();
    private static Map<Integer, Creature> creatures = new HashMap<>();
    private static Map<Integer, Fight> fights = new HashMap<>();

}
